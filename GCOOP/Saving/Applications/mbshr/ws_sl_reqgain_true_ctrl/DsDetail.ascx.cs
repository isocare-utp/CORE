using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_sl_reqgain_true_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.MBGAINMASTERDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBGAINMASTER;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve(string mem_no)
        {
            String sql = @"select * from   mbgainmaster
                            where   coop_id ={0} and member_no ={1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, mem_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}