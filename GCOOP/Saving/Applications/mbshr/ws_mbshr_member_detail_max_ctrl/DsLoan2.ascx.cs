using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl
{
    public partial class DsLoan2 : DataSourceFormView
    {
        public DataSet1.DT_LOANDataTable DATA { get; set; }

        public void InitDsLoan2(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LOAN;
            this.EventItemChanged = "OnDsLoan2ItemChanged";
            this.EventClicked = "OnDsLoan2Clicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsLoan2");
            this.Register();
        }
    }
}