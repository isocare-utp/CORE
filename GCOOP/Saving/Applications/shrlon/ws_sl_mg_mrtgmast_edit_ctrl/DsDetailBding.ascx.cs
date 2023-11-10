using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl
{
    public partial class DsDetailBding : DataSourceFormView
    {
        public DataSet1.DT_BDINGDataTable DATA { get; set; }

        public void InitDsDetailBding(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_BDING;
            this.EventItemChanged = "OnDsDetailBdingItemChanged";
            this.EventClicked = "OnDsDetailBdingClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetailBding");
            this.TableName = "lnmrtgmaster";
            this.Register();
        }

        public void Retrieve(String ls_mrtgno)
        {
            String sql = @"select   coop_id,
                                    mrtgmast_no,
                                    assettype_code,
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
                                    bd_age,
                                    '01' as collmasttype_code
                            from lnmrtgmaster  
                            where   coop_id = {0} and mrtgmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_mrtgno);
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