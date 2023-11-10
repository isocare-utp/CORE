using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_amsecwins
{
    public partial class amsecwins : PageWebReport, WebReport
    {
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdApplication();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void RunReport()
        {
            try
            {
                iReportArgument args = new iReportArgument();
                args.Add("as_application", iReportArgumentType.String, dsMain.DATA[0].AS_APPLICATION);
                args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopControl);
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