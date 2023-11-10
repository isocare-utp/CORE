using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_member_search : PageWebDialog, WebDialog
    {
        private String pbl = "kp_recieve_return.pbl";
        protected String LoanContractSearch;
        protected String postSalaryId;
        private String ls_sql = "";
        private String ls_sqlext = "";
        private String ls_temp = "";

        protected void cb_find_Click(object sender, EventArgs e)
        {
            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_memgroup = "", ls_contno = "", ls_loancontract = "";
            String ls_cardperson = "", ls_subgroup = "";

            string coop_id = state.SsCoopControl;
            try
            {
                coop_id = dw_data.GetItemString(1, "coop_select");
            }
            catch
            {
                Exception ex;
            }

            string ls_sql1 = "and mbmembmaster.coop_id ='" + coop_id + "'";

            try
            {
                ls_memno = WebUtil.MemberNoFormat(dw_data.GetItemString(1, "member_no").Trim());

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

            //dw_detail.SetSqlSelect(hidden_search.Value);
            //dw_detail.Retrieve();
            DataTable dt = WebUtil.Query(hidden_search.Value);
            DwUtil.ImportData(dt, dw_detail, null);

            DwUtil.RetrieveDDDW(dw_data, "coop_select", pbl, state.SsCoopControl);
        }

        public void InitJsPostBack()
        {
            LoanContractSearch = WebUtil.JsPostBack(this, "LoanContractSearch");
            postSalaryId = WebUtil.JsPostBack(this, "postSalaryId");
        }

        public void WebDialogLoadBegin()
        {
            //ls_sql = dw_detail.GetSqlSelect();
            ls_sql = @"
                SELECT DISTINCT TOP 500
	                MBMEMBMASTER.MEMBER_NO,   
	                MBUCFPRENAME.PRENAME_DESC,   
	                MBMEMBMASTER.MEMB_NAME,   
	                MBMEMBMASTER.MEMB_SURNAME,   
	                MBMEMBMASTER.MEMBGROUP_CODE,   
	                MBUCFMEMBGROUP.MEMBGROUP_DESC  
                FROM 
	                MBMEMBMASTER,   
	                MBUCFMEMBGROUP,   
	                MBUCFPRENAME  
                WHERE 
	                ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE ) and  
	                ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
	                ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID )
	                
            ";
            if (!IsPostBack)
            {
                dw_data.InsertRow(0);

                DwUtil.RetrieveDDDW(dw_data, "coop_select", pbl, state.SsCoopControl);
                string coop_id = state.SsCoopControl;
                try
                {
                    coop_id = dw_data.GetItemString(1, "coop_select");
                }
                catch (Exception ex)
                { }
                DwUtil.RetrieveDDDW(dw_data, "membgroup_no_1", pbl, coop_id);
            }

            else
            {
                dw_data.RestoreContext();
                dw_detail.RestoreContext();
            }

            if (!hidden_search.Value.Equals(""))
            {
                //dw_detail.SetSqlSelect(hidden_search.Value);
                //dw_detail.Retrieve();
                DataTable dt = WebUtil.Query(hidden_search.Value);
                DwUtil.ImportData(dt, dw_detail, null);
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

        private void JsLoanContractSearch()
        {
            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_memgroup = "", ls_contno = "";


            //ls_sql = dw_detail.GetSqlSelect();

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
                ls_memno = hmember_no.Value;
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
                ls_sqlext += " and ( mbmembmaster.member_no in( select lncontmaster.member_no from lncontmaster where lncontmaster.loancontract_no like '" + ls_contno + "%')) ";
            }

            ls_temp = ls_sql + ls_sql1 + ls_sqlext;
            hidden_search.Value = ls_temp;

            //dw_detail.SetSqlSelect(hidden_search.Value);
            //dw_detail.Retrieve();
            DataTable dt = WebUtil.Query(hidden_search.Value);
            DwUtil.ImportData(dt, dw_detail, null);


            dw_data.SetItemString(1, "member_no", "");
            DwUtil.RetrieveDDDW(dw_data, "coop_select", pbl, state.SsCoopControl);
        }
        private void JsPostSalaryId()
        {
            String memberNo = "";
            String salary_id = dw_data.GetItemString(1, "salary_id").Trim();
            //ดึงเลขสมาชิกจากเลขพนักงาน
            string sqlMemb = @"select member_no from mbmembmaster where salary_id like '" + salary_id + @"%' and member_status = 1";
            Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
            if (dtMemb.Next())
            {
                memberNo = dtMemb.GetString("member_no");
                dw_data.SetItemString(1, "member_no", memberNo);
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + memberNo);
            }
        }
        public void WebDialogLoadEnd()
        {
        }
    }
}
