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
    public partial class u_cri_coopid_date_rdepttype_recppaytype_ruserid : PageWebSheet, WebSheet
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
        {  //op
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("select_date", "select_tdate");
            //tdw_criteria.Add("end_date", "end_tdate");

        }

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
                DwUtil.RetrieveDDDW(dw_criteria, "select_coop", "criteria.pbl", state.SsCoopControl);
                dw_criteria.SetItemString(1, "select_coop", state.SsCoopId);
                dw_criteria.SetItemDateTime(1, "select_date", state.SsWorkDate);
                dw_criteria.SetItemString(1, "select_tdate", "");
                string[] minmax = ReportUtil.GetMinMaxDepttype();
                dw_criteria.SetItemString(1, "start_depttype", minmax[0]);
                dw_criteria.SetItemString(1, "end_depttype", minmax[1]);
                dw_criteria.SetItemString(1, "start_itemtype", " ");
                dw_criteria.SetItemString(1, "end_itemtype", " ");
                dw_criteria.SetItemString(1, "start_userid", "0");
                dw_criteria.SetItemString(1, "end_userid", "0");
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
            throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            String coop_id = dw_criteria.GetItemString(1, "select_coop");
            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "select_tdate", null);
            String start_dp_type = dw_criteria.GetItemString(1, "start_depttype");
            String end_dp_type = dw_criteria.GetItemString(1, "end_depttype");
            String start_itemtype = dw_criteria.GetItemString(1, "start_itemtype");
            String end_itemtype = dw_criteria.GetItemString(1, "end_itemtype");
            String start_userid = dw_criteria.GetItemString(1, "start_userid");
            String end_userid = dw_criteria.GetItemString(1, "end_userid");
            string[] rappayminmax = ReportUtil.GetMinMaxRappaytype();
            string[] userminmax = ReportUtil.GetMinMaxUsertype();

            if (start_itemtype == "ฮฮฮ" || start_itemtype == " " || end_itemtype == "ฮฮฮ" || end_itemtype == " ")
            {
                start_itemtype = rappayminmax[0];
                end_itemtype = rappayminmax[1];
            }

            if (start_userid == "0" || end_userid == "0")
            {
                start_userid = userminmax[0];
                end_userid = userminmax[1];
            }

            tdw_criteria.Eng2ThaiAllRow();

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(start_dp_type, ArgumentType.String);
            lnv_helper.AddArgument(end_dp_type, ArgumentType.String);
            lnv_helper.AddArgument(start_itemtype, ArgumentType.String);
            lnv_helper.AddArgument(end_itemtype, ArgumentType.String);
            lnv_helper.AddArgument(start_userid, ArgumentType.String);
            lnv_helper.AddArgument(end_userid, ArgumentType.String);



            //----------------------------------------------------

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
