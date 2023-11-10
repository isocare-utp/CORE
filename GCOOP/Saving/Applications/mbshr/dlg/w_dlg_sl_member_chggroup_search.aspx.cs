using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_sl_member_chggroup_search : PageWebDialog, WebDialog
    {

        protected String LoanContractSearch;
        private DwThDate tdwMain;
        DwTrans SQLCA;
        String ls_sql = "", ls_sqlext = "", ls_temp = "";

        public void InitJsPostBack()
        {

            LoanContractSearch = WebUtil.JsPostBack(this, "LoanContractSearch");
            tdwMain = new DwThDate(dw_data, this);
            tdwMain.Add("mem_date", "mem_tdate");
            tdwMain.Add("work_date", "work_tdate");
        }

        public void WebDialogLoadBegin()
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_data.SetTransaction(SQLCA);
            dw_detail.SetTransaction(SQLCA);
            ls_sql = dw_detail.GetSqlSelect();

            if (!IsPostBack)
            {
                dw_data.InsertRow(0);
                string coop_id = state.SsCoopControl;
                tdwMain.Eng2ThaiAllRow();
            }

            else
            {
                dw_data.RestoreContext();
                dw_detail.RestoreContext();
            }
            if (!hidden_search.Value.Equals(""))
            {
                try
                {
                    dw_detail.SetSqlSelect(hidden_search.Value);
                    dw_detail.Retrieve();
                }
                catch (Exception dw_detail_error)
                {
                    HfErrorMessage.Value = dw_detail_error.Message;
                }
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
            SQLCA.Disconnect();
        }

        protected void cb_find_Click(object sender, EventArgs e)
        {

            String ls_memberno = "";
            String ls_name = "", ls_surname = "";
            String mem_tdate = "";
            String work_tdate = "";
            decimal ldc_docstatus = dw_data.GetItemDecimal(1, "doc_status");


            string coop_id = state.SsCoopId;
            string ls_sql1 = "and MBREQCHANGEGROUP.coop_id ='" + coop_id + "'";


            try
            {
                ls_memberno =WebUtil.MemberNoFormat(dw_data.GetItemString(1, "member_no").Trim());

            }
            catch { ls_memberno = ""; }


            try
            {
                ls_name = dw_data.GetItemString(1, "name").Trim();

            }
            catch { ls_name = ""; }
            try
            {
                ls_surname = dw_data.GetItemString(1, "surname").Trim();

            }
            catch { ls_surname = ""; }


            try
            {
                mem_tdate = dw_data.GetItemString(1, "mem_tdate").Trim();

            }
            catch { ls_name = ""; }
            try
            {
                work_tdate = dw_data.GetItemString(1, "work_tdate").Trim();

            }
            catch { ls_surname = ""; }


            if (ls_memberno.Length > 0)
            {
                ls_sqlext = " and (  MBREQCHANGEGROUP.MEMBER_NO  ='" + ls_memberno + "') ";
            }

            if (ldc_docstatus == 8 || ldc_docstatus == 1 || ldc_docstatus == -9)
            {
                ls_sqlext += " and ( MBREQCHANGEGROUP.request_status = " + ldc_docstatus + ") ";
            }
            if (ls_name.Length > 0)
            {
                ls_sqlext += " and ( MBMEMBMASTER.MEMB_NAME like '%" + ls_name + "%') ";
            }
            if (ls_surname.Length > 0)
            {
                ls_sqlext += " and ( MBMEMBMASTER.memb_surname like '%" + ls_surname + "%') ";
            }
            if (mem_tdate.Length > 0)
            {
                if (mem_tdate != "00000000" )
                {
                    mem_tdate = (Convert.ToInt32(mem_tdate) - 543).ToString("00000000");
                    ls_sqlext += " and ( MBREQCHANGEGROUP.APV_DATE = to_date('" + mem_tdate + "','ddmmyyyy')) ";
                }
            }
            if (work_tdate.Length > 0)
            {
                if (work_tdate != "00000000" )
                {
                    work_tdate = (Convert.ToInt32(work_tdate) - 543).ToString("00000000");
                    ls_sqlext += " and ( MBREQCHANGEGROUP.ENTRY_DATE =to_date('" + work_tdate + "','ddmmyyyy') ) ";
                }
            }
            ls_temp = ls_sql + ls_sql1 + ls_sqlext;
            hidden_search.Value = ls_temp;
            if (!hidden_search.Value.Equals(""))
            {
                try
                {
                    dw_detail.SetSqlSelect(hidden_search.Value);
                    dw_detail.Retrieve();
                }
                catch (Exception dw_detail_error)
                {
                    HfErrorMessage.Value = dw_detail_error.Message;                    
                }
            }
            //dw_detail.SetSqlSelect(hidden_search.Value);
            //dw_detail.Retrieve();
            // DwUtil.RetrieveDDDW(dw_data, "coop_select", "kp_recieve_return.pbl", state.SsCoopControl);
        }
    }
}
