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
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNGRPMANGRTPERMDETDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNGRPMANGRTPERMDET;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve(string mangrtpermgrp_code)
        {
            String sql = @"select 
                mangrtpermgrp_code, 
                seq_no, 
                startshare_amt, 
                endshare_amt, 
                startmember_time, 
                endmember_time, 
                start_salary,
                end_salary,
                percentshare, 
                percentsalary, 
                maxgrt_amt, 
                coop_id,
                multiple_share,
                multiple_salary
            from lngrpmangrtpermdet
            where mangrtpermgrp_code = {0}";
            sql = WebUtil.SQLFormat(sql,mangrtpermgrp_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}