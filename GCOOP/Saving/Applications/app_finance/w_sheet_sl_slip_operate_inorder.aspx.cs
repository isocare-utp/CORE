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
    public partial class w_sheet_sl_slip_operate_inorder : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        private String errorMsg = "";
        protected String initlist;
        protected String initLnRcv = "";
        protected String initSlipCalInt = "";
        protected String getPeriodCont = "";
        protected String calculateAmt = "";
        protected string jsrefresh;
        protected string calinterest;
        protected String fittermoneytype;
        string xml_loan;
        private void CalculateAmt()
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

                for (int j = 1; j <= loanAllRow; j++)
                {
                    Decimal loanFlag;
                    try
                    {
                        loanFlag = dw_loan.GetItemDecimal(j, "operate_flag");
                    }
                    catch { loanFlag = 0; }
                    String loancontract_no;
                    Decimal interest_payamt, bfloanbalamt, principal_payamt, item_payamt;
                    try { loancontract_no = dw_loan.GetItemString(j, "loancontract_no"); }
                    catch { loancontract_no = ""; }
                    if (loanFlag == 1)
                    {
                        Decimal payspec = dw_loan.GetItemDecimal(j, "bfpayspec_method");
                        try { item_payamt = dw_loan.GetItemDecimal(j, "item_payamt"); }
                        catch { item_payamt = 0; }
                        try { principal_payamt = dw_loan.GetItemDecimal(j, "principal_payamt"); }
                        catch { principal_payamt = 0; }
                        try { bfloanbalamt = dw_loan.GetItemDecimal(j, "bfshrcont_balamt"); }
                        catch { bfloanbalamt = 0; }
                        try { interest_payamt = dw_loan.GetItemDecimal(j, "interest_payamt"); }
                        catch { interest_payamt = 0; }
                        item_payamt = principal_payamt + interest_payamt;
                        if (item_payamt != 0)
                        {
                            totalamt = totalamt + item_payamt;
                            lonTotal = bfloanbalamt - principal_payamt;
                            lonPrincipal = item_payamt - interest_payamt;
                            dw_loan.SetItemDecimal(j, "item_balance", lonTotal);
                            dw_loan.SetItemDecimal(j, "item_payamt", item_payamt);
                            dw_loan.SetItemDecimal(j, "interest_payamt", interest_payamt);
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
            dw_main.InsertRow(0);

            dw_main.SetItemDate(1, "slip_date", state.SsWorkDate);
            dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
            dw_main.SetItemString(1, "sliptype_code", "PX");
            dw_main.SetItemString(1, "moneytype_code", "CSH");
            dw_main.SetItemString(1, "entry_id", state.SsUsername);
            dw_main.SetItemDate(1, "entry_date", DateTime.Now);
            dw_list.Reset();
            tDwMain.Eng2ThaiAllRow();

            String arg;
            try
            {
                arg = Hfmoneytype_code.Value;
                if (arg == "") { arg = "CSH"; }
            }
            catch { arg = "CSH"; }
            DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", "sl_slipall.pbl", arg);
            DwUtil.RetrieveDDDW(dw_main, "sliptype_code", "sl_slipall.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
            DwUtil.RetrieveDDDW(dw_etc, "slipitemtype_code", "sl_slipall.pbl", null);
        }

        private void InitLnRcv()
        {
            try
            {
                String member_no = dw_main.GetItemString(1, "member_no");
                String as_redno = "";
                String as_xmllnrcv = "";

                int result = shrlonService.of_getmembslippayin(state.SsWsPass, member_no, ref as_redno, ref as_xmllnrcv);
                if (result == 1)
                {
                    HfisCalInt.Value = "false";
                    str_slippayin astr_slippayin = new str_slippayin();
                    astr_slippayin.slip_date = state.SsWorkDate;
                    //   astr_slippayin.member_no = member_no;
                    astr_slippayin.payinorder_no = as_redno;
                    int resultXML = shrlonService.of_initslippayin(state.SsWsPass, ref astr_slippayin);
                    try
                    {
                        dw_main.Reset();
                        dw_main.ImportString(astr_slippayin.xml_sliphead, FileSaveAsType.Xml);
                        //   DwUtil.DeleteLastRow(dw_main);

                        String moneytype_code = dw_main.GetItemString(1, "moneytype_code"); 
                        dw_main.SetItemString(1, "moneytype_code", moneytype_code);
                        tDwMain.Eng2ThaiAllRow();
                        DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
                        DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", "sl_slipall.pbl", moneytype_code);
                       
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีเลขสมาชิก " + dw_main.GetItemString(1, "member_no"));
                        InitDataWindow();
                    }
                    try
                    {
                        dw_share.Reset();
                        dw_share.ImportString(astr_slippayin.xml_slipshr, FileSaveAsType.Xml);
                    }
                    catch { dw_share.Reset(); }
                    try
                    {
                        dw_loan.Reset();
                        TextBox1.Text = astr_slippayin.xml_sliplon;
                        dw_loan.ImportString(astr_slippayin.xml_sliplon, FileSaveAsType.Xml);
                    }
                    catch { dw_loan.Reset(); }
                    try
                    {
                        dw_etc.Reset();
                        dw_etc.ImportString(astr_slippayin.xml_slipetc, FileSaveAsType.Xml);
                    }
                    catch { dw_etc.Reset(); }
                }
                else if (result > 1)
                {
                    dw_list.Visible = true;
                    dw_list.Reset();
                    TextBox2.Text = as_xmllnrcv;
                    dw_list.ImportString(as_xmllnrcv, Sybase.DataWindow.FileSaveAsType.Xml);
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void InitSlipCalInt()
        {

            //try
            //{
            //    tDwMain.Eng2ThaiAllRow();
            //    DateTime dt = new DateTime();
            //    dt = dw_main.GetItemDateTime(1, "operate_date");
            //    String as_xmlloan = dw_loan.Describe("DataWindow.Data.XML");
            //    String as_sliptype = dw_main.GetItemString(1, "sliptype_code");
            //    int  result = shrlonService.of_initslippayin_calint(state.SsWsPass, ref as_xmlloan, as_sliptype, dt);
            //    dw_loan.Reset();
            //    dw_loan.ImportString(as_xmlloan, FileSaveAsType.Xml);
            //    CalculateAmt();
            //    HfisCalInt.Value = "true";
            //}
            //catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }
        }

        private void SaveSlipPayIn()
        {
            try
            {
                String xmlhead, xmlshr, xmllon, xmletc;
                String moneytype = dw_main.GetItemString(1, "moneytype_code");
                if (dw_main.RowCount > 0) { xmlhead = dw_main.Describe("DataWindow.Data.XML"); }
                else { xmlhead = ""; }
                if (dw_share.RowCount > 0) { xmlshr = dw_share.Describe("DataWindow.Data.XML"); }
                else { xmlshr = ""; }
                if (dw_loan.RowCount > 0) { xmllon = dw_loan.Describe("DataWindow.Data.XML"); }
                else { xmllon = ""; }
                if (dw_etc.RowCount > 0) { xmletc = dw_etc.Describe("DataWindow.Data.XML"); }
                else { xmletc = ""; }
                str_slippayin astr_slippayin = new str_slippayin();
                astr_slippayin.xml_slipetc = xmletc;
                astr_slippayin.xml_sliphead = xmlhead;
                astr_slippayin.xml_sliplon = xmllon;
                astr_slippayin.xml_slipshr = xmlshr;
                astr_slippayin.operate_date = state.SsWorkDate;
                astr_slippayin.entry_id = state.SsUsername;
                astr_slippayin.coop_id = state.SsCoopId;
                int result = shrlonService.of_saveslip_payin(state.SsWsPass, ref astr_slippayin);

                if (result == 1)
                {
                    
                    dw_main.Reset();
                    dw_share.Reset();
                    dw_loan.Reset();
                    dw_etc.Reset();


                    string fromset = "";
                    try
                    {
                        fromset = state.SsPrinterSet;

                    }
                    catch (Exception ex)
                    {
                        fromset = "216";

                    }



                    try
                    {

                        string payinslip_no = astr_slippayin.payinslip_no;
                        //string re = shrlonService.of_printreceiptorder(state.SsWsPass, payinslip_no, fromset);
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }

                    InitDataWindow();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
                else
                {
                    errorMsg = " บันทึกไม่สำเร็จ<br>";
                }

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage( ex); }
        }

        private int DwLoanRowCount()
        {
            return dw_loan.RowCount;
        }

        private int DwShareRowCount()
        {
            return dw_share.RowCount;
        }



        public void InitJsPostBack()
        {
            initLnRcv = WebUtil.JsPostBack(this, "initLnRcv");
            initSlipCalInt = WebUtil.JsPostBack(this, "initSlipCalInt");
            getPeriodCont = WebUtil.JsPostBack(this, "getPeriodCont");
            calculateAmt = WebUtil.JsPostBack(this, "calculateAmt");
            initlist = WebUtil.JsPostBack(this, "initlist");
            jsrefresh = WebUtil.JsPostBack(this, "jsrefresh");
            calinterest = WebUtil.JsPostBack(this, "calinterest");
            fittermoneytype = WebUtil.JsPostBack(this, "fittermoneytype");
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
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }

            //this.ConnectSQLCA();

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_share);
                this.RestoreContextDw(dw_loan);
                this.RestoreContextDw(dw_etc);
                HdIsPostBack.Value = "true";
            }
            if (dw_main.RowCount < 1)
            {
                InitDataWindow();
                HdIsPostBack.Value = "false";
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
                //CalculateAmt();
            }
            else if (eventArg == "initlist")
            {

                Initlist();
            }
            else if (eventArg == "fittermoneytype")
            {

                Fittermoneytype();
            }
            else if (eventArg == "jsrefresh") { }
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
                        Decimal interest_payamt, bfloanbalamt, principal_payamt, item_payamt;
                        try { loancontract_no = dw_loan.GetItemString(j, "loancontract_no"); }
                        catch { loancontract_no = ""; }
                        if (loanFlag == 1)
                        {
                            Decimal payspec = dw_loan.GetItemDecimal(j, "bfpayspec_method");
                            try { item_payamt = dw_loan.GetItemDecimal(j, "item_payamt"); }
                            catch { item_payamt = 0; }
                            try { principal_payamt = dw_loan.GetItemDecimal(j, "principal_payamt"); }
                            catch { principal_payamt = 0; }
                            try { bfloanbalamt = dw_loan.GetItemDecimal(j, "bfshrcont_balamt"); }
                            catch { bfloanbalamt = 0; }
                            try { interest_payamt = dw_loan.GetItemDecimal(j, "interest_payamt"); }
                            catch { interest_payamt = 0; }
                            item_payamt = principal_payamt + interest_payamt;
                            if (item_payamt != 0)
                            {
                                totalamt = totalamt + principal_payamt + interest_payamt;
                                lonTotal = bfloanbalamt - principal_payamt;
                                lonPrincipal = item_payamt - interest_payamt;
                                dw_loan.SetItemDecimal(j, "item_balance", lonTotal);
                                dw_loan.SetItemDecimal(j, "item_payamt", principal_payamt + interest_payamt);
                                dw_loan.SetItemDecimal(j, "interest_payamt", interest_payamt);
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
        private void Fittermoneytype()
        {
            String moneytype_code = Hfmoneytype_code.Value;
            DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", "sl_slipall.pbl", moneytype_code);
            if (moneytype_code == "CBT")
            {
                dw_main.SetItemString(1, "tofrom_accid", "111551");
            }
            else if (moneytype_code == "MOO" || moneytype_code == "MOS" || moneytype_code == "DRF" || moneytype_code == "BEX" || moneytype_code == "CSC" || moneytype_code == "MON")
            {
                //  dw_main.Modify("tofrom_accid_1.visible =0");
                dw_main.Modify("tofrom_accid.visible =0");
                dw_main.Modify("tofrom_accid_t.visible =0");
            }
            else if (moneytype_code == "CSH")
            {
                dw_main.SetItemString(1, "tofrom_accid", "111101");

            }
            else if (moneytype_code == "CHQ")
            {
                dw_main.SetItemString(1, "tofrom_accid", "111551");

            }
            else if (moneytype_code == "TRN")
            {
                dw_main.SetItemString(1, "tofrom_accid", "115110");

            }
            else if (moneytype_code == "TBK")
            {
                dw_main.SetItemString(1, "tofrom_accid", "219101");

            }
        }
        private void Initlist()
        {
            try
            {

                str_slippayin astr_initpayin = new str_slippayin();
                astr_initpayin.coop_id = state.SsCoopId;
                astr_initpayin.entry_id = state.SsUsername;
                astr_initpayin.operate_date = state.SsWorkDate;
                astr_initpayin.slip_date = state.SsWorkDate;

                HfisCalInt.Value = "false";


                //   astr_slippayin.member_no = member_no;
                astr_initpayin.payinorder_no = Hfpayinorder_no.Value;
                int resultXML = shrlonService.of_initslippayin(state.SsWsPass, ref astr_initpayin);
                try
                {
                    dw_main.Reset();
                    dw_main.ImportString(astr_initpayin.xml_sliphead, FileSaveAsType.Xml);
                    //   DwUtil.DeleteLastRow(dw_main);


                    tDwMain.Eng2ThaiAllRow();

                    DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
                    DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", "sl_slipall.pbl", null);
                    dw_main.SetItemString(1, "moneytype_code", "CSH");
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีเลขสมาชิก " + dw_main.GetItemString(1, "member_no"));
                    InitDataWindow();
                }
                try
                {
                    dw_share.Reset();
                    dw_share.ImportString(astr_initpayin.xml_slipshr, FileSaveAsType.Xml);
                }
                catch { dw_share.Reset(); }
                try
                {
                    dw_loan.Reset();
                    dw_loan.ImportString(astr_initpayin.xml_sliplon, FileSaveAsType.Xml);
                }
                catch { dw_loan.Reset(); }
                try
                {
                    dw_etc.Reset();
                    dw_etc.ImportString(astr_initpayin.xml_slipetc, FileSaveAsType.Xml);
                }
                catch { dw_etc.Reset(); }
                // }
                //else if (result == 2)
                //{
                //    dw_list.Visible = true;
                //    dw_list.Reset();
                //    TextBox2.Text = as_xmllnrcv;
                //    dw_list.ImportString(as_xmllnrcv, Sybase.DataWindow.FileSaveAsType.Xml);
                //}
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void SaveWebSheet()
        {
            SaveSlipPayIn();
        }

        public void WebSheetLoadEnd()
        {
            //DwUtil.RetrieveDDDW(dw_main, "sliptype_code", "sl_slipall.pbl", null);
            //DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slipall.pbl", null);
            //DwUtil.RetrieveDDDW(dw_etc, "slipitemtype_code", "sl_slipall.pbl", null);
            //DwUtil.RetrieveDDDW(dw_main, "tofrom_accid", "sl_slipall.pbl", null);
            dw_list.SaveDataCache();
            dw_etc.SaveDataCache();
            dw_loan.SaveDataCache();
            dw_main.SaveDataCache();
            dw_share.SaveDataCache();
        }


    }
}
