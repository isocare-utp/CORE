using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl
{
    public partial class DsColluseSum : DataSourceFormView
    {
        public DataSet1.LNCONTCOLLDataTable DATA { get; set; }

        public void InitDsColluseSum(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTCOLL;
            this.EventItemChanged = "OnDsColluseSumItemChanged";
            this.EventClicked = "OnDsColluseSumClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsColluseSum");
            //this.Button.Add("b_memsearch");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
    }
}