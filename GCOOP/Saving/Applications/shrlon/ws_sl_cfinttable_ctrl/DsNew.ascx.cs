using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_cfinttable_ctrl
{
    public partial class DsNew : DataSourceRepeater
    {
        public DataSet1.LNCFLOANINTRATEDataTable DATA { get; set; }

        public void InitDsNew(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCFLOANINTRATE;
            this.EventItemChanged = "OnDsNewItemChanged";
            this.EventClicked = "OnDsNewClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsNew");
            this.Register();
        }

    }
}