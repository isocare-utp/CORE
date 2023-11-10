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
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve()
        {
            string sql = @"select
                        mangrtpermgrp_code, mangrtpermgrp_code+' - '+mangrtpermgrp_desc as display, 1 as sorter
                        from lngrpmangrtperm
                        union
                        select '','',0  order by sorter, mangrtpermgrp_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "mangrtpermgrp_code", "display", "mangrtpermgrp_code");
        }
    }
}