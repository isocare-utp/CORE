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
using System.Globalization;

namespace Saving.Applications.shrlon.ws_sl_proc_moneyreturn_ctrl
{
    public partial class ws_sl_proc_moneyreturn : PageWebSheet, WebSheet
    {
        public string outputProcess;

        [JsPostBack]
        public string PostProcMoneyreturn { get; set; }
        
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsMain.DdSloantype();
            dsMain.DdEloantype();
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].RETURN_DATE = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostProcMoneyreturn")
            {
                ProcMoneyreturn();
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }

        private void ProcMoneyreturn()
        {
            string option_xml = dsMain.ExportXml();
            outputProcess = WebUtil.runProcessing(state, "PROCINTRETURN", option_xml, "", "");
        }
    }
}