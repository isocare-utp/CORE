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
    public partial class w_sheet_atm_open : PageWebSheet, WebSheet
    {
        private WcfATM.ATMcoreWebClient atmService;
        protected String jsPostMemberNo;
        protected String jsRetrieve;
        protected String jsPostBack;
        protected String jsPrintTest;
        public void InitJsPostBack()
        {
            jsPostMemberNo = WebUtil.JsPostBack(this, "jsPostMemberNo");
            jsRetrieve = WebUtil.JsPostBack(this, "jsRetrieve");
            jsPostBack = WebUtil.JsPostBack(this, "jsPostBack");
            jsPrintTest = WebUtil.JsPostBack(this, "jsPrintTest");
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
                case "jsPrintTest":
                    JsPrintTest();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string[] documentation = new string[3];
                string[] deptaccount_no;
                string member_no = string.Empty;
                string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
                string recvcoop_id = DwMain.GetItemString(1, "receive_coop_id").Trim();
                decimal document_card = DwMain.GetItemDecimal(1, "document_card");
                decimal document_card_office = DwMain.GetItemDecimal(1, "document_card_office");
                decimal doucment_etc = DwMain.GetItemDecimal(1, "doucment_etc");
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
                if (document_card + document_card_office + doucment_etc <= 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุเอกสารประกอบ");
                    return;
                }
                documentation[0] = document_card.ToString();
                documentation[1] = document_card_office.ToString();
                documentation[2] = doucment_etc.ToString();

                if (doucment_etc == 1)
                {
                    try
                    {
                        documentation[2] = DwMain.GetItemString(1, "document").Trim();
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเอกสารประกอบ อื่นๆ");
                        return;
                    }
                    if (documentation[2] == null || documentation[2] == "")
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเอกสารประกอบ อื่นๆ");
                        return;
                    }
                }
                int j = 0;
                if (DwDetail.RowCount > 0)
                {
                    for (int i = 1; i <= DwDetail.RowCount; i++)
                    {
                        if (DwDetail.GetItemDecimal(i, "chk") == 1)
                        {
                            j++;
                        }
                        else if (i == DwDetail.RowCount && j == 0)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("โปรดเลือกเลขที่บัญชีเงินฝากออมทรัพย์ที่ใช้ในบัตร ATM");
                            return;
                        }
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขที่บัญชีเงินฝากออมทรัพย์");
                    return;

                }
                deptaccount_no = new string[j];
                j = 0;
                for (int i = 1; i <= DwDetail.RowCount; i++)
                {
                    if (DwDetail.GetItemDecimal(i, "chk") == 1)
                    {
                        deptaccount_no[j] = DwDetail.GetItemString(i, "deptaccount_no");
                        j++;
                    }
                }
                decimal flag_person = 0;
                try
                {
                    flag_person = DwMain.GetItemDecimal(1, "flag_person");
                }
                catch { }
                if (flag_person == 1)
                {
                    card_person = (card_person + "             ").Substring(0, 13);
                }
                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsATMopen OpenServ = atmService.RqATMopen(coop_id, member_no, prename, memb_name, memb_surname, card_person, documentation, deptaccount_no, state.SsWorkDate, state.SsUsername, recvcoop_id);
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
                    XmlString += "<memb_name>" + prename + memb_name + " " + memb_surname + "</memb_name>";
                    XmlString += "<position_desc>" + position_desc + "</position_desc>";
                    XmlString += "<pindata>" + OpenServ.ATMpin + "</pindata>";
                    XmlString += "<recieve_at>" + recvcoop_id.Trim().Substring(recvcoop_id.Length - 1, 1) + "</recieve_at>";
                    XmlString += "</printing_japplet_row></printing_japplet>";


                    Printing.PrintAppletPB(this, "atmopen_slip", XmlString);
                    DwDetail.Reset();
                    DwMain.Reset();
                    DwMain.InsertRow(0);
                    DwDetail.Visible = false;
                    Message.Text = "";
                    try
                    {
                        DwMain.SetItemString(1, "coop_id", HdCoop_id.Value);
                    }
                    catch { }
                    try
                    {
                        DwMain.SetItemDecimal(1, "document_card", Convert.ToInt32(HdDoccument.Value.Substring(0, 1)));
                        DwMain.SetItemDecimal(1, "document_card_office", Convert.ToInt32(HdDoccument.Value.Substring(1, 1)));
                        DwMain.SetItemDecimal(1, "doucment_etc", Convert.ToInt32(HdDoccument.Value.Substring(2, 1)));
                    }
                    catch { }
                    try
                    {
                        DwMain.SetItemString(1, "receive_coop_id", HdReceive_coop_id.Value);
                    }
                    catch { }

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
                DwUtil.RetrieveDDDW(DwMain, "receive_coop_id", "dddw_coopmaster", null);
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

                Sdt dt = WebUtil.QuerySdt("select 1 from atmmember where atmclose = 0 and member_no = '" + member_no + "'");
                if (dt.Next()) throw new Exception("สมาชิกเลขทะเบียน "+ member_no + " มีบัตรที่ใช้งานได้อยุ๋ในระบบ กรุณาตรวจสอบ");

                DwUtil.RetrieveDataWindow(DwMain, "d_atm_open", null, member_no);
                if (DwMain.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนนี้ในระบบ");
                    return;
                }
                DwUtil.RetrieveDataWindow(DwDetail, "d_atm_open_detail", null, member_no);
                try
                {
                    DwMain.SetItemString(1, "coop_id", HdCoop_id.Value);
                }
                catch { }
                try
                {
                    DwMain.SetItemDecimal(1, "document_card", Convert.ToInt32(HdDoccument.Value.Substring(0, 1)));
                    DwMain.SetItemDecimal(1, "document_card_office", Convert.ToInt32(HdDoccument.Value.Substring(1, 1)));
                    DwMain.SetItemDecimal(1, "doucment_etc", Convert.ToInt32(HdDoccument.Value.Substring(2, 1)));
                }
                catch { }
                try
                {
                    DwMain.SetItemString(1, "receive_coop_id", HdReceive_coop_id.Value);
                }
                catch { }
                if (DwDetail.RowCount > 0)
                {
                    Message.Text = "เลือกเลขที่บัญชีเงินฝากออมทรัพย์ที่ใช้ในบัตร ATM";
                    DwDetail.Visible = true;
                    DwDetail.SetItemDecimal(1, "chk", 1);
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

        private void JsPrintTest()
        {
            try
            {
                String member_no = "01234567";
                int pin = new Random().Next(0, 9999);

                string XmlString = string.Empty;
                XmlString += "<?xml version=\"1.0\" encoding=\"utf-8\"?><printing_japplet><printing_japplet_row>";
                XmlString += "<member_no>" + member_no + "</member_no>";
                XmlString += "<slip_date>" + DateTime.Now.ToString("yyyy-MM-dd") + "</slip_date>";
                XmlString += "<memb_name>ชื่อ นามสกุล</memb_name>";
                XmlString += "<position_desc>สังกัด</position_desc>";
                XmlString += "<pindata>" + pin.ToString("0000") + "</pindata>";
                XmlString += "<recieve_at>1</recieve_at>";
                XmlString += "</printing_japplet_row></printing_japplet>";


                Printing.PrintAppletPB(this, "atmopen_slip", XmlString);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}