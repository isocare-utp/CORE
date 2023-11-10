using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsChgpay : DataSourceRepeater
    {
        public DataSet4.CollateralDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet4 ds = new DataSet4();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.Collateral;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsListItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsListClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, Repeater1, this.DATA, "dsChgpay");

            //กรณีมีปุ่มให้ใช้คำสั่งนี้ add เพื่อให้เกิด event clicked ที่ปุ่ม


            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();
        }
        public void Retrieve(string loancontract_no)
        {
            string sql = @"   SELECT LNREQCONTADJUST.CONTADJUST_DOCNO,   
         LNREQCONTADJUST.CONTADJUST_DATE,   
         LNREQCONTADJUST.ENTRY_ID,   
         LNREQCONTADJUSTDET.LOANPAYMENT_TYPE,   
         LNREQCONTADJUSTDET.PERIOD_PAYMENT,   
         LNREQCONTADJUSTDET.PAYMENT_STATUS,   
         LNREQCONTADJUSTDET.OLDPAYMENT_TYPE,   
         LNREQCONTADJUSTDET.OLDPERIOD_PAYMENT,   
         LNREQCONTADJUSTDET.OLDPAYMENT_STATUS  
    FROM LNREQCONTADJUST,   
         LNREQCONTADJUSTDET  
   WHERE ( LNREQCONTADJUSTDET.CONTADJUST_DOCNO = LNREQCONTADJUST.CONTADJUST_DOCNO ) and  
         ( LNREQCONTADJUST.COOP_ID = LNREQCONTADJUSTDET.COOP_ID ) and  
         ( ( trim(lnreqcontadjust.loancontract_no) = '"+loancontract_no.Trim()+@"' ) AND  
         ( lnreqcontadjustdet.contadjust_code = 'PAY' ) )   ";



            DataTable dt = WebUtil.Query(sql);
            //ทำยังไงก็ได้ให้ได้ DataTable หรือ Xml String ที่มีข้อมูล แล้วนำมาใช้คำสั่ง ImportData
            this.ImportData(dt);


        }
    }
}