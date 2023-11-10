using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.dlg.w_dlg_ln_collmast_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_ListDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_List;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }

        public void Retrieve(string as_memno)
        {
            string ls_sql = @"select lcm.collmast_no,
                lcm.collmast_refno,
                lmt.collmasttype_desc,
                lcm.collmast_desc,
                lcm.estimate_price
                from lncollmaster lcm , lnucfcollmasttype lmt, lncollmastmemco lmc
                where lcm.collmasttype_code = lmt.collmasttype_code
                and lcm.coop_id = lmc.coop_id
                and lcm.collmast_no = lmc.collmast_no
                and lcm.coop_id = {0}
                and lmc.memco_no = {1}
                and lcm.redeem_flag = 0";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_memno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}