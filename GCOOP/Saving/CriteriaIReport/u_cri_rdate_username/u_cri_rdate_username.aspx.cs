using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_rdate_username
{
    public partial class u_cri_rdate_username : PageWebReport, WebReport
    {
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.retrieve(); 
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void RunReport()
        {
            DateTime start_date = dsMain.DATA[0].start_date;
            DateTime end_date = dsMain.DATA[0].end_date;
            String username = "";
            try
            {
                username = dsMain.DATA[0].USER_NAME;
            }
            catch { }
            

            if (!(username.Length > 0))
            {
                username = "%";
            }


            try
            {
                iReportArgument arg = new iReportArgument();

                arg.Add("user_name", iReportArgumentType.String, username);
                arg.Add("start_date", iReportArgumentType.Date, start_date);
                arg.Add("end_date", iReportArgumentType.Date, end_date);

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