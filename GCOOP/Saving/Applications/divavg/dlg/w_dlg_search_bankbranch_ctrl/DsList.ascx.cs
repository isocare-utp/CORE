using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.dlg.w_dlg_search_bankbranch_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.CMUCFBANKBRANCHDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMUCFBANKBRANCH;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }

        public void Retrieve(String bank_code, String branch_name)
        {
            String sql = @"select branch_id,   
	            branch_name
            from cmucfbankbranch 
            where bank_code = {0}
	            and branch_name like " + "'%" + branch_name + "%'";

            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}