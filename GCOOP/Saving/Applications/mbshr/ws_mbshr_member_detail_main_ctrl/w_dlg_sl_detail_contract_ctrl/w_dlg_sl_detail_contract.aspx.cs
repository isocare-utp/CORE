using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_sl_detail_contract_ctrl
{
    public partial class w_dlg_sl_detail_contract : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsData.InitDsData(this);
            dsStatement.InitDsStatement(this);
            dsCollateral.InitDsCollateral(this);
            dsChgpay.InitDsChgpay(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                try
                {
                    string ls_lcontno = Request["lcontno"];
                    dsMain.RetrieveMain(ls_lcontno);
                    dsData.RetrieveData(ls_lcontno);
                    dsStatement.RetrieveStatement(ls_lcontno);
                    dsCollateral.RetrieveCollateral(ls_lcontno);
                    dsChgpay.RetrieveChgpay(ls_lcontno);
                    dsMain.DdLoanType();
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