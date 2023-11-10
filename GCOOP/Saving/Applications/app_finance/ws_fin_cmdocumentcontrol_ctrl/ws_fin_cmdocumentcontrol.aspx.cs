using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_cmdocumentcontrol_ctrl
{
    public partial class ws_fin_cmdocumentcontrol : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostSave { get; set; }
        private n_commonClient ncommon;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSave)
            {
                try
                {
                    // ls_finslipno = ncommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNRECEIVENO");
                    string sql = @"update cmdocumentcontrol set last_documentno=last_documentno+1 where DOCUMENT_CODE='FNRECEIVENO' and coop_id={0}";
                    sql = WebUtil.SQLFormat(sql,state.SsCoopId);
                    WebUtil.ExeSQL(sql);
                    LtServerMessage.Text = WebUtil.CompleteMessage("อัพเดทเลขเอกสารสำเร็จ สามารถทำรายการรับจ่ายได้");
                }
                catch {
                    LtServerMessage.Text = WebUtil.ErrorMessage("อัพเดทเลขเอกสารไม่สำเร็จ ไม่สามารถทำรายการรับจ่ายได้"); return;
                }
            }
        }

        public void SaveWebSheet()
        {
            throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }
    }
}