using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Saving.CriteriaIReport.u_cri_trading.buy_for_cash_trd11
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

              

                TextBox datestart = dsMain.FindTextBox(0, "date_start");
                try
                {
                    // ss = datestart.Text;

                    string substart = datestart.Text.Substring(6, 4);
                    decimal s = Convert.ToDecimal(substart);
                    s -=543;

                    string start = datestart.Text.Substring(0, 2) +"/"+ datestart.Text.Substring(3, 2) +"/"+ s;

                    datestart.Text = start;
                    //dsMain.SetItem(0,date,);
                }
                catch
                {
                }

                TextBox dateend = dsMain.FindTextBox(0, "date_end");
                string subend = dateend.Text.Substring(6, 4);
                decimal e = Convert.ToDecimal(subend);
                e -= 543;
                string end = dateend.Text.Substring(0, 2) + "/" + dateend.Text.Substring(3, 2) + "/" + e;
                dateend.Text = end;
               

                string sql = @"SELECT    s.slip_no,  s.slip_date ,
			dm.debt_no,  PRENAME_DESC || dm.DEBT_NAME  ||' ' || SUFFNAME_DESC as debt_name,
			debtdec.slip_date rec_date,

            'p_nos' as date_inthisreport,           
            'p_noe' as mem_no_as_report,
			s.due_date,  
             round( s.amtbefortax, 2) as amtbefortax,
             round(nvl(s.disc_percent,0),2) as disc_percent,
             round(nvl(s.disc_amt,0),2) as disc_amt, 
             round(s.tax_amt,2) as tax_amt,
			 round(nvl(s.transportfee,0),2) as transfee,
              round(nvl(s.slipnet_amt, 0) - nvl(debtdec.pay_amt, 0),2) as net_amt,
             round(nvl(debtdec.pay_amt, 0),2) as pay_amt
from   	TDSTOCKSLIP  s, tddebtmaster  dm, MBUCFPRENAME   pn,
(select 	ddm.coop_id, ddd.refdoc_no, slip_date, sum(nvl(ddd.pay_amt, 0)) as pay_amt
from 		tddebtdecdet ddd, tddebtdec ddm
where 	ddm.coop_id = ddd.coop_id and
			ddm.debtdectype_code = ddd.debtdectype_code and
			ddm.debtdecdoc_no = ddd.debtdecdoc_no and
			ddm.slip_date between to_date( '" + datestart.Text+"','dd/mm/yyyy') and to_date( '"+dateend.Text+ @"','dd/mm/yyyy')
group by ddm.coop_id, ddd.refdoc_no, slip_date)  debtdec
where     dm.coop_id  =  s.coop_id    and
			dm.debt_no  =  s.debt_no  and
			dm.prename_code =  pn.prename_code(+)   and
  			s.coop_id = debtdec.coop_id(+) and
			s.slip_no = debtdec.refdoc_no(+) and
			sliptype_code  = 'IV'  and
			paymentby  =  'LON' and
			s.slip_date between    to_date( '" + datestart.Text + "','dd/mm/yyyy')  and  to_date('" + dateend.Text + @"','dd/mm/yyyy')

";
                 TextBox datestart_2 = dsMain.FindTextBox(0, "date_start");
               
                    string substart_2 = datestart.Text.Substring(6, 4);
                    decimal s_2 = Convert.ToDecimal(substart_2);
                    s_2 +=543;

                    string start_2 = datestart_2.Text.Substring(0, 2) + "/" + datestart_2.Text.Substring(3, 2) + "/" + s_2;

                    datestart_2.Text = start_2;


                TextBox dateend_2 = dsMain.FindTextBox(0, "date_end");
                string subend_2 = dateend.Text.Substring(6, 4);
                decimal e_2 = Convert.ToDecimal(subend_2);
                e_2 += 543;
                string end_2 = dateend_2.Text.Substring(0, 2) + "/" + dateend_2.Text.Substring(3, 2) + "/" + e_2;
                dateend_2.Text = end_2;

                string descrip = "ตั้งแต่วันที่ " + datestart_2.Text + " ถึง  " + dateend_2.Text;
                sql = sql.Replace("p_nos", descrip);

                TextBox mem_no_s = dsMain.FindTextBox(0, "memb_no1");
                TextBox mem_no_e = dsMain.FindTextBox(0, "memb_no2");
                if (mem_no_s.Text == "" && mem_no_e.Text == "")
                {
                    string mem_nosinrepot = "ทั้งหมดจากวันที่ค้นหา";
                    sql = sql.Replace("p_noe", mem_nosinrepot);
                }
                if (mem_no_s.Text != "" && mem_no_e.Text != "")
                {
                    string mem_nosinrepot = "ตั้งแต่เลขที่สมาชิก " + mem_no_s.Text + " ถึง  " + mem_no_e.Text;
                    sql = sql.Replace("p_noe", mem_nosinrepot);
                }


                if (mem_no_s.Text == "" && mem_no_e.Text == "")

                { }

                else
                {
                    
                    sql += "and dm.debt_no between '"+mem_no_s.Text+"' and '"+mem_no_e.Text+"'";
                }
                sql += "order by dm.debt_no,s.slip_no,s.slip_date";
           

                iReportArgument arg = new iReportArgument(sql);
                iReportBuider report = new iReportBuider(this,arg);
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