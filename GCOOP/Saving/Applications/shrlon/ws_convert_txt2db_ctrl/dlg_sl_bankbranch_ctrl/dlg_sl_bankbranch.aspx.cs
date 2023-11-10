using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Globalization;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl.dlg_sl_bankbranch_ctrl
{
    public partial class dlg_sl_bankbranch : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.RetrieveMain();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "ChangeBank")
            {
                JsChangeBank();
            }
        }

        public void WebDialogLoadEnd()
        {

        }

        public void JsChangeBank()
        {

        }
    }
}