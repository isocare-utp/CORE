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
using System.Web.Services.Protocols;

namespace Saving.Applications.ap_deposit
{
    public partial class u_dpucfapvbook : System.Web.UI.UserControl
    {
        private DwTrans SQLCA;

        protected void Page_Load(object sender, EventArgs e)
        {
            SQLCA = new DwTrans();
            SQLCA.Connect();
            DwMain.SetTransaction(SQLCA);
            DwMain.Retrieve();
        }

        protected void Page_LoadComplete()
        {
            WebState state = new WebState();
            for (int i = 1; i <= DwMain.RowCount; i++)
            {
                DwMain.SetItemString(i, "coop_id", state.SsCoopId);
            }
            try
            {
                SQLCA.Disconnect();
            }
            catch { }
        }

        protected void PreUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        protected void PostUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
        }
    }
}