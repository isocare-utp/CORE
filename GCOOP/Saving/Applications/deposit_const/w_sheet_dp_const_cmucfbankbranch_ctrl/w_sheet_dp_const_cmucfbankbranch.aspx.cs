using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.deposit_const.w_sheet_dp_const_cmucfbankbranch_ctrl
{
    public partial class w_sheet_dp_const_cmucfbankbranch : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostBankId { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsDetail.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdApplication();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

            if (eventArg == PostBankId)
            {
                string app = dsMain.DATA[0].BANK_CODE;
                dsDetail.Retrieve(app);
            }
            else if (eventArg == PostDeleteRow)
            {
                int rowDel = dsDetail.GetRowFocus();
                dsDetail.DeleteRow(rowDel);
            }
            else if (eventArg == PostInsertRow)
            {
                //เพิ่มแถว
                dsDetail.InsertAtRow(0);
                dsDetail.DATA[0].BANK_CODE = dsMain.DATA[0].BANK_CODE;

            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                exed1.AddRepeater(dsDetail);
                int ii = exed1.Execute();
                string app = dsMain.DATA[0].BANK_CODE;
                dsDetail.Retrieve(app);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ [" + ii + "]");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

        public void WebSheetLoadEnd()
        {
        }
    }
}