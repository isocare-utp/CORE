using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_dptran_div_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPDEPTTRANDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTTRAN;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        /// <summary>
        /// ดึงข้อมูลที่มีอยู่แล้วขึ้นมา
        /// </summary>
        public void RetrieveListDIV(decimal year)
        {
            string sql = @"SELECT member_no,deptaccount_no FROM DPDEPTTRAN 
                           where COOP_ID = '" + state.SsCoopControl + "' and tran_year = " + year + @"
                           and system_code = 'DIV' 
                           ORDER BY member_no ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void RetrieveListDIV(decimal year, String member_no)
        {
            string sql = @"SELECT member_no,deptaccount_no FROM DPDEPTTRAN 
                           where COOP_ID = '" + state.SsCoopControl + "' and tran_year = " + year + @"
                           and system_code = 'DIV' and member_no = '" + member_no + @"' 
                           ORDER BY member_no,deptaccount_no ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}