using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using CoreSavingLibrary;
using DataLibrary;


namespace Saving.CriteriaIReport.u_cri_coopid_date
{
    public partial class u_cri_coopid_date : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;

        public void InitJsPostBack()
        {

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
                ReportName = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName = "[" + rid + "]";
            }
            Label1.Text = ReportName;
           
            if (!IsPostBack)
            {
                adtm_date.Text = state.SsWorkDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            try
            {
                iReportArgument arg = new iReportArgument();
 
                string[] adtm_date1 = adtm_date.Text.Split('/');
                string as_adtm_date = adtm_date1[0] + "/" + adtm_date1[1] + "/" + (Convert.ToDecimal(adtm_date1[2]) - 543);
                DateTime adtm_date_new = new DateTime();
                try
                {
                    adtm_date_new = DateTime.ParseExact(as_adtm_date, "dd/MM/yyyy", null);
                }
                catch{}
                arg.Add("adtm_date", iReportArgumentType.Date, adtm_date_new);
                arg.Add("as_coop", iReportArgumentType.String, state.SsCoopControl);
                //iReportBuider report;
                iReportBuider report = new iReportBuider(this, arg);
                //report.AddCriteria("rpt01235_deposit_tranall_bank_excel", "open report excel", ReportType.xls_full, arg);
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