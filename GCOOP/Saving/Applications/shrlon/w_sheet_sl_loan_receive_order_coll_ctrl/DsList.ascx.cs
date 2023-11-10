using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_order_coll_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNCONTMASTERDataTable DATA { get; private set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTMASTER;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.Register();
        }
    }
}