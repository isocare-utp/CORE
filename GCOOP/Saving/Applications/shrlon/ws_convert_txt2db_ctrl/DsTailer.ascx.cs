using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl
{
    public partial class DsTailer : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsTailer(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsTailerItemChanged";
            this.EventClicked = "OnDsTailerClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsTailer");
            this.Button.Add("b_notpost");
            this.Button.Add("b_post");
            this.Register();
        }
    }
}