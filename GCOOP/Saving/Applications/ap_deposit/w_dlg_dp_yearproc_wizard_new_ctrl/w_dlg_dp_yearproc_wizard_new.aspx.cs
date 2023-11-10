using System;
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
using Sybase.DataWindow;
using System.Data.OracleClient;
using System.Globalization;
using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Web.Services.Protocols;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_dlg_dp_yearproc_wizard_new : PageWebSheet, WebSheet
    {
        // JavaSctipt PostBack
        protected String postCloseYear;
        private DwThDate tdw_closeyear;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            postCloseYear = WebUtil.JsPostBack(this, "postCloseYear");
        }

        public void WebSheetLoadBegin()
        {
            HdCloseyear.Value = "false";
            if (!IsPostBack)
            {
                int year = state.SsWorkDate.Year + 543;
                dsMain.DATA[0].CLOSEYEAR = year.ToString();

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCloseYear")
            {
                JsPostCloseYear();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion

        private void JsPostCloseYear()
        {
            n_depositClient depService = wcf.NDeposit;
            try
            {
                //depService.RunCloseYearProcess(state.SsWsPass, state.CurrentPage, Convert.ToInt16(dsMain.DATA[0].CLOSEYEAR), state.SsWorkDate, state.SsUsername, state.SsClientIp, state.SsApplication, state.SsCoopControl);
               depService.of_close_year(state.SsWsPass, Convert.ToInt16(dsMain.DATA[0].CLOSEYEAR), state.SsWorkDate, state.SsUsername, state.SsClientIp, state.SsApplication, state.SsCoopControl);
               LtServerMessage.Text = WebUtil.CompleteMessage("ปิดงานสิ้นปีเสร็จแล้ว");
                //HdCloseyear.Value = "true";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}