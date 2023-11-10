using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.keeping.w_sheet_sl_update_data_from_disk_ctrl
{
    public partial class Dwlist : DataSourceRepeater
    {
        public DataSet1.DataTable2DataTable DATA { get; set; }

        public void InitDwlist(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable2;
            this.EventItemChanged = "OnDwlistItemChanged";
            this.EventClicked = "OnDwlistClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dwlist");
            this.Register();
        }
    }
}