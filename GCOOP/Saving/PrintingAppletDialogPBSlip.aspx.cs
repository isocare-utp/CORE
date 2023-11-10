using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving
{
    public partial class PrintingAppletDialogPBSlip : System.Web.UI.Page
    {
        protected String appletPath;
        protected String printingName;
        protected String urlXmlData;
        protected String urlXmlMaster;
        protected String serverPBPath;
        protected String clientPBPath;
        protected String autoUpdate;
        protected String serverVersion;
        protected String serverFiles;

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
            String connectIndex = "0";
            WebState state = new WebState();
            try
            {
                connectIndex = Request["con_index"];// "0:" + state.SsConnectionString;
                //connectIndex = connectIndex.Replace("&", "¤");
                //connectIndex = connectIndex.Replace("?", "¢");
                //connectIndex = connectIndex.Replace(" ", "»");
            }
            catch { }

            XmlConfigService xml = new XmlConfigService(WebUtil.GetGcoopPath());

            appletPath = GetRequest("appletPath") + "?tmtm=" + DateTime.Now.ToString("hhmmssss");
            printingName = GetRequest("printingName");
            urlXmlData = GetRequest("urlXmlData");
            String appletSavingUrl = WebUtil.GetSavingUrlCore() + "SlipApp/" + "xml_printing_master.aspx?con_index=" + connectIndex + "&vdir="+ WebUtil.GetVirtualDirectory() +"&printing_name=" + printingName + "&mode=runtime&tmtm=" + DateTime.Now.ToString("hhmmssss");
            urlXmlMaster = appletSavingUrl + "&printing_table=master";
            serverPBPath = xml.AppletPBSlipServerPath;
            clientPBPath = xml.AppletPBSlipClientPath;
            autoUpdate = xml.AppletPBSlipAutoUpdate;
            serverVersion = xml.AppletPBSlipVersion;
            serverFiles = xml.AppletPBSlipFiles;
        }
    }
}