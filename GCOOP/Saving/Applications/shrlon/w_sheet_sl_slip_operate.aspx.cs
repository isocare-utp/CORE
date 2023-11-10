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

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_slip_operate : PageWebSheet, WebSheet
    {
        private string pbl = "sl_slipall.pbl";
        private DwThDate tDwMain;
        private n_shrlonClient shrlonService;
        private String errorMsg = "";
        protected string jsrefresh;
        protected String initLnRcv = "";
        protected String initSlipCalInt = "";
        protected String getPeriodCont = "";
        protected String calculateAmt = "";
        protected String fittermoneytype;
        protected String calinterest;
        protected String directCalAmt;
        protected String changeMemCoop;
        private bool toFromAccRetrieveFlag = true;
        private decimal crossCoopFlag = 0;
        String xml_loan;

        private void CalculateAmt()
        {
            Decimal totalamt = 0;
            Decimal shrTotal = 0;

            int shareAllRow = DwShareRowCount();
            for (int i = 1; i <= shareAllRow; i++)
            {
                Decimal shareFlag = dw_share.GetItemDecimal(i, "operate_flag");
                if (shareFlag == 1)
                {
                    Decimal shrAmt = dw_share.GetItemDecimal(i, "item_payamt");
                    Decimal bfsharebalamt = dw_share.GetItemDecimal(i, "bfshrcont_balamt");
                    if (shrAmt != 0)
                    {
                        totalamt = totalamt + shrAmt;
                        shrTotal = bfsharebalamt + shrAmt;
                        dw_share.SetItemDecimal(i, "item_balance", shrTotal);
                        dw_share.SetItemDecimal(i, "operate_flag", shareFlag);
                    }
                }
            }
            int EtcAllRow = dw_etc.RowCount;
            for (int x = 1; x <= EtcAllRow; x++)
            {
                Decimal etcFlag = dw_etc.GetItemDecimal(x, "operate_flag");
                if (etcFlag == 1)
                {
                    Decimal item_payamt = dw_etc.GetItemDecimal(x, "item_payamt");
                    if (item_payamt != 0)
                    {
                        totalamt = totalamt + item_payamt;
                    }
                }
            }

            int loanAllRow = dw_loan.RowCount;
            try
            {
                for (int i = 1; i <= loanAllRow; i++)
                {
                    Decimal loanFlag = DwUtil.GetDec(dw_loan, i, "operate_flag", 0);
                    string loancontract_no = DwUtil.GetString(dw_loan, i, "loancontract_no", "");

                    if (loanFlag == 1)
                    {
                        decimal payspec = dw_loan.GetItemDecimal(i, "bfpayspec_method");
                        decimal princAmt = DwUtil.GetDec(dw_loan, i, "principal_payamt", 0);
                        decimal interAmt = DwUtil.GetDec(dw_loan, i, "interest_payamt", 0);
                        decimal lonAmt = princAmt + interAmt;
                        dw_loan.SetItemDecimal(i, "item_payamt", lonAmt);

                        decimal bfAmt = DwUtil.GetDec(dw_loan, i, "bfshrcont_balamt", 0);
                        dw_loan.SetItemDecimal(i, "item_balance", bfAmt - princAmt);
                        if (lonAmt != 0)
                        {
                            totalamt = totalamt + lonAmt;
                        }
                    }
                    else
                    {
                        decimal bfAmt = DwUtil.GetDec(dw_loan, i, "bfshrcont_balamt", 0);
                        dw_loan.SetItemDecimal(i, "item_balance", bfAmt);
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            dw_main.SetItemDecimal(1, "slip_amt", totalamt);
        }

        private void GetPeriodCont()
        {
            int rowSelected = Convert.ToInt32(HfRowSelected.Value);

            Decimal flags = 0;
            try
            {
                flags = Convert.ToDecimal(dw_loan.GetItemString(rowSelected, "periodcount_flag"));
            }
            catch
            {
                flags = 0;
            }

            decimal period = dw_loan.GetItemDecimal(rowSelected, "period");

            if (flags == 1)
            {

                dw_loan.SetItemDecimal(rowSelected, "period", period + 1);
            }
            else
            {
                dw_loan.SetItemDecimal(rowSelected, "period", period - 1);
            }

        }

        private void InitDataWindow()
        {
            dw_main.Reset();
            dw_etc.Reset();
            dw_loan.Reset();
            dw_share.Reset();
            dw_main.InsertRow(0);
         
            dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            dw_main.SetItemDateTime(1, "slip_date", state.SsWorkDate);
            dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            dw_main.SetItemString(1, "sliptype_code", "PX");
            dw_main.SetItemString(1, "entry_id", state.SsUsername);
            dw_main.SetItemDateTime(1, "entry_date", DateTime.Now);
            dw_main.SetItemString(1, "memcoop_id", state.SsCoopId);

            ////tDwMain.Eng2ThaiAllRow();
            ////DwUtil.RetrieveDDDW(dw_main, "sliptype_code", "sl_slipall.pbl", null);
            ////DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
            ////DwUtil.RetrieveDDDW(dw_etc, "slipitemtype_code", "sl_slipall.pbl", null);
            //String arg;
            //try
            //{
            //    arg = Hfmoneytype_code.Value;
            //    if (arg == "") { arg = "CSH"; }
            //}
            //catch { arg = "CSH"; }
            //DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", "sl_slipall.pbl", arg);
            DwUtil.RetrieveDDDW(dw_main, "sliptype_code", "sl_slipall.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
            DwUtil.RetrieveDDDW(dw_etc, "slipitemtype_code", "sl_slipall.pbl", null);
        }

        private void InitLnRcv()
        {
            try
            {
                HfisCalInt.Value = "false";
                String ls_memno = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
                dw_main.SetItemString(1, "member_no", ls_memno);

                str_slippayin slip = new str_slippayin();
                slip.member_no = ls_memno;
                slip.slip_date = dw_main.GetItemDateTime(1, "slip_date");
                slip.operate_date = dw_main.GetItemDate(1, "operate_date");
                slip.sliptype_code = dw_main.GetItemString(1, "sliptype_code");
                slip.memcoop_id = dw_main.GetItemString(1, "memcoop_id");// state.SsCoopId;
                slip.coop_id = state.SsCoopId;
                slip.entry_id = state.SsUsername;

                int result = shrlonService.of_initslippayin(state.SsWsPass, ref slip);
                try
                {
                    DwUtil.ImportData(slip.xml_sliphead, dw_main, null, FileSaveAsType.Xml);
                    String mType = DwUtil.GetString(dw_main, 1, "moneytype_code", "");
                    if (mType == "")
                    {
                        dw_main.SetItemString(1, "moneytype_code", "CSH");
                    }
                    FitterMoneyType();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    ResetAllDw();
                    return;
                }
                try
                {
                    DwUtil.ImportData(slip.xml_slipshr, dw_share, null, FileSaveAsType.Xml);
                }
                catch { dw_share.Reset(); }
                try
                {
                    TextBox1.Text = slip.xml_sliplon;
                    DwUtil.ImportData(slip.xml_sliplon, dw_loan, null, FileSaveAsType.Xml);
                }
                catch { dw_loan.Reset(); }
                try
                {
                    DwUtil.ImportData(slip.xml_slipetc, dw_etc, null, FileSaveAsType.Xml);
                }
                catch { dw_etc.Reset(); }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                ResetAllDw();
            }
        }

        private void InitSlipCalInt()
        {
            try
            {
                DateTime dt = new DateTime();
                dt = dw_main.GetItemDateTime(1, "operate_date");
                String as_xmlloan = dw_loan.Describe("DataWindow.Data.XML");
                String as_sliptype = dw_main.GetItemString(1, "sliptype_code");
                Int32 result = shrlonService.of_initslippayin_calint(state.SsWsPass,ref as_xmlloan, as_sliptype, dt);
                //dw_loan.Reset();
                DwUtil.ImportData(as_xmlloan, dw_loan, null, FileSaveAsType.Xml);
                //dw_loan.ImportString(xmlloan, FileSaveAsType.Xml);
                CalculateAmt();
                HfisCalInt.Value = "true";
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }
        }

        private int DwLoanRowCount()
        {
            return dw_loan.RowCount;
        }

        private int DwShareRowCount()
        {
            return dw_share.RowCount;
        }

        private void FitterMoneyType()
        {
            try
            {
                dw_main.SetItemNull(1, "tofrom_accid");
                String moneytype_code = dw_main.GetItemString(1, "moneytype_code");
                DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", pbl, moneytype_code);
                toFromAccRetrieveFlag = false;
                DataWindowChild dc = dw_main.GetChild("tofrom_accid");
                String accId = dc.GetItemString(1, "account_id").Trim();
                dw_main.SetItemString(1, "tofrom_accid", accId);
            }
            catch { }
            //String moneytype_code = Hfmoneytype_code.Value;
            //DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", pbl, moneytype_code);

            //if (moneytype_code == "CBT")
            //{
            //    dw_main.SetItemString(1, "tofrom_accid", "111510");
            //}
            //else if (moneytype_code == "MOO" || moneytype_code == "MOS" || moneytype_code == "DRF" || moneytype_code == "BEX" || moneytype_code == "CSC" || moneytype_code == "MON")
            //{
            //    //  dw_main.Modify("tofrom_accid_1.visible =0");
            //    dw_main.Modify("tofrom_accid.visible =0");
            //    dw_main.Modify("tofrom_accid_t.visible =0");
            //}
            //else if (moneytype_code == "CSH")
            //{
            //    dw_main.SetItemString(1, "tofrom_accid", "111101");

            //}
            //else if (moneytype_code == "CHQ")
            //{
            //    dw_main.SetItemString(1, "tofrom_accid", "111551");

            //}
            //else if (moneytype_code == "TRN")
            //{
            //    dw_main.SetItemString(1, "tofrom_accid", "115110");

            //}
            //else if (moneytype_code == "TBK")
            //{
            //    dw_main.SetItemString(1, "tofrom_accid", "219101");
            //}
        }

        public void InitJsPostBack()
        {
            initLnRcv = WebUtil.JsPostBack(this, "initLnRcv");
            initSlipCalInt = WebUtil.JsPostBack(this, "initSlipCalInt");
            getPeriodCont = WebUtil.JsPostBack(this, "getPeriodCont");
            calculateAmt = WebUtil.JsPostBack(this, "calculateAmt");
            fittermoneytype = WebUtil.JsPostBack(this, "fittermoneytype");
            jsrefresh = WebUtil.JsPostBack(this, "jsrefresh");
            calinterest = WebUtil.JsPostBack(this, "calinterest");
            directCalAmt = WebUtil.JsPostBack(this, "directCalAmt");
            changeMemCoop = WebUtil.JsPostBack(this, "changeMemCoop");
            //----------------------------------------------------------
            tDwMain = new DwThDate(dw_main, this);
            tDwMain.Add("slip_date", "slip_tdate");
            tDwMain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            if (!IsPostBack)
            {
                dw_main.InsertRow(0);
                dw_main.SetItemString(1, "coop_id", state.SsCoopId);
                dw_main.SetItemDateTime(1, "slip_date", state.SsWorkDate);
                dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                dw_main.SetItemString(1, "sliptype_code", "PX");
                dw_main.SetItemString(1, "entry_id", state.SsUsername);
                dw_main.SetItemDateTime(1, "entry_date", DateTime.Now);
                dw_main.SetItemString(1, "memcoop_id", state.SsCoopId);
            }
            else
            {
                this.RestoreContextDw(dw_main, tDwMain);
                this.RestoreContextDw(dw_share);
                this.RestoreContextDw(dw_loan);
                this.RestoreContextDw(dw_etc);
                crossCoopFlag = dw_main.GetItemDecimal(1, "crosscoop_flag");
                if (crossCoopFlag == 0)
                {
                    string memCoop = dw_main.GetItemString(1, "memcoop_id");
                    dw_main.SetItemString(1, "memcoop_no", state.SsCoopId);
                    if (memCoop != state.SsCoopId)
                    {
                        ResetAllDw();
                    }
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "initLnRcv")
            {
                this.InitLnRcv();
                dw_main.SetItemString(1, "entry_id", state.SsUsername);
                dw_main.SetItemDate(1, "entry_date", DateTime.Now);
            }
            else if (eventArg == "initSlipCalInt")
            {
                this.InitSlipCalInt();
                dw_main.SetItemString(1, "entry_id", state.SsUsername);
                dw_main.SetItemDate(1, "entry_date", DateTime.Now);
            }
            else if (eventArg == "getPeriodCont")
            {
                GetPeriodCont();
            }
            else if (eventArg == "calculateAmt")
            {
                this.InitSlipCalInt();
               // HdIsPostBack.Value = "false";
            }
            else if (eventArg == "fittermoneytype")
            {
                FitterMoneyType();
            }
            else if (eventArg == "jsrefresh")
            {
            }
            else if (eventArg == "directCalAmt")
            {
                CalculateAmt();
              //  HdIsPostBack.Value = "false";
            }
            else if (eventArg == "changeMemCoop")
            {
                ResetAllDw();
            }
            else if (eventArg == "calinterest")
            {
                Decimal totalamt = 0;
                Decimal shrTotal = 0;
                Decimal lonTotal = 0;
                Decimal lonPrincipal = 0;

                int rowSelected = 0;
                try
                {
                    rowSelected = Convert.ToInt32(HfRowSelected.Value);
                }
                catch { }

                int EtcAllRow = dw_etc.RowCount;
                for (int x = 1; x <= EtcAllRow; x++)
                {

                    Decimal etcFlag = dw_etc.GetItemDecimal(x, "operate_flag");
                    if (etcFlag == 1)
                    {
                        Decimal item_payamt = dw_etc.GetItemDecimal(x, "item_payamt");

                        if (item_payamt != 0)
                        {
                            totalamt = totalamt + item_payamt;

                        }
                    }
                }

                int shareAllRow = dw_share.RowCount;
                for (int i = 1; i <= shareAllRow; i++)
                {
                    Decimal shrAmt = dw_share.GetItemDecimal(i, "item_payamt");
                    Decimal bfsharebalamt = dw_share.GetItemDecimal(i, "bfshrcont_balamt");
                    Decimal shareFlag = dw_share.GetItemDecimal(i, "operate_flag");
                    if (shareFlag == 1)
                    {
                        if (shrAmt != 0)
                        {
                            totalamt = totalamt + shrAmt;
                            shrTotal = bfsharebalamt + shrAmt;
                            dw_share.SetItemDecimal(i, "item_balance", shrTotal);
                            dw_share.SetItemDecimal(i, "operate_flag", 1);
                        }

                    }
                }
                int loanAllRow = dw_loan.RowCount;
                try
                {

                    for (int j = 1; j <= loanAllRow; j++)
                    {
                        Decimal loanFlag = dw_loan.GetItemDecimal(j, "operate_flag");
                        String loancontract_no;
                        Decimal intLoanpayamt, bfloanbalamt, lonPrnAmt, lonAmt;
                        try { loancontract_no = dw_loan.GetItemString(j, "loancontract_no"); }
                        catch { loancontract_no = ""; }
                        if (loanFlag == 1)
                        {
                            Decimal payspec = dw_loan.GetItemDecimal(j, "bfpayspec_method");
                            try { lonAmt = dw_loan.GetItemDecimal(j, "item_payamt"); }
                            catch { lonAmt = 0; }
                            try { lonPrnAmt = dw_loan.GetItemDecimal(j, "principal_payamt"); }
                            catch { lonPrnAmt = 0; }
                            try { bfloanbalamt = dw_loan.GetItemDecimal(j, "bfshrcont_balamt"); }
                            catch { bfloanbalamt = 0; }
                            try { intLoanpayamt = dw_loan.GetItemDecimal(j, "interest_payamt"); }
                            catch { intLoanpayamt = 0; }

                            if (lonAmt != 0)
                            {
                                totalamt = totalamt + lonPrnAmt + intLoanpayamt;
                                lonTotal = bfloanbalamt - lonPrnAmt - intLoanpayamt;
                                lonPrincipal = lonAmt - intLoanpayamt;
                                dw_loan.SetItemDecimal(j, "item_balance", lonTotal);
                                dw_loan.SetItemDecimal(j, "item_payamt", lonPrnAmt + intLoanpayamt);
                                dw_loan.SetItemDecimal(j, "interest_payamt", intLoanpayamt);
                            }
                        }
                        else if (loancontract_no == "")
                        {

                            try
                            {
                                dw_loan.Reset();
                                xml_loan = TextBox1.Text;
                                dw_loan.ImportString(xml_loan, FileSaveAsType.Xml);
                            }
                            catch { dw_loan.Reset(); }
                        }
                    }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }

                dw_main.SetItemDecimal(1, "slip_amt", totalamt);

            }
        }

        private void ResetAllDw()
        {
            String memCoop = dw_main.GetItemString(1, "memcoop_id");
            dw_main.Reset();
            dw_main.InsertRow(0);
            dw_main.SetItemString(1, "memcoop_id", memCoop);
            dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            dw_main.SetItemDateTime(1, "slip_date", state.SsWorkDate);
            dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            dw_main.SetItemString(1, "sliptype_code", "PX");
            dw_main.SetItemString(1, "entry_id", state.SsUsername);
            dw_main.SetItemDateTime(1, "entry_date", DateTime.Now);

            dw_loan.Reset();
            dw_share.Reset();
            dw_etc.Reset();
        }

        public void SaveWebSheet()
        {
            try
            {
                String xmlshr, xmllon, xmletc;
                string moneytype = dw_main.GetItemString(1, "moneytype_code");
                string memberNo = DwUtil.GetString(dw_main, 1, "member_no", "");
                if (memberNo == "") throw new Exception("กระณากรอกทะเบียนสมาชิก");
                if (memberNo.Length != WebUtil.MemberNoFormat("1").Length) throw new Exception("รูปแบบเลขทะเบียนสมาชิกไม่ถูกต้อง");

                string xmlhead = dw_main.Describe("DataWindow.Data.XML");
                if (dw_share.RowCount > 0) { xmlshr = dw_share.Describe("DataWindow.Data.XML"); }
                else { xmlshr = ""; }
                if (dw_loan.RowCount > 0) { xmllon = dw_loan.Describe("DataWindow.Data.XML"); }
                else { xmllon = ""; }
                if (dw_etc.RowCount > 0) { xmletc = dw_etc.Describe("DataWindow.Data.XML"); }
                else { xmletc = ""; }

                str_slippayin strslip = new str_slippayin();
                strslip.xml_sliphead = xmlhead;
                strslip.xml_slipshr = xmlshr;
                strslip.xml_sliplon = xmllon;
                strslip.xml_slipetc = xmletc;
                strslip.entry_id = state.SsUsername;
                strslip.coop_id = state.SsCoopId;

                int result = shrlonService.of_saveslip_payin(state.SsWsPass, ref strslip);
                string as_xml = "";
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    //dw_main.Reset();
                    //dw_share.Reset();
                    //dw_loan.Reset();
                    //dw_etc.Reset();
                    //if (moneytype == "CSH")
                    //{
                    string fromset = "";
                    try
                    {
                        fromset = state.SsPrinterSet;

                    }
                    catch (Exception ex)
                    {
                        fromset = "216";
                        //if (fromset == "")
                        //{
                        //    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกเครื่องปรื้นก่อน");
                        //}
                        //else { LtServerMessage.Text = WebUtil.ErrorMessage("of_printreceipt" + ex.ToString()); }
                    }
                    string payinslip_no = strslip.payinslip_no;
                    //string re = shrlonService.of_printslippayin(state.SsWsPass, payinslip_no, fromset, state.SsCoopId, memberNo, ref as_xml);
                    //}
                   InitDataWindow();
                }
                else
                {
                    errorMsg = " บันทึกไม่สำเร็จ<br>";
                }

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                String arg = dw_main.GetItemString(1, "moneytype_code").Trim();
                if (arg != "" && toFromAccRetrieveFlag)
                {
                    DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", pbl, arg);
                }
            }
            catch { }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "memcoop_id", pbl, state.SsCoopControl);
            }
            catch { }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "sliptype_code", pbl, null);
            }
            catch { }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "moneytype_code", pbl, null);
            }
            catch { }
            try
            {
                DwUtil.RetrieveDDDW(dw_etc, "slipitemtype_code", pbl, null);
            }
            catch { }
            try
            {
                tDwMain.Eng2ThaiAllRow();
            }
            catch { }
            try
            {
                dw_main.SetItemDecimal(1, "crosscoop_flag", crossCoopFlag);
            }
            catch { }
            dw_etc.SaveDataCache();
            dw_loan.SaveDataCache();
            dw_main.SaveDataCache();
            dw_share.SaveDataCache();
        }
    }
}
