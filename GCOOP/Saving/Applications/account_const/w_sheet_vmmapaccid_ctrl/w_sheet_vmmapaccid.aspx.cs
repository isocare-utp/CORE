using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.account_const.w_sheet_vmmapaccid_ctrl
{
    public partial class w_sheet_vmmapaccid : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDel { get; set; }

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
            if (eventArg == PostInsertRow)
            {
                //dsMain.InsertLastRow();
                //int r = dsMain.RowCount;
                //dsMain.DATA[r - 1].COOP_ID = state.SsCoopId;
                dsMain.InsertAtRow(0);
                dsMain.DATA[0].COOP_ID = state.SsCoopControl;
                dsMain.DATA[0].AREA_CODE ="00";
                    
            }
            else if (eventArg == PostDel)
            {
                int r = dsMain.GetRowFocus();
                dsMain.DeleteRow(r);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource ex = new ExecuteDataSource(this);
                ex.AddRepeater(dsMain);
                int i = ex.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}