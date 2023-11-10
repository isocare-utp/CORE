using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.admin.w_sheet_kill_table_ctrl
{
    public partial class w_sheet_kill_table : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String Post_AllKill { get; set; }
        [JsPostBack]
        public String JsKillTable { get; set; }
        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "Post_AllKill")
            {
                string sql = @"SELECT 
                                request_session_id
                                FROM SYS.DM_TRAN_LOCKS L
                                JOIN SYS.PARTITIONS P 
                                ON L.RESOURCE_ASSOCIATED_ENTITY_ID = P.HOBT_ID 
                                group by request_session_id
                                order by request_session_id";
                Sdt dt = new Sdt();
                dt = WebUtil.QuerySdt(sql);

                if (dt.Next())
                {
                    string request_session_id = "";
                    decimal kill_id = 0;
                    for (int r = 0; r < dt.GetRowCount(); r++)
                    {

                        request_session_id = dt.Rows[r]["request_session_id"].ToString();
                        kill_id = Convert.ToDecimal(kill_id);
                        KillTable(kill_id);
                    }
                    dsList.retrieve();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                }
            }
            else if (eventArg == "JsKillTable")
            {
                try
                {
                    int ln_row = dsList.GetRowFocus();
                    decimal ld_sessionid = dsList.DATA[ln_row].request_session_id;
                    KillTable(ld_sessionid);
                    dsList.retrieve();
                    LtServerMessage.Text = WebUtil.CompleteMessage("Kill Table สำเร็จ");
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("Kill Table ไม่สำเร็จ");
                }
            }
        }

        private void KillTable(decimal kill_id)
        {
            string sql_kill = @"KILL " + kill_id + "";
            Sdt dt_kill = new Sdt();
            dt_kill = WebUtil.QuerySdt(sql_kill);
        }
        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}