using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_loan_request
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
            str_date1.Text = date_show;
            str_date2.Text = date_show;
            DropDown();
            Sta ta2 = new Sta(state.SsConnectionString);
            String sql2 = "select max( loantype_code) as loantype_code  from lnloantype";
            Sdt dt3 = ta2.Query(sql2);
            loangrp2.Text = dt3.Rows[0]["loantype_code"].ToString();
            ta2.Close();
            //loangrp2.Text = "30";
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
                string loan_grp1 = loangrp1.Text;
                string loan_grp2 = loangrp2.Text;
                string[] tmpdate_start = str_date1.Text.Split('/');
                string as_date_chk1 = tmpdate_start[1] + "/" + tmpdate_start[0] + "/" + (Convert.ToDecimal(tmpdate_start[2]) - 543);
                string[] tmpdate_start2 = str_date2.Text.Split('/');
                string as_date_chk2 = tmpdate_start2[1] + "/" + tmpdate_start2[0] + "/" + (Convert.ToDecimal(tmpdate_start2[2]) - 543);

                DateTime as_sdate1 = Convert.ToDateTime(as_date_chk1);
                DateTime as_sdate2 = Convert.ToDateTime(as_date_chk2);
                iReportArgument arg = new iReportArgument();

                arg.Add("as_coop_id_s", iReportArgumentType.String, coop);
                arg.Add("as_coop_id_e", iReportArgumentType.String, coop);
                arg.Add("as_startdate", iReportArgumentType.Date, as_sdate1);
                arg.Add("as_enddate", iReportArgumentType.Date, as_sdate2);
                arg.Add("as_loantype_s", iReportArgumentType.String, loan_grp1);
                arg.Add("as_loantype_e", iReportArgumentType.String, loan_grp2);
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
        private void DropDown()
        {
            try
            {


                String sql_loan_type = @"select loantype_code , loantype_code||' - ' || loantype_desc as loantype_desc from lnloantype order by loantype_code";
                Sta ta2 = new Sta(state.SsConnectionString);
                Sdt dt2 = ta2.Query(sql_loan_type);
                if (dt2.Next())
                {
                    loangrp1.DataSource = dt2;
                    loangrp1.DataTextField = "loantype_desc";
                    loangrp1.DataValueField = "loantype_code";
                    loangrp1.DataBind();

                    loangrp2.DataSource = dt2;
                    loangrp2.DataTextField = "loantype_desc";
                    loangrp2.DataValueField = "loantype_code";
                    loangrp2.DataBind();

                }
                ta2.Close();

            }
            catch { }
        }
    }
}