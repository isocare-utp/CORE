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
    public partial class w_sheet_paychq_fromapvloan : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwCond;
        private DwThDate tDwDateSearch;
        private DwThDate tDwList;
        private String pbl = "paychq_fromapvloan.pbl";
        protected String postInit;
        protected String postSearch;
        protected String postProtect;
        protected String postChqBook;
        protected String postCheckAll;
        protected String postCheckList;
        protected String postBankBranch;


        public void InitJsPostBack()
        {
            tDwCond = new DwThDate(DwCond, this);
            tDwCond.Add("adtm_date", "adtm_tdate");
            tDwDateSearch = new DwThDate(DwDateSearch, this);
            tDwDateSearch.Add("start_date", "start_tdate");
            tDwList = new DwThDate(DwChqList, this);
            tDwList.Add("slip_date", "slip_tdate");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postSearch = WebUtil.JsPostBack(this, "postSearch");
            postCheckAll = WebUtil.JsPostBack(this, "postCheckAll");
            postProtect = WebUtil.JsPostBack(this, "postProtect");
            postChqBook = WebUtil.JsPostBack(this, "postChqBook");
            postCheckList = WebUtil.JsPostBack(this, "postCheckList");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
        }

        public void WebSheetLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                Int32 result, ls_chqlist_xml;
                String cashtype, bankcode, bankbranch,chqcond_Xml = "",cutbank_Xml = "",chqtype_Xml = "",chqlist_Xml = "";
                result = fin.of_init_chqlistfrom_slip(state.SsWsPass, state.SsCoopId, state.SsWorkDate, ref chqcond_Xml, ref cutbank_Xml, ref chqtype_Xml, ref chqlist_Xml);
                DwDateSearch.InsertRow(0);
                DwDateSearch.Modify("all_flag.Protect=1");
                cashtype = "CHQ";
                bankcode = "%";
                bankbranch = "%";
                try
                {
                    DwCond.Reset();
                    DwCond.ImportString(chqcond_Xml, FileSaveAsType.Xml);
                    tDwCond.Eng2ThaiAllRow();

                    DwCond.SetItemString(1, "as_printtype", "DOT");

                    DwBank.Reset();
                    DwBank.ImportString(chqtype_Xml, FileSaveAsType.Xml);

                    DwType.Reset();
                    DwType.ImportString(cutbank_Xml, FileSaveAsType.Xml);

                    ls_chqlist_xml = fin.of_retrievechqfromapvloan(state.SsWsPass, state.SsCoopId, state.SsWorkDate, cashtype, ref chqlist_Xml, bankcode, bankbranch);
                    DwChqList.Reset();

                    if (chqlist_Xml != "")
                    {
                        DwChqList.ImportString(chqlist_Xml, FileSaveAsType.Xml);
                        tDwList.Eng2ThaiAllRow();
                    }

                    DwDateSearch.SetItemDateTime(1, "start_date", state.SsWorkDate);
                    tDwDateSearch.Eng2ThaiAllRow();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                DwUtil.RetrieveDDDW(DwCond, "as_bank", "paychq_fromapvloan.pbl", null);
                DwUtil.RetrieveDDDW(DwType, "as_chqprint_chqtype", "paychq_fromapvloan.pbl", state.SsCoopId);
                DwUtil.RetrieveDDDW(DwDateSearch, "bank_code", "paychq_fromapvloan.pbl", null);
            }
            else
            {
                this.RestoreContextDw(DwCond);
                this.RestoreContextDw(DwBank);
                this.RestoreContextDw(DwType);
                this.RestoreContextDw(DwChqList);
                this.RestoreContextDw(DwDateSearch);
            }
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
            else if (eventArg == "postInit")
            {
                GetInit();
            }
            else if (eventArg == "postCheckList")
            {
                CheckList();
            }
            else if (eventArg == "postCheckAll")
            {
                PostCheckAll();
            }
            else if (eventArg == "postSearch")
            {
                Search();
            }
            else if (eventArg == "postProtect")
            {
                Protect();
            }
        }

        public void SaveWebSheet()
        {
            String ls_chqcond_xml, ls_cutbank_xml, ls_chqtype_xml, ls_chqllist_xml, slipStr, formSet = "";
            DateTime payslip_date;
            formSet = DdPrintSetProfile.SelectedValue.ToString().Trim();

            ls_chqcond_xml = DwCond.Describe("DataWindow.Data.XML");
            ls_cutbank_xml = DwBank.Describe("DataWindow.Data.XML");
            ls_chqtype_xml = DwType.Describe("DataWindow.Data.XML");

            DwChqList.SetFilter("ai_chqflag = 1");
            DwChqList.Filter();
            //DwChqList.Dispose
            slipStr = "";
            for (int i = 1; i <= DwChqList.RowCount; i++)
            {
                slipStr += DwChqList.GetItemString(i, "payoutslip_no") + ",";
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


            ls_chqllist_xml = DwChqList.Describe("DataWindow.Data.XML");

            try
            {
                payslip_date = DwDateSearch.GetItemDateTime(1, "start_date");
            }
            catch { payslip_date = state.SsWorkDate; }
            try
            {
                String re = fin.of_postpaychq_fromapvloan(state.SsWsPass, state.SsCoopId, state.SsUsername, payslip_date, state.SsClientIp, ls_chqcond_xml, ls_cutbank_xml, ls_chqtype_xml, slipStr, formSet);

                if (WebUtil.IsXML(re))
                {
                    //LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");


                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย กรุณากด New[F2]");

                    String as_bank = DwCond.GetItemString(1, "as_bank");
                    String as_printtype = DwCond.GetItemString(1, "as_printtype");

                    Printing.PrintApplet(this, "fin_cheque_" + as_bank + "_" + as_printtype, re);

                    Int32 result, ls_chqlist_xml;
                    String  cashtype, bankcode, bankbranch,chqcond_Xml = "",cutbank_Xml = "",chqtype_Xml = "",chqlist_Xml = "";
                    result = fin.of_init_chqlistfrom_slip(state.SsWsPass, state.SsCoopId, state.SsWorkDate,ref chqcond_Xml,ref cutbank_Xml,ref chqtype_Xml,ref chqlist_Xml);

                    bankcode = "%";
                    bankbranch = "%";

                    DwCond.Reset();
                    DwCond.ImportString(chqcond_Xml, FileSaveAsType.Xml);
                    tDwCond.Eng2ThaiAllRow();
                    DwCond.SetItemString(1, "as_printtype", "DOT");

                    DwBank.Reset();
                    DwBank.ImportString(chqtype_Xml, FileSaveAsType.Xml);

                    DwType.Reset();
                    DwType.ImportString(cutbank_Xml, FileSaveAsType.Xml);
                    cashtype = "CHQ";
                    ls_chqlist_xml = fin.of_retrievechqfromapvloan(state.SsWsPass, state.SsCoopId, state.SsWorkDate, cashtype, ref chqlist_Xml, bankcode, bankbranch);
                    DwChqList.Reset();
                    if (chqlist_Xml != "")
                    {
                        DwChqList.ImportString(chqlist_Xml, FileSaveAsType.Xml);
                    }
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
            DwCond.SaveDataCache();
            DwBank.SaveDataCache();
            DwType.SaveDataCache();
            DwChqList.SaveDataCache();
            DwDateSearch.SaveDataCache();
        }

        private void Search()
        {
            Int32 ls_chqlist_xml;
            String cashtype, bankcode, bankbranch,chqlist_Xml = "";
            DateTime Start_date;
            try
            {
                bankcode = DwCond.GetItemString(1, "as_bank").Trim();
            }
            catch { bankcode = "%"; }

            try
            {
                bankbranch = DwCond.GetItemString(1, "as_bankbranch").Trim();
            }
            catch { bankbranch = "%"; }
            

            Start_date = DwDateSearch.GetItemDateTime(1, "start_date");
            cashtype = "CHQ";
            ls_chqlist_xml = fin.of_retrievechqfrom_apvloancbt(state.SsWsPass, state.SsCoopId, Start_date, bankcode, bankbranch, ref chqlist_Xml, cashtype);
            DwChqList.Reset();
            if (chqlist_Xml != "")
            {
                DwChqList.ImportString(chqlist_Xml, FileSaveAsType.Xml);
                tDwList.Eng2ThaiAllRow();
            }
        }

        private void Protect()
        {
            String print_type;

            print_type = DwCond.GetItemString(1, "as_printtype");

            if (print_type == "LAS")
            {
                DwDateSearch.Modify("all_flag.Protect=1");
            }
            else if (print_type == "DOT")
            {
                DwDateSearch.Modify("all_flag.Protect=0");
            }
        }

        private void CheckList()
        {
            int row = Convert.ToInt32(HdRow.Value);
            Decimal Check = Convert.ToDecimal(HdCheck.Value);

            //for (int i = 1; i <= DwChqList.RowCount; i++)
            //{
            //    DwChqList.SetItemDecimal(i, "ai_chqflag", 0);
            //}
            DwChqList.SetItemDecimal(row, "ai_chqflag", Check);
        }

        protected void PostCheckAll()
        {
            int row = 0;
            row = DwChqList.RowCount;
            Decimal Select = DwDateSearch.GetItemDecimal(1, "all_flag");

            //if (Select == true)
            //{
            //    Set = 1;
            //}
            //else if (Select == false)
            //{
            //    Set = 0;
            //}

            for (int i = 1; i <= DwChqList.RowCount; i++)
            {
                DwChqList.SetItemDecimal(i, "ai_chqflag", Select);
            }
        }

        private void GetInit()
        {
            Int32 result;
            String ls_bank, ls_bankbranch, ls_chqbookno,accno = "",startchqno = "";

            ls_bank = DwCond.GetItemString(1, "as_bank");
            ls_bankbranch = DwCond.GetItemString(1, "as_bankbranch");
            ls_chqbookno = DwCond.GetItemString(1, "as_chqbookno");

            result = fin.of_init_chqnoandbank(state.SsWsPass, state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno,ref accno ,ref startchqno);



            DwUtil.RetrieveDDDW(DwCond, "as_chqstart_no", "paychq_fromapvloan.pbl", state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno);
            DwCond.SetItemString(1, "as_chqstart_no", startchqno);
            DwBank.SetItemString(1, "as_fromaccno", accno);
        }

        private void GetChqBook()
        {
            String BankCode, BankBranch;//, DddwName, DddwChqBookNo;

            BankCode = DwCond.GetItemString(1, "as_bank");
            BankBranch = DwCond.GetItemString(1, "as_bankbranch");

            DwUtil.RetrieveDDDW(DwCond, "as_chqbookno", pbl, state.SsCoopId, BankCode, BankBranch);

        }

        private void GetBankBranch()
        {
            String ls_bank = DwCond.GetItemString(1, "as_bank");
            DwUtil.RetrieveDDDW(DwCond, "as_bankbranch", pbl,  ls_bank);
        }
    }
}
