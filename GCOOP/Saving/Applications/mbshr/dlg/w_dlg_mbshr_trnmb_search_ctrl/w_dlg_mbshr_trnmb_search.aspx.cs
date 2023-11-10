using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.mbshr.dlg.w_dlg_mbshr_trnmb_search_ctrl
{
    public partial class w_dlg_mbshr_trnmb_search : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostSearch { get; set; }
        [JsPostBack]
        public string PostClearData { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                string memold_no = "", memnameold = "", memb_surname = "", sql_search = "";
                memold_no = dsMain.DATA[0].MEMBER_NO;
                String memold_no2 = WebUtil.MemberNoFormat(memold_no);
                memnameold = dsMain.DATA[0].MEMB_NAME;
                memb_surname = dsMain.DATA[0].MEMB_SURNAME;
                if (memold_no.Length > 0)
                {
                    sql_search += "and (MBMEMBMASTER.MEMBER_NO = '" + memold_no2 + "')";
                }
                if (memnameold.Length > 0)
                {
                    sql_search += "and (MBMEMBMASTER.MEMB_NAME like '%" + memnameold + "%')";
                }
                if (memb_surname.Length > 0)
                {
                    sql_search += "and (MBMEMBMASTER.MEMB_SURNAME like '%" + memb_surname + "%')";
                }
                dsList.RetrieveList(sql_search);
            }
            else if (eventArg == PostClearData)
            {
                dsMain.ResetRow();
                dsList.ResetRow();
            }
        }

        public void WebDialogLoadEnd()
        {

        }
    }
}