using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.CriteriaIReport.iframe_ireport
{
    public partial class processing : PageWebDialog, WebDialog
    {
        protected string reportStatus;

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            try
            {
                DataTable dt = WebUtil.Query("select * from cmreportprocessing where PROCESS_ID = '" + Request["pid"] + "'");
                reportStatus = dt.Rows[0]["runtime_status"].ToString() + ReportUtil.Spliter1 + dt.Rows[0]["label_name"].ToString();

            }
            catch (Exception ex)
            {
                reportStatus = "none" + ReportUtil.Spliter1 + ex.Message;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}