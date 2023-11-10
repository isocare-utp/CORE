using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using System.IO;
using DataLibrary;
using System.Globalization;
using System.Text;
namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_kp_export_agent_group : PageWebDialog, WebDialog
    {
        protected String exportTxt;

        public void InitJsPostBack()
        {
            exportTxt = WebUtil.JsPostBack(this, "exportTxt");  
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                dw_main.Reset();
                String sqlSelect = "";

                sqlSelect = @"SELECT * FROM KPAGENTGROUP ORDER BY  AGENTGROUP_CODE ";
                try
                {
                    DwUtil.ImportData(sqlSelect, dw_main, null);
                }
                catch { 
                    dw_main.InsertRow(0);
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลในฐานข้อมูล");
                }
               
                //DataTable dt = WebUtil.Query(sqlSelect);
                //HdRowCount.Value = dt.Rows.Count.ToString();
            }
            else
            {
                this.RestoreContextDw(dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "exportTxt") {
                ExportFile();
            }
        }

        public void WebDialogLoadEnd()
        {
            dw_main.SaveDataCache();
        }

        public void ExportFile()
        {
            String filename = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            filename += "_agent_group";
            String file_tmp = dw_main.Describe("DataWindow.Data");
            if (file_tmp.Length < 0)
            {
                //dw_exp.SetItemString(1, "line_text", "Empty Data");
            }
            Response.Clear();
            Response.ContentType = "application/text";
            //Response.ContentEncoding = Encoding.Unicode;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".txt");
            Response.Write(file_tmp);
            Response.End();
            
        }
     
    }
}
