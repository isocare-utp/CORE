using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications._global.w_dlg_sh_detail_share_ctrl
{
    public partial class w_dlg_sh_detail_share : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }


        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsStatement.InitDs(this);
            dsPayment.InitDs(this);
        }
        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string member_no = Request["member_no"];
                string sharetype = Request["sharetype_code"];
                dsMain.RetrieveMembNo(member_no,sharetype);
                dsStatement.Retrieve(member_no,sharetype);
                dsPayment.Retrieve(member_no);
            }

        }


        public void CheckJsPostBack(string eventArg)
        {
            /*if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);


            }*/



        }
    

        public void WebDialogLoadEnd()
        {

        }
    }
}