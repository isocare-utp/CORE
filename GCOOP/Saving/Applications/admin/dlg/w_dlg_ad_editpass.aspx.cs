using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sybase.DataWindow;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving.Applications.admin.dlg
{
    public partial class w_dlg_ad_editpass : PageWebDialog, WebDialog
    {

        protected String jsSearch;
        protected String jsChangePass;
        string user_name = "";
        public void InitJsPostBack()
        {
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsChangePass = WebUtil.JsPostBack(this, "jsChangePass");
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                user_name = Request["user_name"];
                DwUtil.RetrieveDataWindow(DwMain, "ad_user.pbl", null, user_name);
                try
                {
                    string ck = DwMain.GetItemString(1, "user_name");
                }
                catch
                {
                    DwMain.InsertRow(0);
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกรหัสผู้ใช้ที่ต้องการเปลี่ยนรหัสผ่านก่อน");
                }

            }
            else
            {
                this.RestoreContextDw(DwMain);

            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsSearch")
            {
                JsSearch();
            }
            else if (eventArg == "jsChangePass")
            {
                ChangePassWord();
            }
        }
        public void JsSearch()
        {
            string user_name = Request["user_name"];
            try
            {
                DwUtil.RetrieveDataWindow(DwMain, "ad_user.pbl", null, user_name);
                string test = DwMain.GetItemString(1, "user_name");
            }
            catch
            {
                DwMain.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรหัสผู้ใช้ที่ระบุ");
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }


        public void ChangePassWord()
        {
            user_name = Request["user_name"];
            string oldpass = "";
            string newpass = "";
            string comppass = "";
            string password = "";

            //string user_name = Request["user_name"];
            string selectpass = "select password from amsecusers where coop_id='" + state.SsCoopId + "' and user_name ='" + user_name + "'";
            Sdt pass = WebUtil.QuerySdt(selectpass);
            if (pass.Next())
            {
                password = pass.GetString("password");
            }


            n_adminClient adminService = wcf.NAdmin;
            string resultpass = "";
            try
            {
                resultpass = adminService.of_decodestring(state.SsWsPass, password);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }


            try
            {
                oldpass = DwMain.GetItemString(1, "passold");
                newpass = DwMain.GetItemString(1, "passnew");
                comppass = DwMain.GetItemString(1, "passcon");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูลให้ครบทุกช่อง");
            }

            if (newpass == "" || comppass == "" || oldpass == "")
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูลให้ครบทุกช่อง");
            }
            else if (oldpass != resultpass)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("รหัสผ่านเดิมผิด");
            }
            else if (newpass != comppass)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("รหัสผ่านใหม่ไม่ตรงกัน");
            }
            else
            {

                int result = 0;
                try
                {
                    result = adminService.of_saveuserpassword(state.SsWsPass, user_name, newpass);
                    LtServerMessage.Text = WebUtil.CompleteMessage("เปลี่ยนรหัสผ่านสำเร็จ");
                }
                catch (Exception ex)
                { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

            }
        }
    }
}
