using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.divavg.dlg.w_dlg_search_bankbranch_ctrl
{
    public partial class w_dlg_search_bankbranch : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostBank { get; set; }
        [JsPostBack]
        public String PostBranchName { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string bank_code = Request["expense_bank"].ToString().Trim();
                string bank_branch = Request["expense_branch"].ToString().Trim();
                string expense_accid = Request["expense_accid"].ToString().Trim();
                dsMain.DATA[0].bank_code = bank_code;
                dsMain.DATA[0].branch_id = bank_branch;
                dsMain.DATA[0].expense_accid = expense_accid;
                dsMain.DdBank();
                if (bank_branch != "")
                {
                    dsMain.DdBankBranch(bank_code);
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostBank)
            {
                dsMain.DdBankBranch(dsMain.DATA[0].bank_code);
            }
            else if (eventArg == PostBranchName)
            {
                dsList.Retrieve(dsMain.DATA[0].bank_code, dsMain.DATA[0].branch_name);
            }
        }

        public void WebDialogLoadEnd()
        {
            
        }
    }
}