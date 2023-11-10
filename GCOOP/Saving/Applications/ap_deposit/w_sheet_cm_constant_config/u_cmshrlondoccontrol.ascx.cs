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

namespace Saving.Applications
{
    public partial class u_cmshrlondoccontrol : System.Web.UI.UserControl
    {
        private DwTrans sqlca;

        protected void Page_Load(object sender, EventArgs e)
        {
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
        }

        protected void dw_main_EndUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
        }
    }
}