using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_deptadjust_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.DETAILSETDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DETAILSET;
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.Register();


        }

    }
}