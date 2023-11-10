using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Sybase.DataWindow;
using DataLibrary;
using Saving.WcfATM;
using System.Globalization;
using CoreSavingLibrary;

namespace Saving.Applications.atm
{
    public partial class w_sheet_atm_sequester_loan : PageWebSheet, WebSheet
    {

        private WcfATM.ATMcoreWebClient atmService;
        protected String jsPostMemberNo;
        protected String jsRetrieve;
        protected String jsPostBack;

        public void InitJsPostBack()
        {
            jsPostMemberNo = WebUtil.JsPostBack(this, "jsPostMemberNo");
            jsRetrieve = WebUtil.JsPostBack(this, "jsRetrieve");
            jsPostBack = WebUtil.JsPostBack(this, "jsPostBack");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                try
                {
                    atmService = new WcfATM.ATMcoreWebClient();
                    //atmService.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://127.0.0.1/ATM/CoreCoop/ATMcoopServiceWeb/ATMcoreWeb.svc");
                    WcfATM.RsCallServ CallServ = atmService.RqCallServ();
                    if (CallServ.result != 1)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถติดต่อ Service ได้");
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถติดต่อ Service ได้ : " + ex.Message);
                }

                DwMain.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostMemberNo":
                    JsPostMemberNo();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {

                Sta ta = new Sta(state.SsConnectionString);
                ta.Transection();
                try
                {
                    string member_no = string.Empty;
                    string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
                    string atmcard_id = DwMain.GetItemString(1, "atmcard_id").Trim();
                    try
                    {
                        member_no = DwMain.GetItemString(1, "member_no").Trim();
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเลขทะเบียน");
                        return;
                    }

                    if (member_no == null || member_no == "")
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเลขทะเบียน");
                        return;
                    }
                    if (DwMain.GetItemDecimal(1, "hold_check") != 1) throw new Exception("กรุณาเลือกสถานะอายัดก่อนบันทึก");
                    /////////////////////////////////////////////////////////////////////////
                    String ATMITEMTYPE_CODE = String.Empty;
                    Decimal holdflag = 0;
                    Decimal loanhold = DwMain.GetItemDecimal(1, "loanhold");
                    if (loanhold == 0)
                    {
                        holdflag = 1;
                        ATMITEMTYPE_CODE = "SQL";
                    }
                    else
                    {
                        holdflag = 0;
                        ATMITEMTYPE_CODE = "SCL";
                    }
                    String loancontract_no = DwMain.GetItemString(1, "loancontract_no");

                    String SqlUpdate = "update atmloan set loanhold = " + holdflag + " where atmcard_id = '" + atmcard_id + "' and loancontract_no = '" + loancontract_no + "'";
                    ta.Exe(SqlUpdate);

                    String COOPID = "0010";
                    string sqlString = @"INSERT INTO ATMMEMBER_LOG ( OPERATE_DATE, MEMBER_NO, ATMCARD_ID, ATMITEMTYPE_CODE, COOP_ID, ENTRY_ID ) VALUES 
                                   ( sysdate, '" + member_no + "', '" + atmcard_id + "', '" + ATMITEMTYPE_CODE + "', '" + COOPID + "', '" + state.SsUsername + "' )";
                    ta.Exe(sqlString);

                    ////////////////////////////////////////////////////////////////////////

                }
                catch (Exception ex)
                {
                    ta.RollBack();
                    ta.Close();
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    return;
                }
                ta.Commit();
                ta.Close(); 
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                DwMain.Reset();
                DwMain.InsertRow(0);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                return;
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(DwMain, "coop_id", "dddw_coopmaster", null);
            }
            catch { }
            //tOperateDate.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
        }

        private void JsPostMemberNo()
        {
            try
            {
                string member_no = "00000000" + DwMain.GetItemString(1, "member_no");
                member_no = member_no.Substring(member_no.Length - 8, 8);
                DwUtil.RetrieveDataWindow(DwMain, "d_atm_open", null, member_no);
                if (DwMain.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนนี้ในระบบ ATM");
                    DwMain.InsertRow(0);
                    return;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

    }
}