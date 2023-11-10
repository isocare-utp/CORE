using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl
{
    public partial class DsDetailDeed : DataSourceFormView
    {
        public DataSet1.DT_DETAILDEEDDataTable DATA { get; set; }

        public void InitDsDetailDeed(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DETAILDEED;
            this.EventItemChanged = "OnDsDetailDeedItemChanged";
            this.EventClicked = "OnDsDetailDeedClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetailDeed");
            this.Register();
        }

        public void Retrieve(string as_mrtgno)
        {
            string ls_sql = @"select coop_id,
                mrtgmast_no,   
                size_rai,   
                size_ngan,   
                size_wa,   
                pos_tambol,   
                pos_amphur,   
                pos_province,   
                land_docno,   
                land_ravang,   
                land_survey,   
                land_landno,   
                land_bookno,   
                land_pageno  
                from lnmrtgmaster      
                where coop_id = {0}
                and mrtgmast_no = {1}  ";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_mrtgno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}