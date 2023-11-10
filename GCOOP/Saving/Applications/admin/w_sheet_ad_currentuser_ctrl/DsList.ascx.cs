using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.admin.w_sheet_ad_currentuser_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.SSOTOKENDataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.SSOTOKEN;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void retrieve() {

            string sql = @"select s.username,a.full_name ,ap.description,s.client_ip,s.create_time
from ssotoken s,amsecusers a,amappstatus ap
where s.username=a.user_name
and s.application = ap.application
order by s.username";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}