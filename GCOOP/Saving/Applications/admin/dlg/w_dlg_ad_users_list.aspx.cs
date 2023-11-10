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
    public partial class w_dlg_ad_users_list : PageWebDialog, WebDialog
    {
        
        public string pbl = "ad_user.pbl";
        
        
        public void InitJsPostBack()
        {
          
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {

                

                DwUtil.RetrieveDataWindow(DwMain, pbl, null, null);
            }

            else {

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