using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_cmdocumentcontrol_reset_ctrl
{
    public partial class ws_fin_cmdocumentcontrol_reset : PageWebSheet, WebSheet
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
                    String sql = "update cmprocessing set runtime_status ='1' where runtime_status<>1  and object_name='POSTTOFIN' and coop_id={0}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                    WebUtil.ExeSQL(sql);
                    LtServerMessage.Text = WebUtil.CompleteMessage("update  cmprocessing สำเร็จ");
                }
                catch { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถ update  cmprocessingได้"); }
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