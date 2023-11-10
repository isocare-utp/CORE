using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl.w_dlg_loan_receive_detail_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNCONTMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            //this.Button.Add("b_search");
            //this.Button.Add("b_cancel");
            this.Register();
        }
        public void RetrieveMain(String loancontract_no)
        {
            String sql = @"    SELECT LNCONTMASTER.COOP_ID,   
                                     LNCONTMASTER.LOANCONTRACT_NO,   
                                     LNCONTMASTER.MEMBER_NO,   
                                     LNCONTMASTER.LOANTYPE_CODE,   
                                     LNCONTMASTER.LOANPAYMENT_TYPE, 
                           case when LNCONTMASTER.LOANPAYMENT_TYPE = 1 then 'คงต้น' 
		                        when LNCONTMASTER.LOANPAYMENT_TYPE = 2 then 'คงยอด'
                              end as PAYMENT_TYPE, 
                           case when LNLOANTYPE.CONTRACTINT_TYPE = 0 then 'ตามประกาศ' 
		                        when LNLOANTYPE.CONTRACTINT_TYPE = 1 then 'คงที่มีระยะเวลา'
                                when LNLOANTYPE.CONTRACTINT_TYPE = 2 then 'คงที่ตลอด อัตรา'
                              end as CONTRACTINT_TYPE,
                                     LNLOANTYPE.CONTRACTINT_TIME, 
                                     LNLOANTYPE.CONTRACTINT_RATE,
                                     LNCONTMASTER.LOANAPPROVE_AMT,   
                                     LNCONTMASTER.WITHDRAWABLE_AMT,   
                                     LNCONTMASTER.PRINCIPAL_BALANCE,   
                                     LNCONTMASTER.PERIOD_PAYMENT,   
                                     LNCONTMASTER.PERIOD_PAYAMT,   
                                     LNCONTMASTER.STARTCONT_DATE,   
                                     LNCONTMASTER.PAYMENT_STATUS, 
                           case when LNCONTMASTER.PAYMENT_STATUS = 1 then 'เก็บต้นปกติ' 
		                        when LNCONTMASTER.PAYMENT_STATUS = -11 then 'งดส่งต้นเงิน'
                              end as PAY_STATUS  ,
                                     LNCONTMASTER.LAST_PERIODRCV,   
                                     LNCONTMASTER.LOANOBJECTIVE_CODE,   
                                     LNCONTMASTER.APPROVE_ID,
                                     MBUCFPRENAME.PRENAME_SHORT,   
                                     MBMEMBMASTER.MEMB_NAME,   
                                     MBMEMBMASTER.MEMB_SURNAME, 
                                     MBMEMBMASTER.EXPENSE_CODE,
                                     MBMEMBMASTER.EXPENSE_BANK ,   
                                     LNLOANTYPE.LOANTYPE_DESC,   
                                     MBMEMBMASTER.MEMBGROUP_CODE,   
                                     MBUCFMEMBGROUP.MEMBGROUP_DESC,
								     CMUCFBANK.BANK_DESC,
                                     CMUCFBANKBRANCH.BRANCH_NAME,
                                     LNCONTMASTER.EXPENSE_ACCID,
                                     LNUCFLOANOBJECTIVE.LOANOBJECTIVE_DESC
                                FROM LNCONTMASTER,   
                                     LNLOANTYPE,   
                                     MBMEMBMASTER,   
                                     MBUCFMEMBGROUP,   
                                     MBUCFPRENAME,
								 CMUCFBANK,
                                     CMUCFBANKBRANCH,
                                     LNUCFLOANOBJECTIVE
                               WHERE ( MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE ) and  
                                     ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
                                     ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
                                     ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE (+) )   and
								    ( MBMEMBMASTER.EXPENSE_BANK= CMUCFBANK.BANK_CODE (+) ) AND
                                     (MBMEMBMASTER.EXPENSE_BRANCH = CMUCFBANKBRANCH.BRANCH_ID (+) ) AND
                                     ( LNCONTMASTER.LOANOBJECTIVE_CODE = LNUCFLOANOBJECTIVE.LOANOBJECTIVE_CODE (+) ) AND
                                     (( LNCONTMASTER.COOP_ID ={0} ) AND  
                                     ( LNCONTMASTER.LOANCONTRACT_NO ={1} ) ) 
";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, loancontract_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}