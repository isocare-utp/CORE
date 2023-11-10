using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.w_sheet_paychq_fromslip_ctrl
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
        public void Retrieve(string coop_id,DateTime entry_Date)
        {
            string sql = @" SELECT FINSLIP.ENTRY_DATE,   
                     FINSLIP.PAY_TOWHOM,   
                     FINSLIP.ITEM_AMTNET,   
                     FINSLIP.SLIP_NO,   
                     FINSLIP.COOP_ID,   
                     0 as choose_flag,   
                     FINSLIP.CHEQUE_TYPE,   
                     FINSLIP.RECEIVE_DATE,   
                     FINSLIP.RECEIVE_STATUS,   
                     FINSLIP.PAYMENT_STATUS,   
                     FINSLIP.DATEON_CHQ,   
                     FINSLIP.CHEQUEBOOK_NO,   
                     FINSLIP.CHQ_ADVFLAG,   
                     FINSLIP.ACCOUNT_NO,   
                     FINSLIP.FROM_ACCNO,   
                     FINSLIP.FROM_BANKCODE,   
                     FINSLIP.FROM_BRANCHCODE,   
                     FINSLIP.CHEQUE_STATUS,   
                     FINSLIP.RECVPAY_ID,   
                     FINSLIP.RECVPAY_TIME,   
                     FINSLIP.BANK_CODE,   
                     FINSLIP.BANK_BRANCH,   
                     FINSLIP.ENTRY_ID,   
                     FINSLIP.OPERATE_DATE,   
                     FINSLIP.FROM_SYSTEM,   
                     FINSLIP.CASH_TYPE,   
                     FINSLIP.PAYMENT_DESC,   
                     FINSLIP.ITEMPAY_AMT,   
                     FINSLIP.MEMBER_NO,   
                     FINSLIP.ITEMPAYTYPE_CODE,   
                     FINSLIP.PAY_RECV_STATUS,   
                     FINSLIP.MEMBER_FLAG,   
                     FINSLIP.NONMEMBER_DETAIL,   
                     FINSLIP.MACHINE_ID,   
                     FINSLIP.CANCEL_ID,   
                     FINSLIP.CANCEL_DATE,   
                     FINSLIP.BANKFEE_AMT,   
                     FINSLIP.BANKSRV_AMT,   
                     FINSLIP.TOFROM_ACCID,   
                     FINSLIP.REF_SLIPNO,   
                     FINSLIP.REF_SYSTEM,   
                     FINSLIP.RECEIPT_NO,   
                     FINSLIP.REMARK,   
                     FINSLIP.POSTTOVC_FLAG,   
                     FINSLIP.FORACC_FLAG,   
                     FINSLIP.LOANAPPV_AMT,   
                     FINSLIP.NORM_AMT,   
                     FINSLIP.SHARE_AMT,   
                     FINSLIP.EMER_AMT,   
                     FINSLIP.EXTRA_AMT,   
                     FINSLIP.SHARESPX_AMT,   
                     FINSLIP.TAX_FLAG,   
                     FINSLIP.TAX_AMT,   
                     FINSLIP.TAX_RATE,   
                     FINSLIP.DES_ACCID,   
                     FINSLIP.CANCEL_BYFIN,   
                     FINSLIP.VOUCHER_NO,   
                     FINSLIP.RETAIL_FLAG,   
                     FINSLIP.TAX_CODE,   
                     FINSLIP.ACCUINT_AMT  
                FROM FINSLIP
                WHERE ( FINSLIP.PAYMENT_STATUS = 8 ) AND  
                ( FINSLIP.CASH_TYPE = 'CHQ' ) AND  
                ( FINSLIP.ENTRY_DATE = {1} ) AND  
                ( FINSLIP.PAY_RECV_STATUS = 0 ) AND
                ( FINSLIP.COOP_ID = {0} ) order by  SLIP_NO ";
            sql = WebUtil.SQLFormat(sql, coop_id, entry_Date);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}