using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.admin
{
    public partial class w_sheet_am_report : PageWebSheet, WebSheet
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

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                iReportArgument args = new iReportArgument();
                args.Add("as_application", iReportArgumentType.String, "shrlon");
                args.Add("as_coop_id", iReportArgumentType.String, "001001");

                iReportBuider report = new iReportBuider(this, "ทดสอบ");
                report.AddCriteria("report1", "รายงาน PDF", ReportType.pdf, args);
                report.AddCriteria("report1", "รายงาน EXCEL 97 - 2003", ReportType.xls_data, args);
                report.AddCriteria("report1", "รายงาน EXCEL 2007", ReportType.xlsx_data, args);
                report.AddCriteria("report1", "รายงาน CSV", ReportType.csv, args);

                report.Retrieve();
                LtServerMessage.Text = WebUtil.WarningMessage("ทดสอบการออกรายงานจากหน้า w_sheet");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}