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

namespace Saving.Criteria
{
    public partial class u_cri_date_year_rmembno_rmembgroup_rloanobjective_name : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        protected String checkFlag;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            checkFlag = WebUtil.JsPostBack(this, "checkFlag");
            tdw_criteria = new DwThDate(dw_criteria,this);
            tdw_criteria.Add("select_date", "select_tdate");
           
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
            DwUtil.RetrieveDDDW(dw_criteria, "start_loanobjective", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_loanobjective", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup", "criteria.pbl", null);
            //DwUtil.RetrieveDDDW(dw_criteria, "select_name", "criteria.pbl", null);
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDecimal(1, "select_year", 2553);
                dw_criteria.SetItemDateTime(1, "select_date", state.SsWorkDate);
                string[] minmax = ReportUtil.GetMinMaxMembloanobjective();
                dw_criteria.SetItemString(1, "start_loanobjective", minmax[0]);
                dw_criteria.SetItemString(1, "end_loanobjective", minmax[1]);
                string[] minmax1 = ReportUtil.GetMinMaxMembgroup();
                dw_criteria.SetItemString(1, "start_membgroup", minmax1[0]);
                dw_criteria.SetItemString(1, "end_membgroup", minmax1[1]);
                dw_criteria.SetItemString(1, "start_membno", "");
                dw_criteria.SetItemString(1, "end_membno", "");
                dw_criteria.SetItemString(1, "select_name", "");

                dw_criteria.SetItemString(1, "select_tdate", "");

                dw_criteria.SetItemDecimal(1, "fixmembgroup_flag", 1);
                dw_criteria.SetItemDecimal(1, "fixmembno_flag", 0);
               
               
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
            else if (eventArg == "checkFlag")
            {
                CheckFlag();
            }
        }
      

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {

        }

        #endregion

        private void CheckFlag()
        { 
        //ให้หน้า page refresh
            decimal test_flagG = dw_criteria.GetItemDecimal(1, "fixmembgroup_flag");
            decimal test_flagN = dw_criteria.GetItemDecimal(1, "fixmembno_flag");

            decimal test1 = test_flagG;
            decimal test2 = test_flagN;

            if (test1 == 1)
            {
                dw_criteria.SetItemDecimal(1, "fixmembno_flag", 0);
               
            }
            else
            {
                dw_criteria.SetItemDecimal(1, "fixmembno_flag", 1);
            }
        }
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.


            String select_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "select_tdate", null);
            String start_loanobjective = dw_criteria.GetItemString(1, "start_loanobjective");
            String end_loanobjective = dw_criteria.GetItemString(1, "end_loanobjective");
            String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup");
            String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup");
            String start_membno = dw_criteria.GetItemString(1, "start_membno");
            String end_membno = dw_criteria.GetItemString(1, "end_membno");
            String select_name = dw_criteria.GetItemString(1, "select_name");
            Decimal select_year = dw_criteria.GetItemDecimal(1, "select_year");
            decimal test_flagG = dw_criteria.GetItemDecimal(1, "fixmembgroup_flag");
            decimal test_flagN = dw_criteria.GetItemDecimal(1, "fixmembno_flag");

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(select_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(select_year.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(end_membno, ArgumentType.String);
            lnv_helper.AddArgument(start_membgroup, ArgumentType.String);
            lnv_helper.AddArgument(end_membgroup, ArgumentType.String);
            lnv_helper.AddArgument(start_loanobjective, ArgumentType.String);
            lnv_helper.AddArgument(end_loanobjective, ArgumentType.String);
            lnv_helper.AddArgument(select_name, ArgumentType.String);


            decimal test1 = test_flagG;
            decimal test2 = test_flagN;
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
