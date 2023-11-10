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
    public partial class u_cri_coopid_rdate_begin_amt_core : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        private n_accountClient accService;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            //tdw_criteria.Add("end_date", "end_tdate");   //**
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
                //dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);  //**
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
            //String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);  //**
            String coop_name = state.SsCoopName;
            String coop_id = state.SsCoopControl;
            String wsPass = state.SsWsPass;
            DateTime date = dw_criteria.GetItemDateTime(1, "start_date");
            DateTime period_end_date = state.SsWorkDate;
            DateTime ending_of_account = state.SsWorkDate;
            DateTime starting_of_account = state.SsWorkDate;
            int accyear = 0;
            int accperiod = 0;
            Decimal ldc_total = 0;
            String acccash_id = "";

            short year = 0;
            short period = 0;
            short result = accService.of_get_year_period(state.SsWsPass, date, state.SsCoopId, ref year, ref period);

            Sta ta = new Sta(sqlca.ConnectionString);
            //หารหัสบัญชีเงินสด

            String sql4 = @"select  cash_account_code , beginning_of_accou, ending_of_account from accconstant  where  coop_id ='" + state.SsCoopId + "'";


            Sdt dt4 = ta.Query(sql4);
            if (dt4.Next())
            {
                acccash_id = dt4.GetString("cash_account_code");
                starting_of_account = DateTime.Parse(dt4.GetString("beginning_of_accou"));
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
            String sql = @"select  nvl( max(account_year), 0 )  as acccount_year , nvl( max(period), 0 ) as period from accperiod  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + year + "' and close_flag = 1 ";

            Sdt dt = ta.Query(sql);
            if (dt.Next())
            {
                if (period == 1)
                {
                    accyear = year - 1;
                    accperiod = 12;
                }
                else
                {
                    accyear = int.Parse(dt.GetString("acccount_year"));
                    accperiod = int.Parse(dt.GetString("period"));
                    if (accyear == 0 && accperiod == 0) //กรณียังไม่ได้ปิดสิ้นปี
                    {
                        int lastyear = year - 1;
                        sql = @"select  nvl( max(account_year), 0 )  as acccount_year , nvl( max(period), 0 ) as period from accperiod  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + lastyear + "' and close_flag = 1 ";
                        dt = ta.Query(sql);
                        if (dt.Next())
                        {
                            accyear = int.Parse(dt.GetString("acccount_year"));
                            accperiod = int.Parse(dt.GetString("period"));
                        }
                    }
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลปีของปีบัญชี ");
                sqlca.Rollback();
            }
            //ta.Close();



            //หาวันที่สิ้นงวด

            String sql3 = @"select period_end_date from accperiod  where  coop_id ='" + state.SsCoopId + "' and account_year = '" + year + "' and period = '" + accperiod + "' ";
            Sdt dt3 = ta.Query(sql3);
            if (dt3.Next())
            {
                period_end_date = DateTime.Parse(dt3.GetDateEn("period_end_date"));

            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบวันที่สิ้นงวดบัญชี ");
                sqlca.Rollback();
            }
            ta.Close();

            //หายอดยกมา
            Decimal Begin = 0;
            Decimal Forward = 0;
            DateTime s_date = dw_criteria.GetItemDateTime(1, "start_date");

            Int32 CashBeginForward = wcf.NAccount.of_get_cash_bg_fw_first_period(wsPass, s_date, state.SsCoopId, ref Begin, ref Forward);
            ldc_total = Begin;


            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.

            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            //lnv_helper.AddArgument(end_date, ArgumentType.DateTime);  //**
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
                String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);
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
