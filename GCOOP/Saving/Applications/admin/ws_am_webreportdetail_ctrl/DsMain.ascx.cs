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
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.WEBREPORTGROUPDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw) {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.WEBREPORTGROUP;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        
        }

        public void DDapplication() {
            string sql = @"select distinct(application ) as application ,1 as sorter from webreportgroup 
union
select '',0 from dual order by sorter, application";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "application", "application", "application");
        }

        public void DDgroup(string application) { 
        string sql = @"select  group_id,group_name,1 as sorter from webreportgroup where application={0}
union
select '','',0 from dual order by sorter, group_id";
            sql = WebUtil.SQLFormat(sql,application);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "group_id", "group_name", "group_id");
        }
        
        
    }
}