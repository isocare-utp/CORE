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
    public partial class w_sheet_atm_setatm : PageWebSheet, WebSheet
    {
        private WcfATM.ATMcoreWebClient atmService;
        protected String jsRetrieve;
        protected String jsAddATM;
        protected String jsSaveATM;

        public void InitJsPostBack()
        {
            jsRetrieve = WebUtil.JsPostBack(this, "jsRetrieve");
            jsAddATM = WebUtil.JsPostBack(this, "jsAddATM");
            jsSaveATM = WebUtil.JsPostBack(this, "jsSaveATM");
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
                DwUtil.RetrieveDataWindow(DwMain, "d_atm_setatm", null, "");
                for (int i = 1; i <= DwMain.RowCount; i++)
                {
                    try
                    {
                        DwMain.SetItemString(i, "coopbranch_id", DwMain.GetItemString(i, "coop_id") + DwMain.GetItemString(i, "branch_id"));
                    }
                    catch { }
                }
                DwAddATM.InsertRow(0);
                DwAddATM.Visible = false;
                HdAddATM.Value = "0";
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwAddATM);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsAddATM":
                    if (HdAddATM.Value == "0")
                    {
                        DwAddATM.Visible = true;
                        HdAddATM.Value = "1";
                    }
                    else
                    {
                        DwAddATM.Visible = false;
                        HdAddATM.Value = "0";
                    }
                    break;
                case "jsSaveATM":
                    JsSaveATM();
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
                DwUtil.RetrieveDDDW(DwMain, "coopbranch_id", "dddwcoopmaster", null);
                DwUtil.RetrieveDDDW(DwAddATM, "coopbranch_id", "dddwcoopmaster", null);
            }
            catch { }
            DwMain.SaveDataCache();
            DwAddATM.SaveDataCache();
        }

        private void JsSaveATM()
        {
            try
            {
                string atm_no = DwAddATM.GetItemString(1, "atm_no");
                string coopbranch_id = DwAddATM.GetItemString(1, "coopbranch_id");

                /////////////////////////////////////////////////////////////////////////
                atmService = new WcfATM.ATMcoreWebClient();
                WcfATM.RsInsDelUp ATMserv = atmService.RqInsATM(atm_no, coopbranch_id);
                if (ATMserv.result == "0")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ATMserv.resultDesc);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    DwAddATM.Reset();
                    DwAddATM.InsertRow(0);
                    DwUtil.RetrieveDataWindow(DwMain, "d_atm_setatm", null, "");
                    for (int i = 1; i <= DwMain.RowCount; i++)
                    {
                        try
                        {
                            DwMain.SetItemString(i, "coopbranch_id", DwMain.GetItemString(i, "coop_id") + DwMain.GetItemString(i, "branch_id"));
                        }
                        catch { }
                    }
                    DwAddATM.Visible = false;
                    HdAddATM.Value = "0";
                }
                ////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}
