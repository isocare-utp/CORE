using System;
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
using CoreSavingLibrary;

namespace Saving.Criteria
{
    public partial class u_cri_acc_date_acc_rang : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String JsPostRangeType;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            JsPostRangeType = WebUtil.JsPostBack(this, "JsPostRangeType");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        public void WebSheetLoadBegin()
        {
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
            }
            else
            {
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                tdw_criteria.Eng2ThaiAllRow();
                DwUtil.RetrieveDDDW(dw_criteria, "coop_id", "criteria.pbl", state.SsCoopControl);
                dw_criteria.SetItemString(1, "coop_id", state.SsCoopControl);
                dw_criteria.SetItemString(1, "entry_id", state.SsUsername);
                HdUsername.Value = state.SsUsername;
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
            else if (eventArg == "JsPostRangeType")
            {
                decimal range_type = dw_criteria.GetItemDecimal(1, "range_type");

                if (range_type == 1)
                {
                    dw_criteria.SetItemDecimal(1, "range_type", 2);
                }
                else if (range_type == 2)
                {
                    dw_criteria.SetItemDecimal(1, "range_type", 1);
                }
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
            String start_vcno = "";
            String end_vcno = "";
            String entry_id = "";

            try { }
            catch { }
            try { start_vcno = dw_criteria.GetItemString(1, "start_vcno"); }
            catch { start_vcno = ""; }
            try { end_vcno = dw_criteria.GetItemString(1, "end_vcno"); }
            catch { end_vcno = ""; }
            try { entry_id = dw_criteria.GetItemString(1, "entry_id"); }
            catch { entry_id = ""; }
            
            String account_id = "";
            String sql_txt = "select cash_account_code from accconstant";
            DataTable dt = WebUtil.Query(sql_txt);

            decimal fixuser_type = dw_criteria.GetItemDecimal(1, "fixuser_type");
            decimal range_type = dw_criteria.GetItemDecimal(1, "range_type");
           

            if (dt.Rows.Count > 0)
            {
                account_id = dt.Rows[0][0].ToString().Trim();
            }
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.


            ReportHelper lnv_helper = new ReportHelper();

            //check to call report
            if (fixuser_type == 1 && range_type == 1)  // fix user และ เลือกตามวันที่
            {
                rid = "[VCH0012]";
                lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
                lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
                lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);
                lnv_helper.AddArgument(entry_id, ArgumentType.String);
            }
            else if (fixuser_type == 1 && range_type == 2)  // fix user เลือกตามเลขที่ Voucher
            {
                rid = "[VCH0011]";
                lnv_helper.AddArgument(start_vcno, ArgumentType.String);
                lnv_helper.AddArgument(end_vcno, ArgumentType.String);
                lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);
                lnv_helper.AddArgument(entry_id, ArgumentType.String);
            }
            else if (fixuser_type == 0 && range_type == 1)   // ไม่ fix user เลือกตามวันที่
            {
                rid = "[VCH0010]";
                lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
                lnv_helper.AddArgument(end_date, ArgumentType.DateTime);
                lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);
            }
            else if (fixuser_type == 0 && range_type == 2)   // ไม่ fix user เลือกตาม เลขที่ Voucher
            {
                rid = "[VCH0013]";
                lnv_helper.AddArgument(start_vcno, ArgumentType.String);
                lnv_helper.AddArgument(end_vcno, ArgumentType.String);
                lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);
            }
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
