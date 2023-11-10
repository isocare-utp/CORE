using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.CriteriaIReport.u_cri_trading.sale_for_cash_trd12
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

            string sqlstepment = @"SELECT TD_R_TD_SALE_FOR_CASH.SLIP_NO,
         TD_R_TD_SALE_FOR_CASH.SLIP_DATE,
         TD_R_TD_SALE_FOR_CASH.DUEDATE,
         round(TD_R_TD_SALE_FOR_CASH.AMTBEFORTAX,2) as AMTBEFORTAX,
         TD_R_TD_SALE_FOR_CASH.DISC_PERCENT,
         round(TD_R_TD_SALE_FOR_CASH.DISC_AMT,2) as DISC_AMT,
         round(TD_R_TD_SALE_FOR_CASH.TAX_AMT,2) as TAX_AMT,
         round(TD_R_TD_SALE_FOR_CASH.TRANSPORTFEE,2) as TRANSPORTFEE,
         round(TD_R_TD_SALE_FOR_CASH.SLIPNET_AMT,2) as SLIPNET_AMT,
         TD_R_TD_SALE_FOR_CASH.DEBT_NO,
         TD_R_TD_SALE_FOR_CASH.DEBT_NAME,
            'p_nos' as date_inthisreport,           
            'p_noe' as mem_no_as_report
    FROM TD_R_TD_SALE_FOR_CASH
            

   WHERE ";


            try
            {
                TextBox daet_start = dsMain.FindTextBox(0, "date_start");
                TextBox daet_end = dsMain.FindTextBox(0, "date_end");

                string ds = daet_start.Text.Substring(6, 4);
                string de = daet_end.Text.Substring(6, 4);

                decimal st = Convert.ToDecimal(ds);
                decimal et = Convert.ToDecimal(de);

                st -= 543;
                et -= 543;
                string start = daet_start.Text.Substring(0, 6) + st;
                string end = daet_end.Text.Substring(0, 6) + et;

                sqlstepment += " (TD_R_TD_SALE_FOR_CASH.SLIP_DATE) between to_date('" + start + "','dd/mm/yyyy') and  to_date('" + end + "','dd/mm/yyyy')";
                string descrip = "ตั้งแต่วันที่ " + daet_start.Text + " ถึง  " + daet_end.Text;
                sqlstepment = sqlstepment.Replace("p_nos", descrip);

                //arg.Add("as_slip_date_start", iReportArgumentType.Date, dsMain.DATA[0].date_start);   // add ธรรมดา
                //arg.Add("as_slip_date_end", iReportArgumentType.Date, dsMain.DATA[0].date_end);

                TextBox debtno_s = dsMain.FindTextBox(0, "DEBT_NO1");
                TextBox debtno_e = dsMain.FindTextBox(0, "DEBT_NO2");

                
                if (debtno_s.Text != "" && debtno_e.Text != "")
                {
                    string member_no = "ตั้งแต่เลขที่สมาชิก " + debtno_s.Text + " ถึง  " + debtno_e.Text;
                    sqlstepment = sqlstepment.Replace("p_noe", member_no);

                }
                else
                {
                    string member_no = "ทั้งหมดจากวันที่ค้นหา ";
                    sqlstepment = sqlstepment.Replace("p_noe", member_no);
                }

                if (debtno_s.Text != "" && debtno_e.Text != "")
                {
                    sqlstepment += " AND TD_R_TD_SALE_FOR_CASH.DEBT_NO  between '" + debtno_s.Text + "' and  '" + debtno_e.Text + "' ";
                 
                }
                else 
                {
               
                }
                sqlstepment += @"ORDER BY 
                                  TD_R_TD_SALE_FOR_CASH.DEBT_NO ASC,
                                 TD_R_TD_SALE_FOR_CASH.SLIP_NO ASC,
                                 TD_R_TD_SALE_FOR_CASH.SLIP_DATE ASC";
                //arg.Add("DEBT_NO1", iReportArgumentType.String, dsMain.DATA[0].DEBT_NO1);
                //arg.Add("DEBT_NO2", iReportArgumentType.String, dsMain.DATA[0].DEBT_NO2);

                iReportArgument arg = new iReportArgument(sqlstepment);
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