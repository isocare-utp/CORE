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
    public partial class DsGroup : DataSourceFormView
    {
        public DataSet1.AMSECWINSGROUPDataTable DATA { get; private set; }

        public void InitDsGroup(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECWINSGROUP;
            this.InitDataSource(pw, FormView1, this.DATA, "dsGroup");
            this.EventItemChanged = "OnDsGroupItemChanged";
            this.Register();
        }

        public void DdApplication()
        {
            string sql = @"
            select application, 1 as sorter from  amsecwinsgroup  where coop_control='" + state.SsCoopControl + @"'
            union
            select '', 0 from dual order by sorter, application
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "application", "application", "application");
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