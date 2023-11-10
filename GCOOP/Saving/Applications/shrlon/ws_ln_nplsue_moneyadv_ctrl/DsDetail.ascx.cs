using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_ln_nplsue_moneyadv_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.LNNPLFOLLOWMASTERDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNNPLFOLLOWMASTER;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Register();
        }

        public void Retrieve(string as_membno)
        {
            string sql = @"select coop_id,
                member_no,
                follow_seq,
                mavset_bal,
                contlaw_status
                from lnnplfollowmaster
                where coop_id = {0}
                and member_no = {1}
                and follow_seq = (select min(follow_seq) from lnnplfollowmaster)";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_membno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdContlawStatus()
        {
            string sql = @"
                select contlaw_status, contlaw_desc, 1 as sorter from lnucfcontlaw 
                union
                select 0,'', 0 from dual
                order by sorter, contlaw_status"
            ;
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "contlaw_status", "contlaw_desc", "contlaw_status");
        }
    }
}