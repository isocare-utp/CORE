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
    public partial class w_dlg_sl_deptmaster_search : PageWebDialog, WebDialog
    {

        protected String DeptMasterSearch;

        private void JsDeptMasterSearch()
        {

            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_req_no = "", ls_deptacc_name = "";
            String ls_req_tdate = "", ls_depttype_code = "";

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
                ls_memname = dw_data.GetItemString(1, "member_name").Trim();

            }
            catch { ls_memname = ""; }
            try
            {
                ls_memsurname = dw_data.GetItemString(1, "memb_surname").Trim();

            }
            catch { ls_memsurname = ""; }
            try
            {
                ls_req_no = dw_data.GetItemString(1, "request_no").Trim();

            }
            catch { ls_req_no = ""; }
            try
            {
                ls_deptacc_name = dw_data.GetItemString(1, "deptacc_name").Trim();

            }
            catch { ls_deptacc_name = ""; }
            try
            {
                ls_req_tdate = dw_data.GetItemString(1, "request_tdate").Trim();

            }
            catch { ls_req_tdate = ""; }
            try
            {
                ls_depttype_code = dw_data.GetItemString(1, "deptype_code").Trim();

            }
            catch { ls_depttype_code = ""; }


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
            if (ls_req_no.Length > 0)
            {
                ls_sqlext += " and ( lncontmaster.loancontract_no like '" + ls_req_no + "%') ";
            }
            if (ls_deptacc_name.Length > 0)
            {
                ls_sqlext += " and ( mbucfmembgroup.membgroup_code = '" + ls_deptacc_name + "') ";
            }
            if (ls_req_tdate.Length > 0)
            {
                ls_sqlext += " and ( mbucfmembgroup.membgroup_desc = '" + ls_req_tdate + "') ";
            } if (ls_depttype_code.Length > 0)
            {
                ls_sqlext += " and ( mbucfmembgroup.membgroup_desc = '" + ls_depttype_code + "') ";
            }


            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();

        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            DeptMasterSearch = WebUtil.JsPostBack(this, "DeptMasterSearch");
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
                    JsDeptMasterSearch();
                }
                catch { }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "DeptMasterSearch")
            {
                JsDeptMasterSearch();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion
    }
}
