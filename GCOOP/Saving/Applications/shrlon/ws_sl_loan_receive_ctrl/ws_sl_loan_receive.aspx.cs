using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Drawing;
using CoreSavingLibrary.WcfNShrlon;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_loan_receive_ctrl
{
    public partial class ws_sl_loan_receive : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string member_no = dsMain.DATA[0].MEMBER_NO;
                member_no = WebUtil.MemberNoFormat(member_no);
                dsMain.DATA[0].MEMBER_NO = member_no;
                dsMain.RetrieveMemberName(member_no);
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