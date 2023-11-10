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
    public partial class w_dlg_bank_list : PageWebDialog, WebDialog
    {

        #region WebDialog Members

        public void InitJsPostBack()
        {
            
        }

        public void WebDialogLoadBegin()
        {
            String Bank, BankBranch;
            this.ConnectSQLCA();
            DwBankList.SetTransaction(sqlca);

            Bank = Request["bank"].ToString();
            BankBranch = Request["bankbranch"].ToString();

            DwBankList.Retrieve(Bank, BankBranch);
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
