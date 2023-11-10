using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
//using CoreSavingLibrary.WcfReport;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_estimated_payments_ctrl
{
    public partial class ws_sl_estimated_payments : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostGen { get; set; }
        [JsPostBack]
        public String PostTypePayment { get; set; }
        [JsPostBack]
        public String PostPrint { get; set; }
        [JsPostBack]
        public String PostLoantype { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsMain.RetrieveMain();
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            
            if (!IsPostBack)
            {

                string sql = "select max(recv_period) as recv_period  from kptempreceive";
                Sdt dt = WebUtil.QuerySdt( sql);
                if( dt.Next()) {
                    string recvperiod = dt.GetString("recv_period");
                    dsMain.DATA[0].STARTPAY_PERIOD =  recvperiod;
                }
                dsMain.DATA[0].COOP_ID = state.SsCoopControl;
                dsMain.DATA[0].an_year = 365;
               

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostTypePayment)
            {

                if (dsMain.DATA[0].PAYMENT_TYPE == 0)
                {
                    dsDetail.ResetRow();
                    decimal PRINCIPAL_BALANCE = dsMain.DATA[0].PRINCIPAL_BALANCE;
                    decimal PERIOD_INSTALLMENT = dsMain.DATA[0].PERIOD_INSTALLMENT;

                    dsMain.DATA[0].PERIOD_PAYMENT = Math.Ceiling((PRINCIPAL_BALANCE / PERIOD_INSTALLMENT)/10)*10;
                }
                else if (dsMain.DATA[0].PAYMENT_TYPE == 1)
                {
                    dsDetail.ResetRow();
                    decimal PRINCIPAL_BALANCE = dsMain.DATA[0].PRINCIPAL_BALANCE;
                    decimal CONTINT_RATE = dsMain.DATA[0].CONTINT_RATE;
                    decimal PERIOD_INSTALLMENT = dsMain.DATA[0].PERIOD_INSTALLMENT;
                    decimal INT_RATE = (CONTINT_RATE / 100);
                    double d = Convert.ToDouble((1 + (INT_RATE / 12)));
                    double fr = Math.Exp(Convert.ToDouble(PERIOD_INSTALLMENT * (-1)) * Math.Log(d));
                    decimal a = PRINCIPAL_BALANCE * (INT_RATE / 12) / (1 - Convert.ToDecimal(fr));
                    dsMain.DATA[0].PERIOD_PAYMENT = Math.Ceiling(a/10)*10;

                }

            }
            else if (eventArg == PostGen)
            {
                dsDetail.ResetRow();

                GenTable();
                SumTotal();
            }
            else if (eventArg == PostPrint)
            {
                Printing();
            }
            else if (eventArg == PostLoantype)
            {
                string sql = @"select lnloantype.loantype_code,lnloantype.loantype_desc,interest_rate
from lnloantype,lncfloanintratedet
where lnloantype.coop_id = lncfloanintratedet.coop_id
and lnloantype.inttabrate_code = lncfloanintratedet.loanintrate_code
and lnloantype.coop_id = {0}
and lnloantype.loantype_code = {2}
and {1}between lncfloanintratedet.effective_date and lncfloanintratedet.expire_date";
                sql = WebUtil.SQLFormat(sql,state.SsCoopControl,state.SsWorkDate,dsMain.DATA[0].loantype_code);
                DataTable dt = WebUtil.Query(sql);

                dsMain.DATA[0].CONTINT_RATE = Convert.ToDecimal(dt.Rows[0]["interest_rate"]);
            }
        }
        public void Printing()
        {
            iReportArgument args = new iReportArgument();
            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
            args.Add("as_entryid", iReportArgumentType.String, state.SsUsername);
            iReportBuider report = new iReportBuider(this, "กำลังสร้างตารางการชำระเงิน");
            report.AddCriteria("r_sl_genestimate", "ตารางการชำระเงิน", ReportType.pdf, args);
            report.AutoOpenPDF = true;
            report.Retrieve();
        }
        public static DateTime NextM(DateTime dateT)
        {
            if (dateT.Day != DateTime.DaysInMonth(dateT.Year, dateT.Month))
            {
                dateT = dateT.AddMonths(1);
                return new DateTime(dateT.Year, dateT.Month, DateTime.DaysInMonth(dateT.Year, dateT.Month));
            }
            else
            {
                dateT = dateT.AddDays(1).AddMonths(1).AddDays(-1);
                return new DateTime(dateT.Year, dateT.Month, DateTime.DaysInMonth(dateT.Year, dateT.Month));
            }
        }

        public void GenTable()
        {
            string STARTPAY_PERIOD = dsMain.DATA[0].STARTPAY_PERIOD;
            decimal PERIOD_INSTALLMENT = dsMain.DATA[0].PERIOD_INSTALLMENT;
            decimal PRINCIPAL_BALANCE = dsMain.DATA[0].PRINCIPAL_BALANCE;
            decimal PERIOD_PAYMENT = dsMain.DATA[0].PERIOD_PAYMENT;
            decimal CONTINT_RATE = dsMain.DATA[0].CONTINT_RATE;
            decimal INT_RATE = (CONTINT_RATE / 100);
            decimal prnpay_amt = 0;
            decimal intpay_amt = 0;
            int intday = 0;
            string loantype = dsMain.DATA[0].loantype_code;
            DateTime dateT = DateTime.ParseExact(STARTPAY_PERIOD, "yyyyMM", WebUtil.TH);
            dateT = new DateTime(dateT.Year, dateT.Month, DateTime.DaysInMonth(dateT.Year, dateT.Month));
            decimal an_year = dsMain.DATA[0].an_year;

            for (int i = 1; i <= PERIOD_INSTALLMENT; i++)
            {
                decimal year = 0;
                if (an_year == 0)
                {
                    if ((dateT.Year) % 4 != 0)
                    {
                        year = 365;
                    }
                    else
                    {
                        year = 366;
                    }
                }
                else
                {
                    year = an_year;
                }

                if (dsMain.DATA[0].PAYMENT_TYPE == 0)
                {
                    intday = DateTime.DaysInMonth(dateT.Year, dateT.Month);
                    intpay_amt = (PRINCIPAL_BALANCE * CONTINT_RATE / 100 * intday / year);
                    intpay_amt = Math.Ceiling(intpay_amt / 10) * 10;
                    if (PRINCIPAL_BALANCE >= PERIOD_PAYMENT)
                    {
                        prnpay_amt = PERIOD_PAYMENT;
                    }
                    else
                    {
                        prnpay_amt = PRINCIPAL_BALANCE;
                    }

                    PRINCIPAL_BALANCE = PRINCIPAL_BALANCE - prnpay_amt;
                }
                else if (dsMain.DATA[0].PAYMENT_TYPE == 1)
                {
                    intday = DateTime.DaysInMonth(dateT.Year, dateT.Month);
                    intpay_amt = (PRINCIPAL_BALANCE * INT_RATE * intday / year);
                    //intpay_amt = Math.Truncate(intpay_amt);
                    intpay_amt = Math.Ceiling(intpay_amt);

                    prnpay_amt = PERIOD_PAYMENT - intpay_amt;

                    if (prnpay_amt < 0)
                    {
                        prnpay_amt = 0;
                    }

                    if (i == PERIOD_INSTALLMENT)
                    {
                        if (PRINCIPAL_BALANCE - prnpay_amt < (intpay_amt + prnpay_amt))
                        {
                            prnpay_amt = PRINCIPAL_BALANCE;
                        }
                    }

                    PRINCIPAL_BALANCE = PRINCIPAL_BALANCE - prnpay_amt;
                }

                dsDetail.InsertLastRow();
                dsDetail.DATA[dsDetail.RowCount - 1].SEQ_NO = i;
                dsDetail.DATA[dsDetail.RowCount - 1].PERIOD = i;
                dsDetail.DATA[dsDetail.RowCount - 1].COOP_ID = state.SsCoopControl;
                dsDetail.DATA[dsDetail.RowCount - 1].RECV_DATE = dateT;
                dsDetail.DATA[dsDetail.RowCount - 1].INTPAY_DAY = intday;
                dsDetail.DATA[dsDetail.RowCount - 1].PRNPAY_AMT = prnpay_amt;
                dsDetail.DATA[dsDetail.RowCount - 1].INTPAY_AMT = intpay_amt;
                dsDetail.DATA[dsDetail.RowCount - 1].TOTAL_PERIOD = prnpay_amt + intpay_amt;
                dsDetail.DATA[dsDetail.RowCount - 1].PRNBAL_AMT = PRINCIPAL_BALANCE;
                dsDetail.DATA[dsDetail.RowCount - 1].ENTRY_ID = state.SsUsername;
                dsDetail.DATA[dsDetail.RowCount - 1].LOANTYPE = loantype;
                dsDetail.DATA[dsDetail.RowCount - 1].CONTINT_RATE = INT_RATE;
                dsDetail.DATA[dsDetail.RowCount - 1].TOTAL_PAY = PERIOD_INSTALLMENT;
                dateT = NextM(dateT);
            }
            try
            {
                string sql = @"delete from lngenestimate where entry_id = {0}";
                sql = WebUtil.SQLFormat(sql,state.SsUsername);
                Sdt dt = WebUtil.QuerySdt(sql);

                ExecuteDataSource ex = new ExecuteDataSource(this);
                ex.AddRepeater(dsDetail);
                ex.Execute();
            }
            catch
            {
            }
        }

        public void SumTotal()
        {
            //คิดผลรวมเงินชำระ
            int row_count = dsDetail.RowCount;
            decimal cp_sumprnpayamt = 0, cp_sumintpayamt = 0, cp_sumtotalpay = 0;
            for (int i = 0; i < row_count; i++)
            {
                decimal prnpay_amt = dsDetail.DATA[i].PRNPAY_AMT;
                decimal intpay_amt = dsDetail.DATA[i].INTPAY_AMT;
                decimal total_pay = prnpay_amt + intpay_amt;// dsDetail.DATA[i].TOTAL_PAY;
                cp_sumprnpayamt += prnpay_amt;
                cp_sumintpayamt += intpay_amt;
                cp_sumtotalpay += total_pay;
            }
            dsDetail.prnpay_amt.Text = cp_sumprnpayamt.ToString("#,##0.00");
            dsDetail.intpay_amt.Text = cp_sumintpayamt.ToString("#,##0.00");
            dsDetail.total_pay.Text = cp_sumtotalpay.ToString("#,##0.00");
        }

        //#region Print Report
        //private void Report1st()
        //{

        //    String print_id = "LOAN_BOOK028"; //r_ln_booking_money.srd พิมพ์ใบแจ้งจ่ายเงินกู้
        //    app = state.SsApplication;
        //    gid = "LOAN_BOOK";
        //    rid = print_id;

        //    string branch_id = state.SsCoopId;
        //    string lnpostsend_regno = dsMain.DATA[0].STARTPAY_PERIOD;
        //    ReportHelper lnv_helper = new ReportHelper();
        //    lnv_helper.AddArgument(branch_id, ArgumentType.String);
        //    lnv_helper.AddArgument(lnpostsend_regno, ArgumentType.String);

        //    //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
        //    String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
        //    pdfFileName += "_" + gid + "_" + rid + ".pdf";
        //    pdfFileName = pdfFileName.Trim();
        //    //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
        //    try
        //    {
        //        ReportClient lws_report = wcf.Report;
        //        //String criteriaXML = lnv_helper.PopArgumentsXML();
        //        //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
        //        String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
        //        if (li_return == "true")
        //        {
        //            HdOpenReport.Value = "True";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
        //        return;
        //    }
        //}
        //public void PopupReport()
        //{
        //    //เด้ง Popup ออกรายงานเป็น PDF.
        //    String pop = "Gcoop.OpenPopup('" + pdf + "')";
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        //}
        //#endregion

        public void SaveWebSheet()
        {
            //try
            //{
            //    ExecuteDataSource exed1 = new ExecuteDataSource(this);
            //    dsMain.DATA[0].STARTPAY_PERIOD = dsMain.DATA[0].STARTPAY_PERIOD.Replace("/", "");//ตัด / ออกก่อน save **2556/01
            //    string sql = "delete from LNTEMPTABPAYIN";
            //    exed1.SQL.Add(sql);
            //    exed1.AddFormView(dsMain, ExecuteType.Insert);
            //    dsDetail.ChangeRowStatusInsert();
            //    exed1.AddRepeater(dsDetail);
            //    exed1.Execute();
            //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            //    dsMain.DATA[0].STARTPAY_PERIOD = dsMain.DATA[0].STARTPAY_PERIOD.Insert(4, "/");

            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //}
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}