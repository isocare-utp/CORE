using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_rdate_billpayment
{
    public partial class u_cri_rdate_billpayment : PageWebReport, WebReport
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.RetrieveMain();
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
            String bank_code = dsMain.DATA[0].bank_code;
            String chk_bankall = dsMain.DATA[0].chk_bankall;
            String rid = dsMain.DATA[0].reportchk;

            //Add Arg[]
            iReportArgument args = new iReportArgument();
            iReportBuider report = new iReportBuider(this, args);

            if (rid == "FPB00001")
            {
                args.Add("as_sdate", iReportArgumentType.Date, start_date);
                args.Add("as_edate", iReportArgumentType.Date, end_date);
                if (chk_bankall == "1")
                {
                    args.Add("as_bankcode", iReportArgumentType.String, "0000");
                }
                else
                {
                    args.Add("as_bankcode", iReportArgumentType.String, bank_code);
                }
                report.AddCriteria("r_billpayment_full", "รายงาน Billpayment (ทั้งหมด)", ReportType.pdf, args);  
            }
            else if (rid == "FPB00002")
            {
                args.Add("as_sdate", iReportArgumentType.Date, start_date);
                args.Add("as_edate", iReportArgumentType.Date, end_date);
                if (chk_bankall == "1")
                {
                    args.Add("as_bankcode", iReportArgumentType.String, "0000");
                }
                else
                {
                    args.Add("as_bankcode", iReportArgumentType.String, bank_code);
                }
                report.AddCriteria("r_billpayment_fail", "รายงาน Billpayment (รอตรวจสอบ)", ReportType.pdf, args);
            }
            else if (rid == "FPB00003")
            {
                args.Add("as_sdate", iReportArgumentType.Date, start_date);
                args.Add("as_edate", iReportArgumentType.Date, end_date);
                if (chk_bankall == "1")
                {
                    args.Add("as_bankcode", iReportArgumentType.String, "0000");
                }
                else
                {
                    args.Add("as_bankcode", iReportArgumentType.String, bank_code);
                }
                report.AddCriteria("r_billpayment_finish", "รายงาน Billpayment (Complete)", ReportType.pdf, args);
            }
            //
            report.AutoOpenPDF = true;
            report.Retrieve();
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}