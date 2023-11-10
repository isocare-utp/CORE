using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_conteck_lite_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.DPDEPTSTATEMENTDataTable DATA { get; private set; }

        public void InitDsDetail(PageWeb pw)
        {
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTSTATEMENT;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.EventItemChanged = "OnDsDetailItemChange";
            this.EventClicked = "OnDsDetailClick";
            this.Register();
        }

        public int Retrieve(string coop_id, string account_no)
        {
            string sql = @"
                    SELECT        DPDEPTSTATEMENT.DEPTACCOUNT_NO, DPDEPTSTATEMENT.SEQ_NO, DPDEPTSTATEMENT.DEPTITEMTYPE_CODE, DPDEPTSTATEMENT.OPERATE_DATE, 
                                             DPDEPTSTATEMENT.PRNCBAL, DPDEPTSTATEMENT.CHECKBOOK_CODE_PB, DPDEPTSTATEMENT.ENTRY_ID, DPDEPTSTATEMENT.ITEM_STATUS, 
                                             DPUCFDEPTITEMTYPE.SIGN_FLAG, DPUCFDEPTITEMTYPE.PRINT_CODE, DPDEPTSTATEMENT.DEPTITEM_AMT, DPDEPTSTATEMENT.PRNC_NO, 
                                             DPDEPTSTATEMENT.TAX_AMT, DPDEPTSTATEMENT.RETINT_AMT, DPDEPTSTATEMENT.ENTRY_DATE, DPDEPTSTATEMENT.CLOSEDAY_STATUS, 
                                             DPDEPTSTATEMENT.ACCUINT_AMT, DPDEPTSTATEMENT.CALINT_FROM, DPDEPTSTATEMENT.CALINT_TO, DPDEPTSTATEMENT.INT_AMT, 
                                             DPDEPTSTATEMENT.PRNTOCARD_STATUS, DPDEPTSTATEMENT.PRNTOPB_STATUS, DPDEPTSTATEMENT.CHRG_AMT, 
                                             DPDEPTSTATEMENT.BANK_BRANCH_CODE, DPDEPTSTATEMENT.BANK_CODE, DPDEPTSTATEMENT.CHECK_STATUS, DPDEPTSTATEMENT.CHECKDUE_DATE, 
                                             DPDEPTSTATEMENT.CHECK_NO, '        ' AS operate_tdate, DPDEPTSTATEMENT.NO_BOOK_FLAG, DPUCFDEPTITEMTYPE.PRINT_CODENOBOOK, 
                                             DPDEPTSTATEMENT.COOP_ID, DPDEPTSTATEMENT.DEPTSLIP_NO
                    FROM            DPDEPTSTATEMENT, DPUCFDEPTITEMTYPE
                    WHERE        DPDEPTSTATEMENT.DEPTITEMTYPE_CODE = DPUCFDEPTITEMTYPE.DEPTITEMTYPE_CODE AND (DPDEPTSTATEMENT.DEPTACCOUNT_NO = '" + account_no + @"')
                                              AND (DPDEPTSTATEMENT.FORPRNBK_FLAG = 1) AND (DPDEPTSTATEMENT.COOP_ID = '" + coop_id + @"')
                    ORDER BY DPDEPTSTATEMENT.SEQ_NO DESC";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            return dt.Rows.Count;
        }
    }
}