using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_as_assistpay_ctrl
{
    public partial class DsLoan : DataSourceRepeater
    {
        public DataSet1.ASSSLIPPAYOUTDETDataTable DATA { get; set; }
        public void InitDsLoan(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSSLIPPAYOUTDET;
            this.EventItemChanged = "OnDsLoanItemChanged";
            this.EventClicked = "OnDsLoanClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsLoan");
            this.Button.Add("b_delloan");          
            this.Register();
        }
    }
}