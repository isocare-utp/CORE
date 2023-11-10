using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving
{
    public partial class webprocessingframe : System.Web.UI.Page
    {
        public string coreURI = WebUtil.coreURI;
        public string currentURI = WebUtil.GetVirtualDirectory();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}