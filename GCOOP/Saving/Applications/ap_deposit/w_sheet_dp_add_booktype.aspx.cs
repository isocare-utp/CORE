using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_add_booktype : PageWebSheet,WebSheet
    {
        string pbl = "dp_ucfdptype.pbl";
        protected String jsPostInsertRow;
        protected String jsPostDelRow;
        protected String jsPostEstSideChange;
    
        public void InitJsPostBack()
        {
            jsPostInsertRow = WebUtil.JsPostBack(this, "jsPostInsertRow");
            jsPostDelRow = WebUtil.JsPostBack(this, "jsPostDelRow");
            jsPostEstSideChange = WebUtil.JsPostBack(this,"jsPostEstSideChange");
                       
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwUtil.RetrieveDataWindow(DwMain, pbl, null);
               
            }
            else
            {
                this.RestoreContextDw(DwMain);
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostInsertRow":
                    InsertRow();
                    break;
                case "jsPostDelRow":
                    DelRow();
                    break;
                case "jsPostEstSideChange":
                    EstSideChange();
                    break;
                
            }
        }

        public void SaveWebSheet()
        {
              for (int i = 1; i <= DwMain.RowCount; i++)
                {
                    int[] row = { i };
                    try
                    {
                        DwUtil.InsertDataWindow(DwMain, pbl, "DPUCFBOOKCONST", row);
                    }
                    catch
                    {
                        DwUtil.UpdateDataWindow(DwMain, pbl, "DPUCFBOOKCONST", row);
                    }

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
           
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #region Function
        private void InsertRow()
        {
            int row = DwMain.InsertRow(0);
            DwMain.SetItemString(row, "coop_id", state.SsCoopId);

        }

        private void DelRow()
        {
            int row = Convert.ToInt16(Hdrow.Value);
            try
            {
                string book_type = DwMain.GetItemString(row, "book_type");
                string book_grp = DwMain.GetItemString(row, "book_grp");
                string book_desc = DwMain.GetItemString(row, "book_desc");
                string sqldelete = "delete DPUCFBOOKCONST where book_type='" + book_type + "' and book_grp='" + book_grp + "' and book_desc='" + book_desc + "'";
                try
                {
                    Sdt delete = WebUtil.QuerySdt(sqldelete);
                    DwMain.DeleteRow(row);
                    DwUtil.UpdateDataWindow(DwMain, pbl, "DPUCFBOOKCONST");
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบแถวสำเร็จ");
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ลบแถวไม่สำเร็จ");
                }
            }
            catch
            {
                DwMain.DeleteRow(row);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบแถวสำเร็จ");
            }
        }

        private void EstSideChange()
        {
            string book_type = DwMain.GetItemString(1, "book_type");
            DwUtil.RetrieveDataWindow(DwMain, pbl, null, book_type);
        }
        #endregion
    }
}