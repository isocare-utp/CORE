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
    public partial class DsAutz : DataSourceFormView
    {
        public DataSet1.DT_AUTZDataTable DATA { get; set; }

        public void InitDsAutz(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_AUTZ;
            this.EventItemChanged = "OnDsAutzItemChanged";
            this.EventClicked = "OnDsAutzClicked";
            this.InitDataSource(pw, FormView2, this.DATA, "dsAutz");
            this.Register();
        }

        public void Retrieve(string as_mrtgno)
        {
            string ls_sql = @"select coop_id,
                mrtgmast_no,   
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
                autrz_date   
                from lnmrtgmaster     
                where coop_id = {0}
                and mrtgmast_no = {1}  ";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_mrtgno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}