using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_check_ctrl
{
    public partial class DsMemcoll :DataSourceRepeater
    {
        public DataSet1.LNGRPMANGRTPERMDataTable DATA { get; set; }

        public void InitDsMemcoll(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNGRPMANGRTPERM;
            this.EventItemChanged = "OnDsMemcollItemChanged";
            this.EventClicked = "OnDsMemcollClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMemcoll");

            this.Register();
        }
        public void RetrieveMemcoll(decimal member_type)
        {
            String sql = @"  SELECT LNGRPMANGRTPERM.MANGRTPERMGRP_CODE,   
                LNGRPMANGRTPERM.MANGRTPERMGRP_DESC,   
                LNGRPMANGRTPERM.MANGRTTIME_TYPE,   
                LNGRPMANGRTPERM.EXPORTRIGTH_FLAG,   
                LNGRPMANGRTPERM.MEMBER_TYPE,   
                0.00 as coll_amt  
           FROM LNGRPMANGRTPERM  
           WHERE LNGRPMANGRTPERM.MEMBER_TYPE = {0}
                and MANGRTPERMGRP_CODE not in ('37','38')";

            sql = WebUtil.SQLFormat(sql, member_type);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}