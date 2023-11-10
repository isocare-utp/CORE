using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.DT_DETAILDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DETAIL;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Register();
        }

        public void RetrieveDetail(String ls_dept_no)
        {
            String sql = @"  
                       SELECT DPDEPTSTATEMENT.DEPTACCOUNT_NO,   
                        DPDEPTSTATEMENT.SEQ_NO,   
                        DPDEPTSTATEMENT.DEPTITEMTYPE_CODE,   
                        DPDEPTSTATEMENT.OPERATE_DATE,   
                        DPDEPTSTATEMENT.PRNCBAL,      
                        DPDEPTSTATEMENT.ENTRY_ID,   
                        DPDEPTSTATEMENT.ITEM_STATUS,   
                        DPUCFDEPTITEMTYPE.SIGN_FLAG,   
                        DPUCFDEPTITEMTYPE.PRINT_CODE,   
                        DPDEPTSTATEMENT.PRNC_NO,   
                        DPDEPTSTATEMENT.TAX_AMT,   
                        DPDEPTSTATEMENT.RETINT_AMT,   
                        DPDEPTSTATEMENT.ENTRY_DATE,    
                        DPDEPTSTATEMENT.ACCUINT_AMT,   
                        DPDEPTSTATEMENT.CALINT_FROM,   
                        DPDEPTSTATEMENT.CALINT_TO,   
                        DPDEPTSTATEMENT.INT_AMT,      
                        DPDEPTSTATEMENT.PRNTOPB_STATUS,   
                        DPDEPTSTATEMENT.CHRG_AMT,   
                        DPDEPTSTATEMENT.BANK_BRANCH_CODE,   
                        DPDEPTSTATEMENT.BANK_CODE,   
                        DPDEPTSTATEMENT.CHECK_STATUS,   
                        DPDEPTSTATEMENT.CHECKDUE_DATE,   
                        DPDEPTSTATEMENT.CHECK_NO,   
                        '         ' as operate_tdate,   
                        DPDEPTSTATEMENT.REF_SEQ_NO,    
                        DPDEPTSTATEMENT.NO_BOOK_FLAG,   
                        DPDEPTSTATEMENT.COOP_ID,   
                        '         ' as entry_tdate,   
                        '         ' as oper_tdate,   
                        DPDEPTSTATEMENT.OPERATE_DATE as OPER_DATE,   
                        DPDEPTSTATEMENT.DEPTSLIP_NO,   
                        DPDEPTSTATEMENT.DEPTITEM_AMT  ,
                        (case when DPUCFDEPTITEMTYPE.SIGN_FLAG =1 then DPDEPTSTATEMENT.DEPTITEM_AMT else 0  end )  as cp_deposit ,
                        (case when DPUCFDEPTITEMTYPE.SIGN_FLAG =-1 then DPDEPTSTATEMENT.DEPTITEM_AMT else 0  end )  as cp_withdraw
                FROM DPDEPTSTATEMENT,   
                        DPUCFDEPTITEMTYPE  
                WHERE ( DPDEPTSTATEMENT.DEPTITEMTYPE_CODE = DPUCFDEPTITEMTYPE.DEPTITEMTYPE_CODE ) and  
                        ( ( dpdeptstatement.deptaccount_no = {1}  ) ) AND  
                        DPDEPTSTATEMENT.COOP_ID =  {0}
            order by DPDEPTSTATEMENT.SEQ_NO";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_dept_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}