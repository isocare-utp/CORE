using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.ap_deposit.w_sheet_dp_const_cmucfbank_ctrl
{
    /// <summary>
    /// หน้าจอกำหนดรหัสธนาคาร
    /// </summary>
    public partial class w_sheet_dp_const_cmucfbank : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }

        public void InitJsPostBack()
        {
            dsList.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                // ดึงข้อมูลธนาคารที่อยู่แล้วขึ้นมาก่อน
                dsList.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                //เพิ่มแถว
                dsList.InsertAtRow(0);
            }
            else if (eventArg == PostDelRow)
            {
                //ลบแถวที่เลือก
                int row = dsList.GetRowFocus();
                dsList.DeleteRow(row);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsList);
                int result = exe.Execute();
                dsList.Retrieve();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ (" + result + ")");
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