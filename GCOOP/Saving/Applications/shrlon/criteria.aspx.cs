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

namespace Saving.Applications.shrlon
{
    //เทส Commit จากเครื่อง op
    public partial class criteria : System.Web.UI.Page
    {
        //เทส Commit
        protected void Page_Load(object sender, EventArgs e)
        {
            String req="";
            try
            {
                req = Request["uoobject"];
            }
            catch { }
            LbText.Text = req+".aspx";
        }
    }
}
