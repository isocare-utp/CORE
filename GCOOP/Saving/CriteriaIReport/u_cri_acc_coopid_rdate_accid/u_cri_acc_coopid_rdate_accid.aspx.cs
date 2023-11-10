using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_acc_coopid_rdate_accid
{
    public partial class u_cri_acc_coopid_rdate_accid : PageWebReport, WebReport
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
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
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
                DateTime start_date = dsMain.DATA[0].start_date;
                DateTime end_date = dsMain.DATA[0].end_date;
                String account_id1 = dsMain.DATA[0].account_id1;
                String account_id2 = dsMain.DATA[0].account_id2;

                iReportArgument arg = new iReportArgument();

                String result = wcf.NAccount.of_gen_ledger_report_day(state.SsWsPass, start_date, end_date, account_id1, account_id2, state.SsCoopControl);

                arg.Add("adtm_date", iReportArgumentType.Date, start_date);
                arg.Add("adtm_edate", iReportArgumentType.Date, end_date);
                arg.Add("account_id1", iReportArgumentType.String, account_id1);
                arg.Add("account_id2", iReportArgumentType.String, account_id2);
                arg.Add("as_coopid", iReportArgumentType.String, coop_id);

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