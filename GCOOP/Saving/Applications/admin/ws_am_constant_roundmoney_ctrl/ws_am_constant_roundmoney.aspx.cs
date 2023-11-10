using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.admin.ws_am_constant_roundmoney_ctrl
{
    public partial class ws_am_constant_roundmoney : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        public void InitJsPostBack()
        {
            dsList.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                String ls_applgrp = "DIV";
                dsList.RetrieveList(ls_applgrp);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                dsList.InsertLastRow();
                dsList.DATA[dsList.RowCount - 1].COOP_ID = state.SsCoopId;
                //dsList.DATA[dsList.RowCount - 1].APPLGROUP_CODE = "DIV";
                dsList.FindTextBox(dsList.RowCount - 1, "function_code").Focus();

            }
            else if (eventArg == PostDeleteRow)
            {
                int ls_getrow = dsList.GetRowFocus();
                dsList.DeleteRow(ls_getrow);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);

                exed1.AddRepeater(dsList);
                exed1.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsList.ResetRow();
                String ls_applgrp = "DIV";
                dsList.RetrieveList(ls_applgrp);

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