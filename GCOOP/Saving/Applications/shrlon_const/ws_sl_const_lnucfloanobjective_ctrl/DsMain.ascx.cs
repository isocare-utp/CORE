using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon_const.ws_sl_const_lnucfloanobjective_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNUCFLOANOBJECTIVEDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet1 ds = new DataSet1();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.LNUCFLOANOBJECTIVE;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsMainItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsMainClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");

            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();
        }

        public void TypeLoan()
        {
            string sql = @"
             SELECT DISTINCT LNLOANTYPE.LOANTYPE_CODE,
                    LNLOANTYPE.LOANTYPE_CODE +' ('+ LNLOANTYPE.PREFIX +') '+LNLOANTYPE.LOANTYPE_DESC as texttype,
                    LNLOANTYPE.COOP_ID,
                    LNLOANTYPE.LOANGROUP_CODE
             FROM LNLOANTYPE
             WHERE ( LNLOANTYPE.COOP_ID = {0} ) and
                   ( ( LNLOANTYPE.USELNREQ_FLAG = 1 ) )
             union
            select ' ',' ',' ', ' ' 
                    ORDER BY LOANTYPE_CODE ASC ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_code", "texttype", "LOANTYPE_CODE");

        }
        
    }
}