
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
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FINBANKSTATEMENTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINBANKSTATEMENT;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(String ls_accountno, String ls_bank, String ls_branch)
        {
            String sql = @"  
                     SELECT  FINBANKSTATEMENT.SEQ_NO ,
                    FINBANKSTATEMENT.ACCOUNT_NO ,           
                    FINBANKSTATEMENT.BANK_CODE ,          
                    FINBANKSTATEMENT.BANKBRANCH_CODE ,          
                    FINBANKSTATEMENT.DETAIL_DESC ,           
                    FINBANKSTATEMENT.ENTRY_ID ,           
                    FINBANKSTATEMENT.ENTRY_DATE ,           
                    FINBANKSTATEMENT.OPERATE_DATE ,           
                    FINBANKSTATEMENT.REF_SEQ ,           
                    FINBANKSTATEMENT.ITEM_STATUS ,           
                    FINBANKSTATEMENT.CANCEL_ID ,           
                    FINBANKSTATEMENT.CANCEL_DATE ,           
                    FINBANKSTATEMENT.BALANCE ,           
                    FINBANKSTATEMENT.BALANCE_BEGIN ,           
                    FINBANKSTATEMENT.MACHINE_ID ,           
                    FINBANKSTATEMENT.COOP_ID ,           
                    FINBANKSTATEMENT.REFER_SLIPNO ,           
                    FINBANKSTATEMENT.ITEM_AMT ,           
                    FINBANKSTATEMENT.SIGN_OPERATE    
                    FROM FINBANKSTATEMENT      
                    WHERE ( FINBANKSTATEMENT.ACCOUNT_NO = {1} ) and          
                    ( FINBANKSTATEMENT.BANK_CODE = {2} ) and          
                    ( FINBANKSTATEMENT.BANKBRANCH_CODE = {3} ) and         
                    ( FINBANKSTATEMENT.COOP_ID = {0} )  
                    ORDER BY FINBANKSTATEMENT.SEQ_NO  asc";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_accountno, ls_bank, ls_branch);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}