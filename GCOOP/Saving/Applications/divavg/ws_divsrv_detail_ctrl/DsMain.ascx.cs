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
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }
        public void RetrieveMain(String ls_member_no)
        {
            String sql = @"select mbucfmembgroup.membgroup_code,   
	            mbucfmembgroup.membgroup_desc,   
	            mbucfprename.prename_desc,   
	            mbmembmaster.memb_name,   
	            mbucfmembtype.membtype_code,   
	            mbucfmembtype.membtype_desc,   
	            mbmembmaster.coop_id,   
	            mbmembmaster.member_no,   
	            mbmembmaster.memb_surname,
                mbmembmaster.member_date,
	            shsharemaster.sharestk_amt,
	            shsharetype.unitshare_value
            from mbmembmaster,   
	            mbucfmembgroup,   
	            mbucfprename,   
	            mbucfmembtype,
	            shsharemaster,
	            shsharetype
            where ( mbmembmaster.coop_id = shsharemaster.coop_id )
	            and ( mbmembmaster.member_no = shsharemaster.member_no )
	            and ( shsharemaster.coop_id = shsharetype.coop_id )
	            and ( shsharemaster.sharetype_code = shsharetype.sharetype_code )
	            and ( mbmembmaster.coop_id = mbucfmembgroup.coop_id (+))  
	            and ( mbmembmaster.coop_id = mbucfmembtype.coop_id (+))  
	            and ( mbmembmaster.membtype_code = mbucfmembtype.membtype_code (+))  
	            and ( mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code (+))  
	            and ( mbmembmaster.prename_code = mbucfprename.prename_code (+))
	            and ( mbmembmaster.coop_id = {0} )
	            and ( mbmembmaster.member_no = {1} )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdDivYear(String ls_member_no)
        {
            string sql = @"
            select coop_id,   
                member_no,   
                div_year,   
                membgroup_code,1 as sorter  
            from yrbgmaster 
            where ( yrbgmaster.coop_id = {0} )
                and ( yrbgmaster.member_no = {1} ) 
            union select '','','','',0 order by sorter,div_year desc";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "div_year", "DIV_YEAR", "DIV_YEAR");
        }
    }
}