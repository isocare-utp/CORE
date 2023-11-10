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

namespace Saving.Applications.keeping
{
    public partial class u_cmucfbank : System.Web.UI.UserControl
    {
        private DwTrans sqlca;
        private WebState state;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            DwMain.SetTransaction(sqlca);
            DwMain.Retrieve();
        }

        protected void Page_LoadComplete()
        {
            try
            {
                sqlca.Disconnect();
            }
            catch { }
        }

        protected void PreUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!state.IsWritable)
            {
                e.Cancel = true;
                sqlca.Rollback();
            }
        }

        protected void PostUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
            if (e.RowsUpdated > 0 && state.IsWritable)
            {
                sqlca.Commit();
            }
            else if (e.RowsInserted > 0 && state.IsWritable)
            {
                sqlca.Commit();
            }
            else if (e.RowsDeleted > 0 && state.IsWritable)
            {
                sqlca.Commit();
            }
            else
            {
                sqlca.Rollback();
            }
        }
    }
}