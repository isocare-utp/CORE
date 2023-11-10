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
using CoreSavingLibrary.WcfNShrlon;
using System.Globalization;

namespace Saving.Applications.shrlon
{
    public partial class w_dlg_sl_setintarrear_wizard : PageWebDialog, WebDialog
    {
        protected String jsprocintset;
        private n_shrlonClient slService;
        private DwThDate tdw_main; 

        #region WebDialog Members

        public void InitJsPostBack()
        {
            jsprocintset = WebUtil.JsPostBack(this, "jsprocintset");
            tdw_main = new DwThDate(dw_main, this);
            tdw_main.Add("calintto_date", "calintto_tdate");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_loantype.SetTransaction(sqlca);
            dw_main.Reset();
            dw_main.InsertRow(0);
            dw_loantype.Reset();
            dw_loantype.Retrieve();
            dw_main.SetItemDate( 1, "intproc_date", state.SsWorkDate);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsprocintset") {
                CallProcIntSetProcess();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion

        private void CallProcIntSetProcess()
        {
            String xml_tmp = dw_main.Describe("DataWindow.Data.Xml");
            //call webservice
        }

    }
}
