using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl
{
    public partial class DsLoanpay : DataSourceFormView
    {
        public DataSet1.LNREQCONTADJUSTDETDataTable DATA { get; private set; }

        public void InitDsLoanpay(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQCONTADJUSTDET;
            this.InitDataSource(pw, FormView1, this.DATA, "dsLoanpay");
            this.EventItemChanged = "OnDsLoanpayItemChanged";
            this.EventClicked = "OnDsLoanpayClicked";
            //this.Button.Add("b_search");
            this.Register();
        }

        public void DdBank()
        {
            string sql = @"
            select  bank_code,bank_code  +' '+  bank_desc  as bank_desc, 1 as sorter from cmucfbank 
            union 
            select '','', 0  order by sorter, bank_desc
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loanpay_bank", "bank_desc", "bank_code");
        }

        public void DdBranch(string bank_code)
        {
            string sql = @"
            select bank_code,branch_id, branch_name, 1 as sorter from cmucfbankbranch where bank_code = {0} 
            union
            select '', '','', 0  order by sorter, branch_id
            ";
            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loanpay_branch", "branch_name", "branch_id");
        }
    }
}