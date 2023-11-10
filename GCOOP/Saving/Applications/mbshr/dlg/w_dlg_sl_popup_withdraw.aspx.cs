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
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;
using System.Data;

namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_sl_popup_withdraw : PageWebDialog, WebDialog
    {
        private n_shrlonClient srvShrlon;
        private DwThDate tDwMain;
        protected String calculateitempayamt;
        protected String saveWithdraw;
        protected String initDataWindow;
        protected String loanCalInt;
        protected String calculateAmt;
        protected String checkvalue;
        protected String fittermoneytype;
        protected String postrefresh;
        protected String jsBank;
        protected String jsBankBranch;

        public void InitJsPostBack()
        {
            saveWithdraw = WebUtil.JsPostBack(this, "saveWithdraw");
            initDataWindow = WebUtil.JsPostBack(this, "initDataWindow");
            loanCalInt = WebUtil.JsPostBack(this, "loanCalInt");
            calculateAmt = WebUtil.JsPostBack(this, "calculateAmt");
            calculateitempayamt = WebUtil.JsPostBack(this, "calculateitempayamt");
            checkvalue = WebUtil.JsPostBack(this, "checkvalue");
            fittermoneytype = WebUtil.JsPostBack(this, "fittermoneytype");
            postrefresh = WebUtil.JsPostBack(this, "postrefresh");
            jsBank = WebUtil.JsPostBack(this, "jsBank");
            jsBankBranch = WebUtil.JsPostBack(this, "jsBankBranch");
            tDwMain = new DwThDate(DwMain, this);
            //  tDwMain.Add("operate_date", "operate_tdate");
            tDwMain.Add("slip_date", "slip_tdate");
        }

        public void WebDialogLoadBegin()
        {
            srvShrlon = wcf.NShrlon;
            this.ConnectSQLCA();
            if (!IsPostBack)
            {
                this.InitDataWindow();
                DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
                String arg;
                try
                {
                    arg = Hfmoneytype_code.Value;
                    if (arg == "") { arg = "CSH"; }
                }
                catch { arg = "CSH"; }
                DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", arg);
                DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_slipall.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "expense_bank", "sl_slipall.pbl", null);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                //
                try
                {
                    String tDate1 = DwMain.GetItemString(1, "slip_tdate");
                    DateTime dt1 = DateTime.ParseExact(tDate1, "ddMMyyyy", WebUtil.TH);
                    DwMain.SetItemDateTime(1, "slip_date", dt1);
                }
                catch (Exception ex) { }
                this.RestoreContextDw(DwOperateLoan);
                this.RestoreContextDw(DwOperateEtc);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveWithdraw")
            {
                this.SaveWithdraw();
            }
            else if (eventArg == "loanCalInt")
            {
                this.LoanCalInt();
            }
            else if (eventArg == "initDataWindow")
            {
                this.InitDataWindow();
            }
            else if (eventArg == "calculateAmt")
            {
                this.CalculateAmt();
            }
            else if (eventArg == "calculateitempayamt")
            {
                this.Calculateitempayamt();
            }
            else if (eventArg == "checkvalue")
            {
                Checkvalue();
            }
            else if (eventArg == "fittermoneytype")
            {
                String moneytype_code = Hfmoneytype_code.Value;
                DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", moneytype_code);
            }
            else if (eventArg == "jsBank")
            {
                String bank_code = DwMain.GetItemString(1, "expense_bank");
                DwUtil.RetrieveDDDW(DwMain, "expense_branch", "sl_slipall.pbl", bank_code);
            }
            else if (eventArg == "jsBankBranch")
            {
                String bankbranch_code = DwMain.GetItemString(1, "expense_branch");
                Sta taChk = new Sta(sqlca.ConnectionString);
                String sql = "select fee_status from CMUCFBANKBRANCH where branch_id = '" + bankbranch_code + "'";
                Sdt dtChk = taChk.Query(sql);
                if (dtChk.Next())
                {
                    String fee_status = dtChk.GetString("fee_status");
                    String moneytype_code = DwMain.GetItemString(1, "moneytype_code");
                    double payoutnet_amt = Convert.ToDouble(DwMain.GetItemDecimal(1, "payoutnet_amt"));
                    if (fee_status == "1" && moneytype_code == "CBO")
                    {
                        payoutnet_amt = payoutnet_amt * 0.001;

                        if (payoutnet_amt < 10)
                        { payoutnet_amt = 10; }
                        else if (payoutnet_amt > 1000)
                        { payoutnet_amt = 1000; }

                        decimal payoutnet = Convert.ToDecimal(payoutnet_amt);
                        DwMain.SetItemDecimal(1, "bankfee_amt", payoutnet);
                    }
                }
            }
            else if (eventArg == "postrefresh") { };
        }

        private void Checkvalue()
        {
            String setshrarr_flag = DwMain.Describe("setshrarr_flag.Protect");
            String moneytype_code_1 = DwMain.Describe("moneytype_code_1.Protect");
            String moneytype_code = DwMain.Describe("moneytype_code.Protect");
            String expense_bank = DwMain.Describe("expense_bank.Protect");
            String expense_branch = DwMain.Describe(" expense_branch.Protect");
            String expense_accid = DwMain.Describe("expense_accid.Protect");
            if (DwMain.GetItemDecimal(1, "payoutnet_amt") < 0)
            {
                DwMain.Modify("setshrarr_flag.Protect=0");
                DwMain.Modify("moneytype_code_1.Protect=0");
                DwMain.Modify("moneytype_code.Protect=0");
                DwMain.Modify("expense_bank.Protect=0");
                DwMain.Modify("expense_branch.Protect=0");
                DwMain.Modify("expense_accid.Protect=0");
            }
            if (DwMain.GetItemDecimal(1, "setshrarr_flag") == 1)
            {
                // DwMain.Modify("setshrarr_flag.Protect=0");
                DwMain.Modify("moneytype_code_1.Protect=1");
                DwMain.Modify("moneytype_code.Protect=1");
                DwMain.Modify("expense_bank.Protect=1");
                DwMain.Modify("expense_branch.Protect=1");
                DwMain.Modify("expense_accid.Protect=1");
            }
            else
            {
                DwMain.Modify("moneytype_code_1.Protect=0");
                DwMain.Modify("moneytype_code.Protect=0");
                DwMain.Modify("expense_bank.Protect=0");
                DwMain.Modify("expense_branch.Protect=0");
                DwMain.Modify("expense_accid.Protect=0");
            }
        }

        private void Calculateitempayamt()
        {
            int loanAllRow = DwOperateLoan.RowCount;

            Decimal totalamt = 0;

            Decimal dwmain_bfshrcontbalamt = DwMain.GetItemDecimal(1, "bfshrcont_balamt");

            //ยอดโอนชำระ
            Decimal payoutclramt = 0;

            for (int i = 1; i <= loanAllRow; i++)
            {
                Decimal interest_period = DwOperateLoan.GetItemDecimal(i, "interest_period");
                Decimal bfintarr_amt = DwOperateLoan.GetItemDecimal(i, "bfintarr_amt");
                Decimal sum_interest_bfintarr = interest_period + bfintarr_amt;
                Decimal interest_payamt = DwOperateLoan.GetItemDecimal(i, "interest_payamt");
                Decimal itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");

                //ต้นเงิน bfshrcont_balamt
                Decimal dwloan_bfshrcontbalamt = DwOperateLoan.GetItemDecimal(i, "bfshrcont_balamt");
                Decimal operateflag = DwOperateLoan.GetItemDecimal(i, "operate_flag");
                if (itempayamt > dwloan_bfshrcontbalamt)
                {
                    Response.Write("<script>alert('ยอดชำระมากกว่า ต้นเงิน');</script>");
                }
                else
                {
                    if (itempayamt != 0)
                    {
                        if (operateflag == 1)
                        {
                            if (itempayamt < sum_interest_bfintarr)
                            {
                                DwOperateLoan.SetItemDecimal(i, "interest_payamt", sum_interest_bfintarr - itempayamt);

                                DwOperateLoan.SetItemDecimal(i, "principal_payamt", 0);
                            }
                            else if (itempayamt > sum_interest_bfintarr)
                            {
                                DwOperateLoan.SetItemDecimal(i, "interest_payamt", sum_interest_bfintarr);

                                DwOperateLoan.SetItemDecimal(i, "principal_payamt", itempayamt - sum_interest_bfintarr);
                            }
                        }
                    }
                    //ใช้คำนวณ ยอด ต้นคงเหลือ
                    Decimal principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt");
                    DwOperateLoan.SetItemDecimal(i, "item_balance", dwloan_bfshrcontbalamt - principalpayamt);

                    //หายอดรวมที่ต้องชำระ

                    itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");
                    totalamt += itempayamt;
                    payoutclramt += principalpayamt + interest_payamt;
                }
                //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดโอนชำระ payoutnet_amt
                DwMain.SetItemDecimal(1, "payoutclr_amt", payoutclramt);

                //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดหุ้นชำระ payoutnet_amt
                DwMain.SetItemDecimal(1, "payoutnet_amt", dwmain_bfshrcontbalamt - payoutclramt);
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
            DwOperateLoan.SaveDataCache();
            DwOperateEtc.SaveDataCache();
        }

        public void InitDataWindow()
        {
            int listIndex = 0;
            ArrayList dwList = new ArrayList();
            dwList = (ArrayList)Session["SSListWD"];

            HfAllIndex.Value = dwList.Count.ToString(); //จำนวน Index ของ Array Data ที่ส่งมาจากหน้า Sheet

            try { listIndex = Convert.ToInt32(HfIndex.Value); }
            catch { HfIndex.Value = "0"; listIndex = 0; }

            LbSaveStatus.Text = "(" + (listIndex + 1) + "/" + HfAllIndex.Value + ")";

                str_slippayout sSlipPayOut = new str_slippayout();
                sSlipPayOut = (str_slippayout)dwList[listIndex];

                sSlipPayOut.coop_id = state.SsCoopId;
                sSlipPayOut.contcoop_id = state.SsCoopControl;
                sSlipPayOut.memcoop_id = state.SsCoopControl;
                sSlipPayOut.entry_id = state.SsUsername;
                sSlipPayOut.operate_date = state.SsWorkDate;
                sSlipPayOut.slip_date = state.SsWorkDate;
                sSlipPayOut.initfrom_type = "SWD";
               
            try
            {
                int result = srvShrlon.of_initshrwtd(state.SsWsPass, ref sSlipPayOut);
                if (result == 1)
                {
                    DwMain.Reset();
                    DwUtil.ImportData(sSlipPayOut.xml_sliphead, DwMain, null, FileSaveAsType.Xml);
                    //DwMain.ImportString(sSlipPayOut.xml_sliphead, Sybase.DataWindow.FileSaveAsType.Xml);
                }               
                
                DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
                if (DwMain.RowCount > 1) { DwUtil.DeleteLastRow(DwMain); }
                HfFormType.Value = sSlipPayOut.initfrom_type;
                tDwMain.Eng2ThaiAllRow();
                DwMain.SetItemDecimal(1, "payoutnet_amt", 0);
            }
            catch (Exception ex) { String ext = ex.ToString(); }
            try
            {
                if (sSlipPayOut.xml_slipcutlon == "")
                {
                    TextBox1.Text = "";
                    DwOperateLoan.Reset();
                }
                else
                {
                    TextBox1.Text = sSlipPayOut.xml_slipcutlon;
                    //DwOperateLoan.ImportString(sSlipPayOut.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);
                    DwUtil.ImportData(sSlipPayOut.xml_slipcutlon, DwOperateLoan, null, FileSaveAsType.Xml);
                }
            }
            catch (Exception ex) { String ext = ex.ToString(); }
            try
            {
                DwOperateEtc.Reset();
                // DwOperateEtc.ImportString(sSlipPayOut.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
                DwUtil.ImportData(sSlipPayOut.xml_slipcutetc, DwOperateEtc, null, FileSaveAsType.Xml);
            }
            catch (Exception ex) { String ext = ex.ToString(); }
            Decimal payout_amt, payoutclr_amt;
            try { payout_amt = DwMain.GetItemDecimal(1, "payout_amt"); }
            catch { payout_amt = 0; }
            try
            {
                payoutclr_amt = DwMain.GetItemDecimal(1, "payoutclr_amt");
            }
            catch { payoutclr_amt = 0; }
            DwMain.SetItemDecimal(DwMain.RowCount, "payoutnet_amt", payout_amt - payoutclr_amt);
        }

        private void SaveWithdraw()
        {
            String memno = DwMain.GetItemString(1, "member_no");
            int index = Convert.ToInt32(HfIndex.Value);
            int allIndex = Convert.ToInt32(HfAllIndex.Value);

            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.contcoop_id = state.SsCoopControl;
            strPayOut.memcoop_id = state.SsCoopControl;
            strPayOut.entry_id = state.SsUsername;
            //strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
            strPayOut.member_no = memno;
            strPayOut.slip_date = DwMain.GetItemDateTime(1, "slip_date");// state.SsWorkDate;
            strPayOut.initfrom_type = HfFormType.Value;

            String dwMainXML = "";
            String dwLoanXML = "";
            String dwEtcXML = "";

            dwMainXML = DwMain.Describe("DataWindow.Data.XML");
            try { dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML"); }
            catch { dwLoanXML = ""; }
            try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
            catch { dwEtcXML = ""; }

            strPayOut.xml_sliphead = dwMainXML;
            strPayOut.xml_slipcutlon = dwLoanXML;
            strPayOut.xml_slipcutetc = dwEtcXML;

            try
            {
                int result = srvShrlon.of_saveslip_shrwtd(state.SsWsPass, ref strPayOut);

                int nextIndex = index + 1;
                if (nextIndex > allIndex)
                {
                    nextIndex = index - 1;
                }
                HfIndex.Value = nextIndex.ToString();

                if (nextIndex != allIndex)
                {
                    this.InitDataWindow();
                }
                string payinslip_no = strPayOut.payinslip_no;
                string coop_id = state.SsCoopControl;
                Printing.PrintSlipSlpayin(this, payinslip_no, coop_id);
                //PrintPaymentVoucher();
            }
            catch (Exception ex)
            {
                // Response.Write("<script>alert('ไม่สามารถบันทึกเลขทะเบียน " + memno + " ได้');</script>"); 
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
        }

        private void LoanCalInt()
        {
            try
            {
                tDwMain.Eng2ThaiAllRow();
                DateTime dt = DwMain.GetItemDateTime(1, "slip_date");
                //dt = new DateTime();
                if (TextBox1.Text == "") { CalculateAmt(); }
                else
                {
                    String as_xmlloan = DwOperateLoan.Describe("DataWindow.Data.XML");
                    String as_sliptype = DwMain.GetItemString(1, "sliptype_code");
                    Int32 xmlloan = srvShrlon.of_initslippayin_calint(state.SsWsPass,ref as_xmlloan, as_sliptype, dt);
                    DwOperateLoan.Reset();
                    DwOperateLoan.ImportString(as_xmlloan, FileSaveAsType.Xml);
                    CalculateAmt();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void CalculateAmt()
        {
            int loanAllRow = DwOperateLoan.RowCount;
            Decimal principalpayamt = 0;
            Decimal interest_payamt = 0;
            Decimal totalamt = 0;

            Decimal dwmain_bfshrcontbalamt = DwMain.GetItemDecimal(1, "bfshrcont_balamt");
            //ยอดโอนชำระ
            Decimal payoutclramt = 0;

            int protectcalculate = 1;

            for (int i = 1; i <= loanAllRow; i++)
            {
                Decimal itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");
                //ต้นเงิน bfshrcont_balamt
                Decimal dwloan_bfshrcontbalamt = DwOperateLoan.GetItemDecimal(i, "bfshrcont_balamt");
                //การชำระ ต้นเงิน
                principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt");
                interest_payamt = DwOperateLoan.GetItemDecimal(i, "interest_payamt");
                //ดอกเบี้ยสะสม interest_period + bfintarr_amt 
                Decimal interestperiod;
                try
                {
                    interestperiod = DwOperateLoan.GetItemDecimal(i, "interest_period");
                }
                catch { interestperiod = 0; }
                Decimal bfintarramt = DwOperateLoan.GetItemDecimal(i, "bfintarr_amt");

                Decimal operateflag = DwOperateLoan.GetItemDecimal(i, "operate_flag");
                if (itempayamt == 0)
                {
                    if (operateflag == 1)
                    {
                        if (dwmain_bfshrcontbalamt < dwloan_bfshrcontbalamt)
                        {
                            if (protectcalculate == 1)
                            {
                                DwOperateLoan.SetItemDecimal(i, "principal_payamt", dwmain_bfshrcontbalamt);
                                principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt");
                                protectcalculate += 1;
                            }
                        }
                        DwOperateLoan.SetItemDecimal(i, "interest_payamt", interestperiod + bfintarramt);
                        Decimal interest_payamt_af = DwOperateLoan.GetItemDecimal(i, "interest_payamt");
                        DwOperateLoan.SetItemDecimal(i, "item_payamt", principalpayamt + interest_payamt_af);
                    }
                }
                else if (itempayamt != 0)
                {

                    DwOperateLoan.SetItemDecimal(i, "item_payamt", principalpayamt + interest_payamt);
                }
                //ใช้คำนวณ ยอด ต้นคงเหลือ
                principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt");
                DwOperateLoan.SetItemDecimal(i, "item_balance", dwloan_bfshrcontbalamt - principalpayamt);

                //หายอดรวมที่ต้องชำระ

                itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");
                totalamt = totalamt + itempayamt;
            }

            int EtcAllRow = DwOperateEtc.RowCount;
            for (int x = 1; x <= EtcAllRow; x++)
            {
                Decimal etcFlag = DwOperateEtc.GetItemDecimal(x, "operate_flag");
                if (etcFlag == 1)
                {
                    Decimal item_payamt = DwOperateEtc.GetItemDecimal(x, "item_payamt");

                    if (item_payamt != 0)
                    {
                        totalamt = totalamt + item_payamt;
                    }
                }
            }

            payoutclramt = payoutclramt + totalamt;

            //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดโอนชำระ payoutnet_amt
            DwMain.SetItemDecimal(1, "payoutclr_amt", payoutclramt);

            //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดหุ้นชำระ payoutnet_amt
            DwMain.SetItemDecimal(1, "payoutnet_amt", dwmain_bfshrcontbalamt - payoutclramt);

            if ((dwmain_bfshrcontbalamt - payoutclramt) < 0)
            {

                String setshrarr_flag = DwMain.Describe("setshrarr_flag.Protect");
                String moneytype_code_1 = DwMain.Describe("moneytype_code_1.Protect");
                String moneytype_code = DwMain.Describe("moneytype_code.Protect");
                String expense_bank = DwMain.Describe("expense_bank.Protect");
                String expense_branch = DwMain.Describe(" expense_branch.Protect");
                String expense_accid = DwMain.Describe("expense_accid.Protect");
                DwMain.Modify("setshrarr_flag.Protect=0");
                DwMain.Modify("moneytype_code_1.Protect=0");
                DwMain.Modify("moneytype_code.Protect=0");

                DwMain.Modify("expense_bank.Protect=0");
                DwMain.Modify("expense_branch.Protect=0");
                DwMain.Modify("expense_accid.Protect=0");

                // dw_data.Modify("recv_shrstatus.Protect='1~tIf(IsRowNew(),0,1)'");
                // String setting1 = DwMain.Describe("setshrarr_flag.Protect"); //Checkvalue();
            }
        }

        private void PrintPaymentVoucher()
        {
            DateTime slip_date = DwMain.GetItemDateTime(1, "slip_date");
            String memno = DwMain.GetItemString(1, "member_no");
            String sql = "";
            sql = "select  a.document_no,a.member_no,a.slip_date,a.payout_amt," +
                  "b.memb_name,b.memb_surname," +
                  "c.membgroup_desc," +
                  "d.prename_desc " +
                  "from slslippayout a, mbmembmaster b , mbucfmembgroup c , mbucfprename d " +
                  "where a.member_no = b.member_no and a.memcoop_id='" + state.SsCoopId + "' and " +
                  "b.membgroup_code = c.membgroup_code and " +
                  "b.prename_code = d.prename_code and " +
                  "a.sliptype_code = 'SWD' and " +
                  "a.slip_date = to_date('" + slip_date.ToShortDateString() + "','dd/mm/yyyy') and " +
                  "a.member_no = '" + memno + "'";
            DataTable data = WebUtil.Query(sql);
            //Printing.Print(page, "Slip/shrlon/loanrequest_norm.aspx", data, 10);
            int printMode = xmlconfig.ShrlonPrintMode;
            if (printMode == 1)
            {
                Printing.Print(this, "", data, 10);
            }
            else
            {
                Printing.PrintApplet(this, "fin_payment_voucher", data);
                //shrlon.LoanrequestNorm
            }
        }
    }
}
