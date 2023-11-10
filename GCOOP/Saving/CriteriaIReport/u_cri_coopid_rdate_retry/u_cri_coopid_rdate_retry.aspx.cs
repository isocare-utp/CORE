using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Globalization;

namespace Saving.CriteriaIReport.u_cri_coopid_rdate_retry
{
    public partial class u_cri_coopid_rdate_retry : PageWebReport, WebReport
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
                DateTime now = DateTime.Now;
                decimal year = now.Year;
                year = year + 543;
                dsMain.DATA[0].as_year = Convert.ToString(year);


                string start = "";
                start = state.SsWorkDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                string[] tmpdate_start = start.Split('/');
                string as_date = "10" + "/" + "01" + "/" + (year - 61);
                dsMain.DATA[0].adtm_sdate = as_date;
                
                string as_date_show = "01" + "/" + "10" + "/" + (year - 61);
                dsMain.DATA[0].adtm_sdate = as_date_show;

                string end = "";
                end = state.SsWorkDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                string[] tmpdate_end = end.Split('/');
                string as_date_end = "09" + "/" + "30" + "/" + (year - 60);
                dsMain.DATA[0].adtm_edate = as_date_end;
                
                string as_date_end_show = "30" + "/" + "09" + "/" + (year - 60);
                dsMain.DATA[0].adtm_edate = as_date_end_show;
                
                
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {

            string[] tmpdate_start_1 = dsMain.DATA[0].adtm_sdate.Split('/');
            string adtm_sdate = tmpdate_start_1[1] + "/" + tmpdate_start_1[0] + "/" + ((Convert.ToDecimal(tmpdate_start_1[2]) - 543) ); //

            string[] tmpdate_end_1 = dsMain.DATA[0].adtm_edate.Split('/');
            string adtm_edate = tmpdate_end_1[1] + "/" + tmpdate_end_1[0] + "/" + ((Convert.ToDecimal(tmpdate_end_1[2]) - 543) ); //
            try
            {
                iReportArgument arg = new iReportArgument();
                //as_coopid ตัวแปรใน iReport
                arg.Add("coop_id", iReportArgumentType.String, state.SsCoopId);
                arg.Add("adtm_sdate", iReportArgumentType.String, adtm_sdate);
                arg.Add("adtm_edate", iReportArgumentType.String, adtm_edate);
                arg.Add("as_yaer", iReportArgumentType.String, Convert.ToString(dsMain.DATA[0].as_year));
                
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