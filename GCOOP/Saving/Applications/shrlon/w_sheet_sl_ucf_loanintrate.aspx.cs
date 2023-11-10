using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_ucf_loanintrate : PageWebSheet, WebSheet
    {
        protected String postInsertRow;
        protected String postDeleteRow;
        int InsertRow = 0; //จำนวนแถวที่เพิ่ม
        int DataRow = 0; //จำนวนข้อมูลที่มีอยู่เดิม

        #region WebSheet Members
        public void InitJsPostBack()
        {
            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                DwMain.Retrieve();
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInsertRow")
            {
                DwMain.InsertRow(0);
                DwMain.ScrollLastPage();
            }
            else if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
        }

        public void SaveWebSheet()
        {
            SaveInfo();
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }
        #endregion

        private void JspostDeleteRow()
        {
            Int16 RowDetail = Convert.ToInt16(Hd_row.Value);
            try
            {
                string sqldelete = @"DELETE FROM lccfloanintrate WHERE loanintrate_code = '" + DwMain.GetItemString(RowDetail, "loanintrate_code") + "'";
                Sdt delete = WebUtil.QuerySdt(sqldelete);
                DwMain.DeleteRow(RowDetail);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
            }
            catch
            {
                try
                {
                    DwMain.GetItemString(RowDetail, "loanintrate_code");
                    LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเร็จ");
                }
                catch
                {
                    DwMain.DeleteRow(RowDetail);
                }
            }
        }

        private void SaveInfo()
        {
            bool flag = true;
            string erroe_code = "";
            string loanintrate_code = "";
            string loanintrate_desc = "";
            InsertRow = DwMain.RowCount;
            string sqlcount = @"SELECT * FROM lccfloanintrate";
            Sdt count = WebUtil.QuerySdt(sqlcount);
            DataRow = count.GetRowCount();
            try
            {
                for (int j = 1; j <= DataRow; j++)
                {
                    loanintrate_code = DwMain.GetItemString(j, "loanintrate_code");
                    loanintrate_desc = DwMain.GetItemString(j, "loanintrate_desc");
                    string sqlupdate = @"UPDATE lccfloanintrate SET loanintrate_desc = '" + loanintrate_desc + "' WHERE loanintrate_code = '" + loanintrate_code + "'";
                    Sdt update = WebUtil.QuerySdt(sqlupdate);
                }

                for (int i = DataRow + 1; i <= InsertRow; i++)
                {
                    try
                    {
                        loanintrate_code = DwMain.GetItemString(i, "loanintrate_code");

                        if (loanintrate_code.Length == 1)
                        {
                            loanintrate_code = "0000" + loanintrate_code;
                        }
                        else if (loanintrate_code.Length == 2)
                        {
                            loanintrate_code = "000" + loanintrate_code;
                        }
                        else if (loanintrate_code.Length == 3)
                        {
                            loanintrate_code = "00" + loanintrate_code;
                        }
                        else if (loanintrate_code.Length == 4)
                        {
                            loanintrate_code = "0" + loanintrate_code;
                        }
                        DwMain.SetItemSqlString(i, "loanintrate_code", loanintrate_code);

                        loanintrate_desc = DwMain.GetItemString(i, "loanintrate_desc");
                        string sqlinsert = @"INSERT INTO lccfloanintrate VALUES('001001','" + loanintrate_code + "','" + loanintrate_desc + "')";
                        Sdt insert = WebUtil.QuerySdt(sqlinsert);
                    }
                    catch
                    {
                        if (!flag)
                        {
                            erroe_code += ", ";
                        }
                        erroe_code += loanintrate_code;
                        flag = false;
                    }
                }
                if (flag)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                }
                else
                {
                    DwMain.Reset();
                    DwMain.Retrieve();
                    LtServerMessage.Text = WebUtil.ErrorMessage("รหัสอัตราดอกเบี้ย " + erroe_code + " มีอยู่แล้ว");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
        }
    }
}