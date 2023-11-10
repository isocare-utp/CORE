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
    public partial class u_cri_mis_acc : PageWebReport, WebReport
    {
        protected String postProcessReport;
        protected String changeValue;
        protected String URL;
        private String pbl = "criteria.pbl";
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
            string report_name;
            string moneysheet_code = money_code.Text;
            //string total_show = total_show_r.Text;
            string month1 = str_month1.Text;
            string year1 = str_year1.Text;
            string month2 = str_month2.Text;
            string year2 = str_year2.Text;
            DateTime date_s = Convert.ToDateTime("01/01/2015");
            DateTime date_e = Convert.ToDateTime("01/01/2015");

            //if (total_show == "1")
            //{
            //    report_name = "report_mis_acc_one_date";
            //    string as_date_chk = "15/" + month1 + "/" + (Convert.ToDecimal(year1) - 543);
            //    date_s = Convert.ToDateTime(as_date_chk);
            //    date_e = Convert.ToDateTime(as_date_chk);
               
            //}
            //else
            //{
                report_name = "report_mis_acc";
                string as_date_chk = "15/" + month1 + "/" + (Convert.ToDecimal(year1) - 543);
                date_s = Convert.ToDateTime(as_date_chk);
                string as_date_chk2 = "15/" + month2 + "/" + (Convert.ToDecimal(year2) - 543);
                date_e = Convert.ToDateTime(as_date_chk2);
            //}

            iReportArgument arg = new iReportArgument();
            arg.Add("moneysheet_code", iReportArgumentType.String, moneysheet_code);
            arg.Add("date1", iReportArgumentType.Date, date_s);
            arg.Add("date2", iReportArgumentType.Date, date_e);
            iReportBuider report = new iReportBuider(this, arg);
           
            //if (total_show == "1")
            //{
            //    report.AddCriteria(report_name, "รายงานสำหรับแสดง 1 ช่อง", ReportType.pdf, arg);
            //    report.AutoOpenPDF = false;
            //}
            report.Retrieve();
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}