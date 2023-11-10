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
//using CoreSavingLibrary.WcfReport;

namespace Saving.Criteria
{
    public partial class AjaxSlPrincipalBalance : System.Web.UI.Page
    {
        protected String percent;
        private WebState state;
        //private ReportClient wsReport;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState();
            WcfCalling wcf = new WcfCalling(null);
            //wsReport = wcf.Report;
            try
            {
                String app = Request["app"].ToString();
                String gid = Request["gid"].ToString();
                String rid = Request["rid"].ToString();
               // percent = wsReport.GetStatus(state.SsWsPass, app, gid, rid);
            }
            catch (Exception ex)
            {
                percent = "ไม่มีสถานะการประมวลผล ... " + ex.Message;
            }
            wcf.Close();
        }
    }
}
