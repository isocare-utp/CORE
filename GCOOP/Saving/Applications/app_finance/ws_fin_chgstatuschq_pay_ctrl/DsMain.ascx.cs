using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_chgstatuschq_pay_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Register();
        }
        public void RetrieveMain()
        {
            String sql = @"  
                        SELECT FINCHQEUESTATEMENT.CHEQUE_NO,   
                         FINCHQEUESTATEMENT.CHEQUEBOOK_NO,   
                         FINCHQEUESTATEMENT.DATE_ONCHQ,   
                         FINCHQEUESTATEMENT.SEQ_NO,   
                         FINCHQEUESTATEMENT.BANK_CODE,   
                         FINCHQEUESTATEMENT.ENTRY_ID,   
                         FINCHQEUESTATEMENT.ENTRY_DATE,   
                         FINCHQEUESTATEMENT.BANK_BRANCH,   
                         FINCHQEUESTATEMENT.TO_WHOM,   
                         FINCHQEUESTATEMENT.TYPECHQ_PAY,   
                         FINCHQEUESTATEMENT.MONEY_AMT,   
                         FINCHQEUESTATEMENT.CHQEUE_STATUS,   
                         FINCHQEUESTATEMENT.ADVANCE_CHQ,   
                         FINCHQEUESTATEMENT.COOP_ID,   
                         FINCHQEUESTATEMENT.MACHINE_ID,   
                         FINCHQEUESTATEMENT.CANCEL_ID,   
                         FINCHQEUESTATEMENT.CANCEL_DATE,   
                         FINCHQEUESTATEMENT.MEMBER_NO,   
                         FINCHQEUESTATEMENT.FROM_BANKACCNO,   
                         CMUCFBANK.BANK_DESC,   
                         CMUCFBANKBRANCH.BRANCH_NAME,   
                         FINCHQEUESTATEMENT.REFER_SLIPNO,   
                         '        ' as date_tonchq,   
                         FINCHQEUESTATEMENT.PRINTED_STATUS,   
                         FINCHQEUESTATEMENT.PRINTED_ID,   
                         FINCHQEUESTATEMENT.PRINTED_DATE,   
                         FINCHQEUESTATEMENT.PRINTED_TERMINAL,   
                         FINCHQEUESTATEMENT.CHEQUE_TYPE,   
                         FINCHQEUESTATEMENT.USE_STATUS,   
                         0 as status    ,
					     FINBANKACCOUNT.ACCOUNT_ID
                    FROM FINCHQEUESTATEMENT,   
                         CMUCFBANK,   
                         CMUCFBANKBRANCH  ,
					  finslip , FINBANKACCOUNT
                   WHERE ( FINCHQEUESTATEMENT.BANK_CODE = CMUCFBANK.BANK_CODE ) and  
                         ( FINCHQEUESTATEMENT.BANK_CODE = CMUCFBANKBRANCH.BANK_CODE ) and  
                         ( FINCHQEUESTATEMENT.BANK_BRANCH = CMUCFBANKBRANCH.BRANCH_ID ) and 
                         ( FINCHQEUESTATEMENT.FROM_BANKACCNO = FINBANKACCOUNT.ACCOUNT_NO ) and  
						 (  FINBANKACCOUNT.BANK_CODE = FINCHQEUESTATEMENT.BANK_CODE) AND
                         (  FINBANKACCOUNT.BANKBRANCH_CODE = CMUCFBANKBRANCH.BRANCH_ID ) AND
                         ( (  FINCHQEUESTATEMENT.CHQEUE_STATUS in ( 0 , 8)  ) AND  
                         (  FINCHQEUESTATEMENT.COOP_ID = {0} ) AND  
 					 (ltrim(rtrim(FINCHQEUESTATEMENT.refer_slipno)) = ltrim(rtrim(finslip.slip_no)) ) and 
						finslip.pay_recv_status = 0 and
                         (  FINCHQEUESTATEMENT.USE_STATUS = 1 ) )  
                        order by FINCHQEUESTATEMENT.DATE_ONCHQ,FINCHQEUESTATEMENT.BANK_CODE,
                        FINCHQEUESTATEMENT.BANK_BRANCH,FINCHQEUESTATEMENT.CHEQUE_NO";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}