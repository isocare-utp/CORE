﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_rdate_string
{
    public partial class u_cri_rdate_string : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
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

            if (!IsPostBack)
            {
                //dsMain.DdCoopId();
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
                //dsMain.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            string as_coopid = dsMain.DATA[0].coop_id;
            DateTime date1 = dsMain.DATA[0].start_date;
            DateTime date2 = dsMain.DATA[0].end_date;


            try
            {
                iReportArgument arg = new iReportArgument();
                //as_coopid ตัวแปรใน iReport
                //arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                arg.Add("date1", iReportArgumentType.String, date1.ToString("dd/MM/yyyy"));
                arg.Add("date2", iReportArgumentType.String, date2.ToString("dd/MM/yyyy"));

                iReportBuider report = new iReportBuider(this, arg);
                report.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}