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
//using DataLibrary;

namespace Saving.Criteria
{
    public partial class u_cri_divavg_inform_limit_share : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria,this);
            tdw_criteria.Add("operate_date", "operate_tdate");
            tdw_criteria.Add("meetingperiod_date", "meetingperiod_tdate");
            tdw_criteria.Add("informshr_date", "informshr_tdate");
            tdw_criteria.Add("dueoperate_date", "dueoperate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
           
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                int year = (DateTime.Now.Year) + 543;
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemString(1, "div_year", year.ToString());
                dw_criteria.SetItemDateTime(1, "operate_date", DateTime.Now);
                dw_criteria.SetItemDateTime(1, "meetingperiod_date", DateTime.Now);
                dw_criteria.SetItemDateTime(1, "informshr_date", DateTime.Now);
                dw_criteria.SetItemDateTime(1, "dueoperate_date", DateTime.Now);
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
            String div_year = dw_criteria.GetItemString(1, "div_year");
            String operate_tdate = WebUtil.ConvertDateThaiToEng(dw_criteria, "operate_tdate", null);
            String meetingperiod = dw_criteria.GetItemString(1, "meetingperiod");
            String meetingperiod_tdate = WebUtil.ConvertDateThaiToEng(dw_criteria, "meetingperiod_tdate", null);
            String informshr_tdate = WebUtil.ConvertDateThaiToEng(dw_criteria, "informshr_tdate", null);
            String dueoperate_tdate = WebUtil.ConvertDateThaiToEng(dw_criteria, "dueoperate_tdate", null);
            String namecommit = dw_criteria.GetItemString(1, "namecommit");

            //String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            //String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            //Decimal start_age = dw_criteria.GetItemDecimal(1,"start_age");
            //Decimal end_age = dw_criteria.GetItemDecimal(1,"end_age");

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(div_year, ArgumentType.String);
            lnv_helper.AddArgument(operate_tdate, ArgumentType.DateTime);
            lnv_helper.AddArgument(meetingperiod, ArgumentType.String);
            lnv_helper.AddArgument(meetingperiod_tdate, ArgumentType.DateTime);
            lnv_helper.AddArgument(informshr_tdate, ArgumentType.DateTime);
            lnv_helper.AddArgument(dueoperate_tdate, ArgumentType.DateTime);
            lnv_helper.AddArgument(namecommit, ArgumentType.String);

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
