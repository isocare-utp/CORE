using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.IO;

namespace Saving.Applications.ap_deposit.dlg.w_dlg_dp_signature_ctrl
{
    public partial class w_dlg_dp_signature : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
        }

        public void WebDialogLoadBegin()
        {
            string dpdept_no = "";
            string sv_path_signature = "";
            string sv_path_signature2 = "";
            try
            {
                dpdept_no = Request["dpdept_no"];
                //dpdept_no = dpdept_no.Replace("-", string.Empty);
                sv_path_signature = Server.MapPath("~/ImageMember/dept/" + dpdept_no + "_1.bmp");
                sv_path_signature2 = Server.MapPath("~/ImageMember/dept/" + dpdept_no + "_2.bmp");

                if (File.Exists(sv_path_signature))
                {
                    Img_dp_signature.ImageUrl = "../../../../ImageMember/dept/" + dpdept_no + "_1.bmp";
                }
                else
                {
                    Img_dp_signature.ImageUrl = "../../../../ImageMember/dept/signature_nopic.jpg";
                }

                if (File.Exists(sv_path_signature2))
                {
                    Img_dp_signature2.ImageUrl = "../../../../ImageMember/dept/" + dpdept_no + "_2.bmp";
                }
                else
                {
                    Img_dp_signature2.ImageUrl = "../../../../ImageMember/dept/signature_nopic.jpg";
                }
            }
            catch { }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
        }
    }
}