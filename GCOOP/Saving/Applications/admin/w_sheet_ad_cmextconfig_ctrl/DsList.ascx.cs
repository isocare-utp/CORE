using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.admin.w_sheet_ad_cmextconfig_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.CMEXTCONFIGDataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMEXTCONFIG;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
            this.Button.Add("b_del");
        }

        public void retrieve() {

            string sql = @"select * from cmextconfig";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}