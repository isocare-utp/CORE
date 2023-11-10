using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Saving.Applications.deposit_const.w_sheet_dp_const_dpucfcancelchq_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.DPUCFCANCELCHQDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPUCFCANCELCHQ;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Button.Add("b_del");
            this.Register();
        }

        public void retrieve()
        {
            string sql = "select * from dpucfcancelchq order by CANCEL_ID";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}