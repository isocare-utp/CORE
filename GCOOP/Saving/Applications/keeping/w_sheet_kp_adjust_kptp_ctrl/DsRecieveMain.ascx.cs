using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.keeping.w_sheet_kp_adjust_kptp_ctrl
{
    public partial class DsRecieveMain : DataSourceFormView
    {
        public DataSet1.KPTEMPRECEIVE_2DataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.KPTEMPRECEIVE_2;
            this.EventItemChanged = "OnDsRecieveMainItemChanged";
            this.EventClicked = "OnDsRecieveMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsRecieveMain");
            this.Register();
        }

        public void Retrieve(String member_no, String recv_period)
        {
            String coop_id = state.SsCoopControl;

            String sql = @" select * from kptempreceive
                            where 
                            coop_id = '" + coop_id + "' and memcoop_id = '" + coop_id + @"' and
                            member_no = '" + member_no + "' and recv_period = '" + recv_period + "' ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }

    }
}