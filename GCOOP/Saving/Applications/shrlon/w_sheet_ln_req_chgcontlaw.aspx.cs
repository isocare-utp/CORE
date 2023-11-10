using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_ln_req_chgcontlaw :PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostLoanContractNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].LNCHGCONTLAW_DATE = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                dsMain.RetrieveMembNo();
            }
            else if (eventArg == PostLoanContractNo)
            {
                Sta ta = new Sta(state.SsConnectionString);
                try
                {
                    //AVC_CONCOOP_ID VARCHAR2, AVC_LOANCONTRACT_NO VARCHAR2, AVC_CHANGE_DATE DATE
                    ta.AddInParameter("AVC_CONCOOP_ID", state.SsCoopId);
                    ta.AddInParameter("AVC_LOANCONTRACT_NO", dsMain.DATA[0].LOANCONTRACT_NO);
                    ta.AddInParameter("AVC_CHANGE_DATE", dsMain.DATA[0].LNCHGCONTLAW_DATE, System.Data.OracleClient.OracleType.DateTime);
                    ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.Clob);

                    ta.ExePlSql("W_SHEET_LN_REQ_CHGCONTLAW.OF_INIT_REQCONTLAWCHG");

                    var xmlMain = ta.OutParameter("return_value");

                    dsMain.ImportData(xmlMain);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }

                ta.Close();
            }
        }

        public void SaveWebSheet()
        {
            dsMain.DATA[0].ENTRY_ID = state.SsUsername;
            dsMain.DATA[0].ENTRY_DATE = state.SsWorkDate;
            dsMain.DATA[0].ENTRY_BYCOOPID = state.SsCoopId;

            Sta ta = new Sta(state.SsConnectionString);
            ta.Transection();
            try
            {
                string xml = dsMain.ExportXml();
                ta.AddInParameter("AVC_XML", xml);
                ta.AddInParameter("AVC_COOP_CONTROL", state.SsCoopControl);

                ta.ExePlSql("W_SHEET_LN_REQ_CHGCONTLAW.OF_SAVE_REQCONTLAWCHG");

                ta.Commit();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch(Exception ex)
            {
                ta.RollBack();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            ta.Close();
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}