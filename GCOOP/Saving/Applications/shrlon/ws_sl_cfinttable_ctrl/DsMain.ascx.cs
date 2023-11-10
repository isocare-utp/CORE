using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_cfinttable_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.LNCFLOANINTRATEDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCFLOANINTRATE;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve()
        {
            String sql = @"select 
                            coop_id,
                            loanintrate_code,   
                            loanintrate_desc  
                           from lncfloanintrate order by loanintrate_code";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);

            this.ImportData(dt);
        }
    }
}