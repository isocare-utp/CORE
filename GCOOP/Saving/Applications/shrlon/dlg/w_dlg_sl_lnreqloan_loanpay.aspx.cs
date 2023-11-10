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
using DataLibrary;
namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_lnreqloan_loanpay : PageWebDialog, WebDialog
    {
        protected String refresh;
        protected String jsExpensecode;
        protected string jsGetexpensememno;
        protected string jsExpenseBank;
        protected string jsExpensebankbrRetrieve;
        protected string jsGetbankname;
        protected string jsGetbranchname;

        String pbl = "sl_loan_requestment_cen.pbl";
        public void InitJsPostBack()
        {
            refresh = WebUtil.JsPostBack(this, "refresh");
            jsExpensecode = WebUtil.JsPostBack(this, "jsExpensecode");
            jsGetexpensememno = WebUtil.JsPostBack(this, "jsGetexpensememno");
            jsExpenseBank = WebUtil.JsPostBack(this, "jsExpenseBank");
            jsGetbankname = WebUtil.JsPostBack(this, "jsGetbankname");
            jsGetbranchname = WebUtil.JsPostBack(this, "jsGetbranchname");
            jsExpensebankbrRetrieve = WebUtil.JsPostBack(this, "jsExpensebankbrRetrieve");
        }

        public void WebDialogLoadBegin()
        {

            if (IsPostBack)
            {

                this.RestoreContextDw(dw_detail);
            }
            else
            {
                String member_no, expense_code, expense_bank, expense_branch, expense_accid, buttonc;

                try
                {
                    member_no = Request["member_no"].ToString();
                    if (member_no == "null")
                    {
                        member_no = "";
                    }
                }
                catch { member_no = ""; }
                try
                {
                    expense_code = Request["expense_code"].ToString();

                }
                catch { expense_code = ""; }
                try
                {
                    expense_bank = Request["expense_bank"].ToString();
                    if (expense_bank == "null")
                    {
                        expense_bank = "";
                    }
                }
                catch { expense_bank = ""; }
                try
                {
                    expense_branch = Request["expense_branch"].ToString();
                    if (expense_branch == "null")
                    {
                        expense_branch = "";
                    }
                }
                catch { expense_branch = ""; }
                try
                {
                    expense_accid = Request["expense_accid"].ToString();
                    if (expense_accid == "null")
                    {
                        expense_accid = "";
                    }
                }
                catch { expense_accid = ""; }
                try
                {
                    buttonc = Request["buttonc"].ToString();
                    if (buttonc == "null")
                    {
                        buttonc = "";
                    }
                }
                catch { buttonc = ""; }
                dw_detail.Reset();
                dw_detail.InsertRow(0);
                DwUtil.RetrieveDDDW(dw_detail, "expense_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_detail, "expense_bank_1", pbl, null);

                if (expense_code == "null") { expense_code = "CBT"; }
                if ((expense_code == "CHQ") || (expense_code == "CBT") || (expense_code == "DRF") || (expense_code == "TBK"))
                {

                    //ฝั่งธนาคาร
                    dw_detail.Modify("expense_bank.visible =1");
                    dw_detail.Modify("expense_bank_1.visible =1");
                    dw_detail.Modify("expense_branch.visible =1");
                    dw_detail.Modify("expense_branch_1.visible =1");
                    dw_detail.Modify("expense_branch_t.visible = 1");
                    dw_detail.Modify("expense_accid_t.visible = 1");
                    dw_detail.Modify("expense_accid.visible = 1");
                    dw_detail.Modify("t_2.visible = 1");
                    dw_detail.Modify("t_3.visible = 1");
                }
                else if ((expense_code == "CSH") || (expense_code == "KOT") || (expense_code == "KEP") || (expense_code == "MOS") || (expense_code == "MOO"))
                {

                    //ฝั่งธนาคาร

                    dw_detail.Modify("expense_bank_t.visible =0");
                    dw_detail.Modify("expense_bank.visible =0");
                    dw_detail.Modify("expense_bank_1.visible =0");
                    dw_detail.Modify("t_2.visible = 0");
                    dw_detail.Modify("t_3.visible = 0");
                    dw_detail.Modify("expense_branch.visible =0");
                    dw_detail.Modify("expense_branch_1.visible =0");
                    dw_detail.Modify("expense_branch_t.visible =0");
                    dw_detail.Modify("expense_accid_t.visible =0");
                    dw_detail.Modify("expense_accid.visible = 0");


                }
                else if (expense_code == "TRN")
                {

                    dw_detail.Modify("expense_bank_t.visible =0");
                    dw_detail.Modify("expense_bank.visible =0");
                    dw_detail.Modify("expense_bank_1.visible =0");
                    dw_detail.Modify("t_2.visible = 0");
                    dw_detail.Modify("t_3.visible = 0");
                    dw_detail.Modify("expense_branch.visible =0");
                    dw_detail.Modify("expense_branch_1.visible =0");
                    dw_detail.Modify("expense_branch_t.visible =0");
                    dw_detail.Modify("expense_accid_t.visible = 1");
                    dw_detail.Modify("expense_accid.visible = 1");
                }
                dw_detail.SetItemString(1, "expense_code", expense_code);
                dw_detail.SetItemString(1, "expense_bank", expense_bank);
                dw_detail.SetItemString(1, "expense_branch", expense_branch);
                dw_detail.SetItemString(1, "expense_accid", expense_accid);
                JsExpensebankbrRetrieve();
                dw_detail.SetItemString(1, "buttonc", buttonc);
                Hdbuttonc.Value = buttonc;
                String bankcode = dw_detail.GetItemString(1, "expense_bank");
                String branchcode = dw_detail.GetItemString(1, "expense_branch");
                if (bankcode != "" && bankcode != null)
                {
                    JsGetbankname(bankcode);
                }
                if (bankcode != "" && bankcode != null && branchcode != "" && branchcode != null)
                {
                    if (ChkExpense(bankcode, branchcode))
                    {
                        JsGetbranchname(bankcode, branchcode);
                    }

                }

            }
        }

        public void CheckJsPostBack(string eventArg)
        {

            if (eventArg == "refresh")
            {
                Refresh();
            }
            else if (eventArg == "jsExpensecode")
            {
                JsExpenseCode();
            }
            else if (eventArg == "jsGetexpensememno")
            {

                JsGetexpensememno();
                String bankcode = dw_detail.GetItemString(1, "expense_bank");
                String branchcode = dw_detail.GetItemString(1, "expense_branch");
                JsGetbankname(bankcode);
                JsGetbranchname(bankcode, branchcode);
            }
            else if (eventArg == "jsExpenseBank")
            {
                JsExpenseBank();
            }
            else if (eventArg == "jsExpensebankbrRetrieve")
            {
                JsExpensebankbrRetrieve();
            }
            else if (eventArg == "jsGetbankname")
            {

            }
            else if (eventArg == "jsGetbranchname")
            {
                String branchcode = dw_detail.GetItemString(1, "expense_branch");
                String bankcode = dw_detail.GetItemString(1, "expense_bank");
                if (branchcode != "" && branchcode !=null && branchcode !="" && branchcode !=null)
                {
                    JsGetbranchname(bankcode, branchcode);
                }
             
            }
          

        }

        public void WebDialogLoadEnd()
        {
            dw_detail.SaveDataCache();

        }
        private void JsGetbankname(String bank_code)
        {

            try
            {
                String bankcode = bank_code.Trim();
                String strsql = "SELECT  CMUCFBANK.BANK_DESC FROM CMUCFBANK where CMUCFBANK.Bank_code = '" + bankcode + "'";
                Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                if (dtloanrcv.GetRowCount() <= 0)
                {


                }
                else
                    if (dtloanrcv.Next())
                    {
                        Hdbankname.Value = dtloanrcv.GetString("BANK_DESC").Trim();
                        //  Hdbankcode.Value = dtloanrcv.GetString("BANK_DESC");

                    }
            }

            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

        }
        private void JsGetbranchname(String bank_code, String branch_code)
        {

            try
            {
                String bankcode = bank_code.Trim();
                String branchcode = branch_code.Trim();
                String strsql = @" SELECT   CMUCFBANKBRANCH.BRANCH_NAME FROM CMUCFBANKBRANCH  
   WHERE  CMUCFBANKBRANCH.USE_FLAG = 1   and CMUCFBANKBRANCH.BANK_CODE = '" + bankcode + @"'
        and CMUCFBANKBRANCH.branch_id = '" + branchcode + "'";
                Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                if (dtloanrcv.GetRowCount() <= 0)
                {


                }
                else
                    if (dtloanrcv.Next())
                    {
                        Hdbranchname.Value = dtloanrcv.GetString("BRANCH_NAME").Trim();
                        //  Hdbankcode.Value = dtloanrcv.GetString("BANK_DESC");

                    }
            }

            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

        }
        private void JsGetexpensememno()
        {
            try
            {
                string memno = Request["member_no"].ToString();
                string strsql = @"select expense_code, expense_bank, expense_branch, expense_accid 
                                        from mbmembmaster where member_no = '" + memno + "'";
                try
                {
                    Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                    if (dtloanrcv.GetRowCount() <= 0)
                    {

                        LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบเลขที่บัญชีเงินธนาคารของสมาชิก " + memno);
                    }
                    if (dtloanrcv.Next())
                    {
                        string loanrcv_code = "";
                        string loanrcv_bank = "";
                        string loanrcv_branch = "";
                        string loanrcv_accid = "";
                        try
                        {
                            loanrcv_code = dtloanrcv.GetString("expense_code");
                        }
                        catch { loanrcv_code = "CBT"; }
                        try
                        {
                            loanrcv_bank = dtloanrcv.GetString("expense_bank");
                        }
                        catch { loanrcv_bank = "034"; }
                        try
                        {
                            loanrcv_branch = dtloanrcv.GetString("expense_branch");
                        }
                        catch
                        {
                            loanrcv_branch = "0000";
                        }
                        try
                        {
                            loanrcv_accid = dtloanrcv.GetString("expense_accid");
                        }
                        catch { loanrcv_accid = " "; }
                        dw_detail.SetItemString(1, "expense_code", loanrcv_code);
                        dw_detail.SetItemString(1, "expense_bank", loanrcv_bank);
                        dw_detail.SetItemString(1, "expense_branch", loanrcv_branch);
                        dw_detail.SetItemString(1, "expense_accid", loanrcv_accid);
                        JsExpensebankbrRetrieve();

                    }
                    //JsExpenseBank();
                }
                catch { }

            }
            catch
            {
            }

        }
        private void JsExpensebankbrRetrieve()
        {
            try
            {

                String bankCode;
                try { bankCode = dw_detail.GetItemString(1, "expense_bank").Trim(); }
                catch { bankCode = "034"; }
                DwUtil.RetrieveDDDW(dw_detail, "expense_branch_1", pbl, bankCode);

            }
            catch { }


        }
        private void JsExpenseBank()
        {
            try
            {
                String bankCode;
                try { bankCode = dw_detail.GetItemString(1, "expense_bank").Trim(); }
                catch { bankCode = "034"; }
                DwUtil.RetrieveDDDW(dw_detail, "expense_branch_1", "sl_loan_requestment_cen.pbl", bankCode);
                String bankcode = dw_detail.GetItemString(1, "expense_bank");

                //JsGetbankname(bankcode);
                Hdbranchname.Value = "";
                //JsExpensebankbrRetrieve();
                //dwExpenseBranch.SetFilter("CMUCFBANKBRANCH.bank_code ='" + bankCode + "'");
                //dwExpenseBranch.Filter();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        private Boolean ChkExpense(String bank_code, String branch_code)
        {
            Boolean chkExpen = true;
            try
            {
                String bankcode = bank_code.Trim();
                String branchcode = branch_code.Trim();
                String strsql = @" SELECT   CMUCFBANKBRANCH.BRANCH_NAME FROM CMUCFBANKBRANCH  
   WHERE  CMUCFBANKBRANCH.USE_FLAG = 1   and CMUCFBANKBRANCH.BANK_CODE = '" + bankcode + @"'
        and CMUCFBANKBRANCH.branch_id = '" + branchcode + "'";
                Sdt dtloanrcv = WebUtil.QuerySdt(strsql);

                if (dtloanrcv.GetRowCount() <= 0)
                {
                    chkExpen = false;

                }
            }
            catch (Exception ex) { }
            return chkExpen;
        }
        private void JsExpenseCode()
        {
            //str_itemchange strList = new str_itemchange();
            //strList = WebUtil.nstr_itemchange_session(this);
            string expendCode = "";
            try
            {
                expendCode = dw_detail.GetItemString(1, "expense_code");
            }
            catch { }
            if (expendCode == null || expendCode == "") return;

            if ((expendCode == "CHQ") || (expendCode == "CBT") || (expendCode == "DRF") || (expendCode == "TBK"))
            {

                //ฝั่งธนาคาร
                dw_detail.Modify("expense_bank.visible =1");
                dw_detail.Modify("expense_bank_1.visible =1");
                dw_detail.Modify("expense_branch.visible =1");
                dw_detail.Modify("expense_branch_1.visible =1");
                dw_detail.Modify("expense_branch_t.visible = 1");
                dw_detail.Modify("expense_accid_t.visible = 1");
                dw_detail.Modify("expense_accid.visible = 1");
                dw_detail.Modify("t_2.visible = 1");
                dw_detail.Modify("t_3.visible = 1");
                Refresh();
                try
                {

                    DwUtil.RetrieveDDDW(dw_detail, "expense_bank_1", pbl, null);
                    String bankcode = dw_detail.GetItemString(1, "expense_bank");
                    String branchcode = dw_detail.GetItemString(1, "expense_branch");
                    JsGetbankname(bankcode);
                    JsGetbranchname(bankcode, branchcode);

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if ((expendCode == "CSH") || (expendCode == "KOT") || (expendCode == "KEP") || (expendCode == "MOS") || (expendCode == "MOO"))
            {

                //ฝั่งธนาคาร

                dw_detail.Modify("expense_bank_t.visible =0");
                dw_detail.Modify("expense_bank.visible =0");
                dw_detail.Modify("expense_bank_1.visible =0");
                dw_detail.Modify("t_2.visible = 0");
                dw_detail.Modify("t_3.visible = 0");
                dw_detail.Modify("expense_branch.visible =0");
                dw_detail.Modify("expense_branch_1.visible =0");
                dw_detail.Modify("expense_branch_t.visible =0");
                dw_detail.Modify("expense_accid_t.visible =0");
                dw_detail.Modify("expense_accid.visible = 0");
                Refresh();

            }
            else if (expendCode == "TRN")
            {

                dw_detail.Modify("expense_bank_t.visible =0");
                dw_detail.Modify("expense_bank.visible =0");
                dw_detail.Modify("expense_bank_1.visible =0");
                dw_detail.Modify("t_2.visible = 0");
                dw_detail.Modify("t_3.visible = 0");
                dw_detail.Modify("expense_branch.visible =0");
                dw_detail.Modify("expense_branch_1.visible =0");
                dw_detail.Modify("expense_branch_t.visible =0");
                dw_detail.Modify("expense_accid_t.visible = 1");
                dw_detail.Modify("expense_accid.visible = 1");
                Refresh();
            }
        }

        public void Refresh()
        {
            dw_detail.SetItemString(1, "expense_bank", "");
            dw_detail.SetItemString(1, "expense_branch", "");
            dw_detail.SetItemString(1, "expense_accid", "");
        }



    }
}
