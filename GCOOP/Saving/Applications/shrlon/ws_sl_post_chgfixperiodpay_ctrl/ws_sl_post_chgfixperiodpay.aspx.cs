using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary;
using System.Globalization;

namespace Saving.Applications.shrlon.ws_sl_post_chgfixperiodpay_ctrl
{
    public partial class ws_sl_post_chgfixperiodpay : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String Post { get; set; }
        private CultureInfo th;

        public void InitJsPostBack()
        {
            
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                this.th = new CultureInfo("th-TH");
                DateTime dt_contadjust_date = state.SsWorkDate;
                post_date.Text = dt_contadjust_date.ToString("dd/MM/yyyy", th);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "Post")
            {
                try
                {
                     
                    wcf.NShrlon.of_post_chgfixperiodpay(state.SsWsPass);

                    LtServerMessage.Text = WebUtil.CompleteMessage("ผ่านรายการสำเร็จ");
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