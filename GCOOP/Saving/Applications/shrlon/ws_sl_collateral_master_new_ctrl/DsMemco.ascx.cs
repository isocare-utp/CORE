using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl
{
    public partial class DsMemco : DataSourceRepeater
    {
        public DataSet1.LNCOLLMASTMEMCODataTable DATA { get; set; }

        public void InitDsMemco(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTMEMCO;
            this.InitDataSource(pw, Repeater2, this.DATA, "dsMemco");
            this.EventItemChanged = "OnDsMemcoItemChanged";
            this.EventClicked = "OnDsMemcoClicked";
            this.Button.Add("b_delete");
            this.Button.Add("b_search");
            this.Register();

        }
    }
}