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
    public partial class w_dlg_running_progressbar :PageWebDialog, WebDialog
    {

        #region WebDialog Members

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            HdProcessApplication.Value = Request["application"];
            HdProcessWebSheetId.Value = Request["w_sheet_id"];
            try
            {
                if (Request["warn"] == "true")
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ห้ามปิดหน้าจอในขณะการประมวลผลยังทำงาน");
                }
            }
            catch { LtServerMessage.Text = ""; }
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
