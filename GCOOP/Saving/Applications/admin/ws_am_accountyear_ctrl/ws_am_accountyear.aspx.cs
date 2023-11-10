using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 

namespace Saving.Applications.admin.ws_am_accountyear_ctrl
{
    public partial class ws_am_accountyear : PageWebSheet, WebSheet
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
                dsList.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                int year; 
                int r = dsList.RowCount - 1;
                dsList.InsertLastRow();
                //dsList.InsertAtRow(0);
                year = Convert.ToInt16( dsList.DATA[r].ACCOUNT_YEAR)+1;
                dsList.SetItem(r+1, dsList.DATA.COOP_IDColumn, state.SsCoopControl);
                dsList.SetItem(r+1, dsList.DATA.ACCOUNT_YEARColumn, year);
                
            }
            else if (eventArg == PostDelRow)
            {
                int row = dsList.GetRowFocus();
                dsList.DeleteRow(row);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
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