using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.account.w_acc_ucf_accyear_ctrl
{
    public partial class w_acc_ucf_accyear : PageWebSheet, WebSheet
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
                NewClear();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInsertRow)
            {
                wd_list.InsertLastRow();
                wd_list.FindTextBox(wd_list.RowCount - 1, "account_year").Focus();
                int ls_getrow = wd_list.RowCount - 1;
                wd_list.SetItem(ls_getrow, wd_list.DATA.ACCOUNT_YEARColumn, Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow - 1].ACCOUNT_YEAR) + 1));
            }
            else if (eventArg == PostDeleteRow)
            {
                int ls_getrow = wd_list.GetRowFocus();
                string account_year = Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow].ACCOUNT_YEAR) - 543);
                if (IsUsedaccsection(account_year))
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถลบได้เนื่องจากมีการใช้งานอยู่");
                }
                else
                {
                    ExecuteDataSource exx = new ExecuteDataSource(this);
                    //int rows = wd_list.GetRowFocus();
                    
                    string sqldelete = "delete from accaccountyear where account_year = '" + Convert.ToString(Convert.ToInt32(wd_list.DATA[ls_getrow].ACCOUNT_YEAR) - 543) + "'";
                    WebUtil.Query(sqldelete);
                    exx.SQL.Add(sqldelete);
                    exx.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("delete " + account_year);
                    //--------wd_list.RetrieveList();
                    //wd_list.DeleteRow(ls_getrow);
                    NewClear();
                }
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                //ExecuteDataSource exx = new ExecuteDataSource(this);
                //string sqldelete = "delete from accaccountyear";
                //WebUtil.Query(sqldelete);
                //exx.SQL.Add(sqldelete);
                //exx.Execute();
                for (int r = 0; r < wd_list.RowCount; r++)
                {
                    wd_list.DATA[r].COOP_ID = state.SsCoopControl;
                    wd_list.DATA[r].ACCOUNT_YEAR = Convert.ToString(Convert.ToInt32(wd_list.DATA[r].ACCOUNT_YEAR) - 543);
                    wd_list.DATA[r].CLOSE_ACCOUNT_STAT = "0";
                    
                }


                exed1.AddRepeater(wd_list);
                exed1.Execute();
                //int result = exed1.Execute();
                //wd_list.RetrieveList();
                //LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ"+ result);
                //wd_list.ResetRow();
                //wd_list.RetrieveList();


            }
            catch (Exception ex)//(Exception ex)
            {
               // LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
             LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                wd_list.ResetRow();
                //-------wd_list.RetrieveList();
            NewClear();
        }

        public void WebSheetLoadEnd()
        {

        }
        public bool IsUsedaccsection(string account_year)
        {
            string sql = "select account_year from accperiod where account_year={0}";
            bool chk = false;
            try
            {
                sql = WebUtil.SQLFormat(sql, account_year);
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

        public void NewClear()
        {
            wd_list.RetrieveList();

            for (int a = 0; a < wd_list.RowCount; a++)
            {
                wd_list.DATA[a].ACCOUNT_YEAR = Convert.ToString(Convert.ToInt32(wd_list.DATA[a].ACCOUNT_YEAR) + 543);
            }
        }


    }
}