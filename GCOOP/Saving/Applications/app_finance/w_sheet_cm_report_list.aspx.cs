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
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_cm_report_list : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            d_cm_report_group.SetTransaction(SQLCA);
            d_cm_report_group.InsertRow(0);
            d_cm_report_detail.SetTransaction(SQLCA);
            d_cm_report_detail.InsertRow(0);
        }
        protected void Page_LoadComplete()
        {
            SQLCA.Disconnect();
        }
    }
}
