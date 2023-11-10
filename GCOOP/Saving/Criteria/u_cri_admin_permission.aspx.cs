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
using DataLibrary;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNCommon;

namespace Saving.Criteria
{
    public partial class u_cri_admin_permission : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {
            
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dw_main.InsertRow(0);
            }
            else
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
        }
    }
}