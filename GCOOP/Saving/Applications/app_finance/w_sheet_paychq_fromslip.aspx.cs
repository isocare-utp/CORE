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
    public partial class w_sheet_paychq_fromslip : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwCond;
        private DwThDate tDwDateSearch;
        private String pbl = "paychq_fromslip.pbl";
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
                //SetProfilePrintSet();
                Int32 result;
                String chqcond_xml = "",cutbank_xml = "",chqtype_xml = "",chqlist_xml = "";
                result = fin.of_init_chqlistfrom_slip(state.SsWsPass, state.SsCoopId, state.SsWorkDate, ref chqcond_xml, ref cutbank_xml, ref chqtype_xml, ref chqlist_xml);
                DwDateSearch.InsertRow(0);
                DwDateSearch.Modify("all_flag.Protect=1");
                try
                {
                    DwCond.Reset();
                    //DwCond.ImportString(chqcond_xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(chqcond_xml, DwCond, tDwCond, FileSaveAsType.Xml);
                    tDwCond.Eng2ThaiAllRow();

                    DwBank.Reset();
                    //DwBank.ImportString(chqtype_xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(chqtype_xml, DwBank, tDwCond, FileSaveAsType.Xml);

                    DwType.Reset();
                    //DwType.ImportString(cutbank_xml, FileSaveAsType.Xml);
                    DwUtil.ImportData(cutbank_xml, DwType, tDwCond, FileSaveAsType.Xml);

                    if (chqlist_xml != "")
                    {
                        DwChqList.Reset();
                        //DwChqList.ImportString(chqlist_xml, FileSaveAsType.Xml);
                        DwUtil.ImportData(chqlist_xml, DwChqList, tDwCond, FileSaveAsType.Xml);
                        DwChqList.SetSort("pay_towhom");
                    }

                    DwDateSearch.SetItemDateTime(1, "start_date", state.SsWorkDate);
                    tDwDateSearch.Eng2ThaiAllRow();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                DwUtil.RetrieveDDDW(DwCond, "as_bank", "paychq_fromslip.pbl", null);
                DwUtil.RetrieveDDDW(DwType, "as_chqprint_chqtype", "paychq_fromslip.pbl", state.SsCoopId);
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
            String ls_chqcond_xml, ls_cutbank_xml, ls_chqtype_xml, ls_chqllist_xml;//, formSet;
            //formSet = DdPrintSetProfile.SelectedItem.ToString().Trim();

            ls_chqcond_xml = DwCond.Describe("DataWindow.Data.XML");
            ls_cutbank_xml = DwBank.Describe("DataWindow.Data.XML");
            ls_chqtype_xml = DwType.Describe("DataWindow.Data.XML");

            DwChqList.SetFilter("ai_selected = 1");
            DwChqList.Filter();
            //DwChqList.Dispose

            ls_chqllist_xml = DwChqList.Describe("DataWindow.Data.XML");

            try
            {
                String re = fin.of_postpaychq_fromslip(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, ls_chqcond_xml, ls_cutbank_xml, ls_chqtype_xml, ls_chqllist_xml, "xxx");



                if (WebUtil.IsXML(re))
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย กรุณากด New[F2]");

                    String as_bank = DwCond.GetItemString(1, "as_bank");
                    String as_printtype = DwCond.GetItemString(1, "as_printtype");
                    
                    Printing.PrintApplet(this, "fin_cheque_" + as_bank + "_" + as_printtype, re);

                    WebSheetLoadBegin();
                    
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
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
            String ls_chqlist_xml ="";
            DateTime Start_date;

            //Start_date = DwDateSearch.GetItemDateTime(1, "start_tdate");

            string testdate = DwDateSearch.GetItemString(1, "start_tdate");
            testdate = testdate.Replace("/", "");
            DateTime ls_paytimeTH_ = DateTime.ParseExact(testdate, "ddMMyyyy", new System.Globalization.CultureInfo("th-TH"));

            //ls_chqlist_xml = fin.of_retrievechqfromslip(state.SsWsPass, state.SsCoopId ,ls_paytimeTH_);
            Int32 result;
            result = fin.of_retrievechqfromslip(state.SsWsPass, state.SsCoopId, ls_paytimeTH_,ref ls_chqlist_xml);
            DwChqList.Reset();
            if (ls_chqlist_xml != "")
            {
                //DwChqList.ImportString(ls_chqlist_xml, FileSaveAsType.Xml);
                DwUtil.ImportData(ls_chqlist_xml, DwChqList,tDwCond,FileSaveAsType.Xml);
                DwChqList.SetSort("pay_towhom");
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
            //    DwChqList.SetItemDecimal(i, "ai_selected", 0);
            //}

            DwChqList.SetItemDecimal(row, "ai_selected", Check);
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
                DwChqList.SetItemDecimal(i, "ai_selected", Select);
            }
        }

        private void GetInit()
        {
            Int32 result;
            String ls_bank, ls_bankbranch, ls_chqbookno,ls_addrno ="",ls_strchqno ="";

            ls_bank = DwCond.GetItemString(1, "as_bank");
            ls_bankbranch = DwCond.GetItemString(1, "as_bankbranch");
            ls_chqbookno = DwCond.GetItemString(1, "as_chqbookno");

            result = fin.of_init_chqnoandbank(state.SsWsPass, state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno, ref ls_addrno, ref ls_strchqno);



            DwUtil.RetrieveDDDW(DwCond, "as_chqstart_no", "paychq_fromslip.pbl", state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno);
            //DataWindowChild DcChqNo = DwCond.GetChild("as_chqstart_no");
            //DcChqNo.SetFilter("chequebook_no='" + ls_chqbookno + "'");
            //DcChqNo.Filter();
            DwCond.SetItemString(1, "as_chqstart_no", ls_strchqno);
            DwBank.SetItemString(1, "as_fromaccno", ls_addrno);
        }

        private void GetChqBook()
        {
            String BankCode, BankBranch;//, DddwName, DddwChqBookNo;

            BankCode = DwCond.GetItemString(1, "as_bank");
            BankBranch = DwCond.GetItemString(1, "as_bankbranch");

            DwUtil.RetrieveDDDW(DwCond, "as_chqbookno", pbl, state.SsCoopId, BankCode, BankBranch);
            //DwUtil.RetrieveDDDW(DwCond, "as_chqstart_no", pbl, state.SsCoopId);
            //DataWindowChild DcChqBookNo = DwCond.GetChild("as_chqbookno");
            ////DcChqBookNo.SetTransaction(sqlca);
            ////DcChqBookNo.Retrieve();
            //DddwName = DwCond.Describe("as_chqbookno.dddw.name");
            //DddwChqBookNo = com.GetDDDWXml(state.SsWsPass, DddwName);
            //DcChqBookNo.ImportString(DddwChqBookNo, FileSaveAsType.Xml);
            //DcChqBookNo.SetFilter("bank_code='" + BankCode + "' and bank_branch ='" + BankBranch + "'");
            //DcChqBookNo.Filter();
        }

        private void GetBankBranch()
        {
            String ls_bank = DwCond.GetItemString(1, "as_bank");
            String ls_bankbranch = "";

            DwUtil.RetrieveDDDW(DwCond, "as_bankbranch", pbl, ls_bank);
            //DataWindowChild DcBankBranch = DwCond.GetChild("as_bankbranch");
            //Int32 BankBranchXml = fin.of_dddwbankbranch(state.SsWsPass, ls_bank, ref ls_bankbranch);
            ////DcBankBranch.ImportString(ls_bankbranch, FileSaveAsType.Text);
            //DwUtil.ImportData(ls_bankbranch, DcBankBranch, tDwCond, FileSaveAsType.Text);
            //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcBankBranch.Filter();
        }

        private void SetProfilePrintSet()
        {

            //DataTable printSet = com.GetPrinterFormSetsData(state.SsWsPass);
            //ListItem[] list = new ListItem[printSet.Rows.Count + 1];
            //list[0] = new ListItem();
            //list[0].Text = "เลือก Print Set";
            //list[0].Value = "0";
            //DdPrintSetProfile.Items.Add(list[0]);
            //for (int i = 1; i <= printSet.Rows.Count; i++)
            //{
            //    list[i] = new ListItem();
            //    list[i].Text = printSet.Rows[i - 1]["formset_desc"].ToString();
            //    list[i].Value = printSet.Rows[i - 1]["formset_code"].ToString();
            //    DdPrintSetProfile.Items.Add(list[i]);
            //}
            //if (!string.IsNullOrEmpty(state.SsUsername))
            //{
            //    for (int i = 1; i <= printSet.Rows.Count; i++)
            //    {
            //        if (state.SsPrinterSet == printSet.Rows[i - 1]["formset_code"].ToString())
            //        {
            //            DdPrintSetProfile.SelectedIndex = i;
            //            break;
            //        }
            //    }
            //}
        }

        //protected void DdPrintSetProfile_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ChangedPrintSet(DdPrintSetProfile.SelectedIndex);
        //}

        private void ChangedPrintSet(int p)
        {
            //state.SsPrinterSet = DdPrintSetProfile.Items[p].Value;
        }
    }
}
