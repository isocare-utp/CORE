using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_process
{
    public partial class u_cri_process : PageWebReport, WebReport
    {

        public void InitJsPostBack()
        {
            
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

                string test = str_month_.Text;
                string test2 = str_date.Text;
                string year_date = test2 + test;
                iReportArgument arg = new iReportArgument();


                arg.Add("as_recvperiod", iReportArgumentType.String, year_date);
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