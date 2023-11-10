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
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfShrlon;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_sl_share_withdraw_partial : PageWebSheet, WebSheet
    {
        protected String initDataWindow;
        protected String newclear;
        private DwThDate dwMainThDate;
        private n_shrlonClient srvShrlon;
        //private ShrlonClient srvShrlon;
        protected String loanCalInt;
        protected String calculateAmt;
        protected String calculateitempayamt;
        protected String initLnRcvlist;
        protected String fittermoneytype;



        public void InitJsPostBack()
        {
            initDataWindow = WebUtil.JsPostBack(this, "initDataWindow");
            newclear = WebUtil.JsPostBack(this, "newclear");
            initLnRcvlist = WebUtil.JsPostBack(this, "initLnRcvlist");
            fittermoneytype = WebUtil.JsPostBack(this, "fittermoneytype");
            loanCalInt = WebUtil.JsPostBack(this, "loanCalInt");
            calculateAmt = WebUtil.JsPostBack(this, "calculateAmt");
            calculateitempayamt = WebUtil.JsPostBack(this, "calculateitempayamt");
            dwMainThDate = new DwThDate(dw_main, this);
            dwMainThDate.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                //srvShrlon = wcf.NShrlon;
                srvShrlon = wcf.NShrlon;
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(DwOperateLoan);
                this.RestoreContextDw(DwOperateEtc);
                HdIsPostBack.Value = "true";
            }
            else
            {
                dw_main.InsertRow(0);
                dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "tofrom_accid_1", "sl_slipall.pbl", null);
                dwMainThDate.Eng2ThaiAllRow();
                HdIsPostBack.Value = "false";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "initDataWindow")
            {
                this.InitDataWindow();
            }
            else if (eventArg == "newclear")
            {

                if (dw_main.RowCount > 1)
                {
                    DwUtil.DeleteLastRow(dw_main);
                }
                else
                {
                    DateTime operate_date = Convert.ToDateTime(Session["operate_date"].ToString());
                    dw_main.SetItemDateTime(1, "operate_date", operate_date);
                    DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
                    DwUtil.RetrieveDDDW(dw_main, "tofrom_accid_1", "sl_slipall.pbl", null);
                    dwMainThDate.Eng2ThaiAllRow();
                }
                HdIsPostBack.Value = "false";
            }
            else if (eventArg == "loanCalInt")
            {
                this.LoanCalInt();
            }

            else if (eventArg == "calculateAmt")
            {
                this.CalculateAmt();
            }
            else if (eventArg == "calculateitempayamt")
            {

                this.Calculateitempayamt();
            }
            else if (eventArg == "initLnRcvlist")
            {
                InitLnRcvlist();
            }
            else if (eventArg == "fittermoneytype")
            {
                String moneytype_code = Hfmoneytype_code.Value;
                DwUtil.RetrieveDDDW(dw_main, "tofrom_accid_1", "sl_slipall.pbl", moneytype_code);
            }
        }

        public void SaveWebSheet()
        {
            str_slippayout strslippayout = new str_slippayout();
            strslippayout.initfrom_type = "SWP";
            strslippayout.member_no = WebUtil.StringFormat(dw_main.GetItemString(1, "member_no"), "000000");
            strslippayout.slip_date = dw_main.GetItemDateTime(1, "operate_date");
            strslippayout.operate_date = dw_main.GetItemDateTime(1, "operate_date");
            Session["operate_date"] = dw_main.GetItemDateTime(1, "operate_date");

            strslippayout.entry_id = state.SsUsername;
            strslippayout.contcoop_id = state.SsCoopControl;
            strslippayout.coop_id = state.SsCoopId;
            strslippayout.xml_sliphead = dw_main.Describe("DataWindow.Data.XML");
            strslippayout.xml_slipcutlon = DwOperateLoan.Describe("DataWindow.Data.XML");
            strslippayout.xml_slipcutetc = DwOperateEtc.Describe("DataWindow.Data.XML");

            try
            {
                //int result = srvShrlon.PostShareWithdraw(state.SsWsPass, ref strslippayout);
                int result = srvShrlon.of_saveslip_shrwtd(state.SsWsPass, ref strslippayout);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    dw_main.Reset();
                    dw_main.InsertRow(0);
                    DateTime operate_date = Convert.ToDateTime(Session["operate_date"].ToString());
                    dw_main.SetItemDateTime(1, "operate_date", operate_date);
                    dwMainThDate.Eng2ThaiAllRow();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            DwOperateEtc.SaveDataCache();
            DwOperateLoan.SaveDataCache();
        }



        private void InitDataWindow()
        {
            str_slippayout strslippayout = new str_slippayout();
            strslippayout.initfrom_type = "SWP";
            strslippayout.member_no = WebUtil.StringFormat(dw_main.GetItemString(1, "member_no"), "000000");
            strslippayout.slip_date = dw_main.GetItemDateTime(1, "operate_date");
            strslippayout.operate_date = dw_main.GetItemDateTime(1, "operate_date");
            strslippayout.entry_id = state.SsUsername;
            strslippayout.contcoop_id = state.SsCoopId;
            strslippayout.coop_id = state.SsCoopId;
            strslippayout.xml_sliphead = dw_main.Describe("DataWindow.Data.XML");
            strslippayout.xml_slipcutlon = DwOperateLoan.Describe("DataWindow.Data.XML");
            strslippayout.xml_slipcutetc = DwOperateEtc.Describe("DataWindow.Data.XML");


            try
            {

                //srvShrlon.InitShareWithdraw(state.SsWsPass, ref strslippayout);
                srvShrlon.of_initshrwtd(state.SsWsPass, ref strslippayout);

                try
                {
                    dw_main.Reset();
                    dw_main.ImportString(strslippayout.xml_sliphead, FileSaveAsType.Xml);
                    DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
                    String arg;
                    try
                    {
                        arg = Hfmoneytype_code.Value;
                        if (arg == "") { arg = "CSH"; }
                    }
                    catch { arg = "CSH"; }
                    DwUtil.RetrieveDDDW(dw_main, "tofrom_accid_1", "sl_slipall.pbl", arg);

                    dwMainThDate.Eng2ThaiAllRow();
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
                    DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_slipall.pbl", null);
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
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex); dw_main.Reset();
                DwOperateLoan.Reset();
                DwOperateEtc.Reset();
                dw_main.InsertRow(0);
                DwOperateLoan.InsertRow(0);
                DwOperateEtc.InsertRow(0);
            }




        }
        private void InitLnRcvlist()
        {


            str_slippayout strslippayout = new str_slippayout();
            strslippayout.initfrom_type = "SWP";
            strslippayout.payoutorder_no = Hfpayoutorder_no.Value;
            strslippayout.slip_date = state.SsWorkDate;// dw_main.GetItemDateTime(1, "operate_date");              
            strslippayout.xml_sliphead = dw_main.Describe("DataWindow.Data.XML");
            strslippayout.xml_slipcutlon = DwOperateLoan.Describe("DataWindow.Data.XML");
            strslippayout.xml_slipcutetc = DwOperateEtc.Describe("DataWindow.Data.XML");

            try
            {

                int result = srvShrlon.of_initshrwtd(state.SsWsPass, ref strslippayout);

                if (result == 1)
                {
                    try
                    {
                        dw_main.Reset();
                        dw_main.ImportString(strslippayout.xml_sliphead, FileSaveAsType.Xml);
                        DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
                        DwUtil.RetrieveDDDW(dw_main, "tofrom_accid_1", "sl_slipall.pbl", null);
                        dwMainThDate.Eng2ThaiAllRow();
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
                dw_main.Reset();
                DwOperateLoan.Reset();
                DwOperateEtc.Reset();
                dw_main.InsertRow(0);
                DwOperateLoan.InsertRow(0);
                DwOperateEtc.InsertRow(0);
                dw_list.Visible = false;
            }

        }
        private void LoanCalInt()
        {

            try
            {
                DateTime dt = new DateTime();
                dt = dw_main.GetItemDateTime(1, "operate_date");
                String as_xmlloan = DwOperateLoan.Describe("DataWindow.Data.XML");
                String as_sliptype = dw_main.GetItemString(1, "sliptype_code");
                //String xmlloan = srvShrlon.InitSlipPayInCalInt(state.SsWsPass, as_xmlloan, as_sliptype, dt);
                int res = srvShrlon.of_initslippayin_calint(state.SsWsPass,ref as_xmlloan, as_sliptype, dt);
                String xmlloan = as_xmlloan;
                DwOperateLoan.Reset();
                DwOperateLoan.ImportString(xmlloan, FileSaveAsType.Xml);
                CalculateAmt();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }
        }

        private void CalculateAmt()
        {
            int loanAllRow = DwOperateLoan.RowCount;

            Decimal totalamt = 0;
            Decimal payout_amt = dw_main.GetItemDecimal(1, "payout_amt");
            Decimal dwmain_bfshrcontbalamt = dw_main.GetItemDecimal(1, "bfshrcont_balamt");
            //ยอดโอนชำระ
            Decimal payoutclramt = 0;

            int protectcalculate = 1;

            for (int i = 1; i <= loanAllRow; i++)
            {
                Decimal itempayamt;
                try { itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt"); }
                catch { itempayamt = 0; }
                //ต้นเงิน bfshrcont_balamt
                Decimal dwloan_bfshrcontbalamt;
                try { dwloan_bfshrcontbalamt = DwOperateLoan.GetItemDecimal(i, "bfshrcont_balamt"); }
                catch { dwloan_bfshrcontbalamt = 0; }
                //การชำระ ต้นเงิน
                Decimal principalpayamt;
                try { principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt"); }
                catch { principalpayamt = 0; }
                Decimal interest_payamt;
                try { interest_payamt = DwOperateLoan.GetItemDecimal(i, "interest_payamt"); }
                catch { interest_payamt = 0; }
                //ดอกเบี้ยสะสม interest_period + bfintarr_amt 
                Decimal interestperiod;
                try { interestperiod = DwOperateLoan.GetItemDecimal(i, "interest_period"); }
                catch { interestperiod = 0; }
                Decimal bfintarramt; 
                try { bfintarramt = DwOperateLoan.GetItemDecimal(i, "bfintarr_amt"); }
                catch { bfintarramt = 0; }

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
                totalamt += itempayamt;
                payoutclramt += principalpayamt + interest_payamt;
            }
            //Set ค่า TotalAmt ไปที่ dw_main ช่อง ยอดโอนชำระ payoutnet_amt
            dw_main.SetItemDecimal(1, "payoutclr_amt", payoutclramt);

            //Set ค่า TotalAmt ไปที่ dw_main ช่อง ยอดหุ้นชำระ payoutnet_amt
            dw_main.SetItemDecimal(1, "payoutnet_amt", payout_amt - payoutclramt);

            if ((dwmain_bfshrcontbalamt - payoutclramt) < 0)
            {

                String setshrarr_flag = dw_main.Describe("setshrarr_flag.Protect");
                String moneytype_code_1 = dw_main.Describe("moneytype_code_1.Protect");
                String moneytype_code = dw_main.Describe("moneytype_code.Protect");
                String expense_bank = dw_main.Describe("expense_bank.Protect");
                String expense_branch = dw_main.Describe(" expense_branch.Protect");
                String expense_accid = dw_main.Describe("expense_accid.Protect");
                dw_main.Modify("setshrarr_flag.Protect=0");
                dw_main.Modify("moneytype_code_1.Protect=0");
                dw_main.Modify("moneytype_code.Protect=0");

                dw_main.Modify("expense_bank.Protect=0");
                dw_main.Modify("expense_branch.Protect=0");
                dw_main.Modify("expense_accid.Protect=0");

                // dw_data.Modify("recv_shrstatus.Protect='1~tIf(IsRowNew(),0,1)'");
                // String setting1 = dw_main.Describe("setshrarr_flag.Protect"); //Checkvalue();
            }
        }

        private void Calculateitempayamt()
        {
            //int loanAllRow = DwOperateLoan.RowCount;

            //Decimal totalamt = 0;

            //Decimal payout_amt = dw_main.GetItemDecimal(1, "payout_amt");


            //int EtcAllRow = DwOperateEtc.RowCount;
            //for (int x = 1; x <= EtcAllRow; x++)
            //{

            //    String etcFlag = DwOperateEtc.GetItemString(x, "operate_flag");
            //    if (etcFlag == "1")
            //    {
            //        Decimal item_payamt = DwOperateEtc.GetItemDecimal(x, "item_payamt");

            //        if (item_payamt != 0)
            //        {
            //            totalamt = totalamt + item_payamt;

            //        }

            //    }

            //}

            ////ยอดโอนชำระ
            //Decimal payoutclramt = 0;
            //for (int i = 1; i <= loanAllRow; i++)
            //{
            //    Decimal interest_period = DwOperateLoan.GetItemDecimal(i, "interest_period");
            //    Decimal bfintarr_amt = DwOperateLoan.GetItemDecimal(i, "bfintarr_amt");
            //    Decimal sum_interest_bfintarr = interest_period + bfintarr_amt;
            //    Decimal interest_payamt = DwOperateLoan.GetItemDecimal(i, "interest_payamt");
            //    Decimal itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");

            //    //ต้นเงิน bfshrcont_balamt
            //    //  Decimal payout_amt = DwOperateLoan.GetItemDecimal(i, "payout_amt");
            //    Decimal operateflag = DwOperateLoan.GetItemDecimal(i, "operate_flag");
            //    if (itempayamt > payout_amt)
            //    {
            //        Response.Write("<script>alert('ยอดชำระมากกว่า ต้นเงิน');</script>");

            //    }
            //    else
            //    {
            //        if (itempayamt != 0)
            //        {
            //            if (operateflag == 1)
            //            {
            //                if (itempayamt < sum_interest_bfintarr)
            //                {
            //                    DwOperateLoan.SetItemDecimal(i, "interest_payamt", sum_interest_bfintarr - itempayamt);

            //                    DwOperateLoan.SetItemDecimal(i, "principal_payamt", 0);
            //                }
            //                else if (itempayamt > sum_interest_bfintarr)
            //                {
            //                    DwOperateLoan.SetItemDecimal(i, "interest_payamt", sum_interest_bfintarr);

            //                    DwOperateLoan.SetItemDecimal(i, "principal_payamt", itempayamt - sum_interest_bfintarr);
            //                }

            //            }
            //        }


            //        //ใช้คำนวณ ยอด ต้นคงเหลือ
            //        Decimal principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt");
            //        DwOperateLoan.SetItemDecimal(i, "item_balance", payout_amt - principalpayamt);

            //        //หายอดรวมที่ต้องชำระ

            //        itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");
            //        totalamt += itempayamt;
            //        payoutclramt += principalpayamt + interest_payamt;
            //    }
            //    //Set ค่า TotalAmt ไปที่ dw_main ช่อง ยอดโอนชำระ payoutnet_amt
            //    dw_main.SetItemDecimal(1, "payoutclr_amt", payoutclramt);

            //    //Set ค่า TotalAmt ไปที่ dw_main ช่อง ยอดหุ้นชำระ payoutnet_amt
            //    dw_main.SetItemDecimal(1, "payoutnet_amt", payout_amt - payoutclramt);
            //}


            int loanAllRow = DwOperateLoan.RowCount;

            Decimal totalamt = 0;

            Decimal interest_period = 0;
            Decimal bfintarr_amt = 0;
            Decimal sum_interest_bfintarr = 0;
            Decimal interest_payamt = 0;
            Decimal itempayamt = 0;
            Decimal principalpayamt = 0;
            Decimal dwmain_bfshrcontbalamt = dw_main.GetItemDecimal(1, "bfshrcont_balamt");

            //ยอดโอนชำระ
            Decimal payoutclramt = 0;

            int EtcAllRow = DwOperateEtc.RowCount;
            for (int x = 1; x <= EtcAllRow; x++)
            {

                String etcFlag = DwOperateEtc.GetItemString(x, "operate_flag");
                if (etcFlag == "1")
                {
                    Decimal item_payamt = DwOperateEtc.GetItemDecimal(x, "item_payamt");

                    if (item_payamt != 0)
                    {
                        totalamt = totalamt + item_payamt;

                    }

                }

            }

            for (int i = 1; i <= loanAllRow; i++)
            {
                try { interest_period = DwOperateLoan.GetItemDecimal(i, "interest_period"); }
                catch { interest_period = 0; }
                try { bfintarr_amt = DwOperateLoan.GetItemDecimal(i, "bfintarr_amt"); }
                catch { bfintarr_amt = 0; }
                sum_interest_bfintarr = interest_period + bfintarr_amt;
                try { interest_payamt = DwOperateLoan.GetItemDecimal(i, "interest_payamt"); }
                catch { interest_payamt = 0; }
                try { itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt"); }
                catch { itempayamt = 0; }

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
                    try { principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt"); }
                    catch { principalpayamt = 0; }
                    DwOperateLoan.SetItemDecimal(i, "item_balance", dwloan_bfshrcontbalamt - principalpayamt);

                    //หายอดรวมที่ต้องชำระ

                    try { itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt"); }
                    catch { itempayamt = 0; }
                    //totalamt = totalamt + itempayamt;
                    //payoutclramt = payoutclramt + principalpayamt + interest_payamt;
                }
            }
            totalamt = totalamt + itempayamt;
            payoutclramt = payoutclramt + principalpayamt + interest_payamt + totalamt;
            //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดโอนชำระ payoutnet_amt
            dw_main.SetItemDecimal(1, "payoutclr_amt", payoutclramt);

            //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดหุ้นชำระ payoutnet_amt
            dw_main.SetItemDecimal(1, "payoutnet_amt", dwmain_bfshrcontbalamt - payoutclramt);

        }
    }
}
