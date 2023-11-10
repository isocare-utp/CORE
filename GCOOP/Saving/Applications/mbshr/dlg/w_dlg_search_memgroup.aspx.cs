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


namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_search_memgroup : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        string ls_sqlcoop = "";
        String ls_sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_data.SetTransaction(SQLCA);
            dw_detail.SetTransaction(SQLCA);
            ls_sql = dw_detail.GetSqlSelect();

            ls_sqlcoop = "and (  MBUCFMEMBGROUP_A.COOP_ID = '" + state.SsCoopControl + "') ";
            if (!IsPostBack)
            {
                dw_data.InsertRow(0);
            }
            else
            {
                dw_detail.RestoreContext();
                
                
            }
            if (!hidden_search.Value.Equals(""))
            {      //aek
                //dw_detail.SetSqlSelect(hidden_search.Value);
                //dw_detail.Retrieve();
                string ls_sqlext = "", ls_temp = "";

                //ls_sql = dw_detail.GetSqlSelect();
                //ls_sqlext = ls_sqlcoop;
                //ls_temp = ls_sql + ls_sqlext;
                //hidden_search.Value = ls_temp;
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

            String ls_group_no = "", ls_group_desc = "";
            String ls_sqlext = "", ls_temp = "";
            //ls_sql = dw_detail.GetSqlSelect();
            
           // ls_sql = "  SELECT MBUCFMEMBGROUP.MEMBGROUP_CODE,   MBUCFMEMBGROUP.MEMBGROUP_DESC  FROM MBUCFMEMBGROUP ";

            try
            {
                ls_group_no = dw_data.GetItemString(1, "group_no").Trim();

            }
            catch { ls_group_no = ""; }

            try
            {
                ls_group_desc = dw_data.GetItemString(1, "group_desc").Trim();

            }
            catch { ls_group_desc = ""; }


            if (ls_group_no.Length > 0)
            {
                ls_sqlext = "  AND ( MBUCFMEMBGROUP_A.MEMBGROUP_CODE like '" + ls_group_no + "%') ";

            }

            if (ls_group_desc.Length > 0)
            {
                ls_sqlext = " AND (  (MBUCFMEMBGROUP_A.MEMBGROUP_DESC  like '%" + ls_group_desc + "%') OR (MBUCFMEMBGROUP_B.MEMBGROUP_DESC  like '%" + ls_group_desc + "%')) ";

            }


            ls_temp = ls_sql + ls_sqlcoop + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();

        }
    }
}

