using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNAccount;

namespace Saving.CriteriaIReport.u_cri_coopid_cashflow_cash
{
    public partial class u_cri_coopid_cashflow_cash : PageWebReport, WebReport
    {
        private n_commonClient commonService;
        private n_accountClient accService;
        protected String app;
        protected String gid;
        protected String rid;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            
            commonService = wcf.NCommon;
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
              //  dsMain.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.getaccountid();
                dsMain.getaccountyear();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {

            String as_coopid = state.SsCoopControl;
            String an_month = dsMain.DATA[0].month.ToString("00");
            int an_year = Convert.ToInt32(dsMain.DATA[0].year) - 543;
            DateTime adtm_start = new DateTime(Convert.ToInt16(an_year.ToString()), Convert.ToInt16(an_month.ToString()), 1);
            DateTime adtm_end = new DateTime(Convert.ToInt16(an_year.ToString()), Convert.ToInt16(an_month.ToString()), DateTime.DaysInMonth(Convert.ToInt16(an_year.ToString()), Convert.ToInt16(an_month.ToString())));




            String wsPass = state.SsWsPass;
            Decimal Begin = 0;
            Decimal Forward = 0;
            String account_id = dsMain.DATA[0].account_id;
            Int32 CashBeginForward = wcf.NAccount.of_get_bg_fw(wsPass, adtm_start, state.SsCoopId, account_id, ref Begin, ref Forward);
            String ai_begin = Begin.ToString();
            String lbl_moneyfw = Forward.ToString();
           

            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("adtm_start", iReportArgumentType.Date, adtm_start);
                arg.Add("adtm_end", iReportArgumentType.Date, adtm_end);
                arg.Add("ai_begin", iReportArgumentType.String, ai_begin);
                arg.Add("account_id", iReportArgumentType.String, account_id);

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