using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_as_expense_detail_ctrl
{
    public partial class DsList : DataSourceFormView
    {
        public DataSet1.DsListDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsList;
            this.InitDataSource(pw, FormView1, this.DATA, "dsList");
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.Register();
        }
        public void DdBankDesc()
        {
            string sql = @"
            select  bank_code,rtrim(ltrim(bank_code)) +  '-' + rtrim(ltrim(bank_desc))  as bank_desc, 1 as sorter from cmucfbank 
            union 
            select '','---กรุณาเลือกธนาคาร---', 0 order by sorter, bank_desc
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }

        public void DdBranch(string bank_code)
        {
            string sql = @"
            select bank_code,branch_id, branch_id + '-' + branch_name as branch_name, 1 as sorter from cmucfbankbranch where bank_code = {0} 
            union
            select '', '','---กรุณาเลือกสาขา---', 0 order by sorter, branch_id
            ";
            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "branch_code", "branch_name", "branch_id");
        }

        public void DdFromAccId(string moneytype)
        {
            string sql = @"select tf.account_id, tf.account_id+':'+ac.account_name as fromacc_display
                            from	( select distinct account_id from cmucftofromaccid where applgroup_code = 'SLN' and sliptype_code = 'LWD' and moneytype_code = {0}) tf 
		                            join accmaster ac on tf.account_id = ac.account_id
                            order by tf.account_id 
                ";
            sql = WebUtil.SQLFormat(sql, moneytype);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "fromacc_display", "account_id");
        }

        public void RetrieveDeptaccount(string memno , ref string min_deptno)
        {
            string sql = @"select deptaccount_no, depttype_code from dpdeptmaster where deptclose_status = 0 and coop_id = {0} and member_no = {1}
                        order by depttype_code, deptaccount_no ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, memno);
            DataTable dt = WebUtil.Query(sql);
            min_deptno = dt.Rows[0].Field<string>("deptaccount_no");
            this.DropDownDataBind(dt, "deptaccount_no", "deptaccount_no", "deptaccount_no");
        }
    }
}