using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user.w_dlg_deptno_ctrl
{
    public partial class w_dlg_deptno : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string ls_memberno = WebUtil.MemberNoFormat(Request["memno"]);                
                string ls_groupcode = Request["groupcode"];
                //ls_groupcode = "'" + ls_groupcode.Trim() + "'";
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