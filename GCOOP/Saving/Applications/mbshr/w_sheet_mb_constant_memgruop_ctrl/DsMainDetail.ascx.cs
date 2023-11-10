using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.w_sheet_mb_constant_memgruop_ctrl
{
    public partial class DsMainDetail : DataSourceRepeater
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainDetailItemChanged";
            this.EventClicked = "OnDsMainDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMainDetail");
            this.Register();
        }

        public void RetrieveMemberGroup(String membgroup_code)
        {
            String sql = @"SELECT membgroup_code as ls_mbg_code, membgroup_desc as ls_mbg_name FROM MBUCFMEMBGROUP
                           WHERE coop_id = '" + state.SsCoopControl + "' and membgroup_code like '%" + membgroup_code + "%' order by ls_mbg_code";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}