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
    public partial class w_sheet_atm_cancle : PageWebSheet, WebSheet
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
                DwDetail.Visible = false;
                Message.Text = "";
                //DwMain.SetItemString(1, "coop_id", HdCoop_id.Value);
                //DwMain.SetItemDecimal(1, "document_card", Convert.ToInt32(HdDoccument.Value.Substring(0, 1)));
                //DwMain.SetItemDecimal(1, "document_card_office", Convert.ToInt32(HdDoccument.Value.Substring(1, 1)));
                //DwMain.SetItemDecimal(1, "doucment_etc", Convert.ToInt32(HdDoccument.Value.Substring(2, 1)));
                //DwMain.SetItemString(1, "receive_coop_id", HdReceive_coop_id.Value);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
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
                string member_no = string.Empty;
                string atmcard_id = DwMain.GetItemString(1, "atmcard_id").Trim();
                string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
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

                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsInsDelUp OpenServ = atmService.RqATMclose(coop_id, member_no, atmcard_id, state.SsUsername);
                if (OpenServ.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(OpenServ.resultDesc);
                }
                else
                {
                    if (OpenServ.result != "1")
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(OpenServ.resultDesc);
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    }

                    DwDetail.Reset();
                    DwMain.Reset();
                    DwMain.InsertRow(0);
                    DwDetail.Visible = false;
                    Message.Text = "";
                }
                ////////////////////////////////////////////////////////////////////////

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
            DwDetail.SaveDataCache();
        }

        private void JsPostMemberNo()
        {
            try
            {
                string member_no = "00000000" + DwMain.GetItemString(1, "member_no");
                member_no = member_no.Substring(member_no.Length - 8, 8);
                DwUtil.RetrieveDataWindow(DwMain, "d_atm_cancle", null, member_no);
                if (DwMain.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนนี้ในระบบ");
                    return;
                }
                DwUtil.RetrieveDataWindow(DwDetail, "d_atm_cancle_detail", null, member_no);
                if (DwDetail.RowCount > 0)
                {
                    Message.Text = "เลขที่บัญชีเงินฝากออมทรัพย์ที่ใช้ในบัตร ATM";
                    DwDetail.Visible = true;
                }
                else
                {
                    Message.Text = "ไม่พบเลขที่บัญชีเงินฝากออมทรัพย์";
                    DwDetail.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}