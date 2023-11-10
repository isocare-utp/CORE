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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using Sybase.DataWindow;
using System.Web.Services.Protocols;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_premission_report : PageWebSheet, WebSheet
    {
        protected String jsinitPageSearch;
        protected String jsaddappname;
        protected String jsRemove;
        protected String jsSearch;
        protected String jsSelectAll;
        private String pbl = "ad_user.pbl";


        public void InitJsPostBack()
        {
            jsinitPageSearch = WebUtil.JsPostBack(this, "jsinitPageSearch");
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            jsSelectAll = WebUtil.JsPostBack(this, "jsSelectAll");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwApplication.InsertRow(0);
                DwUser.InsertRow(0);
                DwApplication.Reset();
            }
            else
            {
                this.RestoreContextDw(DwApplication);
                this.RestoreContextDw(DwPermiss);
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
            string report_id = "";
            decimal check_flag = 0;
            string group_id = "";
            string coop_id = state.SsCoopId;
            ExecuteDataSource exed = new ExecuteDataSource(this);

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
                    report_id = DwPermiss.GetItemString(i, "report_id");
                    check_flag = DwPermiss.GetItemDecimal(i, "check_flag");
                    group_id = DwPermiss.GetItemString(i, "group_id");
                    string sql = "";
                    string sqlchk = @"select * from amsecreportpermiss where coop_id={0} and user_name={1}  
                                        and report_id={2} and group_id={3}";
                    sqlchk = WebUtil.SQLFormat(sqlchk, coop_id, user_name, report_id, group_id);
                    Sdt dtchk = WebUtil.QuerySdt(sqlchk);
                    if (dtchk.Next())
                    {
                        sql = @"update amsecreportpermiss set check_flag={0} 
                                        where coop_id={1} and user_name={2}  
                                        and report_id={3} and group_id={4}";
                        sql = WebUtil.SQLFormat(sql, check_flag, coop_id, user_name, report_id, group_id);
                    }
                    else { 
                    sql = @"insert into amsecreportpermiss(coop_id,user_name,group_id,report_id,check_flag)
                                        values ({0},{1},{2},{3},{4})";
                        sql = WebUtil.SQLFormat(sql, coop_id, user_name, group_id, report_id, check_flag);
                    
                    }
                    exed.SQL.Add(sql);
                }

                exed.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จเนื่องจาก : " + ex); }
        }

        public void WebSheetLoadEnd()
        {
            DwApplication.SaveDataCache();
            DwPermiss.SaveDataCache();
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

                DwUtil.RetrieveDataWindow(DwPermiss, pbl, null, AppID.Value, UserName, state.SsCoopId);       //LEK เพิ่ม Argument 1 ค่า คือ state.SsCoopId
            }
            catch
            {
            }
        }
        public void JsSearch()
        {
            try
            {
                HdSelectAll.Value = "0";
                DwApplication.Reset();
                DwPermiss.Reset();
                string UserName = DwUser.GetItemString(1, "user_name");
                DwUtil.RetrieveDataWindow(DwUser, pbl, null, UserName, state.SsCoopId);
                string TestRetrieve = DwUser.GetItemString(1, "user_name");
                try
                {
                    DwUtil.RetrieveDataWindow(DwApplication, pbl, null, UserName, state.SsCoopId);  //LEK เพิ่ม Argument 1 ค่า คือ state.SsCoopId
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
                        DwPermiss.SetItemDecimal(i, "check_flag", 1);
                    }
                    HdSelectAll.Value = "1";
                }
                else
                {
                    for (int i = 1; i < r + 1; i++)
                    {
                        DwPermiss.SetItemDecimal(i, "check_flag", 0);
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