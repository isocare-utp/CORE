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
    public partial class DsBding : DataSourceFormView
    {
        public DataSet1.DT_BDINGDataTable DATA { get; set; }

        public void InitDsBding(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_BDING;
            this.EventItemChanged = "OnDsBdingItemChanged";
            this.EventClicked = "OnDsBdingClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsBding");
            this.TableName = "LNCOLLMASTER";
            this.Register();
        }

        public void Retrieve(String collmast_no)
        {
            String sql = @"select   coop_id,
                                    collmast_no,
                                    collmasttype_code,
                                    bd_village,
                                    bd_addrno,
                                    bd_addrmoo,
                                    bd_soi,
                                    bd_road,
                                    bd_tambol,
                                    bd_amphur,
                                    bd_province,
                                    bd_type,
                                    bd_depreciation,
                                    bd_onlandno,
                                    bd_sumprice,
                                    bd_age
                            from lncollmaster  
                            where   coop_id = {0} and collmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdBdtype();
            DdCollmasttype();
        }

        public void DdBdtype()
        {
            string sql = @"select
                        buildingtype_code,building_desc, 1 as sorter
                        from lnucfbuildingtype
                        union
                        select '','',0 from dual order by sorter, buildingtype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bd_type", "building_desc", "buildingtype_code");

        }

        public void DdCollmasttype()
        {
            string sql = @"select collmasttype_code,collmasttype_desc, 1 as sorter
                           from lnucfcollmasttype  
                           union
                           select '','',0 from dual order by sorter, collmasttype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "collmasttype_code", "collmasttype_desc", "collmasttype_code");

        }
    }
}