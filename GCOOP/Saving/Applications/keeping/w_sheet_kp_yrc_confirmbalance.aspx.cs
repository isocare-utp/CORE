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

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_yrc_confirmbalance : PageWebSheet, WebSheet
    {
        protected String postRefresh;
        protected String postNewClear;
        protected String getXml;
        private n_shrlonClient shService;
        private DwThDate tDwMain;
        private n_commonClient commonService;
        public string outputProcess;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            getXml = WebUtil.JsPostBack(this, "getXml");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");

            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("proc_date", "proc_tdate");
            tDwMain.Add("balance_date", "balance_tdate");
        }

        public void WebSheetLoadBegin()
        {
            HdRunProcess.Value = "false";
            shService = wcf.NShrlon;
            commonService = wcf.NCommon;
            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postRefresh")
            {

            }
            if (eventArg == "getXml")
            {
                String xml_tmp = DwMain.Describe("DataWindow.Data.Xml");
                //เรียกเว็บเซอร์วิสประมวลผลและจัดเก็บ
                CallWSRunRcvProcess(xml_tmp);
            }
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
        }
        
        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            tDwMain.Eng2ThaiAllRow();
        }

        #endregion

        private void CallWSRunRcvProcess(string xml)
        {
            try
            {
                //str_yrcfbal_proc astr_yrcfbal_proc = new str_yrcfbal_proc();
                //astr_yrcfbal_proc.xml_option = xml;
                //shService.RunSlyrconfirmbalance(state.SsWsPass, ref astr_yrcfbal_proc, state.SsApplication, state.CurrentPage);//RunSlyrconfirmbalance
                //HdRunProcess.Value = "true";
                string option_xml = DwMain.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "CONFIRMBAL", option_xml, "", "");
            }
            catch (Exception e)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e);
            }
        }

        protected void cb_process_Click(object sender, EventArgs e)
        {
            String xml_tmp = DwMain.Describe("DataWindow.Data.Xml");
            //เรียกเว็บเซอร์วิสประมวลผลและจัดเก็บ
            CallWSRunRcvProcess(xml_tmp);

        }
        private void JspostNewClear()
        {
            DwMain.Reset();
            DwMain.InsertRow(0);
            DwMain.SetItemString(1, "coop_id", state.SsCoopId);
            DwMain.SetItemString(1, "entry_id", state.SsUsername);
            DwMain.SetItemDateTime(1, "proc_date", state.SsWorkDate);
            DwMain.SetItemDateTime(1, "balance_date", state.SsWorkDate);
        }
    }
}