using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_date_grp
{
    public partial class u_cri_coopid_date_grp : PageWebReport, WebReport
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
                dsMain.DATA[0].as_year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.DATA[0].as_year2 = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.DATA[0].coopid = state.SsCoopControl;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {
            //int li_period = 0;
            string as_coopid = dsMain.DATA[0].coop_id;
            int as_year = Convert.ToInt32(dsMain.DATA[0].as_year) - 543;
            int as_year2 = Convert.ToInt32(dsMain.DATA[0].as_year2) - 543;
            string as_month = dsMain.DATA[0].as_month.ToString("00");
            string as_month2 = dsMain.DATA[0].as_month2.ToString("00");
            decimal as_date = dsMain.DATA[0].as_date;
            decimal as_date2 = dsMain.DATA[0].as_date2;

            try
            {
                iReportArgument arg = new iReportArgument();
                //as_coopid ตัวแปรใน iReport
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                arg.Add("as_date", iReportArgumentType.String, as_date);
                arg.Add("as_date2", iReportArgumentType.String, as_date2);
                arg.Add("as_month", iReportArgumentType.String, as_month);
                arg.Add("as_month2", iReportArgumentType.String, as_month2);
                arg.Add("as_year", iReportArgumentType.String, as_year);
                arg.Add("as_year2", iReportArgumentType.String, as_year2);
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