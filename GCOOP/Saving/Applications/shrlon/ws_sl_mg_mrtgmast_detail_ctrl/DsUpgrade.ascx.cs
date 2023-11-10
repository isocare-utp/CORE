using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl
{
    public partial class DsUpgrade : DataSourceRepeater
    {
        public DataSet1.LNMRTGMASTUPGRADEDataTable DATA { get; set; }

        public void InitDsUpgrade(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNMRTGMASTUPGRADE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string as_mrtgno)
        {
            string ls_sql = @"select coop_id,   
                mrtgmast_no,   
                upgrade_no,   
                upgrade_date,   
                upgrade_addamt,   
                upgrade_oldamt,   
                upgrade_intrate,   
                autrz_name1,   
                autrz_name2,   
                autrz_pos1,   
                autrz_pos2,   
                autzd_name,   
                autzd_age,   
                autzd_nationality,   
                autzd_citizenship,   
                autzd_parentname,   
                autzd_village,   
                autzd_address,   
                autzd_moo,   
                autzd_soi,   
                autzd_road,   
                autzd_tambol,   
                autzd_amphur,   
                autzd_province,   
                entry_id,   
                entry_date  
                from lnmrtgmastupgrade     
                where coop_id = {0}
                and mrtgmast_no = {1}";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_mrtgno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}