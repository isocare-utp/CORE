using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_trading.product_move_trd07
{
    public partial class report : PageWebReport, WebReport
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

            }

        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            try
            {
                iReportArgument arg = new iReportArgument();

                arg.Add("as_product_no_start", iReportArgumentType.String, dsMain.DATA[0].as_product_no_start);
                arg.Add("as_product_no_end", iReportArgumentType.String, dsMain.DATA[0].as_product_no_end);
                arg.Add("as_date_start", iReportArgumentType.Date, dsMain.DATA[0].sdate);
                arg.Add("as_date_end", iReportArgumentType.Date, dsMain.DATA[0].edate);

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