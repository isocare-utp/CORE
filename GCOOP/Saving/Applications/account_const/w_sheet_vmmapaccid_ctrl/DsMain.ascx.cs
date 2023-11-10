using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.account_const.w_sheet_vmmapaccid_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.VCMAPACCIDDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.VCMAPACCID;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Button.Add("b_del");
            this.Register();
        }

        public void retrieve()
        {
            string sql = "select * from vcmapaccid order by system_code,slipitemtype_code,shrlontype_code";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}