using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_rdate_rloantype_moneytype
{
    public partial class u_cri_coopid_rdate_rloantype_moneytype : PageWebReport, WebReport
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
                dsMain.DdMoneyType();
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
                String start_loantype = dsMain.DATA[0].start_loantype;
                String end_loantype = dsMain.DATA[0].end_loantype;
                String moneytype_code = dsMain.DATA[0].moneytype_code;
                if (moneytype_code == null || moneytype_code == "") {
                    moneytype_code = "%";
                }
                iReportArgument arg = new iReportArgument();

                arg.Add("coop_id", iReportArgumentType.String, coop_id);
                arg.Add("start_date", iReportArgumentType.Date, start_date);
                arg.Add("end_date", iReportArgumentType.Date, end_date);
                arg.Add("sloan_type", iReportArgumentType.Integer, start_loantype);
                arg.Add("eloan_type", iReportArgumentType.Integer, end_loantype);
                arg.Add("moneytype_code", iReportArgumentType.String, moneytype_code);

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