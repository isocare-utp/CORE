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

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_sl_detail_contract : System.Web.UI.Page
    {
        private WebState state;
        private DwTrans sqlca;

        private String lcontno = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_main.SetTransaction(sqlca);
            dw_data_1.SetTransaction(sqlca);
            dw_data_2.SetTransaction(sqlca);
            dw_data_3.SetTransaction(sqlca);
            dw_data_4.SetTransaction(sqlca);

            try
            {
                this.lcontno = Session["sslncontno"].ToString();
            }
            catch { }
            if (lcontno != null && lcontno != "")
            {
                dw_main.Retrieve(lcontno);
                dw_data_1.Retrieve(lcontno);
                dw_data_2.Retrieve(lcontno);
                dw_data_3.Retrieve(lcontno);
                dw_data_4.InsertRow(0);
                dw_data_4.Retrieve(lcontno);
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            sqlca.Disconnect();
        }
    }
}
