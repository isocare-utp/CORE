using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.atm
{
    public partial class w_sheet_atm_change_account : PageWebSheet, WebSheet
    {

        private WcfATM.ATMcoreWebClient atmService;
        protected String jsPostMemberNo;
        protected String jsRetrieve;
        protected String jsPostBack;
        protected String jsAddDeptNo;
        protected String jsCloseDeptNo;
        protected String jsOpenDeptNo;

        public void InitJsPostBack()
        {
            jsPostMemberNo = WebUtil.JsPostBack(this, "jsPostMemberNo");
            jsRetrieve = WebUtil.JsPostBack(this, "jsRetrieve");
            jsPostBack = WebUtil.JsPostBack(this, "jsPostBack");
            jsAddDeptNo = WebUtil.JsPostBack(this, "jsAddDeptNo");
            jsCloseDeptNo = WebUtil.JsPostBack(this, "jsCloseDeptNo");
            jsOpenDeptNo = WebUtil.JsPostBack(this, "jsOpenDeptNo");
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
                case "jsAddDeptNo":
                    JsAddDeptNo();
                    break;
                case "jsCloseDeptNo":
                    JsCloseDeptNo();
                    break;
                case "jsOpenDeptNo":
                    JsOpenDeptNo();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string[] documentation = new string[3];
                //string[] deptaccount_no;
                string member_no = string.Empty;
                string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
                decimal document_card = DwMain.GetItemDecimal(1, "document_card");
                decimal document_card_office = DwMain.GetItemDecimal(1, "document_card_office");
                decimal doucment_etc = DwMain.GetItemDecimal(1, "doucment_etc");
                string prename = DwMain.GetItemString(1, "mbucfprename_prename_desc");
                string memb_name = DwMain.GetItemString(1, "mbmembmaster_memb_name");
                string memb_surname = DwMain.GetItemString(1, "mbmembmaster_memb_surname");
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


                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                //WcfATM.RsATMopen OpenServ = atmService.RqATMopen(coop_id, member_no, prename, memb_name, memb_surname, card_person, documentation, deptaccount_no, state.SsWorkDate, state.SsUsername);
                //if (OpenServ.result == "0")
                //{
                //    LtServerMessage.Text = WebUtil.ErrorMessage(OpenServ.resultDesc);
                //}
                //else
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ : รหัสคือ " + OpenServ.ATMpin + " ข้อมูลหมายเลขบนหน้าบัตรคือ " + OpenServ.ATMnumber);
                //    DwMain.Reset();
                //    DwMain.InsertRow(0);
                //}


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
        }

        private void JsPostMemberNo()
        {
            try
            {
                string member_no = "00000000" + DwMain.GetItemString(1, "member_no");
                member_no = member_no.Substring(member_no.Length - 8, 8);
                DwUtil.RetrieveDataWindow(DwMain, "d_atm_change_account", null, member_no);
                if (DwMain.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนนี้ในระบบ ATM");
                    DwMain.InsertRow(0);
                    return;
                }

                DwMain.SetItemString(1, "coop_id", DwMain.GetItemString(1, "atmmember_coop_id") + DwMain.GetItemString(1, "atmmember_branch_id"));
                DwUtil.RetrieveDataWindow(DwDetail, "d_atm_change_account_detail", null, member_no);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JsAddDeptNo()
        {
            try
            {
                String deptaccount_no = HdDeptaccount_no.Value;
                string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
                String member_no = string.Empty;
                try
                {
                    member_no = DwMain.GetItemString(1, "member_no").Trim();
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเลขทะเบียน");
                    return;
                }
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsInsDelUp OpenServ = atmService.RqInsDeptNo(deptaccount_no, member_no, coop_id, state.SsUsername);
                if (OpenServ.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(OpenServ.resultDesc);
                }
                else if (OpenServ.result == string.Empty)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
                }
                else
                {
                    DwUtil.RetrieveDataWindow(DwDetail, "d_atm_change_account_detail", null, member_no);
                }
                HdDeptaccount_no.Value = "";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JsCloseDeptNo()
        {
            try
            {
                String deptaccount_no = HdDeptaccount_no.Value;
                string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
                String member_no = string.Empty;
                try
                {
                    member_no = DwMain.GetItemString(1, "member_no").Trim();
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเลขทะเบียน");
                    return;
                }
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsInsDelUp OpenServ = atmService.RqUpdCloseDeptNo(deptaccount_no, member_no, coop_id, state.SsUsername);
                if (OpenServ.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(OpenServ.resultDesc);
                }
                else if (OpenServ.result == string.Empty)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
                }
                else
                {
                    DwUtil.RetrieveDataWindow(DwDetail, "d_atm_change_account_detail", null, member_no);
                }
                HdDeptaccount_no.Value = "";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JsOpenDeptNo()
        {
            try
            {
                String deptaccount_no = HdDeptaccount_no.Value;
                string coop_id = DwMain.GetItemString(1, "coop_id").Trim();
                String member_no = string.Empty;
                try
                {
                    member_no = DwMain.GetItemString(1, "member_no").Trim();
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาระบุเลขทะเบียน");
                    return;
                }
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsInsDelUp OpenServ = atmService.RqUpdOpenDeptNo(deptaccount_no, member_no, coop_id, state.SsUsername);
                if (OpenServ.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(OpenServ.resultDesc);
                }
                else if (OpenServ.result == string.Empty)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
                }
                else
                {
                    DwUtil.RetrieveDataWindow(DwDetail, "d_atm_change_account_detail", null, member_no);
                }
                HdDeptaccount_no.Value = "";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}