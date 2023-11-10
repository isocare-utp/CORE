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
using Sybase.DataWindow;


namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_grouppermiss : PageWebSheet, WebSheet
    {
        protected String jsinitPageSearch;
        protected String jsaddappname;
        protected String jsRemove;
        protected String jsSearch;
        protected String jsSelectAll;
        private String pbl = "ad_group.pbl";
        Sta ta;
        private n_adminClient adminService;
        //Admin adminService = WsCalling.Admin;

        public void InitJsPostBack()
        {
            jsinitPageSearch = WebUtil.JsPostBack(this, "jsinitPageSearch");
            jsaddappname = WebUtil.JsPostBack(this, "jsaddappname");
            jsRemove = WebUtil.JsPostBack(this, "jsRemove");
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsSelectAll = WebUtil.JsPostBack(this, "jsSelectAll");
        }

        public void WebSheetLoadBegin()
        {
            
            ta = new Sta(state.SsConnectionString);
            adminService = wcf.NAdmin;
            if (!IsPostBack)
            {
                HdSelectAll.Value = "0";
                DwApplication.InsertRow(0);
                DwButtom.InsertRow(0);
                DwGroup.InsertRow(0);
                DwApplication.Reset();
                //DwUtil.RetrieveDataWindow(DwApplication, pbl, null, state.SsUsername);     //Test
            }
            else
            {
                this.RestoreContextDw(DwGroup);
                this.RestoreContextDw(DwPagePermiss); 
                this.RestoreContextDw(DwApplication);
                this.RestoreContextDw(DwButtom);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsinitPageSearch":
                    PageSearch();
                    break;
                case "jsaddappname":
                    AddAppName();
                    break;
                case "jsRemove":
                    RemoveAppName();
                    break;
                case "jsSearch":
                    JsSearch();
                    break;
                case "jsSelectAll":
                    JsSelectAll();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            string group_name = "";
            string application = "";
            string window_id = "";
            decimal save_status = 0;
            decimal check_flag = 0;
            string sqlChk = "", sqlUpdate = "", sqlIns = "";
            try
            {
                group_name = DwGroup.GetItemString(1, "user_name");
                application = DwApplication.GetItemString(Convert.ToInt32(HdRow.Value), "amsecuseapps_application");
            }
            catch
            { }
            try
            {
                int count = DwPagePermiss.RowCount;
                for (int i = 1; i <= count; i++)
                {
                    window_id = DwPagePermiss.GetItemString(i, "amsecwins_window_id");
                    save_status = DwPagePermiss.GetItemDecimal(i, "amsecpermiss_save_status");
                    check_flag = DwPagePermiss.GetItemDecimal(i, "amsecpermiss_check_flag");
                    sqlChk = "select * from amsecpermiss where user_name={0} and window_id={1} and coop_id={2} and application={3}";
                    sqlChk = WebUtil.SQLFormat(sqlChk, group_name, window_id, state.SsCoopControl, application);
                    Sdt dtC = WebUtil.QuerySdt(sqlChk);
                    if (dtC.Next())
                    {
                        sqlUpdate = @"update amsecpermiss set save_status={0},check_flag={1} 
                        where coop_id={2} and user_name={3} and application={4} and window_id={5}";
                        sqlUpdate = WebUtil.SQLFormat(sqlUpdate, save_status, check_flag, state.SsCoopId, group_name, application, window_id);
                        Sdt dt = WebUtil.QuerySdt(sqlUpdate);
                    }
                    else
                    {
                        sqlIns = @"insert into amsecpermiss(coop_id,user_name,application,window_id,save_status,check_flag,coop_control)
                                values ({0},{1},{2},{3},{4},{5},{6})";
                        sqlIns = WebUtil.SQLFormat(sqlIns, state.SsCoopId, group_name, application, window_id, save_status, check_flag, state.SsCoopControl);
                        Sdt dtI = WebUtil.QuerySdt(sqlIns);
                    }
                }
                //เปลี่ยนสิทธิ์คนในกลุ่ม
                string user_name = "";
                string sqldelete = "";
                try
                {
                    string sqluser = "select user_name from amsecgroupings where group_name='" + group_name + "'";
                    Sdt user = WebUtil.QuerySdt(sqluser);
                    while(user.Next())
                    {
                        user_name = user.GetString("user_name");
                        try
                        {
                            sqldelete = "delete from amsecuseapps where user_name='"+user_name+"' and coop_id='"+state.SsCoopId+"'";
                            Sdt delete = WebUtil.QuerySdt(sqldelete);
                            Boolean result = SaveUserPermissChange(user_name, state.SsCoopId);

                        }
                        catch { Boolean result = SaveUserPermissChange(user_name, state.SsCoopId); }
                    }
                }
                catch { }
                //----------------------------------------------------------
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จเนื่องจาก : " + ex); }
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
                Sdt dt = ta.Query(sqlapp);
                while (dt.Next())
                {
                    try
                    {
                        application = dt.GetString("application");
                        sqlinsertapp = "insert into amsecuseapps (coop_id,user_name,application,coop_control) values " +
                            " ('" + Coop_id + "','" + User_Name + "','" + application + "','" + Coop_id + "')";
                        ta.Exe(sqlinsertapp);
                    }
                    catch { }
                    //Set ให้เป็น 0 ก่อน
                    sqlupdatewins0 = "update amsecpermiss set check_flag=" + check_flag + ",save_status=0" +
                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and application='" + application + "'";
                    ta.Exe(sqlupdatewins0);
                    //
                    try
                    {
                        sqlpermissall = "select distinct pm.window_id as win,pm.save_status as save_sta,pm.check_flag as flag " +
                                    "from amsecgroupings gp left join amsecuseapps ua on(gp.group_name = ua.user_name) " +
                                    "left join amsecpermiss pm on (ua.application = pm.application and gp.group_name = pm.user_name) " +
                                    "where gp.user_name = '" + User_Name + "' " +
                                   " and ua.coop_id='" + Coop_id + "' and pm.application='" + application + "'";
                        Sdt dt2 = ta.Query(sqlpermissall);
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
                                ta.Exe(sqlinsertwins);
                            }
                            catch
                            {
                                if (save_status == "0" && check_flag != "0")
                                {
                                    sqlupdatewins = "update amsecpermiss set check_flag=" + check_flag +
                                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and window_id='" + window_id + "'";
                                    ta.Exe(sqlupdatewins);
                                }
                                else if (save_status != "0" && check_flag == "0")
                                {
                                    sqlupdatewins = "update amsecpermiss set save_status=" + save_status +
                                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and window_id='" + window_id + "'";
                                    ta.Exe(sqlupdatewins);
                                }
                                else if (save_status == "1" && check_flag == "1")
                                {
                                    sqlupdatewins = "update amsecpermiss set save_status=" + save_status + ",check_flag=" + check_flag +
                                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and window_id='" + window_id + "'";
                                    ta.Exe(sqlupdatewins);
                                }
                            }
                        }
                    }
                    catch { }
                    //amsecreportpermiss
                    //Set ให้เป็น 0 ทั้งหมดก่อน
                    sqlupdate0 = "update amsecreportpermiss set check_flag= 0 " +
                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and group_id in (select distinct group_id from webreportgroup where application ='" + application + "')";
                    ta.Exe(sqlupdate0);
                    //
                    try
                    {
                        string report_id = "", group_id = "";
                        string sqlreport = @"select rp.report_id as r , rp.group_id as g  , rp.check_flag as flag 
                        from amsecgroupings gp left join amsecreportpermiss rp on (gp.group_name = rp.user_name) where gp.user_name ='" + User_Name + @"' and rp.coop_id='" +
                        Coop_id + @"'  and   rp.group_id in (select distinct group_id from webreportgroup where application ='" + application + "')";
                        Sdt re = ta.Query(sqlreport);
                        while (re.Next())
                        {
                            report_id = re.GetString("r");
                            group_id = re.GetString("g");
                            check_flag = re.GetString("flag");
                            try
                            {
                                sqlinsertwins = "insert into amsecreportpermiss (coop_id,user_name,group_id,report_id,check_flag) " +
                                    " values ('" + Coop_id + "','" + User_Name + "','" + group_id + "','" + report_id + "','" + check_flag + "')";
                                ta.Exe(sqlinsertwins);
                            }
                            catch
                            {
                                if (check_flag != "0")
                                {
                                    sqlupdatewins = "update amsecreportpermiss set check_flag=" + check_flag +
                                    " where coop_id='" + Coop_id + "' and user_name='" + User_Name + "' and report_id='" + report_id + "'";
                                    ta.Exe(sqlupdatewins);
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                ta.Commit();
                ta.Close();
            }
            catch (Exception ex)
            {
                ta.RollBack();
                ta.Close();
                throw ex;
            }
            return result;
        }
        public void WebSheetLoadEnd()
        {
            DwPagePermiss.SaveDataCache();
            DwButtom.SaveDataCache();
            DwApplication.SaveDataCache();
            DwGroup.SaveDataCache();
        }

        public void PageSearch()
        {
            try
            {
                HdSelectAll.Value = "0";
                string GroupName = DwGroup.GetItemString(1, "user_name");
                //DwPagePermiss.Reset();
                //string xmlGetPageGroup = adminService.GetUserPages(state.SsWsPass, GroupName, HdApp.Value, state.SsCoopId);
                //DwPagePermiss.ImportString(xmlGetPageGroup, FileSaveAsType.Xml);
                DwUtil.RetrieveDataWindow(DwPagePermiss, pbl, null, HdApp.Value, GroupName);       
            }
            catch
            {
            }
        }
        public void AddAppName()
        {
            int row = DwApplication.RowCount;
            int check = 1;
            string app_name = HdNew.Value;
            string GroupName = DwGroup.GetItemString(1, "user_name");
            for (int i = 1; i <= row; i++)
            {
                try
                {
                    if (DwApplication.GetItemString(i, "amsecuseapps_application") == app_name)
                        check = 0;
                }
                catch { }
            }
            if (check == 1)
            {
                try
                {
                    DwApplication.InsertRow(0);
                    DwApplication.SetItemString(row + 1, "amsecuseapps_application", app_name);
                    DwApplication.SetItemString(row + 1, "amsecuseapps_coop_id", state.SsCoopId);
                    DwApplication.SetItemString(row + 1, "amsecuseapps_user_name", GroupName);
                    DwApplication.SetItemString(row + 1, "amappstatus_description", HdDesc.Value);
                }
                catch
                {
                }
                try
                {
                    Boolean result = SaveGroupAppWins(app_name, GroupName, state.SsCoopId);
                    result = SaveGroupAppReport(app_name, GroupName, state.SsCoopId);
                }
                catch { Boolean result = SaveGroupAppWins(app_name, GroupName, state.SsCoopId);
                result = SaveGroupAppReport(app_name, GroupName, state.SsCoopId);
                }
            }
            else
                LtServerMessage.Text = WebUtil.WarningMessage("Application ที่เพิ่มเข้ามาใหม่มีอยู่แล้ว");
        }
        public Boolean SaveGroupAppReport(String Application, String Group_Name, String Coop_id)
        {
            Boolean result = false;
            try
            {
                string sql2 = "";
                string report_id = "", group_id = "";
                try
                {
                    string sql = "select report_id,group_id from webreportdetail where group_id in (select distinct group_id from webreportgroup where application='" + Application + "')";
                    Sdt dt = ta.Query(sql);
                    while (dt.Next())
                    {
                        try
                        {
                            report_id = dt.GetString("report_id");
                            group_id = dt.GetString("group_id");
                            sql2 = "insert into amsecreportpermiss (coop_id,user_name,group_id,report_id,check_flag) "
                                + "values ('" + Coop_id + "','" + Group_Name + "','" + group_id + "','" + report_id + "',0)";
                            ta.Exe(sql2);
                        }
                        catch { }
                    }
                }
                catch { }
                ta.Commit();
                ta.Close();
                ta.Close();
            }
            catch (Exception ex)
            {
                ta.RollBack();
                ta.Close();
                throw ex;
            }
            return result;
        }
        public Boolean SaveGroupAppWins(String Application, String Group_Name, String Coop_id)
        {
            Boolean result = false;
            try
            {
                string sql3 = "";
                string window_id = "";
                string sql2 = "";
                try
                {
                    sql3 = "insert into amsecuseapps (coop_id,application,user_name,coop_control) " +
                    " values ('" + Coop_id + "','" + Application + "','" + Group_Name + "','" + Coop_id + "')";
                    ta.Exe(sql3);
                }
                catch { }
                try
                {
                    string sql = "select window_id from amsecwins where application='" + Application + "'";
                    Sdt dt = ta.Query(sql);
                    while (dt.Next())
                    {
                        try
                        {
                            window_id = dt.GetString("window_id");
                            sql2 = "insert into amsecpermiss (coop_id,user_name,application,window_id,save_status,check_flag,coop_control) "
                                + "values ('" + Coop_id + "','" + Group_Name + "','" + Application + "','" + window_id + "',0,0,'" + Coop_id + "')";
                            ta.Exe(sql2);
                        }
                        catch { }
                    }
                }
                catch { }

                ta.Commit();
                ta.Close();
            }
            catch (Exception ex)
            {
                ta.RollBack();
                ta.Close();
                throw ex;
            }
            return result;
        }
        public void RemoveAppName()
        {

            string app_name = "";
            string GroupName = "";
            try
            {
                app_name = DwApplication.GetItemString(Convert.ToInt32(HdRow.Value), "amsecuseapps_application");
                GroupName = DwGroup.GetItemString(1, "user_name");
                Boolean result = RemoveGroupAppWins(app_name, GroupName, state.SsCoopId);
                result = RemoveGroupAppReport(app_name, GroupName, state.SsCoopId);
            }
            catch { Boolean result = RemoveGroupAppWins(app_name, GroupName, state.SsCoopId);
            result = RemoveGroupAppReport(app_name, GroupName, state.SsCoopId);
            }

            DwApplication.DeleteRow(Convert.ToInt32(HdRow.Value));
            HdRow.Value = "";
            DwPagePermiss.Reset();
        }
        public Boolean RemoveGroupAppReport(String Application, String Group_Name, String Coop_id)
        {
            Boolean result = false;
            try
            {
                string sql2 = "";
                try
                {
                    sql2 = "update amsecreportpermiss set check_flag=0 where group_id in (select distinct group_id from webreportgroup where  application='" + Application + "' and user_name='" + Group_Name + "' and coop_id='" + Coop_id +
                        "')  and ( user_name in (select user_name from amsecgroupings where group_name ='" + Group_Name + "') or user_name ='" + Group_Name + "')";
                    ta.Exe(sql2);
                }
                catch { }
                ta.Commit();
                ta.Close();
            }
            catch (Exception ex)
            {
                ta.RollBack();
                ta.Close();
                throw ex;
            }
            return result;
        }
        public Boolean RemoveGroupAppWins(String Application, String Group_Name, String Coop_id)
        {
            Boolean result = false;
            try
            {
                string sql2 = "";
                try
                {
                    string sql = "delete from amsecuseapps where application='" + Application + "' and user_name='" + Group_Name + "' and coop_id='" + Coop_id + "'";
                    ta.Exe(sql);
                }
                catch { }
                try
                {
                    sql2 = "update amsecpermiss set check_flag=0,save_status=0 where application='" + Application + "' and user_name='" + Group_Name + "' and coop_id='" + Coop_id + "'";
                    ta.Exe(sql2);
                }
                catch { }
                ta.Commit();
                ta.Close();
            }
            catch (Exception ex)
            {
                ta.RollBack();
                ta.Close();
                throw ex;
            }
            return result;
        }
        public void JsSearch()
        {
            HdSelectAll.Value = "0";
            string GroupName = DwGroup.GetItemString(1, "user_name");
            DwApplication.Reset();
            try
            {
                DwUtil.RetrieveDataWindow(DwGroup, pbl, null, GroupName, state.SsCoopId);
                string TestRetrieve = DwGroup.GetItemString(1, "user_name");
                try
                {
                    DwUtil.RetrieveDataWindow(DwApplication, pbl,null,GroupName,state.SsCoopId);
                    //string xmlGetUserApp = adminService.GetGroupApps(state.SsWsPass, GroupName, state.SsCoopId);
                    //DwApplication.ImportString(xmlGetUserApp, FileSaveAsType.Xml);
                    string apptest = DwApplication.GetItemString(1, "amsecuseapps_application");
                }
                catch { DwApplication.Reset(); }

            }
            catch
            {
                DwGroup.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรหัสกลุ่มในระบบ");

            }

        }
        public void JsSelectAll()
        {

            try
            {
                int r = DwPagePermiss.RowCount;
                if (HdSelectAll.Value == "0")
                {
                    for (int i = 1; i < r + 1; i++)
                    {
                        DwPagePermiss.SetItemDecimal(i, "amsecpermiss_check_flag", 1);
                        DwPagePermiss.SetItemDecimal(i, "amsecpermiss_save_status", 1);
                    }
                    HdSelectAll.Value = "1";
                }
                else
                {
                    for (int i = 1; i < r + 1; i++)
                    {
                        DwPagePermiss.SetItemDecimal(i, "amsecpermiss_check_flag", 0);
                        DwPagePermiss.SetItemDecimal(i, "amsecpermiss_save_status", 0);
                    }
                    HdSelectAll.Value = "0";
                }
            }
            catch
            {

            }

        }

        //
    }
}