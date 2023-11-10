using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_detail_ctrl
{
    public partial class DsShrmonth : DataSourceRepeater
    {
        public DataSet1.DT_SHRMONTHDataTable DATA { get; set; }

        public void InitShrmonth(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_SHRMONTH;
            this.EventItemChanged = "OnDsShrmonthItemChanged";
            this.EventClicked = "OnDsShrmonthClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsShrmonth");
            this.Register();

        }
        public void RetrieveShrmonth(String ls_member_no, String ls_year)
        {
            string sql = @"select yrbgshrmth.coop_id,   
	            yrbgshrmth.member_no,   
	            yrbgshrmth.div_year,   
	            yrbgshrmth.seq_no,     
	            yrbgshrmth.share_in_amount,   
	            yrbgshrmth.share_out_amount,   
	            yrbgshrmth.share_amount,   
	            yrbgshrmth.sharecal_value,   
	            yrbgshrmth.div_amt,   
	            yrbgshrmth.rdiv_amt,
	            case mth_code when '00' then 'ยกมาต้นปี' 
	            when '12' then 'ยกไปต้นปีหน้า' 
	            else mth_code + ' (x ' + cast( (12 - cast(mth_code as NUMERIC)) as varchar) + ' ด)' end as mth_code 
            from yrbgshrmth   
            where ( yrbgshrmth.coop_id = {0} )
	            and ( yrbgshrmth.member_no = {1} )
	            and ( yrbgshrmth.div_year = {2} )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no, ls_year);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }

        public TextBox getdiv_rate
        {
            get { return this.div_rate; }
        }

    }
}