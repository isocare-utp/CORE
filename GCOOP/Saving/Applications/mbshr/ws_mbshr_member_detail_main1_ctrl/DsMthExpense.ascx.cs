using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl
{
    public partial class DsMthExpense : DataSourceRepeater
    {
        public DataSet1.DT_MTHEXPENSEDataTable DATA { get; set; }

        public void InitDsMthExpense(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MTHEXPENSE;
            this.EventItemChanged = "OnDsMthExpenseItemChanged";
            this.EventClicked = "OnDsMthExpenseClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMthExpense");
            this.Register();
        }

        public void RetrieveMthExpense(String ls_member_no)
        {
            String sql = @"  
                  SELECT MBMEMBMTHEXPENSE.COOP_ID,   
                         MBMEMBMTHEXPENSE.MEMBER_NO,   
                         MBMEMBMTHEXPENSE.SIGN_FLAG,   
                         MBMEMBMTHEXPENSE.SEQ_NO,   
                         MBMEMBMTHEXPENSE.MTHEXPENSE_DESC,   
                         MBMEMBMTHEXPENSE.MTHEXPENSE_AMT  
                    FROM MBMEMBMTHEXPENSE  
                   WHERE
                         ( MBMEMBMTHEXPENSE.COOP_ID = {0} )  AND   
                         ( MBMEMBMTHEXPENSE.MEMBER_NO = {1} )  ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public TextBox TContract
        {
            get { return this.cp_sum_expense; }
        }
    }
}