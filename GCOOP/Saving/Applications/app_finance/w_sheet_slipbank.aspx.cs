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
    public partial class w_sheet_slipbank : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwMain;
        private String pbl = "bankaccount.pbl";
        protected String postChange;
        protected String postGetBank;
        protected String postBankBranch;
        protected String postBankAccSlip;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain);      
            tDwMain.Add("open_date", "open_tdate");
            tDwMain.Add("entry_date", "entry_tdate");
            tDwMain.Add("operate_date", "operate_tdate");
            postChange = WebUtil.JsPostBack(this, "postChange");
            postGetBank = WebUtil.JsPostBack(this, "postGetBank");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
            postBankAccSlip = WebUtil.JsPostBack(this, "postBankAccSlip"); 
        }

        public void WebSheetLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;
            
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }

            if (DwMain.GetItemString(1, "item_code") == "OCA")
            {
                DwMain.SetItemDecimal(1, "last_stm", 0);
            }

            DwUtil.RetrieveDDDW(DwMain, "bank_code", pbl, null);
            //DataWindowChild DcBank = DwMain.GetChild("bank_code");
            //String BankCodeXml = com.GetDDDWXml(state.SsWsPass, "dddw_cm_ucfbank");
            //DwUtil.ImportData(BankCodeXml, DcBank, null, FileSaveAsType.Xml);

            DwMain.SetItemDecimal(1, "balance", 0);
            DwMain.SetItemDecimal(1, "withdraw_amt", 0);

            //DataWindowChild dc = DwMain.GetChild("coopbranch_id");
            DwMain.SetItemString(1, "coop_id", state.SsCoopId);
            //dc.ImportString(fin.GetChildBranch(state.SsWsPass), FileSaveAsType.Xml);

            DwMain.SetItemDateTime(1, "entry_date", DateTime.Now);
            tDwMain.Eng2ThaiAllRow();

            DwMain.SetItemDateTime(1, "open_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
            DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();

            DwMain.SetItemString(1, "entry_id", state.SsUsername);
            DwMain.SetItemString(1, "machine_id", state.SsClientIp);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postGetBank")
            {
                GetBank();
            }
            else if (eventArg == "postBankAccSlip")
            {
                BankAccSlip();
            }
            else if (eventArg == "postChange")
            {
                change();
            }
            else if (eventArg == "postBankBranch")
            {
                GetBankBranch();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String main_Xml = DwMain.Describe("DataWindow.Data.XML");
                int re = fin.of_postslipbank(state.SsWsPass, main_Xml);
                if (re == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");

                    DwMain.Reset();
                    DwMain.InsertRow(0);

                    DwMain.SetItemDecimal(1, "balance", 0);
                    DwMain.SetItemDecimal(1, "withdraw_amt", 0);

                    //DataWindowChild dc = DwMain.GetChild("coopbranch_id");
                    DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                    //dc.ImportString(fin.GetChildBranch(state.SsWsPass), FileSaveAsType.Xml);

                    DwMain.SetItemDateTime(1, "entry_date", DateTime.Now);
                    tDwMain.Eng2ThaiAllRow();

                    DwMain.SetItemDateTime(1, "open_date", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();
                    DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();

                    DwMain.SetItemString(1, "entry_id", state.SsUsername);
                    DwMain.SetItemString(1, "machine_id", state.SsClientIp);
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


        private void GetBankBranch()
        {
            String ls_bank;

            ls_bank = DwMain.GetItemString(1, "bank_code");

            //DataWindowChild DcBankBranch = DwMain.GetChild("bank_branch");
            //String BankBranchXml = fin.OfGetChildBankbranch(state.SsWsPass, ls_bank);
            //DcBankBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
            DwUtil.RetrieveDDDW(DwMain, "bank_branch", pbl, ls_bank);
            //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
            //DcBankBranch.Filter();
        }

        private void BankAccSlip()
        {
            try
            {
                Int32 result;
                String main_xml = DwMain.Describe("Datawindow.Data.XML");
                result = fin.of_init_bankaccount_slip(state.SsWsPass, ref main_xml);
               // DwMain.Reset();
               // DwMain.ImportString(main_xml, FileSaveAsType.Xml);
                GetBankBranch();

                //String ls_bank;
                //ls_bank = DwMain.GetItemString(1, "bank_code");
                //DataWindowChild DcBankBranch = DwMain.GetChild("bank_branch");
                //String BankBranchXml = fin.OfGetChildBankbranch(state.SsWsPass, ls_bank);
                //DcBankBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
                //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
                //DcBankBranch.Filter();
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

        private void GetBank()
        {

        }

        private void change()
        {
            Decimal ItemAmt;
            String ItemCode = HdItemCode.Value;
            Decimal Balance;

            try
            {
                ItemAmt = Convert.ToDecimal(HdItemAmt.Value);
            }
            catch
            { ItemAmt = 0; }

            try
            {
                Balance = Convert.ToDecimal(HdBalance.Value);
            }
            catch
            { Balance = 0; }

            if (ItemCode == "CCA")
            {
                DwMain.SetItemDecimal(1, "balance", 0);
                DwMain.SetItemDecimal(1, "withdraw_amt", 0);
                DwMain.SetItemDecimal(1, "item_amt", Balance);
            }
            else if (ItemCode == "OCA")
            {
                DwMain.Reset();
                DwMain.InsertRow(0);

                DwMain.SetItemDecimal(1, "last_stm", 0);

                DwMain.SetItemDecimal(1, "balance", 0);
                DwMain.SetItemDecimal(1, "withdraw_amt", 0);

                //DataWindowChild dc = DwMain.GetChild("coopbranch_id");
                DwMain.SetItemString(1, "coop_id", state.SsCoopId);
                //dc.ImportString(fin.GetChildBranch(state.SsWsPass), FileSaveAsType.Xml);

                DwMain.SetItemDateTime(1, "entry_date", DateTime.Now);
                tDwMain.Eng2ThaiAllRow();

                DwMain.SetItemDateTime(1, "open_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
                DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();

                DwMain.SetItemString(1, "entry_id", state.SsUsername);
                DwMain.SetItemString(1, "machine_id", state.SsClientIp);
            }
            else if (ItemCode != "CCA" && ItemAmt > 0 && Balance == 0)
            {
                DwMain.SetItemDecimal(1, "balance", ItemAmt);
                DwMain.SetItemDecimal(1, "withdraw_amt", ItemAmt);
                DwMain.SetItemDecimal(1, "item_amt", 0);
            }
            else if (ItemCode != "CCA" && ItemAmt == 0 && Balance >= 0)
            {
                DwMain.SetItemDecimal(1, "balance", Balance);
                DwMain.SetItemDecimal(1, "withdraw_amt", Balance);
                DwMain.SetItemDecimal(1, "item_amt", 0);
            }

            DwMain.SetItemString(1, "item_code", ItemCode);
        }
    }
}
