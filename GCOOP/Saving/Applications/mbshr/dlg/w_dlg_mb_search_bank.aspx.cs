using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;


namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_mb_search_bank : PageWebDialog, WebDialog
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
            DwBank.SetTransaction(SQLCA);

            if (!IsPostBack)
            {
                String seach_key = Request["seach_key"].ToString();

                DwUtil.RetrieveDataWindow(DwBank, pbl, null, seach_key);
            }
            else
            {
                DwBank.RestoreContext();
                
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