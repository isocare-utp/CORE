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
    public partial class DsList : DataSourceFormView
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(string as_chqno,string as_chqbkno,string as_bank,string as_branch ,decimal an_seqno)
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
                             FINCHQEUESTATEMENT.PRINTED_STATUS,   
                             FINCHQEUESTATEMENT.PRINTED_ID,   
                             FINCHQEUESTATEMENT.PRINTED_DATE,   
                             FINCHQEUESTATEMENT.PRINTED_TERMINAL,   
                             FINCHQEUESTATEMENT.CHEQUE_TYPE,   
                             FINCHQEUESTATEMENT.USE_STATUS  
                        FROM FINCHQEUESTATEMENT,   
                             CMUCFBANK,   
                             CMUCFBANKBRANCH  
                       WHERE ( FINCHQEUESTATEMENT.BANK_CODE = CMUCFBANK.BANK_CODE ) and  
                             ( FINCHQEUESTATEMENT.BANK_CODE = CMUCFBANKBRANCH.BANK_CODE ) and  
                             ( FINCHQEUESTATEMENT.BANK_BRANCH = CMUCFBANKBRANCH.BRANCH_ID ) and  
                             ( ( FINCHQEUESTATEMENT.CHEQUE_NO = {1} ) AND  
                             ( FINCHQEUESTATEMENT.CHEQUEBOOK_NO = {2} ) AND  
                             ( FINCHQEUESTATEMENT.BANK_CODE = {3} ) AND  
                             ( FINCHQEUESTATEMENT.BANK_BRANCH = {4} ) AND  
                             ( FINCHQEUESTATEMENT.SEQ_NO = {5} ) ) AND  
                             FINCHQEUESTATEMENT.COOP_ID = {0}    ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl,as_chqno, as_chqbkno, as_bank, as_branch , an_seqno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}