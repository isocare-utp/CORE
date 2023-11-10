using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.w_sheet_finslip_spc_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FINSLIPDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINSLIP;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_member");
            this.Button.Add("b_accid");
            this.Register();
        }
        public void RetrieveFinslip(string coop_id, string slipno)
        {
            string sql = @"SELECT FINSLIP.SLIP_NO,   
                     FINSLIP.ENTRY_ID,   
                     FINSLIP.ENTRY_DATE,   
                     FINSLIP.OPERATE_DATE,   
                     FINSLIP.CASH_TYPE,   
                     FINSLIP.PAYMENT_STATUS,   
                     FINSLIP.PAYMENT_DESC,   
                     FINSLIP.ITEMPAY_AMT,   
                     FINSLIP.ITEMPAYTYPE_CODE,   
                     FINSLIP.PAY_RECV_STATUS,   
                     FINSLIP.MEMBER_FLAG,   
                     FINSLIP.NONMEMBER_DETAIL,   
                     FINSLIP.COOP_ID,   
                     FINSLIP.MACHINE_ID,   
                     FINSLIP.CANCEL_ID,   
                     FINSLIP.CANCEL_DATE,   
                     FINSLIP.RECEIPT_NO,   
                     FINSLIP.POSTTOVC_FLAG,   
                     FINSLIP.FORACC_FLAG,   
                     FINSLIP.TAX_FLAG,   
                     FINSLIP.TAX_AMT,   
                     FINSLIP.RECEIVE_STATUS,   
                     FINSLIP.RECEIVE_DATE,   
                     FINSLIP.ITEM_AMTNET,   
                     FINSLIP.TAX_RATE,   
                     FINSLIP.DES_ACCID,   
                     FINSLIP.RECVPAY_ID,   
                     FINSLIP.RECVPAY_TIME,   
                     FINSLIP.CANCEL_BYFIN,   
                     FINSLIP.VOUCHER_NO,   
                     FINSLIP.MEMBER_NO,   
                     FINSLIP.TOFROM_ACCID,   
                     FINSLIP.RETAIL_FLAG,   
                     FINSLIP.FROM_BANKCODE,   
                     FINSLIP.FROM_BRANCHCODE,   
                     FINSLIP.FROM_ACCNO,   
                     FINSLIP.CHEQUEBOOK_NO,   
                     FINSLIP.PAY_TOWHOM,   
                     FINSLIP.DATEON_CHQ,   
                     FINSLIP.BANK_CODE,   
                     FINSLIP.BANK_BRANCH,   
                     FINSLIP.ACCOUNT_NO,   
                     FINSLIP.CHQ_ADVFLAG,   
                     FINSLIP.CHEQUE_STATUS,   
                     FINSLIP.FROM_SYSTEM,   
                     FINSLIP.REMARK,   
                     FINSLIP.REF_SYSTEM,   
                     FINSLIP.REF_SLIPNO,   
                     FINSLIP.BANKFEE_AMT,   
                     FINSLIP.BANKSRV_AMT,   
                     FINSLIP.TAX_CODE,   
                     FINSLIP.CHEQUE_TYPE,   
                     FINSLIP.SHARESPX_AMT,   
                     FINSLIP.ACCUINT_AMT,   
                     FINSLIP.LOANAPPV_AMT,   
                     FINSLIP.NORM_AMT,   
                     FINSLIP.SHARE_AMT,   
                     FINSLIP.EMER_AMT,   
                     FINSLIP.EXTRA_AMT,   
                     FINSLIP.RETAIL_POST,   
                     FINSLIP.RETAIL_DATE,   
                     FINSLIP.TAXWAYKEEP,   
                     FINSLIP.PRINT_STATUS,   
                     FINSLIP.PAYSLIP_NO,   
                     FINSLIP.TRANSDEPT_FLAG,   
                     FINSLIP.SENDTO_SYSTEM,   
                     FINSLIP.REFENTRY_ID,   
                     FINSLIP.REFENTRY_DATE,   
                     FINSLIP.MEMBGROUP_CODE,   
                     0 as vat_flag,   
                     FINSLIP.VAT_AMT  
                FROM FINSLIP  
               WHERE ( FINSLIP.SLIP_NO = {1} ) AND  
                     ( FINSLIP.COOP_ID = {0} )    ";
            sql = WebUtil.SQLFormat(sql, coop_id, slipno);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
        
        public void DDMoney()
        {
            string sql = @"
                SELECT MONEYTYPE_CODE,MONEYTYPE_DESC,SORT_ORDER as sorter
                FROM CMUCFMONEYTYPE WHERE CMUCFMONEYTYPE.MONEYTYPE_STATUS = 'DAY'    
                order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "CASH_TYPE", "MONEYTYPE_DESC", "MONEYTYPE_CODE");
        }
        public void DDAccid()
        {
            string sql = @"
                SELECT ACCOUNT_ID,ACCOUNT_NAME  ,
                ACCOUNT_ID + ' ' +ACCOUNT_NAME as fullname,
                ACCOUNT_ID as sorter
                FROM ACCMASTER WHERE 
		                 ( ACCMASTER.ACCOUNT_TYPE_ID = '3' ) AND  
                         ( ACCMASTER.ACTIVE_STATUS = 1 ) AND  
                         ( ACCMASTER.COOP_ID = {0} ) 
                order by sorter";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "TOFROM_ACCID", "fullname", "ACCOUNT_ID");
        }
        public void DDTaxType()
        {
            string sql = @"
                SELECT FINTAXTYPE.TAX_CODE,   
                 FINTAXTYPE.TAX_DESC+' | ' + CONVERT(VARCHAR(10),round(FINTAXTYPE.TAX_RATE*100,2)) +' .00%' as fullname,    
                FINTAXTYPE.TAX_RATE,   
                FINUCFICTAXTYPE.ICTAXTYPE_DESC  
                FROM FINTAXTYPE,   
                FINUCFICTAXTYPE  
                   WHERE ( FINTAXTYPE.ICTAXTYPE_CODE = FINUCFICTAXTYPE.ICTAXTYPE_CODE )    
                order by FINTAXTYPE.TAX_CODE";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "TAX_CODE", "fullname", "TAX_CODE");
        }
        public void DDBankCode()
        {
            string sql = @"SELECT CMUCFBANK.BANK_CODE,   
                            CMUCFBANK.BANK_CODE+' '+ CMUCFBANK.BANK_DESC as BANK_DESC,   
                            CMUCFBANK.EDIT_FORMAT  ,
	                    1 as sorter
                    FROM CMUCFBANK   
                    union
                    select '', '','', 0  order by BANK_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "BANK_CODE", "BANK_DESC", "BANK_CODE");
        }
        public void DDBankBranch(string bank_code)
        {
            string sql = @"			SELECT 
					CMUCFBANKBRANCH.BANK_CODE,   
                    CMUCFBANKBRANCH.BRANCH_ID,   
                    CMUCFBANKBRANCH.BRANCH_ID +' '+CMUCFBANKBRANCH.BRANCH_NAME as BRANCH_NAME   ,
                    1 as sorter
                    FROM CMUCFBANKBRANCH  
                    WHERE CMUCFBANKBRANCH.BANK_CODE = {0} 
                    union
                    select '', '','', 0  order by BANK_CODE,BRANCH_ID";
            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "BANK_BRANCH", "BRANCH_NAME", "BRANCH_ID");
        }
    }
}   
