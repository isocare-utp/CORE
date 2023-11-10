using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_am_replication_detail_ctrl
{
    public partial class DsSpare : DataSourceFormView
    {
        public DataSet1.SELECTMAXDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet1 ds = new DataSet1();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.SELECTMAX;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsSpareItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsSpareClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl, argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, FormView1, this.DATA, "dsSpare");

            //เรียกให้คลาสแม่ทำงานอีกครั้ง
            this.Register();
        }

        public void Retrieve()
        {
            string sql = @"
            SELECT
              (SELECT MAX(DEPTSLIP_NO) FROM DPDEPTSLIP) AS DEPT_SLIP,
              (SELECT COUNT(*) FROM DPDEPTSLIP) AS DEPT_COUNT,
  
              (SELECT MAX(payinslip_no) FROM slslippayin) AS SL_SLIP,
              (SELECT COUNT(*) FROM slslippayin) AS SL_COUNT,
  
              (SELECT MAX(SLIP_NO) FROM FINSLIP) AS FIN_SLIP,
              (SELECT COUNT(*) FROM FINSLIP) AS FIN_COUNT
            FROM DUAL";
            DataTable dt = WebUtil.Query(sql);
            //ทำยังไงก็ได้ให้ได้ DataTable หรือ Xml String ที่มีข้อมูล แล้วนำมาใช้คำสั่ง ImportData
            this.ImportData(dt);
        }
    }
}