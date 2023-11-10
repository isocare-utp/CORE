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
    public partial class w_dlg_keepdatadet : System.Web.UI.Page
    {
        private WebState state;
        private DwTrans sqlca;

        private String memno = "", recv_period = "";
        private String pbl = "sl_member_detail.pbl";

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_main.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);
          

            if (!IsPostBack)
            {
                dw_main.InsertRow(0);
                dw_detail.InsertRow(0);
            }

            try
            {
                memno = Request["memno"];
                recv_period = Request["recv_period"];
            }
            catch { }

            if (memno != null && memno != "")
            {
                DwUtil.RetrieveDataWindow(dw_main, pbl, null, state.SsCoopId, memno, recv_period);
                DwUtil.RetrieveDataWindow(dw_detail, pbl, null, state.SsCoopId, memno, recv_period);
             
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

