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
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNShrlon;
using DataLibrary;

namespace Saving.Applications.shrlonclsproc
{
    public partial class w_dlg_sl_closeyear_wizard : PageWebSheet, WebSheet
    {

        #region WebSheet Members

        protected String postCloseYear;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String clearProcess;

        public void InitJsPostBack()
        {
            postCloseYear = WebUtil.JsPostBack(this, "postCloseYear");
            clearProcess = WebUtil.JsPostBack(this, "clearProcess");
        }

        public void WebSheetLoadBegin()
        {
            HdRunProcess.Value = "false";
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();

            if (IsPostBack)
            {

                try
                {
                    dw_criteria.RestoreContext();

                }
                catch { }

            }
            if (dw_criteria.RowCount < 1)
            {
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDecimal(1, "close_year", Convert.ToInt16(state.SsWorkDate.Year) + 543);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCloseYear")
            {
                JsPostCloseYear();
            }
            if (eventArg == "clearProcess")
            {
                JsClearProcess();
            }

        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        private void JsPostCloseYear()
        {
            try
            {

                short ai_year = (short)Convert.ToInt16(dw_criteria.GetItemString(1, "close_year"));
                String as_branch = state.SsCoopId;
                String as_entryid = state.SsUsername;
                shrlonService.RunCloseyearProcess(state.SsWsPass, ai_year, as_branch, as_entryid, state.SsApplication, state.CurrentPage);
                HdRunProcess.Value = "true";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        private void JsClearProcess()
        {           
            dw_criteria.Reset();
            dw_criteria.InsertRow(0);
            dw_criteria.SetItemDecimal(1, "close_year", Convert.ToInt16(state.SsWorkDate.Year) + 543);
        }

        #endregion
    }
}
