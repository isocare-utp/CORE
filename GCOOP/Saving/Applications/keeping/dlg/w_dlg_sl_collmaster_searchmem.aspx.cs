using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_sl_collmaster_searchmem : PageWebDialog, WebDialog   
    {



        protected String collmastersSearch;


        public void InitJsPostBack()
        {
            collmastersSearch = WebUtil.JsPostBack(this, "collmastersSearch");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_master.SetTransaction(sqlca);
            dw_list.SetTransaction(sqlca);

            if (dw_master.RowCount < 1)
            {
                dw_master.InsertRow(0);
                dw_list.InsertRow(0);
            }

            if (IsPostBack)
            {
                dw_master.RestoreContext();
                dw_list.RestoreContext();
            }
            if (!hidden_search.Value.Equals(""))
            {
             
                dw_list.SetSqlSelect(hidden_search.Value);
                dw_list.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "collmastersSearch")
            {
                CollmastersSearch();
            }
        }
        public void CollmastersSearch()
        {
            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_collmast_no = "", ls_memgroup = "", ls_memgroupname = "";
            String ls_collmast_refno = "", ls_collmasttype_code = "";
            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = dw_list.GetSqlSelect();
            //member_no
            try
            {
                ls_memno = dw_master.GetItemString(1, "member_no").Trim();

            }
            //memb_name
            catch { ls_memname = ""; }
            try
            {
                ls_memname = dw_master.GetItemString(1, "memb_name").Trim();

            }
            //memb_surname
            catch { ls_memsurname = ""; }
            try
            {
                ls_memsurname = dw_master.GetItemString(1, "memb_surname").Trim();

            }
            //collmast_no
            catch { ls_collmast_no = ""; }
            try
            {
                ls_collmast_no = dw_master.GetItemString(1, "collmast_no").Trim();

            }
            //membgroup_no
            catch { ls_memgroup = ""; }
            try
            {
                ls_memgroup = dw_master.GetItemString(1, "membgroup_no").Trim();

            }
            //membgroup_no_1
            catch { ls_memgroupname = ""; }
            try
            {
                ls_memgroupname = dw_master.GetItemString(1, "membgroup_no_1").Trim();

            }
            //startcont_tdate
            catch { ls_collmast_refno = ""; }
            try
            {
                ls_collmast_refno = dw_master.GetItemString(1, "collmast_refno").Trim();

            }
            //payment_status
            catch { ls_collmasttype_code = ""; }
            try
            {
                ls_collmasttype_code = dw_master.GetItemString(1, "collmasttype_code").Trim();

            }
            catch { }

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
            if (ls_collmast_no.Length > 0)
            {
                ls_sqlext += " and ( lncolltmaster.collmast_no like '" + ls_collmast_no + "%') ";
            }
            if (ls_memgroup.Length > 0)
            {
                ls_sqlext += " and ( mbucfmembgroup.membgroup_code = '" + ls_memgroup + "') ";
            }
            if (ls_memgroupname.Length > 0)
            {
                ls_sqlext += " and ( mbucfmembgroup.membgroup_desc = '" + ls_memgroupname + "') ";
            }

            if (ls_collmast_refno.Length > 0)
            {
                ls_sqlext += " and ( lncolltmaster.collmast_refno = '" + ls_collmast_refno + "') ";
            }
            if (ls_collmasttype_code.Length > 0)
            {
                ls_sqlext += " and ( lncolltmaster.collmasttype_code = '" + ls_collmasttype_code + "') ";
            }

            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_list.SetSqlSelect(hidden_search.Value);
            dw_list.Retrieve();

        }
        public void WebDialogLoadEnd()
        {
            if (dw_master.RowCount > 1)
            {
                dw_master.DeleteRow(dw_master.RowCount);
            }
        }

    }
}