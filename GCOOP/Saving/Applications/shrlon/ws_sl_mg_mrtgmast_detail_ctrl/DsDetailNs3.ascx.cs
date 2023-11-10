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
    public partial class DsDetailNs3 : DataSourceFormView
    {
        public DataSet1.DT_DETAILNS3DataTable DATA { get; set; }

        public void InitDsDetailNs3(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DETAILNS3;
            this.EventItemChanged = "OnDsDetailNs3ItemChanged";
            this.EventClicked = "OnDsDetailNs3Clicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetailNs3");
            this.Register();
        }

        public void Retrieve(string as_mrtgno)
        {
            string ls_sql = @"select coop_id,
                mrtgmast_no,   
                size_rai,   
                size_ngan,   
                size_wa,   
                pos_moo,   
                pos_tambol,   
                pos_amphur,   
                pos_province,   
                ns3_docno,   
                ns3_bookno,   
                ns3_pageno,   
                ns3_landno,   
                photoair_province,   
                photoair_number,   
                photoair_page  
                from lnmrtgmaster       
                where coop_id = {0}
                and mrtgmast_no = {1}  ";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_mrtgno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}