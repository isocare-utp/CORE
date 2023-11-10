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
    public partial class u_cri_ins_alltype : PageWebSheet, WebSheet
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
            //tdw_criteria = new DwThDate(dw_criteria, this);
            //tdw_criteria.Add("start_date", "start_tdate");
            //tdw_criteria.Add("end_date", "end_tdate");


        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            InitJsPostBack();

            DwUtil.RetrieveDDDW(dw_criteria, "start_instype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_instype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "ai_startmbtype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "ai_endmbtype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_startemtype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_endemtype", "criteria.pbl", null);
            //DwUtil.RetrieveDDDW(dw_criteria, "start_instype", "criteria.pbl", null);
            //DwUtil.RetrieveDDDW(dw_criteria, "end_instype", "criteria.pbl", null);

            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                //dw_criteria.SetItemDateTime(1, "start_date", DateTime.Now);
                //dw_criteria.SetItemDateTime(1, "end_date", DateTime.Now);
                //dw_criteria.SetItemString(1, "start_tdate", "");
                //dw_criteria.SetItemString(1, "end_tdate", "");
                //string[] minmax = ReportUtil.GetMinMaxLoantype();
                //dw_criteria.SetItemString(1, "start_instype", minmax[0]);
                //dw_criteria.SetItemString(1, "end_instype", minmax[1]);
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
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion

        #region Report Process

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            //DateTime start_date = dw_criteria.GetItemDateTime(1, "start_date");
            //DateTime end_date = dw_criteria.GetItemDateTime(1, "end_date");
            //String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            //String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            String start_type = dw_criteria.GetItemString(1, "start_instype");
            String end_type = dw_criteria.GetItemString(1, "end_instype");
            String asstmbtype = dw_criteria.GetItemString(1, "ai_startmbtype");
            String asedmbtype = dw_criteria.GetItemString(1, "ai_endmbtype");
            String asstemtype = dw_criteria.GetItemString(1, "as_startemtype");
            String asedemtype = dw_criteria.GetItemString(1, "as_endemtype");


            //String astype = dw_criteria.GetItemString(1, "as_startapp");
            //string startapp, endapp;
            //startapp = (astype == "01") ? "01" : "03";
            //endapp = (astype == "03") ? "04" : "02";

            //System.Globalization.CultureInfo den = new System.Globalization.CultureInfo("en-US");
            //String crrDate = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);//DateTime.Now.ToString("dd/MM/yyyy");
            //string[] str = crrDate.Split('/');
            //DateTime dm = Convert.ToDateTime(new DateTime(Convert.ToInt32(str[2]), Convert.ToInt32(str[1]), Convert.ToInt32(str[0])), den);
            //string[] xstr = end_date.Split('/');
            //DateTime dx = Convert.ToDateTime(new DateTime(Convert.ToInt32(xstr[2]), Convert.ToInt32(xstr[1]), Convert.ToInt32(xstr[0])), den);


            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            //lnv_helper.AddArgument(start_date.ToString("yyyy-MM-dd", WebUtil.EN), ArgumentType.DateTime);
            //lnv_helper.AddArgument(end_date.ToString("yyyy-MM-dd", WebUtil.EN), ArgumentType.DateTime);
            //lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            //lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(start_type, ArgumentType.String);
            lnv_helper.AddArgument(end_type, ArgumentType.String);
            lnv_helper.AddArgument(asstmbtype.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(asedmbtype.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(asstemtype, ArgumentType.String);
            lnv_helper.AddArgument(asedemtype, ArgumentType.String);

            //lnv_helper.AddArgument(startapp, ArgumentType.String);
            //lnv_helper.AddArgument(endapp, ArgumentType.String);


            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                String criteriaXML = lnv_helper.PopArgumentsXML();
                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);

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
