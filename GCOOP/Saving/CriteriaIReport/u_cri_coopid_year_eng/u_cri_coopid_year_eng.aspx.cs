﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_year_eng
{
    public partial class u_cri_coopid_year_eng : PageWebReport, WebReport
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
                dsMain.DATA[0].coop_id = state.SsCoopControl;
                dsMain.DdEmpno();                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {
            int as_year = Convert.ToInt32(dsMain.DATA[0].as_period) - 543  ;
            string as_year2 = Convert.ToString(as_year);

            string as_coopid = dsMain.DATA[0].coop_id;
            string as_period = as_year2;
            //string as_sempno = dsMain.DATA[0].as_sempno;
            //string as_eempno = dsMain.DATA[0].as_eempno; 

            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, as_coopid);
                arg.Add("as_period", iReportArgumentType.String, as_period);
                //arg.Add("as_sempno", iReportArgumentType.String, as_sempno);
                //arg.Add("as_eempno", iReportArgumentType.String, as_eempno);

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