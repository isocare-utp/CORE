using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.account.dlg
{
    public partial class d_dlg_configm_delete : System.Web.UI.Page
    {
        protected int delete_row = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LbRow.Text = Request["delete_row"];
                delete_row = int.Parse(LbRow.Text);
            }
            catch { }
        }
    }
}