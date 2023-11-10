using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving
{
    public partial class ApplicationSelection : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            MenuApplications m = new MenuApplications();
            Repeater1.DataSource = m.GetMenuApplication(oracleTA, state.SsCoopId, state.SsUsername);
            Repeater1.DataBind();
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}