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
    public partial class u_cri_div_coopid_year_rtyplon_step : PageWebSheet, WebSheet
    {

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");

        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            DwUtil.RetrieveDDDW(dw_criteria, "coop_id", "criteria.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_criteria, "start_memgrp_1", "criteria.pbl", "");
            DwUtil.RetrieveDDDW(dw_criteria, "end_memgrp_1", "criteria.pbl", "");

            if (IsPostBack)
            {
                //dw_criteria.RestoreContext();
                this.RestoreContextDw(dw_criteria);
            }
            else
            {
                //default values.
                // int year = (DateTime.Now.Year) + 543;
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemString(1, "coop_id", state.SsCoopControl);
                JsGetYear();
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
            dw_criteria.SaveDataCache();
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String satang_type = "";
            String round_type = "1";
            decimal truncate_pos_amt = 0;
            decimal round_pos_amt = 0;
            
            try
            {
                String sql = @"select satang_type , truncate_pos_amt , round_type , round_pos_amt                
                from (
	                select satang_type , truncate_pos_amt , round_type , round_pos_amt , 1 as sort
	                from cmroundmoney
	                where coop_id = {0}
	                and applgroup_code = 'DIV'
	                and function_code = 'ALL'
	                and use_flag = 1
	                union
	                select satang_type , truncate_pos_amt , round_type , round_pos_amt , 2 as sort
	                from cmroundmoney
	                where coop_id = {1}
	                and applgroup_code = 'DIV'
	                and function_code = 'roundavg'
	                and use_flag = 1
                ) cmrd
                where rownum = 1";

                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, state.SsCoopControl);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    satang_type = dt.GetString("satang_type");                    
                    round_type = dt.GetString("round_type");
                    truncate_pos_amt = dt.GetDecimal("truncate_pos_amt");
                    round_pos_amt = dt.GetDecimal("round_pos_amt");

                    // มี 7 หลัก
                    if (round_type == "1")
                    {
                        rid = "DVAVEST05_1";
                    }
                    else
                    {
                        rid = "DVAVEST05_2";
                    }

                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }


            String div_year = "";
            Decimal step = 0;
            try
            {
                div_year = Hddiv_year.Value.Trim();
            }
            catch { div_year = Convert.ToString(DateTime.Now.Year + 543); }

            try
            {
                step = Convert.ToDecimal(Hdstep.Value);
            }
            catch { step = 0; }

            String year = dw_criteria.GetItemString(1, "year");
            string start_memgrp = dw_criteria.GetItemString(1, "start_memgrp").Trim();
            string end_memgrp = dw_criteria.GetItemString(1, "end_memgrp").Trim();

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(state.SsCoopId, ArgumentType.String);
            lnv_helper.AddArgument(div_year, ArgumentType.String);
            lnv_helper.AddArgument(start_memgrp, ArgumentType.String);
            lnv_helper.AddArgument(end_memgrp, ArgumentType.String);
            lnv_helper.AddArgument(step.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(satang_type, ArgumentType.String);
            lnv_helper.AddArgument(round_type, ArgumentType.String);
            lnv_helper.AddArgument(truncate_pos_amt.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(round_pos_amt.ToString(), ArgumentType.Number);

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

        private void JsGetYear()
        {
            //  Sta ta = new Sta(sqlca.ConnectionString);
            int account_year = 0;
            try
            {
                String sql = @"select max(current_year) from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(current_year)"));
                    dw_criteria.SetItemString(1, "year", Convert.ToString(account_year));
                    Hddiv_year.Value = Convert.ToString(account_year);
                    //hd
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
                dw_criteria.SetItemString(1, "year", account_year.ToString());
            }
        }

        #endregion
    }
}