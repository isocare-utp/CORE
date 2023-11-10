using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl.ws_dlg_sl_editcollateral_master_ctrl
{
    public partial class ws_dlg_sl_editcollateral_master : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }

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
            if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(member_no);
                dsList.RetrieveList(member_no);
            }
        }

        public void WebDialogLoadEnd()
        {

        }
    }
}