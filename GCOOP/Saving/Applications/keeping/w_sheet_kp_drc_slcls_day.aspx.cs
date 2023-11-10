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
using Sybase.DataWindow;
using DataLibrary;
using CoreSavingLibrary.WcfNKeeping;
using System.Web.Services.Protocols;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_drc_slcls_day : PageWebSheet, WebSheet
    {
        //ประกาศตัวแปร
        #region Variable
        public string outputProcess;
        private n_keepingClient KeepingService;
        protected String postNewClear;
        protected String postRefresh;
        protected String postProcClsDay;
        private DwThDate tDwOption;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            tDwOption = new DwThDate(Dw_option, this);
            tDwOption.Add("clsday_date", "clsday_tdate");

            //=========================================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postProcClsDay = WebUtil.JsPostBack(this, "postProcClsDay");
        }

        public void WebSheetLoadBegin()
        {
            Hd_process.Value = "false";
            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_option);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            // Event ที่เกิดจาก JavaScript
            switch (eventArg)
            {
                case "postNewClear":
                    JspostNewClear();
                    break;
                case "postRefresh":
                    //Refresh();
                    break;
                case "postProcClsDay":
                    JspostProcClsDay();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            Dw_option.SaveDataCache();
        }
        #endregion
        //=============================================
        // function เคลียร์หน้าจอ
        private void JspostNewClear()
        {
            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemDate(1, "clsday_date", state.SsWorkDate);
            tDwOption.Eng2ThaiAllRow();
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);
            Dw_option.SetItemString(1, "entryby_coopid", state.SsCoopId);
            Dw_option.SetItemDate(1, "entry_date", DateTime.Now);
            Dw_option.SetItemString(1, "application", state.SsApplication);
        }


        private void JspostProcClsDay()
        {
            try
            {
                //KeepingService = wcf.NKeeping;
                //str_slcls_proc astr_slcls_proc = new str_slcls_proc();
                //astr_slcls_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                ////KeepingService.RunKpSlClsDayProcess(state.SsWsPass, ref astr_slcls_proc, state.SsApplication, state.CurrentPage);

                //Hd_process.Value = "true";
                string option_xml = Dw_option.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "LNCLOSEDAY", option_xml, "", "");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
        }
    }
}