using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.divavg.ws_divsrv_constant_ucfdfmethpay_ctrl
{
    public partial class ws_divsrv_constant_ucfdfmethpay : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        public void InitJsPostBack()
        {
            dsList.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

                dsList.RetrieveList();
                dsList.DdMethpaytype();
                dsList.DdBank();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                dsList.InsertLastRow();
                dsList.DATA[dsList.RowCount - 1].COOP_ID = state.SsCoopId;
                dsList.FindTextBox(dsList.RowCount - 1, "start_value").Focus();
                dsList.DdMethpaytype();
                dsList.DdBank();
            }
            else if (eventArg == PostDeleteRow)
            {
                int ls_getrow = dsList.GetRowFocus();
                dsList.DeleteRow(ls_getrow);
                dsList.DdMethpaytype();
                dsList.DdBank();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);

                exed1.AddRepeater(dsList);
                exed1.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsList.ResetRow();
                dsList.RetrieveList();
                dsList.DdMethpaytype();
                dsList.DdBank();

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