using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.dlg.wd_fin_showuser_ctrl
{
    public partial class wd_fin_showuser : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.RetrieveDetail(state.SsCoopId);
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