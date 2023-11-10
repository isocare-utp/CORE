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
using Sybase.DataWindow;  //เพิ่มเข้ามา
using System.Data.OracleClient;  //เพิ่มเข้ามา


namespace Saving.Applications.account
{
    public partial class w_acc_postto_pay : PageWebSheet, WebSheet 
    {
        public String postInsertRow;
        public String postDeleteRow;
        public String postChange;

        private void JspostDeleteRow()
        {
            Int16 RowData = Convert.ToInt16(HdRow.Value);
            Dw_data.DeleteRow(RowData);
        }

        private void JspostInsertRow()
        {
            Dw_data.InsertRow(0);
        }



        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postChange = WebUtil.JsPostBack(this, "postChange");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_data.SetTransaction(sqlca);
            if (!IsPostBack) {
         
            } else {
                Dw_data.RestoreContext();
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
            else if (eventArg == "postChange")
            { 
            //ให้เปลี่ยนค่า 
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion
    }
}
