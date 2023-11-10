using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.ws_mb_mbconstant_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBCONSTANTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBCONSTANT;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void retrieve()
        {
            string sql = "select * from mbconstant where coop_id = {0} and member_type = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, this.DATA[0].MEMBER_TYPE);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}