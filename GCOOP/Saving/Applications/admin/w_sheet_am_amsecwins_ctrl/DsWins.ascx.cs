using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_am_amsecwins_ctrl
{
    public partial class DsWins : DataSourceRepeater
    {
        public DataSet1.AMSECWINSDataTable DATA { get; private set; }

        public void InitDsWins(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECWINS;
            this.EventItemChanged = "OnDsWinsItemChanged";
            this.EventClicked = "OnDsWinsClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsWins");
            this.Button.Add("B_DEL");
            this.Register();
        }

        public void Retrieve(string application, string group_code)
        {
            string sql = "select * from amsecwins where application='" + application + "' and coop_control='" + state.SsCoopControl + "' and group_code='" + group_code + "' order by win_order";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdGroupCode(application);
        }

        public void DdGroupCode(string application)
        {
            string sql = @"
            select group_code, group_code + ' - ' + group_desc as group_name, 1 as sorter  from  amsecwinsgroup  where coop_control='" + state.SsCoopControl + @"' and application='" + application + @"'
            union
            select '', '', 0 from dual order by sorter, group_code
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "group_code", "group_name", "group_code");
        }
    }
}