using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl
{
    public partial class DsSum : DataSourceFormView
    {
        public DataSet1.LNREQCONTADJUSTCOLLDataTable DATA { get; private set; }

        public void InitDsSum(PageWeb pw)
        {
            {
                css1.Visible = false;
                css2.Visible = false;
                DataSet1 ds = new DataSet1();
                this.DATA = ds.LNREQCONTADJUSTCOLL;
                this.EventItemChanged = "OnDsSumItemChanged";
                this.EventClicked = "OnDsSumClicked";
                this.InitDataSource(pw, FormView1, this.DATA, "dsSum");
                
                this.Register();
            }
        }
    }
}