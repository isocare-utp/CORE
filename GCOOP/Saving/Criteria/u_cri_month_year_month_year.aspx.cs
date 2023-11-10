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

namespace Saving.Criteria
{
    public partial class u_cri_month_year_month_year : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String previewReport;
        private DwThDate tdw_criteria;
        Sdt dt = new Sdt();
        Sdt dt2 = new Sdt();
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            previewReport = WebUtil.JsPostBack(this, "previewReport");


        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
            Sta ta = new Sta(state.SsConnectionString);
            this.ConnectSQLCA();
            dw_preview.SetTransaction(sqlca);

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

                String sql = "";
                sql = @"SELECT REPORT_NAME,REPORT_DWOBJECT  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();

            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }
            ta.Close();

            dw_preview.DataWindowObject = dt.Rows[0]["REPORT_DWOBJECT"].ToString();
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
                this.RestoreContextDw(dw_preview);
            }
            else
            {
                //default values.
                dw_preview.InsertRow(0);
                dw_criteria.InsertRow(0);

                dw_criteria.SetItemString(1, "select_year", "");
                dw_criteria.SetItemString(1, "select_month", "");
                dw_criteria.SetItemString(1, "select_eyear", "");
                dw_criteria.SetItemString(1, "select_emonth", "");


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
            else if (eventArg == "previewReport")
            {
                PreviewReport();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
            dw_preview.SaveDataCache();
        }
        private void PreviewReport()
        {

            dw_preview.SetTransaction(sqlca);
            String select_year = dw_criteria.GetItemString(1, "select_year");
            String select_month = dw_criteria.GetItemString(1, "select_month");
            String select_eyear = dw_criteria.GetItemString(1, "select_eyear");
            String select_emonth = dw_criteria.GetItemString(1, "select_emonth");
            select_year = (Convert.ToInt32(select_year) - 543).ToString();
            select_eyear = (Convert.ToInt32(select_eyear) - 543).ToString();


            dw_preview.Retrieve(select_year, select_month, select_eyear, select_emonth);


        }
        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            String select_year = dw_criteria.GetItemString(1, "select_year");
            String select_month = dw_criteria.GetItemString(1, "select_month");
            String select_eyear = dw_criteria.GetItemString(1, "select_eyear");
            String select_emonth = dw_criteria.GetItemString(1, "select_emonth");

            select_year = (Convert.ToInt32(select_year) - 543).ToString();
            select_eyear = (Convert.ToInt32(select_eyear) - 543).ToString();
          
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(select_month, ArgumentType.String);
            lnv_helper.AddArgument(select_month, ArgumentType.String);
            lnv_helper.AddArgument(select_eyear, ArgumentType.String);
            lnv_helper.AddArgument(select_emonth, ArgumentType.String);


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
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;

                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);



                //String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
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
