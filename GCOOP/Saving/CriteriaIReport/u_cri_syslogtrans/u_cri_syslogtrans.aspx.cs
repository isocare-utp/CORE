using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_syslogtrans
{
    public partial class u_cri_syslogtrans : PageWebReport, WebReport
    {
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdTable();
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
                args.Add("table", iReportArgumentType.String, dsMain.DATA[0].tabname);
                args.Add("start_date", iReportArgumentType.Date, dsMain.DATA[0].startdate);
                args.Add("end_date", iReportArgumentType.Date, dsMain.DATA[0].enddate);
                args.Add("stmp_user", iReportArgumentType.String, dsMain.DATA[0].username);
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