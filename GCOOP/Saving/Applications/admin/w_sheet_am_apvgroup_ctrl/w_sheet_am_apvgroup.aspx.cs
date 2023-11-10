using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_am_apvgroup_ctrl
{
    public partial class w_sheet_am_apvgroup : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostApvDd { get; set; }
        [JsPostBack]
        public string PostDelete { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                dsMain.InsertLastRow();
                dsMain.RetrieveDD();
            }
            else if (eventArg == PostApvDd)
            {

            }
            else if (eventArg == PostDelete)
            {
                int row = dsMain.GetRowFocus();
                dsMain.DeleteRow(row);

            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                DataTable delRow = dsMain.Deleted;
                for (int i = 0; i < delRow.Rows.Count; i++)
                {
                    string sqlDelete1 = "delete from amsecapvgroup where APVLEVEL_ID={0} and GROUP_APV={1} ";
                    sqlDelete1 = WebUtil.SQLFormat(sqlDelete1, dsMain.DATA[i].APVLEVEL_ID, dsMain.DATA[i].GROUP_APV);
                    exed1.SQL.Add(sqlDelete1);
                }
                exed1.AddRepeater(dsMain);
                int ii = exed1.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ" + ii);
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