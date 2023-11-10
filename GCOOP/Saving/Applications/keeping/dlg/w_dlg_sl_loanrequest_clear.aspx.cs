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

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_sl_loanrequest_clear : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            dw_lrq_cleardet.SetTransaction(SQLCA);



            if (!IsPostBack)
            {
                dw_lrq_cleardet.InsertRow(0);
            }

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            SQLCA.Disconnect();
        }
     
    }
}
