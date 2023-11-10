using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_kp_bank_search : PageWebDialog, WebDialog
    {
        WebState state;
        protected String LoanContractSearch;
        DwTrans SQLCA;
        String ls_sql = "", ls_sqlext = "", ls_temp = "";


        protected void cb_find_Click(object sender, EventArgs e)
        {

            String bank_desc = "";
           
            try
            {
                bank_desc = dw_data.GetItemString(1, "bank_desc").Trim();

            }
            catch { bank_desc = ""; }



            if (bank_desc.Length > 0)
            {
                ls_sqlext = " where (  cmucfbank.bank_desc like '%" + bank_desc + "%') ";
            }

           

            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();
            
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
                dw_detail.SetSqlSelect(hidden_search.Value);
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
