using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.assist.dlg.w_dlg_sl_member_search_tks_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet2.MEMBLISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet2 ds = new DataSet2();
            this.DATA = ds.MEMBLIST;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }
    }
}