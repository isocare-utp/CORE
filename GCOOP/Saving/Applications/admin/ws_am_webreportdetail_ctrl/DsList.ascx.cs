using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.admin.ws_am_webreportdetail_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.WEBREPORTDETAILDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.WEBREPORTDETAIL;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("B_DEL");
            this.Register();

        }

        public void retrieve(string group_id) {
            string sql = "select * from webreportdetail where group_id={0}";
            sql = WebUtil.SQLFormat(sql, group_id);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}