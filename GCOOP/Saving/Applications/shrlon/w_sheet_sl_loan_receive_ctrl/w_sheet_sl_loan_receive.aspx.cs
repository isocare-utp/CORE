using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Drawing;
using CoreSavingLibrary.WcfNShrlon;
using System.Data;
using System.IO;
using System.Globalization;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_ctrl
{
    public partial class w_sheet_sl_loan_receive : PageWebSheet, WebSheet
    {
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
        [JsPostBack]
        public string PostMember { get; set; }
        [JsPostBack]
        public string PostLoancontract { get; set; }
        string[] loans;

        string slip_no = "";
        string slipin_no = "", ls_slipno = "";
        string ls_slnoout = "", ls_slnoin = "";
        CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
        DateTime idtm_lastDate, idtm_activedate;
        int currentLoan = 0;
        bool isNotError = true;
        decimal chkpayoutnet_amt = 0, payavd_flag = 0;

        void WebSheet.InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
            dsAdd.InitDsAdd(this);
        }

        void WebSheet.WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                of_activeworkdate();
                idtm_activedate = dsMain.DATA[0].cp_activedate;
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DATA[0].SLIP_DATE = idtm_activedate;
                if (idtm_activedate != state.SsWorkDate)
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ระบบได้ทำการปิดสิ้นวันแล้ว ระบบจะทำการเปลี่ยนวันที่เป็น " + idtm_activedate.ToString("dd/MM/yyyy", th));
                }
                dsMain.FindDropDownList(0, dsMain.DATA.LNPAYMENT_STATUSColumn).Enabled = false;
            }
            else
            {
                LtServerMessage.Text = "";
                idtm_lastDate = dsMain.DATA[0].cp_lastdate;
            }
        }

        void WebSheet.CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostOperateFlag)
            {
                decimal principal_payamt = 0, interest_payamt = 0;
                int row = dsDetail.GetRowFocus();
                decimal operate_flag = dsDetail.DATA[row].OPERATE_FLAG;
                decimal bfshrcont_balamt = dsDetail.DATA[row].BFSHRCONT_BALAMT;
                decimal interest_period = dsDetail.DATA[row].INTEREST_PERIOD;
                decimal bfintarr_amt = dsDetail.DATA[row].BFINTARR_AMT;
                decimal payout_amt = dsMain.DATA[0].PAYOUT_AMT;

                if (operate_flag == 1)
                {
                    decimal ldc_rkeepprin = dsDetail.DATA[row].RKEEP_PRINCIPAL;
                    decimal ldc_rkeepint = dsDetail.DATA[row].RKEEP_INTEREST;
                    decimal ldc_nkeepint = dsDetail.DATA[row].NKEEP_INTEREST;
                    int li_pxafkeep = Convert.ToInt32(dsDetail.DATA[row].BFPXAFTERMTHKEEP_TYPE);

                    principal_payamt = bfshrcont_balamt;

                    // ถ้าเป็นการกันยอดเรียกเก็บไว้ ตัดต้นเรียกเก็บออก
                    if (li_pxafkeep == 1 && ldc_rkeepprin > 0)
                    {
                        principal_payamt = principal_payamt - ldc_rkeepprin;
                    }
                    // ถ้าเป็นการกันยอดเรียกเก็บไว้ในส่วน ด/บ ต้องดูว่ามีค้างจิงๆเท่าไหร่
                    if (li_pxafkeep == 1 && ldc_rkeepint > 0)
                    {
                        bfintarr_amt = bfintarr_amt - (ldc_rkeepint - ldc_nkeepint);
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
                of_calbankfee(moneytype_code);
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
                    sql = @"SELECT SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE,   SLUCFSLIPITEMTYPE.SLIPITEMTYPE_DESC
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
            else if (eventArg == PostRetrieveBranch)
            {
                dsMain.DdBranchLike(dsMain.DATA[0].EXPENSE_BANK, dsMain.DATA[0].txt_branch_name);
            }
            else if (eventArg == PostMember)
            {
                dsMain.DATA[0].MEMBER_NO = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                RetrieveMemberName(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveLnreqloan(dsMain.DATA[0].MEMBER_NO);
            }
            else if (eventArg == PostLoancontract)
            {
                of_initloanrcv();
            }
        }

        void WebSheet.SaveWebSheet()
        {
            try
            {
                payavd_flag = Convert.ToDecimal(HdPayavd.Value);
                if (payavd_flag == 0)
                {
                    //เช็คยอดขอกู้ ยอดหักชำระ
                    decimal payoutnet_amt = dsMain.DATA[0].PAYOUTNET_AMT;
                    if (payoutnet_amt >= 0)
                    {
                        Boolean lbl_fin = of_checkfin();
                        if (lbl_fin == false) { return; }
                    }
                    else
                    {
                        chkpayoutnet_amt = payoutnet_amt;
                        this.SetOnLoadedScript("alert('ยอดจ่ายเงินกู้น้อยกว่ายอดหักชำระ กรุณาตรวจสอบ!!')");
                        return;
                    }

                    string coop_id = state.SsCoopControl;
                    ExecuteDataSource exed = new ExecuteDataSource(this);
                    string loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO;
                    string lnrcvfrom_code = HdLnrcvfrom.Value;

                    str_slippayout strPayOut = new str_slippayout();
                    strPayOut.entry_id = state.SsUsername;
                    strPayOut.xml_sliphead = dsMain.ExportXml();
                    strPayOut.xml_slipcutlon = dsDetail.ExportXml();
                    strPayOut.xml_slipcutetc = dsAdd.ExportXml();

                    wcf.NShrlon.of_saveslip_lnrcv(state.SsWsPass, ref strPayOut);
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");

                    decimal print = dsMain.DATA[0].PRINT;
                    if (print == 1)
                    {
                        // สะสม slip ใบจ่าย
                        slip_no = strPayOut.payoutslip_no;
                        Hdslpayout.Value += "," + slip_no;

                        // สะสม slip ใบเสร็จ
                        slip_no = strPayOut.payinslip_no;
                        if (!(string.IsNullOrEmpty(slip_no)))
                        {
                            Hdslpayin.Value += "," + slip_no;
                            // ตรวจสอบว่าพิมพ์ใบเสร็จรับชำระแบบไหน
                            string[] reportobj = WebUtil.GetIreportObjPrintLoan();
                            string printslip_type = reportobj[0];
                            // ถ้าเป็นการพิมพ์เลย
                            if (printslip_type == "PBA")
                            {
                                Printing.PrintSlipSlpayin(this, slip_no, state.SsCoopControl);
                            }
                        }
                    }
                    ResetPage();
                }
                else
                {
                    throw new Exception("ไม่สามารถบันทึกการจ่ายเงินกู้ได้");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        void WebSheet.WebSheetLoadEnd()
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
        }

        #region function
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
                string message_service = "";
                string loanContractNo = dsMain.DATA[0].LOANCONTRACT_NO; ;
                string lnrcvfrom_code = "CON";
                string coopId = state.SsCoopId;

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

                message_service = sSlipPayOut.message_service;

                dsMain.DATA[0].cp_activedate = idtm_activedate;
                dsMain.DATA[0].cp_lastdate = idtm_lastDate;

                //เช็คยอดจ่ายเงินกู้ ว่าน้อยกว่ายอดหักหรือไม่
                decimal ldc_payoutnet = dsMain.DATA[0].PAYOUTNET_AMT;
                if (ldc_payoutnet < 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ยอดหักชำระมากกว่ายอดจ่ายเงินกู้ กรุณาตรวจสอบ");
                }
                if (message_service != null && message_service != "")
                {
                    LtServerMessage.Text = WebUtil.WarningMessage2(message_service);
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

        /// <summary>
        /// คำนวณดอกเบี้ยใหม่
        /// </summary>
        private void of_initrecalint()
        {

            string loan = loans[currentLoan];

            int fixCode = 0;
            fixCode = loan.IndexOf("@");
            string loanFix = loan.Substring(fixCode + 1);
            string coopId = state.SsCoopId;

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
        /// ตรวจสอบการเงิน
        /// </summary>
        private Boolean of_checkfin()
        {
            string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
            string sqlStr = "";
            decimal allpay_atfin = 0;
            Sdt dt;
            if (moneytype_code == "CSH")
            {
                //เช็คว่าวันจ่ายเงินกู้เป็นวันเดียวกันกับวันทำการหรือไม่
                idtm_activedate = dsMain.DATA[0].cp_activedate;

                //เช็คว่าต้องการตรวจสอบการเงินหรือไม่
                sqlStr = @"select allpay_atfin from finconstant where coop_id = '" + state.SsCoopControl + "'";
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    allpay_atfin = dt.GetInt32("allpay_atfin");
                }

                if (allpay_atfin == 1)
                {
                    //เช็คลิ้นชักการเงิน
                    try
                    {
                        sqlStr = @"select status from fintableusermaster where user_name = {0} and opdatework = {1}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsUsername, state.SsWorkDate);
                        dt = WebUtil.QuerySdt(sqlStr);
                        if (dt.GetRowCount() <= 0)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ผู้ทำรายกายยังไม่ได้เปิดลิ้นชัก หรือค่าคงที่ตั้งผิด ");
                        }
                        if (dt.Next())
                        {
                            int status = dt.GetInt32("status");
                            if (status == 14)
                            {
                                this.SetOnLoadedScript("alert('ไม่สามารถทำรายการได้เนื่องจากมีการปิดลิ้นชักไปแล้วของ " + state.SsUsername + "')");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ผู้ทำรายกายยังไม่ได้เปิดลิ้นชัก หรือค่าคงที่ตั้งผิด ");
                            this.SetOnLoadedScript("alert('ผู้ทำรายกายยังไม่ได้เปิดลิ้นชัก " + state.SsUsername + "')");
                            return true;
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
            }
            return true;
        }

        /// <summary>
        /// set คู่บัญชี
        /// </summary> 
        private void of_setdefaulttofromaccid()
        {
            try
            {
                string sliptype_code = "LWD";
                string sql = @"select account_id
                    from cmucftofromaccid 
                    where coop_id={0} and 
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
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        /// <summary>
        /// ดึงสาขาธนาคาร
        /// </summary> 
        public void of_setbankbranh(String moneytype_code)
        {
            try
            {
                string sql1 = "";
                dsMain.FindDropDownList(0, dsMain.DATA.EXPENSE_BANKColumn).Enabled = true;
                if (moneytype_code == "CBT" || moneytype_code == "CBO")
                {
                    string expense_bank = dsMain.DATA[0].EXPENSE_BANK;
                    string expense_branch = dsMain.DATA[0].EXPENSE_BRANCH;
                    if (expense_branch == "")
                    {
                        sql1 = @"select expense_bank, expense_branch, expense_accid from lnreqloan where coop_id = {0} and member_no = {1}";
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
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
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

        public void RetrieveMemberName(string memberNo)
        {
            try
            {
                string sql = @"select mb.member_no, mp.prename_desc, mb.memb_name, mb.memb_surname, mb.member_type, 
                    trim(mb.membgroup_code) as membgroup_code, FT_MEMGRP(mb.coop_id, mb.membgroup_code) as membgroup_desc
                    from mbmembmaster mb, mbucfprename mp
                    where mb.coop_id = {0} 
                    and mb.member_no = {1}
                    and mb.prename_code = mp.prename_code";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memberNo);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsMain.DATA[0].PRENAME_DESC = dt.GetString("prename_desc");
                    dsMain.DATA[0].MEMB_NAME = dt.GetString("memb_name");
                    dsMain.DATA[0].MEMB_SURNAME = dt.GetString("memb_surname");
                    dsMain.DATA[0].MEMBER_TYPE = dt.GetDecimal("member_type");
                    dsMain.DATA[0].MEMBGROUP_CODE = dt.GetString("membgroup_code");
                    dsMain.DATA[0].MEMBGROUP_DESC = dt.GetString("membgroup_desc");
                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void ResetPage()
        {
            dsMain.ResetRow();
            dsDetail.ResetRow();
            dsAdd.ResetRow();
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
            dsAdd.InitDsAdd(this);
            of_activeworkdate();
            idtm_activedate = dsMain.DATA[0].cp_activedate;
            dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
            dsMain.DATA[0].SLIP_DATE = idtm_activedate;
            if (idtm_activedate != state.SsWorkDate)
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ระบบได้ทำการปิดสิ้นวันแล้ว ระบบจะทำการเปลี่ยนวันที่เป็น " + idtm_activedate.ToString("dd/MM/yyyy", th));
            }
            dsMain.FindDropDownList(0, dsMain.DATA.LNPAYMENT_STATUSColumn).Enabled = false;
        }

        #endregion

    }
}