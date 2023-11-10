using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl
{
    public partial class DsDetailFail : DataSourceRepeater
    {
        public DataSet1.DataTable3DataTable DATA { get; set; }

        public void InitDsDetailFail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable3;
            this.EventItemChanged = "OnDsDetailFailItemChanged";
            this.EventClicked = "OnDsDetailFailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetailFail");
            this.Register();
        }
    }
}