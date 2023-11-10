using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.dlg.w_dlg_ln_loan_search_ctrl
{
    public partial class DsSearch : DataSourceRepeater
    {
        public DataSet1.LNNPLSEARCHDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLSEARCH;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsSearch");
            this.EventItemChanged = "OnDsSearchItemChanged";
            this.EventClicked = "OnDsSearchClicked";
            this.Register();
        }
    }
}