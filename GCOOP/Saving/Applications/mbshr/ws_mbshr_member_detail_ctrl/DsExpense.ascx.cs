using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl
{
    public partial class DsExpense : DataSourceFormView
    {
        public DataSet1.DT_EXPENSEDataTable DATA { get; set; }

        public void InitDsExpense(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_EXPENSE;
            this.EventItemChanged = "OnDsExpenseItemChanged";
            this.EventClicked = "OnDsExpenseClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsExpense");
            this.Register();
        }

        public void RetrieveExpense(String ls_member_no)
        {
            String sql = @"  
                  SELECT MBMEMBMASTER.EXPENSE_CODE,   
                         MBMEMBMASTER.EXPENSE_BANK,   
                         MBMEMBMASTER.EXPENSE_BRANCH,   
                         SUBSTRING(MBMEMBMASTER.EXPENSE_ACCID,0,3)+'-'+SUBSTRING(MBMEMBMASTER.EXPENSE_ACCID,4,1)+'-'+SUBSTRING(MBMEMBMASTER.EXPENSE_ACCID,5,5)+'-'+SUBSTRING(MBMEMBMASTER.EXPENSE_ACCID,10,1) as EXPENSE_ACCID,   
                         CMUCFBANK.BANK_DESC,   
                         CMUCFBANKBRANCH.BRANCH_NAME  
                    FROM MBMEMBMASTER 
						LEFT JOIN CMUCFBANK on  mbmembmaster.expense_bank = cmucfbank.bank_code 
                          LEFT JOIN CMUCFBANKBRANCH on mbmembmaster.expense_branch = cmucfbankbranch.branch_id and mbmembmaster.expense_bank = cmucfbankbranch.bank_code
                    WHERE
                          coop_id = {0}
                         and  MEMBER_NO = {1} ";   

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

            //string expense_accid = this.DATA[0].EXPENSE_ACCID.Trim();
            //if (expense_accid != "")
            //{
            //    expense_accid = expense_accid.Substring(0, 2) + "-" + expense_accid.Substring(2, 9) + "-" + expense_accid.Substring(11, 1);
            //    this.DATA[0].EXPENSE_ACCID = expense_accid;
            //}
        }
    }
}