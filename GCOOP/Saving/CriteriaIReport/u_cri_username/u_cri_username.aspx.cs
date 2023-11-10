using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_username
{
    public partial class u_cri_username : PageWebReport, WebReport
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
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void RunReport()
        {
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
                arg.Add("coop_control", iReportArgumentType.String, state.SsCoopControl);

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