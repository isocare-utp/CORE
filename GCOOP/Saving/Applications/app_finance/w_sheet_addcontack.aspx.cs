using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;


namespace Saving.Applications.app_finance
{
    public partial class w_sheet_addcontack : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private String pbl = "addcontack.pbl";
        protected String postContact;
        protected String postFormat;
        protected String postGetDistrict;
        protected String postGetBank;   //**



        #region WebSheet Members

        public void InitJsPostBack()
        {
            postContact = WebUtil.JsPostBack(this, "postContact");
            postFormat = WebUtil.JsPostBack(this, "postFormat");
            postGetDistrict = WebUtil.JsPostBack(this, "postGetDistrict");
            postGetBank = WebUtil.JsPostBack(this, "postGetBank");   //**
        }

        public void WebSheetLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;



            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                DwUtil.RetrieveDDDW(DwMain, "province", "addcontack.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "prename_code", "addcontack.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "bank_code", "addcontack.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "bank_branch", "addcontack.pbl", null);

 
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
                case "postContact":
                    Contact();
                    break;
                case "postFormat":
                    Format();
                    break;
                case "postGetDistrict":
                    GetDistrict();
                    break;
                case "postGetBank":
                    GetBank();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            String ConNo, ls_contack_xml = "";
            int re;

            try
            {
                ConNo = DwMain.GetItemString(1, "contack_no");
            }
            catch
            {
                ConNo = "";
            }

            try
            {
                ls_contack_xml = DwMain.Describe("DataWindow.Data.XML");

                if (ConNo == "")
                {
                    re = fin.of_postfincontact(state.SsWsPass, ls_contack_xml, "ADD");
                }
                else
                {
                    re = fin.of_postfincontact(state.SsWsPass, ls_contack_xml, "EDIT");
                }

                if (re == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion



        private void Format()
        {
            String ContactNo = HfContact.Value;
            ContactNo = "000000" + ContactNo;
            ContactNo = WebUtil.Right(ContactNo, 6);

            DwMain.SetItemString(1, "contack_no", ContactNo);

            Contact();
        }

        private void Contact()
        {
            String ls_province;
            String ls_bank;
            String ConNo = HfContact.Value;
            String ls_contact_xml = DwMain.Describe("DataWindow.Data.XML");
            Int32 result = fin.of_init_fincontact(state.SsWsPass,ref ls_contact_xml);
            DwMain.Reset();
            //DwMain.ImportString(ls_contact_xml, FileSaveAsType.Xml);
            try { DwUtil.ImportData(ls_contact_xml, DwMain, null, FileSaveAsType.Xml); }
            catch { }
            DwMain.SetItemString(1, "coop_id", state.SsCoopId);
            ls_province = DwMain.GetItemString(1, "province");
            DwUtil.RetrieveDDDW(DwMain, "district", pbl, ls_province);
            ls_bank = DwMain.GetItemString(1, "bank_code");
            DwUtil.RetrieveDDDW(DwMain, "bank_branch", pbl, ls_bank);  
            //DataWindowChild DcDistrict = DwMain.GetChild("district");
            //DcDistrict.Reset();
            //DwUtil.RetrieveDDDW(DwMain, "district", "addcontack.pbl", null);
            //DcDistrict.SetFilter("province_code = '" + ls_province + "'");
            //DcDistrict.Filter();
        }


        private void GetDistrict()
        {
            String ls_province;

            ls_province = HfProvince.Value;
            DwUtil.RetrieveDDDW(DwMain, "district", pbl, ls_province);

            //DataWindowChild DcDistrict = DwMain.GetChild("district");
            //DcDistrict.Reset();
            //DwUtil.RetrieveDDDW(DwMain, "district", "addcontack.pbl", null);
            //DcDistrict.SetFilter("province_code = '" + ls_province + "'");
            //DcDistrict.Filter();

        }
        private void GetBank()    
        {
            String ls_bank;

            ls_bank = Hfbank.Value;
            DwUtil.RetrieveDDDW(DwMain, "bank_branch", pbl, ls_bank);


        }
    }
}
