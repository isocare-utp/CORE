using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_tp_upg_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.LNMRTGMASTUPGRADEDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNMRTGMASTUPGRADE;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Button.Add("b_autzd");
            this.Button.Add("b_mrtgsearch");
            this.Register();
        }

        public void Retrieve(decimal adc_upgradeno)
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
                and upgrade_no = {1}  ";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, adc_upgradeno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }

        public void RetrieveAutzd(decimal as_tempno)
        {
            string ls_sql = @"select coop_id,
                template_no,   
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
                autzd_province  
                from lnmrtgtemplateautzd     
                where coop_id = {0}
                and template_no = {1}";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_tempno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}