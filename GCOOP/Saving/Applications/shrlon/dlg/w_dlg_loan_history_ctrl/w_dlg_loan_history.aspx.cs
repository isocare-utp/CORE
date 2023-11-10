using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.dlg.w_dlg_loan_history_ctrl
{
    public partial class w_dlg_loan_history : PageWebDialog, WebDialog
    {
        private String modtb_code = "", clm_name = "", clmold_desc = "", clmnew_desc = "";

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                try
                {
                    modtb_code = Request["modtb_code"];
                    clm_name = Request["clm_name"];
                    clmold_desc = Request["clmold_desc"];
                    clmnew_desc = Request["clmnew_desc"];
                }
                catch { }

                dsMain.DATA[0].MODTB_CODE = modtb_code;
                dsMain.DATA[0].CLM_NAME = clm_name;
                dsMain.DATA[0].CLMOLD_DESC = clmold_desc;
                dsMain.DATA[0].CLMNEW_DESC = clmnew_desc;
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