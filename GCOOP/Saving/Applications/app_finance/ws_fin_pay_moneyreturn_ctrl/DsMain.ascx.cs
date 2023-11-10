using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_pay_moneyreturn_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.TABLEMAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.TABLEMAIN;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_retrieve");
            //this.Button.Add("b_process");
            this.Register();
            

        }

        public void DDStartMembgroup() {

            string sql = "select membgroup_code,membgroup_desc,membgroup_code||'-'||membgroup_desc as group_desc from mbucfmembgroup order by membgroup_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "start_membgroup", "group_desc", "membgroup_code");
        }

        public void DDEndMembgroup()
        {

            string sql = "select membgroup_code,membgroup_desc,membgroup_code||'-'||membgroup_desc as group_desc from mbucfmembgroup order by membgroup_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "end_membgroup", "group_desc", "membgroup_code");
        }
    }
}