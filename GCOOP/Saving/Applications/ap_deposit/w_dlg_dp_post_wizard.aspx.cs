using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.ap_deposit
{
	public partial class w_dlg_dp_post_wizard : System.Web.UI.Page
	{
        private DwTrans sqlca;

		protected void Page_Load(object sender, EventArgs e)
		{
            sqlca = new DwTrans();
            sqlca.Connect();

            string app = "ap_deposit";
            int openType = 1;

            DwMain.SetTransaction(sqlca);
            DwMain.Retrieve(app, openType);

		}
	}
}
