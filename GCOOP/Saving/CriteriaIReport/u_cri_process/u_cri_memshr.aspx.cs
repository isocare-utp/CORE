using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;


namespace Saving.CriteriaIReport.u_cri_process
{
    public partial class u_cri_memshr : PageWebReport, WebReport
    {

        public void InitJsPostBack()
        {
            
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DateTime to_date = state.SsWorkDate;
                string date = Convert.ToString(to_date);
                string[] date1 = date.Split(' ');
                string[] tmpdate_start = date1[0].Split('/');
                string date_show = tmpdate_start[1] + "/" + tmpdate_start[0] + "/" + (Convert.ToDecimal(tmpdate_start[2]) + 543);
                as_sdate.Text = date_show;
                as_edate.Text = date_show;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {
            try
            {
                string coop = "001001";
                
                string[] tmpdate_start = as_sdate.Text.Split('/');
                string as_date_chk1 = tmpdate_start[1] + "/" + tmpdate_start[0] + "/" + (Convert.ToDecimal(tmpdate_start[2]) - 543);
                
                string test2 = as_edate.Text;
                string[] tmpdate_start2 = as_edate.Text.Split('/');
                string as_date_chk2 = tmpdate_start2[1] + "/" + tmpdate_start2[0] + "/" + (Convert.ToDecimal(tmpdate_start2[2]) - 543);

                DateTime as_sdate1 = Convert.ToDateTime(as_date_chk1);
                DateTime as_edate1 = Convert.ToDateTime(as_date_chk2);
                iReportArgument arg = new iReportArgument();

                arg.Add("as_coop", iReportArgumentType.String, coop);
                arg.Add("as_sdate", iReportArgumentType.Date, as_sdate1);
                arg.Add("as_edate", iReportArgumentType.Date, as_edate1);
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