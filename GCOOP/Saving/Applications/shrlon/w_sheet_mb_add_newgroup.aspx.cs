using System;
using CoreSavingLibrary;
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
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_mb_add_newgroup : PageWebSheet, WebSheet
    {
        //delete and save
        private String codeid;

        protected String deleteRecord;
        protected String filterDistrict;
        protected String bSearch;
        protected String itemChangedReload;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            deleteRecord = WebUtil.JsPostBack(this, "deleteRecord");
            filterDistrict = WebUtil.JsPostBack(this, "filterDistrict");
            bSearch = WebUtil.JsPostBack(this, "bSearch");
            itemChangedReload = WebUtil.JsPostBack(this, "itemChangedReload");
        }

        public void WebSheetLoadBegin()
        {
            try { codeid = HiddenFieldID.Value.Trim(); }
            catch { codeid = ""; }

            this.ConnectSQLCA();
            DwSearch.SetTransaction(sqlca);
            DwList.SetTransaction(sqlca);

            if (IsPostBack)
            {
                DwSearch.RestoreContext();
                DwList.RestoreContext();

            }
            else
            {
                DwSearch.Reset();
                DwSearch.InsertRow(1);
                DwList.Reset();
                DwList.Retrieve();
            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "bSearch")
            {
                BSearch();
            }
            else if (eventArg == "deleteRecord") {
                DeleteRecord();
            }
            else if (eventArg == "itemChangedReload") {

            }
            else if (eventArg == "filterDistrict") {
                FilterDistrict();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                DwList.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                DwList.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาดในการบันทึกข้อมูล");
                codeid = "";
            }
        }

        public void WebSheetLoadEnd()
        {

            String s_id = "";
            String s_desc = "";
            try
            {
                s_id = DwSearch.GetItemString(1, "membgroup_code").Trim();
            }
            catch { s_id = ""; }
            try
            {
                s_desc = DwSearch.GetItemString(1, "membgroup_desc").Trim();
            }
            catch { s_desc = ""; }
            if (s_id!="" && s_desc!="")
            { BSearch(); }

            try
            {
                String district = DwList.GetItemString(1, "membgroup_distinct");
                FilterDistrict();
            }
            catch { }
        }

        #endregion

        private void BSearch()
        {
            try
            {
                String s_id = "";
                String s_desc = "";
                String sql_filter = "";
                try
                {
                    s_id = DwSearch.GetItemString(1, "membgroup_code").Trim();
                }
                catch { s_id = ""; }
                try
                {
                    s_desc = DwSearch.GetItemString(1, "membgroup_desc").Trim();
                }
                catch { s_desc = ""; }

                if (s_id != "" && s_desc != "")
                {
                    sql_filter = "membgroup_code like '%" + s_id + "%' and membgroup_desc like '%" + s_desc + "%'";

                }
                else if (s_id != "")
                {
                    sql_filter = "membgroup_code like '%" + s_id + "%'";
                }
                else if (s_desc != "")
                {
                    sql_filter = "membgroup_desc like '%" + s_desc + "%'";
                }
                //filter from search
                DwList.Reset();
                DwList.Retrieve();
                DwList.SetFilter(sql_filter);
                DwList.Filter();
            }
            catch { LtServerMessage.Text = WebUtil.WarningMessage("ลองตรวจสอบการสืบค้นอีกครั้ง"); }

        }

        private void DeleteRecord()
        {
            String code = HiddenFieldID.Value.Trim();
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                String sql = @"delete from mbucfmembgroup where mbucfmembgroup.membgroup_code ='" + code + "'";
                try
                {
                    ta.Exe(sql);
                }
                catch(Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                DwList.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบรายการสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ลบรายการไม่สำเร็จ");
            }
            ta.Close();

            DwSearch.Reset();
            DwSearch.InsertRow(1);
        }


        /// <summary>
        /// filter ตำบล
        /// </summary>
        private void FilterDistrict()
        {
            String prov = "";
            try
            {
                prov = DwList.GetItemString(1, "membgroup_province");
                DataWindowChild dc = DwList.GetChild("membgroup_distinct");
                dc.SetTransaction(sqlca);
                dc.Retrieve();
                dc.SetFilter("province_code ='" + prov + "'");
                dc.Filter();
            }
            catch
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ลองเลือกจังหวัดใหม่อีกครั้ง");
            }
        }

    }
}
