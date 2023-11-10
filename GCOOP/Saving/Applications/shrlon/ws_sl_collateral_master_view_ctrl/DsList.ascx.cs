using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string as_memno)
        {
            string ls_sql = @"select
lncollmaster.collmast_no,
lnucfcollmasttype.collmasttype_desc,
lncollmaster.dol_prince,
lncollmaster.est_price
from    lncollmaster ,lnucfcollmasttype
where lncollmaster.collmasttype_code = lnucfcollmasttype.collmasttype_code
and lncollmaster.coop_id = {0} and lncollmaster.member_no = {1}";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopId, as_memno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}