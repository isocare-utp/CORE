using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_syslogtrans
{
    public partial class DsMain : DataSourceFormView
    {

        public DataSet1.syslogtransDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.syslogtrans;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        }

        public void DdTable()
        {
            string sql ="select distinct(tabnm ),tabdsc from syslogtrans";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tabname", "tabnm", "tabnm");

        }
    }
}