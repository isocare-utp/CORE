using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl.w_dlg_ln_collmast_ctrl
{
    public partial class w_dlg_ln_collmast : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string ls_memno = Request["member_no"];
                dsList.Retrieve(ls_memno);
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