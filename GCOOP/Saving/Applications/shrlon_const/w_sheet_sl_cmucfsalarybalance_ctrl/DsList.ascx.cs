using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Saving.Applications.shrlon_const.w_sheet_sl_cmucfsalarybalance_ctrl;

namespace Saving.Applications.shrlon.w_sheet_sl_cmucfsalarybalance_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.CMUCFSALARYBALANCEDataTable DATA { get; private set; }

        public void InitDsList(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet1 ds = new DataSet1();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.CMUCFSALARYBALANCE;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsDeListItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsListClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            //กรณีมีปุ่มให้ใช้คำสั่งนี้ add เพื่อให้เกิด event clicked ที่ปุ่ม
            this.Button.Add("b_del");//ถ้าหน้าจอไม่มีปุ่มให้ลบหรือคอมเมนท์บรรทัดนี้

            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();
        }

        public void Retrieve()
        {

            string sql = @"SELECT CMUCFSALARYBALANCE.SALARYBAL_CODE,   
            CMUCFSALARYBALANCE.SALARYBAL_DESC,   
            CMUCFSALARYBALANCE.SALARYBAL_AMT,   
            CMUCFSALARYBALANCE.SALARYBAL_PERCENT,   
            CMUCFSALARYBALANCE.INCREMENT_AMT,
            CMUCFSALARYBALANCE.CHKDEPT_FLAG              
            FROM CMUCFSALARYBALANCE";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}