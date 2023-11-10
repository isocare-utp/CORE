using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_bankaccount_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FINBANKACCOUNTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINBANKACCOUNT;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
        }
        public void RetrieveMain(string account_no, string bank_code, string bankbranch_code)
        {
            String sql = @"  
                     SELECT  FINBANKACCOUNT.COOP_ID ,  
                    FINBANKACCOUNT.ACCOUNT_NO ,  
                    FINBANKACCOUNT.BANK_CODE ,  
                    FINBANKACCOUNT.BANKBRANCH_CODE,   
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
        public void DDBank()
        {
            string sql = @"
                     SELECT BANK_CODE,BANK_DESC ,1 as sorter  FROM CMUCFBANK";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["BANK_CODE"].ToString().Trim() + " - " + row["BANK_DESC"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,BANK_CODE asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "BANK_CODE", "display", "BANK_CODE");
        }
        public void DDBankbranch(string bank)
        {
            string sql = @"
                        SELECT BANK_CODE,BRANCH_NAME,BRANCH_ID,1 as sorter FROM CMUCFBANKBRANCH  WHERE BANK_CODE ={0}";
            sql = WebUtil.SQLFormat(sql, bank);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["BRANCH_ID"].ToString().Trim() + " - " + row["BRANCH_NAME"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,BRANCH_ID asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "BANKBRANCH_CODE", "display", "BRANCH_ID");
        }
        public void DDAccountid()
        {
            string sql = @"
                        SELECT ACCOUNT_ID as value_code,ACCOUNT_NAME as value_desc,1 as sort
                        FROM ACCMASTER 
                        WHERE ( ACCOUNT_TYPE_ID = 3 ) and (ACTIVE_STATUS = 1 ) and  ( COOP_ID = {0} ) ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.DefaultView.Sort = "sort,value_code asc";
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "ACCOUNT_ID", "display", "value_code");
        }
    }
}