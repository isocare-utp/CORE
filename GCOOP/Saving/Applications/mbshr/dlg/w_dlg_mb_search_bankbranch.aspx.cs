using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_mb_search_bankbranch : PageWebDialog, WebDialog
    {
        WebState state;
        DwTrans SQLCA;
        String pbl = "sl_member_new.pbl";

        public void InitJsPostBack()
        {

        }

        public void WebDialogLoadBegin()
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            DwBankBranch.SetTransaction(SQLCA);

            if (!IsPostBack)
            {
                String seach_key = Request["seach_key"].ToString();
                String bankCode = Request["bankCode"].ToString();

                DwUtil.RetrieveDataWindow(DwBankBranch, pbl, null,bankCode, seach_key);
            }
            else
            {
                DwBankBranch.RestoreContext();

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