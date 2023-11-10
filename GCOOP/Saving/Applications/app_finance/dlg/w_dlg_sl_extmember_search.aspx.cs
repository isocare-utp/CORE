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
    public partial class w_dlg_sl_extmember_search : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            //d_sl_extmembsrch_criteria.SetTransaction(SQLCA);
            d_dp_extmember_search_memno_list.SetTransaction(SQLCA);

            if (!IsPostBack)
            {
                //d_sl_extmembsrch_criteria.InsertRow(0);
                d_dp_extmember_search_memno_list.Retrieve(state.SsCoopControl);
            }
            else {
                //d_sl_extmembsrch_criteria.RestoreContext();
                d_dp_extmember_search_memno_list.RestoreContext();
            }

        }

        protected void Page_LoadComplete()
        {
            SQLCA.Disconnect();
            //d_sl_extmembsrch_criteria.SaveDataCache();
            d_dp_extmember_search_memno_list.SaveDataCache();
        }
    }
}
