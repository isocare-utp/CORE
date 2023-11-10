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

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_bankstatement : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
        d_banklist_statement.SetTransaction(SQLCA);
        String account_no, bank_code, bankbranch_code, coopbranch_id;
        try
        {
            account_no = Request["account_no"];
            bank_code = Request["bank_code"];
            bankbranch_code = Request["bankbranch_code"];
            coopbranch_id = Request["coopbranch_id"];

        }
        catch { account_no = ""; bank_code = ""; bankbranch_code = ""; coopbranch_id = ""; }

            if (!IsPostBack)
            {
                d_banklist_statement.Retrieve(bank_code, bankbranch_code, account_no, coopbranch_id);
            }

        }

        protected void Page_LoadComplete()
        {
            SQLCA.Disconnect();
        }
    }
}
