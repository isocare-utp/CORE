using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.assist.ws_as_img_deneficiary_ctrl  
{
    public partial class ws_as_img_deneficiary : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {
            
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                string url = state.SsUrl + "Applications/fom/dlg/wd_fom_upload_image_ctrl/wd_fom_upload_image.aspx?Token=&application=assist&img_type_code=001&column_name=member_no&column_data=";
                IMG_Iframe.Attributes.Add("src", url);
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