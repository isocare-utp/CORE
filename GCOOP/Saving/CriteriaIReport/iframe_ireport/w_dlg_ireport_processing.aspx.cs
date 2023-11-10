using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Net;
using System.Text;

namespace Saving.CriteriaIReport.iframe_ireport
{
    public partial class w_dlg_ireport_processing : PageWebDialog, WebDialog
    {
        protected string pid;
        protected int cid;
        protected string processUrl;
        protected string spliter1;
        protected string savingUrl;

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            savingUrl = WebUtil.GetSavingUrlCore();
            spliter1 = ReportUtil.Spliter1;
            pid = Request["pid"];
            cid = int.Parse(Request["cid"]);
            string savUrl = state.SsUrl;

            try
            {
                bool retry = Request["retry"] == "true";
                if (retry)
                {
                    string ireportDoamin = xmlconfig.iReportDomain;
                    if (ireportDoamin == "*")
                    {
                        ireportDoamin = xmlconfig.SavDomain;
                    }
                    if (ireportDoamin == "*")
                    {
                        ireportDoamin = WebUtil.GetSavingAddress();
                    }
                    WebClient myWebClient = new WebClient();
                    byte[] myDataBuffer = myWebClient.DownloadData("http://" + ireportDoamin + ":" + xmlconfig.iReportPort + "/?conId=" + cid + "&rId=" + pid + "&ttt=10");
                    String downloads = Encoding.UTF8.GetString(myDataBuffer);
                }
            }
            catch { }

            LtJsCss.Text = "<script src=\"" + WebUtil.GetSavingUrlCore() + "JsCss/Ajax.js\" type=\"text/javascript\"></script>";
            processUrl = WebUtil.GetSavingUrlCore() + "CriteriaIReport/iframe_ireport/processing.aspx";
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}