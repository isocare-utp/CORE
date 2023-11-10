using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl.w_dlg_loan_receive_detail_ctrl
{
    public partial class w_dlg_loan_receive_detail : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack) {
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