using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_sl_lnucfprobitemtype_ctrl
{
    public partial class w_sheet_sl_lnucfprobitemtype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostProbitemtype { get; set; }
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        
        public void InitJsPostBack()
        {
            dsList.InitDsList(this);          
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack) {
                dsList.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                dsList.InsertAtRow(0);
                //dsList.SetItem(0, dsList.DATA.COOP_IDColumn, state.SsCoopControl);
                dsList.SetItem(0, dsList.DATA.PROBITEMTYPE_CODEColumn, "");
            }
            else if (eventArg == PostDelRow)
            {
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