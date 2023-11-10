using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.account.w_sheet_ucf_contcode_ctrl
{
    public partial class w_sheet_ucf_contcode : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        public void InitJsPostBack()
        {
            wd_list.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                wd_list.RetrieveList();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                wd_list.InsertLastRow();
                wd_list.FindTextBox(wd_list.RowCount - 1, "cnt_code").Focus();
                int row = wd_list.RowCount-1;
                wd_list.DATA[row].COOP_ID = state.SsCoopId;
                
                

            }
            else if (eventArg == PostDeleteRow)
            {
                int ls_getrow = wd_list.GetRowFocus();
                string cnt_code = wd_list.DATA[ls_getrow].cnt_code;
                if (IsUsedProvince(cnt_code))
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถลบได้เนื่องจากมีการใช้งานอยู่");
                }
                else
                {
                    wd_list.DeleteRow(ls_getrow);
                }
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);

                exed1.AddRepeater(wd_list);
                exed1.Execute();
                wd_list.RetrieveList();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                wd_list.ResetRow();
                wd_list.RetrieveList();


            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
        public bool IsUsedProvince(string cnt_code)
        {
            string sql = "select cnt_code from acccntmoneysheet where cnt_code={0}";
            bool chk = false;
            try
            {
                sql = WebUtil.SQLFormat(sql, cnt_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    chk = true;
                }
            }catch(Exception ex){
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            return chk;
        }
    }
}