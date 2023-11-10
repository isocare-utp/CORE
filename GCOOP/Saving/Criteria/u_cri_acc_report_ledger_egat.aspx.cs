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
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Criteria
{
    public partial class u_cri_acc_report_ledger_egat : PageWebSheet, WebSheet
    {
        private n_commonClient commonService;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        protected String postSetDate;
        public String outputProcess = "";

        #region WebSheet Members
        //=============
        private void JspostCheckLastmonth()
        {
            String t_date = "01";
            String t_month = dw_criteria.GetItemString(1, "month");
            String t_year = (dw_criteria.GetItemString(1, "year"));
            String t_receive = t_date + "/" + t_month.ToString() + "/" + t_year.ToString();
            DateTime start_date = DateTime.Parse(t_receive);
            try
            {
                DateTime newdate = commonService.of_lastdayofmonth(state.SsWsPass, start_date);
                dw_criteria.SetItemDate(1, "start_date", start_date);
                dw_criteria.SetItemDate(1, "end_date", newdate);
                tdw_criteria.Eng2ThaiAllRow();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void JspostNewClear()
        {
            dw_criteria.Reset();
            dw_criteria.InsertRow(0);
            SetChildDW();
            String kp_month = Convert.ToString(DateTime.Now.Month);
            if (kp_month.Length != 2)
            {
                kp_month = "0" + kp_month;
            }

            dw_criteria.SetItemString(1, "year", Convert.ToString(DateTime.Now.Year + 543));
            dw_criteria.SetItemString(1, "month", kp_month);
            string[] minmax = ReportUtil.GetMinMaxAccountId();
            dw_criteria.SetItemString(1, "start_accid", minmax[0].Trim());
            dw_criteria.SetItemString(1, "end_accid", minmax[1].Trim());
            JspostCheckLastmonth();
        }

        //============
        
        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
            postSetDate = WebUtil.JsPostBack(this, "postSetDate");
        }

        
        public void WebSheetLoadBegin()
        {
            commonService = wcf.NCommon;
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                JspostNewClear();
            }

            //--- Page Arguments
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
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);        }

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
            else if (eventArg == "postSetDate")
            {
                JspostCheckLastmonth();
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

            DateTime start_date = dw_criteria.GetItemDateTime(1, "start_date");
            DateTime end_date = dw_criteria.GetItemDateTime(1, "end_date");
            String coop_name = state.SsCoopName;
            String branch_id = state.SsCoopId;
            //XmlDataSourceView str_tmp = dw_criteria.Describe("Datawindow.Data.XML");
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.\
            ReportHelper lnv_helper = new ReportHelper();
            String sDate = start_date.Day.ToString("00") + start_date.Month.ToString("00") + start_date.Year.ToString();
            String eDate = end_date.Day.ToString("00") + end_date.Month.ToString("00") + end_date.Year.ToString();
            lnv_helper.AddArgument(sDate, ArgumentType.String);
            lnv_helper.AddArgument(eDate, ArgumentType.String);
            lnv_helper.AddArgument(dw_criteria.GetItemString(1, "start_accid"), ArgumentType.String);
            lnv_helper.AddArgument(dw_criteria.GetItemString(1, "end_accid"), ArgumentType.String);
            lnv_helper.AddArgument(branch_id, ArgumentType.String);
            //----------------------------------------------------


            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;

                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);

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
        protected void SetChildDW()
        {
            // accstart
            DwUtil.RetrieveDDDW(dw_criteria, "start_accid", "criteria.pbl", null);

            // accend
            DwUtil.RetrieveDDDW(dw_criteria, "end_accid", "criteria.pbl", null);
        }
        #endregion
    }
}
