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
    public partial class w_sheet_ad_addusergroup : PageWebSheet, WebSheet
    {
        protected String jsSearch;
        protected String jsaddusername;
        protected String jsRemove;
        protected String jsAddUserTOGroup;
        public void InitJsPostBack()
        {
            jsAddUserTOGroup = WebUtil.JsPostBack(this, "jsAddUserTOGroup");
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsaddusername = WebUtil.JsPostBack(this, "jsaddusername");
            jsRemove = WebUtil.JsPostBack(this, "jsRemove");
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
            string user_id = DwGroup.GetItemString(1, "user_name");

            try
            {
                DwUtil.RetrieveDataWindow(DwGroup, "ad_group.pbl", null, user_id,state.SsCoopId);
                string test = DwGroup.GetItemString(1, "user_name");
                try
                {
                    DwUtil.RetrieveDataWindow(DwGroupUsers, "ad_group.pbl", null, user_id, state.SsCoopId);
                    test = DwGroupUsers.GetItemString(1, "user_name");
                }
                catch { DwGroupUsers.Reset(); }
                if (TextBox1.Text != "" && TextBox1.Text != null)
                {
                    try
                    {
                        string group_name = DwGroup.GetItemString(1, "user_name");
                        DwUtil.RetrieveDataWindow(DwUsers, "ad_user.pbl", null, group_name, state.SsCoopId, "%" + TextBox1.Text + "%");
                        test = DwUsers.GetItemString(1, "user_name");
                    }
                    catch { DwUsers.Reset(); }
                }
            }
            catch
            {
                DwGroup.InsertRow(0);
                DwGroupUsers.Reset();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรหัสกลุ่มในระบบ");
            }

        }


        public void SaveWebSheet()
        {
            n_adminClient adminService = wcf.NAdmin;
            string group_name = DwGroup.GetItemString(1, "user_name");
            string user_name = "";
            
            for (int i = 1; i <= DwGroupUsers.RowCount; i++)
            {
                try
                {
                    user_name = DwGroupUsers.GetItemString(i, "user_name");
                    string savegroupusers = "insert into amsecgroupings (group_name,user_name) values ('" + group_name + "','" + user_name + "')";
                    Sdt insertuser = WebUtil.QuerySdt(savegroupusers);
                    try
                    {
                        Boolean result = SaveUserPermissChange(user_name, state.SsCoopId);
                    }
                    catch { Boolean result = SaveUserPermissChange(user_name, state.SsCoopId); }
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
                catch
                {

                }
            }
        }
        public Boolean SaveUserPermissChange(String User_Name, String Coop_id)
        {
            Boolean result = false;
            string application = "", sqlapp = "", sqlinsertapp = "";
            string sqlpermissall = "", sqlinsertwins = "", sqlupdatewins = "", window_id = "";
            string save_status = "0", check_flag = "0", sqlupdatewins0 = "", sqlupdate0 = "";
            try
            {
                sqlapp = "select ua.application as application from amsecgroupings gp left join amsecuseapps ua on(gp.group_name = ua.user_name) " +
                    " where gp.user_name = '" + User_Name + "'  and ua.coop_id='" + Coop_id + "' group by ua.application";

                Sdt dt = WebUtil.QuerySdt(sqlapp);
                while (dt.Next())
                {
                    try
                    {
                        application = dt.GetString("application");
                        sqlinsertapp = "insert into amsecuseapps (coop_id,user_name,application,coop_control) values " +
                            " ('" + Coop_id + "','" + User_Name + "','" + application + "','" + Coop_id + "')";
                        WebUtil.ExeSQL(sqlinsertapp);
                    }
                    catch { }
                    //Set ให้เป็น 0 ก่อน
                    sqlupdatewins0 = "update amsecpermiss set check_flag=" + check_flag + ",save_status=0" +
                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and application='" + application + "'";
                    WebUtil.ExeSQL(sqlupdatewins0);
                    //
                    try
                    {
                        sqlpermissall = "select distinct pm.window_id as win,pm.save_status as save_sta,pm.check_flag as flag " +
                                    "from amsecgroupings gp left join amsecuseapps ua on(gp.group_name = ua.user_name) " +
                                    "left join amsecpermiss pm on (ua.application = pm.application and gp.group_name = pm.user_name) " +
                                    "where gp.user_name = '" + User_Name + "' " +
                                   " and ua.coop_id='" + Coop_id + "' and pm.application='" + application + "'";
                        Sdt dt2 = WebUtil.QuerySdt(sqlpermissall);
                        while (dt2.Next())
                        {
                            window_id = dt2.GetString("win");
                            save_status = dt2.GetString("save_sta");
                            check_flag = dt2.GetString("flag");
                            try
                            {
                                sqlinsertwins = "insert into amsecpermiss (coop_id,user_name,application,window_id,save_status,check_flag,coop_control) " +
                                    " values ('" + Coop_id + "','" + User_Name + "','" + application + "','" + window_id + "','" + save_status + "','" + check_flag + "','" + Coop_id
                                    + "')";
                                WebUtil.ExeSQL(sqlinsertwins);
                            }
                            catch
                            {
                                if (save_status == "0" && check_flag != "0")
                                {
                                    sqlupdatewins = "update amsecpermiss set check_flag=" + check_flag +
                                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and window_id='" + window_id + "'";
                                    WebUtil.ExeSQL(sqlupdatewins);
                                }
                                else if (save_status != "0" && check_flag == "0")
                                {
                                    sqlupdatewins = "update amsecpermiss set save_status=" + save_status +
                                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and window_id='" + window_id + "'";
                                    WebUtil.ExeSQL(sqlupdatewins);
                                }
                                else if (save_status == "1" && check_flag == "1")
                                {
                                    sqlupdatewins = "update amsecpermiss set save_status=" + save_status + ",check_flag=" + check_flag +
                                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and window_id='" + window_id + "'";
                                    WebUtil.ExeSQL(sqlupdatewins);
                                }
                            }
                        }
                    }
                    catch { }
                    //amsecreportpermiss
                    //Set ให้เป็น 0 ทั้งหมดก่อน
                    sqlupdate0 = "update amsecreportpermiss set check_flag= 0 " +
                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and group_id in (select distinct group_id from webreportgroup where application ='" + application + "')";
                    WebUtil.ExeSQL(sqlupdate0);
                    //
                    try
                    {
                        string report_id = "", group_id = "";
                        string sqlreport = @"select rp.report_id as r , rp.group_id as g  , rp.check_flag as flag 
                        from amsecgroupings gp left join amsecreportpermiss rp on (gp.group_name = rp.user_name) where gp.user_name ='" + User_Name + @"' and rp.coop_id='" +
                        Coop_id + @"'  and   rp.group_id in (select distinct group_id from webreportgroup where application ='" + application + "')";
                        Sdt re = WebUtil.QuerySdt(sqlreport);
                        while (re.Next())
                        {
                            report_id = re.GetString("r");
                            group_id = re.GetString("g");
                            check_flag = re.GetString("flag");
                            try
                            {
                                sqlinsertwins = "insert into amsecreportpermiss (coop_id,user_name,group_id,report_id,check_flag) " +
                                    " values ('" + Coop_id + "','" + User_Name + "','" + group_id + "','" + report_id + "','" + check_flag + "')";
                                WebUtil.ExeSQL(sqlinsertwins);
                            }
                            catch
                            {
                                if (check_flag != "0")
                                {
                                    sqlupdatewins = "update amsecreportpermiss set check_flag=" + check_flag +
                                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and report_id='" + report_id + "'";
                                    WebUtil.ExeSQL(sqlupdatewins);
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                WebUtil.ExeSQL("commit");
            }
            catch (Exception ex)
            {
                WebUtil.ExeSQL("rollback");
                throw ex;
            }
            return result;
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
            //------------------------------------------------------------
            n_adminClient adminService = wcf.NAdmin;
            string group_name = DwGroup.GetItemString(1, "user_name");
            //------------------------------------------------------------
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
                //------------------------------------------------------------
                try
                {
                    string deletefromgroup = "delete amsecgroupings where group_name='" + group_name + "' and user_name='" + user_name + "'";
                    Sdt de = WebUtil.QuerySdt(deletefromgroup);

                    //LtServerMessage.Text = WebUtil.CompleteMessage("ลบผู้ใช้ออกจากกลุ่มสำเร็จ");
                }
                catch {
                    //LtServerMessage.Text = WebUtil.ErrorMessage("ลบผู้ใช้ออกจากลุ่มไม่สำเร็จ " + ex); 
                }
                //------------------------------------------------------------
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
               string group_name = DwGroup.GetItemString(1, "user_name");
               DwUtil.RetrieveDataWindow(DwUsers, "ad_user.pbl", null, group_name, state.SsCoopId, "%" + TextBox1.Text + "%");
               string test = DwUsers.GetItemString(1, "user_name");
           }
           catch { DwUsers.Reset(); }
       }
    }
}