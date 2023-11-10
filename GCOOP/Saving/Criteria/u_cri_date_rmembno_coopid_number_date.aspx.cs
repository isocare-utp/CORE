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
    public partial class u_cri_date_rmembno_coopid_number_date : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String postSetStartMemberno;
        protected String postSetEndMemberno;
        protected String post;

        private DwThDate tdw_criteria;
        #region WebSheet Members

        private void JspostSetEndMemberno()
        {
            String membNo = dw_criteria.GetItemString(1, "end_membno");
            membNo = WebUtil.MemberNoFormat(membNo);
            dw_criteria.SetItemString(1, "end_membno", membNo);
        }

        private void JspostSetStartMemberno()
        {
            String membNo = dw_criteria.GetItemString(1, "start_membno");
            membNo = WebUtil.MemberNoFormat(membNo);
            dw_criteria.SetItemString(1, "start_membno", membNo);
        }


        public void InitJsPostBack()
        {
             postSetStartMemberno = WebUtil.JsPostBack(this, "postSetStartMemberno");
              postSetEndMemberno = WebUtil.JsPostBack(this, "postSetEndMemberno");
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            post = WebUtil.JsPostBack(this, "post");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("today_date", "today_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
          

            InitJsPostBack();

            DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup", "criteria.pbl", state.SsCoopId);
            DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup", "criteria.pbl", state.SsCoopId);

            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "today_date", state.SsWorkDate);
                tdw_criteria.Eng2ThaiAllRow();

                string[] minmaxgroup = ReportUtil.GetMinMaxMembgroup();
                dw_criteria.SetItemString(1, "start_membgroup", minmaxgroup[0]);
                dw_criteria.SetItemString(1, "end_membgroup", minmaxgroup[1]);
              
                string[] minmax = ReportUtil.GetMinMaxMembno();
                dw_criteria.SetItemString(1, "start_membno", minmax[0]);
                dw_criteria.SetItemString(1, "end_membno", minmax[1]);
                dw_criteria.SetItemSqlDecimal(1, "number", 1);
                dw_criteria.SetItemString(1, "president", " ");
                dw_criteria.SetItemString(1, "namecheck", " ");
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
            else if (eventArg == "postSetStartMemberno")
            {
                JspostSetStartMemberno();
            }

            else if (eventArg == "postSetEndMemberno")
            {
                JspostSetEndMemberno();
            }
            else if (eventArg == "post")
            {
                DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup_1", "criteria.pbl", null);
                DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup_1", "criteria.pbl", null);
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

            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup").Trim();
            String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup").Trim();
            String start_membno = dw_criteria.GetItemString(1, "start_membno").Trim();
            String end_membno = dw_criteria.GetItemString(1, "end_membno").Trim();
            Decimal number_no = dw_criteria.GetItemDecimal(1, "number");
            String president = dw_criteria.GetItemString(1, "president");
            String namecheck = dw_criteria.GetItemString(1, "namecheck");
            String today_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "today_tdate", null);
            String coop_id = state.SsCoopId;

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(start_membgroup, ArgumentType.String);
            lnv_helper.AddArgument(end_membgroup, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(end_membno, ArgumentType.String);
            lnv_helper.AddArgument(number_no.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(president, ArgumentType.String);
            lnv_helper.AddArgument(namecheck, ArgumentType.String);
            lnv_helper.AddArgument(today_date, ArgumentType.DateTime);


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
