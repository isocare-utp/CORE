using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.dlg.wd_fin_bankaccount_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FINBANKACCOUNTDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINBANKACCOUNT;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveData(string ls_coopid,string sql_text)
        {
            string sql = @"SELECT 
                FINBANKACCOUNT.ACCOUNT_NO,   
                FINBANKACCOUNT.ACCOUNT_NAME ,
                FINBANKACCOUNT.BANK_CODE ,
                CMUCFBANK.BANK_DESC, 
                FINBANKACCOUNT.BANKBRANCH_CODE,
                CMUCFBANKBRANCH.BRANCH_NAME,
                CLOSE_STATUS,
                (CASE WHEN FINBANKACCOUNT.CLOSE_STATUS=0 THEN 'เปิดบัญชี' ELSE 'ปิดบัญชี' END) AS CLOSE_DESC,
                 ACCOUNT_TYPE,
                (CASE WHEN  ISNULL(ACCOUNT_TYPE,'00') ='00' THEN 'ออมทรัพย์' 
                WHEN  ISNULL(ACCOUNT_TYPE,'00') ='01' THEN 'กระแส' END )ACCOUNT_DESC
                FROM FINBANKACCOUNT  
                INNER JOIN CMUCFBANK ON FINBANKACCOUNT.BANK_CODE = CMUCFBANK.BANK_CODE 
                INNER JOIN CMUCFBANKBRANCH ON  FINBANKACCOUNT.BANKBRANCH_CODE = CMUCFBANKBRANCH.BRANCH_ID AND  CMUCFBANK.BANK_CODE  = CMUCFBANKBRANCH.BANK_CODE
                WHERE FINBANKACCOUNT.COOP_ID = {0} " + sql_text+@"
                ORDER BY FINBANKACCOUNT.CLOSE_STATUS,FINBANKACCOUNT.BANK_CODE,FINBANKACCOUNT.BANKBRANCH_CODE,FINBANKACCOUNT.ACCOUNT_NO ASC ";
            sql = WebUtil.SQLFormat(sql, ls_coopid);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);           
        }
    }
}