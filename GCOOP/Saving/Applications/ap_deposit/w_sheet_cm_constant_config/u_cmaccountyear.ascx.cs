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
    public partial class u_cmaccountyear : System.Web.UI.UserControl
    {
        private DwTrans sqlca;
        private DwThDate thDwMain;

        protected void Page_Load(object sender, EventArgs e)
        {
            thDwMain = new DwThDate(DwMain);
            thDwMain.Add("accend_date", "accend_tdate");
            thDwMain.Add("accstart_date", "accstart_tdate");
            try
            {
                sqlca = new DwTrans();
                sqlca.Connect();
                DwMain.SetTransaction(sqlca);
                DwMain.Retrieve();
                thDwMain.Eng2ThaiAllRow();
            }
            catch { }
        }

        protected void Page_LoadComplete()
        {
            sqlca.Disconnect();
        }
    }
}