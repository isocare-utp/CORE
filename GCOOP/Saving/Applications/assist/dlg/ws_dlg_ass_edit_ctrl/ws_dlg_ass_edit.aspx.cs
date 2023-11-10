using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.assist.dlg.ws_dlg_ass_edit_ctrl
{
    public partial class ws_dlg_ass_edit : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostRetrive { get; set; }
        [JsPostBack]
        public string PostRetriveMemberNo { get; set; }

        public void InitJsPostBack()
        {
            
            dsList.InitDsList(this);
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
               
                /*string ls_getdate = Request["assisttype_code"];
                string[] ls_arr_memn = ls_getdate.Split('|');
                string ls_memno = ls_arr_memn[1];
                string ls_assisttype_code = ls_arr_memn[0];*/

                dsMain.DDAssisttype();
               
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostRetrive)
            {
                string ls_memno = dsMain.DATA[0].member_no;
                string ls_assisttype_code = dsMain.DATA[0].assisttype_code;
                 dsList.RetrieveReqAssist(ls_memno,ls_assisttype_code);
            }
            else if (eventArg == PostRetriveMemberNo) {
                string ls_memno = WebUtil.MemberNoFormat(dsMain.DATA[0].member_no.Trim());
                dsMain.DATA[0].member_no =ls_memno;
                dsList.RetrieveReqAssist(ls_memno, "");
            }
        }

        public void WebDialogLoadEnd()
        {

        }
    }
}