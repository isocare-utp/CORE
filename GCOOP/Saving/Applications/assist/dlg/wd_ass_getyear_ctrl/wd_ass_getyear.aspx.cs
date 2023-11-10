using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.assist.dlg.wd_ass_getyear_ctrl
{
    public partial class wd_ass_getyear : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {

            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {
        }
    }
}