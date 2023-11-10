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
    public partial class w_sheet_paychq_apvloan_cbt : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwMain;
        private DwThDate tDwDateSearch;
        private DwThDate tDwChqList;
        protected String postInit;
        protected String postSearch;
        protected String postcheckAll;
        protected String postGetChqBook;
        protected String postSumChqList;
        protected String postBankBranch;
        protected String postCheckBankAll;
        protected String postGetChqNo;
        protected String postSumValue;



        public void InitJsPostBack()
        {

            postInit = WebUtil.JsPostBack(this, "postInit");
            postSearch = WebUtil.JsPostBack(this, "postSearch");
            postcheckAll = WebUtil.JsPostBack(this, "postcheckAll");
            postGetChqBook = WebUtil.JsPostBack(this, "postGetChqBook");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
            postSumChqList = WebUtil.JsPostBack(this, "postSumChqList");
            postCheckBankAll = WebUtil.JsPostBack(this, "postCheckBankAll");
            postGetChqNo = WebUtil.JsPostBack(this, "postGetChqNo");
            postSumValue = WebUtil.JsPostBack(this, "postSumValue");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("cheque_date", "cheque_tdate");
            tDwDateSearch = new DwThDate(DwDateSearch, this);
            tDwDateSearch.Add("start_date", "start_tdate");
            tDwChqList = new DwThDate(DwDetail, this);
            tDwChqList.Add("slip_date", "slip_tdate");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                Int32 resultXml;
                String cashtype = "CHQ", mainXml = "", chqlist_Xml = "";
                resultXml = fin.of_init_paychq_apvloancbt(state.SsWsPass, state.SsCoopId, state.SsWorkDate, ref mainXml, ref chqlist_Xml, cashtype);

                DwDateSearch.InsertRow(0);
                DwMain.ImportString(mainXml, FileSaveAsType.Xml);
                if (chqlist_Xml != "")
                {
                    DwDetail.ImportString(chqlist_Xml, FileSaveAsType.Xml);
                    DwDetail.SetSort("loangroup_code A , loancontract_no A");
                    DwDetail.Sort();
                    tDwChqList.Eng2ThaiAllRow();

                }
                tDwMain.Eng2ThaiAllRow();
                DwDateSearch.SetItemDateTime(1, "start_date", state.SsWorkDate);
                tDwDateSearch.Eng2ThaiAllRow();
                DwDateSearch.SetItemString(1, "loangroup_code", "02");

                DwUtil.RetrieveDDDW(DwDateSearch, "bank_code", "paychq_apvloan_cbt.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "frombank", "paychq_apvloan_cbt.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "bank_code", "paychq_apvloan_cbt.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "cheque_type", "paychq_apvloan_cbt.pbl", state.SsCoopId);
                DwUtil.RetrieveDDDW(DwMain, "account_no", "paychq_apvloan_cbt.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "as_tofromaccid", "paychq_apvloan_cbt.pbl", null);
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
                case "postcheckAll":
                    JsCheckAll();
                    break;
                case "postCheckBankAll":
                    CheckBankAll();
                    break;
                case "postGetChqNo":
                    GetChqNo();
                    break;
                case "postSumValue":
                    JsSumValue();
                    break;
            }
        }

        private void JsSumValue()
        {
            Decimal ChgAmt, bankfee_amt, banksrv_amt;
            Decimal totalamt = 0;
            String loangroup = "";
            int SelectRow = DwDetail.RowCount;
            for (int i = 1; i <= SelectRow; i++)
            {

                ChgAmt = DwDetail.GetItemDecimal(i, "payoutnet_amt");
                try
                {
                    bankfee_amt = DwDetail.GetItemDecimal(i, "bankfee_amt");
                }
                catch { bankfee_amt = 0; }

                try
                {
                    banksrv_amt = DwDetail.GetItemDecimal(i, "banksrv_amt");
                }
                catch { banksrv_amt = 0; }

                loangroup = DwDetail.GetItemString(i, "loangroup_code");
                if (loangroup != "01")
                {
                    ChgAmt = ChgAmt + bankfee_amt + banksrv_amt;
                }


                Decimal ChgFlag = DwDetail.GetItemDecimal(i, "ai_chqflag");

                if (ChgFlag == 1)
                {
                    //Decimal ChgAmt = DwDetail.GetItemDecimal(i, "loanpay_amt");
                    if (ChgAmt != 0)
                    {
                        totalamt = totalamt + ChgAmt;
                        //DwDetail.SetItemDecimal(1, "cheque_amt", totalamt);
                    }
                }

            }
            DwMain.SetItemDecimal(1, "cheque_amt", totalamt);
        }

        public void SaveWebSheet()
        {
            DateTime slipdate;

            String ls_main_xml, ls_chqlist_xml, slipStr, formSet = "";
            formSet = DdPrintSetProfile.SelectedValue.ToString().Trim();
            tDwMain.Eng2ThaiAllRow();
            ls_main_xml = DwMain.Describe("DataWindow.Data.XML");
            ls_chqlist_xml = DwDetail.Describe("DataWindow.Data.XML");
            slipStr = "";
            DwDetail.SetFilter("ai_chqflag = 1");
            DwDetail.Filter();
            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                slipStr += DwDetail.GetItemString(i, "payoutslip_no") + ",";
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
            try
            {
                slipdate = DwDateSearch.GetItemDateTime(1, "start_date");

                //String re = fin.OfPostPayChqApvLoanCbt(state.SsWsPass, state.SsCoopId, state.SsUsername, slipdate, state.SsClientIp, ls_main_xml, slipStr, formSet);

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");

                Int32 result;
                String cashtype = "CHQ";
                result = fin.of_init_paychq_apvloancbt(state.SsWsPass, state.SsCoopId, state.SsWorkDate, ref ls_main_xml, ref ls_chqlist_xml, cashtype);
                DwMain.Reset();
                DwDetail.Reset();
                DwMain.ImportString(ls_main_xml, FileSaveAsType.Xml);
                DwDetail.ImportString(ls_chqlist_xml, FileSaveAsType.Xml);
                tDwMain.Eng2ThaiAllRow();
                tDwChqList.Eng2ThaiAllRow();

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
            DwDetail.SaveDataCache();
            DwDateSearch.SaveDataCache();
        }


        private void Search()
        {
            String ls_chqlist_xml = "", bank_code, loangroup_code;
            DateTime Start_date;
            Int16 bankAll = 0;
            Int32 result;


            bankAll = Convert.ToInt16(DwDateSearch.GetItemDecimal(1, "bank_flag"));

            Start_date = DwDateSearch.GetItemDate(1, "start_date");
            try
            {
                bank_code = DwDateSearch.GetItemString(1, "bank_code").Trim();
            }
            catch { bank_code = ""; }

            if (bankAll == 1)
            {
                bank_code = "";
            }
            try
            {
                loangroup_code = DwDateSearch.GetItemString(1, "loangroup_code").Trim();
            }
            catch { loangroup_code = ""; }
            try
            {
                String cashtype = "CHQ";
                result = fin.of_retrievechqfrom_apvloancbt(state.SsWsPass, state.SsCoopId, Start_date, bank_code, loangroup_code, ref ls_chqlist_xml, cashtype);

                DwDetail.Reset();
                if (ls_chqlist_xml != "")
                {
                    DwDetail.ImportString(ls_chqlist_xml, FileSaveAsType.Xml);
                    DwDetail.SetSort("loangroup_code A , loancontract_no A");
                    DwDetail.Sort();
                    tDwChqList.Eng2ThaiAllRow();
                }
                tDwMain.Eng2ThaiAllRow();
                DwMain.SetItemDecimal(1, "cheque_amt", 0);
                lb_showcount.Text = "จำนวนที่เลือก : 0 รายการ";
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        private void GetInit()
        {
            Int32 result;
            String ls_bank, ls_bankbranch, ls_chqbookno, accno = "", startchqno = "";

            ls_bank = DwMain.GetItemString(1, "bank_code");
            ls_bankbranch = DwMain.GetItemString(1, "bank_branch");
            ls_chqbookno = DwMain.GetItemString(1, "cheque_bookno");

            result = fin.of_init_chqnoandbank(state.SsWsPass, state.SsCoopId, ls_bank, ls_bankbranch, ls_chqbookno, ref accno, ref startchqno);

            DwMain.SetItemString(1, "account_no", startchqno);
            DwMain.SetItemString(1, "fromaccount_no", accno);
        }

        private void SumChqList()
        {
            Decimal li_chqflag, loanpay_amt, bankfee_amt, banksrv_amt;
            Decimal SumItemAmt = 0, count = 0;
            String lngroup = "";
            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                try
                {
                    li_chqflag = DwDetail.GetItemDecimal(i, "ai_chqflag");
                }
                catch { li_chqflag = 0; }
                //payoutnet_amt +  bankfee_amt +  banksrv_amt 
                loanpay_amt = DwDetail.GetItemDecimal(i, "payoutnet_amt");

                try
                {
                    bankfee_amt = DwDetail.GetItemDecimal(i, "bankfee_amt");
                }
                catch { bankfee_amt = 0; }

                try
                {
                    banksrv_amt = DwDetail.GetItemDecimal(i, "banksrv_amt");
                }
                catch { banksrv_amt = 0; }
                lngroup = DwDateSearch.GetItemString(i, "loangroup_code");

                if (lngroup != "01")
                {
                    loanpay_amt = loanpay_amt + bankfee_amt + banksrv_amt;
                }
                if (li_chqflag == 1)
                {
                    count++;
                    SumItemAmt = SumItemAmt + loanpay_amt;
                }
                lb_showcount.Text = "จำนวนที่เลือก : " + count.ToString("#,##0") + " รายการ";
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

            BankCode = DwMain.GetItemString(1, "bank_code");
            BankBranch = DwMain.GetItemString(1, "bank_branch");


            DwUtil.RetrieveDDDW(DwMain, "cheque_bookno", "paychq_apvloan_cbt.pbl", state.SsCoopId, BankCode, BankBranch);
            DwUtil.RetrieveDDDW(DwMain, "as_tofromaccid", "paychq_apvloan_cbt.pbl", state.SsCoopId);

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

            //object[] argBank = new object[1] { ls_bank };
            DwUtil.RetrieveDDDW(DwMain, "bank_branch", "paychq.pbl", ls_bank);

            //DataWindowChild DcBankBranch = DwMain.GetChild("bank_branch");
            //String BankBranchXml = fin.OfGetChildBankbranch(state.SsWsPass, ls_bank);
            //DcBankBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
            //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcBankBranch.Filter();

            DwUtil.RetrieveDDDW(DwMain, "frombranch", "paychq.pbl", ls_bank);
            //DataWindowChild DcFromBranch = DwMain.GetChild("frombranch");
            //DcFromBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
            //DcFromBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcFromBranch.Filter();
        }

        private void JsCheckAll()
        {
            Decimal Set = 1, bankfee_amt, banksrv_amt;
            Boolean Select = CheckAll.Checked;
            Decimal loanpay_amt = 0, sumAmt = 0, count = 0;
            String loangroup = "";
            if (Select == true)
            {
                Set = 1;
            }
            else if (Select == false)
            {
                Set = 0;
            }

            for (int i = 1; i <= DwDetail.RowCount; i++)
            {
                loanpay_amt = DwDetail.GetItemDecimal(i, "payoutnet_amt");
                try
                {
                    bankfee_amt = DwDetail.GetItemDecimal(i, "bankfee_amt");
                }
                catch { bankfee_amt = 0; }

                try
                {
                    banksrv_amt = DwDetail.GetItemDecimal(i, "banksrv_amt");
                }
                catch { banksrv_amt = 0; }

                loangroup = DwDetail.GetItemString(i, "loangroup_code");
                if (loangroup != "01")
                {
                    loanpay_amt = loanpay_amt + bankfee_amt + banksrv_amt;
                }
                DwDetail.SetItemDecimal(i, "ai_chqflag", Set);

                sumAmt = sumAmt + loanpay_amt;
                count++;
            }
            if (Set == 0)
            {
                count = 0;
                sumAmt = 0;
            }
            DwMain.SetItemDecimal(1, "cheque_amt", sumAmt);
            lb_showcount.Text = "จำนวนที่เลือก : " + count.ToString("#,##0") + " รายการ";
        }

        private void CheckBankAll()
        {
            Decimal bank_flag;

            bank_flag = DwDateSearch.GetItemDecimal(1, "bank_flag");

            if (bank_flag == 1)
            {
                DwDateSearch.Modify("bank_code.protect=1");
            }
            else if (bank_flag == 0)
            {
                DwDateSearch.Modify("bank_code.protect=0");
            }
        }

        private void GetChqNo()
        {
            try
            {
                Int32 result;
                String nBankCode, nBankBranch, bookno, accno = "", startchqno = "";

                nBankCode = DwMain.GetItemString(1, "bank_code");
                nBankBranch = DwMain.GetItemString(1, "bank_branch");
                bookno = DwMain.GetItemString(1, "cheque_bookno");

                //DataWindowChild DcChqNo = DwMain.GetChild("account_no");
                DwUtil.RetrieveDDDW(DwMain, "account_no", "paychq_apvloan_cbt.pbl", state.SsCoopId, nBankCode, nBankBranch, bookno);

                result = fin.of_init_chqnoandbank(state.SsWsPass, state.SsCoopId, nBankCode, nBankBranch, bookno, ref accno, ref startchqno);

                DwMain.SetItemString(1, "account_no", startchqno);
                DwMain.SetItemString(1, "fromaccount_no", accno);

                //DcChqNo.SetFilter("bank_code='" + nBankCode + "' and bank_branch ='" + nBankBranch + "'and chequebook_no ='" + bookno + "'");
                //DcChqNo.Filter();

                //if (DcChqNo.RowCount > 0)
                //{
                //    DwMain.SetItemString(1, "account_no", DcChqNo.GetItemString(1, "cheque_no"));
                //}

                //String AccId = fin.DefaultAccId(state.SsWsPass, "CHQ");
                //DwMain.SetItemString(1, "as_tofromaccid", AccId);

                //DwMain.SetItemString(1, "frombank", nBankCode);
                //DwMain.SetItemString(1, "frombranch", nBankBranch);
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
        }
    }
}
