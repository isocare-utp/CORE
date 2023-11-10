﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_rperiod
{
    public partial class u_cri_rperiod : PageWebReport, WebReport
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
                dsMain.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {
            //string as_coopid = dsMain.DATA[0].coop_id;
            int an_period = Convert.ToInt32(dsMain.DATA[0].year)-543;
            int s_month = Convert.ToInt32(dsMain.DATA[0].s_month.ToString("00"));
            int e_month = Convert.ToInt32(dsMain.DATA[0].e_month.ToString("00"));


            try
            {
                iReportArgument arg = new iReportArgument();
                //arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("ai_cal_smonth", iReportArgumentType.String, Convert.ToString(s_month));
                arg.Add("ai_cal_emonth", iReportArgumentType.String, Convert.ToString(e_month));
                arg.Add("ai_cal_year", iReportArgumentType.String, Convert.ToString(an_period));


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