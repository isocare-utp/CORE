using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_ln_remarkstatus : PageWebDialog, WebDialog
    {
        WebState state;
        DwTrans SQLCA;
        String pbl = "sl_loan_requestment_cen.pbl";
        public void InitJsPostBack()
        {
            
        }

        public void WebDialogLoadBegin()
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            DwMain.SetTransaction(SQLCA);

            if (!IsPostBack)
            {
                String MemberNo = Request["MemberNo"].ToString();
                MemberNo = WebUtil.MemberNoFormat(MemberNo);
                DwUtil.RetrieveDataWindow(DwMain, pbl, null, MemberNo);
            }
            else
            {
                DwMain.RestoreContext();

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            throw new NotImplementedException();
        }

        public void WebDialogLoadEnd()
        {
            SQLCA.Disconnect();
        }
    }
}