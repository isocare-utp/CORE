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
    public partial class w_dlg_dp_extmember_search : PageWebDialog,WebDialog
    {
        protected String postExtramem;
        private String ls_sql;

        private void JspostExtramem()
        {
            string ls_member_no = "", ls_card_person = "", ls_member_name = "", ls_member_surname = "";
            string ls_sqlext = "", ls_temp = "";

            try
            {
                ls_member_no = DwData.GetItemString(1, "deptmem_id");
            }
            catch
            {
                ls_member_no = "";
            }
            try
            {
                ls_member_name = DwData.GetItemString(1, "memb_name");
            }
            catch
            {
                ls_member_name = "";
            }
            try
            {
                ls_member_surname = DwData.GetItemString(1, "memb_surname");
            }
            catch
            {
                ls_member_surname = "";
            }
            try
            {
                ls_card_person = DwData.GetItemString(1, "deptmem_taxid");
            }
            catch
            {
                ls_card_person = "";
            }

            if (ls_member_no.Length > 0)
            {
                ls_sqlext = " ( DPDEPTEXTRAMEMBER.DEPTMEM_ID LIKE '" + ls_member_no + "%') ";
            }
            if ((ls_member_name != "" || ls_member_surname != "" || ls_card_person != "") && ls_member_no != "")
            {
                ls_sqlext += " AND ";
            }
            if (ls_member_name.Length > 0)
            {
                ls_sqlext += " ( DPDEPTEXTRAMEMBER.DEPTMEM_NAME like '" + ls_member_name + "%') ";
            }
            if ((ls_member_surname != "" || ls_card_person != "") && (ls_member_no != "" || ls_member_name != ""))
            {
                ls_sqlext += " AND ";
            }
            if (ls_member_surname.Length > 0)
            {
                ls_sqlext += " ( DPDEPTEXTRAMEMBER.DEPTMEM_SURNAME like '" + ls_member_surname + "%') ";
            }
            if (ls_card_person.Length > 0)
            {
                ls_sqlext += " ( DPDEPTEXTRAMEMBER.DEPTMEM_TAXID like '" + ls_card_person + "%') ";
            }

            if (ls_sqlext == null) ls_sqlext = "";
            if (ls_sqlext != "") ls_sqlext = " WHERE" + ls_sqlext;
            ls_sql = @"
                 SELECT DPDEPTEXTRAMEMBER.DEPTMEM_ID,   
                        DPDEPTEXTRAMEMBER.DEPTMEM_NAME,   
                        DPDEPTEXTRAMEMBER.DEPTMEM_SURNAME,   
                        DPDEPTEXTRAMEMBER.DEPTMEM_TAXID  
                FROM DPDEPTEXTRAMEMBER";
            ls_temp = ls_sql + ls_sqlext;
            try
            {
                DwList.SetSqlSelect(ls_temp);
                DwList.Retrieve();
            }
            catch (Exception )
            {
            }

        }
        
        #region WebDialog Members

        public void InitJsPostBack()
        {
            postExtramem = WebUtil.JsPostBack(this, "postExtramem");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            DwData.SetTransaction(sqlca);
            DwList.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                DwData.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwData);
                this.RestoreContextDw(DwList);
            }
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postExtramem")
            {
                JspostExtramem();
            }
        }


        public void WebDialogLoadEnd()
        {
            DwData.SaveDataCache();
            DwList.SaveDataCache();
        }

        #endregion
    }
}
