using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving
{
    public partial class PrintingDialog : System.Web.UI.Page
    {
        protected string pageName;
        protected int totalPage;
        protected int rowCount;
        protected int rowPerPage;
        protected string reportName;
        protected string ipAddress;
        protected string xmlConfigUrl;
        protected int sessionFirst;

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
            pageName = GetRequest("pageName");
            totalPage = GetIntRequest("totalPage");
            rowCount = GetIntRequest("rowCount");
            rowPerPage = GetIntRequest("rowPerPage");
            reportName = GetRequest("reportName");
            ipAddress = GetRequest("ipAddress");
            xmlConfigUrl = GetRequest("xmlConfigUrl");

            try
            {
                sessionFirst = int.Parse(Session["ss_printfirst"] + "");
            }
            catch
            {
                sessionFirst = 1;
            }
            Session["ss_printfirst"] = 0;
        }
    }
}