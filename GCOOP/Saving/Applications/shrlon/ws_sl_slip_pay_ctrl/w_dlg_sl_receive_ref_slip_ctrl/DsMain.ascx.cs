using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_receive_ref_slip_ctrl;

namespace Saving.Applications.shrlon.dlg.w_dlg_sl_receive_ref_slip_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.REFSLIPDataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.REFSLIP;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
        }
    }
}