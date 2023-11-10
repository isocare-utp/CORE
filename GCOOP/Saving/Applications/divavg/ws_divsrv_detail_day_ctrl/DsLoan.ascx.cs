using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_detail_day_ctrl
{
    public partial class DsLoan : DataSourceRepeater
    {
        public DataSet1.DT_LOANDataTable DATA { get; set; }

        public void InitLoan(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LOAN;
            this.EventItemChanged = "OnDsLoanItemChanged";
            this.EventClicked = "OnDsLoanClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsLoan");
            this.Register();
        }

        public void RetrieveLoan(String ls_member_no, String ls_year)
        {
            string sql = @"select yrbglonmaster.coop_id,   
	            yrbglonmaster.member_no,   
	            yrbglonmaster.div_year,   
	            yrbglonmaster.seq_no,   
	            yrbglonmaster.concoop_id,   
	            yrbglonmaster.loancontract_no,   
	            yrbglonmaster.interest_accum,   
	            yrbglonmaster.misspay_amt,   
	            yrbglonmaster.avg_amt,   
	            yrbglonmaster.ravg_amt,
	            yrcfrateln.avgpercent_rate
            from yrbglonmaster,
	            lncontmaster,
	            yrcfrateln   
            where ( yrbglonmaster.coop_id = lncontmaster.coop_id )
	            and ( yrbglonmaster.loancontract_no = lncontmaster.loancontract_no )
	            and ( yrbglonmaster.coop_id = yrcfrateln.coop_id )
	            and ( yrbglonmaster.div_year = yrcfrateln.div_year )
	            and ( lncontmaster.loantype_code = yrcfrateln.loantype_code )
	            and ( yrbglonmaster.coop_id = {0} )
	            and ( yrbglonmaster.member_no = {1} )
	            and ( yrbglonmaster.div_year = {2} )
	            and ( yrcfrateln.procavg_type = 'R' )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no,ls_year);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}