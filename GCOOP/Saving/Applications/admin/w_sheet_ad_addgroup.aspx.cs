using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_addgroup : PageWebSheet, WebSheet
    {
        protected String jsSearch;
        protected String jsaddusername;
        protected String jsRemove;
        protected String jsDescription;
        protected String jsAddUserTOGroup;
        public void InitJsPostBack()
        {
            jsAddUserTOGroup = WebUtil.JsPostBack(this, "jsAddUserTOGroup");
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsaddusername = WebUtil.JsPostBack(this, "jsaddusername");
            jsRemove = WebUtil.JsPostBack(this, "jsRemove");
            jsDescription = WebUtil.JsPostBack(this, "jsDescription");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwGroup.InsertRow(0);
                DwButtom.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwGroup);
                this.RestoreContextDw(DwGroupUsers);
                this.RestoreContextDw(DwButtom);
                this.RestoreContextDw(DwUsers);
               
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsSearch":
                    JsSearch();
                    break;
                case "jsaddusername":
                    AddUserName();
                    break;
                case "jsRemove":
                    RemoveUserName();
                    break; 
                case "jsDescription":
                    DescriptionCheck();
                    break;
                case "jsAddUserTOGroup":
                    AddUserTOGroup();
                    break;
 
            }          
        }

        public void AddUserTOGroup()
        {
            int r = DwUsers.RowCount;
            int r2 = DwGroupUsers.RowCount;
            string user_name = "";
            string full_name = "";
            decimal flag = 0;
            for (int i = r; i > 0; i--)
            {
                try
                {
                    flag = DwUsers.GetItemDecimal(i, "flag");
                }
                catch { flag = 0; }
                if (flag == 1)
                {
                    try
                    {
                        r2++;
                        user_name = DwUsers.GetItemString(i, "user_name");
                        full_name = DwUsers.GetItemString(i, "full_name");
                        DwUsers.DeleteRow(i);
                        DwGroupUsers.InsertRow(0);
                        DwGroupUsers.SetItemString(r2, "user_name", user_name);
                        DwGroupUsers.SetItemString(r2, "full_name", full_name);
                    }
                    catch { }
                }
            }

            DwGroupUsers.SetSort("user_name");
            DwGroupUsers.Sort();
        }

        public void DescriptionCheck()
        {
            HdCkDes.Value = "1";
            string user_n = DwGroup.GetItemString(1, "user_name");
            string des = DwGroup.GetItemString(1, "description");
            string sqlck = "select description from amsecusers where description ='" + des + "' and user_name <>'" + user_n + "'";
            Sdt ckdes = WebUtil.QuerySdt(sqlck);
            if (ckdes.Next())
            {
                LtServerMessage.Text = WebUtil.WarningMessage("คำอธิบายนี้มีอยู่แล้วในระบบ กรุณากรอกคำอธิบายใหม่");
                HdCkDes.Value = "0";
            }
        }
        public void AddUserName()
        {
            string G = DwGroup.GetItemString(1, "user_name");
            string user_name = HdNew.Value;
            string full_name = HdName.Value;
            int row = DwGroupUsers.RowCount;
            //เช็คซ้ำ
            string cku = "";
            string statusck = "0";
            for (int i = 1; i < row + 1; i++)
            {
                cku = DwGroupUsers.GetItemString(i, "user_name");
                if (cku == user_name)
                {
                    statusck = "1";
                }
            }

            if (statusck == "0")
            {
                DwGroupUsers.InsertRow(0);
                DwGroupUsers.SetItemString(row + 1, "user_name", user_name);
                DwGroupUsers.SetItemString(row + 1, "full_name", full_name);
                DwGroupUsers.SetItemString(row + 1, "group_name", G);
            }
            else { LtServerMessage.Text = WebUtil.WarningMessage("เพิ่มคนซ้ำ"); }
        }
        public void JsSearch()
        {
            string user_id = DwGroup.GetItemString(1,"user_name").Trim();
            
            try
            {
                DwUtil.RetrieveDataWindow(DwGroup, "ad_group.pbl", null, user_id , state.SsCoopId);
                DwUtil.RetrieveDataWindow(DwGroupUsers, "ad_group.pbl", null, user_id ,state.SsCoopId);
                string test = DwGroup.GetItemString(1, "user_name");
                LtServerMessage.Text = WebUtil.WarningMessage("รหัสนี้มีอยุ่ในระบบแล้ว");
            }
            catch
            {
                DwGroup.InsertRow(0);
                DwGroup.SetItemString(1,"user_name",user_id);
                DwGroup.SetItemString(1, "coop_id", state.SsCoopId);
                DwGroup.SetItemDecimal(1, "user_type", 2);
                DwGroup.SetItemDecimal(1, "point_ofcash", 0);
                DwGroup.SetItemDecimal(1, "apvcash_flag", 0);
                DwGroup.SetItemDecimal(1, "freez_flag", 0);
                DwGroup.SetItemDecimal(1, "user_c", 0);
            }

        }

        public void SaveWebSheet()
        {
            n_adminClient adminService = wcf.NAdmin;
            string group_name = DwGroup.GetItemString(1, "user_name");
            string description = DwGroup.GetItemString(1, "description");
            int result = 2;
            try
            {
                result = adminService.of_existinguser(state.SsWsPass, group_name, description);
            }
            catch (Exception e)
            { LtServerMessage.Text = WebUtil.ErrorMessage(e); }

            if (result == 0 && HdCkDes.Value != "0")
            {
                int resultsave = 0;

                String d_um_user_xml = DwGroup.Describe("Datawindow.data.XML");
                try
                {
                    resultsave = adminService.of_savenewgroup(state.SsWsPass, d_um_user_xml);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }

                if (resultsave == 1 && DwGroupUsers.RowCount > 0)
                {
                    string user_name = "";
                    for (int i = 1; i <= DwGroupUsers.RowCount; i++)
                    {
                        try
                        {
                            user_name = DwGroupUsers.GetItemString(i, "user_name");
                            string savegroupusers = "insert into amsecgroupings (group_name,user_name) values ('" + group_name + "','" + user_name + "')";
                            Sdt insertuser = WebUtil.QuerySdt(savegroupusers);
                            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    if (resultsave == 1)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    }
                    else
                    { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้"); }
                }

            }
            else
            { LtServerMessage.Text = WebUtil.WarningMessage("รหัสกลุ่มผู้ใช้งาน หรือ รายละเอียดกลุ่มผู้ใช้งานซ้ำ"); }

        }

        public void WebSheetLoadEnd()
        {
            DwUsers.SaveDataCache();
            DwGroup.SaveDataCache();
            DwGroupUsers.SaveDataCache();
            DwButtom.SaveDataCache();
        }

        public void RemoveUserName()
        {
            string user_name = "";
            string full_name = "";
            int r = DwGroupUsers.RowCount;
            int r2 = DwUsers.RowCount;
            decimal flag = 0;
            for (int i = r; i > 0; i--)
            {
                try
                {
                    flag = DwGroupUsers.GetItemDecimal(i, "flag");
                }
                catch { flag = 0; }
                if (flag == 1)
                {
                    try
                    {
                        r2++;
                        user_name = DwGroupUsers.GetItemString(i, "user_name");
                        full_name = DwGroupUsers.GetItemString(i, "full_name");
                        DwGroupUsers.DeleteRow(i);

                        DwUsers.InsertRow(0);
                        DwUsers.SetItemString(r2, "user_name", user_name);
                        DwUsers.SetItemString(r2, "full_name", full_name);
                    }
                    catch { }
                }
            }
            DwUsers.SetSort("user_name");
            DwUsers.Sort();
            HdValue.Value = "";
            HdRow.Value = "";
       
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DwUtil.RetrieveDataWindow(DwUsers, "ad_user.pbl", null, "", state.SsCoopId,"%"+TextBox1.Text+"%");
                string test = DwUsers.GetItemString(1, "user_name");
            }
            catch { DwUsers.Reset(); }
        }
    }
}