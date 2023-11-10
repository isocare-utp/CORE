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
    public partial class DsStatement : DataSourceRepeater
    {
        public DataSet2.StatementDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet2 ds = new DataSet2();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.Statement;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsListItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsListClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, Repeater1, this.DATA, "dsStatement");

            //กรณีมีปุ่มให้ใช้คำสั่งนี้ add เพื่อให้เกิด event clicked ที่ปุ่ม


            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();
        }
        public void Retrieve(string member_no,string sharetype)
        {
            string sql = @"   SELECT SHSHARESTATEMENT.MEMBER_NO,   
         SHSHARESTATEMENT.SHARETYPE_CODE,   
         SHSHARESTATEMENT.SEQ_NO,   
         SHSHARESTATEMENT.OPERATE_DATE,   
         SHSHARESTATEMENT.REF_DOCNO,   
         SHSHARESTATEMENT.SHRITEMTYPE_CODE,   
         SHSHARESTATEMENT.PERIOD,   
         SHSHARESTATEMENT.SHARE_AMOUNT,   
         SHSHARESTATEMENT.SHARESTK_AMT,   
         SHSHARETYPE.UNITSHARE_VALUE,   
         SHUCFSHRITEMTYPE.SIGN_FLAG,   
         SHSHARESTATEMENT.SHRARREAR_AMT,   
         SHSHARESTATEMENT.SLIP_DATE,   
         SHSHARESTATEMENT.REMARK  
    FROM SHSHARESTATEMENT,   
         SHSHARETYPE,   
         SHUCFSHRITEMTYPE  
   WHERE ( SHSHARESTATEMENT.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
         ( SHSHARESTATEMENT.SHRITEMTYPE_CODE = SHUCFSHRITEMTYPE.SHRITEMTYPE_CODE ) and  
         ( SHSHARESTATEMENT.COOP_ID = SHSHARETYPE.COOP_ID ) and  
         ( ( shsharestatement.member_no = '"+member_no+@"' ) AND  
         ( shsharestatement.sharetype_code = '"+sharetype+"' ) )  ";



            DataTable dt = WebUtil.Query(sql);
            //ทำยังไงก็ได้ให้ได้ DataTable หรือ Xml String ที่มีข้อมูล แล้วนำมาใช้คำสั่ง ImportData
            this.ImportData(dt);


        }
    }
}