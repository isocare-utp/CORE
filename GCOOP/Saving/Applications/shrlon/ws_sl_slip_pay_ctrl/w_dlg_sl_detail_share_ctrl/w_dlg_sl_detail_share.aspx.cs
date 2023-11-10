using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_detail_share_ctrl
{
    public partial class w_dlg_sl_detail_share : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsPayment.InitDsPayment(this);
            dsStatement.InitDsStatement(this);

        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                try
                {
                    string ls_memno = Request["memno"];
                    string ls_sharetype = Request["shrtype"];
                    dsMain.RetrieveMain(ls_memno, ls_sharetype);
                    dsPayment.RetrievePayment(ls_memno);
                    dsStatement.RetrieveStatement(ls_memno, ls_sharetype);
                }
                catch { }
            }


        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {

        }
    }
}