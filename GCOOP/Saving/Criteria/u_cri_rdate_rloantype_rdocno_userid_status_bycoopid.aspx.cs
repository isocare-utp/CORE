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
    public partial class u_cri_rdate_rloantype_rdocno_userid_status_bycoopid : PageWebSheet, WebSheet
    {

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        protected String checkFlag;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            checkFlag = WebUtil.JsPostBack(this, "checkFlag");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
            DwUtil.RetrieveDDDW(dw_criteria, "start_loantype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_loantype", "criteria.pbl", null);
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
                //string[] minmax = ReportUtil.GetMinMaxLoantype();
                //dw_criteria.SetItemString(1, "start_loantype", minmax[0]);
                //dw_criteria.SetItemString(1, "end_loantype", minmax[1]);
                dw_criteria.SetItemString(1, "start_loantype", "");//MiW
                dw_criteria.SetItemString(1, "end_loantype", "");//MiW
                dw_criteria.SetItemString(1, "start_docno", "");
                dw_criteria.SetItemString(1, "end_docno", "");
                dw_criteria.SetItemDecimal(1, "fixentry_flag", 1);
                dw_criteria.SetItemDecimal(1, "fixreqsts_flag", 0);
                dw_criteria.SetItemString(1, "select_userid", "");
                dw_criteria.SetItemDecimal(1, "select_constatus", 1);
                CheckFlag();//MiW
                int flag_user;
                try
                {
                    flag_user = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "fixentry_flag"));
                }
                catch { flag_user = 1; }

                if (flag_user == 1)
                {
                    dw_criteria.SetItemString(1, "select_userid", state.SsUsername);
                }
                else
                {
                    dw_criteria.SetItemString(1, "select_userid", "");
                }

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
            int flag_user;
            try
            {
                flag_user = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "fixentry_flag"));
            }
            catch { flag_user = 1; }

            if (flag_user == 1)
            {
                dw_criteria.SetItemString(1, "select_userid", state.SsUsername);
            }
            else
            {
                dw_criteria.SetItemString(1, "select_userid", "");
            }
        }
        #region Report Process
        private void RunProcess()
        {
            String start_loantype = "", end_loantype = "";
            string[] minmax = ReportUtil.GetMinMaxLoantype();

            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            #region เพิ่นเงื่อนไขหากไม่ทำการเลือกให้ออกรายงานทั้งหมด หากเลือกเพียง"ตั้งแต่เงินกู้"ให้แสดงเฉพาะ"ตั้งแต่"เงินกูอย่างเดียว
            start_loantype = dw_criteria.GetItemString(1, "start_loantype");
            end_loantype = dw_criteria.GetItemString(1, "end_loantype");
            if (start_loantype == "" || start_loantype == null)
            {
                start_loantype = minmax[0];                
            }
            if (end_loantype == "" || end_loantype == null)
            {
                end_loantype = minmax[1];
            }
            #endregion
            String start_docno = dw_criteria.GetItemString(1, "start_docno");
            String end_docno = dw_criteria.GetItemString(1, "end_docno");
            String select_userid = dw_criteria.GetItemString(1, "select_userid");
            Decimal select_constatus = dw_criteria.GetItemDecimal(1, "select_constatus");

            if (start_docno == "" || start_docno == null) { start_docno = "0000000000"; } else { start_docno = dw_criteria.GetItemString(1, "start_docno"); }
            if (end_docno == "" || end_docno == null) { end_docno = "ฮฮฮฮฮฮฮฮฮฮ"; } else { end_docno = dw_criteria.GetItemString(1, "end_docno"); }
            if (select_userid == "" || select_userid == null) { select_userid = "%"; } else { select_userid = dw_criteria.GetItemString(1, "select_userid"); }
            int li_fixreqflag = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "fixreqsts_flag"));
            if (li_fixreqflag == 0) { select_constatus = 8; }

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);
            lnv_helper.AddArgument(state.SsCoopId, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(start_loantype, ArgumentType.String);
            lnv_helper.AddArgument(end_loantype, ArgumentType.String);
            lnv_helper.AddArgument(start_docno, ArgumentType.String);
            lnv_helper.AddArgument(end_docno, ArgumentType.String);
            lnv_helper.AddArgument(select_userid, ArgumentType.String);
            lnv_helper.AddArgument(select_constatus.ToString(), ArgumentType.Number);


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
