﻿using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsStatement : DataSourceRepeater
    {
        public DataSet3.StalementDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet3 ds = new DataSet3();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.Stalement;
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
        public void Retrieve( string loancontract_no)
        {
            string sql = @"  SELECT  LNCONTSTATEMENT.LOANCONTRACT_NO ,          
 LNCONTSTATEMENT.SEQ_NO ,          
 LNCONTSTATEMENT.LOANITEMTYPE_CODE ,          
 LNCONTSTATEMENT.OPERATE_DATE ,           
LNCONTSTATEMENT.REF_DOCNO ,         
  LNCONTSTATEMENT.PERIOD ,         
  LNCONTSTATEMENT.PRINCIPAL_PAYMENT ,         
  LNCONTSTATEMENT.INTEREST_PAYMENT ,         
  LNCONTSTATEMENT.PRINCIPAL_BALANCE ,          
 LNCONTSTATEMENT.CALINT_FROM ,         
  LNCONTSTATEMENT.CALINT_TO ,           
LNCONTSTATEMENT.INTEREST_PERIOD ,        
   LNCONTSTATEMENT.INTEREST_ARREAR ,         
  LNCONTSTATEMENT.INTEREST_RETURN ,        
   LNCONTSTATEMENT.ENTRY_ID ,         
  LNCONTSTATEMENT.ENTRY_DATE ,     
LNCONTSTATEMENT.MONEYTYPE_CODE ,        
   LNCONTSTATEMENT.SLIP_DATE ,          
 LNUCFLOANITEMTYPE.SIGN_FLAG ,         
  LNCONTSTATEMENT.REMARK    
 FROM LNCONTSTATEMENT ,           
LNUCFLOANITEMTYPE    
 WHERE ( LNCONTSTATEMENT.LOANITEMTYPE_CODE = LNUCFLOANITEMTYPE.LOANITEMTYPE_CODE ) and       
   ( LNCONTSTATEMENT.COOP_ID = LNUCFLOANITEMTYPE.COOP_ID ) and    
      ( ( trim(lncontstatement.loancontract_no) = '" + loancontract_no.Trim() + @"' ) ) ";



            DataTable dt = WebUtil.Query(sql);
            //ทำยังไงก็ได้ให้ได้ DataTable หรือ Xml String ที่มีข้อมูล แล้วนำมาใช้คำสั่ง ImportData
            this.ImportData(dt);


        }
    }
}