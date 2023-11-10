using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.IO;
using CoreSavingLibrary;
using System.Data;
using System.Windows.Forms;

namespace Saving.Applications.ap_deposit.ws_dp_upload_pic_ctrl
{
    public partial class ws_dp_upload_pic_ctrl : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostDeposit { get; set; }
        [JsPostBack]
        public string PostDelDeposit { get; set; }
        

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostDeposit)
            {
                string deposit_no = dsMain.DATA[0].deposit_no;
                if (deposit_no != "")
                {
                ShowImage_Member();
                }else{
                    dsMain.DATA[0].deposit_no = deposit_no;
                }
            }
            else if (eventArg == PostDelDeposit)
            {
                string deposit_no = dsMain.DATA[0].deposit_no;
                if (deposit_no != "")
                {
                DelImg();
                }
                else
                {
                    dsMain.DATA[0].deposit_no = deposit_no;
                }
            }
            
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
        public void ShowImage_Member()
        {
            string deposit_no = dsMain.DATA[0].deposit_no;

            String deposit = deposit_no; //WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            string path_depositer = "";
            try
            {
                if (deposit_no != "" && deposit_no != "00000000" && deposit_no != null)
                {
                    path_deposit.Visible = true;
                    path_depositer = state.SsUrl + "ImageMember/dept/" + deposit + ".jpg";
                    if (path_depositer !="")
                    {
                        path_deposit.ImageUrl = state.SsUrl + "ImageMember/dept/" + deposit + ".jpg";
                    }
                    else
                    {
                        path_deposit.ImageUrl = state.SsUrl + "ImageMember/dept/signature_nopic.jpg";
                    }
                }
                else
                {
                    path_deposit.Visible = false;
                }
            }
            catch
            {
                path_deposit.Visible = false;                
            }

            dsMain.DATA[0].deposit_no = deposit_no;
            dsMain.DATA[0].path_deposit = path_depositer;
        }

        public void DelImg()
        {
            string deposit_no = dsMain.DATA[0].deposit_no;
            string FileToDelete;
            FileToDelete = Server.MapPath("~/ImageMember/dept/") + deposit_no + ".jpg";
            File.Delete(FileToDelete);
        }

        protected void Upload(object sender, EventArgs e)
        {
            string deposit_no = dsMain.DATA[0].deposit_no;
            bool chk_dept = true;
            string err_mes = "";
           
           
            try //รูปลายเซ็นบัญชีเงินฝากรูปที่ 1
            {
                if (UploadDept.HasFile)
                {
                    string fileNameDept = Path.GetFileName(UploadDept.PostedFile.FileName);
                    if (deposit_no != "")
                    {
                        UploadDept.PostedFile.SaveAs(Server.MapPath("~/ImageMember/dept/") + deposit_no + ".jpg");
                        //LtServerMessege.Text = WebUtil.CompleteMessage("บันทึกรูปลายเซ็นบัญชีเงินฝากสำเร็จ");
                        chk_dept = true;
                    }
                    // Response.Redirect(state.SsUrl);
                }
            }
            catch (Exception ex)
            {
                chk_dept = false;
                err_mes += " รูปลายเซ็นบัญชีเงินฝาก:" + ex.Message;
            }

            if (chk_dept)
            {
                LtServerMessege.Text = WebUtil.CompleteMessage("บันทึกรูปสำเร็จ");
            }
            else
            {
                LtServerMessege.Text = WebUtil.ErrorMessage(err_mes);
            }
        }
    }
}