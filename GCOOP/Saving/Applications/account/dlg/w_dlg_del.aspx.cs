using System;
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
using CoreSavingLibrary;

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_del : PageWebDialog, WebDialog
    {
        protected String jsPostSearch;
        string pbl = "asset.pbl";

        #region WebDialog Members
        public void InitJsPostBack()
        {
            jsPostSearch = WebUtil.JsPostBack(this, "jsPostSearch");
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                DwHead.InsertRow(0);
                Search();
            }
            else
            {
                this.RestoreContextDw(DwHead);
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            { 
                case "jsPostSearch":
                    Search();
                    break;
            }
        }

        public void WebDialogLoadEnd()
        {
            DwHead.SaveDataCache();
            DwMain.SaveDataCache();
        }
        #endregion

        #region Function
        private void Search()
        {
            string asset_doc = "%";
            string desc_text = "%";
            try { asset_doc += DwHead.GetItemString(1, "asset_doc") + "%"; }
            catch { asset_doc = "%"; }
            try { desc_text += DwHead.GetItemString(1, "desc_text") + "%"; }
            catch { desc_text = "%"; }
            DwUtil.RetrieveDataWindow(DwMain, pbl, null, state.SsCoopId, asset_doc, desc_text);
            Filter();
        }

        protected void Radio1_CheckChanged(object sender, EventArgs e)
        {
            //if (Radio1.Checked)
            //{
            //    //Radio2.Checked = false;
            //}
            //Filter();
        }

        protected void Radio2_CheckChanged(object sender, EventArgs e)
        {
            //if (Radio2.Checked)
            //{
            //    Radio1.Checked = false;
            //}
            //Filter();
        }

        private void Filter()
        {
            if (Radio1.Checked)
            {
                DwMain.SetFilter("type_of_caldp = 1");
                DwMain.Filter();
            }
            else
            {
                DwMain.SetFilter("type_of_caldp <> 1");
                DwMain.Filter();
            }
        }
        #endregion
    }
}