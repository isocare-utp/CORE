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
using DataLibrary;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNAccount;

namespace Saving.Criteria
{
    public partial class u_cri_coopid_rdate_begin_amt : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        private n_accountClient accService;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
            this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            //DwUtil.RetrieveDDDW(dw_criteria, "coop_id", "criteria.pbl", state.SsCoopControl);
            accService = wcf.NAccount;
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);

                dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                //dw_criteria.SetItemString(1, "coop_id", state.SsCoopId);
                //          dw_criteria.SetItemString(1, "coop_control", state.SsCoopControl);

                tdw_criteria.Eng2ThaiAllRow();
            }

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
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            //            try
            //            {
            //                Sta ta2 = new Sta(state.SsConnectionString);
            //                String sql2 = "";
            //                sql2 = @"sum(dr_amt), sum(cr_amt) from vcvoucherdet where account_id = '11010100' and voucher_no in(
            //                select voucher_no from vcvoucher where voucher_date >= to_date('01012012','ddmmyyyy') and voucher_date <= to_date('15102012','ddmmyyyy') and voucher_status = 1)";
            //                Sdt dt2 = ta2.Query(sql2);
            //                String dr_amt = dt2.Rows[0]["sum(dr_amt)"].ToString();
            //                String cr_amt = dt2.Rows[0]["sum(cr_amt)"].ToString();
            //                ta2.Close();
            //            }
            //            catch
            //            {
            //                ReportName.Text = "[" + rid + "]";
            //            }

            //Link back to the report menu.
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "runProcess")
            {
                RunProcess();
            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            String coop_name = state.SsCoopName;
            String coop_id = state.SsCoopControl;
            String wsPass = state.SsWsPass;
            DateTime date = dw_criteria.GetItemDateTime(1, "start_date");   
            DateTime period_end_date = state.SsWorkDate;
            DateTime ending_of_account = state.SsWorkDate;
            int accyear = 0;
            int accperiod = 0;
            Decimal ldc_dr_forward = 0, ldc_cr_forward = 0, ldc_total = 0;
            String acccash_id = "";

            short year = 0;
            short period = 0;
            short result = accService.of_get_year_period(state.SsWsPass, date, state.SsCoopId, ref year, ref period);

            Sta ta = new Sta(sqlca.ConnectionString);
            //หารหัสบัญชีเงินสด

            String sql4 = @"select  cash_account_code , ending_of_account from accconstant  where  coop_id ='" + state.SsCoopId + "'";
     

            Sdt dt4 = ta.Query(sql4);
            if (dt4.Next())
            {
                acccash_id = dt4.GetString("cash_account_code");
                ending_of_account = DateTime.Parse(dt4.GetString("ending_of_account"));
            }
            else
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบรหัสบัญชีเงินสด ");
                sqlca.Rollback();
            }

            if (year == 0 || period == 0)
            {
                result = accService.of_get_year_period(state.SsWsPass, ending_of_account, state.SsCoopId, ref year, ref period);
            }



            //หาเดือนล่าสุดที่มีการปิดสิ้นเดือน
            String sql = @"select  max(account_year) as acccount_year , max(period) as period from accperiod  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + year + "' and close_flag = 1 ";

            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                accyear = int.Parse(dt.GetString("acccount_year"));
                accperiod = int.Parse(dt.GetString("period"));
                //period_end_date = DateTime.Parse(dt.GetString("period_end_date"));

            }
            else
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลปีของปีบัญชี ");
                sqlca.Rollback();
            }
            //ta.Close();

            //หายอดยกมา
            if (accperiod >= period)  
            {
                int bf_period = 0;
                bf_period = period - 1;
                String sql2 = @"select  forward_dr_amount , forward_cr_amount from accsumledgerperiod  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + year + "' and period = '" + bf_period + "' and account_id = '" + acccash_id  + "'";

                Sdt dt2 = ta.Query(sql2);
                if (dt2.Next())
                {
                    ldc_dr_forward = dt2.GetDecimal("forward_dr_amount");
                    ldc_cr_forward = dt2.GetDecimal("forward_cr_amount");
                    ldc_total      = ldc_dr_forward - ldc_cr_forward;

                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบยอดเงินสดยกมา ");
                    sqlca.Rollback();
                }

                //หาวันที่สิ้นงวด
                String sql3 = @"select period_end_date from accperiod  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + year + "' and period = '" + bf_period + "' ";
                Sdt dt3 = ta.Query(sql3);
                if (dt3.Next())
                {
                    period_end_date = DateTime.Parse(dt3.GetDateEn("period_end_date"));

                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบวันที่สิ้นงวดบัญชี ");
                    sqlca.Rollback();
                }

            }
            else
            {
                int bf_period = 0;
                bf_period = period - 1;
                String sql2 = @"select  forward_dr_amount , forward_cr_amount from accsumledgerperiod  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + year + "' and period = '" + period + "' and account_id = '" + acccash_id + "' ";

                Sdt dt2 = ta.Query(sql2);
                if (dt2.Next())
                {
                    ldc_dr_forward = dt2.GetDecimal("forward_dr_amount");
                    ldc_cr_forward = dt2.GetDecimal("forward_cr_amount");
                    ldc_total = ldc_dr_forward - ldc_cr_forward;

                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบยอดเงินสดยกมา ");
                    sqlca.Rollback();
                }

                //หาวันที่สิ้นงวด
                String sql3 = @"select period_end_date from accperiod  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + year + "' and period = '" + period + "' ";
                Sdt dt3 = ta.Query(sql3);
                if (dt3.Next())
                {
                    period_end_date = DateTime.Parse(dt3.GetDateEn("period_end_date"));
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบวันที่สิ้นงวดบัญชี ");
                    sqlca.Rollback();
                }
            }

            ta.Close();

            //Decimal[] CashBeginForward = accService.GetCashBeginForward(wsPass, date, state.SsCoopControl);
            //String lbl_moneybg = CashBeginForward[0].ToString("#,##0.00");
            //String lbl_moneyfw = CashBeginForward[1].ToString("#,##0.00");

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.

            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(period_end_date.ToString(), ArgumentType.DateTime);
            lnv_helper.AddArgument(ldc_total.ToString(), ArgumentType.String);
            lnv_helper.AddArgument(acccash_id, ArgumentType.String);
            //----------------------------------------------------


            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                //String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                //String li_return = lws_report.Run(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }

        }
        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }
        #endregion

    }
}
