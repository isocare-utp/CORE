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
    public partial class w_sheet_ad_premission : PageWebSheet, WebSheet
    {
        protected String jsinitPageSearch;
        protected String jsaddappname;
        protected String jsRemove;
        protected String jsSearch;
        protected String jsSelectAll;
        Sta ta;
        private String pbl = "ad_user.pbl";


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
            if (!IsPostBack)
            {
                HdSelectAll.Value = "0";
                DwApplication.InsertRow(0);
                DwButtom.InsertRow(0);
                DwUser.InsertRow(0);
                DwApplication.Reset();
            }
            else
            {
                this.RestoreContextDw(DwApplication);
                this.RestoreContextDw(DwPermiss);
                this.RestoreContextDw(DwButtom);
                this.RestoreContextDw(DwUser);
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
            string user_name = "";
            string application = "";
            string window_id = "";
            decimal save_status = 0;
            decimal check_flag = 0;
            string sqlChk = "", sqlIns = "", sqlUpdate = "";
            try
            {
                user_name = DwUser.GetItemString(1, "user_name");
                application = DwApplication.GetItemString(Convert.ToInt32(HdRow.Value), "amsecuseapps_application");
            }
            catch
            { }
            try
            {
                int count = DwPermiss.RowCount;
                for (int i = 1; i <= count; i++)
                {
                    window_id = DwPermiss.GetItemString(i, "amsecwins_window_id");
                    save_status = DwPermiss.GetItemDecimal(i, "amsecpermiss_save_status");
                    check_flag = DwPermiss.GetItemDecimal(i, "amsecpermiss_check_flag");
                    sqlChk = "select * from amsecpermiss where user_name={0} and window_id={1} and coop_id={2} and application={3} and coop_control={4}";
                    sqlChk = WebUtil.SQLFormat(sqlChk, user_name, window_id, state.SsCoopId, application,state.SsCoopControl);
                    Sdt dtC = WebUtil.QuerySdt(sqlChk);
                    if (dtC.Next())
                    {
                        sqlUpdate = "update amsecpermiss set save_status={0},check_flag={1} where coop_id={2} and user_name={3} and application={4} and window_id={5} and coop_control={6}";
                        sqlUpdate = WebUtil.SQLFormat(sqlUpdate, save_status, check_flag, state.SsCoopId, user_name, application, window_id, state.SsCoopControl);
                        Sdt dt = WebUtil.QuerySdt(sqlUpdate);
                    }
                    else
                    {
                        sqlIns = @"insert into amsecpermiss(coop_id,user_name,application,window_id,save_status,check_flag,coop_control)
                                values ({0},{1},{2},{3},{4},{5},{6})";
                        sqlIns = WebUtil.SQLFormat(sqlIns, state.SsCoopId, user_name, application, window_id, save_status, check_flag, state.SsCoopControl);
                        Sdt dtI = WebUtil.QuerySdt(sqlIns);
                    }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จเนื่องจาก : " + ex); }
        }
        public void WebSheetLoadEnd()
        {
            DwApplication.SaveDataCache();
            DwPermiss.SaveDataCache();
            DwButtom.SaveDataCache();
            DwUser.SaveDataCache();
        }
        public void PageSearch()
        {
            try
            {
                HdSelectAll.Value = "0";
                string UserName = DwUser.GetItemString(1, "user_name");
                DwPermiss.InsertRow(0);
                DwPermiss.Reset();


                DwUtil.RetrieveDataWindow(DwPermiss, pbl, null, AppID.Value, UserName);       //Test
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
            string UserName = DwUser.GetItemString(1, "user_name");
            n_adminClient adminService = wcf.NAdmin;

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
                    DwApplication.SetItemString(row + 1, "amappstatus_description", HdDesc.Value);
                    DwApplication.SetItemString(row + 1, "amsecuseapps_user_name", UserName);
                }
                catch
                {
                }
                HdSave.Value = "ADD";

                ////Save App
                try
                {
                    Boolean result = SaveAppWins(app_name, UserName, state.SsCoopId);
                    result = SaveAppReport(app_name, UserName, state.SsCoopId);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("Error AddAppName :" + ex.Message);
                }
            }
            else
                LtServerMessage.Text = WebUtil.WarningMessage("Application ที่เพิ่มเข้ามาใหม่มีอยู่แล้ว");
        }
        public Boolean SaveAppWins(String Application, String User_Name, String Coop_id)
        {
            Boolean result = false;
            try
            {
                string sql = "", sql2 = "", sql3 = "";
                string window_id = "";
                ExecuteDataSource exed = new ExecuteDataSource(this);
                sql3 = "insert into amsecuseapps (coop_id,application,user_name,coop_control)  values ({0},{1},{2},{3})";
                sql3 = WebUtil.SQLFormat(sql3, state.SsCoopId, Application, User_Name, state.SsCoopControl);
                exed.SQL.Add(sql3);
                sql = "select window_id from amsecwins where application='" + Application + "' and used_flag=1";
                Sdt dt = WebUtil.QuerySdt(sql);
                while (dt.Next())
                {
                    window_id = dt.GetString("window_id");
                    sql2 = "insert into amsecpermiss (coop_id,user_name,application,window_id,save_status,check_flag,coop_control) values ({0},{1},{2},{3},{4},{5},{6})";
                    sql2 = WebUtil.SQLFormat(sql2, state.SsCoopId, User_Name, Application, window_id, 0, 0, state.SsCoopControl);
                    exed.SQL.Add(sql2);
                }
                exed.Execute();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception("SaveAppWins " + ex.Message);
            }
            return result;
        }
        public Boolean SaveAppReport(String Application, String User_Name, String Coop_id)
        {
            Boolean result = false;
            try
            {
                string sql2 = "";
                string report_id = "", group_id = "";
                ExecuteDataSource exed = new ExecuteDataSource(this);

                string sql = "select report_id,group_id from webreportdetail where group_id in (select distinct group_id from webreportgroup where application='" + Application + "')";
                Sdt dt = WebUtil.QuerySdt(sql);
                while (dt.Next())
                {
                    report_id = dt.GetString("report_id");
                    group_id = dt.GetString("group_id");
                    sql2 = "insert into amsecreportpermiss (coop_id,user_name,group_id,report_id,check_flag) values ({0},{1},{2},{3},{4})";
                    sql2 = WebUtil.SQLFormat(sql2,state.SsCoopId,User_Name,group_id,report_id,0);
                    exed.SQL.Add(sql2);
                }
                exed.Execute();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception("SaveAppReport " + ex.Message);
            }
            return result;
        }
        public void RemoveAppName()
        {
            //ลบ useapps อัพเดท permiss check_flag เป็น 0 
            string app_name = "";
            string UserName = "";
            n_adminClient adminService = wcf.NAdmin;
            try
            {
                app_name = DwApplication.GetItemString(Convert.ToInt32(HdRow.Value), "amsecuseapps_application");
                UserName = DwUser.GetItemString(1, "user_name");
                Boolean result = RemoveAppWins(app_name, UserName, state.SsCoopId);
                result = RemoveAppReport(app_name, UserName, state.SsCoopId);
            }
            catch
            {
                Boolean result = RemoveAppWins(app_name, UserName, state.SsCoopId);
                result = RemoveAppReport(app_name, UserName, state.SsCoopId);
            }
            DwApplication.DeleteRow(Convert.ToInt32(HdRow.Value));
            HdRow.Value = "";
            DwPermiss.Reset();
        }
        public Boolean RemoveAppReport(String Application, String User_Name, String Coop_id)
        {
            Boolean result = false;
            try
            {
                string sql2 = "";
                sql2 = "delete from amsecreportpermiss where group_id in (select distinct group_id from webreportgroup where  application='" + Application + "' and user_name='" + User_Name + "' and coop_id='" + Coop_id + "') and user_name='" + User_Name + "' and coop_id='" + Coop_id + "'";
                WebUtil.QuerySdt(sql2);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;

        }
        public Boolean RemoveAppWins(String Application, String User_Name, String Coop_id)
        {
            Boolean result = false;
            try
            {
                string sql = "delete from amsecuseapps where application={0} and user_name={1} and coop_id={2} and coop_control={3}";
                sql = WebUtil.SQLFormat(sql, Application, User_Name, state.SsCoopId, state.SsCoopControl);
                WebUtil.QuerySdt(sql);

                string sql2 = "delete from amsecpermiss  where application={0} and user_name={1} and coop_id={2} and coop_control={3}";
                sql2 = WebUtil.SQLFormat(sql2, Application, User_Name, state.SsCoopId, state.SsCoopControl);
                WebUtil.QuerySdt(sql2);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;

        }
        public void JsSearch()
        {
            string UserName = DwUser.GetItemString(1, "user_name");

            try
            {
                HdSelectAll.Value = "0";
                DwUtil.RetrieveDataWindow(DwUser, pbl, null, UserName, state.SsCoopId);
                string TestRetrieve = DwUser.GetItemString(1, "user_name");
                try
                {
                    DwUtil.RetrieveDataWindow(DwApplication, pbl, null, UserName, state.SsCoopId); //keng เพิ่ม Argument state.SsCoopId เพราะใน data window รับ coop_id ด้วยแต่ไม่ได้ส่งไป
                    HdSave.Value = "EDIT";
                    string apptest = DwApplication.GetItemString(1, "amsecuseapps_application");
                }
                catch { DwApplication.Reset(); }
            }
            catch
            {
                DwUser.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบรหัสผู้ใช้ในระบบ");

            }

        }
        public void JsSelectAll()
        {

            try
            {
                int r = DwPermiss.RowCount;
                if (HdSelectAll.Value == "0")
                {
                    for (int i = 1; i < r + 1; i++)
                    {
                        DwPermiss.SetItemDecimal(i, "amsecpermiss_check_flag", 1);
                        DwPermiss.SetItemDecimal(i, "amsecpermiss_save_status", 1);
                    }
                    HdSelectAll.Value = "1";
                }
                else
                {
                    for (int i = 1; i < r + 1; i++)
                    {
                        DwPermiss.SetItemDecimal(i, "amsecpermiss_check_flag", 0);
                        DwPermiss.SetItemDecimal(i, "amsecpermiss_save_status", 0);
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