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

namespace Saving.Applications.ap_deposit
{
    public partial class u_fnucfictaxtype : System.Web.UI.UserControl
    {
        DwTrans sqlca;

        protected void Page_Load(object sender, EventArgs e)
        {
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_main.SetTransaction(sqlca);
            dw_main.Retrieve(sqlca.state.SsCoopControl);
        }

        protected void Page_LoadComplete()
        {
            try
            {
                sqlca.Disconnect();
            }
            catch { }
        }
        protected void dw_main_BeginUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        protected void dw_main_EndUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
        }
    }
}