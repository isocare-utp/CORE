using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data; 

namespace Saving.Applications.admin_const.ws_am_cmcoop_ctrl
{
    public partial class ws_am_cmcoop : PageWebSheet, WebSheet 
    {

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdProvince();
                dsMain.RetrieveDsmain();
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

        }
    }
}