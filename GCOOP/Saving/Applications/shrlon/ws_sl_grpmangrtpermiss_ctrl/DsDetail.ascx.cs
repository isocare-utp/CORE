using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_grpmangrtpermiss_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.LNGRPMANGRTPERMDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNGRPMANGRTPERM;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Register();
        }

        public void Retrieve(string mangrtpermgrp_code)
        {
            String sql = @"select mangrtpermgrp_code, 
                                mangrtpermgrp_desc, 
                                mangrttime_type, 
                                member_type, 
                                grtright_contflag, 
                                grtright_memflag, 
                                grtright_contract, 
                                grtright_member, 
                                coop_id,
                                member_time
                           from lngrpmangrtperm
                           where mangrtpermgrp_code = {0}";
            sql = WebUtil.SQLFormat(sql, mangrtpermgrp_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}