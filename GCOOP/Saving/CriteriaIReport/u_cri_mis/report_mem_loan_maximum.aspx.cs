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
    public partial class report_mem_loan_maximum : PageWebReport, WebReport
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
           String as_num = doc_no.Text;
            try
            {
                iReportArgument arg = new iReportArgument();

                arg.Add("as_rank", iReportArgumentType.String, as_num);

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