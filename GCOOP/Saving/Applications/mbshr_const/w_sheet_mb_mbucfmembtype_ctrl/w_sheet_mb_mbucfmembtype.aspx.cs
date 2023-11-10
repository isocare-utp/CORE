using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembtype_ctrl
{
    public partial class w_sheet_mb_mbucfmembtype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }

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
                dsList.FindTextBox(ls_rowcount, "membtype_code").Focus();
            }
            else if (eventArg == PostDeleteRow)
            {
                int rowDel = dsList.GetRowFocus();
                dsList.DeleteRow(rowDel);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed = new ExecuteDataSource(this);

                exed.AddRepeater(dsList);
                int result = exed.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                dsList.ResetRow();
                dsList.retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}