using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saving.Applications._global.w_dlg_dp_slip_recppaytype_ctrl;

namespace Saving.Applications.deposit.w_dlg_dp_slip_recppaytype_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DPUCFRECPPAYTYPEDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPUCFRECPPAYTYPE;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMaintemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
        }

    }
}