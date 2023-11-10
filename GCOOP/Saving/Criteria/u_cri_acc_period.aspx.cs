﻿using System;
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
    public partial class u_cri_acc_period : PageWebSheet, WebSheet
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
        //========================
      
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

            String kp_month = Convert.ToString(DateTime.Now.Month);
            if (kp_month.Length != 2)
            {
                kp_month = "0" + kp_month;
            }
            dw_criteria.SetItemString(1, "year", Convert.ToString(DateTime.Now.Year + 543));
            dw_criteria.SetItemString(1, "month", kp_month);

            JspostCheckLastmonth();
        }
        //========================
        public void InitJsPostBack()
        {

            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");

            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            postSetDate = WebUtil.JsPostBack(this, "postSetDate");


        }

        //protected void Page_Load(object sender, EventArgs e)
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
            
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date , ArgumentType.DateTime);

            //----------------------------------------------------------------

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                String criteriaXML = lnv_helper.PopArgumentsXML();
                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);

                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                //String li_return = lws_report.Run(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                //string li_return = lws_report.ReportPDF(state.SsWsPass,app,gid,rid,
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
