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
using CoreSavingLibrary.WcfNAccount;

namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_vc_trn_loan : PageWebDialog, WebDialog
    {
        //private DwThDate tDwMain;
        private n_accountClient accService; //ประกาศเสมอ
        protected String postSlip;
        protected String PostCal;

        public void InitJsPostBack()
        {
            postSlip = WebUtil.JsPostBack(this, "postSlip");
            PostCal = WebUtil.JsPostBack(this, "PostCal");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();

            accService = wcf.NAccount;//ประกาศ new
            Dw_main.SetTransaction(sqlca);
            String queryStrVcDate = "";
            if (!IsPostBack)
            {
                try { queryStrVcDate = Request["vcDate"].Trim(); }
                catch { }
                if (queryStrVcDate != "")
                {
                    JspostSlip();
                }
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }

        }


        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSlip")
            {
                JspostSlip();
            }

        }

        public void WebDialogLoadEnd()
        {
            Dw_main.SaveDataCache();
        }

        private void JspostSlip()
        {
            String queryStrVcDate = "";
            try { queryStrVcDate = Request["vcDate"].Trim(); }
            catch { }
            DateTime vcDate = DateTime.ParseExact(queryStrVcDate, "ddMMyyyy", new CultureInfo("th-TH"));
            DwUtil.RetrieveDataWindow(Dw_main, "vc_genwizard_vcdate.pbl", null, vcDate, state.SsCoopControl);
        }
    }
}
