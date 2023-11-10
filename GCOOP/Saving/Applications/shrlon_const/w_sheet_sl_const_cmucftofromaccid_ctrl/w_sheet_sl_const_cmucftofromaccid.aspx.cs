using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.shrlon_const.w_sheet_sl_const_cmucftofromaccid_ctrl
{
    public partial class w_sheet_sl_const_cmucftofromaccid : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string PostSetAccName { get; set; }

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
                dsList.InsertAtRow(0);
                dsList.DATA[0].COOP_ID = state.SsCoopControl;
                dsList.DATA[0].APPLGROUP_CODE = "SLN";
            }
            else if (eventArg == PostDelRow)
            {
                int row = dsList.GetRowFocus();
                dsList.DeleteRow(row);
            }
            else if (eventArg == PostSetAccName)
            {
                int r = dsList.GetRowFocus();
                dsList.DATA[r].account_name = "";

                string sql = @"select account_name 
                from accmaster
                where coop_id = {0}
	                and account_id = {1}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsList.DATA[r].ACCOUNT_ID);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    string account_name = dt.GetString("account_name");
                    dsList.DATA[r].account_name = account_name;
                }
            }
        }

        public void WebSheetLoadEnd()
        {

        }
        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsList);
                exe.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}