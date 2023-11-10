using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl
{
    public partial class DsLoan : DataSourceRepeater
    {
        public DataSet1.LNCONTMASTERDataTable DATA { get; set; }
        public void InitDsLoan(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTMASTER;
            this.EventItemChanged = "OnDsLoanItemChanged";
            this.EventClicked = "OnDsLoanClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsLoan");
            this.Register();
        }

        public void RetriveLoan(string member_no)
        {
            string sql = @"select lncontmaster.loancontract_no,   
                 lnloantype.prefix,   
                 lncontmaster.loanapprove_amt,   
                 lncontmaster.period_payment,   
                 lncontmaster.startcont_date,   
                 lncontmaster.principal_balance,   
                 lncontmaster.last_periodpay,   
                 lncontmaster.interest_accum,   
                 lncontmaster.payment_status,   
                 lncontmaster.contract_status,   
                 lncontmaster.loantype_code,   
                 lncontmaster.withdrawable_amt,   
                 lncontmaster.lastreceive_date,   
                 lncontmaster.lastpayment_date,   
                 lncontmaster.period_payamt,   
                 lncontmaster.period_payment_max,   
                 lncontmaster.interest_arrear,   
                 lncontmaster.loanpaymentchg_date,   
                 lncontmaster.interest_return,   
                 lncontmaster.status_desc,   
                 lncontmaster.contlaw_status,   
                 lncontmaster.transfer_status  
            from lncontmaster,   
                 lnloantype  
            where ( lnloantype.loantype_code = lncontmaster.loantype_code )  
                 and ( lncontmaster.memcoop_id = lnloantype.coop_id )  
                 and ( ( lncontmaster.memcoop_id = {0} ) 
                 and ( lncontmaster.member_no = {1} )
                 and ( lncontmaster.contract_status = 1 ) )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}