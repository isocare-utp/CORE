using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_sl_share_withdraw : PageWebSheet, WebSheet
    {
        private n_shrlonClient srvShrlon;
        private DwThDate tDwMain;
        protected String calculateitempayamt;
        protected String saveWithdraw;
        protected String initDataWindow;
        protected String loanCalInt;
        protected String calculateAmt;
        protected String checkvalue;
        protected String getMemberNo;
        protected String fittermoneytype;
        protected String initLnRcvlist;
        protected String newclear;
        public void InitJsPostBack()
        {
            saveWithdraw = WebUtil.JsPostBack(this, "saveWithdraw");
            initDataWindow = WebUtil.JsPostBack(this, "initDataWindow");
            loanCalInt = WebUtil.JsPostBack(this, "loanCalInt");
            calculateAmt = WebUtil.JsPostBack(this, "calculateAmt");
            initLnRcvlist = WebUtil.JsPostBack(this, "initLnRcvlist");
            fittermoneytype = WebUtil.JsPostBack(this, "fittermoneytype");
            calculateitempayamt = WebUtil.JsPostBack(this, "calculateitempayamt");
            newclear = WebUtil.JsPostBack(this, "newclear");
            checkvalue = WebUtil.JsPostBack(this, "checkvalue");
            getMemberNo = WebUtil.JsPostBack(this, "getMemberNo");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("operate_date", "operate_tdate");
            tDwMain.Add("slip_date", "slip_tdate");

        }

        public void WebSheetLoadBegin()
        {


            srvShrlon = wcf.NShrlon;

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwOperateLoan.InsertRow(0);
                DwOperateEtc.InsertRow(0);
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
                dw_list.Visible = false;
                HdIsPostBack.Value = "false";
                CheckBox1.Checked = true;

            }
            else
            {
                this.RestoreContextDw(DwMain);
                try
                {
                    String dtString = DwMain.GetItemString(1, "operate_tdate");
                    dtString = dtString.Replace("/", "");
                    DwMain.SetItemDateTime(1, "operate_date", DateTime.ParseExact(dtString, "ddMMyyyy", WebUtil.TH));
                }
                catch { }
                this.RestoreContextDw(DwOperateLoan);
                this.RestoreContextDw(DwOperateEtc);
                this.RestoreContextDw(dw_list);
                HdIsPostBack.Value = "true";
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
            else if (eventArg == "getMemberNo")
            {
                this.InitDataWindow();
            }
            else if (eventArg == "initLnRcvlist")
            {
                InitLnRcvlist();
            }
            else if (eventArg == "fittermoneytype")
            {
                //String moneytype_code = Hfmoneytype_code.Value;
                //DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", moneytype_code);
                String moneytype_code = Hfmoneytype_code.Value;
                DwUtil.RetrieveDDDW(DwMain, "tofrom_accid", "sl_slipall.pbl", moneytype_code);
                DataWindowChild dwExpenseBranch = DwMain.GetChild("tofrom_accid");
                dwExpenseBranch.SetFilter("CMUCFTOFROMACCID.MONEYTYPE_CODE='" + moneytype_code + "'");
                dwExpenseBranch.Filter();
            }
            else if (eventArg == "newclear")
            {
                Newclear();
            }
        }

        private void Newclear()
        {
            if (DwMain.RowCount > 1)
                {
                    DwUtil.DeleteLastRow(DwMain);
                }
            if (DwOperateLoan.RowCount > 1)
            {
                DwUtil.DeleteLastRow(DwOperateLoan);
            } if (DwOperateEtc.RowCount > 1)
            {
                DwUtil.DeleteLastRow(DwOperateEtc);
            }    
          
            DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
            String moneytype_code = Hfmoneytype_code.Value;
            DwUtil.RetrieveDDDW(DwMain, "tofrom_accid", "sl_slipall.pbl", moneytype_code);

            DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_slipall.pbl", null);
            dw_list.Visible = false;
            HdIsPostBack.Value = "false";
            CheckBox1.Checked = true;






        }

        private void InitLnRcvlist()
        {


            str_slippayout strslippayout = new str_slippayout();
            strslippayout.initfrom_type = "SWD";
            strslippayout.payoutorder_no = Hfpayoutorder_no.Value;
            strslippayout.slip_date = state.SsWorkDate;// dw_main.GetItemDateTime(1, "operate_date");              
            strslippayout.xml_sliphead = DwMain.Describe("DataWindow.Data.XML");
            strslippayout.xml_slipcutlon = DwOperateLoan.Describe("DataWindow.Data.XML");
            strslippayout.xml_slipcutetc = DwOperateEtc.Describe("DataWindow.Data.XML");

            try
            {

                int result = srvShrlon.of_initshrwtd(state.SsWsPass, ref strslippayout);

                if (result == 1)
                {
                    try
                    {
                        DwMain.Reset();
                        DwMain.ImportString(strslippayout.xml_sliphead, FileSaveAsType.Xml);
                        DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
                        DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", null);
                        tDwMain.Eng2ThaiAllRow();
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex + "DwMain"); }

                    try
                    {
                        DwOperateLoan.Reset();
                        DwOperateLoan.ImportString(strslippayout.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);
                    }
                    catch (Exception ex)
                    {
                        if (strslippayout.xml_slipcutlon == "") { DwOperateLoan.Reset(); }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex + "DwOperateLoan");
                        }
                    }
                    try
                    {
                        DwOperateEtc.Reset();
                        DwOperateEtc.ImportString(strslippayout.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
                    }
                    catch (Exception ex)
                    {
                        if (strslippayout.xml_slipcutetc == "") { DwOperateEtc.Reset(); }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex + "DwOperateEtc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                DwMain.Reset();
                DwOperateLoan.Reset();
                DwOperateEtc.Reset();
                DwMain.InsertRow(0);
                DwOperateLoan.InsertRow(0);
                DwOperateEtc.InsertRow(0);
                dw_list.Visible = false;
            }

        }

        private void GetMemberNo()
        {

        }

        public void SaveWebSheet()
        {
            SaveWithdraw();
        }

        public void WebSheetLoadEnd()
        {
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
            DwMain.SaveDataCache();
            DwOperateLoan.SaveDataCache();
            DwOperateEtc.SaveDataCache();
            dw_list.SaveDataCache();
        }

        private void InitDataWindow()
        {
            String as_memno = HfMemberNo.Value;
            String as_sliptype = "SWD";
            String as_ordno = "";
            String as_xmlordlist = dw_list.Describe("DataWindow.Data.XML");
            int result = srvShrlon.of_getmembshrwtd(state.SsWsPass, as_memno, as_sliptype, ref as_ordno, ref as_xmlordlist);
            if (result == 1)
            {
                str_slippayout astr_slippayout = new str_slippayout();

                try
                {

                    dw_list.Visible = false;
                    astr_slippayout.xml_sliphead = DwMain.Describe("DataWindow.Data.XML");
                    astr_slippayout.xml_slipcutlon = DwOperateLoan.Describe("DataWindow.Data.XML");
                    astr_slippayout.xml_slipcutetc = DwOperateEtc.Describe("DataWindow.Data.XML");
                    astr_slippayout.member_no = HfMemberNo.Value;
                    astr_slippayout.operate_date = state.SsWorkDate;
                    astr_slippayout.slip_date = state.SsWorkDate;
                    astr_slippayout.payoutorder_no = as_ordno;
                    int re = srvShrlon.of_initshrwtd(state.SsWsPass, ref astr_slippayout);

                    if (re == 1)
                    {
                        try
                        {
                            DwMain.Reset();
                            DwMain.ImportString(astr_slippayout.xml_sliphead, Sybase.DataWindow.FileSaveAsType.Xml);
                            DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
                            DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", null);
                            tDwMain.Eng2ThaiAllRow();
                            DwMain.SetItemDecimal(1, "payoutnet_amt", 0);

                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex + "DwMain"); }
                        try
                        {
                            DwOperateLoan.Reset();
                            DwOperateLoan.ImportString(astr_slippayout.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);
                        }
                        catch (Exception ex)
                        {
                            if (astr_slippayout.xml_slipcutlon == "") { DwOperateLoan.Reset(); }
                            else
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(ex + "DwOperateLoan");
                            }
                        }
                        try
                        {
                            DwOperateEtc.Reset();
                            DwOperateEtc.ImportString(astr_slippayout.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
                        }
                        catch (Exception ex)
                        {
                            if (astr_slippayout.xml_slipcutetc == "") { DwOperateEtc.Reset(); }
                            else
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(ex + "DwOperateEtc");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    DwMain.Reset();
                    DwOperateLoan.Reset();
                    DwOperateEtc.Reset();
                    DwMain.InsertRow(0);
                    DwOperateLoan.InsertRow(0);
                    DwOperateEtc.InsertRow(0);
                }
            }
            else if (result == 2)
            {
                dw_list.Visible = true;
                dw_list.Reset();
                TextBox2.Text = as_xmlordlist;
                dw_list.ImportString(as_xmlordlist, Sybase.DataWindow.FileSaveAsType.Xml);
            }

            Decimal dwmain_bfshrcontbalamt = DwMain.GetItemDecimal(1, "bfshrcont_balamt");
            Decimal payoutclr_amt = DwMain.GetItemDecimal(1, "payoutclr_amt");
            DwMain.SetItemDecimal(1, "payoutnet_amt", dwmain_bfshrcontbalamt - payoutclr_amt);
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

        private void SaveWithdraw()
        {
            String memno = DwMain.GetItemString(1, "member_no");


            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.entry_id = state.SsUsername;
            strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
            strPayOut.member_no = memno;
            strPayOut.slip_date = state.SsWorkDate;
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
                string fromset = "";
                try
                {
                    fromset = state.SsPrinterSet;

                }
                catch (Exception ex)
                {
                    fromset = "216";

                }
                if (CheckBox1.Checked == true)
                {
                    try
                    {
                        string payinslip_no = strPayOut.payinslip_no;
                        //string re = srvShrlon.of_printreceiptloan(state.SsWsPass, payinslip_no, fromset);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                //Response.Write("<script>alert('ไม่สามารถบันทึกเลขทะเบียน " + memno + " ได้');</script>"); 
            }
            Newclear();

        }

        private void LoanCalInt()
        {

            try
            {
                DateTime dt = new DateTime();
                dt = DwMain.GetItemDateTime(1, "operate_date");
                String as_xmlloan = DwOperateLoan.Describe("DataWindow.Data.XML");
                String as_sliptype = DwMain.GetItemString(1, "sliptype_code");
                Int32 xmlloan = srvShrlon.of_initslippayin_calint(state.SsWsPass,ref as_xmlloan, as_sliptype, dt);
                DwOperateLoan.Reset();
                DwOperateLoan.ImportString(as_xmlloan, FileSaveAsType.Xml);
                CalculateAmt();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }
        }

        private void CalculateAmt()
        {
            int loanAllRow = DwOperateLoan.RowCount;

            Decimal totalamt = 0;
            Decimal interest_payamt = 0;
            Decimal principalpayamt = 0;
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
                Decimal interestperiod = DwOperateLoan.GetItemDecimal(i, "interest_period");
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


            payoutclramt = payoutclramt + totalamt + interest_payamt;

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
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
