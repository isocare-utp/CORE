using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_am_amsecwinsgroup_ctrl
{
    public partial class w_sheet_am_amsecwinsgroup : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostApplication { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }

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
            if (eventArg == PostApplication)
            {
                string app = dsMain.DATA[0].APPLICATION;
                dsDetail.Retrieve(app);
            }
            else if (eventArg == PostInsertRow)
            {
                string app = dsMain.DATA[0].APPLICATION;
                if (!string.IsNullOrEmpty(app))
                {
                    dsDetail.InsertLastRow();
                    int row = dsDetail.RowCount - 1;
                    dsDetail.SetItem(row, dsDetail.DATA.APPLICATIONColumn, app);
                    dsDetail.SetItem(row, dsDetail.DATA.COOP_CONTROLColumn, state.SsCoopControl);
                }
            }
            else if (eventArg == PostDeleteRow)
            {
                int rowDel = dsDetail.GetRowFocus();
                dsDetail.DeleteRow(rowDel);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                exed1.AddRepeater(dsDetail);
                int ii = exed1.Execute();
                string app = dsMain.DATA[0].APPLICATION;
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