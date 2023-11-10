using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.divavg.ws_divsrv_opr_slip_ccl_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.DT_DETAILDataTable DATA { get; set; }

        public void InitDetail(PageWeb pw)
        {

            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DETAIL;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Register();

        }
    }
}