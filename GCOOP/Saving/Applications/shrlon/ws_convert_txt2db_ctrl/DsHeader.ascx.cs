using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl
{
    public partial class DsHeader : DataSourceFormView
    {
        public DataSet1.DataTable2DataTable DATA { get; set; }

        public void InitDsHeader(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable2;
            this.EventItemChanged = "OnDsHeaderItemChanged";
            this.EventClicked = "OnDsHeaderClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsHeader");
            //this.Button.Add("b_memsearch");
            // this.Button.Add("b_contsearch");
            this.Register();
        }
    }

}