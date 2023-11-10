using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.ap_deposit.w_sheet_close_month_ctrl
{
    public partial class w_sheet_close_month : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostProcess { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            //ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 3600;
           // AsyncPostBackTimeout = 3600;
            Server.ScriptTimeout = 3600;
            if (!IsPostBack)
            {
                dsMain.SetItem(0, dsMain.DATA.yearColumn, state.SsWorkDate.Year + 543);
                dsMain.SetItem(0, dsMain.DATA.monthColumn, state.SsWorkDate.Month);
                dsMain.SetItem(0, dsMain.DATA.start_dateColumn, state.SsWorkDate);
                dsMain.SetItem(0, dsMain.DATA.end_dateColumn, state.SsWorkDate);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

            if (eventArg == PostProcess)
            {
                try
                {
                    int re = Runprocess();
                    LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลสำเร็จ (" + re + ")");
                }

                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);

                }


            }
        }



        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
        private int Runprocess()
        {
            int re = -1;
            Sta ta = new Sta(state.SsConnectionString);
            ta.Transection();
            try
            {
                String sql1 = "Delete from DPCLOSEACCOMONTH where ACCOUNT_YEAR = {0} and CLOSEMONTH_NO = {1}";
                sql1  = WebUtil.SQLFormat(sql1, dsMain.DATA[0].start_date.Year + 543, dsMain.DATA[0].start_date.Month);
                ta.Exe(sql1);




                String sql = @"INSERT INTO DPCLOSEACCOMONTH( 
           COOP_ID,   
           ACCOUNT_YEAR,   
           CLOSEMONTH_NO,   
           DEPTACCOUNT_NO,   
           DEPTMONTH_STATUS,   
           DEPTMONTH_AMT,   
           MONTHINT_STATUS,   
           MONTHINTPAY_METH,   
           SPCINT_RATE_STATUS,   
           SPCINT_RATE,   
           INTARREAR_AMT,   
           SUMDEPT_AMT,   
           SUMWITD_AMT,   
           SUMINTPAY_AMT,   
           SUMTAXPAY_AMT,   
           MONTHINTPAY_AMT,   
           MONTHTAXPAY_AMT,   
           PRNCBAL,   
           CHECKPEND_AMT,   
           LOANGARANTEE_AMT,   
           SEQUEST_STATUS,   
           WITHDRAWABLE_AMT,   
           LASTCALINT_DATE,   
           ACCUINT_AMT,   
           ACCUINTPAY_AMT,   
           ACCUTAXPAY_AMT,   
           MBEGINBAL,   
           CLOSEUSER_ID,   
           CLOSE_DATE,   
           SEQUEST_AMT,   
           BOOK_BALANCE )  
(select  distinct
	coop_id as cop,
	{0},
	{1},
 	deptaccount_no as acc ,
	sum( deptmonth_status ) as deptmonth_status,
	sum( deptmonth_amt ) as deptmonth_amt,
	0,
	0,
	0,   
     0,   
     0,   
    sum( dept_amt ) as dept_amt,  
    sum( with_amt ) as with_amt , 
     0,   
     0,   
     0,   
     0,   
     sum( prncbal ) + ( sum( dept_amt )   -   sum( with_amt ) ) as af_prncbal,  
     0,   
    null,   
    0,   
     sum( with_amt ) as with_amt , 
     null,  
     0,   
     0,   
     0,   
     0,   
     '',   
    {5},   
     0,   
     0
 from
	( select a.prncbal as prncbal , c.depttype_desc as depttype_desc , 
		c.depttype_code as depttype_code,  
		0 as dept_amt  , 
		0 as with_amt, 
		d.deptaccount_no as deptaccount_no,
		d.deptmonth_status as deptmonth_status, 
		d.deptmonth_amt as deptmonth_amt,
		d.coop_id as coop_id
	from dpdeptstatement a, dpdepttype c, dpdeptmaster d
	where a.seq_no = ( 	select max(seq_No ) 
								from dpdeptstatement b
								where b.entry_date < {2}
								and a.deptaccount_no = b.deptaccount_no
								)
	and c.depttype_code = d.depttype_code
	and a.deptaccount_no = d.deptaccount_no


union all

select 0 as prncbal ,c.depttype_desc as depttype_desc , c.depttype_code as depttype_code,   
		case when e.sign_flag > 0 then deptslip_amt else 0.00 end as dept_amt, 
		case when e.sign_flag < 0 then deptslip_amt else  0.00 end as with_amt ,
		d.deptaccount_no as deptaccount_no,
		0 as deptmonth_status ,
		0 as deptmonth_amt,
		d.coop_id as coop_id
	from dpdeptslip s, dpdepttype c, dpdeptmaster d ,  dpucfdeptitemtype  e
	where s.entry_date between  {2} and  {3}
	and s.deptitemtype_code = e.deptitemtype_code
	and c.depttype_code = d.depttype_code
	and s.deptaccount_no = d.deptaccount_no
	and s.item_status = 1
	

	)
group by deptaccount_no,coop_id)
";

                //sql1 = WebUtil.SQLFormat(sql1, dsMain.DATA[0].start_date.Year + 543, dsMain.DATA[0].start_date.Month, dsMain.DATA[0].start_date, dsMain.DATA[0].end_date);
                sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].start_date.Year + 543, dsMain.DATA[0].start_date.Month, dsMain.DATA[0].start_date, dsMain.DATA[0].end_date, state.SsCoopControl,state.SsWorkDate);
                
                re = ta.Exe(sql);
                ta.Commit();
                ta.Close();
            }
            catch (Exception ex) {
                ta.RollBack();
                ta.Close();
                throw ex;
            }
            return re;
        }
    }
}