using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_grploanpermiss_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.LNGRPLOANPERMISSDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNGRPLOANPERMISS;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve()
        {
            String sql = @"SELECT COOP_ID,
                LOANPERMGRP_CODE,   
                LOANPERMGRP_DESC,   
                MAXPERMISS_AMT,   
                LOANGROUP_CODE  
            FROM LNGRPLOANPERMISS
            ORDER BY LOANPERMGRP_CODE  ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}