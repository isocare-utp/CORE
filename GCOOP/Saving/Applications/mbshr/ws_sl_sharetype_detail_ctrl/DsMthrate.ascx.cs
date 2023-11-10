using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl
{
    public partial class DsMthrate :DataSourceRepeater
    {
        public DataSet1.SHSHARETYPEMTHRATEDataTable DATA { get; set; }

        public void InitDsMthrate(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHARETYPEMTHRATE;
            this.EventItemChanged = "OnDsMthrateItemChanged";
            this.EventClicked = "OnDsMthrateClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMthrate");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve(string memb_type, string sharetype)
        {
            String sql = @"  select shsharetypemthrate.sharetype_code,   
         shsharetypemthrate.start_salary,   
         shsharetypemthrate.end_salary,   
         shsharetypemthrate.entry_id,   
         shsharetypemthrate.entry_date, 
         shsharetype.unitshare_value,   
         shsharetypemthrate.sharerate_percent,  
         shsharetypemthrate.minshare_amt,   
         shsharetypemthrate.coop_id,   
         shsharetypemthrate.seq_no,   
         shsharetypemthrate.maxshare_amt,   
         shsharetypemthrate.member_type,
shsharetypemthrate.operate_type    
    from shsharetypemthrate,   
         shsharetype  
   where ( shsharetypemthrate.sharetype_code = shsharetype.sharetype_code ) and  
         ( shsharetypemthrate.coop_id = shsharetype.coop_id ) and
         (shsharetypemthrate.member_type = {0}) and 
         shsharetypemthrate.sharetype_code ={1}
                       order by shsharetypemthrate.start_salary asc";
            sql = WebUtil.SQLFormat(sql, memb_type, sharetype);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}