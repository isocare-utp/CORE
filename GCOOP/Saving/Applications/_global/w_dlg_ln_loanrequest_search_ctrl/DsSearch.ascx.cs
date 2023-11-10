using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications._global.w_dlg_ln_loanrequest_search_ctrl
{
    public partial class DsSearch : DataSourceRepeater
    {
        public DataSet1.LNLOANREQUESTSEARCHDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANREQUESTSEARCH;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsSearch");
            this.EventItemChanged = "OnDsSearchItemChanged";
            this.EventClicked = "OnDsSearchClicked";
            this.Register();
        }
    }
}