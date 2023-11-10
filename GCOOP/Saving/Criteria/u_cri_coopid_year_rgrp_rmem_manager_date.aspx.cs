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
    public partial class u_cri_coopid_year_rgrp_rmem_manager_date : PageWebSheet, WebSheet
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
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("operate_date", "operate_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            DwUtil.RetrieveDDDW(dw_criteria, "coop_id", "criteria.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup", "criteria.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup", "criteria.pbl", state.SsCoopControl);
            //DwUtil.RetrieveDDDW(dw_criteria, "start_objective_1", "criteria.pbl", null);
            //DwUtil.RetrieveDDDW(dw_criteria, "end_objective_1", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "manager", "criteria.pbl", null);

            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemString(1, "coop_id", state.SsCoopId);
                JsGetYear();
                string[] minmax = ReportUtil.GetMinMaxMembgroup();
                dw_criteria.SetItemString(1, "start_membgroup", minmax[0]);
                dw_criteria.SetItemString(1, "end_membgroup", minmax[1]);
                string[] minmax_memno = ReportUtil.GetMinMaxMembno();
                dw_criteria.SetItemString(1, "start_membno", minmax_memno[0]);
                dw_criteria.SetItemString(1, "end_membno", minmax_memno[1]);
                dw_criteria.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                tdw_criteria.Eng2ThaiAllRow();

                String manager = "";
               
                try
                {
                    Sta ta = new Sta(state.SsConnectionString);
                    String sql = "";
                    sql = @"SELECT manager
                    FROM cmcoopconstant  
                    WHERE ( coop_no = '" + state.SsCoopId + @"' ) ";
                    Sdt dt = ta.Query(sql);

                    manager = dt.Rows[0]["manager"].ToString();
                    ta.Close();
                }
                catch
                {
                    dw_criteria.SetItemString(1, "manager", "");
                    dw_criteria.SetItemString(1, "position", "");
                }

                dw_criteria.SetItemString(1, "manager", manager);
                dw_criteria.SetItemString(1, "position", "ผู้จัดการ");

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
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String start_membgroup = "", end_membgroup = "";
            string start_membno = "", end_membno = "", manager = "";
            String coop_id = dw_criteria.GetItemString(1, "coop_id");
            decimal ai_year = dw_criteria.GetItemDecimal(1, "ai_year");
           
            try
            {
                start_membgroup = dw_criteria.GetItemString(1, "start_membgroup").Trim();
            }
            catch { start_membgroup = ""; }
            try
            {
                end_membgroup = dw_criteria.GetItemString(1, "end_membgroup").Trim();
            }
            catch { end_membgroup = ""; }
            try
            {
                start_membno = dw_criteria.GetItemString(1, "start_membno");
            }
            catch { start_membno = ""; }
            try
            {
                end_membno = dw_criteria.GetItemString(1, "end_membno");
            }
            catch { end_membno = ""; }

            string[] minmax_memno = ReportUtil.GetMinMaxMembno();
            string[] minmax = ReportUtil.GetMinMaxMembgroup();
           
            if (start_membgroup == "" || start_membgroup == null)
            {
                start_membgroup = minmax[0];
            }
            if (end_membgroup == "" || end_membgroup == null)
            {
                end_membgroup = minmax[1];
            }

            if (start_membno == "" || start_membno == null)
            {
                start_membno = minmax_memno[0];
            }
            if (end_membno == "" || end_membno == null)
            {
                end_membno = minmax_memno[1];
            }
            start_membno = WebUtil.MemberNoFormat(start_membno);
            end_membno = WebUtil.MemberNoFormat(end_membno);

            try
            {
                manager = dw_criteria.GetItemString(1, "manager");
            }
            catch { manager = ""; }

            String position = "";
            try
            {
                position = dw_criteria.GetItemString(1, "position");
            }
            catch { position = ""; }
           
            String operate_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "operate_tdate", null);
           
            
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(ai_year.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(start_membgroup, ArgumentType.String);
            lnv_helper.AddArgument(end_membgroup, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(end_membno, ArgumentType.String);
            lnv_helper.AddArgument(manager, ArgumentType.String);
            lnv_helper.AddArgument(position, ArgumentType.String);
            lnv_helper.AddArgument(operate_date, ArgumentType.DateTime);

            //-------------------------------------------------------

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
        #endregion

        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

        private void JsGetYear()
        {
            int account_year = 0;
            try
            {
                String sql = @"select max(current_year) from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(current_year)"));
                    dw_criteria.SetItemDecimal(1, "ai_year", account_year);
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
                dw_criteria.SetItemDecimal(1, "ai_year", account_year);

            }
        }
        #endregion
    }
}
