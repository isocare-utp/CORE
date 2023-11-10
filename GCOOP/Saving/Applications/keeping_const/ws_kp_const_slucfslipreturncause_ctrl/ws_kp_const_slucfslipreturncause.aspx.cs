using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.keeping_const.ws_kp_const_slucfslipreturncause_ctrl
{
    public partial class ws_kp_const_slucfslipreturncause : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDel { get; set; }

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
                int li_row = dsList.RowCount - 1;
                dsList.DATA[li_row].COOP_ID = state.SsCoopControl;
                dsList.FindTextBox(li_row, "slipretcause_code").Focus();
            }
            else if (eventArg == PostDel)
            {
                int r = dsList.GetRowFocus();
                dsList.DeleteRow(r);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource ex = new ExecuteDataSource(this);
                ex.AddRepeater(dsList);
                ex.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}