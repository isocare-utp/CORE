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
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_editpass : PageWebSheet, WebSheet
    {
        protected String jsSearch;
        public void InitJsPostBack()
        {
          String  jsSearch = WebUtil.JsPostBack(this, "jsSearch");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                string user_name = state.SsUsername;
                DwMain.InsertRow(0);
                DwUtil.RetrieveDataWindow(DwMain,"ad_user.pbl", null, user_name,state.SsCoopId);
                //DwMain.SetItemString(1, "password");
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
        }

        private void JsSearch()
        {
            string user_name = state.SsUsername;
            try
            {
                DwUtil.RetrieveDataWindow(DwMain, "ad_user.pbl", null, user_name, state.SsCoopId);
                //DwMain.SetItemString(1,"password","");
                string test = DwMain.GetItemString(1, "user_name");
            }
            catch
            {
                DwMain.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรหัสผู้ใช้ที่ระบุ");
            }
        }
        public void SaveWebSheet()
        {
            n_adminClient adminService = wcf.NAdmin;
            string user_name = state.SsUsername;
            string password = DwMain.GetItemString(1, "password");
            string username = "";
            string oldpass = "";
            string new_pass = "";
            string comp_pass = "";
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
                username = DwMain.GetItemString(1, "user_name");
                oldpass = DwMain.GetItemString(1, "oldpass").Trim();
                new_pass = DwMain.GetItemString(1, "newpass").Trim();
                comp_pass = DwMain.GetItemString(1, "passcon").Trim();
            }
               catch
            {

            }
                 if (new_pass == "" || comp_pass == "" || oldpass == "")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูลให้ครบทุกช่อง");
                }
                 else if (oldpass != resultpass)
                 {
                     LtServerMessage.Text = WebUtil.ErrorMessage("รหัสเก่าไม่ถูกต้อง");
                 }
                 else if (new_pass != comp_pass)
                 {
                     LtServerMessage.Text = WebUtil.ErrorMessage("รหัสผ่านใหม่ไม่ตรงกัน");
                 }
                 else
                 {

                int result = 0;
                try
                {
                    result = adminService.of_saveuserpassword(state.SsWsPass, user_name, new_pass);
                    LtServerMessage.Text = WebUtil.CompleteMessage("เปลี่ยนรหัสผ่านสำเร็จ");
                 }
                catch (Exception ex)
                { 
                 LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                 LtServerMessage.Text = WebUtil.ErrorMessage("เปลี่ยนรหัสไม่สำเร็จ");
                }
           } 
     }

        public void WebSheetLoadEnd()
        {

            DwMain.SaveDataCache();
        }

    }
}