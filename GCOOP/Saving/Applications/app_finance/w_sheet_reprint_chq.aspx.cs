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
using CoreSavingLibrary.WcfNFinance;
using System.Web.Services.Protocols;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_reprint_chq : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwCon;

        protected String postChqBook;
        protected String postBankBranch;
        protected String postChqlistRetrieve;
        protected String postPrint;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwCon = new DwThDate(DwCon, this);
            tDwCon.Add("adtm_date", "adtm_tdate");

            postChqBook = WebUtil.JsPostBack(this, "postChqBook");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
            postChqlistRetrieve = WebUtil.JsPostBack(this, "postChqlistRetrieve");
            postPrint = WebUtil.JsPostBack(this, "postPrint");
        }

        public void WebSheetLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwCon.InsertRow(0);
                DwChqSize.InsertRow(0);
                DwCon.SetItemDateTime(1, "adtm_date", state.SsWorkDate);

                DwUtil.RetrieveDDDW(DwCon, "as_bank", "reprint.pbl", null);

            }
            else
            {
                this.RestoreContextDw(DwCon);
                this.RestoreContextDw(DwChqSize);
                this.RestoreContextDw(DwChqList);
            }
            tDwCon.Eng2ThaiAllRow();

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postBankBranch")
            {
                GetBankBranch();
            }
            else if (eventArg == "postChqBook")
            {
                GetChqBook();
            }
            else if (eventArg == "postChqlistRetrieve")
            {
                ChqlistRetrieve();
            }
            else if (eventArg == "postPrint")
            {
                PostPrint();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            DwChqList.SaveDataCache();
            DwChqSize.SaveDataCache();
            DwCon.SaveDataCache();
        }

        #endregion


        private void GetChqBook()
        {
            String BankCode, BankBranch;

            BankCode = DwCon.GetItemString(1, "bank_code");
            BankBranch = DwCon.GetItemString(1, "bank_branch");

            DwUtil.RetrieveDDDW(DwCon, "as_chqstart_no", "reprint.pbl", state.SsCoopId, BankCode, BankBranch);
            //DataWindowChild DcChqBookNo = DwCon.GetChild("as_chqstart_no");
            //String DddwName = DwCon.Describe("as_chqstart_no.dddw.name");
            //String DddwChqBookNo = com.GetDDDWXml(state.SsWsPass, DddwName);
            //DcChqBookNo.ImportString(DddwChqBookNo, FileSaveAsType.Xml);
            //DcChqBookNo.SetFilter("bank_code='" + BankCode + "' and bank_branch ='" + BankBranch + "'");
            //DcChqBookNo.Filter();

            String AccId = fin.of_defaultaccid(state.SsWsPass, "CHQ");
            DwCon.SetItemString(1, "as_tofromaccid", AccId);

            DwCon.SetItemString(1, "frombank", BankCode);
            DwCon.SetItemString(1, "frombranch", BankBranch);
        }

        private void GetBankBranch()
        {
            String ls_bank,ls_bankbranch = "";

            try
            {
                ls_bank = DwCon.GetItemString(1, "as_bank");
            }
            catch
            {
                ls_bank = "";
            }

            DataWindowChild DcBankBranch = DwCon.GetChild("as_bankbranch");
            Int32 BankBranchXml = fin.of_dddwbankbranch(state.SsWsPass, ls_bank, ref ls_bankbranch);
            DcBankBranch.ImportString(ls_bankbranch, FileSaveAsType.Text);
            DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            DcBankBranch.Filter();
        }

        private void ChqlistRetrieve()
        {
            try
            {
                String chqlist_Xml = "";
                String ls_retrieve_xml = DwCon.Describe("DataWindow.Data.XML");
                Int32 resultXml = fin.of_retrievereprintchq(state.SsWsPass, state.SsCoopId, ls_retrieve_xml, ref chqlist_Xml);
                DwChqList.Reset();
                //DwChqList.ImportString(chqlist_Xml, FileSaveAsType.Xml);
                DwUtil.ImportData(chqlist_Xml, DwChqList, tDwCon, FileSaveAsType.Xml);
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
        }

        private void PostPrint()
        {
            try
            {
                String formSet = DdPrintSetProfile.SelectedValue.ToString().Trim();
                DwChqList.SetFilter("ai_check=" + 1 + "");
                DwChqList.Filter();
                String ls_cond_xml = DwChqSize.Describe("DataWindow.Data.XML");
                String ls_retrieve_xml = DwCon.Describe("DataWindow.Data.XML");
                String ls_chqlist_xml = DwChqList.Describe("DataWindow.Data.XML");
                String result = fin.of_postreprintchq(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, formSet, ls_cond_xml, ls_retrieve_xml, ls_chqlist_xml);

                if (WebUtil.IsXML(result))
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย กรุณากด New[F2]");

                    if (state.SsCoopControl == "051001")
                    {
                        String as_chqbookno = DwChqList.GetItemString(1, "chequebook_no").Trim();
                        String as_bank = DwChqList.GetItemString(1, "bank_code").Trim();
                        String as_chqstart_no = DwChqList.GetItemString(1, "cheque_no").Trim();
                        String ai_prndate = DwCon.GetItemString(1, "ai_prndate").Trim();
                        String ai_killer = DwCon.GetItemString(1, "killer").Trim();
                        String ai_payee = DwCon.GetItemString(1, "payee_flag").Trim();
                        String as_bankbranch = DwChqList.GetItemString(1, "bank_branch").Trim();

                        string report_name = "";
                        report_name = "ir_finchqslip";
                        string report_label = "พิมพ์เช็ค";
                        iReportArgument args = new iReportArgument();
                        args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                        args.Add("as_chqbookno", iReportArgumentType.String, as_chqbookno);
                        args.Add("as_chequeno", iReportArgumentType.String, as_chqstart_no);
                        args.Add("ai_prndate", iReportArgumentType.String, ai_prndate);
                        args.Add("ai_killer", iReportArgumentType.String, ai_killer);
                        args.Add("ai_payee", iReportArgumentType.String, ai_payee);
                        args.Add("as_bankbranch", iReportArgumentType.String, as_bankbranch);
                        args.Add("as_bank", iReportArgumentType.String, as_bank);
                        iReportBuider report = new iReportBuider(this, "");
                        report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                        report.AutoOpenPDF = true;
                        report.Retrieve();
                    }
                    else
                    {
                        String as_bank = DwCon.GetItemString(1, "as_bank");
                        String as_printtype = DwChqSize.GetItemString(1, "as_printtype");

                        Printing.PrintApplet(this, "fin_cheque_" + as_bank + "_" + as_printtype, result);
                    }
                    


                    ChqlistRetrieve();
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
    }
}
