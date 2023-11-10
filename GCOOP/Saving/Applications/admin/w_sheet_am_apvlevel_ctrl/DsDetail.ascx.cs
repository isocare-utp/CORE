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
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.AMSECAPVLEVELDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw) {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECAPVLEVEL;
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Register();
        }

        public void RetrieveApv(string apv_id) {
            string sql = "select * from amsecapvlevel where apvlevel_id={0}";
            sql = WebUtil.SQLFormat(sql, apv_id);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}