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
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNCFLOANINTRATEDETDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCFLOANINTRATEDET;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve(string loanintrate_code)
        {
            String sql = @"select coop_id,   
                             loanintrate_code,   
                             effective_date,   
                             lower_amt,   
                             expire_date,   
                             upper_amt,   
                             interest_rate  
                        from lncfloanintratedet 
                           where loanintrate_code = {0} order by  effective_date,lower_amt ";
            sql = WebUtil.SQLFormat(sql, loanintrate_code);
            DataTable dt = WebUtil.Query(sql);

            this.ImportData(dt);
        }
    }
}