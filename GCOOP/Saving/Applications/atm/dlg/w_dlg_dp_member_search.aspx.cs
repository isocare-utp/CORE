using System;
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
using Saving.WcfCommon;

namespace Saving.Applications.atm.dlg
{
    public partial class w_dlg_dp_member_search : PageWebDialog,WebDialog
    {
        protected String MemberNoSearch;

        private void JsMemberNoSearch()
        {
            String ls_memno = "", ls_memname = "", ls_memsurname = "";
            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            //ls_sql = DwList.GetSqlSelect();

            ls_sql = @"  SELECT MBMEMBMASTER.MEMBER_NO,   
                         MBUCFPRENAME.PRENAME_DESC,   
                         MBMEMBMASTER.MEMB_NAME,   
                         MBMEMBMASTER.MEMB_SURNAME,   
                         MBMEMBMASTER.COOP_ID  
                    FROM MBMEMBMASTER,   
                         MBUCFPRENAME  
                   WHERE ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) AND ( MBMEMBMASTER.COOP_ID = '" + HdCoopid.Value + "' )";
 
            //member_no
            try
            {
                ls_memno = DwMain.GetItemString(1, "member_no").Trim();

            }
            //memb_name
            catch { ls_memno = ""; }
            try
            {
                ls_memname = DwMain.GetItemString(1, "member_name").Trim();

            }
            //memb_surname
            catch { ls_memname = ""; }
            try
            {
                ls_memsurname = DwMain.GetItemString(1, "member_surname").Trim();

            }
            catch { ls_memsurname = ""; }

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

            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            DwList.SetSqlSelect(hidden_search.Value);
            DwList.Retrieve(HdCoopid.Value);
        }
        
        #region WebDialog Members

        public void InitJsPostBack()
        {
            MemberNoSearch = WebUtil.JsPostBack(this, "MemberNoSearch");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            DwList.SetTransaction(sqlca);
            if (IsPostBack)
            {
                DwMain.RestoreContext();
            }

            if (DwMain.RowCount < 1)
            {
                HdCoopid.Value = Request["coopid"];
                DwMain.InsertRow(0);
                DwList.Retrieve(HdCoopid.Value);
            }
            if (!hidden_search.Value.Equals(""))
            {
                DwList.SetSqlSelect(hidden_search.Value);
                DwList.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "MemberNoSearch")
            {
                JsMemberNoSearch();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion
    }
}
