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

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loancontract_search_memno : PageWebDialog, WebDialog
    {
        protected String LoanContractSearch;

        private void JsLoanContractSearch()
        {

            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_lncontno = "", ls_memgroup = "", ls_memgroupname = "";
            String ls_stratcont = "", ls_payment = "", ls_cont = "";

            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = dw_detail.GetSqlSelect();
            //member_no            

            try
            {
                //รับค่ามาจากชี๊ต memno
                ls_memno = dw_data.GetItemString(1, "member_no").Trim();

            }

            catch { ls_memno = ""; }
            try
            {
                ls_memname = dw_data.GetItemString(1, "memb_name").Trim();

            }

            catch { ls_memname = ""; }
            try
            {
                ls_memsurname = dw_data.GetItemString(1, "memb_surname").Trim();

            }

            catch { ls_memsurname = ""; }
            try
            {
                ls_lncontno = dw_data.GetItemString(1, "loancontract_no").Trim();

            }

            catch { ls_lncontno = ""; }
            try
            {
                ls_memgroup = dw_data.GetItemString(1, "membgroup_no").Trim();

            }

            catch { ls_memgroup = ""; }
            try
            {
                ls_memgroupname = dw_data.GetItemString(1, "membgroup_no_1").Trim();

            }

            catch { ls_memgroupname = ""; }
            try
            {
                ls_stratcont = dw_data.GetItemString(1, "startcont_tdate").Trim();

            }

            catch { ls_stratcont = ""; }
            try
            {
                ls_payment = dw_data.GetItemString(1, "payment_status").Trim();

            }

            catch { ls_payment = ""; }
            try
            {
                ls_cont = dw_data.GetItemString(1, "contract_status").Trim();

            }
            catch { ls_cont = ""; }

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
            if (ls_cont.Length > 0)
            {
                if (ls_cont == "1")
                {
                    ls_sqlext += " and ( lncontmaster.contract_status >0) ";
                }
                else
                {
                    ls_sqlext += " and ( lncontmaster.contract_status <0) ";
                }
            }

            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            LoanContractSearch = WebUtil.JsPostBack(this, "LoanContractSearch");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_data.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);

            if (IsPostBack)
            {
                dw_data.RestoreContext();
            }
            else
            {
                dw_data.InsertRow(1);
                try
                {
                    String memno = Request["memno"].Trim();
                    dw_data.SetItemString(1, "member_no", memno);
                    JsLoanContractSearch();
                }
                catch { }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "LoanContractSearch")
            {
                JsLoanContractSearch();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion
    }
}
