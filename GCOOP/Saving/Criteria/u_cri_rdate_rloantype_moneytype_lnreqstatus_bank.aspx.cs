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
    public partial class u_cri_rdate_rloantype_moneytype_lnreqstatus_bank : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String checkFlag;
        private DwThDate tdw_criteria;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            checkFlag = WebUtil.JsPostBack(this, "checkFlag");
            tdw_criteria = new DwThDate(dw_criteria,this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            InitJsPostBack();

            DwUtil.RetrieveDDDW(dw_criteria, "start_loantype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_loantype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_moneytype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_bankcode", "criteria.pbl", null);

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
                string[] minmax = ReportUtil.GetMinMaxLoantype();
                dw_criteria.SetItemString(1, "start_loantype", minmax[0]);
                dw_criteria.SetItemString(1, "end_loantype", minmax[1]);
                dw_criteria.SetItemString(1, "as_bankcode", "");
                dw_criteria.SetItemDecimal(1, "fixed_flag", 1);
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

        private void CheckFlag()
        {
            //ให้หน้า page refresh

            Decimal fixed_flagtest = dw_criteria.GetItemDecimal(1, "fixed_flag");
            if (fixed_flagtest == 0)
            {
                dw_criteria.SetItemString(1, "as_bankcode", "");

            }

        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            //DateTime start_date = dw_criteria.GetItemDateTime(1, "start_date");
            //DateTime end_date = dw_criteria.GetItemDateTime(1, "end_date");
            String as_coopid = state.SsCoopId;
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            String start_loantype = dw_criteria.GetItemString(1, "start_loantype");
            String end_loantype = dw_criteria.GetItemString(1, "end_loantype");
            //String as_moneytype = dw_criteria.GetItemString(1, "as_moneytype");
            String as_lnreqstatus = dw_criteria.GetItemString(1, "as_lnreqstatus");
            String as_bankcode = dw_criteria.GetItemString(1, "as_bankcode");

            

            Decimal fixed_flagtest1 = dw_criteria.GetItemDecimal(1, "fixed_flag");
            if (fixed_flagtest1 == 0)
            {
                as_bankcode = "%";

            }

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            //lnv_helper.AddArgument(start_date.ToString("yyyy-MM-dd", WebUtil.EN), ArgumentType.DateTime);
            //lnv_helper.AddArgument(end_date.ToString("yyyy-MM-dd", WebUtil.EN), ArgumentType.DateTime);
            lnv_helper.AddArgument(as_coopid, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(start_loantype, ArgumentType.String);
            lnv_helper.AddArgument(end_loantype, ArgumentType.String);
            //lnv_helper.AddArgument(as_moneytype, ArgumentType.String);
            lnv_helper.AddArgument(as_lnreqstatus, ArgumentType.String);
            lnv_helper.AddArgument(as_bankcode, ArgumentType.String);

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
