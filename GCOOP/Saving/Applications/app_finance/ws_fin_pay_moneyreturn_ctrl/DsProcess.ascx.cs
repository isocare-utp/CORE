using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.ws_fin_pay_moneyreturn_ctrl
{
    public partial class DsProcess : DataSourceFormView

    {
        public DataSet1.TABLEPROCESSDataTable DATA { get; set; }

        public void InitDsProcess(PageWeb pw)
        {
            //css1.Visible = false;
            //css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.TABLEPROCESS;
            this.InitDataSource(pw, FormView1, this.DATA, "dsProcess");
            this.EventItemChanged = "OnDsProcessItemChanged";
            this.EventClicked = "OnDsProcessClicked";

            this.Button.Add("b_process");
            this.Register();

        }
    }
}