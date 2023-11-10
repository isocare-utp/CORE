using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_ctrl
{
    public partial class DsCondo : DataSourceFormView
    {
        public DataSet1.DT_CONDODataTable DATA { get; set; }

        public void InitDsCondo(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_CONDO;
            this.EventItemChanged = "OnDsCondoItemChanged";
            this.EventClicked = "OnDsCondoClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsCondo");
            this.TableName = "LNCOLLMASTER";
            this.Register();
        }
        public void Retrieve(String collmast_no)
        {
            String sql = @"select   coop_id,
                                    collmast_no,
                                    condo_registno,
                                    condo_name,
                                    condo_towerno,
                                    condo_floor,
                                    condo_roomno,
                                    condo_roomsize,
                                    condo_pricesquare,
                                    pos_tambol,
                                    pos_amphur,
                                    pos_province,
                                    condo_depreciation,
                                    condo_age,
                                    bd_onlandno
                            from    lncollmaster 
                            where   coop_id = {0} and collmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}