using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_ln_collredeem_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNREQCOLLMASTREDEEMDataTable DATA { get; private set; }        

        public void InitDsMain(PageWeb pw) {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQCOLLMASTREDEEM;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_collmast");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
    }
}