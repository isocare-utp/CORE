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

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_detail_share : System.Web.UI.Page
    {
        private WebState state;
        private DwTrans sqlca;

        private String memno = "", sharetype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_data.SetTransaction(sqlca);
            dw_data_1.SetTransaction(sqlca);
            dw_data_2.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                dw_data.InsertRow(0);
            }

            try
            {
                memno = Request["memno"];
                sharetype = Request["shrtype"];
            }
            catch { }

            if (memno != null && memno != "")
            {
                dw_data.Retrieve(memno, sharetype);
                dw_data_1.Retrieve(memno, sharetype);
                dw_data_2.Retrieve(memno);
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            try
            {
                sqlca.Disconnect();
            }
            catch { }
        }
    }
}

