using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.mbshr.dlg.w_dlg_mbshr_getmembno_ctrl
{
    public partial class w_dlg_mbshr_getmembno : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostSave { get; set; }
        [JsPostBack]
        public string PostSearch { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebDialogLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSave)
            {
                try
                {
                    ExecuteDataSource exed1 = new ExecuteDataSource(this);
                    decimal last_documentno = dsMain.DATA[0].LAST_DOCUMENTNO;
                    decimal last_documentno_cono = dsMain.DATA[0].last_documentno_cono;
                    string sql_update = "update cmdocumentcontrol set last_documentno='" + last_documentno + "' where document_code='MBMEMBERNO'";
                    exed1.SQL.Add(sql_update);
                    string sql_update2 = "update cmdocumentcontrol set last_documentno='" + last_documentno_cono + "' where document_code='MBMEMBERCONO'";
                    exed1.SQL.Add(sql_update2);
                    exed1.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขทะเบียนสำเร็จ");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("แก้ไขทะเบียนไม่สำเร็จ");

                }

            }
            else if (eventArg == PostSearch)
            {
               
                string sql_select = @"select last_documentno from cmdocumentcontrol where document_code='MBMEMBERNO'";
                sql_select = WebUtil.SQLFormat(sql_select);
                Sdt dt1 = WebUtil.QuerySdt(sql_select);
                if (dt1.Next())
                {
                    dsMain.DATA[0].LAST_DOCUMENTNO = dt1.GetDecimal("last_documentno");
                    dsMain.DATA[0].last_documentno2 = dt1.GetDecimal("last_documentno");
                }
                string sql_select2 = @"select last_documentno from cmdocumentcontrol where document_code='MBMEMBERCONO'";
                sql_select2 = WebUtil.SQLFormat(sql_select2);
                Sdt dt2 = WebUtil.QuerySdt(sql_select2);
                if (dt2.Next())
                {
                    dsMain.DATA[0].last_documentno_cono = dt2.GetDecimal("last_documentno");
                    dsMain.DATA[0].last_documentno_cono2 = dt2.GetDecimal("last_documentno");
                }

            }
        }

        public void WebDialogLoadEnd()
        {
            try
            {
                
                string sql_select = @"select last_documentno from cmdocumentcontrol where document_code='MBMEMBERNO'";
                sql_select = WebUtil.SQLFormat(sql_select);
                Sdt dt1 = WebUtil.QuerySdt(sql_select);
                if (dt1.Next())
                {
                    dsMain.DATA[0].LAST_DOCUMENTNO = dt1.GetDecimal("last_documentno");
                    dsMain.DATA[0].last_documentno2 = dt1.GetDecimal("last_documentno");
                }
                string sql_select2 = @"select last_documentno from cmdocumentcontrol where document_code='MBMEMBERCONO'";
                sql_select2 = WebUtil.SQLFormat(sql_select2);
                Sdt dt2 = WebUtil.QuerySdt(sql_select2);
                if (dt2.Next())
                {
                    dsMain.DATA[0].last_documentno_cono = dt2.GetDecimal("last_documentno");
                    dsMain.DATA[0].last_documentno_cono2 = dt2.GetDecimal("last_documentno");
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }
    }
}