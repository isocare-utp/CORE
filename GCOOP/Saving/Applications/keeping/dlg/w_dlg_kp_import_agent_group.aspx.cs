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

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_kp_import_agent_group : PageWebDialog, WebDialog
    {
        protected String importTxt;
        protected String showTxt;

        public void InitJsPostBack()
        {
            importTxt = WebUtil.JsPostBack(this, "importTxt");
            showTxt = WebUtil.JsPostBack(this, "showTxt");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                dw_main.Reset();
                dw_main.InsertRow(0);

                dw_data.Reset();
                String sqlSelect = "";

                sqlSelect = @"SELECT * FROM KPAGENTGROUP ";
                try
                {
                    DwUtil.ImportData(sqlSelect, dw_data , null);
                }
                catch
                {
                    dw_data.InsertRow(0);
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลในฐานข้อมูล");
                }
            }
            else
            {
                this.RestoreContextDw(dw_main);
                this. RestoreContextDw (dw_data );
            }
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "importTxt") {
                ImportTxt();
            }
            else if (eventArg == "showTxt") {
                ShowDetail();
            }
        }

        public void WebDialogLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_data.SaveDataCache();
        }
        public void DeleteAll() 
        {
            string sqlDelete = @"  DELETE FROM KPAGENTGROUP";
            Sta staDelete = new Sta(sqlca.ConnectionString);
            try
            {
                staDelete.Exe(sqlDelete);
                staDelete.Close();
            }
            catch { LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถลบข้อมูลได้"); }
        }
        public void ImportTxt() 
        {
            //DeleteAll();
            String agentGroupCode = "";
            String agentGroupDesc = "";
            String address = "";
            String agentName = "";
            String sqlInsert = "";
            String flag = "";

            Sta staInsert = new Sta(sqlca.ConnectionString);
            Sta staUpdate = new Sta(sqlca.ConnectionString);
            //int dwDataRowCount = dw_data.RowCount();

            for (int r = 1; r <= dw_main.RowCount; r++)
            {
                String sqlUpdate = "";
                //String sqlInsert = "";
                try { agentGroupCode = dw_main.GetItemString(r, "agentgroup_code").Trim(); }
                catch { agentGroupCode = ""; }
                try { agentGroupDesc = dw_main.GetItemString(r, "agentgroup_desc").Trim(); }
                catch { agentGroupDesc = ""; }
                try { address = dw_main.GetItemString(r, "address_desc").Trim(); }
                catch { address = ""; }
                try { agentName = dw_main.GetItemString(r, "agent_name").Trim(); }
                catch { agentName = ""; }
                int foundFlag = dw_data.FindRow("agentgroup_code = '" + agentGroupCode + "'", 1, dw_data.RowCount);
                if (foundFlag < 1)
                {
                    //กรณีไม่มีข้อมูลอยู่ในฐานข้อมูล
                    //dwDataRowCount += 1;
                    sqlInsert = @"INSERT INTO KPAGENTGROUP
                                        ( AGENTGROUP_CODE, AGENTGROUP_DESC,  ADDRESS_DESC, AGENT_NAME) 
                                        VALUES ('" + agentGroupCode + "', '" + agentGroupDesc + "', '" + address + "', '" + agentName + "' ) "
                    ;
                    try
                    {
                        staInsert.Exe(sqlInsert);
                        staInsert.Close();
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        flag = "0";
                    }
                }
                else {
                    //กรณีมีข้อมูลอยู่ในฐานข้อมูลแล้ว  ต้อง Update
                    sqlUpdate = @"UPDATE KPAGENTGROUP
                        SET AGENTGROUP_CODE = '" + agentGroupCode +
                             "', AGENTGROUP_DESC = '" + agentGroupDesc +
                             "', ADDRESS_DESC ='" + address +
                             "', AGENT_NAME ='" + agentName +
                             "' WHERE AGENTGROUP_CODE = '" + agentGroupCode + "' "
                    ;
                    try
                    {
                        staUpdate.Exe(sqlUpdate);
                        staUpdate.Close();
                        
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        flag = "0";
                    }
                }
            }
            if (flag != "0") {
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
            }
            HdReturn.Value = "10";
        }
        public void ShowDetail()
        {
            String filename = "";
            String save = "";
            try
            {
                filename = Path.GetFileName(FileUpload.PostedFile.FileName);
            }
            catch { filename = ""; }
            if (filename != "")
            {
                save = "C:\\GCOOP\\Saving\\filecommon\\Temp_AgentGroupIMP.txt";
                FileUpload.PostedFile.SaveAs(save);
                try
                {
                    dw_main.Reset();
                    dw_main.ImportFile(save, FileSaveAsType.Text );
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
        }
        
    }
}
