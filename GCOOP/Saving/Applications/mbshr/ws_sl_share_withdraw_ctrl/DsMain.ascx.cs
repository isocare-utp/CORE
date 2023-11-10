using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SHSHAREMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHAREMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");

            this.Button.Add("b_withdraw");
            this.Button.Add("b_search");
            this.Button.Add("b_print");            

            this.Register();
        }

        public void RetrieveCoop()
        {
            String sql = @"select coop_id,coop_name,1 as sorter 
            from cmcoopmaster
            where coop_control = {0} 
union select '','',0 from dual order by sorter,coop_id";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "coop_id", "coop_name", "coop_id");
        }
    }
}