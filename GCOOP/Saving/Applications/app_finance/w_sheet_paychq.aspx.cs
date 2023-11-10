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
    public partial class w_sheet_paychq : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwMain;
        private DwThDate tDwDateSearch;
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
            tDwDateSearch = new DwThDate(DwDateSearch, this);
            tDwDateSearch.Add("start_date", "start_tdate");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postSearch = WebUtil.JsPostBack(this, "postSearch");
            postGetChqBook = WebUtil.JsPostBack(this, "postGetChqBook");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
            postSumChqList = WebUtil.JsPostBack(this, "postSumChqList");
        }

        public void WebSheetLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                //Int32 resultXml;
                //String mainXml = "", chqlistXml = "";
                //resultXml = fin.of_init_paychq(state.SsWsPass, state.SsCoopId, state.SsWorkDate,ref mainXml,ref chqlistXml);
                //DwMain.ImportString(mainXml, FileSaveAsType.Xml);
                //if (chqlistXml != "")
                //{
                //    DwDetail.ImportString(chqlistXml, FileSaveAsType.Xml);
                //}

                DwMain.InsertRow(0);
                DwMain.SetItemDateTime(1, "cheque_date", state.SsWorkDate);
                DwUtil.RetrieveDDDW(DwMain, "frombank", "paychq.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "bank_code", "paychq.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "cheque_type", "paychq.pbl", state.SsCoopId);
                DwUtil.RetrieveDDDW(DwMain, "as_tofromaccid", "paychq.pbl", "CHQ");

                DwDateSearch.InsertRow(0);
                DwDateSearch.SetItemDateTime(1, "start_date", state.SsWorkDate);
                Search();
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
                this.RestoreContextDw(DwDateSearch);
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
                case "postSumChqList":
                    SumChqList();
                    break;
                case "postSearch":
                    Search();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            String ls_main_xml, ls_chqlist_xml, slipStr , formSet = "",ls_chqtype_xml = "";
            formSet = ""; // DdPrintSetProfile.SelectedValue.ToString().Trim();
            tDwMain.Eng2ThaiAllRow();
            
            
            slipStr = "";
            DwDetail.SetFilter("ai_chqflag = 1");
            DwDetail.Filter();
            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                slipStr += DwDetail.GetItemString(i, "slip_no").Trim() + ",";
            }
            if (slipStr != "")
            {
                slipStr = slipStr.Trim();
                String simbol = "";
                int Len = slipStr.Length;
                simbol = slipStr.Substring(Len - 1, 1);
                if (simbol == ",")
                {
                    Len = Len - 1;
                    slipStr = slipStr.Substring(0, Len);
                }
            }
            ls_main_xml = DwMain.Describe("DataWindow.Data.XML");
            ls_chqlist_xml = DwDetail.Describe("DataWindow.Data.XML");
            try
            {
                String re = fin.of_postpaychq(state.SsWsPass, state.SsCoopControl, state.SsUsername, state.SsWorkDate, state.SsClientIp, ls_main_xml, ls_chqlist_xml, formSet);


                if (WebUtil.IsXML(re))
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย กรุณากด New[F2]");

                    String as_bank = DwMain.GetItemString(1, "bank_code");
                    String as_printtype = DwMain.GetItemString(1, "print_type");

                    Printing.PrintApplet(this, "fin_cheque_" + as_bank + "_" + as_printtype, re);

                    WebSheetLoadBegin();

                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
            DwDateSearch.SaveDataCache();
            tDwMain.Eng2ThaiAllRow();
            tDwDateSearch.Eng2ThaiAllRow();
        }

        #endregion


        private void Search()
        {

            //result = fin.of_retrievepaychqlist(state.SsWsPass, state.SsCoopId, Start_date, ref ls_chqlist_xml);
            //DwDetail.Reset();
            //if (ls_chqlist_xml != "")
            //{
            //    DwDetail.ImportString(ls_chqlist_xml, FileSaveAsType.Xml);
            //}
            DateTime start_date = new DateTime() ;
            start_date = DwDateSearch.GetItemDateTime(1, "start_date");
            DwUtil.RetrieveDataWindow(DwDetail, pbl, null, state.SsCoopControl, start_date);
        }

        private void GetInit()
        {
            try
            {
                Int32 result;
                String ls_bank, ls_bankbranch, ls_chqbookno,accno = "",startchqno = "";

                ls_bank = DwMain.GetItemString(1, "bank_code").Trim();
                ls_bankbranch = DwMain.GetItemString(1, "bank_branch").Trim();
                ls_chqbookno = DwMain.GetItemString(1, "cheque_bookno").Trim();

                result = fin.of_init_chqnoandbank(state.SsWsPass, state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno,ref accno,ref startchqno);

                DwMain.SetItemString(1, "account_no", startchqno);
                DwMain.SetItemString(1, "fromaccount_no", accno);

                DwUtil.RetrieveDDDW(DwMain, "account_no", pbl, state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno);
                //DwUtil.RetrieveDDDW(DwMain, "account_no", "paychq_fromslip.pbl", null);
                //DataWindowChild DcChqNo = DwMain.GetChild("account_no");
                //DcChqNo.SetFilter("chequebook_no='" + ls_chqbookno + "'");
                //DcChqNo.Filter();
            }catch(SoapException ex){
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
        }

        private void SumChqList()
        {
            Decimal li_chqflag, item_amtnet;
            Decimal SumItemAmt = 0;

            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                try
                {
                    li_chqflag = DwDetail.GetItemDecimal(i, "ai_chqflag");
                }
                catch { li_chqflag = 0; }

                item_amtnet = DwDetail.GetItemDecimal(i, "item_amtnet");

                if (li_chqflag == 1)
                {
                    SumItemAmt = SumItemAmt + item_amtnet;
                }
            }

            try
            {
                DwMain.SetItemDecimal(1, "cheque_amt", SumItemAmt);
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
    }
}
