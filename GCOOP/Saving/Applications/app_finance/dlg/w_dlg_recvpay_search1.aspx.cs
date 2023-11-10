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
using Saving;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNFinance;
using System.Web.Services.Protocols;


namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_recvpay_search1 : PageWebDialog, WebDialog
    {
        n_financeClient fin;
        protected String jsSearch;
        protected String sqlSearch;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");


        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            fin = wcf.NFinance;
            DwMain.SetTransaction(sqlca);
            sqlSearch = DwMain.GetSqlSelect();

            if (!IsPostBack)
            {


                sqlSearch += " WHERE ";
                sqlSearch += " FINUCFITEMTYPE.COOP_ID = '" + state.SsCoopControl.Trim() + "'";
                DwMain.SetSqlSelect(sqlSearch);
                DwMain.Retrieve();

                DwSearch.Reset();
                DwSearch.InsertRow(0);

            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwSearch);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsSearch") { Search(); }

        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
            DwSearch.SaveDataCache();
        }

        private void Search()
        {

            String des, code;

            sqlSearch += " WHERE ";
            sqlSearch += " FINUCFITEMTYPE.COOP_ID = '" + state.SsCoopControl.Trim() + "'";


            try { des = DwSearch.GetItemString(1, "item_desc"); }
            catch { des = ""; }
            try
            {
                code = DwSearch.GetItemString(1, "slipitemtype_code");
            }
            catch { code = ""; }

            //String sqlSearch = DwMain.GetSqlSelect();

            if (des.Length > 0)
            {
                sqlSearch += " and ";
                sqlSearch += " FINUCFITEMTYPE.ITEM_DESC like '%" + des.Trim() + "%'";
            }
            if (code.Length > 0)
            {
                sqlSearch += " and ";
                if (des.Length <= 0)
                {
                    sqlSearch += " FINUCFITEMTYPE.SLIPITEMTYPE_CODE like '%" + code.Trim() + "%'";
                }
                else { sqlSearch += " AND FINUCFITEMTYPE.SLIPITEMTYPE_CODE like '%" + code.Trim() + "%'"; }
            }
            DwMain.SetSqlSelect(sqlSearch);
            DwMain.Retrieve();
            DwSearch.Reset();
            DwSearch.InsertRow(0);
        }

        #endregion
    }
}
