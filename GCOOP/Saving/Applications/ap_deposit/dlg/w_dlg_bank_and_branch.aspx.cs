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
                object[] args = new object[] { bankCode };
                DwUtil.RetrieveDataWindow(DwBranch, "dp_slip.pbl", null, args);
            }
            catch (Exception ex)
            {
            }
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postSelectBank = WebUtil.JsPostBack(this, "postSelectBank");
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                DwUtil.RetrieveDataWindow(DwBank, "dp_slip.pbl", null, null);
                try
                {
                    HdSheetRow.Value = Request["sheetRow"].ToString().Trim();
                }
                catch { HdSheetRow.Value = ""; }
                try
                {
                    String bankCode = Request["bankCodeQuery"].ToString().Trim();
                    if (!string.IsNullOrEmpty(bankCode))
                    {
                        object[] args = new object[] { bankCode };
                        DwUtil.RetrieveDataWindow(DwBranch, "dp_slip.pbl", null, args);
                    }
                }
                catch { }
            }
            else
            {
                this.RestoreContextDw(DwBank);
                this.RestoreContextDw(DwBranch);
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
            DwBank.SaveDataCache();
            DwBranch.SaveDataCache();
        }

        #endregion
    }
}