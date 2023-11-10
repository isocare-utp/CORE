using System;
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
using CoreSavingLibrary.WcfNCommon;
//using adminservice;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving
{
    public partial class ChangePass : PageWebSheet, WebSheet
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //รหัสผ่านที่ป้อนจากหน้าจอ.
            String userID;
            WebState state = new WebState(Session,Request);
            userID = state.SsUsername;

            string oldpass = "";
            string newpass = "";
            string comppass = "";
            string password = "";

            //string user_name = Request["user_name"];
            string selectpass = "select password from amsecusers where coop_id='" + state.SsCoopId + "' and user_name ='" + userID + "'";
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
                oldpass = TextBox1.Text.Trim();
                newpass = TextBox2.Text.Trim();
                comppass = TextBox3.Text.Trim();
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
                    result = adminService.of_saveuserpassword(state.SsWsPass, userID, newpass);
                    LtServerMessage.Text = WebUtil.CompleteMessage("เปลี่ยนรหัสผ่านสำเร็จ");
                }
                catch (Exception ex)
                { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

            }
        }

        public void InitJsPostBack()
        {

            this.IgnoreReadable = true;
        }

        public void WebSheetLoadBegin()
        {
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
