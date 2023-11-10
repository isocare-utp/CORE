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
using Sybase.DataWindow;


namespace Saving.Applications.shrlon
{
    public partial class w_dlg_confirm_rcvproc_wizard : PageWebSheet, WebSheet
    {

        protected String typeProcStatus;
        protected String jsProcess;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        private DwThDate tdwcriteria;

        public void InitJsPostBack()
        {
            jsProcess = WebUtil.JsPostBack(this, "jsProcess");
            tdwcriteria = new DwThDate(dw_criteria, this);
            tdwcriteria.Add("balance_date", "balance_tdate");
            typeProcStatus = WebUtil.JsPostBack(this, "typeProcStatus");
        }

        public void WebSheetLoadBegin()
        {
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
                dw_criteria.SetItemDate(1, "proc_date", state.SsWorkDate);
                tdwcriteria.Eng2ThaiAllRow();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsProcess")
            {
                JsProcess();
            }
            if (eventArg == "typeProcStatus")
            {
                TypeProcStatus();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            tdwcriteria.Eng2ThaiAllRow();
        }

        private void JsProcess()
        {

            try
            {
                String entry_id = state.SsUsername;
                String ls_xmldwcriteria = dw_criteria.Describe("DataWindow.Data.XML");
                //shrlonService.RunCfbalProcess(state.SsWsPass, ls_xmldwcriteria, entry_id, state.SsApplication, state.CurrentPage);
                //int result = shrlonService.ProcConfirmBalance(state.SsWsPass, ls_xmldwcriteria, entry_id);
                //if (result == 1)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("เรียบร้อย");
                //    dw_criteria.Reset(); dw_criteria.InsertRow(0);
                //}
                //else { LtServerMessage.Text = WebUtil.CompleteMessage("ไม่เรียบร้อย"); }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void TypeProcStatus()
        {

        }
    }
}
