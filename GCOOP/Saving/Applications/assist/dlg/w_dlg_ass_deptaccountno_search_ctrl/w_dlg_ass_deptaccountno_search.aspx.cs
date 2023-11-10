using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.assist.dlg.w_dlg_ass_deptaccountno_search_ctrl
{
    public partial class w_dlg_ass_deptaccountno_search : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                String ls_memberno = Request["memno"];
                dsList.RetrieveDeptno(ls_memberno);
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