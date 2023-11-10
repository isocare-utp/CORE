using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_receipandpay_cash_daily_ctrl
{
    public partial class ws_sl_receipandpay_cash_daily : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostBack { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsReceipt.InitDsReceipt(this);
            dsPayloan.InitDsPayloan(this);
            dsShare.InitDsShare(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].entry_date = state.SsWorkDate;
                dsMain.DdEntryId();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostBack)
            {
                try
                {
                    DateTime entry_date = dsMain.DATA[0].entry_date;
                    string entry_id = dsMain.DATA[0].entry_id;
                    decimal allentry_flag = dsMain.DATA[0].allentry_flag;
                    if (allentry_flag == 0)
                    {
                        entry_id = "%";
                        dsReceipt.RetrieveReceipt(entry_date, entry_id);
                        dsPayloan.RetrievePayloan(entry_date, entry_id);
                        dsShare.RetrieveShare(entry_date, entry_id);
                        SumTotal();
                    }
                    else
                    {
                        dsReceipt.RetrieveReceipt(entry_date, entry_id);
                        dsPayloan.RetrievePayloan(entry_date, entry_id);
                        dsShare.RetrieveShare(entry_date, entry_id);
                        SumTotal();
                    }
                }
                catch (Exception ex)
                {


                }

            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
        public void SumTotal()
        {
            //คิดผลรวมจำนวน,เงินสด,ดอกเบี้ย รับชำระ
            int row_count = dsReceipt.RowCount;
            decimal cp_sum_count_item = 0;
            decimal cp_sum_allreceiptprnc = 0;
            decimal cp_sum_allreceiptint = 0;
            decimal sumreceipt_loan = 0;
            for (int i = 0; i < row_count; i++)
            {
                decimal count_item = dsReceipt.DATA[i].count_item;
                cp_sum_count_item += count_item;

                decimal sum_prnamt = dsReceipt.DATA[i].sum_prnamt;
                cp_sum_allreceiptprnc += sum_prnamt;

                decimal sum_intamt = dsReceipt.DATA[i].sum_intamt;
                cp_sum_allreceiptint += sum_intamt;

                sumreceipt_loan = cp_sum_allreceiptprnc + cp_sum_allreceiptint;

            }
            int row = dsShare.RowCount;
            decimal sum_share = 0;
            for (int j = 0; j < row; j++)
            {
                decimal sum_prinamt = dsShare.DATA[j].sum_prinamt;
                sum_share += sum_prinamt;
            }

            dsMain.DATA[0].sumreceipt_loan = sumreceipt_loan + sum_share;
            dsReceipt.CountItem.Text = cp_sum_count_item.ToString("#,##0");
            dsReceipt.Allreceiptprnc.Text = cp_sum_allreceiptprnc.ToString("#,##0.00");
            dsReceipt.Allreceiptint.Text = cp_sum_allreceiptint.ToString("#,##0.00");

            //คิดผลรวมจำนวน,เงินต้น จ่าย
            int row_count2 = dsPayloan.RowCount;
            decimal cp_sum_count_receiptall = 0;
            decimal cp_sum_allreceiptprnc2 = 0;

            for (int i = 0; i < row_count2; i++)
            {
                decimal count_slip = dsPayloan.DATA[i].count_item;
                cp_sum_count_receiptall += count_slip;

                decimal sum_item = dsPayloan.DATA[i].sum_prnamt;
                cp_sum_allreceiptprnc2 += sum_item;
            }
            dsMain.DATA[0].sumpay_loan = cp_sum_allreceiptprnc2;
            dsPayloan.CountReceiptall.Text = cp_sum_count_receiptall.ToString("#,##0");
            dsPayloan.Allreceiptprnc.Text = cp_sum_allreceiptprnc2.ToString("#,##0.00");
        }
    }
}