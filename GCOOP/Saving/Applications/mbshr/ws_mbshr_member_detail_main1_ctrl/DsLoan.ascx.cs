using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl
{
    public partial class DsLoan : DataSourceRepeater
    {
        public DataSet1.DT_LOANDataTable DATA { get; set; }

        public void InitDsLoan(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LOAN;
            this.EventItemChanged = "OnDsLoanItemChanged";
            this.EventClicked = "OnDsLoanClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsLoan");
            this.Button.Add("bloan_detail");
            this.Register();
        }

        public void RetrieveLoan(String ls_member_no)
        {
            String sql = @"  
                         SELECT LNCONTMASTER.LOANCONTRACT_NO,   
                                 LNCONTMASTER.LOANAPPROVE_AMT,   
                                 LNCONTMASTER.PERIOD_PAYMENT,   
                                 LNCONTMASTER.STARTCONT_DATE,   
                                 LNCONTMASTER.PRINCIPAL_BALANCE,   
                                 LNCONTMASTER.LAST_PERIODPAY,   
                                 LNCONTMASTER.PAYMENT_STATUS,   
                                 LNCONTMASTER.CONTRACT_STATUS,   
                                 LNCONTMASTER.LOANTYPE_CODE,   
                                 LNCONTMASTER.WITHDRAWABLE_AMT,   
                                 LNCONTMASTER.LASTPAYMENT_DATE,   
                                 LNCONTMASTER.PERIOD_PAYAMT,   
                                 LNCONTMASTER.CONTLAW_STATUS,   
                                 LNCONTMASTER.TRANSFER_STATUS ,
                                 LNCONTMASTER.trnlntocoll_flag 
	                   FROM LNCONTMASTER,   
                                 LNLOANTYPE  
                           WHERE ( LNLOANTYPE.LOANTYPE_CODE = LNCONTMASTER.LOANTYPE_CODE ) and  
                                 ( LNCONTMASTER.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                                 ( ( LNCONTMASTER.COOP_ID = {0} )  AND  
                                 ( LNCONTMASTER.MEMBER_NO = {1} )   AND
                                 (LNCONTMASTER.CONTRACT_STATUS >0))
                        ORDER BY LNCONTMASTER.LASTPAYMENT_DATE ASC ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

      
    }
}