using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.account.w_acc_ucf_accperiod_ctrl
{
    public partial class w_acc_ucf_accperiod : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String Postperiod { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }


        public void InitJsPostBack()
        {
            wd_main.InitList(this);
            wd_list.InitList(this);
            
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
               
                wd_main.RetrieveList();
               
                
                for (int a = 0; a < wd_main.RowCount; a++)
                {
                    wd_main.DATA[a].ACCOUNT_YEAR = Convert.ToString(Convert.ToInt32(wd_main.DATA[a].ACCOUNT_YEAR) + 543);

                }
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                wd_list.InsertLastRow();
                wd_list.FindTextBox(wd_list.RowCount - 1, "account_year").Focus();
                int ls_getrow = wd_list.RowCount - 1;
                wd_list.SetItem(ls_getrow, wd_list.DATA.PERIODColumn, Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow - 1].PERIOD + 1)));
                wd_list.SetItem(ls_getrow, wd_list.DATA.PERIOD_PREVColumn, Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow - 1].PERIOD )));
                wd_list.SetItem(ls_getrow, wd_list.DATA.ACCOUNT_YEARColumn, Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow - 1].ACCOUNT_YEAR) ));
                wd_list.SetItem(ls_getrow, wd_list.DATA.ACCOUNT_YEAR_PREVColumn, Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow - 1].ACCOUNT_YEAR_PREV)));
                wd_list.DATA[ls_getrow].COOP_ID = state.SsCoopId;
                wd_list.DATA[ls_getrow].BRANCH_ID ="000";
            }
               else if (eventArg == Postperiod)
            {
                int r = wd_main.GetRowFocus();
                string account_year = Convert.ToString(Convert.ToInt32(wd_main.DATA[r].ACCOUNT_YEAR) - 543);
                wd_list.RetrieveList(account_year);
            }
            else if (eventArg == PostDeleteRow)
            {
                ExecuteDataSource exx = new ExecuteDataSource(this);
                int ls_getrow = wd_list.GetRowFocus();
               // wd_list.DeleteRow(ls_getrow);
                string account_year = Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow].ACCOUNT_YEAR));
                string period = Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow].PERIOD));
                string sqldelete = "delete from accperiod where account_year = '" + account_year + "' and  period = '" + Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow].PERIOD)) + "'";
                WebUtil.Query(sqldelete);
                exx.SQL.Add(sqldelete);
                exx.Execute();
                wd_list.RetrieveList(account_year);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบเสร็จเรียบร้อย");
            }
        }

        public void SaveWebSheet()
        {
            try
            {

                ExecuteDataSource exed1 = new ExecuteDataSource(this);

                exed1.AddRepeater(wd_list);
                exed1.Execute();
               // wd_list.RetrieveList();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                wd_list.ResetRow();
               // wd_list.RetrieveList();


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