using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_interestpay_estimate_ctrl.w_dlg_sl_detail_ctrl
{
    public partial class w_dlg_sl_detail : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {

                string loancontract_no = Request["loancontract_no"];
                dsMain.RetrieveMain(loancontract_no);
             

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