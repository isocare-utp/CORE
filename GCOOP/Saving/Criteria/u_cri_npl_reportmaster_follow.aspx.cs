using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data;

namespace Saving.Criteria
{
    public partial class u_cri_npl_reportmaster_follow : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String JsPostRangeType;
        protected String JsPostPost;
        private DwThDate tdw_criteria;
        public String outputProcess = "";
        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            JsPostPost = WebUtil.JsPostBack(this, "JsPostPost");
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
                //DwUtil.RetrieveDDDW(dw_criteria, "tyear", "", state.SsCoopControl);
                //dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);
                //dw_criteria.SetItemDateTime(1, "end_date", state.SsWorkDate);
                //tdw_criteria.Eng2ThaiAllRow();
                //DwUtil.RetrieveDDDW(dw_criteria, "coop_id", "criteria.pbl", state.SsCoopControl);
                //dw_criteria.SetItemString(1, "coop_id", state.SsCoopControl);
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
            int tyear = 0, month = 0, seq = 0;
            try
            {
                try
                {
                    DwUtil.RetrieveDDDW(dw_criteria, "tyear", "", state.SsCoopControl);
                }
                catch { }
                try
                {
                    tyear = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "tyear"));
                }
                catch { }
            }
            catch { }
            if (tyear > 0)
            {
                try
                {
                    DwUtil.RetrieveDDDW(dw_criteria, "month", "", state.SsCoopControl, tyear);
                }
                catch { }
                try
                {
                    month = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "month"));
                }
                catch { }
            }
            else
            {
                month = 0;
                try
                {
                    dw_criteria.SetItemNull(1, "month");
                }
                catch { }
            }
            if (month > 0)
            {
                try
                {
                    DwUtil.RetrieveDDDW(dw_criteria, "seq_no", "", state.SsCoopControl, tyear, month);
                }
                catch { }
                try
                {
                    seq = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "seq_no"));
                }
                catch { }
            }
            else
            {
                seq = 0;
                try
                {
                    dw_criteria.SetItemNull(1, "seq_no");
                }
                catch { }
            }
            if (seq > 0)
            {
                try
                {
                    string sql = "select entry_date from lnnplreportmaster where coop_id = {0} and year_th = {1} and \"MONTH\" = {2} and seq_no = {3} and rownum <= 1";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, tyear, month, seq);
                    DataTable dt = WebUtil.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        string showDate = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd/MM/yyyy HH:mm", WebUtil.TH);
                        dw_criteria.SetItemString(1, "create_time", showDate);
                    }
                }
                catch
                {
                    dw_criteria.SetItemNull(1, "create_time");
                }
            }
            else
            {
                dw_criteria.SetItemNull(1, "create_time");
            }
            dw_criteria.SaveDataCache();
        }

        #endregion

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            decimal tyear = 0, month = 0, seq_no = 0;

            try
            {
                tyear = dw_criteria.GetItemDecimal(1, "tyear");
            }
            catch { }
            try
            {
                month = dw_criteria.GetItemDecimal(1, "month");
            }
            catch { }
            try
            {
                seq_no = dw_criteria.GetItemDecimal(1, "seq_no");
            }
            catch { }

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(state.SsCoopControl, ArgumentType.String);
            lnv_helper.AddArgument(tyear.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(month.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(seq_no.ToString(), ArgumentType.Number);

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
