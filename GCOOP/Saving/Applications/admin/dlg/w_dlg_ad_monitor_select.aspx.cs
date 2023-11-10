using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data;

namespace Saving.Applications.admin.dlg
{
    public partial class w_dlg_ad_monitor_select : PageWebDialog, WebDialog
    {
        private String connectionString = "";
        private bool isUsingLog = false;
        protected String postSave;
        protected String postDelete;

        public void InitJsPostBack()
        {
            postSave = WebUtil.JsPostBack(this, "postSave");
            postDelete = WebUtil.JsPostBack(this, "postDelete");
        }

        public void WebDialogLoadBegin()
        {
            isUsingLog = xmlconfig.CentLogUsing;
            if (!isUsingLog)
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ยังไม่ได้เปิดใช้งาน LOG");
                return;
            }
            connectionString = xmlconfig.CentLogConnectionString;
            if (!IsPostBack)
            {
                Sta ta = new Sta(connectionString);
                if (ta.DbType == DataLibrary.DbType.MySQL)
                {
                    ddDbType.SelectedIndex = 0;
                }
                else
                {
                    ddDbType.SelectedIndex = 1;
                }
                try
                {
                    RefreshGridView(ta);
                    //String sql = "select * from selectlog order by modify_time desc";
                    //Sdt dt = ta.Query(sql);
                    //if (dt.Next())
                    //{
                    //    GridView1.DataSource = dt;
                    //    GridView1.DataBind();
                    //}
                }
                catch { }
                ta.Close();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (!isUsingLog)
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ยังไม่ได้เปิดใช้งาน LOG");
                return;
            }
            if (eventArg == "postSave")
            {
                JsPostSave();
            }
            else if (eventArg == "postDelete")
            {
                JsPostDelete();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        private void JsPostSave()
        {
            String selectName = tbSelectName.Text;
            Sta ta = new Sta(connectionString);
            try
            {
                String sql1 = "select * from selectlog where select_name = '" + selectName + "'";
                Sdt dt = ta.Query(sql1);
                String sqlExe = "";

                String seName = "'" + tbSelectName.Text + "'";
                String desc = "'" + tbDescription.Text + "'";
                String saveBy = "'" + tbSaveBy.Text + "'";
                String sqlWhere = "'" + GetWhereFormat() + "'";

                String sqlNow = "";

                if (ddDbType.SelectedItem.Value == "MySQL")
                {
                    sqlNow = "now()";
                }
                else
                {
                    sqlNow = "sysdate";
                }

                if (dt.Next())
                {
                    sqlExe = "update selectlog set description={1}, sql_syntax={2}, modify_time={3}, modify_by={4} where select_name={0}";
                    sqlExe = String.Format(sqlExe, seName, desc, sqlWhere, sqlNow, saveBy);
                }
                else
                {
                    sqlExe = @"
                        insert into selectlog
                        (
                            select_name,    description,    sql_syntax,     create_time,
                            modify_time,    create_by,      modify_by
                        )
                        values
                        (
                            {0},            {1},            {2},            {3},
                            {3},            {4},            {4}
                        )
                    ";
                    sqlExe = String.Format(sqlExe, seName, desc, sqlWhere, sqlNow, saveBy);
                }
                int result = ta.Exe(sqlExe);
                RefreshGridView(ta);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ " + result + " รายการ");
            }
            catch { }
            ta.Close();
        }

        private void JsPostDelete()
        {
            Sta ta = new Sta(connectionString);
            try
            {
                String sql = "delete from selectlog where select_name = '" + HdDeleteName.Value + "'";
                int result = ta.Exe(sql);
                RefreshGridView(ta);
                HdDeleteName.Value = "";
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ " + result + " รายการ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            ta.Close();
        }

        private String GetWhereFormat()
        {
            String syn = tbSqlSyntax.Text.Trim();
            if (!string.IsNullOrEmpty(syn))
            {
                syn = syn.Replace("'", "\\'");
                syn = syn.Replace("\"", "\\'");
            }
            return syn;
        }

        private void RefreshGridView(Sta ta)
        {
            try
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

                String sql = "select * from selectlog order by modify_time desc";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch { }
        }
    }
}