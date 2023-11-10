using System;
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
using Saving.CmConfig;
using Saving.WsDeposit;

namespace Saving.Applications.ap_deposit
{
    public partial class AjaxDpCloseDay : System.Web.UI.Page
    {
        protected String percent;
        private WebState state;
        private Deposit dp;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState();
            dp = new Deposit();
            try
            {
                percent = dp.GetStatusRunCloseDayProcess(state.SsWsPass, state.SsApplication);
            }
            catch (Exception ex)
            {
                percent = "ไม่มีสถานะการประมวลผล ... " + ex.Message;
            }
        }
    }
}
