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
    public partial class w_dlg_bank_and_branch : PageWebDialog, WebDialog
    {
        protected String postSelectBank;


        private void JsPostSelectBank()
        {
            int row = -1;
            try
            {
                row = int.Parse(HdBankRow.Value);
            }
            catch { }
            JsPostSelectBank(row);
        }

        private void JsPostSelectBank(int row)
        {
            String bankCode = DwBank.GetItemString(row, "bank_code");
            String bankDesc = DwBank.GetItemString(row, "bank_desc");
            Label1.Text = "รหัส " + bankCode + ": " + bankDesc + " - กรุณาเลือกสาขาธนาคาร";
            DwBranch.Reset();
            try
            {
                DwBranch.Retrieve(bankCode);
            }
            catch { }
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postSelectBank = WebUtil.JsPostBack(this, "postSelectBank");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            DwBank.SetTransaction(sqlca);
            DwBank.Retrieve();
            DwBranch.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                try
                {
                    HdSheetRow.Value = Request["sheetRow"].ToString();
                }
                catch { HdSheetRow.Value = ""; }
                DwBank.Retrieve();
            }
            else
            {
                DwBank.RestoreContext();
                try
                {
                    DwBranch.RestoreContext();
                }
                catch { }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSelectBank")
            {
                JsPostSelectBank();
            }
        }

        public void WebDialogLoadEnd()
        {
            if (!IsPostBack)
            {
                try
                {
                    String queryBank = Request["bankCodeQuery"];
                    if (queryBank != null && queryBank != "")
                    {
                        int row = DwBank.FindRow("bank_code='" + queryBank + "'", 1, DwBank.RowCount);
                        JsPostSelectBank(row);
                    }
                }
                catch { }
            }
        }

        #endregion
    }
}