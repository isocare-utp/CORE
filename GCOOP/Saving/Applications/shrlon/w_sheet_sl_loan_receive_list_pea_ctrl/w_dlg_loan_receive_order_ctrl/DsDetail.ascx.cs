using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_pea_ctrl.w_dlg_loan_receive_order_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDETDataTable DATA { get; set; }
        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYINDET;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");

            this.Register();
        }

    }
}