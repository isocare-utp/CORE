using System;
using CoreSavingLibrary;
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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.IO;

namespace Saving.Applications
{
    public partial class w_dlg_picture : PageWebDialog, WebDialog
    {
        protected String savePic;
        private String member_no;

        public void InitJsPostBack()
        {
            savePic = WebUtil.JsPostBack(this, "savePic");
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "savePic")
            {
                SaveWebSheet();
                try
                {
                    member_no = Request["member"];

                    if (member_no != "")
                    {
                        try
                        {

                            String imageUrl = state.SsUrl + "Applications/keeping/dlg/member_picture/" + member_no + ".jpg";
                            Image1.ImageUrl = imageUrl;
                            Label1.Text = "รูปภาพสมาชิก " + member_no;
                        }
                        catch (Exception ex) { Label2.Text = ex.ToString(); }
                    }
                    else
                    {
                        Image1.ImageUrl = "";
                        Label1.Text = "";
                    }
                    TextBox1.Text = member_no;
                }
                catch (Exception ex)
                {

                    Label2.Text = ex.ToString();
                    member_no = "";

                }
            }
        }

        public void SaveWebSheet()
        {
            member_no = Request["member"];
            TextBox1.Text = member_no;
            if ((FileUpload.PostedFile != null) && (FileUpload.PostedFile.ContentLength > 0) && member_no != "")
            {
                try
                {
                    String filename = Path.GetFileName(FileUpload.PostedFile.FileName);
                    String save = Server.MapPath("") + "\\member_picture\\" + member_no + ".jpg";
                    FileUpload.PostedFile.SaveAs(save);

                }
                catch (Exception ex)
                {
                    ex.ToString();
                    Label2.Text = "ไม่สามารถ อัปโหลดรูปได้  <br />";
                }
            }
            else
            {
                if (FileUpload.PostedFile == null)
                {
                    Label2.Text = "ไม่พบรูป <br />";
                }
                else
                {
                    Label2.Text = FileUpload.PostedFile.ToString();
                }
            }
        }

     

        //protected void Save_Click(object sender, EventArgs e)
        //{
        //    SaveWebSheet();
        //    try
        //    {
        //        member_no = Request["member"];

        //        if (member_no != "")
        //        {
        //            try
        //            {
                      
        //                String imageUrl = state.SsUrl + "Applications/keeping/dlg/member_picture/" + member_no + ".jpg";
        //                Image1.ImageUrl = imageUrl;
        //                Label1.Text = "รูปภาพสมาชิก " + member_no;
        //            }
        //            catch (Exception ex) { Label2.Text = ex.ToString(); }
        //        }
        //        else
        //        {
        //            Image1.ImageUrl = "";
        //            Label1.Text = "";
        //        }
        //        TextBox1.Text = member_no;
        //    }
        //    catch (Exception ex)
        //    {

        //        Label2.Text = ex.ToString();
        //        member_no = "";

        //    }
           
        //}



        #region WebDialog Members


        public void WebDialogLoadBegin()
        {
             try
            {
                member_no = Request["member"];


                if (member_no != "")
                {
                    String imageUrl = state.SsUrl + "Applications/keeping/dlg/member_picture/" + member_no + ".jpg";
                    Image1.ImageUrl = imageUrl;
                    Label1.Text = "รูปภาพสมาชิก " + member_no;
                }
                else
                {
                    Image1.ImageUrl = "";
                    Label1.Text = "";
                }
                TextBox1.Text = member_no;
            }
            catch
            {
                member_no = "";

            }
        }

        public void WebDialogLoadEnd()
        {
          //  throw new NotImplementedException();
        }

        #endregion
    }
}
