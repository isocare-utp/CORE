using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.assist.dlg.wd_as_search_deptaccount_ctrl
{
    public partial class wd_as_search_deptaccount : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostSearch { get; set; }

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                //String ls_memberno = Request["memno"];
                //dsList.RetrieveDeptno(ls_memberno);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostSearch")
            {
                string sqltext = "";
                if (dsMain.DATA[0].member_no.Trim() != "")
                {
                    sqltext += " and dpdeptmaster.member_no = '" + WebUtil.MemberNoFormat(dsMain.DATA[0].member_no) + "'";
                    dsMain.DATA[0].member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].member_no);
                }
                if (dsMain.DATA[0].deptaccount_name.Trim() != "")
                {
                    sqltext += " and dpdeptmaster.deptaccount_name like '%" + dsMain.DATA[0].deptaccount_name + "%'";
                }
                if (dsMain.DATA[0].deptaccount_no.Trim() != "")
                {
                    sqltext += " and dpdeptmaster.deptaccount_no = '" + dsMain.DATA[0].deptaccount_no + "'";
                }
                dsList.RetrieveListPage(sqltext, 0);
            }
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}