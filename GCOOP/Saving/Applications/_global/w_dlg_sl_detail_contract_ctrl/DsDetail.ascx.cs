using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet2.LNCONTMASTERDataTable DATA { get; set; }


        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet2 ds = new DataSet2();
            this.DATA = ds.LNCONTMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
           
            this.Register();
        }

        public void RetrieveMembNo(String member_no, string loancontract_no)
        {
            string sql = @"
         SELECT  
LNCONTMASTER.LOANCONTRACT_NO ,           
LNCONTMASTER.MEMBER_NO ,           
LNCONTMASTER.LOANTYPE_CODE ,           
LNCONTMASTER.LOANREQUEST_DOCNO ,           
LNCONTMASTER.LOANPAYMENT_TYPE ,           
LNCONTMASTER.LOANAPPROVE_AMT ,           
LNCONTMASTER.WITHDRAWABLE_AMT ,           
LNCONTMASTER.PERIOD_PAYAMT ,           
LNCONTMASTER.PERIOD_PAYMENT ,           
LNCONTMASTER.PERIOD_PAYMENT_MAX ,           
LNCONTMASTER.STARTCONT_DATE ,           
LNCONTMASTER.PRINCIPAL_BALANCE ,           
LNCONTMASTER.LAST_PERIODPAY ,           
LNCONTMASTER.LASTCALINT_DATE ,           
LNCONTMASTER.LASTPROCESS_DATE ,           
LNCONTMASTER.PRINCIPAL_ARREAR ,           
LNCONTMASTER.INTEREST_ARREAR ,           
LNCONTMASTER.INTEREST_ACCUM ,                    
LNCONTMASTER.CONTRACT_STATUS ,           
LNCONTMASTER.LOANOBJECTIVE_CODE ,           
LNCONTMASTER.RKEEP_PRINCIPAL ,           
LNCONTMASTER.RKEEP_INTEREST ,           
LNCONTMASTER.INTEREST_RETURN ,           
LNCONTMASTER.PRNCBALBEGIN_AMT ,           
LNCONTMASTER.TRNFROM_CONTNO ,           
LNCONTMASTER.TRNFROM_MEMNO ,           
LNCONTMASTER.PAYMENT_STATUS ,           
LNCONTMASTER.LAST_PERIODRCV ,           
LNCONTMASTER.COMPOUND_STATUS ,           
LNCONTMASTER.EXPIRECONT_DATE ,                     
LNCONTMASTER.PRINCIPAL_TRANS ,           
LNCONTMASTER.MISSPAY_AMT ,           
LNCONTMASTER.INTMONTH_ARREAR ,           
LNCONTMASTER.LASTRECEIVE_DATE ,           
LNCONTMASTER.LASTPAYMENT_DATE ,           
LNCONTMASTER.STARTKEEP_PERIOD ,           
LNCONTMASTER.CONTRACT_TYPE ,                     
LNCONTMASTER.COMPOUND_PERIOD ,           
LNCONTMASTER.COMPOUNDDUE_DATE ,           
LNCONTMASTER.INTYEAR_ARREAR ,           
LNCONTMASTER.OD_FLAG ,           
LNCONTMASTER.COMPOUND_PAYMENT ,           
LNCONTMASTER.COMPOUND_PAYSTATUS ,           
LNCONTMASTER.COMPOUND_PAYTYPE, 
LNUCFCONTRACTTYPE.CONTRACT_TYPE,   
         LNUCFCONTRACTTYPE.CONTRACTTYPE_DESC,   
         LNUCFCONTRACTTYPE.PROBLEM_FLAG,   
         LNUCFCONTRACTTYPE.SETINTARR_FLAG,   
         LNUCFCONTRACTTYPE.SETPAYADVANCE_FLAG,   
         LNUCFCONTRACTTYPE.DESCRIPTION_SHORT,
        LNUCFLOANOBJECTIVE.LOANOBJECTIVE_CODE,   
         LNUCFLOANOBJECTIVE.LOANOBJECTIVE_DESC
  
FROM LNCONTMASTER left join LNUCFCONTRACTTYPE on LNCONTMASTER.CONTRACT_TYPE = LNUCFCONTRACTTYPE.CONTRACT_TYPE
      left join LNUCFLOANOBJECTIVE on LNCONTMASTER.LOANOBJECTIVE_CODE =  LNUCFLOANOBJECTIVE.LOANOBJECTIVE_CODE
WHERE 
( trim(lncontmaster.loancontract_no) = '" + loancontract_no.Trim()+@"' ) and
            ( (  LNCONTMASTER.MEMBER_NO = '" + member_no + "' ) )  ";


            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }

        protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }
    }
}
