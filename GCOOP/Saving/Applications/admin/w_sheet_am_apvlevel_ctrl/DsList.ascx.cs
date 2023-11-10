using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_am_apvlevel_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.AMSECAPVLEVELDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw) {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECAPVLEVEL;
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve() {
            string sql = "select APVLEVEL_ID,DESCRIPTION from amsecapvlevel order by apvlevel_id";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        
        }
    }
}