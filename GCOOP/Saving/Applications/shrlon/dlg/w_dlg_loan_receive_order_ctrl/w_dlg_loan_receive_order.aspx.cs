using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Globalization;

namespace Saving.Applications.shrlon.dlg.w_dlg_loan_receive_order_ctrl
{
    public partial class w_dlg_loan_receive_order : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostCancel { get; set; }
        [JsPostBack]
        public string PostSave { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostOperateFlag { get; set; }
        [JsPostBack]
        public string PostOperateFlagAdd { get; set; }
        [JsPostBack]
        public string PostBank { get; set; }
        [JsPostBack]
        public string PostRcvperiod { get; set; }
        [JsPostBack]
        public string PostItem { get; set; }
        [JsPostBack]
        public string PostSlipItem { get; set; }
        [JsPostBack]
        public string PostMoneytype { get; set; }
        [JsPostBack]
        public string PostCalBankFee { get; set; }
        [JsPostBack]
        public string PostSlip_date { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }
        [JsPostBack]
        public string PostPrint { get; set; }

        public string IsShow = "visible";
        string[] loans;//Request["loans"].Split(',');

        DateTime lastDate;
        int currentLoan = 0;
        DateTime operate_date;
        int fincash_status = 1;
        string exc = "";
        string alerts = "";
        string saveResult = "";
        decimal chkpayoutnet_amt = 0;
        decimal payavd_flag = 0;

        string slip_no = "";

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
            dsAdd.InitDsAdd(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                //เช็คปิดวัน
                try
                {
                    if (state.SsCloseDayStatus == 1)
                    {
                        try
                        {
                            DateTime adtm_nextworkdate = new DateTime();
                            int result = wcf.NCommon.of_getnextworkday(state.SsWsPass, state.SsWorkDate, ref adtm_nextworkdate);
                            if (result == 1)
                            {
                                LtServerMessage.Text = WebUtil.WarningMessage("ระบบได้ทำการปิดวันไปแล้ว เปลี่ยนวันทำการเป็น " + adtm_nextworkdate.ToString("dd/MM/yyyy"));
                                this.SetOnLoadedScript("alert('ระบบได้ทำการปิดวันไปแล้ว เปลี่ยนวันทำการเป็น " + adtm_nextworkdate.ToString("dd/MM/yyyy") + " ')");
                                //alerts += "alert('ระบบได้ทำการปิดวันไปแล้ว เปลี่ยนวันทำการเป็น " + adtm_nextworkdate + " ') \n ";
                                DateTime nextworkdate = adtm_nextworkdate;
                                operate_date = adtm_nextworkdate;
                                dsMain.DATA[0].OPERATE_DATE = nextworkdate;
                                dsMain.DATA[0].SLIP_DATE = nextworkdate;
                            }
                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                    }
                    else
                    {
                        operate_date = state.SsWorkDate;
                        dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                    }

                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                loans = Request["loans"].Split(',');


                try
                {
                    string sdate = Request["sdate"];
                    DateTime slip_date = DateTime.ParseExact(sdate, "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                    payavd_flag = Convert.ToDecimal(Request["pay_flag"]);
                    HdPayavd.Value = payavd_flag.ToString();
                    if (payavd_flag == 1)
                    {
                        dsMain.DATA[0].SLIP_DATE = slip_date;
                    }
                }
                catch { }
                // loans2 = Request["loans"].Split('@');

                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;

                InitLoan();
                //string sliptype_code = "PX";
                //string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                //dsMain.DdBankDesc();
                //dsMain.DdFromAccId(sliptype_code, moneytype_code);
                //SetDefaultTofromaccid();
                HdIndex.Value = currentLoan + "";
                dsMain.FindDropDownList(0, dsMain.DATA.LNPAYMENT_STATUSColumn).Enabled = false;


            }
            else
            {
                operate_date = dsMain.DATA[0].OPERATE_DATE;
                lastDate = dsMain.DATA[0].SLIP_DATE;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostOperateFlag)
            {
                int row = dsDetail.GetRowFocus();
                decimal operate_flag = dsDetail.DATA[row].OPERATE_FLAG;
                decimal bfshrcont_balamt = dsDetail.DATA[row].BFSHRCONT_BALAMT;
                decimal interest_period = dsDetail.DATA[row].INTEREST_PERIOD;
                decimal bfintarr_amt = dsDetail.DATA[row].BFINTARR_AMT;
                decimal principal_payamt = dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                decimal interest_payamt = dsDetail.DATA[row].INTEREST_PAYAMT;
                decimal item_payamt = dsDetail.DATA[row].ITEM_PAYAMT;
                decimal item_balance = dsDetail.DATA[row].ITEM_BALANCE;
                decimal inerest_bfintarr = dsDetail.DATA[row].INTERST_BFINTARR;
                decimal payout_amt = dsMain.DATA[0].PAYOUT_AMT;
                decimal payoutclr_amt = dsMain.DATA[0].PAYOUTCLR_AMT;
                decimal payoutnet_amt = dsMain.DATA[0].PAYOUTNET_AMT;

                if (operate_flag == 1)
                {
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = bfshrcont_balamt;
                    dsDetail.DATA[row].INTEREST_PAYAMT = interest_period + bfintarr_amt;
                    //dsDetail.DATA[row].BFINTARR_AMT = interest_period + bfintarr_amt;
                    dsDetail.DATA[row].ITEM_PAYAMT = dsDetail.DATA[row].PRINCIPAL_PAYAMT + dsDetail.DATA[row].INTEREST_PAYAMT;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;

                    calItemPay();
                }
                else if (operate_flag == 0)
                {
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = 0;
                    dsDetail.DATA[row].INTEREST_PAYAMT = 0;
                    dsDetail.DATA[row].ITEM_PAYAMT = 0;
                    //dsMain.DATA[0].PAYOUTCLR_AMT = 0;
                    dsMain.DATA[0].PAYOUTNET_AMT = payout_amt - 0;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                    // dsDetail.DATA[row].BFINTARR_AMT = 0;
                    calItemPay();
                }
            }
            else if (eventArg == PostMoneytype)
            {
                string sliptype_code = "PX";
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                //dsMain.DATA[0].TOFROM_ACCID = "";
                dsMain.DdFromAccId(sliptype_code, moneytype_code);
                SetDefaultTofromaccid();

                SetBankBranh(moneytype_code);
                CalBankFee(moneytype_code);
            }
            else if (eventArg == PostCalBankFee)
            {
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                CalBankFee(moneytype_code);
            }
            else if (eventArg == PostSlip_date)
            {
                lastDate = dsMain.DATA[0].SLIP_DATE;
                Boolean status = wcf.NCommon.of_isworkingdate(state.SsWsPass, lastDate);
                if (status == false)
                {
                    DateTime adtm_nextworkdate = new DateTime();
                    int result = wcf.NCommon.of_getnextworkday(state.SsWsPass, lastDate, ref adtm_nextworkdate);
                    if (result == 1)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("วันที่จ่ายเงินกู้ไม่ใช่วันทำการ เปลี่ยนวันที่จ่ายเงินกู้เป็น " + adtm_nextworkdate.ToString("dd/MM/yyyy"));
                        this.SetOnLoadedScript("alert('วันที่จ่ายเงินกู้ไม่ใช่วันทำการ เปลี่ยนวันที่จ่ายเงินกู้เป็น " + adtm_nextworkdate.ToString("dd/MM/yyyy") + " ')");

                        DateTime nextworkdate = adtm_nextworkdate;
                        dsMain.DATA[0].SLIP_DATE = nextworkdate;
                        lastDate = nextworkdate;
                    }
                }

                //operate_date = dsMain.DATA[0].OPERATE_DATE;

                currentLoan = Convert.ToInt16(HdIndex.Value);
                loans = Request["loans"].Split(',');
                InitLoan();
            }
            else if (eventArg == PostItem)
            {
                calItemPay();
            }
            else if (eventArg == PostRcvperiod)
            {
                decimal rcvperiod_flag = dsMain.DATA[0].RCVPERIOD_FLAG;
                if (rcvperiod_flag == 1)
                {
                    dsMain.DATA[0].LNPAYMENT_STATUS = -11;
                    dsMain.FindDropDownList(0, dsMain.DATA.LNPAYMENT_STATUSColumn).Enabled = true;
                }
                else
                {
                    dsMain.DATA[0].LNPAYMENT_STATUS = 1;
                    dsMain.FindDropDownList(0, dsMain.DATA.LNPAYMENT_STATUSColumn).Enabled = false;
                }
            }
            else if (eventArg == PostOperateFlagAdd)
            {
                int row = dsAdd.GetRowFocus();
                decimal operate_flag = dsAdd.DATA[row].OPERATE_FLAG;
                decimal item_payamt = dsAdd.DATA[row].ITEM_PAYAMT;
                string slipitemtype_code = dsAdd.DATA[row].SLIPITEMTYPE_CODE;
                string slipitem_desc = dsAdd.DATA[row].SLIPITEM_DESC;
                if (operate_flag == 1)
                {
                    dsAdd.DdLoanTypeEtc();
                    //dsAdd.DATA[row].SLIPITEMTYPE_CODE = slipitemtype_code;
                    //dsAdd.DATA[row].SLIPITEM_DESC = slipitem_desc;
                    //dsAdd.DATA[row].ITEM_PAYAMT = item_payamt;
                    //calItemPay();
                }
                else if (operate_flag == 0)
                {
                    dsAdd.DATA[row].SLIPITEMTYPE_CODE = "";
                    dsAdd.DATA[row].SLIPITEM_DESC = "";
                    dsAdd.DATA[row].ITEM_PAYAMT = 0;
                    calItemPay();
                }
            }
            else if (eventArg == PostSlipItem)
            {
                int row = dsAdd.GetRowFocus();
                string slipitemtype_code = dsAdd.DATA[row].SLIPITEMTYPE_CODE;
                //dsAdd.ItemType(slipitemtype_code);
                string sql = @" 
                 SELECT SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE,   SLUCFSLIPITEMTYPE.SLIPITEMTYPE_DESC
                 FROM SLUCFSLIPITEMTYPE  
                 WHERE SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE={0}";
                sql = WebUtil.SQLFormat(sql, slipitemtype_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsAdd.DATA[row].SLIPITEM_DESC = dt.GetString("SLIPITEMTYPE_DESC");
                }
            }
            else if (eventArg == PostBank)
            {
                string bank_code = dsMain.DATA[0].EXPENSE_BANK;
                //dsMain.SetItem(0, dsMain.DATA.BANK_CODEColumn,"");
                dsMain.DdBranch(bank_code);
            }
            else if (eventArg == PostInsertRow)
            {
                dsAdd.InsertLastRow();
                int currow = dsAdd.RowCount - 1;
                try
                {
                    dsAdd.DATA[currow].SEQ_NO = dsAdd.GetMaxValueDecimal("SEQ_NO") + 1;
                }
                catch
                {
                    if (dsAdd.RowCount < 1)
                    {
                        dsAdd.DATA[currow].SEQ_NO = 1;
                    }
                }

                dsAdd.DdLoanType();
            }
            else if (eventArg == PostDeleteRow)
            {
                int row = dsAdd.GetRowFocus();
                dsAdd.DeleteRow(row);

            }
            else if (eventArg == PostCancel)
            {
                NextLoan();
            }
            else if (eventArg == PostSave)
            {
                payavd_flag = Convert.ToDecimal(HdPayavd.Value);
                if (payavd_flag == 0)
                {

                    //เช็คยอดขอกู้ ยอดหักชำระ

                    decimal payoutnet_amt = dsMain.DATA[0].PAYOUTNET_AMT;

                    if (payoutnet_amt >= 0)
                    {

                        //เช็คลิ้นชักการเงิน
                        string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;

                        if (moneytype_code == "CSH")
                        {
                            try
                            {
                                string sql = @"select count(1) from fincash_control where entry_id = {0} and operate_date = {1} and status = 14";
                                sql = WebUtil.SQLFormat(sql, state.SsUsername, state.SsWorkDate);
                                Sdt dt = WebUtil.QuerySdt(sql);
                                if (dt.Next())
                                {
                                    fincash_status = dt.GetInt32("count(1)");
                                    if (fincash_status != 1)
                                    {
                                        NextLoan();
                                    }
                                    else
                                    {
                                        SaveLoan();
                                    }
                                }
                            }
                            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                        }
                        else
                        {
                            SaveLoan();
                        }
                    }
                    else
                    {
                        chkpayoutnet_amt = payoutnet_amt;
                        NextLoan();
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกการจ่ายเงินกู้ได้");
                }
            }
            else if (eventArg == PostPrint)
            {
                payavd_flag = Convert.ToDecimal(HdPayavd.Value);
                if (payavd_flag == 1)
                {
                    PrintPay();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกการพิมพ์จ่ายได้ เนื่องจากไม่ได้เลือกจ่ายล่วงหน้า");
                }
            }
        }

        public void SaveLoan()
        {
            bool isNotError = true;

            try
            {
                //เขียนคำสั่ง save ในนี้

                string coop_id = state.SsCoopControl;
                ExecuteDataSource exed = new ExecuteDataSource(this);
                string loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO;
                string lnrcvfrom_code = HdLnrcvfrom.Value;

                str_slippayout strPayOut = new str_slippayout();
                strPayOut.coop_id = state.SsCoopId;
                strPayOut.contcoop_id = state.SsCoopControl;
                strPayOut.slip_date = lastDate;
                strPayOut.operate_date = state.SsWorkDate;
                //strPayOut.member_no = dsMain.DATA[0].MEMBER_NO;
                strPayOut.entry_id = state.SsUsername;
                //strPayOut.loancontract_no = loancontract_no;
                //strPayOut.initfrom_type = lnrcvfrom_code;
                strPayOut.xml_sliphead = dsMain.ExportXml();
                strPayOut.xml_slipcutlon = dsDetail.ExportXml();
                strPayOut.xml_slipcutetc = dsAdd.ExportXml();

                try
                {
                    int result = wcf.NShrlon.of_saveslip_lnrcv(state.SsWsPass,ref strPayOut);
                    if (result == 1)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                        //this.SetOnLoadedScript("alert('" + alerts + "'); \n alert('บันทึกข้อมูลสำเร็จ')");
                        saveResult = "alert('บันทึกข้อมูลสำเร็จ') \n ";
                        decimal print = dsMain.DATA[0].PRINT;
                        if (state.SsCoopId == "008001") {
                            if (print == 1)
                            {
                                dsMain.ImportData(strPayOut.xml_sliphead);
                                slip_no = dsMain.DATA[0].PAYOUTSLIP_NO;
                                //slip_no = strPayOut.payoutslip_no;
                            }
                        }
                        else
                        {
                            if (print == 1)
                            {
                                try
                                {
                                    string payinslip_no = strPayOut.payinslip_no;
                                    Printing.PrintSlipSlpayin(this, payinslip_no, coop_id);
                                }
                                catch (Exception ex) { }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    exc += "alert('" + ex.Message + "'); \n ";
                    this.SetOnLoadedScript("alert('" + ex.Message + "');");
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            catch (Exception ex)
            {
                isNotError = false;
                //exc += "alert('" + ex.Message + "'); \n ";                
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            if (isNotError)
            {
                SaveNextLoan();
            }
        }

        public void WebDialogLoadEnd()
        {

            for (int i = 0; i < dsDetail.RowCount; i++)
            {
                if (dsDetail.DATA[i].OPERATE_FLAG == 1)
                {
                    dsDetail.FindTextBox(i, dsDetail.DATA.LOANCONTRACT_NOColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFLASTCALINT_DATEColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.INTEREST_PERIODColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFINTARR_AMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.PRINCIPAL_PAYAMTColumn).ReadOnly = false;
                    dsDetail.FindTextBox(i, dsDetail.DATA.INTEREST_PAYAMTColumn).ReadOnly = false;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_PAYAMTColumn).ReadOnly = false;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_BALANCEColumn).ReadOnly = true;

                }
                else
                {
                    dsDetail.FindTextBox(i, dsDetail.DATA.LOANCONTRACT_NOColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFLASTCALINT_DATEColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.INTEREST_PERIODColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFINTARR_AMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.PRINCIPAL_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.INTEREST_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_BALANCEColumn).ReadOnly = true;
                }
            }
            for (int i = 0; i < dsAdd.RowCount; i++)
            {
                if (dsAdd.DATA[i].OPERATE_FLAG == 1)
                {
                    dsAdd.FindDropDownList(i, dsAdd.DATA.SLIPITEMTYPE_CODEColumn).Enabled = true;
                    dsAdd.FindTextBox(i, dsAdd.DATA.SLIPITEM_DESCColumn).ReadOnly = false;
                    dsAdd.FindTextBox(i, dsAdd.DATA.ITEM_PAYAMTColumn).ReadOnly = false;
                }
                else
                {
                    dsAdd.FindDropDownList(i, dsAdd.DATA.SLIPITEMTYPE_CODEColumn).Enabled = false;
                    dsAdd.FindTextBox(i, dsAdd.DATA.SLIPITEM_DESCColumn).ReadOnly = true;
                    dsAdd.FindTextBox(i, dsAdd.DATA.ITEM_PAYAMTColumn).ReadOnly = true;
                }
            }
            if (dsMain.DATA[0].RCVPERIOD_FLAG == 0)
            {
                dsMain.FindTextBox(0, dsMain.DATA.PAYOUT_AMTColumn).ReadOnly = true;
            }
            else if (dsMain.DATA[0].RCVPERIOD_FLAG == 1)
            {
                dsMain.FindTextBox(0, dsMain.DATA.PAYOUT_AMTColumn).ReadOnly = false;
            }

            //ล้อคหน้าจอสำหรับ pea 
            if (state.SsCoopId == "008001")
            {
                dsMain.FindTextBox(0, dsMain.DATA.MEMBER_NOColumn).ReadOnly = true;
                dsMain.FindTextBox(0, dsMain.DATA.MEMBGROUPColumn).ReadOnly = true;
                dsMain.FindTextBox(0, dsMain.DATA.EXPENSE_ACCIDColumn).ReadOnly = true;
                //dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BANKColumn).Enabled = false;
                //dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BRANCHColumn).Enabled = false;
                //dsMain.FindDropDownList(0, dsMain.DATA.MONEYTYPE_CODEColumn).Enabled = false;


            }
            else
            {

                IsShow = "hidden";
            }

        }

        private void InitLoan()
        {
            try
            {
                string loan = loans[currentLoan];

                int fixCode = 0;
                fixCode = loan.IndexOf("@");

                string loanContractNo = loan.Substring(0, fixCode);
                string loanFix = loan.Substring(fixCode + 1);

                fixCode = loanFix.IndexOf("@");
                string lnrcvfrom_code = loanFix.Substring(0, fixCode);
                string coopId = loanFix.Substring(fixCode + 1);

                HdLnrcvfrom.Value = lnrcvfrom_code + "";


                str_slippayout sSlipPayOut = new str_slippayout();
                sSlipPayOut.coop_id = coopId;
                sSlipPayOut.contcoop_id = coopId;
                sSlipPayOut.operate_date = operate_date;
                sSlipPayOut.loancontract_no = loanContractNo;
                sSlipPayOut.slip_date = dsMain.DATA[0].SLIP_DATE;

                String initfrom_type = lnrcvfrom_code;
                sSlipPayOut.initfrom_type = initfrom_type;

                wcf.NShrlon.of_initlnrcv(state.SsWsPass, ref sSlipPayOut);

                dsMain.ImportData(sSlipPayOut.xml_sliphead);
                dsDetail.ImportData(sSlipPayOut.xml_slipcutlon);
                dsAdd.ImportData(sSlipPayOut.xml_slipcutetc);
                dsMain.DdBankDesc();
                string sliptype_code = "PX";
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                dsMain.DdFromAccId(sliptype_code, moneytype_code);
                SetDefaultTofromaccid();
                dsMain.DdLoanType();
                dsMain.DdMoneyType();

                SetBankBranh(moneytype_code);

                //Doys สงสัยคืออะไร
                if (currentLoan == 0)
                {
                    dsMain.DATA[0].OPERATE_DATE = operate_date;
                }
                else if (lastDate != null)
                {
                    dsMain.DATA[0].OPERATE_DATE = operate_date;
                    dsMain.DATA[0].SLIP_DATE = lastDate;
                }

                CalBankFee(moneytype_code);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void NextLoan()
        {
            currentLoan = Convert.ToInt16(HdIndex.Value);
            loans = Request["loans"].Split(',');

            dsMain.ResetRow();
            currentLoan += 1;
            if (currentLoan < loans.Length)
            {
                if (fincash_status == 0)
                {
                    this.SetOnLoadedScript("alert('ไม่สามารถทำรายการได้ยังไม่ได้เปิดลิ้นชัก หรือได้ปิดไปแล้วของ " + state.SsUsername + "') \n alert('ดึงข้อมูลรายการต่อไป')");
                }
                if (chkpayoutnet_amt < 0)
                {
                    this.SetOnLoadedScript("alert('ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ') \n alert('ดึงข้อมูลรายการต่อไป')");
                }

                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;
                InitLoan();
                //dsMain.DATA[0].LOANCONTRACT_NO = loans[currentLoan];
                // dsMain.DATA[0].NAME = loans[currentLoan];
            }
            HdIndex.Value = currentLoan + "";
            if ((currentLoan) >= loans.Length)
            {
                if (chkpayoutnet_amt < 0)
                {
                    this.SetOnLoadedScript("alert('ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ') \n parent.RemoveIFrame()");
                }
                else if (fincash_status == 0)
                {
                    this.SetOnLoadedScript("alert('ไม่สามารถทำรายการได้ยังไม่ได้เปิดลิ้นชัก หรือได้ปิดไปแล้วของ " + state.SsUsername + "') \n parent.RemoveIFrame()");
                }
                else
                {
                    if (state.SsCoopId == "008001")
                    {
                        decimal print = dsMain.DATA[0].PRINT;
                        if (print == 1)
                        {
                            this.SetOnLoadedScript(" parent.GetValReport(\""+slip_no+"\");");
                        }
                        else
                        {
                            this.SetOnLoadedScript(" parent.RemoveIFrame();");
                        }
                    }
                    else
                    {
                        this.SetOnLoadedScript(" parent.GetShowData();"); //"parent.RemoveIFrame() \n" + 
                    }
                }
            }
        }

        private void SaveNextLoan()
        {
            currentLoan = Convert.ToInt16(HdIndex.Value);
            loans = Request["loans"].Split(',');

            dsMain.ResetRow();
            dsDetail.ResetRow();
            dsAdd.ResetRow();
            dsMain.DATA[0].SLIP_DATE = lastDate;
            currentLoan += 1;
            if (currentLoan < loans.Length)
            {
                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;
                InitLoan();
                //dsMain.DATA[0].LOANCONTRACT_NO = loans[currentLoan];
                // dsMain.DATA[0].NAME = loans[currentLoan];
            }
            HdIndex.Value = currentLoan + "";
            if ((currentLoan) >= loans.Length)
            {
                //this.SetOnLoadedScript("parent.RemoveIFrame()");

                alerts += exc + " " + saveResult;

                

                if (state.SsCoopId == "008001")
                {
                    decimal print = dsMain.DATA[0].PRINT;
                    if (print == 1)
                    {
                        this.SetOnLoadedScript(" parent.GetValReport(\"" + slip_no + "\");");
                    }
                    else
                    {
                        this.SetOnLoadedScript(" parent.RemoveIFrame();");
                    }
                }
                else
                {
                    this.SetOnLoadedScript(alerts + " parent.GetShowData();"); //"parent.RemoveIFrame() \n" + 
                }
            }
        }

        // คิดค่าธรรมเนียม ค่าบริการ
        public void CalBankFee(String moneytype_code)
        {
            string bankbranch_code = dsMain.DATA[0].EXPENSE_BRANCH;
            try
            {
                String sql = "select fee_status from CMUCFBANKBRANCH where branch_id = '" + bankbranch_code + "'";
                sql = WebUtil.SQLFormat(sql, bankbranch_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    String fee_status = dt.GetString("fee_status");
                    double payoutnet_amt = Convert.ToDouble(dsMain.DATA[0].PAYOUTNET_AMT);
                    if (fee_status == "1" && (moneytype_code == "CBO"))// || moneytype_code == "CBT"
                    {
                        payoutnet_amt = payoutnet_amt * 0.001;

                        if (payoutnet_amt % 1 != 0)
                        {
                            payoutnet_amt = payoutnet_amt - (payoutnet_amt % 1) + 1;
                        }

                        if (payoutnet_amt < 1)
                        { payoutnet_amt = 1; }
                        else if (payoutnet_amt > 1000)
                        { payoutnet_amt = 1000; }

                        decimal payoutnet = Convert.ToDecimal(payoutnet_amt);
                        dsMain.DATA[0].BANKFEE_AMT = payoutnet;

                        //DateTime operate_date = dsMain.DATA[0].OPERATE_DATE;
                        //String loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO;
                        //String sql1 = "select loanrequest_date from lnreqloan where loancontract_no = '" + loancontract_no + "'";
                        //sql1 = WebUtil.SQLFormat(sql1);
                        //Sdt dt1 = WebUtil.QuerySdt(sql1);
                        //if (dt1.Next())
                        //{
                        //    DateTime loanrequest_date = dt1.GetDate("loanrequest_date");
                        //    if (operate_date == loanrequest_date)
                        //    {
                        dsMain.DATA[0].BANKSRV_AMT = 20;
                        //    }
                        //    else
                        //    {
                        //        dsMain.DATA[0].BANKSRV_AMT = 0;
                        //        dsMain.DATA[0].BANKFEE_AMT = 0;
                        //    }
                        //}
                    }
                    else { dsMain.DATA[0].BANKFEE_AMT = 0; dsMain.DATA[0].BANKSRV_AMT = 0; }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        //ดึงสาขาธนาคาร
        public void SetBankBranh(String moneytype_code)
        {
            try
            {
                dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BANKColumn).Enabled = true;
                if (moneytype_code == "CBT" || moneytype_code == "CBO")
                {
                    string expense_bank = dsMain.DATA[0].EXPENSE_BANK;
                    string expense_branch = dsMain.DATA[0].EXPENSE_BRANCH;
                    if (expense_branch == "")
                    {
                        string sql1 = @"select expense_bank, expense_branch, expense_accid from lnreqloan where coop_id = {0} and member_no = {1}";
                        sql1 = WebUtil.SQLFormat(sql1, state.SsCoopControl, dsMain.DATA[0].MEMBER_NO);
                        Sdt dt1 = WebUtil.QuerySdt(sql1);

                        if (dt1.Next())
                        {
                            expense_branch = dt1.GetString("expense_branch");
                            expense_bank = dt1.GetString("expense_bank");
                            dsMain.DdBranch(expense_bank);

                            string expense_accid = dt1.GetString("expense_accid");
                            dsMain.DATA[0].EXPENSE_BANK = expense_bank;
                            dsMain.DATA[0].EXPENSE_BRANCH = expense_branch;
                            dsMain.DATA[0].EXPENSE_ACCID = expense_accid;
                            SetDefaultTofromaccid();
                        }
                    }
                    else
                    {
                        dsMain.DdBranch(expense_bank);
                    }
                }
                else if (moneytype_code == "CSH")
                {
                    dsMain.DdBranch("");
                    dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BANKColumn).Enabled = false;
                    dsMain.DATA[0].EXPENSE_BANK = "";
                    dsMain.DATA[0].EXPENSE_BRANCH = "";
                    dsMain.DATA[0].EXPENSE_ACCID = "";
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void calItemPay()
        {
            int row = dsDetail.RowCount;
            int rowAdd = dsAdd.RowCount;
            decimal payoutclr_amt = 0;
            for (int i = 0; i < row; i++)
            {
                decimal item_payamt = dsDetail.DATA[i].ITEM_PAYAMT;
                payoutclr_amt += item_payamt;
            }
            for (int i = 0; i < rowAdd; i++)
            {
                decimal item_payamt = dsAdd.DATA[i].ITEM_PAYAMT;
                payoutclr_amt += item_payamt;

            }
            dsMain.DATA[0].PAYOUTCLR_AMT = payoutclr_amt;
            dsMain.DATA[0].PAYOUTNET_AMT = dsMain.DATA[0].PAYOUT_AMT - dsMain.DATA[0].PAYOUTCLR_AMT + dsMain.DATA[0].RETURNETC_AMT;

        }

        private void SetDefaultTofromaccid()
        {
            try
            {
                string sliptype_code = "LWD";
                string sql = @"select 
	            account_id
            from 
	            cmucftofromaccid where
	            coop_id={0} and 
	            moneytype_code={1} and
	            sliptype_code={2} and
	            defaultpayout_flag=1";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].MONEYTYPE_CODE, sliptype_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    string accid = dt.GetString("account_id");
                    dsMain.DATA[0].TOFROM_ACCID = accid;
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void PrintPay()
        {
            try
            {
                string sqlSelLnreqClr = "", sqlUpdateLnreq = "", loanrequest_docno = "", cont_no = "", sqlUpdateInt = "";
                DateTime calint_to, printpay_date;
                decimal intclear_amt = 0, principal_balance = 0, sum_clear = 0;

                sum_clear = 0;
                calint_to = dsMain.DATA[0].SLIP_DATE;
                printpay_date = dsMain.DATA[0].SLIP_DATE;
                loanrequest_docno = dsMain.DATA[0].LOANREQUEST_DOCNO;

                //การทำงานแบบเก่าที่ปุ่มอยู่บนหน้าจอหลัก
                ////ค้นหาสัญญาเก่าที่จะหักกลบมาคำวนวนดอกเบี้ย
                //sqlSelLnreqClr = "select * from lnreqloanclr where coop_id={0} and loanrequest_docno={1} and clear_status=1";
                //sqlSelLnreqClr = WebUtil.SQLFormat(sqlSelLnreqClr, state.SsCoopId, loanrequest_docno);

                //Sdt dtSelLnreqClr = WebUtil.QuerySdt(sqlSelLnreqClr);

                //while (dtSelLnreqClr.Next())
                //{
                //    cont_no = dtSelLnreqClr.GetString("loancontract_no");
                //    intclear_amt = wcf.NShrlon.of_computeinterest(state.SsWsPass, state.SsCoopId, cont_no, calint_to); //คำนวณดอกเบี้ย
                //    principal_balance = dtSelLnreqClr.GetDecimal("principal_balance");
                //    sum_clear += (principal_balance + intclear_amt); //รวมยอดเงินต้นและดอกเบี้ยของสัญญาเก่าที่จะหักกลบ

                //    //อัพเดท ดอกเบี้ยของแต่ละสัญญา
                //    sqlUpdateInt = "update lnreqloanclr set intclear_amt={0} where coop_id={1} and loancontract_no={2} and loanrequest_docno={3}";
                //    sqlUpdateInt = WebUtil.SQLFormat(sqlUpdateInt, intclear_amt, state.SsCoopId, cont_no, loanrequest_docno);
                //    Sdt dtUpdateInt = WebUtil.QuerySdt(sqlUpdateInt);

                //}

                //// อัพเดทสถานะให้ใบคำขอ สถานะ 12 และยอดรวมที่จะหักกลบทั้งหมด
                //sqlUpdateLnreq = "update lnreqloan set loanrequest_status=12 ,sum_clear={0} where coop_id={1} and loanrequest_docno={2}";
                //sqlUpdateLnreq = WebUtil.SQLFormat(sqlUpdateLnreq, sum_clear, state.SsCoopId, loanrequest_docno);
                //Sdt dtUpdateLnreq = WebUtil.QuerySdt(sqlUpdateLnreq);

                UpdateReqLoanClr();

                UpdateReqloanClrOther();
                DateTime wd = state.SsWorkDate;
                DateTime wt = new DateTime();
                wt = DateTime.Now;               
                DateTime dt = new DateTime(wd.Year, wd.Month, wd.Day, wt.Hour, wt.Minute, wt.Second);
                try
                {
                    // อัพเดทสถานะให้ใบคำขอ สถานะ 12 
                    sqlUpdateLnreq = "update lnreqloan set loanrequest_status=12 ,printpay_date={2},entry_date={3} where coop_id={0} and loanrequest_docno={1}";
                    sqlUpdateLnreq = WebUtil.SQLFormat(sqlUpdateLnreq, state.SsCoopControl, loanrequest_docno, printpay_date, dt);
                    Sdt dtUpdateLnreq = WebUtil.QuerySdt(sqlUpdateLnreq);

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการพิมพ์จ่ายสำเร็จ");

                }
                catch
                {
                    throw new Exception("ไม่สามารถบันทึกการพิมพ์จ่ายได้");
                }
            }
            catch (Exception ex)
            {

                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

            
        }

        //อัพเดทสัญญาหักกลบ
        public void UpdateReqLoanClr()
        {

            try
            {
                string loanrequest_docno = "", loancontract_no_clr = "";
                string sql_update_clr = "";
                decimal prnclr_amt = 0, intclr_amt = 0, pricipal_bal = 0, clr_amt = 0;
                loanrequest_docno = dsMain.DATA[0].LOANREQUEST_DOCNO;

                for (int i = 0; i < dsDetail.RowCount; i++)
                {
                    prnclr_amt = 0;
                    intclr_amt = 0;
                    pricipal_bal = 0;
                    clr_amt = 0;
                    loancontract_no_clr = "";
                    try
                    {
                        if (dsDetail.DATA[i].OPERATE_FLAG == 1)
                        {
                            loancontract_no_clr = dsDetail.DATA[i].LOANCONTRACT_NO;
                            prnclr_amt = dsDetail.DATA[i].PRINCIPAL_PAYAMT;
                            intclr_amt = dsDetail.DATA[i].INTEREST_PAYAMT;
                            pricipal_bal = dsDetail.DATA[i].ITEM_BALANCE;
                            clr_amt = dsDetail.DATA[i].ITEM_PAYAMT;
                            sql_update_clr = @"update lnreqloanclr set prnclear_amt={0},intclear_amt={1},principal_balance={2},clear_amount={3},clear_status=1
                    where loanrequest_docno={4} and loancontract_no={5} and coop_id={6}";
                            sql_update_clr = WebUtil.SQLFormat(sql_update_clr, prnclr_amt, intclr_amt, pricipal_bal, clr_amt, loanrequest_docno, loancontract_no_clr, state.SsCoopControl);
                            Sdt dt = WebUtil.QuerySdt(sql_update_clr);
                        }
                        else
                        {
                            sql_update_clr = @"update lnreqloanclr set prnclear_amt={0},intclear_amt={1},clear_amount={2},clear_status=0
                    where loanrequest_docno={3} and loancontract_no={4} and coop_id={5}";
                            sql_update_clr = WebUtil.SQLFormat(sql_update_clr, prnclr_amt, intclr_amt, clr_amt, loanrequest_docno, loancontract_no_clr, state.SsCoopControl);
                            Sdt dt = WebUtil.QuerySdt(sql_update_clr);
                        }
                    }
                    catch { }
                }
            }
            catch
            {
                throw new Exception("ทำรายการบัญชีหักกลบไม่สำเร็จ");
            }

        }

        //อัพเดทหักอื่นๆ
        public void UpdateReqloanClrOther()
        {
            string loanrequest_docno = "", clrothertype_code = "", clrother_desc = "";
            decimal clrother_amt = 0, seq_no = 0, clear_status = 0;
            string sql_select = "", sql_update = "", sql_insert = "";
            try
            {
                loanrequest_docno = dsMain.DATA[0].LOANREQUEST_DOCNO;
                for (int i = 0; i < dsAdd.RowCount; i++)
                {
                    try
                    {
                        seq_no = dsAdd.DATA[i].SEQ_NO;
                        clrothertype_code = dsAdd.DATA[i].SLIPITEMTYPE_CODE;
                        clrother_desc = dsAdd.DATA[i].SLIPITEM_DESC;
                        clrother_amt = dsAdd.DATA[i].ITEM_PAYAMT;
                        clear_status = dsAdd.DATA[i].OPERATE_FLAG;

                        sql_select = "select * from lnreqloanclrother where coop_id={0} and loanrequest_docno={1} and seq_no={2}";
                        sql_select = WebUtil.SQLFormat(sql_select, state.SsCoopControl, loanrequest_docno, seq_no);
                        Sdt dt_sel = WebUtil.QuerySdt(sql_select);
                        if (dt_sel.Next())
                        {
                            sql_update = @"update lnreqloanclrother set clrothertype_code={0},clrother_desc={1},clrother_amt={2},clear_status={3}
where coop_id={4} and loanrequest_docno={5} and seq_no={6}";
                            sql_update = WebUtil.SQLFormat(sql_update, clrothertype_code, clrother_desc, clrother_amt, clear_status, state.SsCoopControl, loanrequest_docno, seq_no);
                            Sdt dt_update = WebUtil.QuerySdt(sql_update);
                        }
                        else
                        {
                            sql_insert = @"insert into lnreqloanclrother (coop_id,loanrequest_docno,seq_no,clrothertype_code,clrother_desc,clrother_amt,clear_status) 
values ({0},{1},{2},{3},{4},{5},{6})";
                            sql_insert = WebUtil.SQLFormat(sql_insert, state.SsCoopControl, loanrequest_docno, seq_no, clrothertype_code, clrother_desc, clrother_amt, clear_status);
                            Sdt dt_ins = WebUtil.QuerySdt(sql_insert);
                        }
                    }
                    catch { }
                }

            }
            catch
            {
                throw new Exception("ทำรายการยอดหักอื่นๆไม่สำเร็จ");
            }

        }


    }
}