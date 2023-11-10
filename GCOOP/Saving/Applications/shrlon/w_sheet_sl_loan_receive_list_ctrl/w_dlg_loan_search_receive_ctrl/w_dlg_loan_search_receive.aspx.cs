using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl.w_dlg_loan_search_receive_ctrl
{
    public partial class w_dlg_loan_search_receive : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostMembgroup { get; set; }
        [JsPostBack]
        public string PostLoanType { get; set; }
        [JsPostBack]
        public string PostSearch { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.TypeLoan();
                dsMain.memgroup();

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMembgroup)
            {

                string memgroup_code = dsMain.DATA[0].MEMBGROUP_DESC;
                dsMain.DATA[0].MEMBGROUP_CODE = memgroup_code;
            }
            else if (eventArg == PostLoanType)
            {
                string loandtype_code = dsMain.DATA[0].LOANTYPE_DESC;
                dsMain.DATA[0].LOANTYPE_CODE = loandtype_code;
            }
            else if (eventArg == PostSearch)
            {
                Search_Click();
            }
        }

        public void WebDialogLoadEnd()
        {

        }

        protected void Search_Click()
        {
            try
            {
                String loancontract_no = "", member_no = "";
                String memb_name = "", memb_surname = "";
                String membgroup_code = "", loantype_code = "";
                String approve_date_s = "", approve_date_e = "";
                String approve_id = "";

                string sqlext_con = "";
                string sqlext_req = "";


                loancontract_no = dsMain.DATA[0].LOANCONTRACT_NO.Trim();
                member_no = dsMain.DATA[0].MEMBER_NO;
                
                memb_name = dsMain.DATA[0].MEMB_NAME;
                memb_surname = dsMain.DATA[0].MEMB_SURNAME;
                membgroup_code = dsMain.DATA[0].MEMBGROUP_CODE;
                loantype_code = dsMain.DATA[0].LOANTYPE_CODE;
                approve_date_s = dsMain.DATA[0].APPROVE_DATE_S;
                approve_date_e = dsMain.DATA[0].APPROVE_DATE_E;
                approve_id = dsMain.DATA[0].APPROVE_ID;
               // string memb_no = String.Format("{0:00000000}", Convert.ToDecimal(member_no));


                if (loancontract_no.Length > 0)
                {
                    sqlext_con = " and (  LNCONTMASTER.LOANCONTRACT_NO like '%" + loancontract_no + "%') ";
                    sqlext_req = " and (  LNREQLOAN.LOANCONTRACT_NO like '%" + loancontract_no + "%') ";
                }
                if (member_no.Length > 0)
                {
                    sqlext_con += " and ( LNCONTMASTER.MEMBER_NO like '%" + member_no + "%') ";
                    sqlext_req += " and ( LNREQLOAN.MEMBER_NO like '%" + member_no + "%') ";
                }
                if (memb_name.Length > 0)
                {
                    sqlext_con += " and ( MBMEMBMASTER.MEMB_NAME like '%" + memb_name + "%') ";
                    sqlext_req += " and ( MBMEMBMASTER.MEMB_NAME like '%" + memb_name + "%') ";
                }
                if (memb_surname.Length > 0)
                {
                    sqlext_con += " and ( MBMEMBMASTER.MEMB_SURNAME = '" + memb_surname + "' )";
                    sqlext_req += " and ( MBMEMBMASTER.MEMB_SURNAME = '" + memb_surname + "' )";
                }
                if (membgroup_code.Length > 0)
                {
                    sqlext_con += " and ( MBMEMBMASTER.MEMBGROUP_CODE = '" + membgroup_code + "' )";
                    sqlext_req += " and ( MBMEMBMASTER.MEMBGROUP_CODE = '" + membgroup_code + "' )";
                }
                if (loantype_code.Length > 0)
                {
                    sqlext_con += " and ( LNLOANTYPE.LOANTYPE_CODE  = '" + loantype_code + "' )";
                    sqlext_req += " and ( LNLOANTYPE.LOANTYPE_CODE  = '" + loantype_code + "' )";
                }
                string date_s = "dd/mm/yyyy";
                if (approve_date_s.Length > 0 && approve_date_e.Length > 0)
                {
                    sqlext_con += " and ( LNCONTMASTER.LOANAPPROVE_DATE between to_date('" + approve_date_s + "','" + date_s + "') and  to_date('" + approve_date_e + "','" + date_s + "' ))";
                    sqlext_req += " and ( LNREQLOAN.APPROVE_DATE between to_date('" + approve_date_s + "','" + date_s + "') and  to_date('" + approve_date_e + "','" + date_s + "' ) )";
                }

                if (approve_id.Length > 0)
                {
                    sqlext_con += " and ( LNCONTMASTER.APPROVE_ID = '" + approve_id + "' )";
                    sqlext_req += " and ( LNREQLOAN.APPROVE_ID = '" + approve_id + "' )";
                }

                this.SetOnLoadedScript("parent.GetItemLoan(\"" + sqlext_con + "\", \"" + sqlext_req + "\");");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}