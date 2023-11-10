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
using CoreSavingLibrary.WcfNAccount;


namespace Saving.Criteria
{
    public partial class u_cri_acc_journal_rdate : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        private n_accountClient accService;
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
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");

        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {

            //this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            //GetRetrieve();
            accService = wcf.NAccount;
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


                dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                dw_criteria.SetItemString(1, "coop_id", state.SsCoopId);
                tdw_criteria.Eng2ThaiAllRow();
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
                //Decimal ai_year = dw_criteria.GetItemDecimal(1, "as_year") - 543;
                //DwUtil.RetrieveDDDW(dw_criteria, "as_month", "criteria.pbl", ai_year);
            }
        }
        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            //dw_criteria.SaveDataCache();  //**
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            String coop_name = state.SsCoopName;
            String coop_id = state.SsCoopId;
            String wsPass = state.SsWsPass;
            DateTime date = dw_criteria.GetItemDateTime(1, "start_date");
            DateTime edate = dw_criteria.GetItemDateTime(1, "end_date");
            Decimal item_status = dw_criteria.GetItemDecimal(1, "type_report");

            Decimal Begin = 0;
            Decimal Forward = 0;

            int result = wcf.NAccount.of_set_default_accountid(wsPass, state.SsCoopControl);
            Int32 CashBeginForward = wcf.NAccount.of_get_cash_bg_fw(wsPass, date, state.SsCoopId, ref Begin, ref Forward);

            //String lbl_moneybg = CashBeginForward.ToString("#,##0.00");
            //String lbl_moneyfw = CashBeginForward.ToString("#,##0.00");
            String lbl_moneybg = Begin.ToString("#,##0.00");
            String lbl_moneyfw = Forward.ToString("#,##0.00");

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.\
            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(lbl_moneybg, ArgumentType.String);
            lnv_helper.AddArgument(lbl_moneyfw, ArgumentType.String);
            //----------------------------------------------------

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();


            if (item_status == 1)
            {
                rid = "[ACD110]";    // งนสด
            }
            else if (item_status == 2)
            {
                rid = "[ACD120]";      // เงินโอน
            }
            else if (item_status == 3)
            {
                rid = "[ACD130]";      // รวม
            }

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
