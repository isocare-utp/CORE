using System;
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
using Saving.CmConfig;
using Saving.WsDeposit;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_printfirstpage : PageWebDialog,WebDialog
    {
        protected String printFirstPage;
        #region WebDialog Members

        public void InitJsPostBack()
        {
            printFirstPage = WebUtil.JsPostBack(this, "printFirstPage");
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                HdDeptAccountNo.Value = Request["deptAccountNo"].Trim();
                HdPassBookNo.Value = Request["deptPassBookNo"].Trim();
            }
            else { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "printFirstPage")
            {
                Deposit dep = new Deposit();
                String apvId = state.SsUsername;
                int normFlag = 1;
                dep.PrintBookFirstPage(state.SsWsPass, state.SsApplication, HdDeptAccountNo.Value, state.SsBranchId, state.SsUsername, HdPassBookNo.Value, "00", apvId, state.SsPrinterSet, normFlag, state.SsWorkDate);
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            Deposit dep = new Deposit();
            String apvId = state.SsUsername;
            int normFlag = 1;
            dep.PrintBookFirstPage(state.SsWsPass, state.SsApplication, HdDeptAccountNo.Value, state.SsBranchId, state.SsUsername, HdPassBookNo.Value, "00", apvId, state.SsPrinterSet, normFlag, state.SsWorkDate);
        }

    }
}
