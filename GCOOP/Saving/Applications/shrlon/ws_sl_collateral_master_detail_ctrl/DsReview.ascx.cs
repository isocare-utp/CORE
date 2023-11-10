using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl
{
    public partial class DsReview : DataSourceRepeater
    {
        public DataSet1.LNCOLLMASTREVIEWDataTable DATA { get; set; }

        public void InitDsReview(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTREVIEW;
            this.InitDataSource(pw, Repeater2, this.DATA, "dsReview");
            this.EventItemChanged = "OnDsReviewItemChanged";
            this.EventClicked = "OnDsReviewClicked";
           // this.Button.Add("b_delreview");
            this.Register();

        }
    }
}