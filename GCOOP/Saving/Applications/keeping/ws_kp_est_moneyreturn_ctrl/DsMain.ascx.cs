using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.keeping.ws_kp_est_moneyreturn_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_post");
            this.Register();
        }

        public void DdStartMembgroup()
        {
            string sql = @"select membgroup_code,membgroup_code +'-'+ isnull(membgroup_desc,'') as membgroup_desc,1 as sorter from mbucfmembgroup 
union
select '','',0 order by sorter , membgroup_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "smembgroup_code", "membgroup_desc", "membgroup_code");
        }

        public void DdEndMembgroup()
        {
            string sql = @"select membgroup_code,membgroup_code +'-'+ isnull(membgroup_desc,'') as membgroup_desc,1 as sorter from mbucfmembgroup 
union
select '','',0 order by sorter , membgroup_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "emembgroup_code", "membgroup_desc", "membgroup_code");
        }
    }
}