using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_reprintchq_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(string sqlsearch,DateTime sdate,DateTime edate)
        {
            String sql = @" 
                        SELECT FINCHQEUESTATEMENT.CHEQUE_NO,   
                         FINCHQEUESTATEMENT.CHEQUEBOOK_NO,   
                         FINCHQEUESTATEMENT.DATE_ONCHQ,   
                         CMUCFBANKBRANCH.BRANCH_NAME,   
                         CMUCFBANK.BANK_DESC,   
                         FINCHQEUESTATEMENT.ENTRY_ID,   
                         FINCHQEUESTATEMENT.ENTRY_DATE,   
                         FINCHQEUESTATEMENT.MONEY_AMT,   
                         FINCHQEUESTATEMENT.MEMBER_NO,   
                         FINCHQEUESTATEMENT.CANCEL_DATE,   
                         FINCHQEUESTATEMENT.CANCEL_ID,   
                         0 as ai_flag,   
                         FINCHQEUESTATEMENT.CHQEUE_STATUS,   
                         FINCHQEUESTATEMENT.SEQ_NO,   
                         FINCHQEUESTATEMENT.BANK_CODE,   
                         FINCHQEUESTATEMENT.BANK_BRANCH,   
                         FINCHQEUESTATEMENT.TO_WHOM,   
                         FINCHQEUESTATEMENT.TYPECHQ_PAY,   
                         FINCHQEUESTATEMENT.ADVANCE_CHQ,   
                         FINCHQEUESTATEMENT.COOP_ID,   
                         FINCHQEUESTATEMENT.MACHINE_ID,   
                         FINCHQEUESTATEMENT.FROM_BANKACCNO,   
                         FINCHQEUESTATEMENT.REFER_SLIPNO,   
                         FINCHQEUESTATEMENT.PRINTED_STATUS,   
                         FINCHQEUESTATEMENT.PRINTED_ID,   
                         FINCHQEUESTATEMENT.PRINTED_DATE,   
                         FINCHQEUESTATEMENT.CHEQUE_TYPE,   
                         FINCHQEUESTATEMENT.PRINTED_TERMINAL,   
                         FINCHQEUESTATEMENT.USE_STATUS,   
                         FINCHQEUESTATEMENT.REMARK,   
                         FINCHQEUESTATEMENT.CANCEL_RESON,   
                         1 as action_flag  
                    FROM FINCHQEUESTATEMENT,   
                         CMUCFBANK,   
                         CMUCFBANKBRANCH  
                   WHERE ( FINCHQEUESTATEMENT.BANK_CODE = CMUCFBANKBRANCH.BANK_CODE ) and  
                         ( FINCHQEUESTATEMENT.BANK_CODE = CMUCFBANK.BANK_CODE ) and  
                         ( FINCHQEUESTATEMENT.BANK_BRANCH = CMUCFBANKBRANCH.BRANCH_ID ) and  
                         ( ( FINCHQEUESTATEMENT.CHQEUE_STATUS not in (-9,-1) ) AND 
                         ( FINCHQEUESTATEMENT.COOP_ID = {0} ) " + sqlsearch + @" and
                         ( FINCHQEUESTATEMENT.DATE_ONCHQ between {1} and {2}) and
                         ( FINCHQEUESTATEMENT.USE_STATUS = 1 ) ) ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, sdate, edate);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}