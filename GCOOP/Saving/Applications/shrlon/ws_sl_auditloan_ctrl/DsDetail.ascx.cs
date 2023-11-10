using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_auditloan_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.LNCONTMASTERDataTable DATA { get; set; }

        public void InitDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTMASTER;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");

            this.Register();
        }

        public void RetrieveDetail(string loancontract_no)
        {
            string sql = @"select lncontmaster.coop_id,
                lncontmaster.loancontract_no,   
	            lncontmaster.loantype_code,   
	            lncontmaster.loanobjective_code,   
	            lncontmaster.startcont_date,   
	            lncontmaster.last_periodrcv,   
	            lncontmaster.last_periodpay,   
	            lncontmaster.lastreceive_date,   
	            lncontmaster.lastpayment_date,   
	            lncontmaster.lastcalint_date,   
	            lncontmaster.lastkeeping_date,   
	            lncontmaster.lastprocess_date,   
	            lncontmaster.principal_arrear,   
	            lncontmaster.interest_arrear,   
	            lncontmaster.intmonth_arrear,   
	            lncontmaster.intyear_arrear,   
	            lncontmaster.interest_accum,   
	            lncontmaster.intaccum_lastyear,   
	            lncontmaster.intpayment_amt,   
	            lncontmaster.last_stm_no,   
	            lncontmaster.last_transcont_no,   
	            lncontmaster.contract_time,   
	            lncontmaster.expirecont_date ,
                lncontmaster.loanapprove_amt,
                lncontmaster.principal_balance,
                lncontmaster.withdrawable_amt,
                lncontmaster.contract_status,
                lncontmaster.expense_accid,
                lncontmaster.principal_return,
                lncontmaster.interest_return,
                lncontmaster.period_payamt,
                lncontmaster.remark
            from lncontmaster  
            where ( lncontmaster.coop_id = {0} )
	            and ( lncontmaster.loancontract_no = {1} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontract_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void Ddloantype()
        {
            string sql = @"select loantype_code, 
                loantype_code+' - '+loantype_desc as display, 1 as sorter
                from lnloantype
                where coop_id = {0}
                union
                select '','',0 from dual order by sorter, loantype_code";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_code", "display", "loantype_code");
        }

        public void DdLoanobjective()
        {
            string sql = @"select loanobjective_code,
                loanobjective_code+' '+loanobjective_desc as display,
                1 as sorter
                from lnucfloanobjective
                where coop_id = {0}
                union
                select '','',0 from dual order by sorter, loanobjective_code
                ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loanobjective_code", "display", "loanobjective_code");
        }
    }
}