using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfgainconcern_ctrl
{
    public partial class w_sheet_mb_mbucfgainconcern : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public string PostInsertRow { get; set; } //ไว้สำหลับเติมแถวเข้าไป

        [JsPostBack]
        public string PostdeleteRow { get; set; } //ไว้สำหลับลบแถวออก



        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.retrieve();
            }


        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                dsList.InsertLastRow();
                int ls_rowcount = dsList.RowCount - 1;
                dsList.FindTextBox(ls_rowcount, "CONCERN_CODE").Focus();


            }

            else if (eventArg == PostdeleteRow)
            {
                int Rowdelete = dsList.GetRowFocus();
                dsList.DeleteRow(Rowdelete);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed = new ExecuteDataSource(this);

                exed.AddRepeater(dsList);

                int result = exed.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ"); //หากบันทึกให้ขึ้น บันทึกแล้ว
                dsList.ResetRow();
                dsList.retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); // กรณีไม่สำเร็จ
            }

        }


        public void WebSheetLoadEnd()
        {
        }
    }
}