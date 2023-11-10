using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_info_collwho_ctrl
{
    public partial class ws_dlg_info_collwho : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string member_no = Request["member_no"];
                dsMain.RetrieveMain(member_no);
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