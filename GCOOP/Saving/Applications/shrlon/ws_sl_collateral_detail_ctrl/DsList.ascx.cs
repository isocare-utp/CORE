using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_detail_ctrl
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
            this.InitDataSource(pw, Repeater3, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(string member_no)
        {
            String sql = @"select 
lncollmaster.collmast_no,
mbmembmaster.member_no,
mbmembmaster.salary_id, 
mbucfprename.prename_desc,
mbmembmaster.memb_name,
mbmembmaster.memb_surname,
lnucfcollmasttypegrp.collmasttype_desc,
lnucfcollmasttypegrp.collmasttype_grp,
lnucfcollmasttype.collmasttype_desc,
lncollmaster.est_price
from lncollmaster , mbmembmaster,mbucfprename,lnucfcollmasttypegrp,lnucfcollmasttype
where
lncollmaster.member_no = mbmembmaster.member_no and
mbucfprename.prename_code = mbmembmaster.prename_code and 
lnucfcollmasttypegrp.collmasttype_grp = lncollmaster.collmasttype_grp and
lnucfcollmasttype.collmasttype_code(+) = lncollmaster.collmasttype_code and
mbmembmaster.member_no = {0} and
mbmembmaster.coop_id = {1}    
order by lncollmaster.collmast_no";
            sql = WebUtil.SQLFormat(sql, member_no,state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}