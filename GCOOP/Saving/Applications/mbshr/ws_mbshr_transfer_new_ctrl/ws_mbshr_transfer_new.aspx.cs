using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_transfer_new_ctrl
{
    public partial class ws_mbshr_transfer_new : PageWebSheet, WebSheet
    {
        private string memb_no;
        [JsPostBack]
        public string PostMember { get; set; }
        [JsPostBack]
        public string SetRefMember { get; set; }
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        //public string PostMem { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDs(this);
        }
        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMember")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(memb_no);
                dsList.RetrieveData(memb_no);

            }
            else if (eventArg == "SetRefMember")
            {

                int row = Convert.ToInt32(HdRow.Value);
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[row].MEMBER_NO);
                dsList.RetrieveData(memb_no);
                String TRAN_DETAIL = "";
                decimal TRAN_SHARE = 0;
                decimal TRAN_LOAN = 0;
                int TRAN_PERIOD = 0;
                DateTime TRAN_DATE = state.SsWorkDate;

                try
                {
                    String sql = @" select 
		                    coop_id,   
		                    member_no ,   
		                    tran_date,        
		                    tran_period,
		                    tran_share,
		                    tran_loan,
		                    tran_detail
		                    from mbmembtransfer
		                    where   (mbmembtransfer.member_no = {1}) and 
		                            ( mbmembtransfer.coop_id = {0} )";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, memb_no);
                    Sdt dt2 = WebUtil.QuerySdt(sql);
                    if (dt2.Next())
                    {
                        TRAN_DETAIL = dt2.GetString("TRAN_DETAIL");
                        TRAN_SHARE = dt2.GetDecimal("TRAN_SHARE");
                        TRAN_PERIOD = dt2.GetInt32(Convert.ToInt32("TRAN_PERIOD"));
                    }
                    dsList.DATA[row].TRAN_DETAIL = TRAN_DETAIL;
                    dsList.DATA[row].MEMBER_NO = memb_no;
                    dsList.DATA[row].TRAN_SHARE = TRAN_SHARE;
                    dsList.DATA[row].TRAN_LOAN = TRAN_LOAN;
                    dsList.DATA[row].TRAN_PERIOD = TRAN_PERIOD;
                    dsList.DATA[row].TRAN_DATE = TRAN_DATE;
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ข้อมูลผิดพลาด");
                }

            }
            else if (eventArg == PostNewRow)
            {

                dsList.InsertLastRow(1);
                //dsList.CMTType();
                //int row = dsList.RowCount; //นับแถว
                //int row = 1; 
                //dsList.DATA[row - 1].SEQ_NO = Convert.ToString(row);
                //dsList.FindTextBox(row, "item_amount").Enabled = true; 

            }
            else if (eventArg == PostDelRow)
            {

            }
        }
        public void SaveWebSheet()
        {
            try
            {
            String member_no = dsMain.DATA[0].MEMBER_NO;
            DateTime tran_date ;
            String tran_detail = "";
            String sqlStr = "";
            int row = dsList.RowCount;
            int tran_period = 0;
            decimal tran_share = 0;
            decimal tran_loan = 0;
            int num = 0;
                    for (int i = 0; i < row; i++)
                    {
                    tran_date = dsList.DATA[i].TRAN_DATE;
                    tran_detail = dsList.DATA[i].TRAN_DETAIL;
                    tran_period = dsList.DATA[i].TRAN_PERIOD;
                    tran_share = dsList.DATA[i].TRAN_SHARE;
                    tran_loan = dsList.DATA[i].TRAN_LOAN;
                    num = i + 1;

                        string sql = @"select * from mbmembtransfer WHERE coop_id={0} and  member_no={1} ";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no );
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {

                            sqlStr = @" UPDATE mbmembtransfer SET tran_detail = {2},tran_period = {3},tran_share = {4},tran_loan = {5} ,tran_date = {6}
                         WHERE coop_id={0} AND member_no={1}";
                            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, member_no, tran_detail, tran_period, tran_share, tran_loan, tran_date);
                            WebUtil.ExeSQL(sqlStr);
                        }

                        else
                        {
                            sqlStr = @"INSERT INTO mbmembtransfer 
                            (coop_id,member_no,tran_date,tran_detail,tran_period,tran_share,tran_loan)
                            VALUES ({0},{1},{2},{3},{4},{5},{6})";
                            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, member_no, tran_date, tran_detail, tran_period, tran_share, tran_loan);
                            WebUtil.ExeSQL(sqlStr);
                        }

                    }

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
                    dsMain.ResetRow();
                    dsList.ResetRow();
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ");
                }
            }          

        public void WebSheetLoadEnd()
        {

           
        }

        
    }
}