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
using DataLibrary; //เพิ่มเติม
using System.Data.OracleClient; //เพิ่มเติม
using System.Globalization; //เพิ่มเติม
using Sybase.DataWindow; //เพิ่มเติม
namespace Saving.Applications.account //เพิ่มเติม
{
    public partial class w_acc_ucf_typeserise : PageWebSheet , WebSheet 
    {

        public String postInsertRow;
        public String postDeleteRow;
        int InsertRow = 0; //จำนวนแถวที่เพิ่ม
        int DataRow = 0; //จำนวนข้อมูลที่มีอยู่เดิม

        private void JspostInsertRow()
        {
            Dw_acc_typeserise.InsertRow(0);
        }

        private void JspostDeleteRow()
        {
            Int16 RowDetail = Convert.ToInt16(Hdrow.Value);
            try
            {
                string sqldelete = @"DELETE FROM acc_typedelseries WHERE type_series = '" + Dw_acc_typeserise.GetItemString(RowDetail, "type_series") + "'";
                Sdt delete = WebUtil.QuerySdt(sqldelete);
                Dw_acc_typeserise.DeleteRow(RowDetail);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch
            {
                try
                {
                    Dw_acc_typeserise.GetItemString(RowDetail, "type_series");
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
                }
                catch
                {
                    Dw_acc_typeserise.DeleteRow(RowDetail);
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
            Dw_acc_typeserise.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                Dw_acc_typeserise.Retrieve();
            }
            else 
            {
                Dw_acc_typeserise.RestoreContext();
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
            bool flag = true;
            string erroe_code = "";
            string type_series = "";
            string type_desc = "";
            InsertRow = Dw_acc_typeserise.RowCount;
            string sqlcount = @"SELECT * FROM acc_typedelseries";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            DataRow = count.GetRowCount();
            try
            {
                for (int j = 1; j <= DataRow; j++)
                {
                    type_series = Dw_acc_typeserise.GetItemString(j, "type_series");
                    type_desc = Dw_acc_typeserise.GetItemString(j, "type_desc");
                    string sqlupdate = @"UPDATE acc_typedelseries SET type_desc = '" + type_desc + "' WHERE type_series = '" + type_series + "'";
                    Sdt update = WebUtil.QuerySdt(sqlupdate);
                }

                for (int i = DataRow + 1; i <= InsertRow; i++)
                {
                    try
                    {
                        type_series = Dw_acc_typeserise.GetItemString(i, "type_series");
                        if (type_series.Length < 2)
                        {
                            type_series = "00" + type_series;
                        }
                        else if (type_series.Length < 3)
                        {
                            type_series = "0" + type_series;
                        }
                        Dw_acc_typeserise.SetItemString(i, "type_series", type_series);

                        type_desc = Dw_acc_typeserise.GetItemString(i, "type_desc");
                        string sqlinsert = @"INSERT INTO acc_typedelseries VALUES('" + state.SsCoopId + "','" + type_series + "','" + type_desc + "')";
                        Sdt insert = WebUtil.QuerySdt(sqlinsert);
                    }
                    catch
                    {
                        if (!flag)
                        {
                            erroe_code += ", ";
                        }
                        erroe_code += type_series;
                        flag = false;
                    }
                }
                if (flag)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                }
                else
                {
                    Dw_acc_typeserise.Reset();
                    Dw_acc_typeserise.Retrieve();
                    LtServerMessage.Text = WebUtil.ErrorMessage("รหัสประเภทสินทรัพย์ " + erroe_code + " มีอยู่แล้ว");
                }
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
