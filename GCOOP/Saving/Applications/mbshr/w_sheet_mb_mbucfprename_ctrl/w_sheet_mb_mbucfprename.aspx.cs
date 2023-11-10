using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.w_sheet_mb_mbucfprename_ctrl
{
    public partial class w_sheet_mb_mbucfprename : PageWebSheet,WebSheet
    {
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack) {
                dsMain.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow) {
                dsMain.InsertLastRow();
            }
            else if (eventArg == PostDeleteRow)
            {
                int rowDel = dsMain.GetRowFocus();
                dsMain.DeleteRow(rowDel);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed = new ExecuteDataSource(this);
                DataTable delRow = dsMain.Deleted;
                for (int i = 0; i < delRow.Rows.Count; i++)
                {
                    string sqlDel = "delete from mbucfprename where prename_code={0}";
                    sqlDel = WebUtil.SQLFormat(sqlDel, delRow.Rows[i]["prename_code"].ToString());
                    exed.SQL.Add(sqlDel);
                }
                exed.AddRepeater(dsMain);

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                int result = exed.Execute(); 
            }
            catch (Exception ex) {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}