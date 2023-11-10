using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using CoreSavingLibrary;
using DataLibrary;
namespace Saving.CriteriaIReport.u_cri_account.u_cri_acc_tribalance_mju
{
    public partial class u_cri_acc_tribalance_mju : PageWebReport, WebReport
    {
        //protected String app;
        //protected String gid;
        //protected String rid;


        public void InitJsPostBack()
        {
        }

        public void WebSheetLoadBegin()
        {
//            //--- Page Arguments
//            try
//            {
//                app = Request["app"].ToString();
//            }
//            catch { }
//            if (app == null || app == "")
//            {
//                app = state.SsApplication;
//            }
//            try
//            {
//                gid = Request["gid"].ToString();
//            }
//            catch { }
//            try
//            {
//                rid = Request["rid"].ToString();
//            }
//            catch { }

//            //Report Name.
//            try
//            {
//                Sta ta = new Sta(state.SsConnectionString);
//                String sql = "";
//                sql = @"SELECT REPORT_NAME  
//                    FROM WEBREPORTDETAIL  
//                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
//                Sdt dt = ta.Query(sql);
//                ReportName = dt.Rows[0]["REPORT_NAME"].ToString();
//                ta.Close();
//            }
//            catch
//            {
//                ReportName = "[" + rid + "]";
//            }
//            Label1.Text = ReportName;

//            if (!IsPostBack)
//            {
//                startdate.Text = state.SsWorkDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
//                enddate.Text = state.SsWorkDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
//            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()

        {
            string check_flag_new = "";
            try
            {
                iReportArgument arg = new iReportArgument();
                 
                switch (dd_form.SelectedIndex)
                {
                    case 0: check_flag_new = "01";
                        break;
                    //case 1: check_flag_new = "02";

                    //    break;
                    //case 2: check_flag_new = "03";

                    //    break;
                    default: break;
                }




                string[] adtm_date1 = startdate.Text.Split('/');
                string[] adtm_date2 = enddate.Text.Split('/');
                string as_adtm_date = adtm_date1[0] + "/" + adtm_date1[1] + "/" + (Convert.ToDecimal(adtm_date1[2]) - 543);
                string as_adtm_date2 = adtm_date2[0] + "/" + adtm_date2[1] + "/" + (Convert.ToDecimal(adtm_date2[2]) - 543);
                DateTime adtm_date_new = new DateTime();
                DateTime adtm_date_new2 = new DateTime();
                //int result = wcf.NAccount.of_gen_trial_bs2(state.SsWsPass, adtm_date_new, adtm_date_new2, check_flag_new, state.SsCoopControl);
                try
                {
                    adtm_date_new = DateTime.ParseExact(as_adtm_date, "dd/MM/yyyy", null);
                    adtm_date_new2 = DateTime.ParseExact(as_adtm_date2, "dd/MM/yyyy", null);
                }
                catch { }
                int result = wcf.NAccount.of_gen_trial_bs2(state.SsWsPass, adtm_date_new, adtm_date_new2, check_flag_new, state.SsCoopControl);

                String szDescTitle = "";
                switch (dd_form.SelectedIndex)
                {
                    case 0: szDescTitle = "r_acc_mth_tribalance_excel_mju";
                        break;
                    //case 1: szDescTitle = "r_acc_mth_tribalance10_excel";

                    //    break;
                    //case 3: szDescTitle = "acm011_r_mth20_14";

                        //break;
                    default: break;
                }

               



                arg.Add("adtm_date", iReportArgumentType.Date, adtm_date_new);
                arg.Add("adtm_edate", iReportArgumentType.Date, adtm_date_new2);
                //arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                
                iReportBuider report = new iReportBuider(this, arg, szDescTitle);

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