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
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Criteria
{
     public partial class u_cri_year_scgolar_level_date_coop_all : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String postFilterScholarshipS;
        protected String postFilterScholarshipE;
        private DwThDate tdw_criteria;
        private decimal level_School;
        public String outputProcess = "";
        #region WebSheet Members

        public void InitJsPostBack()
        { 
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            postFilterScholarshipS = WebUtil.JsPostBack(this, "postFilterScholarshipS");
            postFilterScholarshipE = WebUtil.JsPostBack(this, "postFilterScholarshipE");
            HdOpenIFrame.Value = "False";
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }


        public void WebSheetLoadBegin()
        {
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
                dw_criteria.SetItemString(1, "start_tdate", "");
                dw_criteria.SetItemString(1, "end_tdate", "");
              
              DwUtil.RetrieveDDDW(dw_criteria, "scholarshiptype_type", "criteria.pbl",state.SsCoopId);
              DwUtil.RetrieveDDDW(dw_criteria, "sschoollevel_level", "criteria.pbl", null);
              DwUtil.RetrieveDDDW(dw_criteria, "eschoollevel_level", "criteria.pbl", null);
              tdw_criteria.Eng2ThaiAllRow();
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
            else if (eventArg == "postFilterScholarshipS")
            {
                FilterScholarshipS();
            }
            else if (eventArg == "postFilterScholarshipE")
            {
                FilterScholarshipE();
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
        private void FilterScholarshipS()
        {
            level_School = dw_criteria.GetItemDecimal(1, "sschooldet_level");
            Session["sschooldet_level"] = level_School;

            DwUtil.RetrieveDDDW(dw_criteria, "sschoollevel_level", "criteria.pbl", null);
            DataWindowChild Dc = dw_criteria.GetChild("sschoollevel_level");
            Dc.SetFilter("school_group =" + level_School + "");
            Dc.Filter();

            //GetMoney();
        }
        private void FilterScholarshipE()
        {
            level_School = dw_criteria.GetItemDecimal(1, "eschooldet_level");
            Session["eschooldet_level"] = level_School;

            DwUtil.RetrieveDDDW(dw_criteria, "eschoollevel_level", "criteria.pbl", null);
            DataWindowChild Dc = dw_criteria.GetChild("eschoollevel_level");
            Dc.SetFilter("school_group =" + level_School + "");
            Dc.Filter();

            //GetMoney();
        }
        private void RunProcess()
        {

            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            decimal scholarshiptype = dw_criteria.GetItemDecimal(1, "scholarshiptype_type");
            decimal sschoollevel = dw_criteria.GetItemDecimal(1, "sschoollevel_level");
            decimal eschoollevel = dw_criteria.GetItemDecimal(1, "eschoollevel_level");
            decimal sschooldet = dw_criteria.GetItemDecimal(1, "sschooldet_level");
            decimal eschooldet = dw_criteria.GetItemDecimal(1, "eschooldet_level");
            decimal year = dw_criteria.GetItemDecimal(1, "year");
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);// state.SsWorkDate.Date.ToString();
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);//state.SsWorkDate.Date.ToString();
           // String coop_id = dw_criteria.GetItemString(1, "coop_id");
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(year.ToString(),ArgumentType.Number);
            lnv_helper.AddArgument(scholarshiptype.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(sschooldet.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(eschooldet.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(sschoollevel.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(eschoollevel.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument("001001", ArgumentType.String);
            //***************************************************************************

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
