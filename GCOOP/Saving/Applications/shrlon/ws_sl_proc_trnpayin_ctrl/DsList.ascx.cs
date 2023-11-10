using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_proc_trnpayin_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DtlistDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.Dtlist;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }

        public void Retrieve(string as_trnsource, DateTime adtm_trans)
        {
            string ls_sql = @"select slipitemtype_desc, count(1) as tran_count, sum(trans_amt) as trans_amt 
                from sltranspayin , slucfslipitemtype
                where sltranspayin.transitem_code = slucfslipitemtype.slipitemtype_code
                and coop_id = {0}
                and trnsource_code = {1}
                and trans_date = {2}
                and post_status = 0
                group by slipitemtype_desc";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_trnsource, adtm_trans);
            DataTable dt = WebUtil.Query(ls_sql);
            ImportData(dt);
        }
    }
}