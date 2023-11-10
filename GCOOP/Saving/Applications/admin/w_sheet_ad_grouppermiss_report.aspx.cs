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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_grouppermiss_report : PageWebSheet, WebSheet
    {
        protected String jsinitPageSearch;
        protected String jsSearch;
        protected String jsSelectAll;
        private String pbl = "ad_group.pbl";
        Sta ta;
        
        public void InitJsPostBack()
        {
            jsinitPageSearch = WebUtil.JsPostBack(this, "jsinitPageSearch");
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsSelectAll = WebUtil.JsPostBack(this, "jsSelectAll");
        }

        public void WebSheetLoadBegin()
        {

            ta = new Sta(state.SsConnectionString);
            n_adminClient adminService = wcf.NAdmin;
            if (!IsPostBack)
            {
                HdSelectAll.Value = "0";
                DwApplication.InsertRow(0);
                DwGroup.InsertRow(0);
                DwApplication.Reset();
            }
            else
            {
                this.RestoreContextDw(DwGroup);
                this.RestoreContextDw(DwPagePermiss);
                this.RestoreContextDw(DwApplication);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsinitPageSearch":
                    PageSearch();
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
            try
            {
                n_adminClient adminService = wcf.NAdmin;
                //LtServerMessage.Text = WebUtil.WarningMessage("ยังไม่ได้ทำฟังก์ชั่น");
                string group_name = "";
                string application = "";
                string report_id = "";
                string group_id = "";
                decimal check_flag = 0;
                string coop_id = state.SsCoopId;
                ExecuteDataSource exed = new ExecuteDataSource(this);
                group_name = DwGroup.GetItemString(1, "user_name");
                application = DwApplication.GetItemString(Convert.ToInt32(HdRow.Value), "amsecuseapps_application");

                int count = DwPagePermiss.RowCount;
                for (int i = 1; i <= count; i++)
                {
                    report_id = DwPagePermiss.GetItemString(i, "report_id");
                    group_id = DwPagePermiss.GetItemString(i, "group_id");
                    check_flag = DwPagePermiss.GetItemDecimal(i, "check_flag");
                    //string sql = "update amsecreportpermiss set check_flag=" + check_flag
                    //    + " where coop_id='" + state.SsCoopId + "' and user_name='" + group_name
                    //+ "'  and report_id='" + report_id + "' and group_id='" + group_id + "'";
                    //Sdt dt = WebUtil.QuerySdt(sql);

                    string sql = "";
                    string sqlchk = @"select * from amsecreportpermiss where coop_id={0} and user_name={1}  
                                        and report_id={2} and group_id={3}";
                    sqlchk = WebUtil.SQLFormat(sqlchk, coop_id, group_name, report_id, group_id);
                    Sdt dtchk = WebUtil.QuerySdt(sqlchk);
                    if (dtchk.Next())
                    {
                        sql = @"update amsecreportpermiss set check_flag={0} 
                                        where coop_id={1} and user_name={2}  
                                        and report_id={3} and group_id={4}";
                        sql = WebUtil.SQLFormat(sql, check_flag, coop_id, group_name, report_id, group_id);
                    }
                    else
                    {
                        sql = @"insert into amsecreportpermiss(coop_id,user_name,group_id,report_id,check_flag)
                                        values ({0},{1},{2},{3},{4})";
                        sql = WebUtil.SQLFormat(sql, coop_id, group_name, group_id, report_id, check_flag);

                    }
                    exed.SQL.Add(sql);
                }
                exed.Execute();
                //เปลี่ยนสิทธิ์คนในกลุ่ม
                string user_name = "";
                try
                {
                    string sqluser = "select user_name from amsecgroupings where group_name='" + group_name + "'";
                    Sdt user = WebUtil.QuerySdt(sqluser);
                    while (user.Next())
                    {
                        user_name = user.GetString("user_name");
                        Boolean result = SaveUserPermissReportChange(user_name, application, state.SsCoopId);
                    }
                    //----------------------------------------------------------
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
                catch (Exception ex)
                { throw new Exception("บันทึกไม่สำเร็จ SaveUserPermissReportChange : " + ex); }
            }
            catch (Exception ex) {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จเนื่องจาก : " + ex); 
            }
        }

        public Boolean SaveUserPermissReportChange(String User_Name, string use_apps, String Coop_id)
        {
            Boolean result = false;
            string application = "", sqlapp = "", sqlinsertapp = "";
            string sqlinsertwins = "", sqlupdatewins = "", check_flag = "0", sqlupdate0 = "";
            try
            {
                application = use_apps;
                sqlapp = "select ua.application as application from amsecgroupings gp left join amsecuseapps ua on(gp.group_name = ua.user_name) " +
                    " where gp.user_name = '" + User_Name + "'  and ua.coop_id='" + Coop_id + "' group by ua.application";
                Sdt dt = ta.Query(sqlapp);
                while (dt.Next())
                {
                    try
                    {
                        application = dt.GetString("application");
                        if (application == use_apps)
                        {
                            sqlinsertapp = "insert into amsecuseapps (coop_id,user_name,application,coop_control) values " +
                                " ('" + Coop_id + "','" + User_Name + "','" + application + "','" + Coop_id + "')";
                            ta.Exe(sqlinsertapp);
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
                        string sqlreport = @"select distinct rp.report_id as report_id , rp.group_id as group_id  , rp.check_flag as flag 
                        from amsecgroupings gp left join amsecreportpermiss rp on (gp.group_name = rp.user_name) where gp.user_name ='" + User_Name + @"' and rp.coop_id='" +
                        Coop_id + @"'  and   rp.group_id in (select distinct group_id from webreportgroup where application ='" + application + "')";
                        Sdt dt8 = ta.Query(sqlreport);
                        while (dt8.Next())
                        {
                            report_id = dt8.GetString("report_id");
                            group_id = dt8.GetString("group_id");
                            check_flag = dt8.GetString("flag");
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
            DwApplication.SaveDataCache();
            DwGroup.SaveDataCache();
        }

        public void PageSearch()
        {
            try
            {
                HdSelectAll.Value = "0";
                string GroupName = DwGroup.GetItemString(1, "user_name");
                DwPagePermiss.Reset();
                DwUtil.RetrieveDataWindow(DwPagePermiss, pbl, null, GroupID.Value, GroupName);       //Test
            }
            catch
            {
            }
        }
        public void AddAppName()
        {
            n_adminClient adminService = wcf.NAdmin;
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
                    //DwApplication.SetItemString(row + 1, "amsecuseapps_branch_id", state.SsCoopId);
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
                catch { }
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
            n_adminClient adminService = wcf.NAdmin;
            string app_name = "";
            string GroupName = "";
            try
            {
                app_name = DwApplication.GetItemString(Convert.ToInt32(HdRow.Value), "amsecuseapps_application");
                GroupName = DwGroup.GetItemString(1, "user_name");
                Boolean result = RemoveGroupAppWins(app_name, GroupName, state.SsCoopId);
                result = RemoveGroupAppReport(app_name, GroupName, state.SsCoopId);
            }
            catch { }

            DwApplication.DeleteRow(Convert.ToInt32(HdRow.Value));
            HdRow.Value = "";
            DwPagePermiss.Reset();
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
        public void JsSearch()
        {
            HdSelectAll.Value = "0";
            DwApplication.Reset();
            DwPagePermiss.Reset();
            try
            {
                string GroupName = DwGroup.GetItemString(1, "user_name");
                DwUtil.RetrieveDataWindow(DwGroup, pbl, null, GroupName, state.SsCoopId);
                string TestRetrieve = DwGroup.GetItemString(1, "user_name");
                try
                {
                    DwUtil.RetrieveDataWindow(DwApplication, pbl, null, GroupName, state.SsCoopId);
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
                        DwPagePermiss.SetItemDecimal(i, "check_flag", 1);
                    }
                    HdSelectAll.Value = "1";
                }
                else
                {
                    for (int i = 1; i < r + 1; i++)
                    {
                        DwPagePermiss.SetItemDecimal(i, "check_flag", 0);
                    }
                    HdSelectAll.Value = "0";
                }
            }
            catch
            {

            }

        }

    }
}