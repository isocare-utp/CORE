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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Criteria
{
    public partial class u_cri_rdate_member_notice_chgcoll : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String jsMemberNo;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            jsMemberNo = WebUtil.JsPostBack(this, "jsMemberNo");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("reqloan_date", "reqloan_tdate");
            //tdw_criteria.Add("end_date", "end_tdate");

        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            InitJsPostBack();

            DwUtil.RetrieveDDDW(dw_criteria, "resigncause", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "position_name", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "position_user", "criteria.pbl", null);

            if (IsPostBack)
            {
                //NewClear();
                dw_criteria.RestoreContext();     
            }
            else
            {
                //NewClear();
                //default values.
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "reqloan_date", state.SsWorkDate);
                //dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                dw_criteria.SetItemString(1, "position_all", "");
                dw_criteria.SetItemString(1, "position_user", "");
                dw_criteria.SetItemString(1, "note_1", "");
                dw_criteria.SetItemString(1, "note_2", "");
                dw_criteria.SetItemString(1, "note_3", "");
            
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
            else if (eventArg == "jsMemberNo")
            {
                JsMemberNo();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
        private void JsMemberNo()
        {
            NewClear();
            String mem_no = Hidmem_no.Value;
            dw_criteria.SetItemString(1, "as_memberno", mem_no);
            DwUtil.RetrieveDDDW(dw_criteria, "memb_refno", "criteria.pbl", mem_no);
        }
        private void NewClear()
        {
            dw_criteria.Reset();
            dw_criteria.InsertRow(0);
            dw_criteria.SetItemDateTime(1, "reqloan_date", state.SsWorkDate);
            DwUtil.RetrieveDDDW(dw_criteria, "memb_refno", "criteria.pbl", null);
            dw_criteria.SetItemString(1, "position_all", "");
            dw_criteria.SetItemString(1, "position_user", "");
            dw_criteria.SetItemString(1, "note_1", "");
            dw_criteria.SetItemString(1, "note_2", "");
            dw_criteria.SetItemString(1, "note_3", "");
            tdw_criteria.Eng2ThaiAllRow();
        }
        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            //DateTime start_date = dw_criteria.GetItemDateTime(1, "start_date");
            //DateTime end_date = dw_criteria.GetItemDateTime(1, "end_date");
            String reqloan_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "reqloan_tdate", null);
            //String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            String as_memberno = dw_criteria.GetItemString(1, "as_memberno");
            String resigncause = dw_criteria.GetItemString(1, "resigncause");
            String memb_refno = dw_criteria.GetItemString(1, "memb_refno");
            String position_name = dw_criteria.GetItemString(1, "position_name");
            String position_user = dw_criteria.GetItemString(1, "position_user");
            String position_all = dw_criteria.GetItemString(1, "position_all");
            String note_1 = dw_criteria.GetItemString(1, "note_1");
            String note_2 = dw_criteria.GetItemString(1, "note_2");
            String note_3 = dw_criteria.GetItemString(1, "note_3");

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            //lnv_helper.AddArgument(start_date.ToString("yyyy-MM-dd", WebUtil.EN), ArgumentType.DateTime);
            //lnv_helper.AddArgument(end_date.ToString("yyyy-MM-dd", WebUtil.EN), ArgumentType.DateTime);
            lnv_helper.AddArgument(reqloan_date, ArgumentType.DateTime);
            //lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(as_memberno, ArgumentType.String);
            lnv_helper.AddArgument(resigncause, ArgumentType.String);
            lnv_helper.AddArgument(memb_refno, ArgumentType.String);
            lnv_helper.AddArgument(position_name, ArgumentType.String);
            lnv_helper.AddArgument(position_user, ArgumentType.String);
            lnv_helper.AddArgument(position_all, ArgumentType.String);
            lnv_helper.AddArgument(note_1, ArgumentType.String);
            lnv_helper.AddArgument(note_2, ArgumentType.String);
            lnv_helper.AddArgument(note_3, ArgumentType.String);


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
                //NewClear();
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
