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
using CoreSavingLibrary.WcfNPrincipalBalance;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.shrlon
{
    public partial class AjaxSlPrincipalBalance : System.Web.UI.Page
    {
        protected String percent;
        private WebState state;
        private PrincipalBalanceClient wsPrin;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState();
            WcfCalling wcf = new WcfCalling(null);
            wsPrin = wcf.PrincipalBalance;
            try
            {
                percent = wsPrin.GetStatus(state.SsWsPass);
            }
            catch (Exception ex)
            {
                percent = "ไม่มีสถานะการประมวลผล ... " + ex.Message;
            }
            wcf.Close();
        }
    }
}
