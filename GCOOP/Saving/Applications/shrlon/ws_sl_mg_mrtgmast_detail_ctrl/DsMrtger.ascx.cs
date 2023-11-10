using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl
{
    public partial class DsMrtger : DataSourceFormView
    {
        public DataSet1.DT_MRTGERDataTable DATA { get; set; }

        public void InitDsMrtger(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MRTGER;
            this.EventItemChanged = "OnDsMrtgerItemChanged";
            this.EventClicked = "OnDsMrtgerlClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMrtger");
            this.Register();
        }

        public void Retrieve(string as_mrtgno)
        {
            string ls_sql = @"select coop_id,
                mrtgmast_no,   
                mrtg_name1,   
                mrtg_name2,   
                mrtg_personid1,   
                mrtg_personid2,   
                mrtg_age1,   
                mrtg_age2,   
                mrtg_nationality1,   
                mrtg_nationality2,   
                mrtg_citizenship1,   
                mrtg_citizenship2,   
                mrtg_parentname1,   
                mrtg_parentname2,   
                mrtg_matename1,   
                mrtg_matename2,   
                mrtg_village1,   
                mrtg_village2,   
                mrtg_address1,   
                mrtg_address2,   
                mrtg_moo1,   
                mrtg_moo2,   
                mrtg_soi1,   
                mrtg_soi2,   
                mrtg_road1,   
                mrtg_road2,   
                mrtg_tambol1,   
                mrtg_tambol2,   
                mrtg_amphur1,   
                mrtg_amphur2,   
                mrtg_province1,   
                mrtg_province2  
                from lnmrtgmaster        
                where coop_id = {0}
                and mrtgmast_no = {1}  ";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_mrtgno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}