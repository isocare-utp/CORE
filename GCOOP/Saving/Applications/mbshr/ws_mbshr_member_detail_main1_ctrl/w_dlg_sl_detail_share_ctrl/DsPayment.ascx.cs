using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_sl_detail_share_ctrl
{
    public partial class DsPayment : DataSourceRepeater
    {
        public DataSet1.DT_PAYMENTDataTable DATA { get; set; }

        public void InitDsPayment(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_PAYMENT;
            this.EventItemChanged = "OnDsPaymentItemChanged";
            this.EventClicked = "OnDsPaymentClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPayment");
            this.Register();
        }
        public void RetrievePayment(String ls_member_no)
        {
            String sql = @"  
                      SELECT SHPAYMENTADJUST.PAYADJUST_DOCNO,   
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
                       WHERE ( SHPAYMENTADJUST.MEMBER_NO = {0} ) AND  
                             ( SHPAYMENTADJUST.SHRPAYADJ_STATUS = 1 )     ";

            sql = WebUtil.SQLFormat(sql, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}