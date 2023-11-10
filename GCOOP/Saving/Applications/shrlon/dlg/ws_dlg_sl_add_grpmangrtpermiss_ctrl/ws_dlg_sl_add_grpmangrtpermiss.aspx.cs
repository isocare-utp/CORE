using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary.WcfNShrlon;

namespace Saving.Applications.shrlon.dlg.ws_dlg_sl_add_grpmangrtpermiss_ctrl
{
    public partial class ws_dlg_sl_add_grpmangrtpermiss : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostSave { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);

        }

        public void WebDialogLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostSave")
            {
                try
                {
                    dsMain.DATA[0].COOP_ID = state.SsCoopControl;

                    ExecuteDataSource insert = new ExecuteDataSource(this);
                    insert.AddFormView(dsMain, ExecuteType.Insert);
                    insert.Execute();
                    this.SetOnLoadedScript("parent.GetValueFromDlg(" + dsMain.DATA[0].MANGRTPERMGRP_CODE + ");");
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