using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsChgpay : DataSourceRepeater
    {
        public DataSet1.DT_CHGPAYDataTable DATA { get; set; }

        public void InitDsChgpay(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_CHGPAY;
            this.EventItemChanged = "OnDsChgpayItemChanged";
            this.EventClicked = "OnDsChgpayClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsChgpay");
            this.Register();
        }
        public void RetrieveChgpay(String ls_contno)
        {
            String sql = @"  
                      SELECT LNREQCONTADJUST.CONTADJUST_DOCNO,   
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
                             ( ( lnreqcontadjust.loancontract_no = {0} ) AND  
                             ( lnreqcontadjustdet.contadjust_code = 'PAY' ) )      ";

            sql = WebUtil.SQLFormat(sql, ls_contno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}