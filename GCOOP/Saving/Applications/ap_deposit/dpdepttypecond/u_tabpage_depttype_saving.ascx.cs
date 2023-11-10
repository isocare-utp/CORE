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

namespace Saving.Applications.ap_deposit.dpdepttypecond
{
    public partial class u_tabpage_depttype_saving : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //name="ctl00$ContentPlace$HSelect" id="ctl00_ContentPlace_HSelect" 
                Label1.Text = Request["ctl00$ContentPlace$HSelect"].ToString();
            }
            catch
            {
                Label1.Text = "No Request";
            }
        }
    }
}