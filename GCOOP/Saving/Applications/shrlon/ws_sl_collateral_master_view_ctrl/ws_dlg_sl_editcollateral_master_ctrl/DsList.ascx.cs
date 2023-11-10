using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl.ws_dlg_sl_editcollateral_master_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater3, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(string sql_search)
        {
            String sql = @"select 
lncollmaster.collmast_no,
mbmembmaster.member_no,
mbmembmaster.salary_id, 
mbucfprename.prename_desc,
mbmembmaster.memb_name,
mbmembmaster.memb_surname,
lnucfcollmasttypegrp.collmasttype_desc,
lnucfcollmasttype.collmasttype_desc,
lncollmaster.est_price
from lncollmaster , mbmembmaster,mbucfprename,lnucfcollmasttypegrp,lnucfcollmasttype
where
lncollmaster.member_no = mbmembmaster.member_no and
mbucfprename.prename_code = mbmembmaster.prename_code and 
lnucfcollmasttypegrp.collmasttype_grp = lncollmaster.collmasttype_grp and
lnucfcollmasttype.collmasttype_code(+) = lncollmaster.collmasttype_code " + sql_search +
                            " order by lncollmaster.collmast_no";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}