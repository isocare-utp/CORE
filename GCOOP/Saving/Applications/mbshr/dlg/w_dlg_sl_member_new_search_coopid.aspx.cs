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
    public partial class w_dlg_sl_member_new_search_coopid : PageWebDialog, WebDialog
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
                DwUtil.RetrieveDDDW(dw_data, "coop_id", "sl_member_new_search", state.SsCoopControl);
                dw_data.SetItemString(1, "coop_id", state.SsCoopId);
                tdwMain.Eng2ThaiAllRow();
            }

            else
            {
                dw_data.RestoreContext();
                dw_detail.RestoreContext();
            }
            if (!hidden_search.Value.Equals(""))
            {
                dw_detail.SetSqlSelect(hidden_search.Value);
                dw_detail.Retrieve();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
           

        }
       
        public void WebDialogLoadEnd()
        {
            // SQLCA.Disconnect();
        }

        protected void cb_find_Click(object sender, EventArgs e)
        {

            String ls_docno = "";
            String ls_name = "", ls_surname = "";
            String mem_tdate = "";
            String work_tdate = "";
            decimal ldc_docstatus = dw_data.GetItemDecimal(1, "doc_status");


            //string coop_id = state.SsCoopId;
            String coop_id = dw_data.GetItemString(1, "coop_id");
            string ls_sql1 = "and mbreqappl.coop_id ='" + coop_id + "'";


            try
            {
                ls_docno = dw_data.GetItemString(1, "docno").Trim();

            }
            catch { ls_docno = ""; }


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


            if (ls_docno.Length > 0)
            {
                ls_sqlext = " and (  mbreqappl.appl_docno like '%" + ls_docno + "%') ";
            }

            if (ldc_docstatus == 8 || ldc_docstatus == 1 || ldc_docstatus == -9)
            {
                ls_sqlext += " and ( mbreqappl.appl_status = " + ldc_docstatus + ") ";
            }
            if (ls_name.Length > 0)
            {
                ls_sqlext += " and ( mbreqappl.memb_name like '%" + ls_name + "%') ";
            }
            if (ls_surname.Length > 0)
            {
                ls_sqlext += " and ( mbreqappl.memb_surname like '%" + ls_surname + "%') ";
            }
            if (mem_tdate.Length > 0)
            {
                if (mem_tdate != "00000000" )
                {
                    mem_tdate = (Convert.ToInt32(mem_tdate) - 543).ToString("00000000");
                    ls_sqlext += " and ( mbreqappl.membdatefix_date = to_date('" + mem_tdate + "','ddmmyyyy')) ";
                }
            }
            if (work_tdate.Length > 0)
            {
                if (work_tdate != "00000000" )
                {
                    work_tdate = (Convert.ToInt32(work_tdate) - 543).ToString("00000000");
                    ls_sqlext += " and ( mbreqappl.apply_date =to_date('" + work_tdate + "','ddmmyyyy') ) ";
                }
            }
            ls_temp = ls_sql + ls_sql1 + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();
            // DwUtil.RetrieveDDDW(dw_data, "coop_select", "kp_recieve_return.pbl", state.SsCoopControl);
        }
    }
}
