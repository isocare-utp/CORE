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
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_cnt_savedisk : PageWebSheet, WebSheet
    {
        public String pbl = "kp_cnt_savedisk_type.pbl";
        protected String postNewClear;
        protected String postInit;
        protected String PostCheckGroup;
        protected String PostCheckType;

        //===========================================
        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            PostCheckGroup = WebUtil.JsPostBack(this, "PostCheckGroup");
            PostCheckType = WebUtil.JsPostBack(this, "PostCheckType");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_disktype.SetTransaction(sqlca);
            Dw_membgroup.SetTransaction(sqlca);
            Dw_membtype.SetTransaction(sqlca);

            try
            {
                if (!IsPostBack)
                {
                    JspostNewClear();
                }
                else
                {
                    this.RestoreContextDw(Dw_disktype);
                    this.RestoreContextDw(Dw_membgroup);
                    this.RestoreContextDw(Dw_membtype);
                }
            }
            catch { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postInit")
            {
                JspostInit();
            }
            else if (eventArg == "PostCheckGroup")
            {
                int li_group = Convert.ToInt32(CheckGroupAll.Checked);
                if (li_group == 1)
                {
                    for (int i = 1; i <= Dw_membgroup.RowCount; i++)
                    {
                        Dw_membgroup.SetItemDecimal(i, "operate_flag", 1);
                    }
                }
                else
                {
                    for (int i = 1; i <= Dw_membgroup.RowCount; i++)
                    {
                        Dw_membgroup.SetItemDecimal(i, "operate_flag", 0);
                    }                
                }
            }
            else if (eventArg == "PostCheckType")
            {
                int li_type = Convert.ToInt32(CheckTypeAll.Checked);
                if (li_type == 1)
                {
                    for (int i = 1; i <= Dw_membtype.RowCount; i++)
                    {
                        Dw_membtype.SetItemDecimal(i, "operate_flag", 1);
                    }
                }
                else
                {
                    for (int i = 1; i <= Dw_membtype.RowCount; i++)
                    {
                        Dw_membtype.SetItemDecimal(i, "operate_flag", 0);
                    }
                }
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                Sta ta = new Sta(sqlca.ConnectionString);
                string sql = "";
                string membgroup_code = "";
                string membtype_code = "";

                string disk_code = lbl_diskcode.Text.Trim();
                if (lbl_diskcode.Text != "")
                {
                    DwUtil.UpdateDataWindow(Dw_disktype, pbl, "kpcfdisk");
                    //Dw_disktype.UpdateData();
                    //ลบข้อมูล mbcfmembgroup
                    sql = "delete  from kpcfdiskmembgroup where disk_code = '" + disk_code + "'";
                    ta.Exe(sql);
                    //กรองเอาเฉพาะ ที่ เลือกไว้
                    Dw_membgroup.SetFilter("operate_flag = 1");
                    Dw_membgroup.Filter();

                    if (Dw_membgroup.RowCount > 0)
                    {
                        for (int i = 1; i <= Dw_membgroup.RowCount; i++)
                        {
                            membgroup_code = Dw_membgroup.GetItemString(i, "membgroup_code");
                            sql = @"INSERT INTO kpcfdiskmembgroup (coop_id, disk_code, membgroup_code)";
                            sql += " VALUES ('" + state.SsCoopId + "', '" + disk_code + "', '" + membgroup_code + "')";
                            ta.Exe(sql);
                        }
                    }


                    //ลบข้อมูล kpcfdiskmembtype 
                    sql = "delete  from kpcfdiskmembtype where disk_code = '" + disk_code + "'";
                    ta.Exe(sql);

                    //กรองเอาเฉพาะ membtype ที่ เลือกไว้
                    Dw_membtype.SetFilter("operate_flag = 1");
                    Dw_membtype.Filter();
                    if (Dw_membtype.RowCount > 0)
                    {
                        for (int i = 1; i <= Dw_membtype.RowCount; i++)
                        {
                            membtype_code = Dw_membtype.GetItemString(i, "membtype_code");
                            sql = @"INSERT INTO kpcfdiskmembtype (coop_id, disk_code, membtype_code)";
                            sql += " VALUES ('" + state.SsCoopId + "', '" + disk_code + "', '" + membtype_code + "')";
                            ta.Exe(sql);
                        }
                    }
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                    JspostNewClear();
                }
                else
                {
                    DwUtil.UpdateDataWindow(Dw_disktype, pbl, "kpcfdisk");
                    // LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกประเภทแผ่น Disk ที่ต้องการทำรายการ");
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            Dw_disktype.SaveDataCache();
            Dw_membgroup.SaveDataCache();
            Dw_membtype.SaveDataCache();

            this.DisConnectSQLCA();
        }

        //================================
        private void JspostNewClear()
        {
            Dw_disktype.Reset();
            DwUtil.RetrieveDataWindow(Dw_disktype, pbl, null, state.SsCoopId);

            Dw_membgroup.Reset();
            Dw_membtype.Reset();
            lbl_diskcode.Text = "";
            CheckGroupAll.Checked = false;
            CheckTypeAll.Checked = false;
        }

        private void JspostInit()
        {
            CheckGroupAll.Checked = false;
            CheckTypeAll.Checked = false;

            Dw_membgroup.Reset();
            DwUtil.RetrieveDataWindow(Dw_membgroup, pbl, null, state.SsCoopId);

            Dw_membtype.Reset();
            DwUtil.RetrieveDataWindow(Dw_membtype, pbl, null, state.SsCoopId);

            string membgroup_code = "";
            string membtype_code = "";
            try
            {                
                int li_row = 0;
                // Set เลือกก Row
                int row_click = int.Parse(Hdrow.Value);
                Dw_disktype.SelectRow(0, false);
                Dw_disktype.SelectRow(row_click, true);
                Dw_disktype.SetRow(row_click);
                string disk_code = Dw_disktype.GetItemString(row_click, "disk_code");
                string disk_desc = Dw_disktype.GetItemString(row_click, "disk_desc");
                lbl_diskcode.Text = disk_code;
                lbl_diskdesc.Text = disk_desc;

                //ดึงข้อมูล diskmembgroup ตาม
                String sqlmembgroup = @"select membgroup_code from kpcfdiskmembgroup where disk_code = '" + disk_code + "' and coop_id = '" + state.SsCoopId + "' order by disk_code,membgroup_code";
                Sdt dtgroup = WebUtil.QuerySdt(sqlmembgroup);
                //กรณีมีแถวข้อมูล
                if (dtgroup.Rows.Count > 0)
                {
                    int rowall = dtgroup.Rows.Count;
                    for (int i = 0; i < rowall; i++)
                    {
                        membgroup_code = Convert.ToString(dtgroup.Rows[i]["membgroup_code"]);
                        li_row = Dw_membgroup.FindRow("membgroup_code = '" + membgroup_code + "'", 0, Dw_membgroup.RowCount);
                        //set operate flag ให้ Row ที่มีค่า
                        Dw_membgroup.SetItemDecimal(li_row, "operate_flag", 1);
                    }
                }
                else
                {
                    sqlca.Rollback();
                }

                //ดึงข้อมูล diskmembtype ตาม diskcode
                String sqlmembtype = @"select membtype_code from kpcfdiskmembtype where disk_code = '" + disk_code + "' and coop_id = '" + state.SsCoopId + "' order by disk_code,membtype_code";
                Sdt dtmembtype = WebUtil.QuerySdt(sqlmembtype);
                //กรณีมีแถวข้อมูล
                if (dtmembtype.Rows.Count > 0)
                {
                    int rowall = dtmembtype.Rows.Count;
                    for (int j = 0; j < rowall; j++)
                    {
                        membtype_code = Convert.ToString(dtmembtype.Rows[j]["membtype_code"]);
                        li_row = Dw_membtype.FindRow("membtype_code = '" + membtype_code + "'", 0, Dw_membtype.RowCount);
                        //set operate flag ให้ Row ที่มีค่า
                        Dw_membtype.SetItemDecimal(li_row, "operate_flag", 1);
                    }
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message + membgroup_code); }
        }
    }
}