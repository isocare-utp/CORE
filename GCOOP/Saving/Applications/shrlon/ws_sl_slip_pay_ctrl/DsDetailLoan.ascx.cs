using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_slip_pay_ctrl
{
    public partial class DsDetailLoan : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDETDataTable DATA { get; set; }
        public void InitDsDetailLoan(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYINDET;
            this.EventItemChanged = "OnDsDetailLoanItemChanged";
            this.EventClicked = "OnDsDetailLoanClicked";
            this.InitDataSource(pw, Repeater2, this.DATA, "dsDetailLoan");
             this.Button.Add("bloan_detail");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
        public void RetrieveDetailLoan(string payinslip_no)
        {
            String sql = @"  SELECT SLSLIPPAYIN.COOP_ID,   
                             SLSLIPPAYIN.PAYINSLIP_NO,   
                             SLSLIPPAYINDET.OPERATE_FLAG,   
                             SLSLIPPAYINDET.LOANCONTRACT_NO,   
                             SLSLIPPAYINDET.PERIODCOUNT_FLAG,   
                             SLSLIPPAYINDET.PERIOD,   
                             SLSLIPPAYINDET.BFSHRCONT_BALAMT,   
                             SLSLIPPAYINDET.BFLASTCALINT_DATE,   
                             SLSLIPPAYINDET.INTEREST_PERIOD,   
                             SLSLIPPAYINDET.BFINTARR_AMT,   
                             SLSLIPPAYINDET.PRINCIPAL_PAYAMT,   
                             SLSLIPPAYINDET.INTEREST_PAYAMT,   
                             SLSLIPPAYINDET.ITEM_PAYAMT,   
                             SLSLIPPAYINDET.ITEM_BALANCE,   
                             SLSLIPPAYIN.MEMBER_NO,
                             SLSLIPPAYINDET.BFPERIOD_PAYMENT  
                        FROM SLSLIPPAYIN,   
                             SLSLIPPAYINDET  
                       WHERE ( SLSLIPPAYINDET.COOP_ID = SLSLIPPAYIN.COOP_ID ) and  
                             ( SLSLIPPAYINDET.PAYINSLIP_NO = SLSLIPPAYIN.PAYINSLIP_NO ) and  
                             (SLSLIPPAYINDET.SLIPITEMTYPE_CODE = 'LON') AND
                             ( ( SLSLIPPAYIN.COOP_ID = {0} ) AND  
                             ( SLSLIPPAYIN.PAYINSLIP_NO = {1} ) )    
                    ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, payinslip_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}