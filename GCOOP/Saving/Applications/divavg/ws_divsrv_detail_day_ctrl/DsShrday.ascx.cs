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
    public partial class DsShrday : DataSourceRepeater
    {
        public DataSet1.DT_SHRDAYDataTable DATA { get; set; }

        public void InitShrday(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_SHRDAY;
            this.EventItemChanged = "OnDsShrdayItemChanged";
            this.EventClicked = "OnDsShrdayClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsShrday");
            this.Register();
        }

        public void RetrieveShrday(String ls_member_no, String ls_year)
        {
            string sql = @"select yrbgshrstatement.coop_id,   
	            yrbgshrstatement.member_no,   
	            yrbgshrstatement.div_year,   
	            yrbgshrstatement.seq_no,   
	            yrbgshrstatement.share_date,   
	            yrbgshrstatement.day,   
	            yrbgshrstatement.share_in_amount,   
	            yrbgshrstatement.share_out_amount,   
	            yrbgshrstatement.share_amount,   
	            yrbgshrstatement.sharestk_amt,   
	            yrbgshrstatement.sharecal_value,   
	            yrbgshrstatement.div_amt,   
	            yrbgshrstatement.rdiv_amt
            from yrbgshrstatement  
            where ( yrbgshrstatement.coop_id = {0} )
	            and ( yrbgshrstatement.member_no = {1} )
	            and ( yrbgshrstatement.div_year = {2} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no, ls_year);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}