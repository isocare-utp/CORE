using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dp_import_text_salary_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DTMAINDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DTMAIN;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChange";
            this.EventClicked = "OnDsMainClick";
            this.Register();
        }
    }
}