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

namespace Saving.Applications.app_finance
{
    public partial class u_fnucfconstant : System.Web.UI.UserControl
    {
        DwTrans sqlca;
        WebState state;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_main.SetTransaction(sqlca);
            dw_main.Retrieve(state.SsCoopControl);
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
            if (!state.IsWritable)
            {
                e.Cancel = true;
                sqlca.Rollback();
            }
        }

        protected void dw_main_EndUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
            if (e.RowsUpdated > 0 && state.IsWritable)
            {
                sqlca.Commit();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");   
            }
            else
            {
                sqlca.Rollback();
            }
        }
    }
}