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
    public partial class DsListSlip : DataSourceRepeater
    {
        public DataSet1.KPTEMPRECEIVEDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.KPTEMPRECEIVE;
            this.EventItemChanged = "OnDsListSlipItemChanged";
            this.EventClicked = "OnDsListSlipClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsListSlip");
            this.Register();
        }

        /// <summary>
        /// ดึงรายละเอียด slip
        /// </summary>
        /// <param name="member_no"></param>
        public void Retrieve(String member_no)
        {
            string sql = @" SELECT  *  FROM KPTEMPRECEIVE 
                            where coop_id ='" + state.SsCoopControl + "' and memcoop_id = '" + state.SsCoopControl + @"'
                            and member_no='" + member_no + "' order by recv_period desc ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}