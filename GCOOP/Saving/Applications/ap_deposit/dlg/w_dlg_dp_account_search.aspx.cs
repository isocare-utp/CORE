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
using Sybase.DataWindow;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_account_search : PageWebDialog, WebDialog
    {
        private String is_sql;
        protected string postMemberNo;

        protected void SearchDeposit()
        {
            string ls_member_no, ls_member_name, ls_member_surname, ls_member_group_no, ls_salary_id;
            string ls_account_no, ls_account_name, ls_account_type;
            string ls_sqlext, ls_temp, ls_coopid;
            ls_sqlext = "";
            try
            {
                ls_member_no = DwMain.GetItemString(1, "member_no");
                ls_member_no = WebUtil.MemberNoFormat(ls_member_no);
                DwMain.SetItemString(1, "member_no", ls_member_no);
            }
            catch
            {
                ls_member_no = "";
            }
            try
            {
                ls_salary_id = DwMain.GetItemString(1, "salary_id");
            }
            catch
            {
                ls_salary_id = "";
            }
            try
            {
                ls_member_name = DwMain.GetItemString(1, "member_name");
            }
            catch
            {
                ls_member_name = "";
            }
            try
            {
                ls_member_surname = DwMain.GetItemString(1, "member_surname");
            }
            catch
            {
                ls_member_surname = "";
            }
            try
            {
                ls_member_group_no = DwMain.GetItemString(1, "member_group_no");
            }
            catch
            {
                ls_member_group_no = "";
            }
            try
            {
                ls_account_no = DwMain.GetItemString(1, "account_no");
            }
            catch
            {
                ls_account_no = "";
            }
            try
            {
                ls_account_name = DwMain.GetItemString(1, "account_name");
            }
            catch
            {
                ls_account_name = "";
            }
            try
            {
                ls_account_type = DwMain.GetItemString(1, "account_type");
            }
            catch
            {
                ls_account_type = "";
            }
            try
            {
                ls_coopid = state.SsCoopControl; //HdCoopid.Value;
            }
            catch
            {
                ls_coopid = "";
            }
            //--
            if (ls_member_no.Length > 0)
            {
                ls_sqlext = " and ( dpdeptmaster.member_no = '" + ls_member_no + "') ";
            }
            if (ls_salary_id.Length > 0)
            {
                ls_sqlext = " and ( mbmembmaster.salary_id = '" + ls_salary_id + "') ";
            }
            if (ls_member_name.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_name like '%" + ls_member_name + "%') ";
            }
            if (ls_member_surname.Length > 0)
            {
                ls_sqlext += " and ( mbmembmaster.memb_surname like '%" + ls_member_surname + "%') ";
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
                ls_sqlext += " and ( dpdeptmaster.deptaccount_name Like '%" + ls_account_name + "%') ";
            }
            if (ls_account_type.Length > 0)
            {
                ls_sqlext += " and ( dpdepttype.depttype_group = '" + ls_account_type + "') ";
            }
            if (ls_coopid.Length > 0)
            {
                ls_sqlext += " and ( dpdeptmaster.memcoop_id = '" + ls_coopid + "') ";
            }
            if (ls_sqlext == null) ls_sqlext = "";
            ls_temp = is_sql + ls_sqlext;
            // HSqlTemp.Value = ls_temp;
            dw_detail.SetSqlSelect(ls_temp);
            dw_detail.Retrieve();
            DwUtil.RetrieveDDDW(DwMain, "coop_id", "dp_slip.pbl", null);
            //this.Filter();
        }

        protected void BSearch_Click(object sender, EventArgs e)
        {
            SearchDeposit();
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
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_detail.SetTransaction(sqlca);
            DwMain.SetTransaction(sqlca);
            DataWindowChild dc = DwMain.GetChild("account_type");
            dc.SetTransaction(sqlca);
            dc.Retrieve();
            is_sql = dw_detail.GetSqlSelect();
            if (!IsPostBack)
            {
                HdCoopid.Value = state.SsCoopId;
                DwMain.InsertRow(0);
                DwUtil.RetrieveDDDW(DwMain, "coop_id", "dp_slip.pbl", state.SsCoopControl);
                dw_detail.Retrieve();
            }
            else
            {
                DwMain.RestoreContext();
                dw_detail.RestoreDataCache();
                dw_detail.RestoreContext();
            }
            //this.Filter();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postMemberNo")
            {
                JsPostMemberNo();
            }
        }

        private void JsPostMemberNo()
        {
            String memberNo = DwMain.GetItemString(1, "member_no");
            memberNo = WebUtil.MemberNoFormat(memberNo);
            DwMain.SetItemString(1, "member_no", memberNo);
            SearchDeposit();
            //dw_detail.Retrieve();
            //FilterMemberNo();

        }

        private void FilterMemberNo()
        {
            String memberNo = DwMain.GetItemString(1, "member_no");
            dw_detail.SetFilter("member_no='" + memberNo + "'");
            String text = "member_no='" + memberNo + "'";
            LtServerMessage.Text = WebUtil.ErrorMessage(text);
            dw_detail.Filter();
        }

        public void WebDialogLoadEnd()
        {

            DwMain.SaveDataCache();
            try
            {
                DwMain.Reset();
                DwMain.InsertRow(0);
            }
            catch { }

        }

        #endregion
    }
}