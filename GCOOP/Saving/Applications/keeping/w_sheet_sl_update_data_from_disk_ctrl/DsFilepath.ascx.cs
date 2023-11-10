using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.keeping.w_sheet_sl_update_data_from_disk_ctrl
{
    public partial class DsFilepath : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsFilepath(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.InitDataSource(pw, FormView1, this.DATA, "dsFilepath");
            this.EventItemChanged = "OnDsFilepathItemChanged";
            this.EventClicked = "OnDsFilepathClicked";
            this.Button.Add("cb_1");
            this.Button.Add("cb_2");
            //this.Button.Add("Import");
            //this.Button.Add("Clear");
            //this.Button.Add("Update");
            this.Register();
        }
    }
}