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
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataLibrary;
using System.Data.OracleClient;
using Sybase.DataWindow;


namespace Saving.Applications.account
{
    
    public partial class w_acc_ucf_accsection : PageWebSheet, WebSheet 
    {
        protected String postInsertDetail;
        protected String postDeleteDetail;


        private void JspostInsertDetail()
        {
            Dw_ucf_accsection.InsertRow(0);
     
        }

        private void JspostDeleteDetail()
        {
            try 
            {
                Int16 RowDetail = Convert.ToInt16(Hd_row.Value);
                Dw_ucf_accsection.DeleteRow(RowDetail);
                Dw_ucf_accsection.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); 
            }  
        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            postInsertDetail = WebUtil.JsPostBack(this, "postInsertDetail");
            postDeleteDetail = WebUtil.JsPostBack(this, "postDeleteDetail");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_ucf_accsection.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_ucf_accsection.Retrieve();
            }
            else {
                Dw_ucf_accsection.RestoreContext();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInsertDetail") {
                JspostInsertDetail();
            }
            else if (eventArg == "postDeleteDetail") {
                JspostDeleteDetail();
            }
        }

        public void SaveWebSheet()
        {
            try 
            {
                Dw_ucf_accsection.UpdateData();
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
