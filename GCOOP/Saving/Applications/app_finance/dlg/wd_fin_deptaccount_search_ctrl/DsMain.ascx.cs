using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.dlg.wd_fin_deptaccount_search_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.FINBANKACCOUNTDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINBANKACCOUNT;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.EventClicked = "OnDsMainClicked";
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        }
        public void RetrieveDeptno(String bank, String bank_branch)
        {
            string sql = @"
                    SELECT 
                    FINBANKACCOUNT.ACCOUNT_NO,   
                    FINBANKACCOUNT.ACCOUNT_NAME  
                    FROM FINBANKACCOUNT  
                    WHERE ( FINBANKACCOUNT.BANK_CODE = {1} ) AND  
                    ( FINBANKACCOUNT.BANKBRANCH_CODE = {2} ) and
                    ( FINBANKACCOUNT.COOP_ID = {0} ) and
                    ( FINBANKACCOUNT.CLOSE_STATUS = 0 )  
                    ORDER BY FINBANKACCOUNT.ACCOUNT_NO ASC ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, bank, bank_branch);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}