using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfprename_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.MBUCFPRENAMEDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFPRENAME;
            this.Button.Add("b_del");
            this.EventClicked = "OnDsMainClicked";
            this.EventItemChanged = "OnDsMainItemChanged";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve() {
            string sql = "select * from mbucfprename order by PRENAME_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}