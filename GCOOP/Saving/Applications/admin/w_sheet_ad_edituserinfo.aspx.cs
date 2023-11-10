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
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;
using Sybase.DataWindow;
using System.Web.Services.Protocols;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_edituserinfo : PageWebSheet, WebSheet
    {
        protected String jsSearch;
        protected String jsresetpass;
        protected String NoUserName;
        protected String jsDescription;
        protected String DeleteUserName;
        public void InitJsPostBack()
        {
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsresetpass = WebUtil.JsPostBack(this, "jsresetpass");
            NoUserName = WebUtil.JsPostBack(this, "NoUserName");
            jsDescription = WebUtil.JsPostBack(this, "jsDescription");
            DeleteUserName = WebUtil.JsPostBack(this, "DeleteUserName");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwUserName.InsertRow(0);

            }
            else
            {
                this.RestoreContextDw(DwUserName);

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsSearch")
            {
                JsSearch();
            }
            else if (eventArg == "jsresetpass")
            {
                Jsresetpass();
            }
            else if (eventArg == "NoUserName")
            {
                NoHaveUserName();
            }
            else if (eventArg == "jsDescription")
            {
                DescriptionCheck();
            }
            else if (eventArg == "DeleteUserName")
            {
               // HdCkDes.Value = "1";
                string user_n = DwUserName.GetItemString(1, "user_name");
                string des = DwUserName.GetItemString(1, "full_name");
                ExecuteDataSource exe = new ExecuteDataSource(this);

                string ls_sqldel = @"delete from amsecuseapps where user_name ='" + user_n + "' and coop_id ='" + state.SsCoopId + "'";
                exe.SQL.Add(ls_sqldel);
                exe.Execute();

                string ls_sqldel2 = @"delete from amsecpermiss where user_name ='" + user_n + "' and coop_id ='" + state.SsCoopId + "'";
                exe.SQL.Add(ls_sqldel2);
                exe.Execute();

                string ls_sqldel3 = @"delete from amsecreportpermiss where user_name ='" + user_n + "' and coop_id ='" + state.SsCoopId + "'";
                exe.SQL.Add(ls_sqldel3);
                exe.Execute();

                string ls_sqldel4 = @"delete from amsecusers where full_name='" + des + "' and user_name ='" + user_n + "' and coop_id ='" + state.SsCoopId + "'";
                exe.SQL.Add(ls_sqldel4);
                exe.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("ลบ user สำเร็จเเล้ว");
            }
        }
        public void DescriptionCheck()
        {
            HdCkDes.Value = "1";
            string user_n = DwUserName.GetItemString(1, "user_name");
            string des = DwUserName.GetItemString(1, "full_name");
            string sqlck = "select full_name from amsecusers where full_name ='" + des + "' and user_name <>'" + user_n + "'";
            Sdt ckdes = WebUtil.QuerySdt(sqlck);
            if (ckdes.Next())
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ชื่อนี้มีอยู่แล้วในระบบ กรุณากรอกชื่อใหม่");
                HdCkDes.Value = "0";
            }
        }

        public void NoHaveUserName()
        {
            LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกรหัสผู้ใช้");
        }
        public void Jsresetpass()
        {
            n_adminClient adminService = wcf.NAdmin;
            int result = 0;
            string user_name = DwUserName.GetItemString(1,"user_name"); 
            try
            {
                result = adminService.of_resetuserpassword(state.SsWsPass, user_name);
                LtServerMessage.Text = WebUtil.CompleteMessage("รีเซต รหัสผ่านสำเร็จ");
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        public void JsSearch()
        {
            string user_name = DwUserName.GetItemString(1,"user_name");
            DwUtil.RetrieveDataWindow(DwUserName, "ad_user.pbl", null,user_name,state.SsCoopId);
            try
            {
                string a = DwUserName.GetItemString(1, "user_name");
            }
            catch 
            {
                DwUserName.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลผู้ใช้งาน"); 
            }

    
        }

        public void SaveWebSheet()
        {
            n_adminClient adminService = wcf.NAdmin;
            string user_name = DwUserName.GetItemString(1,"user_name");
            string full_name = DwUserName.GetItemString(1, "full_name");
            int result=0;
            try
            {
                result = adminService.of_existinguser(state.SsWsPass, user_name, full_name);
            }
            catch (Exception e)
            { LtServerMessage.Text = WebUtil.ErrorMessage(e); }


            if (result == 1 && HdCkDes.Value != "0")
            {

                String d_um_user_xml = DwUserName.Describe("Datawindow.data.XML");
                try
                {
                    DwUtil.UpdateDataWindow(DwUserName, "ad_user.pbl", "amsecusers");
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบผู้ใช้ในระบบ หรือ ชื่อผู้ใช้ซ้ำ");
            }
        }

        public void WebSheetLoadEnd()
        {
            DwUtil.RetrieveDDDW(DwUserName, "apvlevel_id", "ad_user.pbl", null);
            DwUserName.SaveDataCache();

        }
    }
}