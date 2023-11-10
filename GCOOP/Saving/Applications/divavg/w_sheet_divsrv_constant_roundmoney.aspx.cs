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
using CoreSavingLibrary.WcfNDivavg;
using System.Web.Services.Protocols;

namespace Saving.Applications.divavg
{
    public partial class w_sheet_dirsrv_constant_roundmoney : PageWebSheet, WebSheet
    {
        protected String postInsertRow;
        protected String postDeleteRow;
        protected String postNewClear;
        protected String postRefresh;
        //=======================
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.Retrieve(state.SsCoopId,"DIV");
        }

        private void JspostInsertRow()
        {
            Dw_main.InsertRow(0);
         
        }

        private void JspostDeleteRow()
        {
            Int16 RowDetail = Convert.ToInt16(Hd_row.Value);
            Dw_main.DeleteRow(RowDetail);
            Dw_main.UpdateData();
            LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();
            }
            catch { LtServerMessage.Text = WebUtil.CompleteMessage("ติดต่อ Database ไม่ได้"); }

            Dw_main.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInsertRow")
            {
                JspostInsertRow();
            }
            else if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
            else if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postRefresh")
            { 
                // Refresh();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                int li_row = Dw_main.FindRow("isnull( function_code )or function_code = ''", 0, Dw_main.RowCount);
                if (li_row > 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูลให้ครบ");
                }
                else
                {
                    Dw_main.UpdateData();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
        }

        #endregion
    }
}