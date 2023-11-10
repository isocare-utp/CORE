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

namespace Saving.Applications.shrlon.dlg.w_dlg_loan_receive_ctrl
{
    public partial class w_dlg_loan_receive : PageWebDialog, WebDialog
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
        public string Postof_calbankfee { get; set; }
        [JsPostBack]
        public string PostSlip_date { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }
        [JsPostBack]
        public string PostRetrieveBranch { get; set; }
        [JsPostBack]
        public string Postof_Payout { get; set; }
        string[] loans;

        string slip_no = "";
        string slipin_no = "";
        CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
        DateTime idtm_lastDate, idtm_activedate;
        int currentLoan = 0;
        bool isNotError = true;
        decimal chkpayoutnet_amt = 0, payavd_flag = 0;

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
                loans = Request["loans"].Split(',');
                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;

                of_activeworkdate();
                idtm_activedate = dsMain.DATA[0].cp_activedate;
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DATA[0].SLIP_DATE = idtm_activedate;
                if (idtm_activedate != state.SsWorkDate)
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ระบบได้ทำการปิดสิ้นวันแล้ว ระบบจะทำการเปลี่ยนวันที่เป็น " + idtm_activedate.ToString("dd/MM/yyyy", th));
                }

                of_initloanrcv();
                HdIndex.Value = currentLoan + "";
                dsMain.FindDropDownList(0, dsMain.DATA.LNPAYMENT_STATUSColumn).Enabled = false;
            }
            else
            {
                LtServerMessage.Text = "";
                idtm_lastDate = dsMain.DATA[0].cp_lastdate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostOperateFlag)
            {
                decimal principal_payamt = 0;
                int row = dsDetail.GetRowFocus();
                decimal operate_flag = dsDetail.DATA[row].OPERATE_FLAG;
                decimal bfshrcont_balamt = dsDetail.DATA[row].BFSHRCONT_BALAMT;
                decimal interest_period = dsDetail.DATA[row].INTEREST_PERIOD;
                decimal bfintarr_amt = dsDetail.DATA[row].BFINTARR_AMT;
                decimal payout_amt = dsMain.DATA[0].PAYOUT_AMT;
                decimal payoutnet_amt = dsMain.DATA[0].PAYOUTNET_AMT;
                if (operate_flag == 1)
                {
                    decimal ldc_rkeepprin = dsDetail.DATA[row].RKEEP_PRINCIPAL;
                    int li_pxafkeep = Convert.ToInt32(dsDetail.DATA[row].BFPXAFTERMTHKEEP_TYPE);
                    //เช็คว่ามีเรียกเก็บอยู่หรือไม่
                    if (li_pxafkeep >= 1 && ldc_rkeepprin > 0)
                    {
                        if (payoutnet_amt < bfshrcont_balamt)
                        {
                            principal_payamt = payoutnet_amt - interest_period - bfintarr_amt;
                        }
                        else
                        {

                            principal_payamt = bfshrcont_balamt - ldc_rkeepprin;
                        }
                    }
                    else
                    {
                        principal_payamt = bfshrcont_balamt;
                    }

                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = principal_payamt;
                    dsDetail.DATA[row].INTEREST_PAYAMT = interest_period + bfintarr_amt;

                    dsDetail.DATA[row].ITEM_PAYAMT = dsDetail.DATA[row].PRINCIPAL_PAYAMT + dsDetail.DATA[row].INTEREST_PAYAMT;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;

                    calItemPay();
                }
                else if (operate_flag == 0)
                {
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = 0;
                    dsDetail.DATA[row].INTEREST_PAYAMT = 0;
                    dsDetail.DATA[row].ITEM_PAYAMT = 0;
                    dsMain.DATA[0].PAYOUTNET_AMT = payout_amt - 0;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;

                    calItemPay();
                }
            }
            else if (eventArg == PostMoneytype)
            {
                string sliptype_code = "PX";
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                //dsMain.DATA[0].TOFROM_ACCID = "";
                dsMain.DdFromAccId(sliptype_code, moneytype_code);
                of_setdefaulttofromaccid();

                of_setbankbranh(moneytype_code);
                of_calbankfee(moneytype_code);
            }
            else if (eventArg == Postof_Payout)
            {
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                currentLoan = Convert.ToInt16(HdIndex.Value);
                loans = Request["loans"].Split(',');
                of_initrecalint();
                of_calbankfee(moneytype_code);
            }
            else if (eventArg == Postof_calbankfee)
            {
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                //of_calbankfee(moneytype_code);
                calItemPay();
            }
            else if (eventArg == PostSlip_date)
            {
                DateTime ldtm_slipdate = dsMain.DATA[0].SLIP_DATE;
                idtm_lastDate = dsMain.DATA[0].cp_lastdate;
                idtm_activedate = dsMain.DATA[0].cp_activedate;
                if (ldtm_slipdate < idtm_activedate)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่อนุญาตให้วันที่จ่ายเงินกู้ " + ldtm_slipdate.ToString("dd/MM/yyyy", th) + " น้อยกว่าวันทำการ " + idtm_activedate.ToString("dd/MM/yyyy", th) + " กรุณาตรวจสอบ");
                    dsMain.DATA[0].SLIP_DATE = idtm_lastDate;
                    return;
                }
                //ตรวจสอบวันที่ป้อน ว่าเป็นวันทำการหรือไม่
                Boolean status = wcf.NCommon.of_isworkingdate(state.SsWsPass, ldtm_slipdate);
                if (status == false)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("วันที่จ่ายเงินกู้ " + ldtm_slipdate.ToString("dd/MM/yyyy", th) + " ไม่ใช่วันทำการ กรุณาตรวจสอบ");
                    dsMain.DATA[0].SLIP_DATE = idtm_lastDate;
                    return;
                }

                dsMain.DATA[0].cp_lastdate = ldtm_slipdate;


                currentLoan = Convert.ToInt16(HdIndex.Value);
                loans = Request["loans"].Split(',');

                of_initrecalint();

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
                string sql;
                if (slipitemtype_code == "SHR")
                {
                    sql = @"select shpay_desc from slucfsliptype where sliptype_code = 'CLC'";
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        dsAdd.DATA[row].SLIPITEM_DESC = dt.GetString("shpay_desc");
                    }
                }
                else
                {
                    sql = @" 
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
                calItemPay();
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
                        Boolean lbl_fin = of_checkfin();
                        if (lbl_fin == false)
                        {
                            isNotError = false;
                            NextLoan();
                            return;
                        }
                        else
                        {
                            SaveLoan();
                        }
                    }
                    else
                    {
                        chkpayoutnet_amt = payoutnet_amt;
                        this.SetOnLoadedScript("alert('ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ!!')");
                        isNotError = false;
                        NextLoan();
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกการจ่ายเงินกู้ได้");
                }
            }
            else if (eventArg == PostRetrieveBranch)
            {
                dsMain.DdBranchLike(dsMain.DATA[0].EXPENSE_BANK, dsMain.DATA[0].txt_branch_name);
            }
        }

        /// <summary>
        /// get วันทำการ
        /// </summary>       
        public void of_activeworkdate()
        {
            try
            {
                string sqlStr;
                int li_clsdaystatus = 0;
                DateTime ldtm_workdate;
                Sdt dt;
                sqlStr = @" select workdate, closeday_status
                    from amappstatus 
                    where coop_id = '" + state.SsCoopId + @"'
                    and application = 'shrlon'";
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    ldtm_workdate = dt.GetDate("workdate");
                    li_clsdaystatus = dt.GetInt32("closeday_status");
                    if (li_clsdaystatus == 1)
                    {
                        int result = wcf.NCommon.of_getnextworkday(state.SsWsPass, state.SsWorkDate, ref idtm_activedate);
                        dsMain.DATA[0].cp_lastdate = idtm_activedate;
                        dsMain.DATA[0].cp_activedate = idtm_activedate;
                    }
                    else
                    {
                        dsMain.DATA[0].cp_activedate = state.SsWorkDate;
                        dsMain.DATA[0].cp_lastdate = state.SsWorkDate;
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        /// Init ข้อมุลการจ่ายเงินกู้
        /// </summary>
        private void of_initloanrcv()
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

                idtm_activedate = dsMain.DATA[0].cp_activedate;
                idtm_lastDate = dsMain.DATA[0].cp_lastdate;

                str_slippayout sSlipPayOut = new str_slippayout();
                sSlipPayOut.initfrom_type = lnrcvfrom_code;
                sSlipPayOut.loancontract_no = loanContractNo;
                sSlipPayOut.slip_date = dsMain.DATA[0].SLIP_DATE;
                sSlipPayOut.operate_date = dsMain.DATA[0].OPERATE_DATE;

                int result = wcf.NShrlon.of_initlnrcv(state.SsWsPass, ref sSlipPayOut);
                dsMain.ResetRow();
                dsDetail.ResetRow();
                dsAdd.ResetRow();
                dsMain.ImportData(sSlipPayOut.xml_sliphead);
                dsDetail.ImportData(sSlipPayOut.xml_slipcutlon);
                dsAdd.ImportData(sSlipPayOut.xml_slipcutetc);

                dsMain.DATA[0].cp_activedate = idtm_activedate;
                dsMain.DATA[0].cp_lastdate = idtm_lastDate;

                //เช็คยอดจ่ายเงินกู้ ว่าน้อยกว่ายอดหักหรือไม่
                decimal ldc_payoutnet = dsMain.DATA[0].PAYOUTNET_AMT;
                if (ldc_payoutnet < 0)
                {
                    of_calgenitemclr();
                    //LtServerMessage.Text = WebUtil.ErrorMessage("ยอดหักชำระมากกว่ายอดจ่ายเงินกู้ กรุณาตรวจสอบ");
                }
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;

                dsMain.DdLoanType();
                dsMain.DdMoneyType();
                dsMain.DdBankDesc();
                dsMain.DdFromAccId("LWD", moneytype_code);
                of_setdefaulttofromaccid();
                of_setbankbranh(moneytype_code);
                of_calbankfee(moneytype_code);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void of_calgenitemclr()
        {
            try
            {

                decimal bfshrcont_balamt = 0, interest_period = 0, bfintarr_amt = 0, principal_payamt = 0;
                decimal payout_amt = dsMain.DATA[0].PAYOUT_AMT;
                decimal net_amt = dsMain.DATA[0].PAYOUTNET_AMT;
                int rowcount = dsDetail.RowCount;
                string loantypercv = dsMain.DATA[0].SHRLONTYPE_CODE;
                string loantypeclr = "";
                if (net_amt < 0)
                {
                    for (int i = 0; i < rowcount; i++)
                    {
                        loantypeclr = dsDetail.DATA[i].SHRLONTYPE_CODE;
                        bfshrcont_balamt = dsDetail.DATA[i].BFSHRCONT_BALAMT;
                        interest_period = dsDetail.DATA[i].INTEREST_PERIOD;
                        bfintarr_amt = dsDetail.DATA[i].BFINTARR_AMT;
                        if (loantypeclr == loantypercv)
                        {

                            decimal ldc_rkeepprin = dsDetail.DATA[i].RKEEP_PRINCIPAL;
                            int li_pxafkeep = Convert.ToInt32(dsDetail.DATA[i].BFPXAFTERMTHKEEP_TYPE);
                            //เช็คว่ามีเรียกเก็บอยู่หรือไม่
                            if (li_pxafkeep >= 1 && ldc_rkeepprin > 0)
                            {
                                principal_payamt = bfshrcont_balamt - ldc_rkeepprin;
                            }
                            else
                            {
                                principal_payamt = bfshrcont_balamt;
                            }
                            if (payout_amt < principal_payamt)
                            {
                                principal_payamt = payout_amt - interest_period - bfintarr_amt;
                            }
                            dsDetail.DATA[i].PRINCIPAL_PAYAMT = principal_payamt;
                            dsDetail.DATA[i].INTEREST_PAYAMT = interest_period + bfintarr_amt;

                            dsDetail.DATA[i].ITEM_PAYAMT = dsDetail.DATA[i].PRINCIPAL_PAYAMT + dsDetail.DATA[i].INTEREST_PAYAMT;
                            dsDetail.DATA[i].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[i].PRINCIPAL_PAYAMT;

                        }
                        else
                        {
                            dsDetail.DATA[i].OPERATE_FLAG = 0;
                            dsDetail.DATA[i].PRINCIPAL_PAYAMT = 0;
                            dsDetail.DATA[i].INTEREST_PAYAMT = 0;
                            dsDetail.DATA[i].ITEM_PAYAMT = 0;
                            // dsMain.DATA[0].PAYOUTNET_AMT = payout_amt - 0;
                            dsDetail.DATA[i].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[i].PRINCIPAL_PAYAMT;


                        }

                        calItemPay();

                    }



                }

            }
            catch (Exception exx)
            {
                LtServerMessage.Text = WebUtil.WarningMessage(exx.ToString());


            }


        }
        /// <summary>
        /// คำนวณดอกเบี้ยใหม่
        /// </summary>
        private void of_initrecalint()
        {

            string loan = loans[currentLoan];

            int fixCode = 0;
            fixCode = loan.IndexOf("@");

            string loanFix = loan.Substring(fixCode + 1);

            fixCode = loanFix.IndexOf("@");
            string coopId = loanFix.Substring(fixCode + 1);

            idtm_activedate = dsMain.DATA[0].cp_activedate;
            idtm_lastDate = dsMain.DATA[0].cp_lastdate;

            str_slippayout sSlipPayOut = new str_slippayout();
            sSlipPayOut.xml_sliphead = dsMain.ExportXml();
            sSlipPayOut.xml_slipcutlon = dsDetail.ExportXml();
            sSlipPayOut.xml_slipcutetc = dsAdd.ExportXml();
            int result = wcf.NShrlon.of_initlnrcv_recalint(state.SsWsPass, ref sSlipPayOut);

            dsMain.ImportData(sSlipPayOut.xml_sliphead);
            dsDetail.ImportData(sSlipPayOut.xml_slipcutlon);
            dsAdd.ImportData(sSlipPayOut.xml_slipcutetc);

            dsMain.DATA[0].cp_activedate = idtm_activedate;
            dsMain.DATA[0].cp_lastdate = idtm_lastDate;

            //เช็คยอดจ่ายเงินกู้ ว่าน้อยกว่ายอดหักหรือไม่
            decimal ldc_payoutnet = dsMain.DATA[0].PAYOUTNET_AMT;
            if (ldc_payoutnet < 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ยอดหักชำระมากกว่ายอดจ่ายเงินกู้ กรุณาตรวจสอบ");
            }
            string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;

            dsMain.DdLoanType();
            dsMain.DdMoneyType();
            dsMain.DdBankDesc();
            dsMain.DdFromAccId("LWD", moneytype_code);
            of_setdefaulttofromaccid();
            of_setbankbranh(moneytype_code);
            of_calbankfee(moneytype_code);
        }

        /// <summary>
        /// บันทึกการจ่ายเงินกู้
        /// </summary>
        public void SaveLoan()
        {
            string coop_id = state.SsCoopControl;
            ExecuteDataSource exed = new ExecuteDataSource(this);
            string loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO;
            string lnrcvfrom_code = HdLnrcvfrom.Value;

            str_slippayout strPayOut = new str_slippayout();
            strPayOut.entry_id = state.SsUsername;
            strPayOut.xml_sliphead = dsMain.ExportXml();
            strPayOut.xml_slipcutlon = dsDetail.ExportXml();
            strPayOut.xml_slipcutetc = dsAdd.ExportXml();

            try
            {
                int result = wcf.NShrlon.of_saveslip_lnrcv(state.SsWsPass, ref strPayOut);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                    this.SetOnLoadedScript("alert('บันทึกข้อมูลสำเร็จ')");
                    decimal print = dsMain.DATA[0].PRINT;
                    if (print == 1)
                    {
                        try
                        {
                            dsMain.ImportData(strPayOut.xml_sliphead);
                            slip_no = dsMain.DATA[0].PAYOUTSLIP_NO;
                            Hdslpayout.Value += "," + slip_no;
                            Hdslpayin.Value += "," + strPayOut.payinslip_no;
                            //slip_no = strPayOut.payoutslip_no;
                        }
                        catch (Exception ex1)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex1.Message);
                        }
                        try
                        {
                            slipin_no = strPayOut.payinslip_no;
                            //Printing.PrintSlipSlpayin(this, payinslip_no, coop_id);
                            if (state.SsCoopControl == "003001") // ของ mhs tomy 19/10/2017
                            {
                                Printing.PrintSlipSlpayin(this, slipin_no, state.SsCoopControl);
                            }
                            else if (state.SsCoopControl == "030001") //ใบเสร็จรับเงิน-จ่ายเงินกู้ = sl_slip_payout 
                            {
                                Printing.PrintSlipSlpayOut(this, slipin_no, state.SsCoopControl);
                            }
                        }
                        catch (Exception ex2)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(LtServerMessage.Text + " : " + ex2.Message);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                isNotError = false;
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            if (isNotError)
            {
                NextLoan();
            }
        }

        /// <summary>
        /// ดึงสัญญาถัดไป
        /// </summary> 
        private void NextLoan()
        {
            currentLoan = Convert.ToInt16(HdIndex.Value);
            loans = Request["loans"].Split(',');

            currentLoan += 1;
            if (currentLoan < loans.Length)
            {
                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;
                of_initloanrcv();
            }
            if (currentLoan < loans.Length)
            {
                if (chkpayoutnet_amt < 0)
                {
                    this.SetOnLoadedScript("alert('ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ') \n alert('ดึงข้อมูลรายการต่อไป')");
                }

                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;
                of_initloanrcv();
            }
            HdIndex.Value = currentLoan + "";
            if ((currentLoan) >= loans.Length)
            {
                if (chkpayoutnet_amt < 0)
                {
                    this.SetOnLoadedScript("alert('ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ') \n parent.RemoveIFrame()");
                }
                else
                {
                    decimal print = dsMain.DATA[0].PRINT;
                    if (print == 1)
                    {
                        if (state.SsCoopControl == "035001")
                        {
                            this.SetOnLoadedScript(" parent.PrintSlipAll(\"" + Hdslpayout.Value + "\",\"" + Hdslpayin.Value + "\"); parent.RemoveIFrame();");
                        }
                        else
                        {
                            this.SetOnLoadedScript(" parent.PrintSlipoutGsb(\"" + slip_no + "\",\"" + slipin_no + "\"); parent.RemoveIFrame();");
                        }
                    }
                    else
                    {
                        this.SetOnLoadedScript("parent.PrintSlipoutGsb(\"" + slip_no + "\",\"" + slipin_no + "\"); parent.RemoveIFrame();");
                    }
                    if (state.SsCoopControl == "030001") //ใบเสร็จรับเงิน-จ่ายเงินกู้ = sl_slip_payout 
                    {
                        this.SetOnLoadedScript("parent.GetShowData();");
                    }
                }
            }
        }

        /// <summary>
        /// ตรวจสอบการเงิน
        /// </summary>
        private Boolean of_checkfin()
        {
            string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
            string sqlStr = "";
            decimal allpay_atfin = 0, shrlonpaysendfin = 0;
            Sdt dt;
            if (moneytype_code == "CSH")
            {
                //เช็คว่าวันจ่ายเงินกู้เป็นวันเดียวกันกับวันทำการหรือไม่
                idtm_activedate = dsMain.DATA[0].cp_activedate;
                //if (state.SsWorkDate != idtm_activedate)
                //{
                //    this.SetOnLoadedScript("alert('ประเภทการจ่ายเป็นเงินสด ไม่สามารถจ่ายเงินกู้ล่วงหน้าได้ กรุณาตรวจสอบ') \n alert('ดึงข้อมูลรายการต่อไป')");
                //    return false;
                //}

                //เช็คว่าต้องการตรวจสอบการเงินหรือไม่
                sqlStr = @"select allpay_atfin, shrlonpaysendfin from finconstant where coop_id = '" + state.SsCoopControl + "'";
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    allpay_atfin = dt.GetInt32("allpay_atfin");
                    shrlonpaysendfin = dt.GetInt32("shrlonpaysendfin");
                }

                if (allpay_atfin == 1)
                {
                    //เช็คลิ้นชักการเงิน
                    if (shrlonpaysendfin == 1 || shrlonpaysendfin == 3)
                    {
                        try
                        {
                            sqlStr = @"select status from fintableusermaster where user_name = {0} and opdatework = {1}";
                            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsUsername, state.SsWorkDate);
                            dt = WebUtil.QuerySdt(sqlStr);
                            if (dt.Next())
                            {
                                int status = dt.GetInt32("status");
                                if (status == 14)
                                {
                                    this.SetOnLoadedScript("alert('ไม่สามารถทำรายการได้เนื่องจากมีการปิดลิ้นชักไปแล้วของ " + state.SsUsername + "') \n alert('ดึงข้อมูลรายการต่อไป')");
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                this.SetOnLoadedScript("alert('ผู้ทำรายกายยังไม่ได้เปิดลิ้นชัก " + state.SsUsername + "') \n alert('ดึงข้อมูลรายการต่อไป')");
                                return false;
                            }
                        }
                        catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// คิดค่าธรรมเนียม ค่าบริการ
        /// </summary>       
        public void of_calbankfee(String moneytype_code)
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
                        dsMain.DATA[0].BANKSRV_AMT = 20;
                    }
                    else { dsMain.DATA[0].BANKFEE_AMT = 0; dsMain.DATA[0].BANKSRV_AMT = 0; }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        /// ดึงสาขาธนาคาร
        /// </summary> 
        public void of_setbankbranh(String moneytype_code)
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
                        string sql1;
                        if (state.SsCoopControl == "027001" || state.SsCoopControl == "021001")
                        {
                            sql1 = @" select expense_bank, expense_branch, expense_accid from lnreqloan where coop_id = {0} and member_no = {1}
	                                and entry_date in (	select max(entry_date) from lnreqloan where coop_id = {0} and member_no = {1}	)";
                        }
                        else
                        {
                            sql1 = @"select expense_bank, expense_branch, expense_accid from lnreqloan where coop_id = {0} and member_no = {1}";
                        }
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
                            of_setdefaulttofromaccid();
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
            of_calbankfee(dsMain.DATA[0].MONEYTYPE_CODE);
        }

        /// <summary>
        /// set คู่บัญชี
        /// </summary> 
        private void of_setdefaulttofromaccid()
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
            //tomy เดียวมาทำต่อ เอาให้พี่มงไปก่อน 9/3/2017
            //if ( dsMain.DATA[0].MONEYTYPE_CODE == "TRN" || dsMain.DATA[0].MONEYTYPE_CODE == "TBK")
            //{
            //    dsMain.FindButton(0, 
            //}
        }
    }
}