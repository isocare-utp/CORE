using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_grpmangrtpermiss_ctrl
{
    public partial class ws_sl_grpmangrtpermiss : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostRetrieve { get; set; }
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDelRow { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            dsMain.Retrieve();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostRetrieve")
            {
                string mangrtpermgrp_code = dsMain.DATA[0].mangrtpermgrp_code;
                dsList.Retrieve(mangrtpermgrp_code);
                dsDetail.Retrieve(mangrtpermgrp_code);
            }
            else if (eventArg == "PostInsertRow")
            {
                dsList.InsertLastRow();

                int r = dsList.RowCount - 1;
                dsList.DATA[r].COOP_ID = state.SsCoopControl;
                dsList.DATA[r].SEQ_NO = dsList.RowCount;
                dsList.DATA[r].MANGRTPERMGRP_CODE = dsMain.DATA[0].mangrtpermgrp_code;

            }
            else if (eventArg == "PostDelRow")
            {
                int r = dsList.GetRowFocus();
                dsList.DeleteRow(r);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddFormView(dsDetail,ExecuteType.Update);
                exe.AddRepeater(dsList);
                exe.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}