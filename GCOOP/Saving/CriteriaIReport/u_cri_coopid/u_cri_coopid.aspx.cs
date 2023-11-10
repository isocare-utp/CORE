using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;
namespace Saving.CriteriaIReport.u_cri_coopid
{
    public partial class u_cri_coopid : PageWebReport,WebReport
    {

        public void InitJsPostBack()
        {
            string gid = "", rid = "";
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
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ReportName.Text = dt.GetString("REPORT_NAME");
                }
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }
        }

        public void WebSheetLoadBegin()
        {
           
        }

        public void CheckJsPostBack(string eventArg)
        {
           
        }

        public void RunReport()
        {
            try
            {
                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                iReportBuider report = new iReportBuider(this, args);
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