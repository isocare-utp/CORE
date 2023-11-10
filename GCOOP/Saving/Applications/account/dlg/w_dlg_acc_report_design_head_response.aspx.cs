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

using System.Globalization;
using Sybase.DataWindow;
//using CoreSavingLibrary.WcfAccount;
using DataLibrary;
using System.Data.OracleClient;
//using System.Web.Services.Protocols;


namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_acc_report_design_head_response : PageWebDialog, WebDialog 
    {
        
        #region WebDialog Members

        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_list.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_list.InsertRow(0);
                Dw_list.Retrieve(state.SsCoopId);
            }
            else {
                Dw_list.RestoreContext();
            }
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
