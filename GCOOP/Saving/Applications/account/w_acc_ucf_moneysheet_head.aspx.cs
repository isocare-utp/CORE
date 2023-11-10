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
    public partial class w_acc_ucf_moneysheet_head : PageWebSheet, WebSheet
    {
        protected String postInsertRow;
        protected String postDeleteRow;
        int InsertRow = 0; //จำนวนแถวที่เพิ่ม
        int DataRow = 0; //จำนวนข้อมูลที่มีอยู่เดิม

        private void JspostInsertRow()
        {
            Dw_main.InsertRow(0);
        }

        private void JspostDeleteRow()
        {
            Int16 RowDetail = Convert.ToInt16(Hd_row.Value);
            try
            {
                string sqldelete = @"DELETE FROM accucfacctype WHERE account_type_id = '" + Dw_main.GetItemString(RowDetail, "account_type_id") + "'";
                Sdt delete = WebUtil.QuerySdt(sqldelete);
                Dw_main.DeleteRow(RowDetail);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch
            {
                try
                {
                    Dw_main.GetItemString(RowDetail, "account_type_id");
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
                }
                catch
                {
                    Dw_main.DeleteRow(RowDetail);
                }
            }
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
            Dw_main.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                Dw_main.Retrieve(state.SsCoopId);
            }
            else
            {
                Dw_main.RestoreContext();
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
        }

        public void SaveWebSheet()
        {
            string coop_name = "";
            string test = state.SsCoopName;
            string sql = @"select coop_name FROM cmcoopmaster WHERE coop_id = '" + state.SsCoopId + "'";
            Sdt dt1 = WebUtil.QuerySdt(sql);
            if (dt1.Next())
            {
                coop_name = dt1.Rows[0][0].ToString();
            }
            Dw_main.SetItemString(Dw_main.RowCount, "coop_id", state.SsCoopId);
            Dw_main.SetItemString(Dw_main.RowCount, "report_heading", coop_name);
            //Dw_main.SetItemString(Dw_main.RowCount, "c1_heading", "ปี");
            Dw_main.SetItemString(Dw_main.RowCount, "c2_heading", "ปี");
            Dw_main.SetItemDecimal(Dw_main.RowCount, "percent_status", 0);
            Dw_main.SetItemDecimal(Dw_main.RowCount, "sign_status", 0);
            Dw_main.SetItemDecimal(Dw_main.RowCount, "total_show", 2);
            try
            {
                Dw_main.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                sqlca.Rollback();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion
    }
}
