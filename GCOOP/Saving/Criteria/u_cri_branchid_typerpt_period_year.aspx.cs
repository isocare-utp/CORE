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
using Saving.Applications.account;
using CoreSavingLibrary.WcfNAccount;

namespace Saving.Criteria
{
    public partial class u_cri_branchid_typerpt_period_year : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        private accFunction accF;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");

            tdw_criteria = new DwThDate(dw_criteria, this);
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            DwUtil.RetrieveDDDW(dw_criteria, "branch_id", "criteria.pbl", state.SsCoopControl);
            try
            {
                accF = new accFunction();
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Function ไม่ได้");
                return;
            }
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemString(1, "branch_id", state.SsCoopId);
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
            String strTmp = "";
            String coop_id = dw_criteria.GetItemString(1, "branch_id");
            Int16 type_rpt = Convert.ToInt16(dw_criteria.GetItemDecimal(1, "type_rpt"));
            String period = dw_criteria.GetItemString(1, "period");
            String year_period = dw_criteria.GetItemString(1, "year_period");

            strTmp = accF.GetFindToLDayOfM(period, year_period, "TH").Trim();
            String SDateTH = "01/" + period + "/" + year_period;
            String SDateUS = "01/" + period + "/" + Convert.ToString(Convert.ToInt16(year_period) - 543);
            String EDateTH = strTmp + "/" + period + "/" + year_period;
            String EDateUS = strTmp + "/" + period + "/" + Convert.ToString(Convert.ToInt16(year_period) - 543);

            DateTime SDate = DateTime.ParseExact(SDateUS, "dd/MM/yyyy", WebUtil.EN);
            DateTime EDate = DateTime.ParseExact(EDateUS, "dd/MM/yyyy", WebUtil.EN);
            
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(SDateUS, ArgumentType.String);
            lnv_helper.AddArgument(EDateUS, ArgumentType.String);
            lnv_helper.AddArgument(type_rpt.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            //lnv_helper.AddArgument(branch_id, ArgumentType.String);
            //lnv_helper.AddArgument(type_rpt.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(period, ArgumentType.String);
            //lnv_helper.AddArgument(year_period, ArgumentType.String);

            //************************************************************************

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
        #endregion
    }
}
