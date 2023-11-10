using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl
{
    public partial class DsColluse : DataSourceRepeater
    {
        public DataSet1.LNCONTCOLLDataTable DATA { get; set; }

        public void InitDsColluse(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTCOLL;
            this.InitDataSource(pw, Repeater2, this.DATA, "dsColluse");
            this.EventItemChanged = "OnDsColluseItemChanged";
            this.EventClicked = "OnDsColluseClicked";
            this.Button.Add("b_detail");
            this.Register();

        }
    }
}