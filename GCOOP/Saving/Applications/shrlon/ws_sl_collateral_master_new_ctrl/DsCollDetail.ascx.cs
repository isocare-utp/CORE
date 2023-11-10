using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl
{
    public partial class DsCollDetail : DataSourceFormView
    {
        public DataSet1.LNCOLLDETAILDataTable DATA { get; private set; }

        public void InitDsCollDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLDETAIL;
            this.InitDataSource(pw, FormView1, this.DATA, "dsCollDetail");
            this.EventItemChanged = "OnDsCollDetailItemChanged";
            this.EventClicked = "OnDsCollDetailClicked";
            this.Register();
        }
    }
}