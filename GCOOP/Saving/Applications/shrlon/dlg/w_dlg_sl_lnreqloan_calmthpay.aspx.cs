using System;
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
using Saving.WcfCommon;
using Sybase.DataWindow;
using DataLibrary;
namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_lnreqloan_calmthpay : PageWebDialog, WebDialog
    {
        protected String refresh;
        protected String jsExpensecode;
        protected string jsGetexpensememno;
        protected string jsExpenseBank;
        protected string jsExpensebankbrRetrieve;
        String pbl = "sl_loan_requestment_cen.pbl";
        public void InitJsPostBack()
        {
             refresh = WebUtil.JsPostBack(this, "refresh");
             jsExpensecode = WebUtil.JsPostBack(this, "jsExpensecode");
             jsGetexpensememno = WebUtil.JsPostBack(this, "jsGetexpensememno");
             jsExpenseBank = WebUtil.JsPostBack(this, "jsExpenseBank");
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
                String member_no = Request["member_no"].ToString();
                String expense_code = Request["expense_code"].ToString();
                String expense_bank = Request["expense_bank"].ToString();
                String expense_branch = Request["expense_branch"].ToString();
                String expense_accid = Request["expense_accid"].ToString();
                string buttonc  = Request["buttonc"].ToString();
                
                dw_detail.Reset();
                dw_detail.InsertRow(0);
                DwUtil.RetrieveDDDW(dw_detail, "expense_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_detail, "expense_bank_1", pbl, null);
                if (expense_code == "null" ) { expense_code = "CBT"; }

                dw_detail.SetItemString(1, "expense_code", expense_code);
                dw_detail.SetItemString(1, "expense_bank", expense_bank);
                dw_detail.SetItemString(1, "expense_branch", expense_branch);
                dw_detail.SetItemString(1, "expense_accid", expense_accid );
                dw_detail.SetItemString(1, "buttonc", buttonc);
                Hdbuttonc.Value = buttonc;

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
            if (eventArg == "refresh")
            {
                Refresh();
            }
            else if (eventArg == "jsExpensecode") {
                JsExpenseCode();
            }
            else if (eventArg == "jsGetexpensememno")
            {
                JsGetexpensememno();
            }
            else if (eventArg == "jsExpenseBank")
            {
                JsExpenseBank();
            }
            else if (eventArg == "jsExpensebankbrRetrieve")
            {
                JsExpensebankbrRetrieve();
            }

            
        }

        public void WebDialogLoadEnd()
        {
            dw_detail.SaveDataCache();

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
                        catch {
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
                String bankbranch;
                try { bankbranch = dw_detail.GetItemString(1, "expense_branch").Trim(); }
                catch { bankbranch = "0000"; }

                DataWindowChild dwExpenseBranch = dw_detail.GetChild("expense_branch_1");
                DwUtil.RetrieveDDDW(dw_detail, "expense_branch_1", "sl_loan_requestment_cen.pbl", bankCode);
                dwExpenseBranch.SetFilter("CMUCFBANKBRANCH.bank_code ='" + bankCode + "'");
                dwExpenseBranch.Filter();
                
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsExpenseCode()
        {
            //str_itemchange strList = new str_itemchange();
            //strList = WebUtil.str_itemchange_session(this);
            string expendCode = "";
            try
            {
                expendCode = dw_detail.GetItemString(1, "expense_code");
            }
            catch { }
            if (expendCode == null || expendCode == "") return;

            if ((expendCode == "CHQ") || (expendCode == "TRN") || (expendCode == "CBT") || (expendCode == "DRF") || (expendCode == "TBK"))
            {

                //ฝั่งธนาคาร
                 dw_detail.Modify("expense_bank.visible =1");
                 dw_detail.Modify("expense_bank_1.visible =1");
                 dw_detail.Modify("expense_branch.visible =1");
                 dw_detail.Modify("expense_branch_1.visible =1");
                dw_detail.Modify("b_expense_branch.visible = 1");

                try
                {

                    DwUtil.RetrieveDDDW(dw_detail, "expense_bank_1", pbl, null);
                    //DwUtil.RetrieveDDDW(dw_detail, "expense_branch_1", "sl_loan_requestment_cen.pbl", "006");
                    //DataWindowChild dwExpenseBranch = dw_detail.GetChild("expense_branch_1");
                    //DataWindowChild dwExpenseBank = dw_detail.GetChild("expense_bank_1");
                    // DwUtil.RetrieveDDDW(dw_detail, "loantype_code_1", pbl, null);
                    JsGetexpensememno();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if ((expendCode == "CSH") || (expendCode == "BEX") || (expendCode == "MON") || (expendCode == "MOS") || (expendCode == "MOO"))
            {

                //ฝั่งธนาคาร
                dw_detail.Modify("expense_bank.visible =0");
                dw_detail.Modify("expense_bank_1.visible =0");

                dw_detail.Modify("expense_branch.visible =0");
                dw_detail.Modify("expense_branch_1.visible =0");
                dw_detail.Modify("b_expense_branch.visible =0");


            }
        }

        public void Refresh() { }

        

    }
}
