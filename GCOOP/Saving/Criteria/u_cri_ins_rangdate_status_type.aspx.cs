﻿using System;
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
    public partial class u_cri_ins_rangdate_status_type : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        public void WebSheetLoadBegin()
        {
            DwUtil.RetrieveDDDW(dw_criteria, "as_instype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "ai_startmbtype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "ai_endmbtype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_startemtype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_endemtype", "criteria.pbl", null);
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
            }
            else
            {
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "start_date", DateTime.Now);  //state.SsWorkDate.AddYears(-1)
                dw_criteria.SetItemDateTime(1, "end_date", DateTime.Now);
                //dw_criteria.SetItemString(1, "start_tdate", "");
                //dw_criteria.SetItemString(1, "end_tdate", "");
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
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
        }

        #endregion

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            string reqstatus = dw_criteria.GetItemString(1, "req_status");
            String typecode = dw_criteria.GetItemString(1, "as_instype");
            String startmbtype = dw_criteria.GetItemString(1, "ai_startmbtype");
            String endmbtype = dw_criteria.GetItemString(1, "ai_endmbtype");
            String startemtype = dw_criteria.GetItemString(1, "as_startemtype");
            String endemtype = dw_criteria.GetItemString(1, "as_endemtype");
            String startcost = dw_criteria.GetItemString(1, "as_startcost");
            String endcost = dw_criteria.GetItemString(1, "as_endcost");

            String astype = dw_criteria.GetItemString(1, "as_startapp");
            string startapp, endapp;
            startapp = (astype == "01") ? "01" : "03";
            endapp = (astype == "03") ? "04" : "02";

            System.Globalization.CultureInfo den = new System.Globalization.CultureInfo("en-US");
            String crrDate = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);//DateTime.Now.ToString("dd/MM/yyyy");
            string[] str = crrDate.Split('/');
            DateTime dm = Convert.ToDateTime(new DateTime(Convert.ToInt32(str[2]), Convert.ToInt32(str[1]), Convert.ToInt32(str[0])), den);
            string[] xstr = end_date.Split('/');
            DateTime dx = Convert.ToDateTime(new DateTime(Convert.ToInt32(xstr[2]), Convert.ToInt32(xstr[1]), Convert.ToInt32(xstr[0])), den);

            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(dm.ToString("dd/MM/yyyy"), ArgumentType.DateTime);
            lnv_helper.AddArgument(dx.ToString("dd/MM/yyyy"), ArgumentType.DateTime);
            lnv_helper.AddArgument(reqstatus.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(typecode, ArgumentType.String);

            lnv_helper.AddArgument(startmbtype.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(endmbtype.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(startemtype, ArgumentType.String);
            lnv_helper.AddArgument(endemtype, ArgumentType.String);


            lnv_helper.AddArgument(startcost, ArgumentType.String);
            lnv_helper.AddArgument(endcost, ArgumentType.String);

            lnv_helper.AddArgument(startapp, ArgumentType.String);
            lnv_helper.AddArgument(endapp, ArgumentType.String);

           
            //----------------------------------------------------


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
    }
}
