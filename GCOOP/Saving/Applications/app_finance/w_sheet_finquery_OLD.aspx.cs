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
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finquery_OLD : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private String pbl = "finquery.pbl";
        DataStore DStore;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            fin = wcf.NFinance;
            DStore = new DataStore();
            DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\app_finance\finquery.pbl";
            DStore.DataWindowObject = "d_fn_showuserfdet";

            if (!IsPostBack)
            {
                DwMain.SetTransaction(sqlca);
                DwMain.Retrieve(state.SsCoopId, state.SsWorkDate);
            }
            else
            {

            }

        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }

        #endregion

    }
}
