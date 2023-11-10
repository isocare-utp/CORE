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
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.LNCFLOANINTRATEDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCFLOANINTRATE;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Register();
        }

        public void Retrieve(string loanintrate_code)
        {
            String sql = @"select 
                            coop_id,
                            loanintrate_code,   
                            loanintrate_desc  
                           from lncfloanintrate 
                           where loanintrate_code = {0}   ";
            sql = WebUtil.SQLFormat(sql, loanintrate_code);
            DataTable dt = WebUtil.Query(sql);

            this.ImportData(dt);
        }
    }
}