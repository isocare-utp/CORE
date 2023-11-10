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
    public partial class DsYear : DataSourceRepeater
    {
        public DataSet1.DT_YEARDataTable DATA { get; set; }

        public void InitYear(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_YEAR;
            this.EventItemChanged = "OnDsYearItemChanged";
            this.EventClicked = "OnDsYearClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsYear");
            this.Register();
        }

        public void RetrieveYear(String ls_member_no)
        {
            string sql = @"select yrbgmaster.coop_id,   
	            yrbgmaster.member_no,   
	            yrbgmaster.div_year,   
	            yrbgmaster.membgroup_code  
            from yrbgmaster 
            where ( yrbgmaster.coop_id = {0} )
	            and ( yrbgmaster.member_no = {1} )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}