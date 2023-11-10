using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.CriteriaIReport.u_cri_sixline
{
    public partial class u_cri_sixline : PageWebReport, WebReport
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
                dsMain.DdMembgroup();
                dsMain.DATA[0].operate_date = state.SsWorkDate;
            }            
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            string line1 = dsMain.DATA[0].line1;
            string line2 = dsMain.DATA[0].line2;
            string line3 = dsMain.DATA[0].line3;
            string line4 = dsMain.DATA[0].line4;
            string line5 = dsMain.DATA[0].line5;
            string line6 = dsMain.DATA[0].line6;
            
            try
            {
                iReportArgument arg = new iReportArgument();

                arg.Add("line1", iReportArgumentType.String, line1);
                arg.Add("line2", iReportArgumentType.String, line2);
                arg.Add("line3", iReportArgumentType.String, line3);
                arg.Add("line4", iReportArgumentType.String, line4);
                arg.Add("line5", iReportArgumentType.String, line5);
                arg.Add("line6", iReportArgumentType.String, line6);   

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