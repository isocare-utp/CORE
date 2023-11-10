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
    public partial class DsList : DataSourceFormView
    {
        public DataSet1.DataListDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataList;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(string bank_code,string bank_branch,string slip_no)
        {
            String sql = @"  
                SELECT FINCHECKRETRIEVE.BANK_CODE AS BANK_CODE,  FINCHECKRETRIEVE.BANKBRANCH_CODE  AS BANKBRANCH_CODE,
                CMUCFBANK.BANK_DESC AS BANK_DESC,CMUCFBANKBRANCH.BRANCH_NAME AS BRANCH_NAME,
                FINCHECKRETRIEVE.REFERDOC_NAME AS REFERDOC_NAME,
                FINCHECKRETRIEVE.CHECKDUE_DATE AS CHECKDUE_DATE,
                FINCHECKRETRIEVE.CHECKCLEAR_STATUS,FINCHECKRETRIEVE.ENTRY_DATE, FINCHECKRETRIEVE.ENTRY_ID,
                FINCHECKRETRIEVE.CHEQUE_AMT AS CHEQUE_AMT
                FROM  FINCHECKRETRIEVE INNER JOIN  CMUCFBANK ON FINCHECKRETRIEVE.BANK_CODE = CMUCFBANK.BANK_CODE
                INNER JOIN CMUCFBANKBRANCH ON  FINCHECKRETRIEVE.BANK_CODE =  CMUCFBANKBRANCH.BANK_CODE
                AND FINCHECKRETRIEVE.BANKBRANCH_CODE = CMUCFBANKBRANCH.BRANCH_ID
                WHERE 
                FINCHECKRETRIEVE.COOP_ID = {0} AND 
                FINCHECKRETRIEVE.CHECKCLEAR_STATUS <>-9 AND
                FINCHECKRETRIEVE.BANK_CODE = {1} AND 
                FINCHECKRETRIEVE.BANKBRANCH_CODE = {2} AND
                FINCHECKRETRIEVE.REFER_DOCNO = {3}
                ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, bank_code, bank_branch, slip_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}