using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_set_intarrear_ctrl
{
    public partial class ws_sl_set_intarrear : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostLoanContractNo { get; set; }
        [JsPostBack]
        public string PostCalIntDate { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {            
            if (!IsPostBack)
            {
                dsMain.DATA[0].INTARRSET_DATE = state.SsWorkDate;
                dsMain.DATA[0].INTARRSET_DOCNO = "AUTO";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                dsMain.DATA[0].MEMBER_NO = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMembNo();
            }
            else if (eventArg == PostLoanContractNo)
            {               
                dsMain.RetrieveLoanContractNo();
                dsMain.DdLoanContractNo();
                dsMain.DATA[0].INTARRSET_CALDATE = state.SsWorkDate;
                CalIntDate();
            }
            else if (eventArg == PostCalIntDate)
            {
                CalIntDate();
            }
        }
        public void CalIntDate()
        {
            
            decimal intarrset_amt = wcf.NShrlon.of_computeinterest_single(state.SsWsPass, state.SsCoopControl, dsMain.DATA[0].LOANCONTRACT_NO, dsMain.DATA[0].BFPRNBAL_AMT, dsMain.DATA[0].BFLASTCALINT_DATE, dsMain.DATA[0].INTARRSET_CALDATE);
            
            dsMain.DATA[0].INTARRSET_AMT = intarrset_amt;
        }

        public void SaveWebSheet()
        {
            try
            {
                dsMain.DATA[0].CONCOOP_ID = state.SsCoopId;
                dsMain.DATA[0].MEMCOOP_ID = state.SsCoopId;
                wcf.NShrlon.of_saveset_intarrear(state.SsWsPass, dsMain.ExportXml() , state.SsUsername);

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsMain.ResetRow();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            dsMain.DATA[0].INTARRSET_DATE = state.SsWorkDate;
        }
    }
}