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
    public partial class w_dlg_confirm_rcvproc_wizard : PageWebSheet, WebSheet
    {

        protected String jsRandomType;
        protected String jsRangeType;
        protected String jsProcess;
        protected String clearProcess;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        private DwThDate tdwcriteria;

        public void InitJsPostBack()
        {
            tdwcriteria = new DwThDate(dw_criteria, this);
            tdwcriteria.Add("balance_date", "balance_tdate");
            tdwcriteria.Add("proc_date", "proc_tdate");

            jsProcess = WebUtil.JsPostBack(this, "jsProcess");
            jsRandomType = WebUtil.JsPostBack(this, "jsRandomType");
            jsRangeType = WebUtil.JsPostBack(this, "jsRangeType");
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
                this.RestoreContextDw(dw_criteria, tdwcriteria);
            }
            else
            {
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDate(1, "proc_date", state.SsWorkDate);
                dw_criteria.SetItemDate(1, "balance_date", state.SsWorkDate);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsProcess":
                    JsProcess();
                    break;
                case "jsRangeType":
                    RangeType();
                    break;
                case "jsRandomType":
                    RandomType();
                    break;
                case "clearProcess":
                    JsClearProcess();
                    break;
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
            tdwcriteria.Eng2ThaiAllRow();
        }

        private void JsProcess()
        {

            try
            {
                String entry_id = state.SsUsername;
                String ls_xmldwcriteria = dw_criteria.Describe("DataWindow.Data.XML");
                shrlonService.RunCfConfirmBal(state.SsWsPass, ls_xmldwcriteria, state.SsApplication, state.CurrentPage);
                HdRunProcess.Value = "true";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void RandomType()
        {
            try
            {
                decimal random_type = dw_criteria.GetItemDecimal(1, "random_type");
                string S_random_type = random_type.ToString().Trim();
                switch (S_random_type)
                {
                    case "0":
                        dw_criteria.SetItemDecimal(1, "random_percent", 0);
                        dw_criteria.SetItemString(1, "random_lastdigit", "");
                        break;
                    case "1":
                        dw_criteria.SetItemDecimal(1, "random_percent", 0);
                        dw_criteria.SetItemString(1, "random_lastdigit", "");
                        break;
                    case "2":
                        dw_criteria.SetItemDecimal(1, "random_percent", 0);
                        dw_criteria.SetItemString(1, "random_lastdigit", "");
                        break;
                    case "3":
                        dw_criteria.SetItemString(1, "random_lastdigit", "");
                        break;
                    case "4":
                        dw_criteria.SetItemDecimal(1, "random_percent", 0);
                        break;
                }
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void RangeType()
        {
            try
            {
                decimal range_type = dw_criteria.GetItemDecimal(1, "range_type");
                string S_range_type = range_type.ToString().Trim();
                switch (S_range_type)
                {
                    case "1":
                        dw_criteria.SetItemString(1, "mem_text", "");
                        dw_criteria.SetItemString(1, "grp_text", "");
                        break;
                    case "2":
                        dw_criteria.SetItemString(1, "mem_text", "");
                        break;
                    case "3":
                        dw_criteria.SetItemString(1, "grp_text", "");
                        break;
                }
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
            dw_criteria.SetItemDate(1, "proc_date", state.SsWorkDate);
            dw_criteria.SetItemDate(1, "balance_date", state.SsWorkDate);
        }
    }
}
