using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Saving.CriteriaIReport.u_cri_date_bank_branch
{
    public partial class u_cri_date_bank_branch : PageWebReport, WebReport
    {
        [JsPostBack]
        public string JsPostBank { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.RetrieveMain();
                dsMain.DATA[0].recieve_date = state.SsWorkDate;
             
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostBank)
            {
                string bank = dsMain.DATA[0].bank_code;
                dsMain.DdBranch(bank);
            }
        }

        public void RunReport()
        {
         
            DateTime recieve_date = dsMain.DATA[0].recieve_date;
            //CultureInfo us = new CultureInfo("en-US");
            //recieve_date.ToString("MM/dd/yyyy",us);
         
            //DateTime rdate = Convert.ToDateTime(recieve_date);

            String bank_code = dsMain.DATA[0].bank_code;
            String bank_branch = dsMain.DATA[0].bank_branch;
           // 
            
          //  DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new ("en-US"));

            //Add Arg[]
            iReportArgument args = new iReportArgument();



            args.Add("as_date", iReportArgumentType.Date, recieve_date);
            args.Add("as_bank_code", iReportArgumentType.String, bank_code);
            args.Add("as_bank_branch", iReportArgumentType.String, bank_branch);
            iReportBuider report = new iReportBuider(this, args);
            report.AddCriteria("r_honda_transfer_a4", "รายงานการโอน", ReportType.pdf, args);
            report.AddCriteria("r_honda_transfer_xml", "รายงานการโอน", ReportType.xlsx_data, args); 
            report.AutoOpenPDF = true;
            report.Retrieve();
        }

        public void WebSheetLoadEnd()
        {

        }       
    }
}