using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_depttype
{
    public partial class u_cri_coopid_depttype : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;
        [JsPostBack]
        public string JsPostBank { get; set; }

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
                dsMain.DdDepttype();
                dsMain.DdDepttypee();

            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {

            try
            {

                string as_coopid = dsMain.DATA[0].coop_id;
                String start_depttype = dsMain.DATA[0].depttype_scode.Trim();
                String end_depttype = dsMain.DATA[0].depttype_ecode.Trim();

                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                arg.Add("as_endtype", iReportArgumentType.String, end_depttype);
                arg.Add("as_starttype", iReportArgumentType.String, start_depttype);
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