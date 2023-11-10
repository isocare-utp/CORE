using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_loan_balance
{
    public partial class report : PageWebReport, WebReport
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
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }
            if (!IsPostBack)
            {
                DateTime to_date = state.SsWorkDate;
                string date = Convert.ToString(to_date);
                string[] date1 = date.Split(' ');
                string[] tmpdate_start = date1[0].Split('/');
                string date_show = tmpdate_start[1] + "/" + tmpdate_start[0] + "/" + (Convert.ToDecimal(tmpdate_start[2]) + 543);
                str_date.Text = date_show;
             }
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {
            try
            {
                string coop = "001001";
                string loan_grp = loangrp.Text;
                string[] tmpdate_start = str_date.Text.Split('/');
                string as_date_chk1 = tmpdate_start[1] + "/" + tmpdate_start[0] + "/" + (Convert.ToDecimal(tmpdate_start[2]) - 543);

                DateTime as_sdate1 = Convert.ToDateTime(as_date_chk1);
                iReportArgument arg = new iReportArgument();

                arg.Add("as_coopid", iReportArgumentType.String, coop);
                arg.Add("as_date", iReportArgumentType.Date, as_sdate1);
                arg.Add("as_loangrp", iReportArgumentType.String, loan_grp);
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