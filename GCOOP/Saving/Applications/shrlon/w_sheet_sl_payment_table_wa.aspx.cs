using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using System.Globalization;
//using CoreSavingLibrary.WcfShrlon;
using DataLibrary;
using System.Data;


namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_payment_table_wa : PageWebSheet, WebSheet
    {
        protected String jsPostCalculate;
        protected String jsPostReset;
        protected String jsPrintreport;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;

        protected void createNPopupPDF()
        {

            ////เอา XML จากหน้าจอที่แสดงอยู่ มาใส่ใน Report เพื่อเปลี่ยนรูปแบบหน้าตา ก่อนสั่งพิมพ์.
            //String xml = dw_result.Describe("Datawindow.data.XML");

            ////WebDataWindowControl dw_report = new WebDataWindowControl();
            ////DwUtil.ImportData(xml, dw_report, null, FileSaveAsType.Xml);

            ////FileName
            //PrintClient svPrint = wcf.Print;
            //String pdfFile = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            //pdfFile += "_payment_table.pdf";
            //pdfFile = pdfFile.Trim();

            ////Print
            //Int32 li_rv = svPrint.PrintPDF(state.SsWsPass, xml, pdfFile);
            //if (li_rv < 0)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("PrintPDF failed : (" + Convert.ToString(li_rv) + ") returned.");
            //    return;
            //}

            //Popup
            //String pdfURL = svPrint.GetPDFURL(state.SsWsPass) + pdfFile;
            //String pop = "Gcoop.OpenPopup('" + pdfURL + "')";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "PaymentTable", pop, true);


            PopupReport();


        }

       

        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }
        //}
        public void InitJsPostBack()
        {
            jsPostCalculate = WebUtil.JsPostBack(this, "jsPostCalculate");
            jsPostReset = WebUtil.JsPostBack(this, "jsPostReset");
            jsPrintreport = WebUtil.JsPostBack(this, "jsPrintreport");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dw_criteria.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(dw_criteria);
                this.RestoreContextDw(dw_result);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostCalculate")
            {
                PostCalculate();
            }
            else if (eventArg == "jsPostReset")
            {
                PostReset();
            }else if (eventArg == "jsPrintreport")
            {
                JsPrintreport();
            }

            

        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
            dw_result.SaveDataCache();
        }

        public void PostReset()
        {
            dw_criteria.Reset();
            dw_criteria.InsertRow(0);
            dw_result.Reset();
        }

        private void JsContPeriod()
        {

            Decimal loanrequest_amt = dw_criteria.GetItemDecimal(1, "prncbal");
            Decimal loanpayment_type = dw_criteria.GetItemDecimal(1, "loanpayment_type");
            Decimal ldc_intrate = dw_criteria.GetItemDecimal(1, "intrate");
            Decimal period_payamt = dw_criteria.GetItemDecimal(1, "pay_time");
            int li_fixcaltype = Convert.ToInt16(dw_criteria.GetItemDecimal(1, "payment_cbttype"));
            Decimal period_payment = 0;
            ldc_intrate = ldc_intrate / 100;
            // ปัดยอดชำระ
          //  String ll_roundpay = dw_criteria.GetItemDecimal(1, "round_payment");
            int roundpay =Convert.ToInt16( dw_criteria.GetItemDecimal(1, "round_payment"));// Convert.ToInt16(ll_roundpay);
            double ldc_fr = 1, ldc_temp = 1, loapayment_amt = 0;
            if (loanpayment_type == 1)
            {
                //คงต้น
                period_payment = loanrequest_amt / period_payamt;
            }
            else
            {
               
                //คงยอด

                if (li_fixcaltype == 1)
                {
                    // ด/บ ทั้งปี / 12
                    ldc_temp = Math.Log(1 + (Convert.ToDouble(ldc_intrate / 12)));
                    ldc_fr = Math.Exp(-Convert.ToDouble(period_payamt) * ldc_temp);
                    loapayment_amt = (Convert.ToDouble(loanrequest_amt) * (Convert.ToDouble(ldc_intrate) / 12)) / ((1 - ldc_fr));
                }
                else
                {
                    // 30 ด/บ ทั้งปี / 365
                    ldc_temp = Math.Log(1 + (Convert.ToDouble(ldc_intrate * (30) / 365)));
                    ldc_fr = Math.Exp(-Convert.ToDouble(period_payamt) * ldc_temp);
                    loapayment_amt = (Convert.ToDouble(loanrequest_amt) * (Convert.ToDouble(ldc_intrate) / 365)) / ((1 - ldc_fr));
                }

                period_payment = Convert.ToDecimal(loapayment_amt);
            }
            if (roundpay > 0)
            {
                decimal ldc_mod = period_payment % roundpay;
                if (ldc_mod > 0)
                {
                    period_payment = period_payment + (roundpay - ldc_mod);
                }
            }
            dw_criteria.SetItemDecimal(1, "pay_amoung", period_payment);
        }

        public void JsPrintreport() {
            try {
                inserttopaytable();

                
            }catch( Exception ex){
            
            }
            JsPrintIreport();
        }
        public void PostCalculate()
        {
            decimal prncbal = 0, intrate = 0, pay_time = 0;
            try { prncbal = dw_criteria.GetItemDecimal(1, "prncbal"); }
            catch { prncbal = 0; }
            try { intrate = dw_criteria.GetItemDecimal(1, "intrate"); }
            catch { intrate = 0; }
            try { pay_time = dw_criteria.GetItemDecimal(1, "pay_time"); }
            catch { pay_time = 0; }

            JsContPeriod();

            Decimal loanpayment_type = dw_criteria.GetItemDecimal(1, "loanpayment_type");
            Decimal pay_amoung = dw_criteria.GetItemDecimal(1, "pay_amoung");
            //dw_criteria.SetItemDecimal(1, "pay_amoung", (prncbal / pay_time));

            decimal sum = 0, period_pay = pay_amoung;
            decimal old_balance = prncbal;


            for (int i = 1; i <= pay_time; i++)
            {

                decimal int_cal = ((old_balance * 30 * (intrate / 100)) / 365);
                try
                {
                    dw_result.InsertRow(i);
                    dw_result.SetItemDecimal(i, "seq_no", i);
                    dw_result.SetItemDecimal(i, "balance_for", old_balance);

                    dw_result.SetItemDecimal(i, "interest", int_cal);
                    //
                    if (loanpayment_type == 2)
                    {
                        pay_amoung = period_pay - int_cal;
                        dw_result.SetItemDecimal(i, "sum", pay_amoung + int_cal);
                    }
                    else
                    {
                        dw_result.SetItemDecimal(i, "sum", pay_amoung + int_cal);
                    }

                    dw_result.SetItemDecimal(i, "prncbal", pay_amoung);
                    if (old_balance - ((prncbal / pay_time) + int_cal) > 0)
                    {
                        dw_result.SetItemDecimal(i, "balance", old_balance - ((prncbal / pay_time)));
                    }
                    else if (old_balance - ((prncbal / pay_time) + int_cal) <= 0)
                    {
                        dw_result.SetItemDecimal(i, "balance", 0);
                    }
                    sum += ((prncbal / pay_time) + int_cal);
                    old_balance = (old_balance - ((prncbal / pay_time)));
                }
                catch { }
                //dw_result.SetItemDecimal(1, "sum_all", sum);
            }

        }
        private void inserttopaytable()
        {
            string userid = state.SsUsername;
            string ippp = state.SsClientIp;
            String loancontract_no = userid;// +ippp.Substring(1, 4);
            String startcont_tdate =  state.SsWorkDate.ToShortDateString();
            string date_start_cont = "to_date('', 'dd/mm/yyyy')";
            try
            {
                DateTime startcont_date;
                startcont_date = DateTime.ParseExact(startcont_tdate, "ddMMyyyy", WebUtil.TH);
                date_start_cont = "to_date('" + startcont_date.ToString("dd/MM/yyyy", WebUtil.EN) + "', 'dd/mm/yyyy')";
            }
            catch
            {
                date_start_cont = "null";
            }


            String firstpay_period = "";// dw_criteria.GetItemString(1, "firstpay_period");

            string firstpay_date = "to_date('', 'dd/mm/yyyy')";
            try
            {
                DateTime firstpay;
                firstpay = DateTime.ParseExact(firstpay_period, "ddMMyyyy", WebUtil.TH);
                firstpay_date = "to_date('" + firstpay.ToString("dd/MM/yyyy", WebUtil.EN) + "', 'dd/mm/yyyy')";
            }
            catch
            {


                firstpay_date = "null";

            }

            Decimal intrate_amt = dw_criteria.GetItemDecimal(1, "intrate")  ;
            Decimal principal_amt = dw_criteria.GetItemDecimal(1, "prncbal");
            Decimal period_amt = dw_criteria.GetItemDecimal(1, "pay_amoung");
            Decimal installment = dw_criteria.GetItemDecimal(1, "pay_time");

            Sta ta2 = new Sta(state.SsConnectionString);
            try
            { ta2.Exe("Delete from paytable"); }
            catch
            { }
            ta2.Close();
            Sta ta3 = new Sta(state.SsConnectionString);
            try
            { ta3.Exe("Delete from paytabledet"); }
            catch
            { }
            ta3.Close();
            Sta ta = new Sta(state.SsConnectionString);
            try
            { ta.Exe("insert into paytable ( loancontract_no,loanreqfixscv_date,firstpay_date,interest_amt,loanrequest_amt,loanperiod_amt,loanall_period) values ('" + loancontract_no + "'," + date_start_cont + "," + firstpay_date + "," + intrate_amt + "," + principal_amt + "," + period_amt + "," + installment + ")"); }
            catch
            { }
            ta.Close();
            Sta ta4 = new Sta(state.SsConnectionString);
            //ดึงค่าจาก dw_result
            for (int i = 1; i <= dw_result.RowCount; i++)
            {
                Decimal seq_no = dw_result.GetItemDecimal(i, "seq_no");

                // String payment_tdate = dw_result.GetItemString(1, "payment_date");
                string payment_tdatee = "to_date('', 'dd/mm/yyyy')";
                try
                {
                    DateTime payment_date = dw_result.GetItemDateTime(i, "payment_date"); ;
                    //payment_date = DateTime.ParseExact(payment_tdate, "ddMMyyyy", WebUtil.TH);
                    payment_tdatee = "to_date('" + payment_date.ToString("dd/MM/yyyy", WebUtil.EN) + "', 'dd/mm/yyyy')";
                }
                catch
                {
                    payment_tdatee = "null";
                }



                Decimal prinpay_amt = dw_result.GetItemDecimal(i, "prncbal");

                Decimal intpay_amt = dw_result.GetItemDecimal(i, "interest");

                Decimal balance_amt = dw_result.GetItemDecimal(i, "balance");
                String sol = "55555";

               
                try
                { ta4.Exe("insert into paytabledet ( loan_id,loancontract_no,loanperiod,loanfixscv_date,itempay_amt,interest_amt,principal_amt,sol) values (" + i + ",'" + loancontract_no + "'," + seq_no + "," + payment_tdatee + "," + prinpay_amt + "," + intpay_amt + "," + balance_amt + ",'" + sol + "')"); }
                catch
                { }
               

            }
            ta4.Close();

        }
        public void JsPrintIreport()
        {
             String LoadDetail = "";
            String LinkDetail = "";
            String szNameReport = "";
            decimal paymentType = dw_criteria.GetItemDecimal(1, "loanpayment_type");
            //String szRepotrNameLike = "";
            LoadDetail = "กำลังสร้างรายงาน ตารางการชำระ ";
            LinkDetail = "เปิดดูรายงาน ตารางการชำระ เพื่อสั่งพิมพ์รายงาน";
            szNameReport = "ir_report_table_payment";
            //szRepotrNameLike = " สัญญาเงินกู้ ";
            try
            {
                iReportArgument args = new iReportArgument();
                args.Add("ai_paytype", iReportArgumentType.Integer, paymentType);//RQ56000514
                iReportBuider report = new iReportBuider(this, LoadDetail);
                report.AddCriteria(szNameReport, LinkDetail, ReportType.pdf, args);
                report.AutoOpenPDF = true;
                String progressId = report.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            /*XmlConfigService xml = new XmlConfigService();
            if (xml.ShrlonPrintMode == 0)
            {
                //JspopupLoanReport();
            }
            else
            {
                // LtServerMessage.Text = WebUtil.ErrorMessage("อยู่ระหว่างรอ เพิ่มเติม Process");
                JspopupLoanCollReport(true, xml.ShrlonPrintMode);
            }//*/
        }
        private void RunProcess()
        {

            // --- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                //gid = Request["gid"].ToString();
                gid = "LN_NORM";
            }
            catch { }
            try
            {
                //rid = Request["rid"].ToString();
                rid = "LN_NORM0003";
            }
            catch { }

            String doc_no = "";





            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(doc_no, ArgumentType.String);


            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF

            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
               // Saving.WcfReport.ReportClient lws_report = wcf.Report;
                //String criteriaXML = lnv_helper.PopArgumentsXML();
                ////this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                //String li_return = lws_report.Run(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //    HdcheckPdf.Value = "True";

                //}
                //else if (li_return != "true")
                //{
                //    HdcheckPdf.Value = "False";
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            Session["pdf"] = pdf;
            //PopupReport();


        }
    }
}
