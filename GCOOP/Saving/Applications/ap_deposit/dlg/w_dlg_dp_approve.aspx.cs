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
    public partial class w_dlg_dp_approve : PageWebDialog,WebDialog
    {
        string itemType;
        string accountNo;
        string nameReq;
        #region WebDialog Members

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                DwMain.Reset();
                itemType = Request["itemType"];
                accountNo = Request["accountNo"];
                nameReq = Request["nameReq"];
                DwMain.InsertRow(0);
                DwMain.SetItemString(1, "as_text", itemType);
                DwMain.SetItemString(1, "acc_no", accountNo);
                DwMain.SetItemString(1, "req_id", nameReq);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion
    }
}
