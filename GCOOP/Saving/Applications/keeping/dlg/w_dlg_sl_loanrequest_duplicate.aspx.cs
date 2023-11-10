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
    public partial class w_dlg_sl_loanrequest_duplicate : System.Web.UI.Page
    {

        private DwTrans sqlca;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_return.SetTransaction(sqlca);
            String member = Request["member"].ToString();

            dw_return.Reset();
            dw_return.InsertRow(0);
            dw_return.Retrieve(member);
        }


    }
}
