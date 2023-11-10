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

namespace Saving.Applications.mbshr
{
    public partial class u_mbucfresigncause : System.Web.UI.UserControl
    {

        private DwTrans sqlca;
       
        private WebState state;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            if (state.IsReadable)
            {
                sqlca = new DwTrans();
                sqlca.Connect();
                dw_main.SetTransaction(sqlca);
                dw_main.Retrieve(state.SsCoopControl);
            }
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
            //if (!state.IsWritable)
            //{
            //    e.Cancel = true;
            //    SQLCA.Rollback();
            //}
        }

        protected void PostUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
            if (true)
            {
                sqlca.Commit();
            }
        }
    }
}