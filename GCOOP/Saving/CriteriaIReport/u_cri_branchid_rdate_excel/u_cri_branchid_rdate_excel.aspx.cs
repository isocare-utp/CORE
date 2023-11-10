using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_branchid_rdate_excel
{
    public partial class u_cri_branchid_rdate_excel : PageWebReport, WebReport
    {
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdCoopId();
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
          
                String coop_id = dsMain.DATA[0].coop_id;
                DateTime start_date = dsMain.DATA[0].start_date;
                DateTime end_date = dsMain.DATA[0].end_date;
            

                iReportArgument arg = new iReportArgument();

                arg.Add("as_coopid", iReportArgumentType.String, coop_id);
                arg.Add("adtm_date", iReportArgumentType.Date, start_date);
                arg.Add("adtm_edate", iReportArgumentType.Date, end_date);
                iReportBuider report = new iReportBuider(this, arg);
              
                report.AutoOpenPDF = true;

                report.Retrieve();
            

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}