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
using DataLibrary;

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_sl_member_search : PageWebDialog, WebDialog
    {
        WebState state;
        protected String LoanContractSearch;
        protected String postSalaryId;
        DwTrans SQLCA;
        String ls_sql = "", ls_sqlext = "", ls_temp = "";


        protected void cb_find_Click(object sender, EventArgs e)
        {

            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_memgroup = "", ls_contno = "", ls_loancontract = "";
            String ls_cardperson = "", ls_subgroup = "";

            string coop_id = state.SsCoopId;
            try
            {
                coop_id = dw_data.GetItemString(1, "coop_select");
            }
            catch
            { Exception ex; }

            string ls_sql1 = "and mbmembmaster.coop_id ='" + coop_id + "'";


            try
            {
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
                ls_memgroup = dw_data.GetItemString(1, "membgroup_no").Trim();

            }
            catch { ls_memgroup = ""; }

            try
            {
                ls_contno = dw_data.GetItemString(1, "loancontract_no").Trim();

            }
            catch { ls_contno = ""; }

            if (ls_memno.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.member_no like '%" + ls_memno + "%') ";
            }

            if (ls_memname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '%" + ls_memname + "%') ";
            }
            if (ls_memsurname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_surname like '%" + ls_memsurname + "%') ";
            }
            if (ls_memgroup.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.membgroup_code = '%" + ls_memgroup + "') ";
            }

            if (ls_contno.Length > 0)
            {
                //ls_sqlext += " and ( lncontmaster.loancontract_no like '" + ls_contno + "%') ";
                ls_sqlext += " and ( mbmembmaster.member_no in( select lncontmaster.member_no from lncontmaster where lncontmaster.loancontract_no like '%" + ls_contno + "%')) ";
            }

            ls_temp = ls_sql + ls_sql1 + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();
            DwUtil.RetrieveDDDW(dw_data, "coop_select", "kp_recieve_return.pbl", state.SsCoopControl);
        }

        public void InitJsPostBack()
        {
            LoanContractSearch = WebUtil.JsPostBack(this, "LoanContractSearch");
            postSalaryId = WebUtil.JsPostBack(this, "postSalaryId");
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

                DwUtil.RetrieveDDDW(dw_data, "coop_select", "kp_recieve_return.pbl", state.SsCoopControl);
                string coop_id = state.SsCoopControl;
                try
                {
                    coop_id = dw_data.GetItemString(1, "coop_select");
                }
                catch (Exception ex)
                { }
                DwUtil.RetrieveDDDW(dw_data, "membgroup_no_1", "kp_recieve_return.pbl", coop_id);
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
            if (eventArg == "LoanContractSearch")
            {
                JsLoanContractSearch();
            }
            else if (eventArg == "postSalaryId")
            {
                JsPostSalaryId();
            }
        }
        private void JsPostSalaryId()
        {
            String salary_id = dw_data.GetItemString(1, "salary_id");
            //ดึงเลขสมาชิกจากเลขพนักงาน
            string sqlMemb = @"select member_no from mbmembmaster where salary_id like '" + salary_id + @"%' and member_status = 1";
            Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
            if (dtMemb.Next())
            {
                //เซตค่าของเลขสมาชิกที่ได้มาจากเลขพนักงานให้กับตัวแปร hmember_no
                hmember_no.Value = dtMemb.GetString("member_no");
                dw_data.SetItemString(1, "member_no", hmember_no.Value);
                if (salary_id != null || salary_id != "") { dw_data.SetItemString(1, "salary_id", salary_id); }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + hmember_no.Value);
            }
        }
        private void JsLoanContractSearch()
        {
            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_memgroup = "", ls_contno = "", ls_loancontract = "";
            String ls_cardperson = "", ls_subgroup = "";


            ls_sql = dw_detail.GetSqlSelect();

            string coop_id = "";
            try
            {
                coop_id = dw_data.GetItemString(1, "coop_select");
            }
            catch
            { Exception ex; }

            string ls_sql1 = "and mbmembmaster.coop_id ='" + coop_id + "'";
            try
            {
                //ls_memno = hmember_no.Value;
                ls_memno = WebUtil.MemberNoFormat(hmember_no.Value);
                //dw_data.GetItemString(1, "member_no").Trim();

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
                ls_memgroup = dw_data.GetItemString(1, "membgroup_no").Trim();

            }
            catch { ls_memgroup = ""; }

            try
            {
                ls_contno = dw_data.GetItemString(1, "loancontract_no").Trim();

            }
            catch { ls_contno = ""; }

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
            if (ls_memgroup.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.membgroup_code = '" + ls_memgroup + "') ";
            }




            if (ls_contno.Length > 0)
            {
                //ls_sqlext += " and ( lncontmaster.loancontract_no like '" + ls_contno + "%') ";
                ls_sqlext += " and ( mbmembmaster.member_no in( select lncontmaster.member_no from lncontmaster where lncontmaster.loancontract_no like '" + ls_contno + "%')) ";
            }

            ls_temp = ls_sql + ls_sql1 + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);



            dw_detail.Retrieve();
            dw_data.SetItemString(1, "member_no", "");
            DwUtil.RetrieveDDDW(dw_data, "coop_select", "kp_recieve_return.pbl", state.SsCoopControl);
        }
        public void WebDialogLoadEnd()
        {
            // SQLCA.Disconnect();
        }
    }
}
