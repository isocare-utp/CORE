using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_template_autzd_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNMRTGTEMPLATEAUTZDDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;            
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNMRTGTEMPLATEAUTZD;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            //this.Button.Add("b_search");
            this.Register();
        }

        public void Retrieve(decimal adc_templateno)
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
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, adc_templateno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}