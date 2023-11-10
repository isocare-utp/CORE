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
using CoreSavingLibrary.WcfNKeeping;
using DataLibrary;

namespace Saving.Criteria
{
    public partial class u_cri_rperiod_membgroup_prc : PageWebSheet, WebSheet
    {
        private n_keepingClient kpService;

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        protected String post;
        protected String postShowReport;

        #region WebSheet Members

        private void JspostShowReport()
        { 
                    //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
             //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup");
            String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup");
            String year = dw_criteria.GetItemString(1, "year");
            String month = dw_criteria.GetItemString(1, "month");
            String period = year + month;
            Decimal report_choice = dw_criteria.GetItemDecimal(1, "report_choice");

            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(period, ArgumentType.String);
            lnv_helper.AddArgument(start_membgroup, ArgumentType.String);
            lnv_helper.AddArgument(end_membgroup, ArgumentType.String);

            //****************************************************************

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
           
       
        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            HdRunProcess.Value = "False";

            runProcess = WebUtil.JsPostBack(this, "runProcess");
            post = WebUtil.JsPostBack(this, "post");
            postShowReport = WebUtil.JsPostBack(this, "postShowReport");

            tdw_criteria = new DwThDate(dw_criteria, this);
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            kpService = wcf.NKeeping;

            //InitJsPostBack();
            DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup", "criteria.pbl", null);
            
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                int year, month;

                dw_criteria.InsertRow(0);
                string[] minmax = ReportUtil.GetMinMaxMembgroup();
                dw_criteria.SetItemString(1, "start_membgroup", minmax[0]);
                dw_criteria.SetItemString(1, "end_membgroup", minmax[1]);


                String kp_month = Convert.ToString(DateTime.Now.Month);
                if(kp_month.Length != 2)
                {
                    kp_month = "0" + kp_month;
                }

                dw_criteria.SetItemString(1, "year", Convert.ToString(DateTime.Now.Year + 543));
                dw_criteria.SetItemString(1, "month", kp_month);
        

               
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
            else if (eventArg == "post")
            {
                DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup_1", "criteria.pbl", null);
                DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup_1", "criteria.pbl", null);
            }
            else if (eventArg == "postShowReport")
            {
                JspostShowReport();
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
            String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup");
            String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup");
            String year = dw_criteria.GetItemString(1, "year");
            String month = dw_criteria.GetItemString(1, "month");
            String period = year + month;
            //Decimal report_choice = dw_criteria.GetItemDecimal(1, "report_choice");
            //if (report_choice == 1)
            //{
            //    // เรียกให้มีการประมวลผลรายงาน
            //    try
            //    {
            //        str_keep astr_keep = new str_keep();
            //        astr_keep.procreport_choice = "001";
            //        astr_keep.recv_period = period;
            //        kpService.RunKpProcessReport(state.SsWsPass, astr_keep, state.SsApplication, state.CurrentPage);
            //        HdRunProcess.Value = "True";

            //        //=========
            //    //    //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            //    //    ReportHelper lnv_helper = new ReportHelper();
            //    //    lnv_helper.AddArgument(period, ArgumentType.String);
            //    //    lnv_helper.AddArgument(start_membgroup, ArgumentType.String);
            //    //    lnv_helper.AddArgument(end_membgroup, ArgumentType.String);

            //    //    //****************************************************************

            //    //    //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            //    //    String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            //    //    pdfFileName += "_" + gid + "_" + rid + ".pdf";
            //    //    pdfFileName = pdfFileName.Trim();

            //    //    //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            //    //    try
            //    //    {
            //    //        //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
            //    //        //String criteriaXML = lnv_helper.PopArgumentsXML();
            //    //        //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
            //    //        String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
            //    //        if (li_return == "true")
            //    //        {
            //    //            HdOpenIFrame.Value = "True";
            //    //        }
            //    //    }
            //    //    catch (Exception ex)
            //    //    {
            //    //        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //    //        return;
            //    //    }
            //    }
            //    catch(Exception ex)
            //    {
            //        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            //    }
            //}

            //else //ไม่ได้มีการ check ให้มีการประมวลผลรายงานจัดเก็บ ออกรายงานเฉย ๆ
            //{

                //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
                ReportHelper lnv_helper = new ReportHelper();
                lnv_helper.AddArgument(period, ArgumentType.String);
                lnv_helper.AddArgument(start_membgroup, ArgumentType.String);
                lnv_helper.AddArgument(end_membgroup, ArgumentType.String);
                
                //****************************************************************

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
          //  }

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
