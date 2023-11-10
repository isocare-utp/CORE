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
    public partial class DsLand : DataSourceFormView
    {
        public DataSet1.DT_LANDDataTable DATA { get; set; }

        public void InitDsLand(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LAND;
            this.EventItemChanged = "OnDsLandItemChanged";
            this.EventClicked = "OnDsLandClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsLand");
            this.TableName = "LNCOLLMASTER";
            this.Register();
        }
        public void Retrieve(String collmast_no)
        {
            String sql = @"select   coop_id,
                                    collmast_no,
                                    collmasttype_code,
                                    land_ravang,
                                    land_docno,
                                    land_landno,
                                    land_survey,
                                    land_bookno,
                                    land_pageno,
                                    pos_tambol,
                                    pos_amphur,
                                    pos_province,
                                    size_rai,
                                    size_ngan,
                                    size_wa,
                                    photoair_province,
                                    photoair_number,
                                    photoair_page,
                                    priceper_unit
                            from    lncollmaster 
                            where   coop_id = {0} and collmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdCollmasttype();
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