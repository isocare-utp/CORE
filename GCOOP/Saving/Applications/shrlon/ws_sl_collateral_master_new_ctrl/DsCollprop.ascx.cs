using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl
{
    public partial class DsCollprop : DataSourceRepeater
    {
        public DataSet1.LNCOLLMASTPROPDataTable DATA { get; set; }

        public void InitDsCollprop(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTPROP;
            this.InitDataSource(pw, Repeater2, this.DATA, "dsCollprop");
            this.EventItemChanged = "OnDsCollpropItemChanged";
            this.EventClicked = "OnDsCollpropClicked";
            this.Button.Add("b_delprop");
            this.Register();

        }
    }
}