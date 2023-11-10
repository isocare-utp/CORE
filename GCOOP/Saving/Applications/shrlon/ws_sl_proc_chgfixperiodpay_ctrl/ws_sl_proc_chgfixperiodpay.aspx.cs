using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_proc_chgfixperiodpay_ctrl
{
    public partial class ws_sl_proc_chgfixperiodpay : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String Post { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].date = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "Post")
            {
                try
                {
                                        
                    wcf.NShrlon.of_proc_chgfixperiodpay(state.SsWsPass, dsMain.DATA[0].date, state.SsUsername); // of_initlntrnres(state.SsWsPass, ref astr_lntrnrespons);

                    LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลสำเร็จ");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
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