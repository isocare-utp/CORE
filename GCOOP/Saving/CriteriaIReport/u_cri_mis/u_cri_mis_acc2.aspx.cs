using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_mis
{
    public partial class u_cri_mis_acc2 : PageWebReport, WebReport
    {

        protected String app;
        protected String gid;
        protected String rid;

        public void InitJsPostBack()
        {
            #region Report Name
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
            #endregion
        }

        public void WebSheetLoadBegin()
        {
            
        }

        public void CheckJsPostBack(string eventArg)
        {
           
        }

        public void RunReport()
        {
            string moneysheet_code = money_code.Text;
            string month1 = str_month1.Text;
            string year1 = str_year1.Text;
            //DateTime date_s = Convert.ToDateTime("01/01/2015");
            DateTime date_e = Convert.ToDateTime("01/01/2015");
            
            try
            {
                string as_date_chk = "15/" + month1 + "/" + (Convert.ToDecimal(year1) - 543);
                DateTime date_s = Convert.ToDateTime(as_date_chk);

                iReportArgument arg = new iReportArgument();
                arg.Add("moneysheet_code", iReportArgumentType.String, moneysheet_code);
                arg.Add("date1", iReportArgumentType.Date, date_s);
                arg.Add("date2", iReportArgumentType.Date, date_e);
                iReportBuider report = new iReportBuider(this, arg);
                report.Retrieve();
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}