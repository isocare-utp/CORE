using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin
{
    public partial class w_sheet_am_amsecwins : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostApplication { get; set; }
        [JsPostBack]
        public string PostGroupCode { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }

        public void InitJsPostBack()
        {
            dsGroup.InitDsGroup(this);
            dsWins.InitDsWins(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsGroup.DdApplication();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostApplication)
            {
                string app = dsGroup.DATA[0].APPLICATION;
                dsGroup.SetItem(0, dsGroup.DATA.GROUP_CODEColumn, "");
                dsGroup.DdGroupCode(app);
                dsWins.ResetRow();
            }
            else if (eventArg == PostGroupCode)
            {
                string app = dsGroup.DATA[0].APPLICATION;
                string grp = dsGroup.DATA[0].GROUP_CODE;
                dsWins.ResetRow();
                if (!string.IsNullOrEmpty(app) && !string.IsNullOrEmpty(grp))
                {
                    dsWins.Retrieve(app, grp);
                }
            }
            else if (eventArg == PostInsertRow)
            {
                string app = dsGroup.DATA[0].APPLICATION;
                string grp = dsGroup.DATA[0].GROUP_CODE;
                if (!string.IsNullOrEmpty(app) && !string.IsNullOrEmpty(grp))
                {
                    dsWins.InsertLastRow();
                    dsWins.DdGroupCode(app);
                    int row = dsWins.RowCount - 1;
                    dsWins.SetItem(row, dsWins.DATA.APPLICATIONColumn, app);
                    dsWins.SetItem(row, dsWins.DATA.COOP_CONTROLColumn, state.SsCoopControl);
                    dsWins.SetItem(row, dsWins.DATA.GROUP_CODEColumn, grp);
                }
            }
            else if (eventArg == PostDeleteRow)
            {
                int rowDel = dsWins.GetRowFocus();
                dsWins.DeleteRow(rowDel);
                dsWins.DdGroupCode(dsGroup.DATA[0].APPLICATION);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                DataTable delRow = dsWins.Deleted;
                string app = dsGroup.DATA[0].APPLICATION;
                for (int i = 0; i < delRow.Rows.Count; i++)
                {
                    string sqlDelete1 = "delete from amsecpermiss where coop_id={0} and application={1} and window_id={2} and coop_control={0} and user_name='admin'";
                    sqlDelete1 = WebUtil.SQLFormat(sqlDelete1, state.SsCoopControl, app, delRow.Rows[i]["window_id"].ToString());
                    exed1.SQL.Add(sqlDelete1);
                }
                exed1.AddRepeater(dsWins);
                int ii = exed1.Execute();

                dsWins.Retrieve(app, dsGroup.DATA[0].GROUP_CODE);

                string reAdmin = "";
                if (CbReAdmin.Checked)
                {
                    try
                    {
                        ExecuteDataSource exed2 = new ExecuteDataSource(this);

                        string sqlDelete2 = "delete from amsecpermiss where coop_id={0} and coop_control={0} and user_name='admin'";
                        sqlDelete2 = WebUtil.SQLFormat(sqlDelete2, state.SsCoopControl);
                        exed2.SQL.Add(sqlDelete2);

                        string sqlSelectAmSecWins = "select application, window_id from amsecwins where coop_control={0} and used_flag = 1";
                        sqlSelectAmSecWins = WebUtil.SQLFormat(sqlSelectAmSecWins, state.SsCoopControl);
                        DataTable dt2 = WebUtil.Query(sqlSelectAmSecWins);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            string sqlInsertAmSecWins = @"
                            insert into amsecpermiss(
                                coop_id,        application,    user_name,      window_id,      
                                save_status,    check_flag,     coop_control
                            )values(
                                {0},            {1},            'admin',        {2},      
                                1,              1,              {0}
                            )";
                            object[] argsInsert = new object[3];
                            argsInsert[0] = state.SsCoopControl;
                            argsInsert[1] = dt2.Rows[i]["application"];
                            argsInsert[2] = dt2.Rows[i]["window_id"];
                            sqlInsertAmSecWins = WebUtil.SQLFormat(sqlInsertAmSecWins, argsInsert);
                            exed2.SQL.Add(sqlInsertAmSecWins);
                        }
                        int iii = exed2.Execute();
                        reAdmin = "/" + iii;
                    }
                    catch (Exception ex)
                    {
                        reAdmin = " :: " + ex.Message;
                    }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ [rows: " + ii + reAdmin + "]");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                for (int i = 0; i < dsWins.RowCount; i++)
                {
                    if (dsWins.GetRowStatus(i) == "insert")
                    {
                        TextBox tb = dsWins.FindTextBox(i, dsWins.DATA.WINDOW_IDColumn);
                        tb.ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}