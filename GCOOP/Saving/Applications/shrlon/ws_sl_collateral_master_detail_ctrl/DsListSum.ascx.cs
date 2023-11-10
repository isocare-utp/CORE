using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl
{
    public partial class DsListSum : DataSourceFormView
    {
        public DataSet1.LNCOLLMASTERDataTable DATA { get; set; }

        public void InitDsListSum(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsListSum");
            this.EventItemChanged = "OnDsListSumItemChanged";
            this.EventClicked = "OnDsListSumClicked";
            //this.Button.Add("b_detail");
            this.Register();

        }
    }
}