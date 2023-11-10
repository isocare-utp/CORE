using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.deposit_const.w_sheet_dp_const_booktyp_ctrl
{
    public partial class w_sheet_dp_const_booktyp : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String Postdel { get; set; }

        [JsPostBack]
        public String PostInsertRow { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == Postdel)
            {
                int row = dsMain.GetRowFocus();
                dsMain.DeleteRow(row);
            }
            else if (eventArg == PostInsertRow)
            {
                //dsMain.InsertLastRow();
                //int lastrow = dsMain.RowCount;
                //dsMain.DATA[lastrow - 1].COOP_ID = state.SsCoopId;
                dsMain.InsertAtRow(0);
                dsMain.DATA[0].COOP_ID = state.SsCoopId;
            }
        }

        public void SaveWebSheet()
        {
            try {
                ExecuteDataSource ex = new ExecuteDataSource(this);
                ex.AddRepeater(dsMain);
                int i = ex.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch(Exception ex) {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}