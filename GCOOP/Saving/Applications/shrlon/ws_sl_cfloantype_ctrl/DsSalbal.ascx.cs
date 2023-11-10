using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cfloantype_ctrl
{
    public partial class DsSalbal : DataSourceFormView
    {
        public DataSet1.LNLOANCKSALBALMAINDataTable DATA { get; set; }

        public void InitDsSalbal(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANCKSALBALMAIN;
            this.EventItemChanged = "OnDsSalbalItemChanged";
            this.EventClicked = "OnDsSalbalClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsSalbal");
            this.Register();
        }
    }
}