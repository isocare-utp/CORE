using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loantype_detail : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_main.SetTransaction(SQLCA);
            dw_main.Retrieve("20");

            dw_detail.SetTransaction(SQLCA);
            dw_detail.Retrieve("20");
            dw_right.SetTransaction(SQLCA);
            dw_right.Retrieve("20");
            dw_custom.SetTransaction(SQLCA);
            dw_custom.Retrieve("20");
            dw_interest.SetTransaction(SQLCA);
            dw_interest.Retrieve("20");
            dw_reqgrt.SetTransaction(SQLCA);
            dw_reqgrt.Retrieve("20");
            dw_colluse.SetTransaction(SQLCA);
            dw_colluse.Retrieve("20");
            dw_objective.SetTransaction(SQLCA);
            dw_objective.Retrieve("20");
            dw_buyshr.SetTransaction(SQLCA);
            dw_buyshr.Retrieve("20");
            dw_period.SetTransaction(SQLCA);
            dw_period.Retrieve("20");
            dw_clear.SetTransaction(SQLCA);
            dw_clear.Retrieve("20");
            dw_pause.SetTransaction(SQLCA);
            dw_pause.Retrieve("20");
            dw_insurance.SetTransaction(SQLCA);
            dw_insurance.Retrieve("20");
            SQLCA.Disconnect();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            SQLCA.Disconnect();
        }
    }
}
