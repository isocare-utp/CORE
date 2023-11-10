using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_deptaccount_search : PageWebDialog, WebDialog
    {
        WebState state;
        protected String LoanContractSearch;
        DwTrans SQLCA;
        String ls_sql = "", ls_sqlext = "", ls_temp = "";



        public void InitJsPostBack()
        {

            LoanContractSearch = WebUtil.JsPostBack(this, "LoanContractSearch");
        }

        public void WebDialogLoadBegin()
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_detail.SetTransaction(SQLCA);
            ls_sql = dw_detail.GetSqlSelect();

            if (!IsPostBack)
            {
                string  member_no = "";
              
                try
                {
                    member_no = Request["member_no"];
                }
                catch
                { }

         
               // string ls_memb = " and (dpdeptmaster.member_no = '" + member_no+ "')";


             //   ls_temp = ls_sql + ls_memb ;
            //    dw_detail.SetSqlSelect(ls_temp);
                dw_detail.Retrieve(member_no);
            }

            else
            {
              string member_no = "";

                try
                {
                    member_no = Request["member_no"];
                }
                catch
                { }


           //     string ls_memb = " and (dpdeptmaster.member_no = '" +member_no+ "')";


             //   ls_temp = ls_sql + ls_memb;
              //  dw_detail.SetSqlSelect(ls_temp);
   
                dw_detail.Retrieve(member_no);
                dw_detail.RestoreContext();
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
