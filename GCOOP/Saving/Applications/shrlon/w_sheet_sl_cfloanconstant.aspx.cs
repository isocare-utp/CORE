using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_cfloanconstant : PageWebSheet, WebSheet
    {
        protected String itemChanged;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            itemChanged = WebUtil.JsPostBack(this, "itemChanged");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            dw_main.Retrieve(state.SsCoopId);

            if (IsPostBack)
            {
               // this.RestoreContextDw(dw_main);
                dw_main.RestoreContext();
            }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(1);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "itemChanged") { 
                //refresh page to action for another field
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                dw_main.SetItemString(1, "coop_id", state.SsCoopControl);
                dw_main.SetItemDecimal(1, "coop_no", 1);
                dw_main.UpdateData();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
        }

        #endregion
    }
}
