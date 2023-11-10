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
    public partial class w_dlg_bankaccount_search : PageWebDialog, WebDialog
    {


        #region WebDialog Members

        public void InitJsPostBack()
        {
          
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            DwMain.Retrieve(state.SsCoopId);
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
