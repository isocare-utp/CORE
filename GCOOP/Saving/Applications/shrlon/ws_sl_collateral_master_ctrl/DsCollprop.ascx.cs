using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_ctrl
{
    public partial class DsCollprop : DataSourceRepeater
    {
        public DataSet1.LNCOLLMASTPROPDataTable DATA { get; set; }

        public void InitDsCollprop(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTPROP;
            this.InitDataSource(pw, Repeater2, this.DATA, "dsCollprop");
            this.EventItemChanged = "OnDsCollpropItemChanged";
            this.EventClicked = "OnDsCollpropClicked";
            this.Button.Add("b_del");
            this.Register();

        }

        public void Retrieve(String collmast_no)
        {
            String sql = @"select * from lncollmastprop
                           where   coop_id = {0} and collmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}