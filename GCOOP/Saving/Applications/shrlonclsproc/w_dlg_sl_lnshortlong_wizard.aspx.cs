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
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;

namespace Saving.Applications.shrlonclsproc
{
    public partial class w_dlg_sl_lnshortlong_wizard : PageWebSheet, WebSheet
    {

        #region WebSheet Members


        protected String jsProcess;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        private DwThDate tdwcriteria;
        protected String clearProcess;
        protected String postSetYear;



        public void InitJsPostBack()
        {
            jsProcess = WebUtil.JsPostBack(this, "jsProcess");
            postSetYear = WebUtil.JsPostBack(this, "postSetYear");

            tdwcriteria = new DwThDate(dw_criteria, this);
            tdwcriteria.Add("acc_start", "acc_tstart");
            tdwcriteria.Add("acc_end", "acc_tend");
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
                dw_criteria.SetItemDecimal(1, "acc_year", Convert.ToInt16(state.SsWorkDate.Year) + 543);
                GetDataStartEnd();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsProcess")
            {
                JsProcess();
            }
            if (eventArg == "clearProcess")
            {
                JsClearProcess();
            }
            else if (eventArg == "postSetYear")
            {
                GetDataStartEnd();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            if (dw_criteria.RowCount > 1)
            {
                dw_criteria.DeleteRow(dw_criteria.RowCount);
            }
            tdwcriteria.Eng2ThaiAllRow();
        }

        private void JsProcess()
        {

            try
            {
                String entry_id = state.SsUsername;
                String ls_xmldwcriteria = dw_criteria.Describe("DataWindow.Data.XML");
                shrlonService.RunLnShotLongProcess(state.SsWsPass, ls_xmldwcriteria, entry_id, state.SsApplication, state.CurrentPage);
                HdRunProcess.Value = "true";
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void JsClearProcess()
        {
            dw_criteria.Reset();
            dw_criteria.InsertRow(0);
            dw_criteria.SetItemDecimal(1, "acc_year", Convert.ToInt16(state.SsWorkDate.Year) + 543);
        }

        private void GetDataStartEnd()
        {
            int year = int.Parse(dw_criteria.GetItemString(1, "acc_year"));
            DateTime starDate = new DateTime(year - 543, 1, 1);
            DateTime endDate = new DateTime(year - 543, 12, 31);

            dw_criteria.SetItemDateTime(1, "acc_start", starDate);
            dw_criteria.SetItemDateTime(1, "acc_end", endDate);
            tdwcriteria.Eng2ThaiAllRow();
        }

        #endregion
    }
}
