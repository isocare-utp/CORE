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

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_reqgain_true : System.Web.UI.Page
    {
        WebState state;
        DwTrans sqlca;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_gain.SetTransaction(sqlca);
            //dw_gaindetail.SetTransaction(sqlca);

            dw_gain.InsertRow(0);

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            sqlca.Disconnect();
        }
    }
}
