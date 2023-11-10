using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Saving.CriteriaIReport.u_cri_trading.tradinf_order_day
{
    public partial class report : PageWebReport, WebReport
    {

        public void InitJsPostBack()
        {
            //RealString = Request.QueryString["Error"];
            // if (RealString == "Report")
            // {
            //     lbError.Text = "*ยังไม่ได้เลือกข้อมูลสำหรับการค้นหา";

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
                

                if (startdate.Text == "" || enddate.Text == "")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาใส่ข้อมูลให้ครบ");
                }
                else
                {
                    string[] stemp = startdate.Text.Split('/');
                    string sdate = stemp[0] + "/" + stemp[1] + "/" + (Convert.ToInt32(stemp[2]) - 543);

                    string[] etemp = enddate.Text.Split('/');
                    string edate = etemp[0] + "/" + etemp[1] + "/" + (Convert.ToInt32(etemp[2]) - 543);

                    arg.Add("as_slip_date_start", iReportArgumentType.String, sdate);
                    arg.Add("as_slip_date_end", iReportArgumentType.String, edate);
                    

                    iReportBuider report = new iReportBuider(this, arg);
                    report.Retrieve();
                }
            }
            catch
            { }
           
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}