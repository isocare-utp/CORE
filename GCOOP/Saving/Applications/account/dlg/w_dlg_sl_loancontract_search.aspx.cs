using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_sl_loancontract_search : PageWebDialog, WebDialog
    {
        protected String LoanContractSearch;
        protected String refresh;
        String ls_sql = "", ls_sqlext = "", ls_temp = "";

        private void JsLoanContractSearch()
        {

            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_lncontno = "", ls_memgroup = "", ls_memgroupname = "";
            String ls_stratcont = "", ls_payment = "";
            Decimal ls_cont = 0;


            //member_no
            try
            {
                ls_memno = WebUtil.MemberNoFormat(hmember_no.Value);// dw_data.GetItemString(1, "member_no").Trim();

            }
            //memb_name
            catch { ls_memno = ""; }
            try
            {
                ls_memname = dw_data.GetItemString(1, "memb_name").Trim();

            }
            //memb_surname
            catch { ls_memname = ""; }
            try
            {
                ls_memsurname = dw_data.GetItemString(1, "memb_surname").Trim();

            }
            //loancontract_no
            catch { ls_memsurname = ""; }
            try
            {
                ls_lncontno = dw_data.GetItemString(1, "loancontract_no").Trim();

            }
            //membgroup_no
            catch { ls_lncontno = ""; }
            try
            {
                ls_memgroup = dw_data.GetItemString(1, "membgroup_no").Trim();

            }
            //membgroup_no_1
            catch { ls_memgroup = ""; }
            try
            {
                ls_memgroupname = dw_data.GetItemString(1, "membgroup_no_1").Trim();

            }
            //startcont_tdate
            catch { ls_memgroupname = ""; }
            try
            {
                ls_stratcont = dw_data.GetItemString(1, "startcont_tdate").Trim();

            }
            //payment_status
            catch { ls_stratcont = ""; }
            try
            {
                ls_payment = dw_data.GetItemString(1, "payment_status").Trim();

            }
            //contract_status
            catch { ls_payment = ""; }
            try
            {
                ls_cont = dw_data.GetItemDecimal(1, "contract_status");

            }
            catch { }

            if (ls_memno.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.member_no like '" + ls_memno + "%') ";
            }
            if (ls_memname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '" + ls_memname + "%') ";
            }
            if (ls_memsurname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_surname like '" + ls_memsurname + "%') ";
            }
            if (ls_lncontno.Length > 0)
            {
                ls_sqlext += " and ( lncontmaster.loancontract_no like '" + ls_lncontno + "%') ";
            }
            if (ls_memgroup.Length > 0)
            {
                ls_sqlext += " and ( mbucfmembgroup.membgroup_code = '" + ls_memgroup + "') ";
            }
            if (ls_memgroupname.Length > 0)
            {
                ls_sqlext += " and ( mbucfmembgroup.membgroup_desc = '" + ls_memgroupname + "') ";
            }

            //"LNCONTMASTER"."STARTCONT_DATE"  
            //if (ls_stratcont.Length > 0)
            //{
            //    ls_sqlext += " and ( mbmembmaster.statcont_date = '" + ls_stratcont + "') ";
            //}

            if (ls_payment.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.resign_status = '" + ls_payment + "') ";
            }

            if (ls_cont == 1)
            {
                ls_sqlext += " and ( lncontmaster.contract_status >0) ";
            }
            else if (ls_cont == 2)
            {
                //กรณีสัญญาไม่ปกติ (-9 สัญญายกเลิก, -1 สัญญาจบ และ -11 สัญญาจบ)
                ls_sqlext += " and ( lncontmaster.contract_status < 0 ) ";
            }



            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();

        }

        public void Refresh()
        {

        }

        public void InitJsPostBack()
        {
            LoanContractSearch = WebUtil.JsPostBack(this, "LoanContractSearch");
            refresh = WebUtil.JsPostBack(this, "refresh");

        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_data.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);

            ls_sql = dw_detail.GetSqlSelect();
            if (IsPostBack)
            {
                dw_data.RestoreContext();
                dw_detail.RestoreContext();
            }
            else
            {
                if (dw_data.RowCount < 1)
                {
                    dw_data.InsertRow(0);
                }
            }

            if (!hidden_search.Value.Equals(""))
            {
                dw_detail.SetSqlSelect(hidden_search.Value);
                dw_detail.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "LoanContractSearch")
            {
                JsLoanContractSearch();
            }
            else if (eventArg == "refresh")
            {
                Refresh();
            }
        }

        public void WebDialogLoadEnd()
        {

        }

    }
}
