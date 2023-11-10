using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Collections.Generic;
using System.Net;

namespace Saving.Applications.mbshr.ws_mbshr_upload_mem_pic_ctrl
{
    public partial class ws_mbshr_upload_mem_pic : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMembNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);


        }

        public void WebSheetLoadBegin()
        {
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMembNo)
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.Retrieve(memb_no);
                dsMain.DATA[0].MEMBER_NO = memb_no;
                dsMain.DdAccid(memb_no);
            }
        }

        public void SaveWebSheet()
        {
            string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
            bool chk_profile = true;
            bool chk_signature = true;
            bool chk_dept = true;
            bool chk_dept2 = true;
            string err_mes = "";
            try //รูปโปรไฟล์สมาชิก
            {
                if (UploadProfile.HasFile)
                {
                    string fileNameProfile = Path.GetFileName(UploadProfile.PostedFile.FileName);
                    UploadProfile.PostedFile.SaveAs(Server.MapPath("~/ImageMember/profile/") + "profile_" + member_no + ".jpg");
                    //LtServerMessege.Text = WebUtil.CompleteMessage("บันทึกรูปโปรไฟล์สำเร็จ");
                    // Response.Redirect(state.SsUrl);
                    chk_profile = true;
                }
            }
            catch (Exception ex)
            {
                chk_profile = false;
                err_mes += " รูปโปรไฟล์สมาชิก:" + ex.Message;
            }
            try //รูปลายเซ็นสมาชิก
            {
                if (UploadSignature.HasFile)
                {
                    string fileNameSignature = Path.GetFileName(UploadSignature.PostedFile.FileName);
                    UploadSignature.PostedFile.SaveAs(Server.MapPath("~/ImageMember/signature/") + "signature_" + member_no + ".jpg");
                    //LtServerMessege.Text = WebUtil.CompleteMessage("บันทึกรูปลายเซ็นสำเร็จ");
                    chk_signature = true;
                }
            }
            catch (Exception ex)
            {
                chk_signature = false;
                err_mes += " รูปลายเซ็นสมาชิก:" + ex.Message;
            }

            //string user = "administrator", pass = "@Icoop20170726";
            try //รูปลายเซ็นบัญชีเงินฝากรูปที่ 1
            {
                if (UploadDept.HasFile)
                {
                    string fileNameDept = Path.GetFileName(UploadDept.PostedFile.FileName);
                    string dept_acc = dsMain.DATA[0].DEPTACCOUNT_NO;
                    dept_acc = WebUtil.ViewAccountNoFormat(dept_acc);
                    if (dept_acc != "--")
                    {
                        try
                        {
                            UploadDept.PostedFile.SaveAs(Server.MapPath("~/ImageMember/dept/") + dept_acc + "_1.bmp");
                            //UploadDept.PostedFile.SaveAs(Server.MapPath("~/WSRPDF/") + "d" + dept_acc + "_1.bmp");
                            //Upload_ftp("ftp://122.154.237.21/dp_signature/", user, pass, (Server.MapPath("~/WSRPDF/") + "d" + dept_acc + "_1.bmp").ToString());                            
                            //LtServerMessege.Text = WebUtil.CompleteMessage("บันทึกรูปลายเซ็นบัญชีเงินฝากสำเร็จ");
                            //FileInfo fi2 = new FileInfo(Server.MapPath("~/WSRPDF/") + dept_acc + "_1.bmp");
                            //fi2.Delete();
                        }
                        catch (Exception ex) { }
                        chk_dept = true;
                    }
                }
            }
            catch (Exception ex)
            {
                chk_dept = false;
                err_mes += " รูปลายเซ็นบัญชีเงินฝากรูปที่ 1:" + ex.Message;
            }

            try //รูปลายเซ็นบัญชีเงินฝากรูปที่ 2
            {
                if (UploadDept_2.HasFile)
                {
                    string fileNameDept = Path.GetFileName(UploadDept_2.PostedFile.FileName);
                    string dept_acc = dsMain.DATA[0].DEPTACCOUNT_NO;
                    dept_acc = WebUtil.ViewAccountNoFormat(dept_acc);
                    if (dept_acc != "--")
                    {
                        try
                        {
                            UploadDept_2.PostedFile.SaveAs(Server.MapPath("~/ImageMember/dept/") + dept_acc + "_2.bmp");
                            //UploadDept_2.PostedFile.SaveAs(Server.MapPath("~/WSRPDF/") + "d" + dept_acc + "_2.bmp");
                            //Upload_ftp("ftp://122.154.237.21/dp_signature/", user, pass, (Server.MapPath("~/WSRPDF/") + "d" + dept_acc + "_2.bmp").ToString());
                            //FileInfo fi2 = new FileInfo(Server.MapPath("~/WSRPDF/") + "d" + dept_acc + "_2.bmp");
                            //fi2.Delete();
                        }
                        catch (Exception ex) { }
                        chk_dept2 = true;
                    }
                }
            }
            catch (Exception ex)
            {
                chk_dept2 = false;
                err_mes += " รูปลายเซ็นบัญชีเงินฝากรูปที่ 2:" + ex.Message;
            }

            if (chk_profile && chk_signature && chk_dept && chk_dept2)
            {
                LtServerMessege.Text = WebUtil.CompleteMessage("บันทึกรูปสำเร็จ");
            }
            else
            {
                LtServerMessege.Text = WebUtil.ErrorMessage(err_mes);
            }
        }

        public void WebSheetLoadEnd()
        {
        }




        private static void Upload_ftp(string ftpServer, string userName, string password, string filename)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    client.Credentials = new System.Net.NetworkCredential(userName, password);
                    client.UploadFile(ftpServer + "/" + new FileInfo(filename).Name, "STOR", filename);
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServer + "/" + new FileInfo(filename).Name);
                    ftpRequest.Credentials = new NetworkCredential(userName, password);
                    ftpRequest.UseBinary = true;
                    ftpRequest.UsePassive = true;
                    ftpRequest.KeepAlive = true;
                    FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                    ftpResponse.Close();
                    ftpRequest = null;
                }
                catch (Exception ex) { }
            }
        }
    }

}