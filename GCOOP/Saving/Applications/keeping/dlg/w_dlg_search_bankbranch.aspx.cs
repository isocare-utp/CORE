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

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_search_bankbranch : System.Web.UI.Page
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
                try
                {

                    String bank_code = Request["bank_code"];
                    if (bank_code == "")
                    {
                        dw_detail.Retrieve();
                    }
                    else
                    {

                        String sql_bank = "SELECT BANK_CODE,BRANCH_ID,BRANCH_NAME,BRANCH_AMPHUR,BRANCH_PROVINCE,FEE_STATUS,SERVICE_AMT FROM CMUCFBANKBRANCH WHERE  CMUCFBANKBRANCH.BANK_CODE = '" + bank_code + "'";
                        dw_detail.SetSqlSelect(hidden_search.Value + sql_bank);
                        dw_detail.Retrieve();

                    }
                }
                catch { }
            }
            if (!hidden_search.Value.Equals(""))
            {

                ////dw_detail.SetSqlSelect(hidden_search.Value);
                ////dw_detail.Retrieve();
            }

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            SQLCA.Disconnect();
        }

        protected void cb_find_Click(object sender, EventArgs e)
        {

            String ls_branch_no = "", ls_branch_desc = "",ls_bank_id="";
            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = "    SELECT BANK_CODE,BRANCH_ID,BRANCH_NAME,BRANCH_AMPHUR,BRANCH_PROVINCE,FEE_STATUS,SERVICE_AMT  FROM CMUCFBANKBRANCH    ";
            try
            {
                ls_branch_no = dw_data.GetItemString(1, "branch_id").Trim();

            }
            catch { ls_branch_no = ""; }

            try
            {
                ls_branch_desc = dw_data.GetItemString(1, "branch_name").Trim();

            }
            catch { ls_branch_desc = ""; }

            try
            {
                ls_bank_id = dw_data.GetItemString(1, "bank_code").Trim();

            }
            catch { ls_bank_id = ""; }

            
                if (ls_branch_no.Length > 0)
                {
                    ls_sqlext = "  WHERE ( BRANCH_ID like '" + ls_branch_no + "%') ";
                }

                if (ls_branch_desc.Length > 0)
                {
                    ls_sqlext += " WHERE (  BRANCH_NAME  like '%" + ls_branch_desc + "%') ";
                }
                if (ls_bank_id.Length > 0)
                {
                    ls_sqlext += " WHERE (  BANK_CODE  like '%" + ls_bank_id + "%') ";
                }
           

            ls_temp = ls_sql + ls_sqlext;
            hidden_search.Value = ls_temp;
            dw_detail.SetSqlSelect(hidden_search.Value);
            dw_detail.Retrieve();

        }
    }
    
}