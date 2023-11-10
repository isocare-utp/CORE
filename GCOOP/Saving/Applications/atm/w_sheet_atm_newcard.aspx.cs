using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.atm
{
    public partial class w_sheet_atm_newcard : PageWebSheet, WebSheet
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
                string[] documentation = new string[3];
                string member_no = string.Empty;
                string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
                string recvcoop_id = DwMain.GetItemString(1, "recvcoop_id").Trim();
                decimal docperson_card = DwMain.GetItemDecimal(1, "docperson_card");
                decimal docoffice_card = DwMain.GetItemDecimal(1, "docoffice_card");
                decimal docetc_flag = DwMain.GetItemDecimal(1, "docetc_flag");
                string atmcard_id = DwMain.GetItemString(1, "atmcard_id");
                string memb_name = DwMain.GetItemString(1, "memb_name");
                string memb_surname = DwMain.GetItemString(1, "memb_surname");
                string card_person = string.Empty;
                string atmitemtype_code = DwMain.GetItemString(1, "atmitemtype_code");
                decimal atmcard_amt = DwMain.GetItemDecimal(1, "atmcard_amt");
                string remark = string.Empty;
                string position_desc = DwMain.GetItemString(1, "mbucfmembgroup_membgroup_desc");
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
                if (docperson_card + docoffice_card + docetc_flag <= 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุเอกสารประกอบ");
                    return;
                }
                documentation[0] = docperson_card.ToString();
                documentation[1] = docoffice_card.ToString();
                documentation[2] = docetc_flag.ToString();

                if (docetc_flag == 1)
                {
                    try
                    {
                        documentation[2] = DwMain.GetItemString(1, "docetc_desc").Trim();
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเอกสารประกอบ อื่นๆ");
                        return;
                    }
                }

                try { remark = DwMain.GetItemString(1, "remark"); }
                catch { }

                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsATMopen OpenServ = atmService.RqNewCard(coop_id, member_no, atmcard_id, memb_name, memb_surname, card_person, documentation, recvcoop_id, atmitemtype_code, atmcard_amt, remark, state.SsWorkDate, state.SsUsername);
                //WcfATM.RsATMopen OpenServ = atmService.RqATMopen(coop_id, member_no, prename, memb_name, memb_surname, card_person, documentation, deptaccount_no, state.SsWorkDate, state.SsUsername);
                if (OpenServ.result == "0") //Error จากระบบ
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(OpenServ.resultDesc);
                }
                else
                {
                    if (OpenServ.result != "1") //Complete แบบมีเงื่อนไขba
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(OpenServ.resultDesc);
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    }
                    if (atmitemtype_code != "AND")
                    {
                        string XmlString = string.Empty;
                        XmlString += "<?xml version=\"1.0\" encoding=\"utf-8\"?><printing_japplet><printing_japplet_row>";
                        XmlString += "<member_no>" + member_no + "</member_no>";
                        XmlString += "<slip_date>" + DateTime.Now.ToString("yyyy-MM-dd") + "</slip_date>";
                        XmlString += "<memb_name>" + memb_name + " " + memb_surname + "</memb_name>";
                        XmlString += "<position_desc>" + position_desc + "</position_desc>";
                        XmlString += "<pindata>" + OpenServ.ATMpin + "</pindata>";
                        XmlString += "<recieve_at>" + recvcoop_id.Trim().Substring(recvcoop_id.Length - 1, 1) + "</recieve_at>";
                        XmlString += "</printing_japplet_row></printing_japplet>";

                        Printing.PrintAppletPB(this, "atmopen_slip", XmlString);
                    }
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
                DwUtil.RetrieveDDDW(DwMain, "recvcoop_id", "dddw_coopmaster", null);
            }
            catch { }
            //tOperateDate.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
        }

        private void JsPostMemberNo()
        {
            try
            {
                String member_no = WebUtil.MemberNoFormat(DwMain.GetItemString(1, "member_no"));
                DwUtil.RetrieveDataWindow(DwMain, "d_atm_open", null, member_no);
                if (DwMain.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนนี้ในระบบ ATM");
                    DwMain.InsertRow(0);
                    return;
                }
                DwMain.SetItemString(1, "atmitemtype_code", "AND");
                DwMain.SetItemDecimal(1, "atmcard_amt", 50);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}