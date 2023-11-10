using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfdepartment_ctrl
{
    public partial class w_sheet_mb_mbucfdepartment : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostInsertRow { get; set; } //สำสั่ง JS postback 

        [JsPostBack]
        public string PostDeleteRow { get; set; } //สำสั่ง JS postback 



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
                int ls_rowcount = dsList.RowCount - 1;
                dsList.DATA[ls_rowcount].COOP_ID = state.SsCoopControl;
                dsList.FindTextBox(ls_rowcount, "DEPARTMENT_CODE").Focus();


            }

            else if (eventArg == PostDeleteRow)
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