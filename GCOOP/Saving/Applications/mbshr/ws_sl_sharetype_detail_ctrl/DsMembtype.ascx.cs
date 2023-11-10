using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl
{
    public partial class DsMembtype : DataSourceFormView
    {
        public DataSet1.membtypeDataTable DATA { get; set; }

        public void InitDsMembtype(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.membtype;
            this.EventItemChanged = "OnDsMembtypeItemChanged";
            this.EventClicked = "OnDsMembtypeClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMembtype");
            this.Register();
        }
    }
}