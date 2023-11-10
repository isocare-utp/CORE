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
using DataLibrary; // เพิ่มเติม
using System.Data.OracleClient; // เพิ่มเติม
using Sybase.DataWindow; // เพิ่มเติม
using System.Globalization; // เพิ่มเติม


namespace Saving.Applications.account
{
    public partial class w_acc_ucf_acctype : PageWebSheet, WebSheet
    {
        protected String postInsertRow;
        protected String postDeleteRow;


        private void JspostInsertRow() 
        {
            Dw_acc_start_acctype.InsertRow(0);
        }

        private void JspostDeleteRow() 
        {
            Int16 RowDetail = Convert.ToInt16(Hd_row.Value);
            Dw_acc_start_acctype.DeleteRow(RowDetail);
            Dw_acc_start_acctype.UpdateData();
            LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_acc_start_acctype.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_acc_start_acctype.Retrieve();
            }
            else 
            {
                Dw_acc_start_acctype.RestoreContext();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInsertRow") {
                JspostInsertRow();
            }
            else if (eventArg == "postDeleteRow") {
                JspostDeleteRow();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                Dw_acc_start_acctype.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex) 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion
    }
}
