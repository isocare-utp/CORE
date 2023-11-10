using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNDeposit;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_addapv_task : PageWebDialog,WebDialog
    {
        
        private n_depositClient depService;
        protected String postCheckApv;

        private void JsPostCheckApv()
        {
            String processId = Request["processId"];
            try
            {
                String result = depService.of_check_approved(state.SsWsPass, processId, state.SsCoopId);
                if (result != "")
                {
                    HdValueCheckApv.Value = result;
                    HdNameApv.Value = result;
                    HdCheckApv.Value = "true";       
                }
                else
                {
                    HdCheckApv.Value = "false";
                }
            }
            catch (Exception)
            {

            }
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postCheckApv = WebUtil.JsPostBack(this, "postCheckApv"); 
        }

        public void WebDialogLoadBegin()
        {
            HdCheckApv.Value = "";
            HdDlgClose.Value = "";
            try
            {
                depService = wcf.NDeposit;
            }
            catch
            {

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postCheckApv")
            {
                JsPostCheckApv();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion

        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            String processId = Request["processId"];
            String avpCode = Request["avpCode"];      
            String itemType = Request["itemType"];
            decimal avpAmt = Convert.ToDecimal(Request["avpAmt"]);
            //depService.ApvPermiss(state.SsWsPass, processId, "0", avpAmt, state.SsClientIp, state.SsUsername, state.SsWorkDate, avpCode, itemType, state.SsCoopId);
            HdDlgClose.Value = "true";
        }
    }
}
