using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_detail_ctrl
{
    public partial class DsLanduse : DataSourceFormView
    {
        public DataSet1.DT_LANDUSEDataTable DATA { get; set; }

        public void InitDsLanduse(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LANDUSE;
            this.EventItemChanged = "OnDsLanduseItemChanged";
            this.EventClicked = "OnDsLanduseClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsLanduse");
            this.TableName = "LNCOLLMASTER";
            this.Register();
        }
        public void Retrieve(String collmast_no)
        {
            String sql = @"select   coop_id,
                                    collmast_no,
                                    desc_position,
                                    desc_utility,
                                    desc_travel,
                                    desc_etc
                            from lncollmaster
                            where   coop_id = {0} and collmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}