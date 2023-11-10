using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.admin_const.ws_am_cmcoop_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.CMCOOPCONSTANTDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet1 ds = new DataSet1();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.CMCOOPCONSTANT;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsDetailItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsDetailClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");

            //กรณีมีปุ่มให้ใช้คำสั่งนี้ add เพื่อให้เกิด event clicked ที่ปุ่ม
           // this.Button.Add("b_del");//ถ้าหน้าจอไม่มีปุ่มให้ลบหรือคอมเมนท์บรรทัดนี้

            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();

        }

        public void RetrieveDsmain()
        {
            string sql = "select * from cmcoopconstant";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdProvince()
        {
            string sql = @"select province_code,province_desc, 1 as sorter from mbucfprovince 
                           union
                           select '','',0 from dual order by sorter";
            DataTable dt = WebUtil.Query(sql);
            //Province_Code
            this.DropDownDataBind(dt,"province_Code","province_desc","province_code");
        }
        public void DdDistric(string province_code)
        {
            string sql = @"select distric_code,distric_desc, 1 as sorter from mbucfdistric from province_code={0}
                           union
                           select '','',0 from dual order by sorter";
            sql = WebUtil.SQLFormat(sql, province_code);
            DataTable dt = WebUtil.Query(sql);
            //Distric_Code
            this.DropDownDataBind(dt, "distric_Code", "distric_desc", "distric_code");
        }
        public void DdTambol(string distric_code)
        {
            string sql = @"select tambol_code,tambol_desc, 1 as sorter from mbucftambol from distric_code={0}
                           union
                           select '','',0 from dual order by sorter";
            sql = WebUtil.SQLFormat(sql, distric_code);
            DataTable dt = WebUtil.Query(sql);
            //tambol_Code
            this.DropDownDataBind(dt, "tambol_Code", "tambol_desc", "tambol_code");
        }
    }
}