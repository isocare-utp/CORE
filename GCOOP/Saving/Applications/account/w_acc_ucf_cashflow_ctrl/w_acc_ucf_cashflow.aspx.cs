using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data.OracleClient; //เพิ่มเติม
using Sybase.DataWindow; //เพิ่มเติม
using System.Globalization; //เพิ่มเติม

namespace Saving.Applications.account.w_acc_ucf_cashflow_ctrl
{
    public partial class w_acc_ucf_cashflow : PageWebSheet, WebSheet
    {

        protected String jsPostGetAccid;
        #region WebSheet Members

        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        [JsPostBack]
        public String PostGetAccid { get; set; }

        public void InitJsPostBack()
        {
            wd_list.InitList(this);
            jsPostGetAccid = WebUtil.JsPostBack(this, "jsPostGetAccid");

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
                wd_list.FindTextBox(wd_list.RowCount - 1, "seq_no").Focus();

            }
            else if (eventArg == PostDeleteRow)
            {
                int ls_getrow = wd_list.GetRowFocus();
                string seq_no = wd_list.DATA[ls_getrow].SEQ_NO;

                wd_list.DeleteRow(ls_getrow);

            }
            else if (eventArg == PostGetAccid)
            {
                GetAccid();


                //int ls_getrow = wd_list.GetRowFocus();
                //string seq_no = wd_list.DATA[ls_getrow].SEQ_NO;
                //string acclist = wd_list.DATA[ls_getrow].ACCID_LIST;
                //string sql = "update ACCUCFCASHFLOW set accid_list  = '" + acclist + "' where seq_no = '" + seq_no + "' ";
            }




        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);

                for (int r = 0; r < wd_list.RowCount; r++)
                {
                    wd_list.DATA[r].COOP_ID = state.SsCoopControl;
                }


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
        public bool IsUsedseq_no(string seq_no)
        {
            string sql = "select seq_no from accmaster where section_id={0}";
            bool chk = false;
            try
            {
                sql = WebUtil.SQLFormat(sql, seq_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    chk = true;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            return chk;
        }


        #endregion

        #region Function
        private void GetAccid()
        {
            string acc_list = Hdacclist.Value;
            int row = wd_list.GetRowFocus();
            wd_list.SetItem(row, wd_list.DATA.ACCID_LISTColumn, acc_list);
           // wd_list.SetItemString(row, "accid_list", acc_list);
        }
        #endregion
    }    
    
}