using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_am_amsecwinsgroup_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.AMSECWINSGROUPDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECWINSGROUP;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        }

        public void DdApplication()
        {
            string sql = @"
            select application, 1 as sorter from  amappstatus  where coop_id = '" + state.SsCoopControl + @"' and used_flag = 1
            union
            select '', 0 from dual order by sorter, application
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "application", "application", "application");
        }
    }
}