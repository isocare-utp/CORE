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
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using CoreSavingLibrary;

namespace Saving.Applications.atm
{
    public partial class w_sheet_atm_openadmin : PageWebSheet, WebSheet
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
                string member_no = string.Empty;
                string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
                string prename = DwMain.GetItemString(1, "mbucfprename_prename_desc");
                string memb_name = DwMain.GetItemString(1, "mbmembmaster_memb_name");
                string memb_surname = DwMain.GetItemString(1, "mbmembmaster_memb_surname");
                string position_desc = DwMain.GetItemString(1, "mbucfmembgroup_membgroup_desc");
                string card_person = string.Empty;
                try
                {
                    card_person = DwMain.GetItemString(1, "mbmembmaster_card_person");
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขบัตรประชาชน");
                    return;
                }
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
                if (coop_id == null || coop_id == "")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกสาขา");
                    return;
                }

                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsATMopen OpenServ = atmService.RqATMopenAdmin(coop_id, member_no, prename, memb_name, memb_surname, card_person, state.SsWorkDate, state.SsUsername);
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

                    string XmlString = string.Empty;
                    XmlString += "<?xml version=\"1.0\" encoding=\"utf-8\"?><printing_japplet><printing_japplet_row>";
                    XmlString += "<member_no>" + member_no + "</member_no>";
                    XmlString += "<slip_date>" + DateTime.Now.ToString("yyyy-MM-dd") + "</slip_date>";
                    XmlString += "<memb_name>" + prename + memb_name + " " + memb_surname + " (Admin)</memb_name>";
                    XmlString += "<position_desc>" + position_desc + "</position_desc>";
                    XmlString += "<pindata>" + OpenServ.ATMpin + "</pindata>";
                    XmlString += "<recieve_at></recieve_at>";
                    XmlString += "</printing_japplet_row></printing_japplet>";


                    Printing.PrintAppletPB(this, "atmopen_slip", XmlString);
                    DwMain.Reset();
                    DwMain.InsertRow(0);
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
            DwMain.SaveDataCache();
        }

        private void JsPostMemberNo()
        {
            try
            {
                string member_no = "00000000" + DwMain.GetItemString(1, "member_no");
                member_no = member_no.Substring(member_no.Length - 8, 8);
                DwUtil.RetrieveDataWindow(DwMain, "d_atm_open", null, member_no);
                if (DwMain.RowCount != 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนนี้ในระบบ");
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