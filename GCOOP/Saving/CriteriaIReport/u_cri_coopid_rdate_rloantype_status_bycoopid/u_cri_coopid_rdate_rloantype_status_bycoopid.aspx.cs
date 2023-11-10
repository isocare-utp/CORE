using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_coopid_rdate_rloantype_status_bycoopid
{
    public partial class u_cri_coopid_rdate_rloantype_status_bycoopid : PageWebReport, WebReport
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
                dsMain.DdLoanTypeS();
                dsMain.DdLoanTypeE();
                dsMain.DATA[0].coop_id = state.SsCoopControl;
                dsMain.DATA[0].coop_id2 = state.SsCoopControl;
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
                dsMain.DATA[0].status = "8";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            try
            {
                String coop_id = dsMain.DATA[0].coop_id;
                String coop_id2 = dsMain.DATA[0].coop_id2;
                DateTime start_date = dsMain.DATA[0].start_date;
                DateTime end_date = dsMain.DATA[0].end_date;
                String start_loantype = dsMain.DATA[0].start_loantype;
                String end_loantype = dsMain.DATA[0].end_loantype;
                String status = dsMain.DATA[0].status;

                iReportArgument arg = new iReportArgument();

                arg.Add("as_coopid", iReportArgumentType.String, coop_id);
                arg.Add("as_coopid2", iReportArgumentType.String, coop_id2);
                arg.Add("adtm_startdate", iReportArgumentType.Date, start_date);
                arg.Add("adtm_enddate", iReportArgumentType.Date, end_date);
                arg.Add("as_startlntype", iReportArgumentType.String, start_loantype);
                arg.Add("as_endlntype", iReportArgumentType.String, end_loantype);
                arg.Add("as_status", iReportArgumentType.String, status);

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