using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mthexpense_ctrl
{
    public partial class DsSum : DataSourceFormView
    {
        public DataSet1.DataTable3DataTable DATA { get; private set; }

        public void InitDsSum(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable3;
            this.InitDataSource(pw, FormViewMain, this.DATA, "dsSum");         
            this.Register();
        }
    }
}