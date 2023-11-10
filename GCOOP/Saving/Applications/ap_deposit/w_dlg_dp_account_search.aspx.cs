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


namespace Saving.Applications.ap_deposit
{
    public partial class w_dlg_dp_account_search : PageWebDialog, WebDialog
    {
        private string is_sql;

        protected void BSearch_Click(object sender, EventArgs e)
        {
            string ls_member_no, ls_member_name, ls_member_surname, ls_member_group_no;
            string ls_account_no, ls_account_name, ls_account_type;
            string ls_sqlext, ls_temp, ls_branch;
            //dw_data.acceptteaxt();
            ls_sqlext = "";
            try
            {
                ls_member_no = dw_data.GetItemString(1, "member_no");
            }
            catch
            {
                ls_member_no = "";
            }
            try
            {
                ls_member_name = dw_data.GetItemString(1, "member_name");
            }
            catch
            {
                ls_member_name = "";
            }
            try
            {
                ls_member_surname = dw_data.GetItemString(1, "member_surname");
            }
            catch
            {
                ls_member_surname = "";
            }
            try
            {
                ls_member_group_no = dw_data.GetItemString(1, "member_group_no");
            }
            catch
            {
                ls_member_group_no = "";
            }
            try
            {
                ls_account_no = dw_data.GetItemString(1, "account_no");
            }
            catch
            {
                ls_account_no = "";
            }
            try
            {
                ls_account_name = dw_data.GetItemString(1, "account_name");
            }
            catch
            {
                ls_account_name = "";
            }
            try
            {
                ls_account_type = dw_data.GetItemString(1, "account_type");
            }
            catch
            {
                ls_account_type = "";
            }
            try
            {
                ls_branch = dw_data.GetItemString(1, "branch_id");
            }
            catch
            {
                ls_branch = "";
            }
            //--
            if (ls_member_no.Length > 0)
            {
                ls_sqlext = " and ( mbmembmaster.member_no = '" + ls_member_no + "') ";
            }
            if (ls_member_name.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '" + ls_member_name + "%') ";
            }
            if (ls_member_surname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_surname like '" + ls_member_surname + "%') ";
            }
            if (ls_member_group_no.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.membgroup_code = '" + ls_member_group_no + "') ";
            }
            if (ls_account_no.Length > 0)
            {
                ls_sqlext += " and ( dpdeptmaster.deptaccount_no Like '%" + ls_account_no + "%' ) ";
            }
            if (ls_account_name.Length > 0)
            {
                ls_sqlext += " and ( dpdeptmaster.deptaccount_name Like '" + ls_account_name + "%') ";
            }
            if (ls_account_type.Length > 0)
            {
                ls_sqlext += " and ( dpdepttype.depttype_group = '" + ls_account_type + "') ";
            }
            if (ls_branch.Length > 0)
            {
                ls_sqlext += " and ( dpdeptmaster.branch_id = '" + ls_branch + "') ";
            }
            if (ls_sqlext == null) ls_sqlext = "";
            ls_temp = is_sql + ls_sqlext;
            HSqlTemp.Value = ls_temp;
            dw_detail.SetSqlSelect(HSqlTemp.Value);
            dw_detail.Retrieve();
            this.Filter();
        }

        private void Filter()
        {
            int li_closestatus;
            bool lb_status;
            lb_status = false;
            if (lb_status)
            {
                li_closestatus = 1;
            }
            else
            {
                li_closestatus = 0;
            }
            dw_detail.SetFilter("deptclose_status = " + li_closestatus);
            dw_detail.Filter();
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_detail.SetTransaction(sqlca);
            is_sql = dw_detail.GetSqlSelect();
            try
            {
                if (!IsPostBack)
                {
                    dw_data.InsertRow(0);
                    HSqlTemp.Value = is_sql;
                }
            }
            catch { }
            //Sybase.DataWindow.DataWindowChild dc = dw_detail.GetChild("");
            //dc.SetTransaction(sqlca);
            //dc.Retrieve();
            dw_detail.SetSqlSelect(HSqlTemp.Value);
            dw_detail.Retrieve();
            this.Filter();

        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion
    }
}