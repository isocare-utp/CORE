using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.keeping_const.ws_kp_const_slucfslipreturncause_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.SLUCFSLIPRETURNCAUSEDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLUCFSLIPRETURNCAUSE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }

        public void retrieve()
        {
            string sql = @"select coop_id,
                slipretcause_code,
                slipretcause_desc
                from slucfslipreturncause
                where coop_id = '" + state.SsCoopControl + @"'
                order by slipretcause_code";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}