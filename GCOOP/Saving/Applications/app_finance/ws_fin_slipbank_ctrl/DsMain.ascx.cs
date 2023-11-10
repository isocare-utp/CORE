using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_slipbank_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FINSLIPBANKDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINSLIPBANK;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }
        public void RetrieveData(string account_no, string bank_code, string bankbranch_code)
        {
            String sql = @"  
                     SELECT  FINBANKACCOUNT.COOP_ID ,  
                    FINBANKACCOUNT.ACCOUNT_NO ,  
                    FINBANKACCOUNT.BANK_CODE ,  
                    FINBANKACCOUNT.BANKBRANCH_CODE AS BANK_BRANCH,   
                    FINBANKACCOUNT.ACCOUNT_NAME ,   
                    FINBANKACCOUNT.BEGINBAL ,     
                    FINBANKACCOUNT.BALANCE ,      
                    FINBANKACCOUNT.CLOSE_STATUS ,     
                    FINBANKACCOUNT.CLOSE_DATE ,      
                    FINBANKACCOUNT.OPEN_DATE ,     
                    FINBANKACCOUNT.LASTSTM_SEQ ,
                    FINBANKACCOUNT.ENTRY_ID ,    
                    FINBANKACCOUNT.ENTRY_DATE ,    
                    FINBANKACCOUNT.ACCOUNT_TYPE , 
                    FINBANKACCOUNT.DEPT_AMT ,    
                    FINBANKACCOUNT.WITH_AMT ,       
                    FINBANKACCOUNT.BOOK_LASTUPDATE ,      
                    FINBANKACCOUNT.BOOK_NO ,     
                    FINBANKACCOUNT.LASTACCESS_DATE , 
                    FINBANKACCOUNT.SCO_BALANCE , 
                    FINBANKACCOUNT.ACCOUNT_ID ,    
                    (FINBANKACCOUNT.INT_RATE * 100 ) AS INT_RATE,      
                    FINBANKACCOUNT.REMARK       
                    FROM FINBANKACCOUNT    
                    WHERE 
                    ( FINBANKACCOUNT.ACCOUNT_NO = {0} ) AND 
                    ( FINBANKACCOUNT.BANK_CODE = {1} ) AND 
                    ( FINBANKACCOUNT.BANKBRANCH_CODE = {2} )";

            sql = WebUtil.SQLFormat(sql, account_no, bank_code, bankbranch_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void RetrieveFromAcc(string account_no)
        {
            String sql = @"  
                     SELECT  FINBANKACCOUNT.COOP_ID ,  
                    FINBANKACCOUNT.ACCOUNT_NO ,  
                    FINBANKACCOUNT.BANK_CODE ,  
                    FINBANKACCOUNT.BANKBRANCH_CODE AS BANK_BRANCH,   
                    FINBANKACCOUNT.ACCOUNT_NAME ,   
                    FINBANKACCOUNT.BEGINBAL ,     
                    FINBANKACCOUNT.BALANCE ,      
                    FINBANKACCOUNT.CLOSE_STATUS ,     
                    FINBANKACCOUNT.CLOSE_DATE ,      
                    FINBANKACCOUNT.OPEN_DATE ,     
                    FINBANKACCOUNT.LASTSTM_SEQ ,
                    FINBANKACCOUNT.ENTRY_ID ,    
                    FINBANKACCOUNT.ENTRY_DATE ,    
                    FINBANKACCOUNT.ACCOUNT_TYPE , 
                    FINBANKACCOUNT.DEPT_AMT ,    
                    FINBANKACCOUNT.WITH_AMT ,       
                    FINBANKACCOUNT.BOOK_LASTUPDATE ,      
                    FINBANKACCOUNT.BOOK_NO ,     
                    FINBANKACCOUNT.LASTACCESS_DATE , 
                    FINBANKACCOUNT.SCO_BALANCE , 
                    FINBANKACCOUNT.ACCOUNT_ID ,    
                    (FINBANKACCOUNT.INT_RATE * 100 ) AS INT_RATE,      
                    FINBANKACCOUNT.REMARK       
                    FROM FINBANKACCOUNT    
                    WHERE 
                    ( FINBANKACCOUNT.ACCOUNT_NO = {0} ) ";

            sql = WebUtil.SQLFormat(sql, account_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DDBank()
        {
            string sql = @"
                     SELECT BANK_CODE,(BANK_CODE||' '||BANK_DESC) as BANK_DESC,1 as sorter  FROM CMUCFBANK
                    union
                    select '','',0 from dual order by sorter,BANK_CODE asc";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "BANK_DESC", "BANK_CODE");
        }
        public void DDBankbranch(string bank)
        {
            string sql = @"
                        SELECT BANK_CODE,(BRANCH_ID||'  '||BRANCH_NAME) as BRANCH_NAME,   BRANCH_ID,1 as sorter FROM CMUCFBANKBRANCH  WHERE BANK_CODE ={0}
                        union
                        select '','','',0 from dual order by sorter, BRANCH_ID asc";
            sql = WebUtil.SQLFormat(sql, bank);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_branch", "BRANCH_NAME", "BRANCH_ID");
        }
    }
}