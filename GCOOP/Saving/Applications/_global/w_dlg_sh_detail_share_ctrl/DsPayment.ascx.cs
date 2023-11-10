using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_sh_detail_share_ctrl
{
    public partial class DsPayment : DataSourceRepeater
    {
        public DataSet3.SHPAYMENTADJUSTDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet3 ds = new DataSet3();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.SHPAYMENTADJUST;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsListItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsListClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPayment");

            //กรณีมีปุ่มให้ใช้คำสั่งนี้ add เพื่อให้เกิด event clicked ที่ปุ่ม


            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();
        }
        public void Retrieve(string member_no)
        {
            string sql = @"  SELECT SHPAYMENTADJUST.PAYADJUST_DOCNO,   
         SHPAYMENTADJUST.MEMBER_NO,   
         SHPAYMENTADJUST.PAYADJUST_DATE,   
         SHPAYMENTADJUST.SHAREBEGIN_VALUE,   
         SHPAYMENTADJUST.SHARESTK_VALUE,   
         SHPAYMENTADJUST.SHRLAST_PERIOD,   
         SHPAYMENTADJUST.PERIODBASE_VALUE,   
         SHPAYMENTADJUST.OLD_PERIODVALUE,   
         SHPAYMENTADJUST.OLD_PAYSTATUS,   
         SHPAYMENTADJUST.NEW_PERIODVALUE,   
         SHPAYMENTADJUST.NEW_PAYSTATUS,   
         SHPAYMENTADJUST.SHRPAYADJ_STATUS,   
         SHPAYMENTADJUST.APVIMMEDIATE_FLAG,   
         SHPAYMENTADJUST.REMARK,   
         SHPAYMENTADJUST.CHGSTOP_FLAG,   
         SHPAYMENTADJUST.CHGCONT_FLAG,   
         SHPAYMENTADJUST.CHGADD_FLAG,   
         SHPAYMENTADJUST.CHGLOW_FLAG,   
         SHPAYMENTADJUST.ENTRY_ID,   
         SHPAYMENTADJUST.ENTRY_DATE,   
         SHPAYMENTADJUST.APPROVE_ID,   
         SHPAYMENTADJUST.APPROVE_DATE  
    FROM SHPAYMENTADJUST  
   WHERE ( SHPAYMENTADJUST.MEMBER_NO = '"+member_no+@"' ) AND  
         ( SHPAYMENTADJUST.SHRPAYADJ_STATUS = 1 )   ";



            DataTable dt = WebUtil.Query(sql);
            //ทำยังไงก็ได้ให้ได้ DataTable หรือ Xml String ที่มีข้อมูล แล้วนำมาใช้คำสั่ง ImportData
            this.ImportData(dt);


        }
    }
}