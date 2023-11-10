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
using DataLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_sl_loan_receive : PageWebSheet, WebSheet
    {

        private n_shrlonClient slService;
        private n_commonClient commonSrv;
        private DwThDate tDwMain;
        protected String newClear;
        protected String saveSlipLnRcv;
        protected String initLnRcvlist;
        protected String initDataWindow;
        protected String initLnRcvReCalInt;
        protected String calculateitempayamt;
        protected String fittermoneytype;
        protected String jsRefresh;
        protected ArrayList dwList;
        protected String setPayoutclrAmt;
        protected String[] arrValue = new String[3];
        String loanContract_No;
        String as_initfrom = "", as_reqcontno = "", as_xmllnrcv = "";
        protected String GetNewLoan;


        public void InitJsPostBack()
        {
            saveSlipLnRcv = WebUtil.JsPostBack(this, "saveSlipLnRcv");
            initDataWindow = WebUtil.JsPostBack(this, "initDataWindow");
            initLnRcvReCalInt = WebUtil.JsPostBack(this, "initLnRcvReCalInt");
            initLnRcvlist = WebUtil.JsPostBack(this, "initLnRcvlist");
            newClear = WebUtil.JsPostBack(this, "newClear");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            GetNewLoan = WebUtil.JsPostBack(this, "GetNewLoan");
            setPayoutclrAmt = WebUtil.JsPostBack(this, "setPayoutclrAmt");
            calculateitempayamt = WebUtil.JsPostBack(this, "calculateitempayamt");
            fittermoneytype = WebUtil.JsPostBack(this, "fittermoneytype");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("operate_date", "operate_tdate");

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveSlipLnRcv")
            {
                this.SaveSlipLnRcv();
            }
            if (eventArg == "initLnRcvReCalInt")
            {
                this.InitLnRcvReCalInt();
            }
            if (eventArg == "initDataWindow")
            {
                this.InitDataWindow();
            }
            if (eventArg == "fieldProperty")
            {

            }
            if (eventArg == "jsRefresh")
            {
                Refresh();
            }
            if (eventArg == "GetNewLoan")
            {
                JsGetNewLoan();
            }
            if (eventArg == "calculateitempayamt")
            {
                Calculateitempayamt();
            }
            if (eventArg == "initLnRcvlist")
            {

                InitLnRcvlist();
            }
            if (eventArg == "newClear")
            {
                NewClear();

            }
            if (eventArg == "fittermoneytype")
            {
                Fittermoneytype();
               

            }

            if (eventArg == "setPayoutclrAmt") { SetPayoutclrAmt(); }
        }

        private void Fittermoneytype()
        {
            String moneytype_code = Hfmoneytype_code.Value;
            DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", moneytype_code);
            if (moneytype_code == "CBT")
            {
                DwMain.SetItemString(1, "tofrom_accid", "111510");
            }
            else if (moneytype_code == "MOO" || moneytype_code == "MOS" || moneytype_code == "DRF" || moneytype_code == "BEX" || moneytype_code == "CSC" || moneytype_code == "MON")
            {
                DwMain.Modify("tofrom_accid_1.visible =0");
                DwMain.Modify("tofrom_accid.visible =0");
                DwMain.Modify("tofrom_accid_t.visible =0");
            }
            else if (moneytype_code == "CSH")
            {
                DwMain.SetItemString(1, "tofrom_accid", "111101");

            }
            else if (moneytype_code == "CHQ")
            {
                DwMain.SetItemString(1, "tofrom_accid", "111230");

            }
            else if (moneytype_code == "TRN")
            {
                DwMain.SetItemString(1, "tofrom_accid", "115110");

            }
            else if (moneytype_code == "TBK")
            {
                DwMain.SetItemString(1, "tofrom_accid", "219101");

            }
        }

        private void SetPayoutclrAmt()
        {
            Decimal bfshrcont_balamt = 0;
            Decimal principal_payamt = 0;
            Decimal interest_payamt = 0;
            Decimal payoutclr_amt = 0;
            Decimal item_payamt = 0;
            Decimal operate_flag;
            
            for (int i = 1; i <= DwOperateLoan.RowCount; i++)
            {

                operate_flag = DwOperateLoan.GetItemDecimal(i, "operate_flag");

                //Edit by Bank สำหรับไม่คิดดอกเบี้ยหลังเรียกเก็บ
                Decimal rkeepprnc = DwOperateLoan.GetItemDecimal(i, "rkeep_principal");
                Decimal rkeepint = DwOperateLoan.GetItemDecimal(i, "rkeep_interest");
                Decimal itempay = DwOperateLoan.GetItemDecimal(i, "item_payamt");
                bfshrcont_balamt = DwOperateLoan.GetItemDecimal(i, "bfshrcont_balamt");
                if ((operate_flag == 1))
                {
                    if ((rkeepprnc > 0) || (rkeepint > 0))
                    {
                        //bfshrcont_balamt = bfshrcont_balamt ;
                        //itempay = itempay - rkeepint - rkeepprnc;
                        interest_payamt = 0;
                        DwOperateLoan.SetItemDecimal(i, "interest_payamt",0);
                        //DwOperateLoan.SetItemDecimal(i, "bfshrcont_balamt", bfshrcont_balamt);
                        //DwOperateLoan.SetItemDecimal(i, "interest_period", 0);
                        //DwOperateLoan.SetItemDecimal(i, "principal_payamt", bfshrcont_balamt);
                        //DwOperateLoan.SetItemDecimal(i, "interest_payamt", 0);
                        //DwOperateLoan.SetItemDecimal(i, "item_payamt", bfshrcont_balamt);
                        //DwOperateLoan.SetItemDecimal(i, "item_balance", 0);
                        //DwOperateLoan.SetItemDecimal(i, "item_payamt", bfshrcont_balamt + interest_payamt);
                        //payoutclr_amt += (bfshrcont_balamt + interest_payamt);
                    }
                   
                        bfshrcont_balamt = DwOperateLoan.GetItemDecimal(i, "bfshrcont_balamt");
                        principal_payamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt");
                        interest_payamt = DwOperateLoan.GetItemDecimal(i, "interest_payamt");
                        DwOperateLoan.SetItemDecimal(i, "item_balance", bfshrcont_balamt - principal_payamt);
                        DwOperateLoan.SetItemDecimal(i, "item_payamt", principal_payamt + interest_payamt);
                        payoutclr_amt += (principal_payamt + interest_payamt);
                    

                }

                // end bank edit

            }

            for (int j = 1; j <= DwOperateEtc.RowCount; j++)
            {
                item_payamt += DwOperateEtc.GetItemDecimal(j, "item_payamt");

            }
            Decimal payout_amt;
            try
            {
                payout_amt = DwMain.GetItemDecimal(1, "payout_amt");
            }
            catch (Exception ex)
            {
                payout_amt = 0;
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            DwMain.SetItemDecimal(1, "payoutclr_amt", payoutclr_amt + item_payamt);
            DwMain.SetItemDecimal(1, "payoutnet_amt", payout_amt - (payoutclr_amt + item_payamt));
        }

        private void NewClear()
        {
            DwMain.Reset();
            dw_list.Reset();
            DwOperateEtc.Reset();
            DwOperateLoan.Reset();
            DwMain.InsertRow(0);
            DwOperateEtc.InsertRow(0);
            DwOperateLoan.InsertRow(0);
            DwUtil.RetrieveDDDW(DwMain, "shrlontype_code_1", "sl_slipall.pbl", null);
            DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
            DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_slipall.pbl", null);
            // DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", null);
            CheckBox1.Checked = false;
            dw_list.Visible = false;
            TextBox1.Text = "";
        }

        public void WebSheetLoadBegin()
        {
            slService = wcf.NShrlon;
            commonSrv = wcf.NCommon;
            this.ConnectSQLCA();
            Sta ta = new Sta(sqlca.ConnectionString);
            Hloancheck.Value = "true";
            if (!IsPostBack)
            {
                CheckBox1.Checked = true;
                DwMain.InsertRow(0);
                DwOperateEtc.InsertRow(0);
                DwOperateLoan.InsertRow(0);
                DwUtil.RetrieveDDDW(DwMain, "shrlontype_code_1", "sl_slipall.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
                DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_slipall.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
                String arg;
                try
                {
                    arg = Hfmoneytype_code.Value;
                    if (arg == "") { arg = "CSH"; }
                }
                catch { arg = "CSH"; }
                DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", arg);

                dw_list.Visible = false;

                String sql = @"  SELECT  FINCONSTANT.CONFIRMCSHLNRCV_FLAG  FROM FINCONSTANT    ";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    Int32 CONFIRMCSHLNRCV_FLAG = Convert.ToInt32(dt.GetString("CONFIRMCSHLNRCV_FLAG"));
                    if (CONFIRMCSHLNRCV_FLAG == 0)
                    {
                        try
                        {
                            String confirmcash_protect = DwMain.Describe("confirmcash_amt.Protect");
                            DwMain.Modify("confirmcash_amt.Protect=1");
                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                    }
                }

            }
            else
            {
                String sql = @"  SELECT  FINCONSTANT.CONFIRMCSHLNRCV_FLAG  FROM FINCONSTANT    ";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    Int32 CONFIRMCSHLNRCV_FLAG = Convert.ToInt32(dt.GetString("CONFIRMCSHLNRCV_FLAG"));
                    if (CONFIRMCSHLNRCV_FLAG == 0)
                    {
                        try
                        {
                            String confirmcash_protect = DwMain.Describe("confirmcash_amt.Protect");
                            DwMain.Modify("confirmcash_amt.Protect=1");
                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                    }
                }
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
            }

        }

        public void SaveWebSheet()
        {
            SaveSlipLnRcv();
        }

        public void WebSheetLoadEnd()
        {
            //Retrive DDDW
            try
            {
                //Clear ค่าใน DDDW
                //DwMain.SetItemString(1, "tofrom_accid_1", "");
                ////แสดงค่าใน tofrom_accid_1
                //DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", null);
                //DataWindowChild dc = DwMain.GetChild("tofrom_accid_1");
                String moneyType = DwUtil.GetString(DwMain, 1, "moneytype_code_1", "");
                //ตรวจสอบค่าว่าง
                //if (!string.IsNullOrEmpty(moneyType))
                //{
                //    // กำหนด filter
                //    dc.SetFilter("moneytype_code = '" + moneyType + "'");
                //    dc.Filter();
                //}
                //แสดงข้อมูลเฉพาะ ของ โอนธนาคาร และเช็คธนาคาร
                if ((moneyType == "CBT") || (moneyType == "CHQ"))
                {

                    DwMain.Modify("expense_bank_t.visible =1");
                    DwMain.Modify("expense_bank.visible =1");
                    DwMain.Modify("expense_branch_t.visible =1");
                    DwMain.Modify("expense_branch.visible =1");
                    DwMain.Modify("expense_accid_t.visible =1");
                    DwMain.Modify("expense_accid.visible =1");
                }
                else
                {
                    //ไม่แสดงข้อมูล
                    DwMain.Modify("expense_bank_t.visible =0");
                    DwMain.Modify("expense_bank.visible =0");
                    DwMain.Modify("expense_branch_t.visible =0");
                    DwMain.Modify("expense_branch.visible =0");
                    DwMain.Modify("expense_accid_t.visible =0");
                    DwMain.Modify("expense_accid.visible =0");

                    // เฉพาะเงินสดกำหนดค่าให้ได้เลย
                    //if (moneyType == "CSH")
                    //{
                    //    DwMain.SetItemString(1, "tofrom_accid_1", "11100001");
                    //}
                }
                DwUtil.RetrieveDDDW(DwMain, "shrlontype_code_1", "sl_slipall.pbl", null);
                DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_slipall.pbl", null);
            }
            catch { }
          //  DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_slipall.pbl", null);
          //  String arg;
          //  try
          //  {
          //      arg = Hfmoneytype_code.Value;
          //      if (arg == "") { arg = "CSH"; }
          //  }
          //  catch { arg = "CSH"; }
          //DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_slipall.pbl", arg);
            DwMain.SaveDataCache();
            DwOperateEtc.SaveDataCache();
            DwOperateLoan.SaveDataCache();
            dw_list.SaveDataCache();
        }

        private void InitLnRcvlist()
        {
            str_slippayout sSlipPayOut = new str_slippayout();
            string as_coopid = state.SsCoopId;
            sSlipPayOut.entry_id = state.SsUsername;
            sSlipPayOut.operate_date = state.SsWorkDate;
            sSlipPayOut.loancontract_no = Hfloancontract_no.Value;
            sSlipPayOut.member_no = HfMemberNo.Value;
           DateTime adtm_paydate = state.SsWorkDate;
            string as_montype = Hflnrcvfrom_code.Value;
            String initfrom_type = sSlipPayOut.initfrom_type;
         //   of_initlist_lnrcvfin(String wsPass, String as_montype, DateTime adtm_paydate)
            string re = slService.of_initlist_lnrcv(state.SsWsPass,as_coopid);
            DwMain.Reset();
            DwMain.ImportString(re, Sybase.DataWindow.FileSaveAsType.Xml);
            // DwUtil.ImportData(sSlipPayOut.xml_sliphead,DwMain, tDwMain);
            DwUtil.DeleteLastRow(DwMain);
            tDwMain.Eng2ThaiAllRow();
            //DwMain.SetItemString(1, "moneytype_code", "CSH");



            try
            {
                DwOperateLoan.Reset();
                DwOperateLoan.ImportString(sSlipPayOut.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);
                // Decimal payout_amt = DwMain.GetItemDecimal(1, "payout_amt");

            }
            catch (Exception ex) { String ext = ex.ToString(); }
            try
            {
                DwOperateEtc.Reset();
                DwOperateEtc.ImportString(sSlipPayOut.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
            }
            catch (Exception ex) { String ext = ex.ToString(); }

            dw_list.Visible = true;
            dw_list.Reset();
            as_xmllnrcv = TextBox2.Text;
            dw_list.ImportString(as_xmllnrcv, Sybase.DataWindow.FileSaveAsType.Xml);
            if (initfrom_type == "REQ")
            {
                //Button1.Visible = true;
                //TextBox1.Visible = true;
                //String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
                //String loan_no = slService.GetNextContractNo(state.SsWsPass, shrlontype_code);
                //TextBox1.Text = loan_no;
            }
            else
            {
                Button1.Visible = false;
                TextBox1.Visible = false;
            }
            // HfFormtype.Value = as_initfrom;
        }

        private void Calculateitempayamt()
        {
            int loanAllRow = DwOperateLoan.RowCount;

            Decimal totalamt = 0;

            Decimal payout_amt;
            try
            {
                payout_amt = DwMain.GetItemDecimal(1, "payout_amt");
            }
            catch (Exception ex)
            {
                payout_amt = 0;
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

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

                    //itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");
                    //totalamt += itempayamt;
                    //payoutclramt += principalpayamt + interest_payamt;
                }
                //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดโอนชำระ payoutnet_amt
                SetPayoutclrAmt();
                //Decimal payoutclr_amt = DwMain.GetItemDecimal(1, "payoutclr_amt");
                //Decimal payoutnet_amt = payout_amt - payoutclr_amt;

                ////Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดหุ้นชำระ payoutnet_amt
                //DwMain.SetItemDecimal(1, "payoutnet_amt",payoutnet_amt );
            }
        }

        private void JsGetNewLoan()
        {
            //String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
            //String loan_no = slService.GenReqDocNo(state.SsWsPass, shrlontype_code);
            //DwMain.SetItemString(1, "loancontract_no", loan_no);
        }

        private void InitDataWindow()
        {

            String as_memno = HfMemberNo.Value;
            String initfrom="", as_xmlordlist="", as_coopid = state.SsCoopId,reqcoopid="",reqcontno = "";

            int result = slService.of_getmemblnrcv(state.SsWsPass, as_coopid, as_memno, ref  initfrom,  ref reqcoopid,ref reqcontno, ref  as_xmlordlist);
            if (result == 1)
            {
                dw_list.Visible = false;
                str_slippayout sSlipPayOut = new str_slippayout();
                sSlipPayOut.coop_id = state.SsCoopId;
                sSlipPayOut.contcoop_id = state.SsCoopControl;
                sSlipPayOut.payoutslip_no = reqcoopid;
                sSlipPayOut.operate_date = state.SsWorkDate;
                sSlipPayOut.loancontract_no = as_reqcontno;
                sSlipPayOut.member_no = as_memno;
                sSlipPayOut.slip_date = state.SsWorkDate;

                try
                {
                    int resultfin = wcf.NShrlon.of_initlnrcv(state.SsWsPass, ref sSlipPayOut);
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                DwMain.Reset();
                DwUtil.ImportData(sSlipPayOut.xml_sliphead, DwMain, null,Sybase.DataWindow.FileSaveAsType.Xml);
              //  DwMain.ImportString(sSlipPayOut.xml_sliphead, Sybase.DataWindow.FileSaveAsType.Xml);

                DwUtil.DeleteLastRow(DwMain);
                tDwMain.Eng2ThaiAllRow();

                try
                {
                    DwOperateLoan.Reset();
                    DwUtil.ImportData(sSlipPayOut.xml_slipcutlon, DwOperateLoan, null, Sybase.DataWindow.FileSaveAsType.Xml);
                   // DwOperateLoan.ImportString(sSlipPayOut.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);

                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                try
                {
                    DwOperateEtc.Reset();
                    DwUtil.ImportData(sSlipPayOut.xml_slipcutetc, DwOperateEtc, null, Sybase.DataWindow.FileSaveAsType.Xml);
                   // DwOperateEtc.ImportString(sSlipPayOut.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
                    DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_slipall.pbl", null);
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                InitLnRcvReCalInt();
            }
            else if (result == 2)
            {
                dw_list.Visible = true;
                dw_list.Reset();
                TextBox2.Text = as_xmllnrcv;
                dw_list.ImportString(as_xmllnrcv, Sybase.DataWindow.FileSaveAsType.Xml);
            }
            //Button1.Visible = true;
            //TextBox1.Visible = true;
            //String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
            //String loan_no = slService.GetNextContractNo(state.SsWsPass, shrlontype_code);
            //TextBox1.Text = loan_no;
            //if (HfFormtype.Value == "REQ")
            //{
            //    Button1.Visible = true;
            //    TextBox1.Visible = true;
            //    String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
            //    String loan_no = slService.GetNextContractNo(state.SsWsPass, shrlontype_code);
            //    TextBox1.Text = loan_no;
            //}
            //else
            //{
            //    Button1.Visible = false;
            //    TextBox1.Visible = false;
            //}
            DateTime operate_date = DwMain.GetItemDateTime(1, "operate_date");
            DateTime today = state.SsWorkDate;
            if (today > operate_date)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("วันที่จ่ายเงินกู้น้อยกว่าวันปัจจุบัน");
            }
        }

        private void InitLnRcvReCalInt()
        {
            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.entry_id = state.SsUsername;
            strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
            try { strPayOut.loancontract_no = DwMain.GetItemString(1, "loancontract_no"); }
            catch { strPayOut.loancontract_no = loanContract_No; }

            strPayOut.member_no = DwMain.GetItemString(1, "member_no"); ;
            strPayOut.slip_date = state.SsWorkDate;
            strPayOut.initfrom_type = HfFormtype.Value;
            String dwMainXML = "";
            String dwLoanXML = "";
            String dwEtcXML = "";

            dwMainXML = DwMain.Describe("DataWindow.Data.XML");
            dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML");
            try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
            catch { dwEtcXML = ""; }

            strPayOut.xml_sliphead = dwMainXML;
            strPayOut.xml_slipcutlon = dwLoanXML;
            strPayOut.xml_slipcutetc = dwEtcXML;


            slService.of_initlnrcv_recalint(state.SsWsPass, ref strPayOut);

            DwMain.Reset();
            DwUtil.ImportData(strPayOut.xml_sliphead, DwMain, null, Sybase.DataWindow.FileSaveAsType.Xml);
            //  DwMain.ImportString(sSlipPayOut.xml_sliphead, Sybase.DataWindow.FileSaveAsType.Xml);

            DwUtil.DeleteLastRow(DwMain);
            tDwMain.Eng2ThaiAllRow();

            try
            {
                DwOperateLoan.Reset();
                DwUtil.ImportData(strPayOut.xml_slipcutlon, DwOperateLoan, null, Sybase.DataWindow.FileSaveAsType.Xml);
                // DwOperateLoan.ImportString(sSlipPayOut.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);
                SetPayoutclrAmt();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            try
            {
                DwOperateEtc.Reset();
                DwUtil.ImportData(strPayOut.xml_slipcutetc, DwOperateEtc, null, Sybase.DataWindow.FileSaveAsType.Xml);
                // DwOperateEtc.ImportString(sSlipPayOut.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
                DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_slipall.pbl", null);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void SaveSlipLnRcv()
        {
            DateTime operate_date = DwMain.GetItemDateTime(1, "operate_date");
            DateTime today = state.SsWorkDate;
            if (today > operate_date)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("วันที่จ่ายเงินกู้น้อยกว่าวันปัจจุบัน");
            }
            else
            {
                String memno = DwMain.GetItemString(1, "member_no");
                //int index = Convert.ToInt32(HfIndex.Value);
                //int allIndex = Convert.ToInt32(HfAllIndex.Value);
                Sta ta = new Sta(sqlca.ConnectionString);

                String sql = @"  SELECT   
                                         FINCONSTANT.CONFIRMCSHLNRCV_FLAG  
                                    FROM FINCONSTANT    ";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    Int32 CONFIRMCSHLNRCV_FLAG = Convert.ToInt32(dt.GetString("CONFIRMCSHLNRCV_FLAG"));
                    if (CONFIRMCSHLNRCV_FLAG == 1)
                    {
                        Decimal payoutnet_amt, confirmcash_amt;
                        payoutnet_amt = DwMain.GetItemDecimal(1, "payoutnet_amt");

                        confirmcash_amt = DwMain.GetItemDecimal(1, "confirmcash_amt");
                        if (confirmcash_amt != payoutnet_amt)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ยอดเงินสดที่จ่ายไม่ตรงกับข้อมูลการจ่ายเงินกู้");
                        }
                    }
                    else
                    {
                        //str_slippayout strPayOut = new str_slippayout();
                        //strPayOut.coop_id = state.SsCoopId;
                        //strPayOut.entry_id = state.SsUsername;
                        //strPayOut.contcoop_id = state.SsCoopControl;
                        //strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
                        //try
                        //{
                        //    strPayOut.loancontract_no = DwMain.GetItemString(1, "loancontract_no");
                        //}
                        //catch { strPayOut.loancontract_no = ""; }
                        //strPayOut.member_no = memno;
                        //strPayOut.slip_date = state.SsWorkDate;
                        //strPayOut.initfrom_type = HfFormtype.Value;
                        str_slippayout astr_lnslip = new str_slippayout();
                        string as_concoopid = state.SsCoopControl;
                        string as_slipcoopid =state.SsCoopId;
                        string as_slipno = DwMain.GetItemString(1, "payoutslip_no");
                        String dwMainXML = "";
                        String dwLoanXML = "";
                        String dwEtcXML = "";

                        dwMainXML = DwMain.Describe("DataWindow.Data.XML");
                        try { dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML"); }
                        catch { dwLoanXML = ""; }
                        try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
                        catch { dwEtcXML = ""; }

                        //strPayOut.xml_sliphead = dwMainXML;
                        //strPayOut.xml_slipcutlon = dwLoanXML;
                        //strPayOut.xml_slipcutetc = dwEtcXML;

                        try
                        {
                            int result = slService.of_saveslip_lnrcv(state.SsWsPass,ref astr_lnslip);

                            if (result == 1)
                            {



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
                                    //string payinslip_no = strslip.payinslip_no;
                                    string payinslip_no = as_slipno;
                                    if (xmlconfig.LnReceivePrintMode == 0)
                                    {
                                        try
                                        {
                                           
                                            //string re = slService.of_printreceiptloan(state.SsWsPass, payinslip_no, fromset);
                                        }
                                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }
                                    }
                                    else
                                    {
                                        Printing.ShrlonPrintSlipPayIn(this, state.SsCoopId, payinslip_no, xmlconfig.LnReceivePrintMode);
                                    }
                                }
                            }
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                            NewClear();
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                            // Response.Write("<script>alert('ไม่สามารถบันทึกเลขทะเบียน " + memno + " ได้');</script>"); 
                        }


                    }

                }
            }
        }

        private void Refresh()
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
            //String loan_no = slService.GetNextContractNo(state.SsWsPass, shrlontype_code);
            //TextBox1.Text = loan_no;
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }



    }
}
