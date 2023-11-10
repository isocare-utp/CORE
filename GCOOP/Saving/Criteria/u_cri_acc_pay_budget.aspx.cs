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
    public partial class u_cri_acc_pay_budget : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        protected String changeValue;
        //เพิ่ม 2.
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            changeValue = WebUtil.JsPostBack(this, "changeValue");
            tdw_criteria = new DwThDate(dw_criteria, this);
            //tdw_criteria.Add("start_date", "start_tdate");
            //tdw_criteria.Add("end_date", "end_tdate");

        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {

            this.ConnectSQLCA();
            dw_criteria.SetTransaction(sqlca);
            //GetRetrieve();

            if (IsPostBack)
            {
                //dw_criteria.RestoreContext();
               this.RestoreContextDw(dw_criteria);
            }
            else
            {
                //default values.
                dw_criteria.Reset();
                dw_criteria.InsertRow(0);
                DwUtil.RetrieveDDDW(dw_criteria, "start_branch", "criteria.pbl", state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_criteria, "end_branch", "criteria.pbl", state.SsCoopControl);
                dw_criteria.SetItemString(1, "start_branch", state.SsCoopControl);
                dw_criteria.SetItemString(1, "end_branch", state.SsCoopControl);
                //tdw_criteria.Eng2ThaiAllRow();
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
            else if (eventArg == "changeValue")
            {
                Decimal ai_year = dw_criteria.GetItemDecimal(1, "as_year") - 543;
                DwUtil.RetrieveDDDW(dw_criteria, "as_month", "criteria.pbl", ai_year);
            }
        }
        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            //if (dw_criteria.RowCount > 1)
            //{
            //    dw_criteria.DeleteRow(dw_criteria.RowCount);
            //}
            dw_criteria.SaveDataCache();
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String coop_name = state.SsCoopName;

            String coop_id = state.SsCoopId;
            // String branch_id = "000";
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.\
            ReportHelper lnv_helper = new ReportHelper();
            //---------------
            string start_branch_id = dw_criteria.GetItemString(1, "start_branch");
            string end_branch_id = dw_criteria.GetItemString(1, "end_branch");
            Decimal year = dw_criteria.GetItemDecimal(1, "as_year") - 543;
            decimal recv_period = dw_criteria.GetItemDecimal(1, "as_month");
            Decimal item_status = dw_criteria.GetItemDecimal(1, "type_report");

            //--------------
            lnv_helper.AddArgument(start_branch_id, ArgumentType.String);
            lnv_helper.AddArgument(end_branch_id, ArgumentType.String);
            lnv_helper.AddArgument(Convert.ToString(year), ArgumentType.Number);
            lnv_helper.AddArgument(Convert.ToString(recv_period), ArgumentType.Number);
            //----------------------------------------------------


            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            if (item_status == 1)
            {
                rid = "[ACM081]";    // งบประมาณรับ
            }
            else if (item_status == 2)
            {
                rid = "[ACM071]";      // งบประมาณจ่าย
            }
       
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //เพิ่ม 3
                String criteriaXML = lnv_helper.PopArgumentsXML();
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
        //private void changeSheet()
        //{
        //    dw_criteria.Retrieve(HdSheetCode.Value);
        //}
        //#endregion

        //private void GetRetrieve()
        //{
        //    try
        //    {
        //        dw_criteria.Reset();
        //        dw_criteria.Retrieve(HdSheetCode.Value);
        //        //Response.Write("<script> alert('XML = " + dw_main.GetItemString(1, "month_1_1") + "') </script>");
        //    }
        //    catch
        //    {
        //        HdSheetCode.Value = "";
        //    }
        //}
    }
}
        #endregion