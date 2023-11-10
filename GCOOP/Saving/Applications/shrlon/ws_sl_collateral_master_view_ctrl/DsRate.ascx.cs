using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl
{
    public partial class DsRate : DataSourceFormView
    {
        public DataSet1.DT_RATEDataTable DATA { get; set; }

        public void InitDsRate(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_RATE;
            this.EventItemChanged = "OnDsRateItemChanged";
            this.EventClicked = "OnDsRateClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsRate");
            this.TableName = "LNCOLLMASTER";
            this.Register();
        }

        public void Retrieve(String collmast_no)
        {
            String sql = @"select   coop_id,
                                    collmast_no,
                                    dol_prince,
                                    est_percent,
                                    est_price
                            from lncollmaster
                            where   coop_id = {0} and collmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}