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
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.keeping
{
    public partial class AjaxAllProcessing : System.Web.UI.Page
    {
        protected String percent;
        private WebState state;
        private n_commonClient common;

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState();
            XmlConfigService xml = new XmlConfigService(WebUtil.GetGcoopPath());
            WcfCalling wcf = new WcfCalling(xml);
            wcf.ProgressBar = true;
            common = wcf.NCommon;
            try
            {
                String app = Request["application"];
                String wId = Request["w_sheet_id"];
                //percent = common.GetProcessStatus(state.SsWsPass, app, wId);
            }
            catch (SoapException ex)
            {
                percent = "Exception: " + WebUtil.SoapMessage(ex);// ex.Message;
            }
            catch (Exception ex)
            {
                percent = "Exception: " + ex.Message;
            }
            wcf.Close();
        }
    }
}
