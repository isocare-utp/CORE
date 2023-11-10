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
    public partial class u_dpucfcancelchqreson : System.Web.UI.UserControl
    {
        private DwTrans SQLCA;
        private WebState state;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            if (state.IsReadable)
            {
                SQLCA = new DwTrans();
                SQLCA.Connect();
                DwMain.SetTransaction(SQLCA);
                DwMain.Retrieve();
            }
        }

        protected void Page_LoadComplete()
        {
            try
            {
                SQLCA.Disconnect();
            }
            catch { }
        }

        protected void PreUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!state.IsWritable)
            {
                e.Cancel = true;
                SQLCA.Rollback();
            }
        }

        protected void PostUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
            if (e.RowsUpdated > 0 && state.IsWritable)
            {
                SQLCA.Commit();
            }
            else if (e.RowsInserted > 0 && state.IsWritable)
            {
                SQLCA.Commit();
            }
            else if (e.RowsDeleted > 0 && state.IsWritable)
            {
                SQLCA.Commit();
            }
            else
            {
                SQLCA.Rollback();
            }
        }
    }
}