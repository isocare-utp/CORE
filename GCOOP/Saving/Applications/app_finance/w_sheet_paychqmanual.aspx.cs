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
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNFinance;
using System.Web.Services.Protocols;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_paychqmanual : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwMain;
        private String pbl = "paychq.pbl";
        protected String postInit;
        protected String postSearch;
        protected String postGetChqBook;
        protected String postSumChqList;
        protected String postBankBranch;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("cheque_date", "cheque_tdate");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postSearch = WebUtil.JsPostBack(this, "postSearch");
            postGetChqBook = WebUtil.JsPostBack(this, "postGetChqBook");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
            postSumChqList = WebUtil.JsPostBack(this, "postSumChqList");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                String resultXml;
                resultXml = "";
                GetBegin();
                DwUtil.RetrieveDDDW(DwMain, "frombank", "paychq.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "bank_code", "paychq.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "cheque_type", "paychq.pbl", state.SsCoopId);
                DwUtil.RetrieveDDDW(DwMain, "as_tofromaccid", "paychq.pbl", null);
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
                case "postBankBranch":
                    GetBankBranch();
                    break;
                case "postGetChqBook":
                    GetChqBook();
                    break;
                case "postInit":
                    GetInit();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            String ls_main_xml , formSet = "";
            formSet = DdPrintSetProfile.SelectedValue.ToString().Trim();

            tDwMain.Eng2ThaiAllRow();
            ls_main_xml = DwMain.Describe("DataWindow.Data.XML");
            try
            {
                String re = fin.of_postpaychq_manual(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, ls_main_xml, formSet);

                if (WebUtil.IsXML(re))
                {

                    if (state.SsCoopControl == "051001")
                    {
                        String as_chqbookno = DwMain.GetItemString(1, "cheque_bookno").Trim();
                        String as_bank = DwMain.GetItemString(1, "bank_code").Trim();
                        String as_chqstart_no = DwMain.GetItemString(1, "account_no").Trim();
                        String ai_prndate = "1";
                        String ai_killer = DwMain.GetItemString(1, "killer").Trim();
                        String ai_payee = DwMain.GetItemString(1, "payee_flag").Trim();
                        String as_bankbranch = DwMain.GetItemString(1, "bank_branch").Trim();

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
                        String as_bank = DwMain.GetItemString(1, "bank_code").Trim();
                        String as_printtype = DwMain.GetItemString(1, "print_type").Trim();

                        Printing.PrintApplet(this, "fin_cheque_" + as_bank + "_" + as_printtype, re);
                    }
                  
                    GetBegin();
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



        private void GetInit()
        {
            try
            {
                Int32 result;
                String ls_bank, ls_bankbranch, ls_chqbookno,ls_accno = "",ls_strchqno = "";

                ls_bank = DwMain.GetItemString(1, "bank_code").Trim();
                ls_bankbranch = DwMain.GetItemString(1, "bank_branch").Trim();
                ls_chqbookno = DwMain.GetItemString(1, "cheque_bookno").Trim();

                result = fin.of_init_chqnoandbank(state.SsWsPass, state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno, ref ls_accno, ref ls_strchqno);

                DwMain.SetItemString(1, "account_no", ls_strchqno);
                DwMain.SetItemString(1, "fromaccount_no", ls_accno);

                DwUtil.RetrieveDDDW(DwMain, "account_no", pbl, state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno);
                //DwUtil.RetrieveDDDW(DwMain, "account_no", "paychq_fromslip.pbl", null);
                //DataWindowChild DcChqNo = DwMain.GetChild("account_no");
                //DcChqNo.SetFilter("chequebook_no='" + ls_chqbookno + "'");
                //DcChqNo.Filter();
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
        }

        private void GetChqBook()
        {
            String BankCode, BankBranch;
            DwMain.SetItemString(1, "cheque_bookno", "");
            BankCode = DwMain.GetItemString(1, "bank_code");
            BankBranch = DwMain.GetItemString(1, "bank_branch");
            DwUtil.RetrieveDDDW(DwMain, "cheque_bookno", pbl, state.SsCoopId, BankCode, BankBranch);


            //DataWindowChild DcChqBookNo = DwMain.GetChild("cheque_bookno");
            //String DddwName = DwMain.Describe("cheque_bookno.dddw.name");
            //String DddwChqBookNo = com.GetDDDWXml(state.SsWsPass, DddwName);
            //DcChqBookNo.ImportString(DddwChqBookNo, FileSaveAsType.Xml);
            //DcChqBookNo.SetFilter("bank_code='" + BankCode + "' and bank_branch ='" + BankBranch + "'");
            //DcChqBookNo.Filter();

            String AccId = fin.of_defaultaccid(state.SsWsPass, "CHQ");
            DwMain.SetItemString(1, "as_tofromaccid", AccId);

            DwMain.SetItemString(1, "frombank", BankCode);
            DwMain.SetItemString(1, "frombranch", BankBranch);
        }

        private void GetBankBranch()
        {
            String ls_bank;

            try
            {
                ls_bank = DwMain.GetItemString(1, "bank_code");
            }
            catch
            {
                ls_bank = DwMain.GetItemString(1, "frombank");
            }

            object[] argBank = new object[1] { ls_bank };
            DwUtil.RetrieveDDDW(DwMain, "bank_branch", "paychq.pbl", argBank);

            //DataWindowChild DcBankBranch = DwMain.GetChild("bank_branch");
            //String BankBranchXml = fin.OfGetChildBankbranch(state.SsWsPass, ls_bank);
            //DcBankBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
            //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcBankBranch.Filter();

            DwUtil.RetrieveDDDW(DwMain, "frombranch", "paychq.pbl", argBank);
            //DataWindowChild DcFromBranch = DwMain.GetChild("frombranch");
            //DcFromBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
            //DcFromBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcFromBranch.Filter();
        }

        private void GetBegin()
        {
            DwMain.Reset();
            DwMain.InsertRow(0);
            DwMain.SetItemDateTime(1, "cheque_date", state.SsWorkDate);
            DwMain.SetItemDecimal(1, "chq_size", 1);
            DwMain.SetItemString(1, "cheque_type", "01");
            DwMain.SetItemString(1, "print_type", "LAS");
            DwMain.SetItemDecimal(1, "cheque_status", 8);
            tDwMain.Eng2ThaiAllRow();

        
        
        }
    }
}
