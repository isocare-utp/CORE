using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_proc_paymoneyreturn_ctrl
{
    public partial class ws_sl_proc_paymoneyreturn : PageWebSheet, WebSheet
    {
        public string outputProcess;

        [JsPostBack]
        public string PostProcPayMoneyreturn { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].PAYMRT_DATE = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostProcPayMoneyreturn")
            {
                ProcPayMoneyreturn();
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }

        private void ProcPayMoneyreturn()
        {
            string option_xml = dsMain.ExportXml();
            outputProcess = WebUtil.runProcessing(state, "POSTINTRETURNDEP", option_xml, "", "");
        }
    }
}