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

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_member_search : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_data.SetTransaction(SQLCA);
            dw_detail.SetTransaction(SQLCA);


            if (!IsPostBack)
            {
                dw_data.InsertRow(0);
            }
            if (!hidden_search.Value.Equals(""))
            {
                dw_detail.SetSqlSelect(hidden_search.Value);
                dw_detail.Retrieve();
            }

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            SQLCA.Disconnect();
        }

        protected void cb_find_Click(object sender, EventArgs e)
        {

            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_memgroup = "", ls_contno = "", ls_salaryid = "";
            String ls_cardperson = "", ls_subgroup = "";
            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = dw_detail.GetSqlSelect();
            try
            {
                ls_memno = dw_data.GetItemString(1, "member_no").Trim();

            }
            catch { ls_memno = ""; }
            try
            {
                ls_cardperson = dw_data.GetItemString(1, "card_person").Trim();

            }
            catch { ls_cardperson = ""; }
            try
            {
                ls_salaryid = dw_data.GetItemString(1, "salary_id").Trim();

            }
            catch { ls_salaryid = ""; }
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
                ls_subgroup = dw_data.GetItemString(1, "subgroup_code").Trim();

            }
            catch { ls_subgroup = ""; }
            try
            {
                ls_contno = dw_data.GetItemString(1, "loancontract_no").Trim();

            }
            catch { ls_contno = ""; }




            if (ls_memno.Length > 0)
            {
                ls_sqlext = " and (  mbmembmaster.member_no like '%" + ls_memno + "%') ";
            }
            if (ls_cardperson.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.card_person like '" + ls_cardperson + "%') ";
            }
            if (ls_salaryid.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.salary_id like '" + ls_salaryid + "%') ";
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
            if (ls_subgroup.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.subgroup_code = '" + ls_subgroup + "') ";
            }
            if (ls_contno.Length > 0)
            {
                ls_sqlext += " and ( lncontmaster.loancontract_no like '" + ls_contno + "%') ";
            }

            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();
        }
    }
}
