using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_dptran_div_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.DPDEPTTRANDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTTRAN;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Register();
        }

        /// <summary>
        /// ดึงข้อมูลที่มีอยู่แล้วขึ้นมา
        /// </summary>
        public void RetrieveDetail(String member_no, String deptaccount_no, Decimal year)
        {
            string sql = @"SELECT member_no,deptaccount_no,tran_year,deptitem_amt,tran_status FROM DPDEPTTRAN 
                           where COOP_ID = '" + state.SsCoopControl + "' and tran_year = " + year + @"
                           and member_no = '" + member_no + "' and deptaccount_no = '" + deptaccount_no + @"'
                           and system_code = 'DIV' 
                           ORDER BY member_no ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void RetrieveDetail(String member_no, Decimal year)
        {
            string sql = @"SELECT member_no,deptaccount_no,tran_year,deptitem_amt,tran_status FROM DPDEPTTRAN 
                           where COOP_ID = '" + state.SsCoopControl + "' and tran_year = " + year + @"
                           and member_no = '" + member_no + @"' and system_code = 'DIV' 
                           ORDER BY member_no ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}