using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SLSLIPPAYOUTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYOUT;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView2, this.DATA, "dsMain");
            this.Button.Add("b_sbranch");
            this.Button.Add("b_deptacc");
            this.Register();
        }

        public void RetrieveMemberName(String member_no)
        {
            string sql = @"select member_no, FT_GETMEMNAME(coop_id, member_no) as name,
                member_type, trim(membgroup_code) || ' ' || FT_MEMGRP(coop_id, membgroup_code) as membgroup
                from mbmembmaster where coop_id = {0} and member_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void RetrieveLnreqloan(String member_no)
        {
            String sql = @"SELECT 'CON' as RCVFROMREQCONT_CODE,  
                    LNCONTMASTER.COOP_ID,   
                    LNCONTMASTER.LOANCONTRACT_NO,   
                    LNCONTMASTER.MEMBER_NO,   
                    LNCONTMASTER.WITHDRAWABLE_AMT,   
                    LNLOANTYPE.PREFIX,   
                    MBUCFPRENAME.PRENAME_DESC,   
                    MBMEMBMASTER.MEMB_NAME,   
                    MBMEMBMASTER.MEMB_SURNAME,   
                    MBMEMBMASTER.MEMBGROUP_CODE,   
                    0 as operate_flag,
                    1 as sorter  
            FROM LNCONTMASTER,   
                    MBMEMBMASTER,   
                    MBUCFPRENAME,   
                    LNLOANTYPE  
            WHERE ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
                    ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                    ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
                    ( MBMEMBMASTER.COOP_ID = LNCONTMASTER.MEMCOOP_ID ) and  
                    ( LNLOANTYPE.COOP_ID = LNCONTMASTER.COOP_ID) and  
                    ( ( LNCONTMASTER.COOP_ID = {0} ) AND 
                    ( LNCONTMASTER.MEMBER_NO = {1} ) AND
                    ( ( lncontmaster.withdrawable_amt > 0 ) AND  
                    ( lncontmaster.contract_status > 0 ) ) AND  
                    ( LNCONTMASTER.LOANCONTRACT_NO not in ( SELECT SLSLIPPAYOUT.LOANCONTRACT_NO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )   
            UNION   
            SELECT 'REQ' as RCVFROMREQCONT_CODE,
                    LNREQLOAN.COOP_ID,   
                    LNREQLOAN.LOANREQUEST_DOCNO,   
                    LNREQLOAN.MEMBER_NO,   
                    LNREQLOAN.LOANAPPROVE_AMT,   
                    LNLOANTYPE.PREFIX,   
                    MBUCFPRENAME.PRENAME_DESC,   
                    MBMEMBMASTER.MEMB_NAME,   
                    MBMEMBMASTER.MEMB_SURNAME,   
                    MBMEMBMASTER.MEMBGROUP_CODE,   
                    0 as operate_flag,
                    2 as sorter  
            FROM LNREQLOAN,   
                    MBMEMBMASTER,   
                    MBUCFPRENAME,   
                    LNLOANTYPE  
            WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
                    ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                    ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
                    ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
                    ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                    ( ( LNREQLOAN.MEMCOOP_ID = {0} ) AND
                    ( LNREQLOAN.MEMBER_NO = {1} ) AND
                    ( ( lnreqloan.loanrequest_status = 11 ) ) AND  
                    ( LNREQLOAN.LOANREQUEST_DOCNO not in ( SELECT SLSLIPPAYOUT.LOANREQUEST_DOCNO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )))
            union 
            select '','','','', 0,'','','','','', 0, 0 as sorter from dual order by sorter";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
            DataTable dt = WebUtil.Query(sql);
            //this.ImportData(dt);
            this.DropDownDataBind(dt, "loancontract_no", "LOANCONTRACT_NO", "LOANCONTRACT_NO");
        }

        public void DdLoanType()
        {
            string sql = @" 
                SELECT LOANTYPE_CODE,   
                       LOANTYPE_DESC,
                        LOANTYPE_CODE || ' - ' || LOANTYPE_DESC as LOANTYPE_DISPLAY
                  FROM LNLOANTYPE order by  LOANTYPE_CODE
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "shrlontype_code", "LOANTYPE_DISPLAY", "LOANTYPE_CODE");

        }

        public void DdMoneyType()
        {
            string sql = @" 
                   SELECT MONEYTYPE_CODE,   
                          MONEYTYPE_DESC,   
                          SORT_ORDER  ,
                          MONEYTYPE_CODE || ' - '|| MONEYTYPE_DESC as MONEYTYPE_DISPLAY
                     FROM CMUCFMONEYTYPE where MONEYTYPE_STATUS = 'DAY' order by MONEYTYPE_CODE
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MONEYTYPE_CODE", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");

        }

        public void DdBankDesc()
        {
            string sql = @"
            select  bank_code,bank_code|| ' '||bank_desc  as bank_desc, 1 as sorter from cmucfbank 
            union 
            select '','', 0 from dual order by sorter, bank_desc
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_bank", "bank_desc", "bank_code");
        }

        public void DdBranch(string bank_code)
        {
            string sql = @"
            select bank_code,branch_id, branch_name, 1 as sorter from cmucfbankbranch where bank_code = {0} 
            union
            select '', '','', 0 from dual order by sorter, branch_name
            ";
            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_branch", "branch_name", "branch_id");
        }

        public void DdBranchLike(string bank_code, string branch_name)
        {
            string sql = @"
            select bank_code,branch_id, branch_name from cmucfbankbranch where bank_code = {0} and branch_name like {1}
            union
            select '', '','' from dual order by branch_name
            ";
            sql = WebUtil.SQLFormat(sql, bank_code, "%" + branch_name + "%");
            DataTable dt = WebUtil.Query(sql);

            this.DropDownDataBind(dt, "expense_branch", "branch_name", "branch_id");
            this.SetItem(0, "expense_branch", Convert.ToString(dt.Rows[0]["branch_id"]));
        }

        public void DdFromAccId(string sliptype_code, string moneytype_code)
        {
            string sql = @"
                   SELECT 
                        CMUCFTOFROMACCID.MONEYTYPE_CODE, 
			             CMUCFTOFROMACCID.ACCOUNT_ID,    
			             CMUCFTOFROMACCID.ACCOUNT_ID ||'-'||ACCMASTER.ACCOUNT_NAME AS fromacc_display,
                  		 ACCMASTER.ACCOUNT_NAME  ,  
                         1 as sorter
                 FROM ACCMASTER,   
        		      CMUCFTOFROMACCID  
                 WHERE 
		              ( ACCMASTER.COOP_ID = CMUCFTOFROMACCID.COOP_ID )  
                 and  (CMUCFTOFROMACCID.ACCOUNT_ID =ACCMASTER.ACCOUNT_ID)
                 and (CMUCFTOFROMACCID.SLIPTYPE_CODE = '" + sliptype_code + "') and (CMUCFTOFROMACCID.MONEYTYPE_CODE = '" + moneytype_code + @"')
                union select '','','','',0 from dual order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "fromacc_display", "ACCOUNT_ID");
        }
    }
}