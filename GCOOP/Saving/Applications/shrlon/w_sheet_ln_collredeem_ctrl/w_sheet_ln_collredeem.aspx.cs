using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.w_sheet_ln_collredeem_ctrl
{
    public partial class w_sheet_ln_collredeem : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostCollmastNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            //this.showExternalLogin = true;
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].REDEEM_DATE = state.SsWorkDate;
                //date aa = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostCollmastNo)
            {
                Sta ta = new Sta(state.SsConnectionString);
                try
                {
                    ta.AddInParameter("AVC_COOPID", state.SsCoopControl);
                    ta.AddInParameter("AVC_MASTNO", dsMain.DATA[0].COLLMAST_NO);
                    ta.AddInParameter("ADTM_DEEM", dsMain.DATA[0].REDEEM_DATE, System.Data.OracleClient.OracleType.DateTime);
                    ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.Clob);

                    ta.ExePlSql("W_SHEET_LN_COLLREDEEM.OF_INIT_REQCOLLMASTREDEEM");

                    var xmlMain = ta.OutParameter("return_value");

                    dsMain.ResetRow();
                    dsMain.ImportData(xmlMain);
                    dsMain.DATA[0].REDEEM_DATE = state.SsWorkDate;

                }
                catch (Exception ex)
                {
                    dsMain.ResetRow();
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();
            }
        }

        public void SaveWebSheet()
        {

            Sta ta = new Sta(state.SsConnectionString);
            try
            {
                string redeemNo = "";
                ta.AddInParameter("ACLOB_XML", dsMain.ExportXml(), System.Data.OracleClient.OracleType.Clob);
                string xml = dsMain.ExportXml();
                ta.AddInParameter("AVC_ENTRYID", state.SsUsername, System.Data.OracleClient.OracleType.VarChar);
                ta.AddInParameter("AVC_ENTRYCOOPID", state.SsCoopId, System.Data.OracleClient.OracleType.VarChar);
                ta.AddOutParameter("AVC_REDEEMNO", System.Data.OracleClient.OracleType.VarChar);
                ta.AddInParameter("AVC_COOPCONTROL", state.SsCoopControl, System.Data.OracleClient.OracleType.VarChar);

                ta.ExePlSql("W_SHEET_LN_COLLREDEEM.OF_SAVE_LCCOLLMASTREDEEM");

                redeemNo = ta.OutParameter("AVC_REDEEMNO").ToString();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsMain.ResetRow();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            ta.Close();
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}