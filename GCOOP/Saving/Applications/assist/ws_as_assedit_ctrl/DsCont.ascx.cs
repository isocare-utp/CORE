using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.assist.ws_as_assedit_ctrl
{
    public partial class DsCont : DataSourceFormView
    {
        public DataSet1.ASSCONTMASTERDataTable DATA { get; set; }
        public void InitDsCont(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSCONTMASTER;
            this.EventItemChanged = "OnDsContItemChanged";
            this.EventClicked = "OnDsContClicked";
            this.InitDataSource(pw, FormView2, this.DATA, "dsCont");
            this.Register();
        }

        public void RetrieveData(String ls_asscontno)
        {
            string sql = @"
                        select  ass.*
                        from	asscontmaster ass
                        where ass.coop_id = {0} 
                        and ass.asscontract_no = {1} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_asscontno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdAssistType()
        {
            string sql = @"select assisttype_code,   
                assisttype_desc,   
                assisttype_code||':'||assisttype_desc as display
                from assucfassisttype
                order by assisttype_code asc ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code", "display", "assisttype_code");
        }

        public void DdAssistPay(string as_asscode)
        {
            string sql = @"select assistpay_code,   
                assistpay_desc,   
                assistpay_code||':'||assistpay_desc as display
                from assucfassisttypepay
                where assisttype_code = '"+as_asscode+@"' 
                order by assistpay_code asc ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assistpay_code", "display", "assistpay_code");
        }

        public void DdBank()
        {
            string sql = @"select bank_code,   
                bank_desc,   
                bank_code||'-'||bank_desc as display  ,1 as sorter
                from cmucfbank
            union
            select '','','เลือกธนาคาร',0 from dual order by sorter,bank_code asc ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_bank", "display", "bank_code");
        }

        public void DdBranch(String as_bank)
        {
            string sql = @"select branch_id,   
                        branch_name,   
                        branch_name as display  
                        from cmucfbankbranch where bank_code={0}
                        union
                        select '','','เลือกสาขา' from dual
                        order by branch_id asc ";
            sql = WebUtil.SQLFormat(sql, as_bank.Trim());
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_branch", "display", "branch_id");
        }

        public void DdMoneyType()
        {
            string sql = @" 
                   SELECT MONEYTYPE_CODE,   
                          MONEYTYPE_DESC,   
                          SORT_ORDER  ,
                          MONEYTYPE_CODE || ' - '|| MONEYTYPE_DESC as MONEYTYPE_DISPLAY
                     FROM CMUCFMONEYTYPE where MONEYTYPE_CODE in ('CSH','CHQ','CBT','CBO','TRN') order by sort_order
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "moneytype_code", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");
        }

        public void DdDeptaccount(string as_memno)
        {
            string sql = @" select deptaccount_no, deptaccount_desc from
                        (
	                        select deptaccount_no, deptaccount_no deptaccount_desc, depttype_code from dpdeptmaster where deptclose_status = 0 and coop_id = {0} and member_no = {1}
	                        union
	                        select '', 'กรุณาเลือกบัญชี', '00' depttype_code from dpdeptmaster where rownum = 1
                        )
                        order by depttype_code, deptaccount_no ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_memno);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "deptaccount_no", "deptaccount_desc", "deptaccount_no");
        }

    }
}