using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saving.WcfShrlon;
using DataLibrary;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl.w_dlg_loan_receive_order_ctrl
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

        string[] loans;//Request["loans"].Split(',');

        DateTime lastDate;
        int currentLoan = 0;


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

                // loans2 = Request["loans"].Split('@');

                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;

                InitLoan();
                dsMain.DdBankDesc();
                dsMain.DdFromAccId();
                HdIndex.Value = currentLoan + "";

            }
            else
            {
                lastDate = dsMain.DATA[0].OPERATE_DATE;
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
                    dsMain.DATA[0].PAYOUTCLR_AMT = 0;
                    dsMain.DATA[0].PAYOUTNET_AMT = payout_amt - 0;
                    // dsDetail.DATA[row].BFINTARR_AMT = 0;

                }


            }
            else if (eventArg == PostItem)
            {
                calItemPay();
            }
            else if (eventArg == PostOperateFlagAdd)
            {

            }
            else if (eventArg == PostSlipItem)
            {
                int row = dsAdd.GetRowFocus();
                string slipitemtype_code = dsAdd.DATA[row].SLIPITEMTYPE_CODE;
                //dsAdd.ItemType(slipitemtype_code);
                string sql = @" 
                 SELECT SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE,   SLUCFSLIPITEMTYPE.SLIPITEMTYPE_DESC
                 FROM SLUCFSLIPITEMTYPE  
                 WHERE ( slucfslipitemtype.manual_flag = 1 ) and  SLUCFSLIPITEMTYPE.SLIPITEMTYPE_CODE={0}";
                sql = WebUtil.SQLFormat(sql, slipitemtype_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    dsAdd.DATA[row].SLIPITEM_DESC = dt.GetString("SLIPITEMTYPE_DESC");
                }
            }


            else if (eventArg == PostBank)
            {
                string bank_code = dsMain.DATA[0].BANK_CODE;
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
            else if (eventArg == PostCancel)
            {
                NextLoan();
            }
            else if (eventArg == PostSave)
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
                    //strPayOut.slip_date = state.SsWorkDate;
                    //strPayOut.operate_date = dsMain.DATA[0].OPERATE_DATE; ;
                    //strPayOut.member_no = dsMain.DATA[0].MEMBER_NO;
                    strPayOut.entry_id = state.SsUsername;
                    //strPayOut.loancontract_no = loancontract_no;
                    //strPayOut.initfrom_type = lnrcvfrom_code;
                    strPayOut.xml_sliphead = dsMain.ExportXml();
                    strPayOut.xml_slipcutlon = dsDetail.ExportXml();
                    strPayOut.xml_slipcutetc = dsAdd.ExportXml();


                    try
                    {
                        int result = wcf.Shrlon.of_saveslip_lnrcv(state.SsWsPass, ref strPayOut);
                        if (result == 1)
                        {
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                        }


                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }

                }
                catch (Exception ex)
                {
                    isNotError = false;
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }

                if (isNotError)
                {
                    SaveNextLoan();
                }
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
                sSlipPayOut.operate_date = state.SsWorkDate;
                sSlipPayOut.loancontract_no = loanContractNo;
                sSlipPayOut.slip_date = state.SsWorkDate;

                String initfrom_type = lnrcvfrom_code;
                sSlipPayOut.initfrom_type = initfrom_type;

                sSlipPayOut = wcf.Shrlon.InitLnRcv(state.SsWsPass, sSlipPayOut);

                dsMain.ImportData(sSlipPayOut.xml_sliphead);
                dsDetail.ImportData(sSlipPayOut.xml_slipcutlon);
                dsAdd.ImportData(sSlipPayOut.xml_slipcutetc);
                dsMain.DdLoanType();
                dsMain.DdMoneyType();
                dsMain.DdBankDesc();
                dsMain.DdFromAccId();


                if (currentLoan == 0)
                {
                    dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                }
                else
                    if (lastDate != null)
                    {
                        dsMain.DATA[0].OPERATE_DATE = lastDate;
                    }

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
                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;
                InitLoan();
                //dsMain.DATA[0].LOANCONTRACT_NO = loans[currentLoan];
                // dsMain.DATA[0].NAME = loans[currentLoan];
            }
            HdIndex.Value = currentLoan + "";
            if ((currentLoan) >= loans.Length)
            {
                this.SetOnLoadedScript("parent.RemoveIFrame()");
            }
        }
        private void SaveNextLoan()
        {
            currentLoan = Convert.ToInt16(HdIndex.Value);
            loans = Request["loans"].Split(',');

            dsMain.ResetRow();
            dsDetail.ResetRow();
            dsAdd.ResetRow();
            currentLoan += 1;
            if (currentLoan < loans.Length)
            {
                lbCurrentLoan.Text = (currentLoan + 1) + "/" + loans.Length;
                InitLoan();
                //dsMain.DATA[0].LOANCONTRACT_NO = loans[currentLoan];
                // dsMain.DATA[0].NAME = loans[currentLoan];
            }
            HdIndex.Value = currentLoan + "";
            if ((currentLoan) > loans.Length)
            {
                this.SetOnLoadedScript("parent.RemoveIFrame()");
            }
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
            dsMain.DATA[0].PAYOUTNET_AMT = dsMain.DATA[0].PAYOUT_AMT - dsMain.DATA[0].PAYOUTCLR_AMT;

        }


    }
}