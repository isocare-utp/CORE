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
    public partial class DsMaster : DataSourceFormView
    {
        public DataSet1.DT_MASTERDataTable DATA { get; set; }

        public void InitDsMaster(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MASTER;
            this.EventItemChanged = "OnDsMasterItemChanged";
            this.EventClicked = "OnDsMasterClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMaster");
            this.Register();
        }
        public void RetrieveMaster(String ls_member_no, String ls_year)
        {
            String sql = @"select yrbgmaster.coop_id,   
	            yrbgmaster.member_no,   
	            yrbgmaster.div_year,   
	            yrdivmaster.div_amt,   
	            yrdivmaster.div_balamt,   
	            yrdivmaster.avg_amt,   
	            yrdivmaster.avg_balamt
            from yrdivmaster right join yrbgmaster on yrdivmaster.coop_id = yrbgmaster.coop_id and yrdivmaster.member_no = yrbgmaster.member_no  
            where 
	             
	            ( yrdivmaster.div_year = yrbgmaster.div_year )
	            and ( yrbgmaster.coop_id = {0} )
	            and ( yrbgmaster.member_no = {1} )
	            and ( yrbgmaster.div_year = {2} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no, ls_year);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}