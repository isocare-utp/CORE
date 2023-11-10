using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Applications.shrlon
{
    public partial class tabpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LtTabJavaScript.Text = WebUtil.GenJavaScriptTabPage(4, 3);
            LtTabMenu.Text = WebUtil.GenHeadTabMenu("1,2,3,4", 4, 3);
            
        }
    }
}
