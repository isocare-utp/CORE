using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.dlg.wd_fin_deptaccount_search_ctrl
{
    public partial class wd_fin_deptaccount_search : PageWebDialog, WebDialog
    {
       
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                String bank_code = Request["frombank"];
                String bank_branch = Request["frombranch"];
                dsMain.RetrieveDeptno(bank_code, bank_branch);
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