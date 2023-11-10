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
    public partial class DsDpcancoll : DataSourceRepeater
    {
        public DataSet1.DPDEPTTYPEDataTable DATA { get; set; }

        public void InitDsDpcancoll(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTTYPE;
            this.EventItemChanged = "OnDsDpcancollItemChanged";
            this.EventClicked = "OnDsDpcancollClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDpcancoll");
            this.Register();
        }

        public void RetrieveDep()
        {
            String sql = @"select depttype_code, depttype_desc, coop_id from dpdepttype order by depttype_code";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}