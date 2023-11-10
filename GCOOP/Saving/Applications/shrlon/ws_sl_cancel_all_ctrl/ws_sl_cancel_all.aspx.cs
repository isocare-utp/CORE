using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cancel_all_ctrl
{
    public partial class ws_sl_cancel_all : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMembno { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);

        }

        public void WebSheetLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMembno")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].member_no);
                dsMain.Retrieve(memb_no);
                dsList.Retrieve(memb_no);
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}