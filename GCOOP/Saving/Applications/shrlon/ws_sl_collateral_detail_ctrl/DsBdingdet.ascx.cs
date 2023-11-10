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
    public partial class DsBdingdet : DataSourceRepeater
    {
        public DataSet1.LNCOLLMASTBUILDINGDataTable DATA { get; set; }

        public void InitDsBdingdet(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTBUILDING;
            this.EventItemChanged = "OnDsBdingdetItemChanged";
            this.EventClicked = "OnDsBdingdetClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsBdingdet");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve(String collmast_no)
        {
            String sql = @"select coop_id,
                            collmast_no,
                            floor_no,
                            floor_square,
                            floor_pricesquare,
                            floor_sumprice
                            from lncollmastbuilding
                           where   coop_id = {0} and collmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}