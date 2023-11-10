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
using DataLibrary;
using System.Data.OracleClient;
using System.Web.Services.Protocols;

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_template : PageWebDialog, WebDialog
    {
        protected String jsPostFilter;
        String pbl = "vc_voucher_edit.pbl";

        #region Webdialog Members
        public void InitJsPostBack()
        {
            jsPostFilter = WebUtil.JsPostBack(this, "jsPostFilter");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            Dwlist.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dwmain.InsertRow(0);
                Dwmain.SetItemString(1, "voucher_type", "JV");
                DwUtil.RetrieveDataWindow(Dwlist, pbl, null, Dwmain.GetItemString(1, "voucher_type"));
            }
            else
            {
                this.RestoreContextDw(Dwmain);
                this.RestoreContextDw(Dwlist);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostFilter":
                    Filter();
                    break;
            }
        }

        public void WebDialogLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(Dwmain, "voucher_type", pbl, null);
            }
            catch { }
            Dwmain.SaveDataCache();
            Dwlist.SaveDataCache();
        }
        #endregion

        #region Function
        private void Filter()
        {
            try
            {
                DwUtil.RetrieveDataWindow(Dwlist, pbl, null, Dwmain.GetItemString(1, "voucher_type"));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        #endregion
    }
}