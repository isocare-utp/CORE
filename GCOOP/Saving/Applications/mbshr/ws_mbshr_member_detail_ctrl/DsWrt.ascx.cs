using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl
{
    public partial class DsWrt : DataSourceRepeater
    {
        public DataSet1.WRTFUNDSTATEMENTDataTable DATA { get; set; }

        public void InitDsWRT(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.WRTFUNDSTATEMENT;
            this.EventItemChanged = "OnDsWrtItemChanged";
            this.EventClicked = "OnDsWrtClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsWrt");
            this.Register();
        }

        public void RetrieveWrt(string memb_no) {
         /*   string sql = "select * from wrtfundstatement where member_no={0} and coop_id={1} order by seq_no";
            sql = WebUtil.SQLFormat(sql, memb_no, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);*/
        
        }
    }
}