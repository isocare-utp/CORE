using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.deposit_const.w_sheet_dp_const_dpucfaccholdtype_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPUCFACCTYPEDataTable  DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            
            DataSet1 ds = new DataSet1();
            
            this.DATA = ds.DPUCFACCTYPE;
            
            this.EventItemChanged = "OnDsListItemChanged";
            
            this.EventClicked = "OnDsListClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            //กรณีมีปุ่มให้ใช้คำสั่งนี้ add เพื่อให้เกิด event clicked ที่ปุ่ม
            this.Button.Add("b_del");

            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();
        }

        public void Retrieve()
        {
            string sql = "SELECT * FROM DPUCFACCTYPE where COOP_ID = '" + state.SsCoopControl + "' ORDER BY ACCCONT_TYPE ";
            DataTable dt = WebUtil.Query(sql);
            //ทำยังไงก็ได้ให้ได้ DataTable หรือ Xml String ที่มีข้อมูล แล้วนำมาใช้คำสั่ง ImportData
            this.ImportData(dt);
        }
    }
}