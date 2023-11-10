using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl
{
    public partial class DsPayment : DataSourceFormView
    {
        public DataSet1.LNREQCONTADJUSTDETDataTable DATA { get; private set; }

        public void InitDsPayment(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQCONTADJUSTDET;
            this.InitDataSource(pw, FormView1, this.DATA, "dsPayment");
            this.EventItemChanged = "OnDsPaymentItemChanged";
            this.EventClicked = "OnDsPaymentClicked";
            //this.Button.Add("b_search");
            this.Register();
        }
    }
}