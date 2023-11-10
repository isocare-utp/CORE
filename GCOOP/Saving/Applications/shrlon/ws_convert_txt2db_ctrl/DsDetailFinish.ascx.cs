using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl
{
    public partial class DsDetailFinish : DataSourceRepeater
    {
        public DataSet1.DataTable4DataTable DATA { get; set; }

        public void InitDsDetailFinish(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable4;
            this.EventItemChanged = "OnDsDetailFinishChanged";
            this.EventClicked = "OnDsDetailFinishClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetailFinish");
            this.Register();
        }
    }
}