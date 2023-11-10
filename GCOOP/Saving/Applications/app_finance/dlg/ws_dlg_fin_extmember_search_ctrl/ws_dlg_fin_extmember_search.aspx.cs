using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace Saving.Applications.app_finance.dlg.ws_dlg_fin_extmember_search_ctrl
{
    public partial class ws_dlg_fin_extmember_search : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.RetrieveDetail(state.SsCoopControl);
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