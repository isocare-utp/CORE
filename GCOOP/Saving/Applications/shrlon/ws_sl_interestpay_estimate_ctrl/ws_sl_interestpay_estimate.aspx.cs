using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_interestpay_estimate_ctrl
{
    public partial class ws_sl_interestpay_estimate : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostOperateFlag { get; set; }
        [JsPostBack]
        public string PostOperateDate { get; set; }
        [JsPostBack]
        public string Saveandprint { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                of_initslippayin();
            }
            else if (eventArg == Saveandprint)
            {
                string sql = @"delete from tempslippayin where entry_id = {0}";
                sql = WebUtil.SQLFormat(sql, state.SsUsername);
                Sdt dt = WebUtil.QuerySdt(sql);

                for (int i = 0; i < dsDetail.RowCount; i++)
                {
                    string coop_id = state.SsCoopId;
                    string member_no = dsMain.DATA[0].MEMBER_NO;
                    DateTime operate_date = dsMain.DATA[0].OPERATE_DATE;
                    decimal sharestk_value = dsMain.DATA[0].SHARESTK_VALUE;
                    decimal slip_amt = dsMain.DATA[0].SLIP_AMT;
                    string loancontract_no = dsDetail.DATA[i].LOANCONTRACT_NO;
                    decimal bfshrcont_balamt = dsDetail.DATA[i].BFSHRCONT_BALAMT;
                    DateTime bflastcalint_date = dsDetail.DATA[i].BFLASTCALINT_DATE;
                    decimal interest_amt = dsDetail.DATA[i].COMPUTE1;
                    decimal principal_payamt = dsDetail.DATA[i].PRINCIPAL_PAYAMT;
                    decimal interest_payamt = dsDetail.DATA[i].INTEREST_PAYAMT;
                    decimal item_payamt = dsDetail.DATA[i].ITEM_PAYAMT;
                    decimal item_balance = dsDetail.DATA[i].ITEM_BALANCE;
                    string entry_id = state.SsUsername;

                    try
                    {
                        string sql1 = @"insert into tempslippayin(coop_id,member_no,operate_date,sharestk_value,slip_amt,loancontract_no,bfshrcont_balamt,bflastcalint_date,interest_amt,principal_payamt,interest_payamt,item_payamt,item_balance,entry_id) 
                                values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})";
                        sql1 = WebUtil.SQLFormat(sql1, coop_id, member_no, operate_date, sharestk_value, slip_amt, loancontract_no, bfshrcont_balamt, bflastcalint_date, interest_amt, principal_payamt, interest_payamt, item_payamt, item_balance, entry_id);
                        Sdt dt1 = WebUtil.QuerySdt(sql1);
                    }
                    catch
                    {
                    }
                }
                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                args.Add("as_entryid", iReportArgumentType.String, state.SsUsername);
                iReportBuider report = new iReportBuider(this, "กำลังสร้างใบประมาณการดอกเบี้ย");
                report.AddCriteria("r_sl_interest_estimate", "ใบประมาณการดอกเบี้ย", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();
            }
            else if (eventArg == PostOperateFlag)
            {
                int row = dsDetail.GetRowFocus();
                decimal operate_flag = dsDetail.DATA[row].OPERATE_FLAG;
                decimal principal_payamt = dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                decimal interest_payamt = dsDetail.DATA[row].INTEREST_PAYAMT;
                decimal item_payamt = dsDetail.DATA[row].ITEM_PAYAMT;
                decimal item_balance = dsDetail.DATA[row].ITEM_BALANCE;
                decimal bfshrcont_balamt = dsDetail.DATA[row].BFSHRCONT_BALAMT;

                if (operate_flag == 1)
                {
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = bfshrcont_balamt;
                    dsDetail.DATA[row].INTEREST_PAYAMT = dsDetail.DATA[row].COMPUTE1;
                    dsDetail.DATA[row].ITEM_PAYAMT = dsDetail.DATA[row].PRINCIPAL_PAYAMT + dsDetail.DATA[row].INTEREST_PAYAMT;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                    calItemPay();
                }
                else if (operate_flag == 0)
                {
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = 0;
                    dsDetail.DATA[row].ITEM_PAYAMT = 0;
                    dsDetail.DATA[row].INTEREST_PAYAMT = 0;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                    calItemPay();
                }
            }
            else if (eventArg == PostOperateDate)
            {
                DateTime operate_date = dsMain.DATA[0].OPERATE_DATE;
                string xml_sliplon = dsDetail.ExportXml();


                try
                {

                    long ll_return = wcf.NShrlon.of_initslippayin_calint(state.SsWsPass, ref xml_sliplon, "PX", operate_date);

                    // dsMain.ResetRow();
                    dsDetail.ResetRow();
                    dsDetail.ImportData(xml_sliplon);

                    int row = dsDetail.GetRowFocus();
                    decimal bfshrcont_balamt = dsDetail.DATA[row].BFSHRCONT_BALAMT;
                    dsDetail.DATA[row].PRINCIPAL_PAYAMT = bfshrcont_balamt;
                    dsDetail.DATA[row].INTEREST_PAYAMT = dsDetail.DATA[row].COMPUTE1;
                    dsDetail.DATA[row].ITEM_PAYAMT = dsDetail.DATA[row].PRINCIPAL_PAYAMT + dsDetail.DATA[row].INTEREST_PAYAMT;
                    dsDetail.DATA[row].ITEM_BALANCE = bfshrcont_balamt - dsDetail.DATA[row].PRINCIPAL_PAYAMT;
                    calItemPay();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            for (int i = 0; i < dsDetail.RowCount; i++)
            {
                if (dsDetail.DATA[i].OPERATE_FLAG == 1)
                {
                    dsDetail.FindTextBox(i, dsDetail.DATA.LOANCONTRACT_NOColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFSHRCONT_BALAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.BFLASTCALINT_DATEColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.COMPUTE1Column).ReadOnly = true;
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
                    dsDetail.FindTextBox(i, dsDetail.DATA.COMPUTE1Column).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.PRINCIPAL_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.INTEREST_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_PAYAMTColumn).ReadOnly = true;
                    dsDetail.FindTextBox(i, dsDetail.DATA.ITEM_BALANCEColumn).ReadOnly = true;

                }
            }
        }

        public void calItemPay()
        {
            int row = dsDetail.RowCount;


            decimal slip_amt = 0;
            for (int i = 0; i < row; i++)
            {
                decimal item_payamt = dsDetail.DATA[i].ITEM_PAYAMT;
                slip_amt += item_payamt;
            }

            dsMain.DATA[0].SLIP_AMT = slip_amt;

        }

        private void of_initslippayin()
        {
            try
            {
                string member_no = dsMain.DATA[0].MEMBER_NO;
                string mem_no = String.Format("{0:00000000}", Convert.ToDecimal(member_no));

                str_slippayin slip = new str_slippayin();
                slip.member_no = mem_no;
                slip.slip_date = dsMain.DATA[0].SLIP_DATE;
                slip.operate_date = dsMain.DATA[0].OPERATE_DATE;
                slip.sliptype_code = "px";
                slip.memcoop_id = state.SsCoopControl;

                wcf.NShrlon.of_initslippayin(state.SsWsPass, ref slip);
                dsMain.ImportData(slip.xml_sliphead);


                try
                {
                    dsDetail.ImportData(slip.xml_sliplon);

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }


            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }
    }
}