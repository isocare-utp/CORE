using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.dlg.w_dlg_finconfrim_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FINSLIPDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINSLIP;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveDetail(string ls_coopid, string ls_sqlext)
        {
            string sql = @" SELECT TOP 150 FINSLIP.SLIP_NO,   
                         FINSLIP.ENTRY_ID,   
                         FINSLIP.ENTRY_DATE,   
                         FINSLIP.OPERATE_DATE,   
                         FINSLIP.CASH_TYPE,   
                         FINSLIP.PAYMENT_STATUS,   
                         FINSLIP.PAYMENT_DESC,   
                         FINSLIP.ITEMPAY_AMT,   
                         FINSLIP.ITEMPAYTYPE_CODE,   
                         FINSLIP.PAY_RECV_STATUS,
                         (case when PAY_RECV_STATUS=1 then 'รับ' else 'จ่าย' end  )PAY_RECV_DESC,
                         FINSLIP.MEMBER_FLAG,   
                         FINSLIP.NONMEMBER_DETAIL,   
                         FINSLIP.COOP_ID,   
                         FINSLIP.RECEIPT_NO,   
                         FINSLIP.MEMBER_NO,    
                         FINSLIP.PAY_TOWHOM,  
                         FINSLIP.FROM_SYSTEM,   
                         FINSLIP.REMARK,   
                         FINSLIP.REF_SYSTEM,   
                         FINSLIP.REF_SLIPNO,   
                         FINSLIP.PAYSLIP_NO,   
                         FINSLIP.MEMBGROUP_CODE,
                         FINSLIP.NONMEMBER_DETAIL as fullname 
                    FROM FINSLIP
                   WHERE 
                         FINSLIP.PAYMENT_STATUS = 8   AND  
                         FINSLIP.OPERATE_DATE >= {0} AND
                         FINSLIP.COOP_ID = '" + ls_coopid + "' " + ls_sqlext + "   ORDER BY FINSLIP.SLIP_NO ASC   ";
            sql = WebUtil.SQLFormat(sql, state.SsWorkDate);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);  
            //วันที่ข้อมูลต้องไม่น้อยกว่าวันที่ระบบ
        }
    }
}