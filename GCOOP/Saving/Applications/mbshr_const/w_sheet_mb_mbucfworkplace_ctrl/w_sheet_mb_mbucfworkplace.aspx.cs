using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfworkplace_ctrl
{
    public partial class w_sheet_mb_mbucfworkplace : PageWebSheet, WebSheet
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
                //dsList.InsertLastRow();

                //int row = dsList.RowCount;
                //dsList.DATA[row - 1].COOP_ID = state.SsCoopId;
                dsList.InsertLastRow();
                dsList.DATA[dsList.RowCount - 1].COOP_ID = state.SsCoopControl;
                dsList.FindTextBox(dsList.RowCount - 1, "DEPARTMENT_CODE").Focus();

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
                //for (int i = 0; i <= dsList.RowCount; i++)
                //{
                //    string ls_appltype_code = dsList.DATA[i].APPLTYPE_CODE;
                //    if (ls_appltype_code == "")
                //    {
                //        dsList.DeleteRow(i);
                //    }

                //}
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