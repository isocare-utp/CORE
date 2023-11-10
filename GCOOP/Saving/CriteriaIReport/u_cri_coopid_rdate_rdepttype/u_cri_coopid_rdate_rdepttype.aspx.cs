using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_rdate_rdepttype
{
    public partial class u_cri_coopid_rdate_rdepttype : PageWebReport, WebReport
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
                dsMain.DATA[0].COOP_ID = state.SsCoopId;
                dsMain.DATA[0].ENTRY_SDATE = state.SsWorkDate;
                dsMain.DATA[0].ENTRY_EDATE = state.SsWorkDate;
                dsMain.DdDepttype();
                dsMain.DdDepttypee();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {

            try
            {
                DateTime sentry_date = dsMain.DATA[0].ENTRY_SDATE;
                DateTime eentry_date = dsMain.DATA[0].ENTRY_EDATE;
                String start_depttype = dsMain.DATA[0].depttype_scode;
                String end_depttype = dsMain.DATA[0].depttype_ecode;
                       

                iReportArgument arg = new iReportArgument();
                arg.Add("adtm_eentry_date", iReportArgumentType.Date, eentry_date);
                arg.Add("adtm_sentry_date", iReportArgumentType.Date, sentry_date);                
                arg.Add("as_end_type", iReportArgumentType.String, end_depttype);
                arg.Add("as_scoopid", iReportArgumentType.String, state.SsCoopId);
                arg.Add("as_start_type", iReportArgumentType.String, start_depttype);



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