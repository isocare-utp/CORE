using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Net;

namespace Saving
{
    public partial class GetFile : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["filepath"] == null)
            {
                Response.Write("<script>alert(\"No data found!!\");</script>");
            }
            else
            {
                //Session["filepath"] = "d://20170206113118_deposit_bank_deposit_bank002.xls";
                if (Request["filepath"] != null) Session["filepath"] = Request["filepath"];
                WebUtil.GetResponseGetFileDownload(Session["filepath"].ToString());
                Session["filepath"] = null;
            }
        }
    }
}