using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.admin.dlg
{
    public partial class w_dlg_ad_group_list : PageWebDialog, WebDialog
    {
      

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                DwUtil.RetrieveDataWindow(DwMain, "ad_group.pbl", null, null);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            

           
        }

      

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }
    }
}