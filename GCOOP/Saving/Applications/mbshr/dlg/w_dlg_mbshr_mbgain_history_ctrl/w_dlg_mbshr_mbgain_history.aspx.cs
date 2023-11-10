using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.dlg.w_dlg_mbshr_mbgain_history_ctrl
{
    public partial class w_dlg_mbshr_mbgain_history : PageWebDialog, WebDialog
    {

        [JsPostBack]
        public String PostSearch { get; set; }
        [JsPostBack]
        public String PostMemberNo { get; set; }


        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
               
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMemberNo")
            {
                dsMain.DATA[0].MEMBER_NO = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
            }
            else if (eventArg == "PostSearch")
            {
                dsList.RetrieveList(dsMain.DATA[0].MEMBER_NO);
                initRow();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        public void initRow()
        {
            for (int r = 1; r <= dsList.RowCount; r++)
            {
                dsList.DATA[r - 1].SEQ_NO = r;
            }
        }
    }
}