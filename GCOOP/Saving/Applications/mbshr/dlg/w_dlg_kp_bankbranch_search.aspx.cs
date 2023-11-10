using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_kp_bankbranch_search : PageWebDialog, WebDialog
    {
        WebState state;
        protected String LoanContractSearch;
        DwTrans SQLCA;
        String ls_sql = "", ls_sqlext = "", ls_temp = "";


        protected void cb_find_Click(object sender, EventArgs e)
        {
            DwTrans SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_detail.SetTransaction(SQLCA);
            ls_sql = dw_detail.GetSqlSelect();
            string bank_code = "";
            string branch_name = "";
            string ls_bank = "";
            
            try {
                bank_code = Request["bank_code"];
            }
            catch
            { bank_code = ""; }
            
            String bank_desc = "";

           

             ls_bank = " where cmucfbankbranch.bank_code = '" + bank_code + "'";
            
          

            try
            {
                branch_name = dw_data.GetItemString(1, "branch_desc").Trim();

            }
            catch { branch_name = ""; }



            if (branch_name.Length > 0)
            {
                ls_sqlext = " and  cmucfbankbranch.branch_name like '%" + branch_name + "%'";
            }



           // ls_temp = ls_sql + ls_bank + ls_sqlext;

            //hidden_search.Value = ls_temp;
            //dw_detail.SetSqlSelect(hidden_search.Value); โค้ดเก่า
            // dw_detail.Retrieve();
            ls_temp = ls_sql + ls_bank + ls_sqlext;
            dw_detail.SetSqlSelect(ls_temp);
            dw_detail.Retrieve();
            SQLCA.Disconnect();
            
        }

        public void InitJsPostBack()
        {

            LoanContractSearch = WebUtil.JsPostBack(this, "LoanContractSearch");
        }

        public void WebDialogLoadBegin()
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_data.SetTransaction(SQLCA);
            dw_detail.SetTransaction(SQLCA);
            ls_sql = dw_detail.GetSqlSelect();

            if (!IsPostBack)
            {
                dw_data.InsertRow(0);
            }

            else
            {
                dw_data.RestoreContext();
                dw_detail.RestoreContext();
            }
            if (!hidden_search.Value.Equals(""))
            {
               //dw_detail.SetSqlSelect(hidden_search.Value);
               //dw_detail.Retrieve();
                dw_detail.SetSqlSelect(ls_temp);
                dw_detail.Retrieve();
               

            }

        }

        public void CheckJsPostBack(string eventArg)
        {
           
        }
        public void WebDialogLoadEnd()
        {
            SQLCA.Disconnect();
        }
    }
}
