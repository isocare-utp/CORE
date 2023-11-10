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
    public partial class u_cri_instype : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        public String outputProcess = "";
       

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");

        }

        public void WebSheetLoadBegin()
        {
           
            DwUtil.RetrieveDDDW(dw_criteria, "as_instype", "criteria.pbl", null);

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
            }
            else
            {
                dw_criteria.InsertRow(0);
               
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
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
        }

        #endregion

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

           
            String instype = dw_criteria.GetItemString(1, "as_instype");

            //String starttype = dw_criteria.GetItemString(1, "as_starttype");
            //String endtype = dw_criteria.GetItemString(1, "as_endtype");
            //String status = dw_criteria.GetItemString(1, "ai_status");
            //String startmbtype = dw_criteria.GetItemString(1, "ai_startmbtype");
            //String endmbtype = dw_criteria.GetItemString(1, "ai_endmbtype");
            //String startemtype = dw_criteria.GetItemString(1, "as_startemtype");
            //String endemtype = dw_criteria.GetItemString(1, "as_endemtype");
            //decimal startage = dw_criteria.GetItemDecimal(1, "ai_startage");
            //decimal endage = dw_criteria.GetItemDecimal(1, "ai_endage");

            //String astype = dw_criteria.GetItemString(1, "as_startapp");
            //string startapp, endapp;
            //startapp = (astype == "01") ? "01" : "03";
            //endapp = (astype == "03") ? "04" : "02";

            String account_id = "";
            String sql_txt = "select cash_account_code from accconstant";
            DataTable dt = WebUtil.Query(sql_txt);
            if (dt.Rows.Count > 0)
            {
                account_id = dt.Rows[0][0].ToString().Trim();
            }
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.

            ReportHelper lnv_helper = new ReportHelper();

         
            lnv_helper.AddArgument(instype, ArgumentType.String);
            //lnv_helper.AddArgument(starttype, ArgumentType.String);
            //lnv_helper.AddArgument(endtype, ArgumentType.String);

            //lnv_helper.AddArgument(status.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(startmbtype.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(endmbtype.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(startemtype, ArgumentType.String);
            //lnv_helper.AddArgument(endemtype, ArgumentType.String);
            lnv_helper.AddArgument(state.SsUsername, ArgumentType.Number);
            //lnv_helper.AddArgument(endage.ToString(), ArgumentType.Number);

            //lnv_helper.AddArgument(startapp, ArgumentType.String);
            //lnv_helper.AddArgument(endapp, ArgumentType.String);

            //lnv_helper.AddArgument(membno, ArgumentType.String);
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
    }
}
