using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsData : DataSourceFormView
    {
        public DataSet1.DT_DATADataTable DATA { get; set; }

        public void InitDsData(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DATA;
            this.EventItemChanged = "OnDsDataItemChanged";
            this.EventClicked = "OnDsDataClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsData");
            this.Register();
        }
        public void RetrieveData(String ls_cont_no)
        {
            String sql = @"select lncontmaster.loancontract_no,   
                             lncontmaster.member_no,   
                             lncontmaster.loantype_code,   
                             lncontmaster.loanrequest_docno,   
                             lncontmaster.loanpayment_type,   
                             lncontmaster.loanapprove_amt,   
                             lncontmaster.withdrawable_amt,   
                             lncontmaster.period_payamt,   
                             lncontmaster.period_payment,   
                             lncontmaster.period_payment_max,   
                             lncontmaster.startcont_date,   
                             lncontmaster.principal_balance,   
                             lncontmaster.last_periodpay,   
                             lncontmaster.lastcalint_date,   
                             lncontmaster.lastprocess_date,   
                             lncontmaster.principal_arrear,   
                             lncontmaster.interest_arrear,   
                             lncontmaster.interest_accum,   
                             lncontmaster.contract_status,   
                             lncontmaster.loanobjective_code,   
                             lncontmaster.rkeep_principal,   
                             lncontmaster.rkeep_interest,   
                             lncontmaster.interest_return,   
                             lncontmaster.prncbalbegin_amt,   
                             lncontmaster.trnfrom_contno,   
                             lncontmaster.trnfrom_memno,   
                             lncontmaster.payment_status,   
                             lncontmaster.last_periodrcv,   
                             lncontmaster.compound_status,   
                             lncontmaster.expirecont_date,   
                             lncontmaster.principal_trans,   
                             lncontmaster.misspay_amt,   
                             lncontmaster.intmonth_arrear,   
                             lncontmaster.lastreceive_date,   
                             lncontmaster.lastpayment_date,   
                             lncontmaster.startkeep_period,   
                             lncontmaster.contlaw_status,   
                             lncontmaster.compound_period,   
                             lncontmaster.compounddue_date,   
                             lncontmaster.intyear_arrear,   
                             lncontmaster.od_flag,   
                             lncontmaster.expense_accid,
                             lncontmaster.compound_payment,   
                             lncontmaster.compound_paystatus,   
                             lncontmaster.compound_paytype,   
                             lnucfcontlaw.contlaw_desc,
lnucfloanobjective.loanobjective_code+' '+lnucfloanobjective.loanobjective_desc as loanobjective_desc  
                        from lncontmaster 
                            join lnucfcontlaw on lncontmaster.contlaw_status = lnucfcontlaw.contlaw_status
                            left outer join lnucfloanobjective on lncontmaster.loanobjective_code = lnucfloanobjective.loanobjective_code and lncontmaster.loantype_code = lnucfloanobjective.loantype_code
                       where ( lncontmaster.loancontract_no = {0} )    
                    ";

            sql = WebUtil.SQLFormat(sql, ls_cont_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdLoanType()
        {
            string sql = @" 
                  SELECT LOANOBJECTIVE_CODE,   
                         LOANOBJECTIVE_DESC ,
                         LOANOBJECTIVE_CODE||' '||LOANOBJECTIVE_DESC as display,1 as sorter
                    FROM LNUCFLOANOBJECTIVE 
                    union
                    select '','','',0 from dual order by sorter,LOANOBJECTIVE_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loanobjective_code", "display", "LOANOBJECTIVE_CODE");

        }
    }
}