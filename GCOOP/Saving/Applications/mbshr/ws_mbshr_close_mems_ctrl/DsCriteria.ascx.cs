using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; set; }

        public void InitDsCriteria(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.EventItemChanged = "OnDsCriteriaItemChanged";
            this.EventClicked = "OnDsCriteriaClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsCriteria");
            this.Button.Add("b_search");
            this.Register();
        }
    }
}