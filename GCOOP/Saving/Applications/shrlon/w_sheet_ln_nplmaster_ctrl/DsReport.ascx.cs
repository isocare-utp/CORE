using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl
{
    public partial class DsReport : DataSourceFormView
    {
        public DataSet1.LNCONTMASTERDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsReport");
            this.EventItemChanged = "OnDsReportItemChanged";
            this.EventClicked = "OnDsReportClicked";
            this.Button.Add("b_search");
            this.Register();
        }
    }
}