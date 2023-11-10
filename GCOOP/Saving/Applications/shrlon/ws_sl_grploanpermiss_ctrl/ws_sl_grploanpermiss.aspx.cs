using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_grploanpermiss_ctrl
{
    public partial class ws_sl_grploanpermiss : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDelRow { get; set; }

        public void InitJsPostBack()
        {
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsDetail.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostInsertRow")
            {
                dsDetail.InsertLastRow();
                int r = dsDetail.RowCount - 1;
                dsDetail.DATA[r].COOP_ID = state.SsCoopControl;
            }
            else if (eventArg == "PostDelRow")
            {
                int r = dsDetail.GetRowFocus();
                dsDetail.DeleteRow(r);
            }
        }

        public void SaveWebSheet()
        {
            
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsDetail);
                exe.Execute(); 
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
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