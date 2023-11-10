using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.keeping.w_sheet_cm_constant_config
{
    public partial class u_lnucfcontracttype : System.Web.UI.UserControl
    {
        private DwTrans sqlca;
        private WebState state;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_main.SetTransaction(sqlca);
            dw_main.Retrieve();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            try
            {
                sqlca.Disconnect();
            }
            catch { }
        }

        protected void dw_main_BeginUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!state.IsWritable) { e.Cancel = true; sqlca.Rollback(); }
        }

        protected void dw_main_EndUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {

            if (e.RowsUpdated > 0 && state.IsWritable) { sqlca.Commit(); }
            else if (e.RowsInserted > 0 && state.IsWritable) { sqlca.Commit(); }
            else if (e.RowsDeleted > 0 && state.IsWritable) { sqlca.Commit(); }
            else { sqlca.Rollback(); }
        }
    }
}