using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sybase.DataWindow;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving.Applications.admin.dlg
{
    public partial class w_dlg_ad_memberof : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
        
        }

        public void WebDialogLoadBegin()
        {
        
        }

        public void CheckJsPostBack(string eventArg)
        {
   
        }

        public void WebDialogLoadEnd()
        {
            string user_name = Request["user_name"];
            DwUtil.RetrieveDataWindow(DwMain, "ad_user.pbl", null,user_name);
        }
    }
}