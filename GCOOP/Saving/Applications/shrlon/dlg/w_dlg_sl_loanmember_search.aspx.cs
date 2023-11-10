using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanmember_search : PageWebDialog, WebDialog
    {
        WebState state;
        protected String LoanContractSearch;
        DwTrans SQLCA;
        String ls_sql = "", ls_sqlext = "", ls_temp = "";

        String coop_id;
        protected void cb_find_Click(object sender, EventArgs e)
        {

            DwTrans SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_detail.SetTransaction(SQLCA);
            
            
            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_memgroup = "", ls_contno = "", ls_loancontract = "";
            String ls_cardperson = "", ls_subgroup = "", ls_salaid = "";


            try
            {
                ls_salaid = ls_memno = dw_data.GetItemString(1, "salary_id").Trim();
            }
            catch { ls_salaid = ""; }

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

            if (ls_salaid.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.salary_id like '" + ls_salaid + "%') and mbmembmaster.coop_id='" + coop_id + "'";
            }

            if (ls_memno.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.member_no like '" + ls_memno + "%') and mbmembmaster.coop_id='" + coop_id + "'";
            }

            if (ls_memname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '" + ls_memname + "%') and mbmembmaster.coop_id='" + coop_id + "'";
            }
            if (ls_memsurname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_surname like '" + ls_memsurname + "%') and mbmembmaster.coop_id='" + coop_id + "'";
            }
            if (ls_memgroup.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.membgroup_code = '" + ls_memgroup + "') and mbmembmaster.coop_id='" + coop_id + "'";
            }

            if (ls_contno.Length > 0)
            {
                //ls_sqlext += " and ( lncontmaster.loancontract_no like '" + ls_contno + "%') ";
                ls_sqlext += " and ( mbmembmaster.member_no in( select lncontmaster.member_no from lncontmaster where lncontmaster.loancontract_no like '" + ls_contno + "%')and coop_id='" + coop_id + "') and mbmembmaster.coop_id='" + coop_id + "'";
            }

            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;

            //hidden_search.Value = ls_temp;
            //dw_detail.SetSqlSelect(hidden_search.Value); โค้ดเก่า
            // dw_detail.Retrieve();

            dw_detail.SetSqlSelect(ls_temp);
            dw_detail.Retrieve();
            
        }

        public void InitJsPostBack()
        {

            LoanContractSearch = WebUtil.JsPostBack(this, "LoanContractSearch");
        }

        public void WebDialogLoadBegin()
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();

            dw_data.SetTransaction(SQLCA);
            dw_detail.SetTransaction(SQLCA);
            ls_sql = dw_detail.GetSqlSelect();

            coop_id = Request["coopId"].ToString();
            if (coop_id == state.SsCoopControl)
            {
                ls_sql = ls_sql + "and mbmembmaster.coop_id='" + coop_id + "'";
            }
            else
            {
                coop_id = state.SsCoopControl;
                ls_sql = ls_sql + "and mbmembmaster.coop_id='" + coop_id + "'";
            }
            ls_sql = ls_sql + "and mbmembmaster.coop_id='" + coop_id + "'";

            if (!IsPostBack)
            {
                // coop_id = Request["coopId"].ToString();
                dw_data.InsertRow(0);
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

        }
        private void JsLoanContractSearch()
        {
            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_memgroup = "", ls_contno = "", ls_loancontract = "";
            String ls_cardperson = "", ls_subgroup = "", ls_salaid = "";

            DwTrans SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_detail.SetTransaction(SQLCA);


            try
            {
                ls_salaid = dw_data.GetItemString(1, "salary_id").Trim();
            }
            catch { ls_salaid = ""; }

            try
            {
                //mai แก้ไขกรณีที่กรอกข้อมูลเป็น S ตัวเดียว
                if (HdMemberNo.Value == "S")
                {
                    ls_memno = "S";
                }
                else
                {
                    ls_memno = WebUtil.MemberNoFormat(HdMemberNo.Value);
                }
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

            if (ls_salaid.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.salary_id like '" + ls_salaid + "%') and mbmembmaster.coop_id='" + coop_id + "'";
            }

            if (ls_memno.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.member_no like '" + ls_memno + "%') and mbmembmaster.coop_id='" + coop_id + "'";
            }

            if (ls_memname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '" + ls_memname + "%') and mbmembmaster.coop_id='" + coop_id + "'";
            }
            if (ls_memsurname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_surname like '" + ls_memsurname + "%')and mbmembmaster.coop_id='" + coop_id + "'";
            }
            if (ls_memgroup.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.membgroup_code = '" + ls_memgroup + "')and mbmembmaster.coop_id='" + coop_id + "'";
            }

            if (ls_contno.Length > 0)
            {
                //ls_sqlext += " and ( lncontmaster.loancontract_no like '" + ls_contno + "%') ";
                ls_sqlext += " and ( mbmembmaster.member_no in( select lncontmaster.member_no from lncontmaster where lncontmaster.loancontract_no like '" + ls_contno + "%') and coop_id='" + coop_id + "') and mbmembmaster.coop_id='" + coop_id + "'";
            }

            ls_temp = ls_sql + ls_sqlext;
            //hidden_search.Value = ls_temp;
            //dw_detail.SetSqlSelect(hidden_search.Value);
            //dw_detail.Retrieve();


            dw_detail.SetSqlSelect(ls_temp);
            dw_detail.Retrieve();

            dw_data.SetItemString(1, "member_no", "");
        }



        public void WebDialogLoadEnd()
        {
            // SQLCA.Disconnect();
        }
    }
}
