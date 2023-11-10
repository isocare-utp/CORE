using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications._global.w_dlg_dp_deposit_search_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DPDEPTMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMaintemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
        }

        public void DpBranchGroup()
        {
            string sql = @"
            SELECT coop_id, 1 as sorter
            FROM dpdeptmaster
       union
            SELECT '', 0 from dual   
            ORDER BY sorter 
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            this.DropDownDataBind(sql, "coop_id", "coop_id", "coop_id");
        }
    }
}