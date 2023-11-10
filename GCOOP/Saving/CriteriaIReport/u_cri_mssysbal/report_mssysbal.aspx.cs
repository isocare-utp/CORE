using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_mssysbal
{
    public partial class report_mssysbal : PageWebReport, WebReport
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
                //dsMain.RetrieveMain();
                dsMain.DATA[0].work_date = state.SsWorkDate;
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void RunReport()
        {
            DateTime work_date = dsMain.DATA[0].work_date;
            string test = work_date.ToString("yyyy/MM/dd", WebUtil.EN);
            iReportArgument args = new iReportArgument();
            args.Add("date", iReportArgumentType.String, work_date.ToString("yyyy/MM/dd",WebUtil.EN));
            iReportBuider report = new iReportBuider(this, args);
            report.Retrieve();
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}