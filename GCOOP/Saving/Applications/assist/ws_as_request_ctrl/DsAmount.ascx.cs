using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_request_ctrl
{
    public partial class DsAmount : DataSourceFormView
    {
        public DataSet1.ASSREQMASTERDataTable DATA { get; set; }
        public void InitDsAmount(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQMASTER;
            this.EventItemChanged = "OnDsAmountItemChanged";
            this.EventClicked = "OnDsAmountClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsAmount");
            this.Button.Add("b_search");
            this.Register();
        }

        public void Retrieve(String as_reqno)
        {
            string sql = @"select * from assreqmaster where coop_id = {0} and assist_docno = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_reqno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        
        public void RetrieveBank()
        {
            string sql = @"select bank_code,   
                bank_desc,   
                bank_code+'-'+bank_desc as display  ,1 as sorter
                from cmucfbank
            union
            select '','','เลือกธนาคาร',0  order by sorter,bank_code asc ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_bank", "display", "bank_code");
        }
        public void RetrieveBranch(String bank)
        {
            string sql = @"select branch_id,   
                        branch_name,   
                        branch_id + '-' + branch_name as display  
                        from cmucfbankbranch where bank_code={0}
                        order by branch_id asc ";
            sql = WebUtil.SQLFormat(sql, bank.Trim());
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_branch", "display", "branch_id");
        }
       
        public void RetrieveMoneyType()
        {
            string sql = @" 
                   SELECT MONEYTYPE_CODE,   
                          MONEYTYPE_DESC,   
                          SORT_ORDER  ,
                          MONEYTYPE_CODE + ' - '+ MONEYTYPE_DESC as MONEYTYPE_DISPLAY
                     FROM CMUCFMONEYTYPE where MONEYTYPE_CODE in ('CSH','CHQ','CBT','CBO','TRN') order by sort_order
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "moneytype_code", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");
        }

        public void RetrieveDeptaccount(string memno)
        {
            string sql = @" select deptaccount_no, deptaccount_desc from
                        (
	                        select deptaccount_no, deptaccount_no deptaccount_desc, depttype_code from dpdeptmaster where deptclose_status = 0 and coop_id = {0} and member_no = {1}
	                        union
	                        select top 1  '', 'กรุณาเลือกบัญชี', '00' depttype_code from dpdeptmaster 
                        )as  deptaccount_desc
                        order by depttype_code, deptaccount_no ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, memno);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "deptaccount_no", "deptaccount_desc", "deptaccount_no");
        }

        public void DddeptaccountnoDlg(string memno, string deptaccount_no)
        {
            string sql = @" select deptaccount_no, deptaccount_desc from
                        (
	                        select deptaccount_no, deptaccount_no deptaccount_desc, depttype_code from dpdeptmaster 
                            where deptclose_status = 0 and coop_id = {0} and member_no = {1} or deptaccount_no in ({2})
	                        union
	                        select top 1 '', 'กรุณาเลือกบัญชี', '00' depttype_code from dpdeptmaster 
                        )as deptaccount_desc
                        order by depttype_code, deptaccount_no ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, memno, deptaccount_no);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "deptaccount_no", "deptaccount_desc", "deptaccount_no");
        }

        
    }
}