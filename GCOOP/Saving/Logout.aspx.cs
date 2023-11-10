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
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ประกาศ web state
            WebState state = new WebState();

            // พยายามลบ token id ในกรณีที่ยังไม่ได้ logout จริงๆ
            try
            {
                string connectionString = state.SsConnectionString;
                string tokenId = state.SsTokenId;
                if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(tokenId))
                {
                    Sta ta = new Sta(connectionString);
                    string sql = "delete from ssotoken where token_id = '" + tokenId + "'";
                    ta.Exe(sql);
                }
            }
            catch { }

            //string vDir = WebUtil.GetVirtualDirectory();

            string url = WebUtil.GetSavingUrl();

            HyperLink1.NavigateUrl = url;

            Session.Abandon();
        }
    }
}