using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DataLibrary;

namespace Saving
{
    public partial class PrintingAppletDialog : System.Web.UI.Page
    {
        protected String appletPath;
        protected String printingName;
        protected String urlXmlData;
        protected String urlXmlMaster;
        protected String urlXmlDetail;

        private String GetRequest(String req)
        {
            try
            {
                return Request[req].Trim();
            }
            catch
            {
                return "";
            }
        }

        private int GetIntRequest(String req)
        {
            try
            {
                return int.Parse(GetRequest(req));
            }
            catch
            {
                return -1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int connectIndex = 0;
            try
            {
                //WebState state = new WebState();
                //connectIndex = state.SsConnectionIndex;
                connectIndex = int.Parse(Request["con_index"]);
            }
            catch { }
            appletPath = GetRequest("appletPath") + "?tmtm=" + DateTime.Now.ToString("hhmmssss");
            printingName = GetRequest("printingName");
            urlXmlData = GetRequest("urlXmlData");

            XmlConfigService xml = new XmlConfigService(WebUtil.GetGcoopPath());
            string domain = xml.SavAppletDomain;
            if (domain == "*")
            {
                domain = xml.SavDomain;
            }
            if (domain == "*")
            {
                domain = WebUtil.GetSavingAddress();
            }
            String appletSavingUrl = xml.SavAppletProtocol + "://" + domain + ":" + xml.SavAppletPort + "/CORE/" + xml.SavPathPattern + "";
            appletSavingUrl += "SlipApp/" + "xml_printing_master.aspx?con_index=" + connectIndex + "&vdir=" + WebUtil.GetVirtualDirectory() + "&printing_name=" + printingName + "&mode=runtime&tmtm=" + DateTime.Now.ToString("hhmmssss");

            urlXmlMaster = appletSavingUrl + "&printing_table=master";
            urlXmlDetail = appletSavingUrl + "&printing_table=detail";
        }
    }
}