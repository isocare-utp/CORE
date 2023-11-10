using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cfloantype_ctrl
{
    public partial class DsLoantypeSalbal : DataSourceRepeater
    {
        public DataSet1.LNLOANTYPESALBALDataTable DATA { get; set; }

        public void InitDsLoantypeSalbal(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPESALBAL;
            this.EventItemChanged = "OnDsLoantypeSalbalItemChanged";
            this.EventClicked = "OnDsLoantypeSalbalClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsLoantypeSalbal");
            this.Button.Add("b_dellnbal");
            this.Register();
        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT * FROM LNLOANTYPESALBAL WHERE COOP_ID = {0} and LOANTYPE_CODE = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}