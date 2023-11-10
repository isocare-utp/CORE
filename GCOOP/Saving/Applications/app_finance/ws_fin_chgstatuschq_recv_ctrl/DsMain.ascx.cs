using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_chgstatuschq_recv_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.DataMainDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataMain;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Register();
        }
        public void RetrieveMain()
        {
            String sql = @"  
                SELECT FINCHECKRETRIEVE.BANK_CODE AS BANK_CODE,  FINCHECKRETRIEVE.BANKBRANCH_CODE  AS BANKBRANCH_CODE,
                CMUCFBANK.BANK_DESC AS BANK_DESC,CMUCFBANKBRANCH.BRANCH_NAME AS BRANCH_NAME,
                FINCHECKRETRIEVE.REFERDOC_NAME AS REFERDOC_NAME,
                FINCHECKRETRIEVE.CHECKDUE_DATE AS CHECKDUE_DATE,
                FINCHECKRETRIEVE.CHECKCLEAR_STATUS,FINCHECKRETRIEVE.ENTRY_DATE, FINCHECKRETRIEVE.ENTRY_ID,
                FINCHECKRETRIEVE.CHEQUE_AMT AS CHEQUE_AMT,
                FINCHECKRETRIEVE.REFER_DOCNO AS REFER_DOCNO,
			    FINBANKACCOUNT.ACCOUNT_ID
                FROM  FINCHECKRETRIEVE 
                INNER JOIN FINBANKACCOUNT ON FINBANKACCOUNT.ACCOUNT_NO = FINCHECKRETRIEVE.TOBANK_ACCNO AND FINBANKACCOUNT.BANK_CODE = FINCHECKRETRIEVE.BANK_CODE AND
                FINBANKACCOUNT.BANKBRANCH_CODE = FINCHECKRETRIEVE.BANKBRANCH_CODE 
                INNER JOIN CMUCFBANK ON FINCHECKRETRIEVE.BANK_CODE = CMUCFBANK.BANK_CODE
                INNER JOIN CMUCFBANKBRANCH ON  FINCHECKRETRIEVE.BANK_CODE =  CMUCFBANKBRANCH.BANK_CODE  AND FINCHECKRETRIEVE.BANKBRANCH_CODE = CMUCFBANKBRANCH.BRANCH_ID
                WHERE 
                FINCHECKRETRIEVE.COOP_ID = {0} AND 
                FINCHECKRETRIEVE.CHECKCLEAR_STATUS in(0,8) ORDER BY FINCHECKRETRIEVE.CHECKDUE_DATE,
                FINCHECKRETRIEVE.BANK_CODE,FINCHECKRETRIEVE.BANKBRANCH_CODE 
                ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}