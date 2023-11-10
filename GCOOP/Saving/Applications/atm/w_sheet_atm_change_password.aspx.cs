using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.atm
{
    public partial class w_sheet_atm_change_password : PageWebSheet, WebSheet
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
                string prename = DwMain.GetItemString(1, "mbucfprename_prename_desc");
                string memb_name = DwMain.GetItemString(1, "mbmembmaster_memb_name");
                string memb_surname = DwMain.GetItemString(1, "mbmembmaster_memb_surname");
                string position_desc = DwMain.GetItemString(1, "mbucfmembgroup_membgroup_desc");
                string member_no = string.Empty;
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
                WcfATM.RsATMchangePasswd changePasswd = atmService.RqATMchangePasswd(member_no, state.SsUsername);
                if (changePasswd.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(changePasswd.resultDesc);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                    string XmlString = string.Empty;
                    XmlString += "<?xml version=\"1.0\" encoding=\"utf-8\"?><printing_japplet><printing_japplet_row>";
                    XmlString += "<member_no>" + member_no + "</member_no>";
                    XmlString += "<slip_date>" + DateTime.Now.ToString("yyyy-MM-dd") + "</slip_date>";
                    XmlString += "<memb_name>" + prename + memb_name + " " + memb_surname + "</memb_name>";
                    XmlString += "<position_desc>" + position_desc + "</position_desc>";
                    XmlString += "<pindata>" + changePasswd.ATMpin + "</pindata>";
                    XmlString += "<recieve_at>0</recieve_at>";
                    XmlString += "</printing_japplet_row></printing_japplet>";


                    Printing.PrintAppletPB(this, "atmopen_slip", XmlString);
                    DwMain.Reset();
                    DwMain.InsertRow(0);
                    DwDetail.Reset();
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
        }

        private void JsPostMemberNo()
        {
            try
            {
                string member_no = "00000000" + DwMain.GetItemString(1, "member_no");
                member_no = member_no.Substring(member_no.Length - 8, 8);
                DwUtil.RetrieveDataWindow(DwMain, "d_atm_change_password", null, member_no);
                if (DwMain.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนนี้ในระบบ ATM");
                    DwMain.InsertRow(0);
                    return;
                }

                DwMain.SetItemString(1, "coop_id", DwMain.GetItemString(1, "atmmember_coop_id") + DwMain.GetItemString(1, "atmmember_branch_id"));
                DwUtil.RetrieveDataWindow(DwDetail, "d_atm_change_password_detail", null, member_no);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}