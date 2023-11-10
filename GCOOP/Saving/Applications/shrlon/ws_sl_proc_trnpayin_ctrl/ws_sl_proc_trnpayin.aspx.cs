using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNShrlon;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_proc_trnpayin_ctrl
{
    public partial class ws_sl_proc_trnpayin : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public String PostCheck { get; set; }
        [JsPostBack]
        public String PostProc { get; set; }
        [JsPostBack]
        public String PostPrint { get; set; }
        public string outputProcess;
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }


        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].trans_date = state.SsWorkDate;
                dsMain.DATA[0].source_system = "DIV";
                dsMain.DATA[0].entry_id = state.SsUsername;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostCheck")
            {
                dsList.Retrieve(dsMain.DATA[0].source_system, dsMain.DATA[0].trans_date);
            }
            else if (eventArg == "PostProc")
            {
                try
                {
                    //str_proctrnpayin astr_proctrnpayin = new str_proctrnpayin();
                    //astr_proctrnpayin.source_code = dsMain.DATA[0].source_system;
                    //astr_proctrnpayin.trans_date = dsMain.DATA[0].trans_date;
                    //astr_proctrnpayin.entry_id = state.SsUsername;
                    string xml = dsMain.ExportXml();
                    //wcf.NShrlon.of_proc_trnpayin(state.SsWsPass, ref astr_proctrnpayin);
                    outputProcess = WebUtil.runProcessing(state, "LNPOSTTRNPAYIN", xml, "", "");
                    //Hdstartslipno.Value = astr_proctrnpayin.payinslip_startno;
                    //Hdendslipno.Value = astr_proctrnpayin.payinslip_endno;
                    LtServerMessage.Text = WebUtil.CompleteMessage("ผ่านรายการสำเร็จ");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if(eventArg == "PostPrint"){
                Printing.SlipNoPrintSlipSlpayin(this, Hdstartslipno.Value, Hdendslipno.Value, state.SsCoopControl); 
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}