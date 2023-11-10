using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_acc_asset_liquidity
{
    public partial class u_cri_acc_asset_liquidity : PageWebReport, WebReport
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
                dsMain.DATA[0].year = state.SsWorkDate.Year + 543;
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {

            try
            {
                Decimal period = 0, accyear = 0;
                Decimal year = dsMain.DATA[0].year;
                year = year - 543;
                String month = dsMain.DATA[0].month;
                String sql = @"select account_year , period from accperiod
                                where to_char( period_end_date,'MM' )  = {1}
                                and to_char( period_end_date,'yyyy' ) = {0}";
                sql = WebUtil.SQLFormat(sql, year, month);
                Sdt dt = WebUtil.QuerySdt(sql);               
                if (dt.Next())
                {
                    period = dt.GetInt32("period");
                    accyear = dt.GetInt32("account_year");
                }
                int result = wcf.NAccount.of_gen_data_asset_liquidity(state.SsWsPass, (short)accyear, (short)period, state.SsCoopControl);

                iReportArgument arg = new iReportArgument();                
                arg.Add("ai_period", iReportArgumentType.Integer, period);
                arg.Add("ai_year", iReportArgumentType.Integer, accyear);
                arg.Add("start_branch", iReportArgumentType.String, state.SsCoopControl);                

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