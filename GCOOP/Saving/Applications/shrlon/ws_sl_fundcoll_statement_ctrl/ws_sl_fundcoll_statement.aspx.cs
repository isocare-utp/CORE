using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNDeposit;

namespace Saving.Applications.shrlon.ws_sl_fundcoll_statement_ctrl
{
    public partial class ws_sl_fundcoll_statement : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string JsPostMember { get; set; }
        [JsPostBack]
        public string JsPostPrintBook { get; set; }
        [JsPostBack]
        public string JsPostPrintStatement { get; set; }
        [JsPostBack]
        public string JsPostProcessInt { get; set; }

        public string outputProcess;

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
            else if (eventArg == JsPostPrintBook)
            {
                PostPrintBookFund();
            }
            else if (eventArg == JsPostPrintStatement)
            {
                PostPrintStatementFund();
            }
            else if (eventArg == JsPostProcessInt)
            {
                PostProcessInt();
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }

        #region AllFunction

        private void PostMember()
        {
            string memberNo = "";
            memberNo = dsMain.DATA[0].MEMBER_NO;
            memberNo = WebUtil.MemberNoFormat(memberNo);
            dsMain.DATA[0].MEMBER_NO = memberNo.Trim();
            dsMain.Retrieve(memberNo);
            dsList.Retrieve(memberNo);
            dsMain.DATA[0].LASTREC_NO_PB = dsMain.DATA[0].LASTREC_NO_PB + 1;
        }

        private void PostPrintBookFund()
        {
            string fundmemberno = "", xmlreturn = "";
            fundmemberno = dsMain.DATA[0].FUNDMEMBER_NO.Trim();
            int result = wcf.NDeposit.of_print_bookfund_firstpage(state.SsWsPass, fundmemberno, state.SsWorkDate, state.SsUsername, ref xmlreturn, 0, null, null);
            
            if (result == 1 && (xmlreturn != null || xmlreturn != ""))
            {
                Printing.FundPrintBook(this, xmlreturn);
            }
        }

        private void PostPrintStatementFund()
        {
            string fundmemberno = "", xmlreturn = "", returnstr = "";
            short fromseq_no, page_no, line_no ;
            fundmemberno = dsMain.DATA[0].FUNDMEMBER_NO.Trim();
            fromseq_no = Convert.ToInt16(dsMain.DATA[0].LASTREC_NO_PB);
            page_no = Convert.ToInt16(dsMain.DATA[0].LASTPAGE_NO_PB);
            line_no = Convert.ToInt16(dsMain.DATA[0].LASTLINE_NO_PB);

            int result = wcf.NDeposit.of_print_book_fund(state.SsWsPass, fundmemberno, fromseq_no, page_no, line_no, true, ref xmlreturn, ref returnstr);

            if (result == 1 && (xmlreturn != null || xmlreturn != ""))
            {
                Printing.FundPrintStatement(this, xmlreturn);
            }
      
        }

        private void PostProcessInt()
        {
            try
            {
                outputProcess = WebUtil.runProcessing(state, "FUNDMTHBALANCE", "", "", "");
                LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลดอกเบี้ยกองทุน สำเร็จ");
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        #endregion
    }
}