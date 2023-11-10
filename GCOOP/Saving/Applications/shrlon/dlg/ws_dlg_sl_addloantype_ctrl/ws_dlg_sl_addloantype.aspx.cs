using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.shrlon.dlg.ws_dlg_sl_addloantype_ctrl
{
    public partial class ws_dlg_sl_addloantype : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostSave { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {
            dsMain.Ddloantype();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostSave")
            {
                try
                {
                    dsMain.DATA[0].coop_id = state.SsCoopControl;
                    String xml = dsMain.ExportXml();
                    int result = wcf.NShrlon.of_savenewlntype(state.SsWsPass, xml);
                    if (result == 1)
                    {
                        this.SetOnLoadedScript("parent.GetValueFromDlg(" + dsMain.DATA[0].loantype_code + ");");
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex);
                }
            }

        }

        public void WebDialogLoadEnd()
        {

        }
    }
}