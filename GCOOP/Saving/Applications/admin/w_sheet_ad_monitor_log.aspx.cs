using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Text;

namespace Saving.Applications.admin
{
    public partial class w_sheet_ad_monitor_log : PageWebSheet, WebSheet
    {
        protected String ajaxUrl = "";
        protected String monitorUrl = "";
        protected String postSelectName;
        private Sdt dtData = null;

        public void InitJsPostBack()
        {
            postSelectName = WebUtil.JsPostBack(this, "postSelectName");
        }

        public void WebSheetLoadBegin()
        {
            String savUrl = state.SsUrl;
            String url = savUrl + "JsCss/Ajax.js";
            ajaxUrl = "<script src=\"" + url + "\" language=\"javascript\"></script>";
            monitorUrl = savUrl + "CentLog.aspx";
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSelectName")
            {
                JsPostSelectName();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        private void JsPostSelectName()
        {
            int totalRow = 0;

            Sta ta = new Sta(xmlconfig.CentLogConnectionString);
            try
            {
                String sql = "select * from selectlog where select_name = '" + HdSelectName.Value + "'";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    String sql2 = @"
                        SELECT
                            " + GetAlertText() + @" as alert_text,
                            CONCAT(session_id, DATE_FORMAT(hit_date, '%Y%m%d%H%i%s')) as primary_id,
                            hit_date,
                            DATE_FORMAT(hit_date, '%H:%i:%s') as hit_time,
                            server_ip,
                            client_ip,
                            session_id,
                            application,
                            coop_id,
                            username,
                            CASE 
                                WHEN 'th' = 'th' AND current_pagedesc IS NOT NULL AND current_pagedesc <> '' 
                                THEN current_pagedesc
                                ELSE current_page 
                            END AS curr_page,
                            current_pagedesc,
                            url_absolute,
                            jspostback,
                            CONCAT(SUBSTR(methode,1,1), '+', message_type) as methode_type,
                            escape_coding(server_message) as server_message,
                            load_time
                        FROM hitlog 
                        WHERE 
                    ";
                    sql2 += dt.GetString("sql_syntax");
                    Sdt dt2 = ta.Query(sql2);
                    if (dt2.Next())
                    {
                        dtData = dt2;
                        Repeater1.DataSource = dt2;
                        Repeater1.DataBind();
                        totalRow = dt2.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            ta.Close();
            LbLoopTime.Text = "ค้นหาด้วย profile ชื่อ: " + HdSelectName.Value + " :: ได้ " + totalRow + " แถว";
            HdSelectName.Value = "";
        }

        private String GetAlertText()
        {
            String text = @"
                CONCAT(
                    'hit_date = ', DATE_FORMAT(hit_date, '%a %d/%m/%Y %H:%i:%s') , '{0}',
                    'session_id = ', session_id, '{0}',
                    'server_ip = ', server_ip, '{0}',
                    'client_ip = ', client_ip, '{0}',
                    'application = ', application, '{0}',
                    'username = ', username, '{0}',
                    'coop_id/control = ', coop_id , '/', coop_control, '{0}',
                    'current_page = ', current_page, ' ', current_pagedesc, '{0}',
                    'url_absolute = ', url_absolute, '{0}',
                    'methode = ', methode,  '{0}',
                    'jspostback = ', jspostback, '{0}',
                    'message_type = ', message_type, '{0}',
                    'server_message = ', REPLACE(REPLACE(REPLACE(server_message, '{1}', '^'), '\r', ''), '\n', ' '), '{0}',
                    'load_time = ', load_time, '{0}'
                )";
            return String.Format(text, "\\r\\n\\r\\n", "\"");
        }

        protected String GetAlertText(object primary_id)
        {
            try
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (dtData.Rows[i]["primary_id"].ToString() == primary_id.ToString())
                    {
                        byte[] b1 = (byte[])dtData.Rows[i]["alert_text"];

                        return Encoding.UTF8.GetString(b1);
                    }
                }
            }
            catch { }
            return "";
        }
    }
}