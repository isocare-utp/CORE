using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.divavg.dlg
{
    public partial class w_dlg_divsrv_search_share : PageWebDialog,WebDialog
    {
        public String pbl = "divsrv_req_methpay.pbl";
        //=======================================
        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                Dw_main.Reset();
                Dw_main.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {
            Dw_main.SaveDataCache();
        }
    }
}