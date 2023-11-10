using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.fund.ws_fund_statement_ctrl
{
    public partial class ws_fund_statement : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string JsPostMember { get; set; }


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
            if (eventArg == JsPostMember)
            {
                PostMember();
            }
        }
        private void PostMember()
        {
            string memberNo = "";
            memberNo = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO.Trim());
            dsMain.ResetRow();
            dsList.ResetRow();
            dsMain.DATA[0].MEMBER_NO = memberNo;
            dsMain.Retrieve(memberNo);
            if (dsMain.RowCount < 1)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลเลขสมาชิก " + memberNo); return;
            }
            dsList.RetrieveStatement(memberNo);

            if (dsList.RowCount < 1)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลกองทุนของเลขสมาชิก " + memberNo); return;
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