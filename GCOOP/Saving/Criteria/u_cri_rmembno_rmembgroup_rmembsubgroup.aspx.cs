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
    public partial class u_cri_rmembno_rmembgroup_rmembsubgroup : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        protected String checkFlag;
        protected String checkFlag1;
        protected String checkFlag2;
        protected String checkFlag3;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            checkFlag = WebUtil.JsPostBack(this, "checkFlag");
            checkFlag1 = WebUtil.JsPostBack(this, "checkFlag1");
            checkFlag2 = WebUtil.JsPostBack(this, "checkFlag2");
            checkFlag3 = WebUtil.JsPostBack(this, "checkFlag3");
          
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();

            //this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            DwUtil.RetrieveDDDW(dw_criteria, "as_startgroup", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_endgroup", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_startsubgrp", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "as_endsubgrp", "criteria.pbl", null);
           
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                string[] minmax = ReportUtil.GetMinMaxMembgroup();
                dw_criteria.SetItemString(1, "as_startgroup", minmax[0]);
                dw_criteria.SetItemString(1, "as_endgroup", minmax[1]);
                string[] minmax1 = ReportUtil.GetMinMaxMembsubgroup();
                dw_criteria.SetItemString(1, "as_startsubgrp", minmax1[0]);
                dw_criteria.SetItemString(1, "as_endsubgrp", minmax1[1]);
               
                dw_criteria.SetItemString(1, "as_startmem","");
                dw_criteria.SetItemString(1, "as_endmem", "");
                //tdw_criteria.Eng2ThaiAllRow();
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
            else if (eventArg == "checkFlag1")
            {
                CheckFlag1();

            }
            else if (eventArg == "checkFlag2")
            {
                CheckFlag2();

            }
            else if (eventArg == "checkFlag3")
            {
                CheckFlag3();

            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
        private void CheckFlag()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "as_startgroup", HdStGroup.Value);

        }
        private void CheckFlag1()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "as_endgroup", HdEnGroup.Value);

        }
        private void CheckFlag2()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "as_startsubgrp", HdStsubGroup.Value);

        }
        private void CheckFlag3()
        {
            //ให้หน้า page refresh
            dw_criteria.SetItemString(1, "as_endsubgrp", HdEnsubGroup.Value);

        }
        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.


            String as_startgroup = dw_criteria.GetItemString(1, "as_startgroup");
            String as_endgroup = dw_criteria.GetItemString(1, "as_endgroup");
            String as_startsubgrp = dw_criteria.GetItemString(1, "as_startsubgrp");
            String as_endsubgrp = dw_criteria.GetItemString(1, "as_endsubgrp");
            String as_startmem = dw_criteria.GetItemString(1, "as_startmem");
            String as_endmem = dw_criteria.GetItemString(1, "as_endmem");
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(as_startgroup, ArgumentType.String);
            lnv_helper.AddArgument(as_endgroup, ArgumentType.String);
            lnv_helper.AddArgument(as_startsubgrp, ArgumentType.String);
            lnv_helper.AddArgument(as_endsubgrp, ArgumentType.String);
            lnv_helper.AddArgument(as_startmem, ArgumentType.String);
            lnv_helper.AddArgument(as_endmem, ArgumentType.String);

            //----------------------------------------------------------------

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
        private string Convert(string p)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
