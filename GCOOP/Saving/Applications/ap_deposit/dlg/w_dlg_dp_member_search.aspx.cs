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

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_member_search : PageWebDialog, WebDialog
    {
        protected String MemberNoSearch;

        private void JsMemberNoSearch()
        {
            String ls_memno = "%", ls_salary_id = "%", ls_memname = "%", ls_memsurname = "%";
            String ls_sql = "", ls_sqlext = "", ls_temp = "";


            //ls_sql = DwList.GetSqlSelect();

            ls_sql = @"  SELECT MBMEMBMASTER.MEMBER_NO,   
                         MBUCFPRENAME.PRENAME_DESC,   
                         MBMEMBMASTER.MEMB_NAME,   
                         MBMEMBMASTER.MEMB_SURNAME,   
                         MBMEMBMASTER.COOP_ID  
                    FROM MBMEMBMASTER,   
                         MBUCFPRENAME  
                   WHERE ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) 
                     AND ( MBMEMBMASTER.COOP_ID = '" + state.SsCoopControl + "' )";

            //member_no
            try
            {
                ls_memno += (DwMain.GetItemString(1, "member_no")+ "%");

            }
            //memb_name
            catch {}
            try
            {
                ls_salary_id += (DwMain.GetItemString(1, "salary_id") + "%");

            }
            //memb_name
            catch { }
            try
            {
                ls_memname += (DwMain.GetItemString(1, "member_name") + "%");

            }
            //memb_surname
            catch {}
            try
            {
                ls_memsurname += (DwMain.GetItemString(1, "member_surname") + "%");

            }
            catch {}

            DwUtil.RetrieveDataWindow(DwList, "dp_member_search.pbl", null, HdCoopid.Value, ls_memno, ls_memname, ls_memsurname, ls_salary_id);

            //if (ls_memno.Length > 0)
            //{
            //    ls_sqlext = " and (  mbmembmaster.member_no like '" + ls_memno + "%') ";
            //}
            //if (ls_memname.Length > 0)
            //{
            //    ls_sqlext += " and ( mbmembmaster.memb_name like '" + ls_memname + "%') ";
            //}
            //if (ls_memsurname.Length > 0)
            //{
            //    ls_sqlext += " and ( mbmembmaster.memb_surname like '" + ls_memsurname + "%') ";
            //}

            //ls_temp = ls_sql + ls_sqlext;
           // hidden_search.Value = ls_temp;
            //DwList.SetSqlSelect(hidden_search.Value);
            //DwList.Retrieve();
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
                //DwList.Retrieve(HdCoopid.Value);
                DwUtil.RetrieveDataWindow(DwList, "dp_member_search.pbl", null, HdCoopid.Value,"%","%","%");
            }
            //if (!hidden_search.Value.Equals(""))
            //{
            //    DwList.SetSqlSelect(hidden_search.Value);
            //    DwList.Retrieve(HdCoopid.Value);
            //}
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
