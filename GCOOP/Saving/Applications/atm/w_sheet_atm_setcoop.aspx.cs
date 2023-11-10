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
using CoreSavingLibrary;

namespace Saving.Applications.atm
{
    public partial class w_sheet_atm_setcoop : PageWebSheet, WebSheet
    {
        private WcfATM.ATMcoreWebClient atmService;
        protected String jsRetrieve;
        protected String jsSaveCoop;
        protected String jsSaveBranch;
        protected String jsDelBranch;
        protected String jsDelCoop;
        protected String jsAddCoop;
        protected String jsAddBranch;

        public void InitJsPostBack()
        {
            jsRetrieve = WebUtil.JsPostBack(this, "jsRetrieve");
            jsSaveCoop = WebUtil.JsPostBack(this, "jsSaveCoop");
            jsSaveBranch = WebUtil.JsPostBack(this, "jsSaveBranch");
            jsDelBranch = WebUtil.JsPostBack(this, "jsDelBranch");
            jsDelCoop = WebUtil.JsPostBack(this, "jsDelCoop");
            jsAddCoop = WebUtil.JsPostBack(this, "jsAddCoop");
            jsAddBranch = WebUtil.JsPostBack(this, "jsAddBranch");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                try
                {
                    atmService = new WcfATM.ATMcoreWebClient();
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
                DwUtil.RetrieveDataWindow(DwCoop, "d_atm_setcoop_coop", null, "");
                DwUtil.RetrieveDataWindow(DwBranch, "d_atm_setcoop_branch", null, "");
                DwMasterCoop.InsertRow(0);
                DwMasterBranch.InsertRow(0);

                DwMasterBranch.Visible = false;
                DwMasterCoop.Visible = false;
                AddCoopFlag.Value = "0";
                AddBranchFlag.Value = "0";

            }
            else
            {
                this.RestoreContextDw(DwCoop);
                this.RestoreContextDw(DwBranch);
                this.RestoreContextDw(DwMasterCoop);
                this.RestoreContextDw(DwMasterBranch);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsSaveCoop":
                    JsSaveCoop();
                    break;
                case "jsSaveBranch":
                    JsSaveBranch();
                    break;
                case "jsDelBranch":
                    JsDelBranch();
                    break;
                case "jsDelCoop":
                    JsDelCoop();
                    break;
                case "jsAddCoop":
                    if (AddCoopFlag.Value == "0")
                    {
                        DwMasterCoop.Visible = true;
                        AddCoopFlag.Value = "1";
                    }
                    else
                    {
                        DwMasterCoop.Visible = false;
                        AddCoopFlag.Value = "0";
                    }
                    break;
                case "jsAddBranch":
                    if (AddBranchFlag.Value == "0")
                    {
                        DwMasterBranch.Visible = true;
                        AddBranchFlag.Value = "1";
                    }
                    else
                    {
                        DwMasterBranch.Visible = false;
                        AddBranchFlag.Value = "0";
                    }
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
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
                DwUtil.RetrieveDDDW(DwMasterBranch, "atmcoop_coop_id", "dddwcoop", null);
                //DwUtil.RetrieveDDDW(DwMain, "receive_coop_id", "dddw_coopmaster", null);
            }
            catch { }
            //tOperateDate.Eng2ThaiAllRow();
            DwCoop.SaveDataCache();
            DwBranch.SaveDataCache();
            DwMasterCoop.SaveDataCache();
            DwMasterBranch.SaveDataCache();
        }

        public void JsSaveCoop()
        {
            try
            {
                string atmcoop_coop_id = DwMasterCoop.GetItemString(1, "atmcoop_coop_id");
                string atmcoop_coop_desc = DwMasterCoop.GetItemString(1, "atmcoop_coop_desc");
                decimal atmcoop_coophold = DwMasterCoop.GetItemDecimal(1, "atmcoop_coophold");
                decimal atmcoop_withdraw_flag = DwMasterCoop.GetItemDecimal(1, "atmcoop_withdraw_flag");
                decimal atmcoop_atmdept_maxamt = DwMasterCoop.GetItemDecimal(1, "atmcoop_atmdept_maxamt");
                decimal atmdept_maxcnt = DwMasterCoop.GetItemDecimal(1, "atmdept_maxcnt");

                if (atmcoop_coop_id.Length != 4)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุรหัสให้ครบ 4 ตัว");
                    return;
                }
                if (atmcoop_atmdept_maxamt <= 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุยอดถอนเงินต่อวันต่อบัตร");
                    return;
                }
                if (atmdept_maxcnt <= 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุจำนวนครั้งที่ถอนต่อวันต่อบัตร");
                    return;
                }

                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsATMcoopINS ATMserv = atmService.RqATMcoopINS(atmcoop_coop_id, atmcoop_coop_desc, atmcoop_coophold, atmcoop_withdraw_flag, atmcoop_atmdept_maxamt, atmdept_maxcnt);
                if (ATMserv.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ATMserv.resultDesc);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    DwMasterCoop.Reset();
                    DwMasterCoop.InsertRow(0);
                    if (DwMasterBranch.RowCount < 1)
                    {
                        DwMasterBranch.InsertRow(0);
                    }
                    DwUtil.RetrieveDataWindow(DwCoop, "d_atm_setcoop_coop", null, "");
                    DwMasterCoop.Visible = false;
                    AddCoopFlag.Value = "0";
                }
                ////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                return;
            }

        }

        public void JsSaveBranch()
        {
            try
            {
                string atmcoop_coop_id = DwMasterBranch.GetItemString(1, "atmcoop_coop_id");
                string atmcoopbranch_branch_id = DwMasterBranch.GetItemString(1, "atmcoopbranch_branch_id");
                string atmcoopbranch_branch_desc = DwMasterBranch.GetItemString(1, "atmcoopbranch_branch_desc");
                decimal atmcoopbranch_branchhold = DwMasterBranch.GetItemDecimal(1, "atmcoopbranch_branchhold");
                decimal atmcoopbranch_withdraw_flag = DwMasterBranch.GetItemDecimal(1, "atmcoopbranch_withdraw_flag");

                if (atmcoop_coop_id.Length != 4)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกรหัส");
                    return;
                }
                if (atmcoopbranch_branch_id.Length != 2)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("โปรดระบุรหัสสาขาให้ครบ 2 ตัว");
                    return;
                }

                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsATMcoopBranchINS ATMserv = atmService.RqATMcoopBranchINS(atmcoop_coop_id, atmcoopbranch_branch_id, atmcoopbranch_branch_desc, atmcoopbranch_branchhold, atmcoopbranch_withdraw_flag);
                if (ATMserv.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ATMserv.resultDesc);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    DwMasterBranch.Reset();
                    DwMasterBranch.InsertRow(0);
                    if (DwMasterCoop.RowCount < 1)
                    {
                        DwMasterCoop.InsertRow(0);
                    }
                    DwUtil.RetrieveDataWindow(DwBranch, "d_atm_setcoop_coop", null, "");
                    DwMasterBranch.Visible = false;
                    AddBranchFlag.Value = "0";
                }
                ////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                return;
            }
        }

        public void JsDelBranch()
        {
            try
            {
                int row = Convert.ToInt32(HdRowDelBranch.Value);
                string atmcoop_coop_id = DwBranch.GetItemString(row, "atmcoop_coop_id");
                string atmcoopbranch_branch_id = DwBranch.GetItemString(row, "atmcoopbranch_branch_id");

                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsInsDelUp ATMserv = atmService.RqDelATMcoopBranch(atmcoop_coop_id, atmcoopbranch_branch_id);
                if (ATMserv.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ATMserv.resultDesc);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    if (DwMasterBranch.RowCount < 1)
                    {
                        DwMasterBranch.InsertRow(0);
                    }
                    if (DwMasterCoop.RowCount < 1)
                    {
                        DwMasterCoop.InsertRow(0);
                    }
                    DwUtil.RetrieveDataWindow(DwBranch, "d_atm_setcoop_coop", null, "");
                }
                ////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                return;
            }
        }

        public void JsDelCoop()
        {
            try
            {
                int row = Convert.ToInt32(HdRowDelCoop.Value);
                string atmcoop_coop_id = DwCoop.GetItemString(row, "atmcoop_coop_id");

                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsInsDelUp ATMserv = atmService.RqDelATMcoop(atmcoop_coop_id);
                if (ATMserv.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ATMserv.resultDesc);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    if (DwMasterBranch.RowCount < 1)
                    {
                        DwMasterBranch.InsertRow(0);
                    }
                    if (DwMasterCoop.RowCount < 1)
                    {
                        DwMasterCoop.InsertRow(0);
                    }
                    DwUtil.RetrieveDataWindow(DwCoop, "d_atm_setcoop_coop", null, "");
                }
                ////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                return;
            }
        }
    }
}