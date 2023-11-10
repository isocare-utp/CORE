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
using CoreSavingLibrary.WcfNDeposit;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_closeday : PageWebDialog, WebDialog
    {
        #region WebDialog Members

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            LtServerMessage.Text = WebUtil.WarningMessage("ห้ามปิดหน้าจอในขณะการประมวลผลยังทำงาน");
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
