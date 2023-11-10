using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications._global.w_dlg_dp_current_account_no_ctrl
{
    public partial class w_dlg_dp_current_account_no : PageWebDialog , WebDialog
    {

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.LastDocNo();
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