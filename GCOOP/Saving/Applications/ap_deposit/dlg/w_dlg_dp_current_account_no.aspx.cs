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
    public partial class w_dlg_dp_current_account_no : PageWebDialog,WebDialog
    {
        protected String postUpdate;
        #region WebDialog Members

        public void InitJsPostBack()
        {
            postUpdate = WebUtil.JsPostBack(this, "postUpdate");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            HdCloseDlg.Value = "false";
            if (!IsPostBack)
            {
                DwMain.Retrieve(state.SsCoopControl);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postUpdate")
            {
                try
                {
                    DwMain.UpdateData();
                    HdCloseDlg.Value = "true";
                }
                catch (Exception)
                {
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion
    }
}
