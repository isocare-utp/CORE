using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfprename_ctrl
{
    public partial class w_sheet_mb_mbucfprename : PageWebSheet, WebSheet
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
                int ls_rowcount = dsMain.RowCount - 1;
                dsMain.FindTextBox(ls_rowcount, "prename_code").Focus();
            }
            else if (eventArg == PostDeleteRow)
            {
                int rowDel = dsMain.GetRowFocus();
                dsMain.DeleteRow(rowDel);

                string ls_prename_code = dsMain.DATA[rowDel].PRENAME_CODE;
                decimal checkdata = OfcheckData(ls_prename_code);

                if (checkdata == 0)
                {
                    ExecuteDataSource exed1 = new ExecuteDataSource(this);
                    string sql = "delete from mbucfprename where coop_id='" + state.SsCoopControl + "' and prename_code='" + ls_prename_code + "'";
                    exed1.SQL.Add(sql);
                    exed1.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
                    dsMain.Retrieve();
                }
                else
                {
                    this.SetOnLoadedScript("alert('คำนำหน้าที่ต้องการลบ ไม่สามารถทำการลบได้ เนื่องจากมีข้อมูลที่เกี่ยวข้องอยู่')");
                    LtServerMessage.Text = WebUtil.ErrorMessage("คำนำหน้า " + ls_prename_code.Trim() + " ที่ต้องการลบไม่สามารถทำการลบได้ เนื่องจากมีข้อมูลที่เกี่ยวข้องอยู่");
                }





                    
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed = new ExecuteDataSource(this);
                exed.AddRepeater(dsMain);

                int result = exed.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                dsMain.ResetRow();
                dsMain.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private decimal OfcheckData(string ls_prename_code)
        {
            decimal check_status = 0;
            string sql = @"select count(prename_code) as c 
                    from mbmembmaster where coop_id= {0} and rtrim(ltrim(prename_code)) = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_prename_code.Trim());
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                check_status = dt.GetDecimal("c");
            }
            return check_status;
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}