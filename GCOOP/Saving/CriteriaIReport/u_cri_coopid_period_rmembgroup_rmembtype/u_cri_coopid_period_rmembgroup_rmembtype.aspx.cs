﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_period_rmembgroup_rmembtype
{
    public partial class u_cri_coopid_period_rmembgroup_rmembtype : PageWebReport, WebReport
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
                dsMain.DdCoopId();
                dsMain.DATA[0].COOP_ID = state.SsCoopId;
                dsMain.DATA[0].YEAR = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.DATA[0].ROUND = "01";
                dsMain.DdSmembgroup();
                dsMain.DdEmembgroup();
                dsMain.DdSMembtype();
                dsMain.DdEMembtype();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            int li_period = 0;
            string as_coopid = dsMain.DATA[0].COOP_ID;
            string as_smembgroup = dsMain.DATA[0].S_MEMBGROUP;
            string as_emembgroup = dsMain.DATA[0].E_MEMBGROUP;
            string as_smembtype = dsMain.DATA[0].S_MEMBTYPE;
            string as_emembtype = dsMain.DATA[0].E_MEMBTYPE;
            int an_period = Convert.ToInt32(dsMain.DATA[0].YEAR + dsMain.DATA[0].MONTH.ToString("00")+dsMain.DATA[0].ROUND);

            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("as_smembgroup", iReportArgumentType.String, as_smembgroup);
                arg.Add("as_emembgroup", iReportArgumentType.String, as_emembgroup);
                arg.Add("as_smembtype", iReportArgumentType.String, as_smembtype);
                arg.Add("as_emembtype", iReportArgumentType.String, as_emembtype);
                arg.Add("as_recvperiod", iReportArgumentType.String, Convert.ToString(an_period));

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