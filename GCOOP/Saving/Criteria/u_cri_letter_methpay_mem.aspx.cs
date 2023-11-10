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
    public partial class u_cri_letter_methpay_mem : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
	
            tdw_criteria = new DwThDate(dw_criteria,this);
            tdw_criteria.Add("sent_date", "sent_tdate");
            tdw_criteria.Add("meeting_date", "meeting_tdate");
            tdw_criteria.Add("sentback_date", "sentback_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            InitJsPostBack();

         
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                DwUtil.RetrieveDDDW(dw_criteria, "branch_id", "criteria.pbl", state.SsCoopControl);
                dw_criteria.SetItemString(1, "branch_id", state.SsCoopId);
                dw_criteria.SetItemString(1, "start_membno", "000000");
                dw_criteria.SetItemString(1, "end_membno", "999999");
                JsGetYear();
               // dw_criteria.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
                dw_criteria.SetItemDecimal(1, "running_number", 1);
                dw_criteria.SetItemDateTime(1, "sent_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "meeting_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "sentback_date", state.SsWorkDate);
                tdw_criteria.Eng2ThaiAllRow();
                dw_criteria.SetItemString(1, "name", "นายอนันต์  ชาตรูประชีวิน");
                dw_criteria.SetItemString(1, "position", "ผู้จัดการใหญ่");
                dw_criteria.SetItemString(1, "department", "เงินทุน");
                dw_criteria.SetItemString(1, "tel", "02-496-1199 ต่อ 108-111");
                dw_criteria.SetItemString(1, "fax", "02-496-1188");
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
            String branch_id = state.SsCoopId;
            String start_membno = dw_criteria.GetItemString(1, "start_membno").Trim();
            String end_membno = dw_criteria.GetItemString(1, "end_membno").Trim();
            String div_year = dw_criteria.GetItemString(1, "div_year");
            
            String meet_loc = dw_criteria.GetItemString(1, "meeting_loc");
            Decimal running_number = dw_criteria.GetItemDecimal(1, "running_number");
            String sent_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "sent_tdate", null);
            String meeting_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "meeting_tdate", null);
            String sentback_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "sentback_tdate", null);
            String name = dw_criteria.GetItemString(1, "name");
            String position = dw_criteria.GetItemString(1, "position");
            String department = dw_criteria.GetItemString(1, "department");
            String tel = dw_criteria.GetItemString(1, "tel");
            String fax = dw_criteria.GetItemString(1, "fax");

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(branch_id, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(end_membno, ArgumentType.String);
            lnv_helper.AddArgument(div_year, ArgumentType.String);
            lnv_helper.AddArgument(meet_loc, ArgumentType.String);
            lnv_helper.AddArgument(running_number.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(sent_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(meeting_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(sentback_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(name, ArgumentType.String);
            lnv_helper.AddArgument(position, ArgumentType.String);
            lnv_helper.AddArgument(department, ArgumentType.String);
            lnv_helper.AddArgument(tel, ArgumentType.String);
            lnv_helper.AddArgument(fax, ArgumentType.String);
            
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

        private void JsGetYear()
        {
            int account_year = 0;
            try
            {
                String sql = @"select max(current_year) from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(current_year)"));
                    dw_criteria.SetItemString(1, "div_year", Convert.ToString(account_year));
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                account_year = DateTime.Now.Year;
                account_year = account_year + 543;
                dw_criteria.SetItemString(1, "div_year", account_year.ToString());
            }
        }
        #endregion

    }
}
