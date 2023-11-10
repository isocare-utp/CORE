using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon_const.w_sheet_sl_const_cmucftofromaccid_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.CMUCFTOFROMACCIDDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet1 ds = new DataSet1();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.CMUCFTOFROMACCID;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsDeListItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsListClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Button.Add("b_del");

            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();
        }

        public void Retrieve()
        {
            string sql = @"select cta.coop_id,   
	            cta.applgroup_code,   
	            cta.sliptype_code,   
	            cta.moneytype_code,   
	            cta.account_id,   
	            cta.default_flag,   
	            cta.defaultpayout_flag,
	            acm.account_name 
            from cmucftofromaccid cta,
	            accmaster acm
            where cta.coop_id = acm.coop_id
	            and cta.account_id = acm.account_id
	            and cta.applgroup_code = 'SLN'
            order by sliptype_code,moneytype_code,account_id ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            //ทำยังไงก็ได้ให้ได้ DataTable หรือ Xml String ที่มีข้อมูล แล้วนำมาใช้คำสั่ง ImportData
            this.ImportData(dt);
        }
    }
}