using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_interestpay_estimate_detail_ctrl.w_dlg_sl_detail_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNCONTMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            //this.Button.Add("b_search");
            //this.Button.Add("b_cancel");
            this.Register();
        }
        public void RetrieveMain(String loancontract_no)
        {
            String sql = @"SELECT 
                 LNCONTMASTER.COOP_ID,
                 LNCONTMASTER.LOANCONTRACT_NO,
                 LNCONTMASTER.LOANAPPROVE_AMT,   
                 LNCONTMASTER.PRINCIPAL_BALANCE,   
                 LNCONTMASTER.PERIOD_PAYMENT,   
                 LNCONTMASTER.PERIOD_INSTALLMENT,  
                 LNCONTMASTER.LOANPAYMENT_TYPE, 
                 case when LNCONTMASTER.LOANPAYMENT_TYPE = 1 then 'คงต้น' 
		         when LNCONTMASTER.LOANPAYMENT_TYPE = 2 then 'คงยอด'
                 end as PAYMENT_TYPE,  
			     LNCONTMASTER.INT_CONTINTTYPE, 
			     LNCONTMASTER.INT_CONTINTRATE ,  
			     LNCONTMASTER.INT_CONTINTTABCODE,
			     LNCONTMASTER.INT_CONTINTINCREASE, 
                 LNCONTMASTER.INTEREST_ACCUM,
			     case 
			     when LNCONTMASTER.INT_CONTINTTYPE = 0 then 'ไม่คิดดอกเบี้ย'
			     when LNCONTMASTER.INT_CONTINTTYPE = 1 then 'คงที่อัตรา '
			     when LNCONTMASTER.INT_CONTINTTYPE = 2 then 'ตามตาราง ด/บ '
			     when LNCONTMASTER.INT_CONTINTTYPE = 4 then 'ตามตาราง ด/บ เงินฝากประเภท '
			     when LNCONTMASTER.INT_CONTINTTYPE = 5 then 'ตามตาราง ด/บ เงินฝากประเภท' 
			     when LNCONTMASTER.INT_CONTINTTYPE = 3 then 'อัตราพิเศษ เป็นช่วง '
		         end as INT_CONTTYPE,  

                 LNCONTMASTER.LASTPAYMENT_DATE,   
                 LNCONTMASTER.LASTCALINT_DATE,   
                 LNCONTMASTER.INTEREST_ARREAR,   
                 LNCONTMASTER.STARTCONT_DATE  
                FROM LNCONTMASTER   
                WHERE ( LNCONTMASTER.COOP_ID ={0} ) AND  
                     ( LNCONTMASTER.LOANCONTRACT_NO ={1} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontract_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}