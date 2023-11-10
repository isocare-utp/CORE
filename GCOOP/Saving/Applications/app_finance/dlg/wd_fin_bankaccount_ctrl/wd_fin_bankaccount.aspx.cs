using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.dlg.wd_fin_bankaccount_ctrl
{
    public partial class wd_fin_bankaccount : PageWebDialog, WebDialog
    {
       
        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string ls_sqltext = "";
                string chkclose_status = Request["chkclose_status"];
                if (chkclose_status == "1")
                {
                    ls_sqltext = "AND FINBANKACCOUNT.CLOSE_STATUS = 0";
                }
                dsList.RetrieveData(state.SsCoopControl, ls_sqltext);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
            
        }
    }
}