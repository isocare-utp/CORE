using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving
{
    public partial class FrameDialog : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageWebDialog pd = (PageWebDialog)ContentPlace.Page;
            if (pd.isLogon)
            {
                string strq = "?qdt" + DateTime.Today.ToString("yyyyMMdd");
                LtHideJs.Text = "<link href=\"" + pd.state.SsUrl + "JsCss/DataSourceTool.css" + strq + "\" rel=\"stylesheet\" type=\"text/css\" />\n";
                LtHideJs.Text += "<script src=\"" + pd.state.SsUrl + "JsCss/DataSourceTool.js\" type=\"text/javascript\"></script>";
            }
            else
            {
                pd.Response.Clear();
                pd.Response.Write("<br /><br /><div align='center'>คุณยังไม่ได้เข้าสู่ระบบ</div>");
            }
        }
    }
}