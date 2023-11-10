using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_interestpay_estimate_detail_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDETDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYINDET;     
            this.InitDataSource(pw, Repeater2, this.DATA, "dsDetail");
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.Button.Add("b_detail");
            this.Register();
            
            //this.Button.Add("b_contsearch");

        }

        public void RetrieveDetail(String payinslip_no)
        {
            String sql = @"SELECT SLSLIPPAYINDET.COOP_ID,   
         SLSLIPPAYINDET.PAYINSLIP_NO,   
         SLSLIPPAYINDET.SLIPITEMTYPE_CODE,   
         SLSLIPPAYINDET.SEQ_NO,   
         SLSLIPPAYINDET.OPERATE_FLAG,   
         SLSLIPPAYINDET.SHRLONTYPE_CODE,   
         SLSLIPPAYINDET.CONCOOP_ID,   
         SLSLIPPAYINDET.LOANCONTRACT_NO,   
         SLSLIPPAYINDET.SLIPITEM_DESC,   
         SLSLIPPAYINDET.PERIODCOUNT_FLAG,   
         SLSLIPPAYINDET.PERIOD,   
         SLSLIPPAYINDET.PRINCIPAL_PAYAMT,   
         SLSLIPPAYINDET.INTEREST_PAYAMT,   
         SLSLIPPAYINDET.INTARREAR_PAYAMT,   
         SLSLIPPAYINDET.ITEM_PAYAMT,   
         SLSLIPPAYINDET.ITEM_BALANCE,   
         SLSLIPPAYINDET.PRNCALINT_AMT,   
         SLSLIPPAYINDET.CALINT_FROM,   
         SLSLIPPAYINDET.CALINT_TO,   
         SLSLIPPAYINDET.INTEREST_PERIOD,   
         SLSLIPPAYINDET.INTEREST_RETURN,   
         SLSLIPPAYINDET.STM_ITEMTYPE,   
         SLSLIPPAYINDET.BFPERIOD,   
         SLSLIPPAYINDET.BFINTARR_AMT,   
         SLSLIPPAYINDET.BFINTARRSET_AMT,   
         SLSLIPPAYINDET.BFLASTCALINT_DATE,   
         SLSLIPPAYINDET.BFLASTPROC_DATE,   
         SLSLIPPAYINDET.BFLASTPAY_DATE,   
         SLSLIPPAYINDET.BFWITHDRAW_AMT,   
         SLSLIPPAYINDET.BFPERIOD_PAYMENT,   
         SLSLIPPAYINDET.BFSHRCONT_BALAMT,   
         SLSLIPPAYINDET.BFSHRCONT_STATUS,   
         SLSLIPPAYINDET.BFCONTLAW_STATUS,   
         SLSLIPPAYINDET.BFCOUNTPAY_FLAG,   
         SLSLIPPAYINDET.BFPAYSPEC_METHOD,   
         SLSLIPPAYINDET.BFCONTSTATUS_DESC,   
         SLSLIPPAYINDET.RKEEP_PRINCIPAL,   
         SLSLIPPAYINDET.RKEEP_INTEREST,   
         SLSLIPPAYINDET.NKEEP_INTEREST,   
         SLSLIPPAYINDET.BFINTRETURN_FLAG,   
         SLSLIPPAYINDET.BFPXAFTERMTHKEEP_TYPE  
    FROM SLSLIPPAYINDET  
   WHERE (SLSLIPPAYINDET.COOP_ID = {0}) and
         ( slslippayindet.payinslip_no = {1} )    
 ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, payinslip_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}