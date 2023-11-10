using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_account
{
    public partial class u_cri_coopid_account : PageWebReport, WebReport
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
                dsMain.Ddaccmaster();
                //dsMain.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {
            //int li_period = 0;
            string as_coopid = state.SsCoopId;
            DateTime date1 = dsMain.DATA[0].start_date;
            DateTime date2 = dsMain.DATA[0].end_date;
            int cash_type = dsMain.DATA[0].cash_type;
            string print_id = dsMain.DATA[0].print;

          /*  this.SetOnLoadedScript(" alert('" + as_coopid + "');");
            this.SetOnLoadedScript(" alert('" + date1 + "');");
            this.SetOnLoadedScript(" alert('" + date2 + "');");
            this.SetOnLoadedScript(" alert('" + cash_type + "');");
            this.SetOnLoadedScript(" alert('" + acc_id + "');");
            this.SetOnLoadedScript(" alert('" + print_id + "');");*/






            //int as_period = Convert.ToInt32(dsMain.DATA[0].year + dsMain.DATA[0].month.ToString("00"));
            //int an_membtype = dsMain.DATA[0].membtype;
            //string ls_sql = @"select max(recv_period) as recv_period from kptempreceive";
            //Sdt dt = WebUtil.QuerySdt(ls_sql);
            //if (dt.Next())
            //{
            //    li_period = Convert.ToInt32(dt.GetString("recv_period"));
            //}

            try
            {
                iReportArgument arg = new iReportArgument();
                //as_coopid ตัวแปรใน iReport

                string cash_account_code = "";

                Sta ta = new Sta(state.SsConnectionString);
                string sql = @"select cash_account_code from accconstant";
                Sdt dt = ta.Query(sql);
                cash_account_code = dt.Rows[0]["cash_account_code"].ToString();
                ta.Close();
                
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                arg.Add("date1", iReportArgumentType.Date, date1);
                arg.Add("date2", iReportArgumentType.Date, date2);
                arg.Add("acc_id", iReportArgumentType.String, cash_account_code);
                arg.Add("cash_type", iReportArgumentType.Integer, cash_type);
                arg.Add("print_id", iReportArgumentType.String, print_id);
                
                
                //arg.Add("as_period", iReportArgumentType.String, Convert.ToString(as_period));
                //arg.Add("an_membtype", iReportArgumentType.String, Convert.ToString(an_membtype));
                iReportBuider report = new iReportBuider(this, arg);
                //if (an_period < li_period)
                //{
                //    report.AddCriteria("r_010_kp_slip_mthkeep_mas", "ใบเสร็จเรียกเก็บประจำเดือน", ReportType.pdf, arg);
                //}
                //else
                //{
                //    report.AddCriteria("r_010_kp_slip_mthkeep", "ใบเสร็จเรียกเก็บประจำเดือน", ReportType.pdf, arg);
                //}
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