using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.w_dlg_npl_retrievedetail_ctrl
{
    public partial class w_dlg_npl_retrievedetail : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                string loancontractNo = Request["loancontract_no"];
                initDaMain(loancontractNo);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }

        private void initDaMain(string loancontractNo)
        {
            string sql = @"
            select		
              b.principal_balance,
              b.lastcalint_date,
              b.period_payment,
              b.interest_arrear,
              b.loanapprove_amt,
              b.interest_accum,
              b.lastpayment_date,
              b.contract_status,
              b.intset_arrear,
              a.member_no
            from lnnplmaster a
              inner join lncontmaster b on a.coop_id = b.coop_id and a.loancontract_no = b.loancontract_no
            where
              a.coop_id = {0} and
              a.loancontract_no = {1}
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loancontractNo);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                string ls_yearbf = (state.SsWorkDate.Year - 1).ToString();
                decimal li_contract_status = dt.GetDecimal("contract_status");
                decimal ldc_dayafter = 0;
                decimal ldc_principalbf = 0;
                decimal ldc_interestbf = dt.GetDecimal("intset_arrear");
                DateTime lastpayment_date = dt.GetDate("lastpayment_date");
                ldc_dayafter = (state.SsWorkDate - lastpayment_date).Days;
                ldc_interestbf = dt.GetDecimal("intset_arrear");
                string sql2 = @"
                SELECT PRINCIPAL_BALANCE
                FROM LNCONTSTATEMENT
                WHERE
                    coop_id = {0} and
                    LOANCONTRACT_NO = {1} and
                    TO_CHAR(SLIP_DATE, 'YYYY') = {2} and
                    TO_CHAR(SLIP_DATE, 'MM') = '12'
                ORDER BY SLIP_DATE DESC
                ";
                sql2 = WebUtil.SQLFormat(sql2, state.SsCoopControl, loancontractNo, ls_yearbf);
                Sdt dt2 = WebUtil.QuerySdt(sql2);
                if (dt2.Next())
                {
                    ldc_principalbf = dt2.GetDecimal("principal_balance");
                }
                if (ldc_principalbf <= 0)
                {
                    ldc_principalbf = dt.GetDecimal("principal_balance");
                }
                if (ldc_principalbf < dt.GetDecimal("principal_balance"))
                {
                    ldc_principalbf = dt.GetDecimal("principal_balance");
                }
                dsMain.DATA[0].indict_prnamt = dt.GetDecimal("principal_balance"); // เงินต้นฟ้อง
                dsMain.DATA[0].prn_last_year = ldc_principalbf; // ต้นเงินสิ้นปี ldc_principalbalance
                dsMain.DATA[0].int_last_year = ldc_interestbf;
                dsMain.DATA[0].int_balance = ldc_interestbf;
                dsMain.DATA[0].last_payment = lastpayment_date; // ชำระล่าสุด (ไว้โชว์เฉยๆ)
                dsMain.DATA[0].dayafter = ldc_dayafter;
                dsMain.DATA[0].debtor_class = "A"; // ชั้นลูกหนี้
                if (ldc_dayafter > 35)
                {
                    dsMain.DATA[0].debtor_class = "B"; // ชั้นลูกหนี้
                }
                else if (ldc_dayafter > 90)
                {
                    dsMain.DATA[0].debtor_class = "C"; // ชั้นลูกหนี้
                }
                if (li_contract_status == 11 || li_contract_status == -11)
                {
                    dsMain.DATA[0].status = 2;
                }
                else
                {
                    dsMain.DATA[0].status = 1;
                }
                dsMain.DATA[0].receive_date = DateTime.Today;
                dsMain.DATA[0].period_payment = dt.GetDecimal("period_payment");
            }
            else
            {
                return;
            }
        }
    }
}