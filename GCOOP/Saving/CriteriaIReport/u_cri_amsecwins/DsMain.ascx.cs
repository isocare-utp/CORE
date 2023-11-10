using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_amsecwins
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.REPORT1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.REPORT1;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
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
            this.DropDownDataBind(dt, "as_application", "application", "application");
        }
    }
}