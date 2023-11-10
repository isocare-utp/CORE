using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mthexpense_ctrl
{
    public partial class ws_mthexpense : PageWebSheet, WebSheet
    {
        private string memb_no;
        [JsPostBack]
        public string PostMember { get; set; }
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string PostCalSum { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDs(this);
            dsSum.InitDsSum(this);
        }
        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                //dsList.Retrieve();//show data first
                //dsList.RetriveGroup();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMember")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(memb_no);
                dsList.RetrieveData(memb_no);
                cal_sum_pay();
                cal_sum_recv();
                //update_numrow();
            }
            else if (eventArg == PostNewRow)
            {

                dsList.InsertLastRow(1);
                int row = dsList.RowCount; //นับแถว
                String row_seq = dsList.DATA[row - 1].SEQ_NO;
                dsList.DATA[row - 1].SEQ_NO = Convert.ToString(row);
                //dsList.DATA[row - 1].SIGN_FLAG = "-1";
                cal_sum_pay();
                cal_sum_recv();
            }
            else if (eventArg == PostDelRow)
            {
                String ls_memb = "";
                int row = dsList.GetRowFocus();
                String member_no = dsMain.DATA[0].MEMBER_NO;
                String seq_no = dsList.DATA[row].SEQ_NO;

                //dsList.DeleteRow(row);
                try
                {
                    ls_memb = @"DELETE FROM mbmembmthexpense WHERE coop_id={0} and  member_no={1} and seq_no={2}";
                    ls_memb = WebUtil.SQLFormat(ls_memb, state.SsCoopId, member_no, seq_no);
                    WebUtil.ExeSQL(ls_memb);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
                    dsList.RetrieveData(member_no);
                    SaveWebSheet();
                    
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเสร็จ");
                }
            }
            else if (eventArg == PostCalSum)
            {
                cal_sum_pay();
                cal_sum_recv();
            }


        }

        public void SaveWebSheet()
        {
            try
            {
                String member_no = dsMain.DATA[0].MEMBER_NO;
                String sign_flag = "";
                String seq_no = "";
                String mthexpense_desc = "";
                Decimal mthexpense_amt = 0;
                String sqlStr = "";
                int row = dsList.RowCount;
                int num = 0;
                for (int i = 0; i < row; i++)
                {
                    sign_flag = dsList.DATA[i].SIGN_FLAG;
                    seq_no = dsList.DATA[i].SEQ_NO;
                    mthexpense_desc = dsList.DATA[i].MTHEXPENSE_DESC;
                    mthexpense_amt = dsList.DATA[i].MTHEXPENSE_AMT;
                    num = i + 1;

                    string sql = @"select * from mbmembmthexpense WHERE coop_id={0} and  member_no={1} and seq_no={2}";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no, seq_no);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        sqlStr = @" UPDATE mbmembmthexpense SET sign_flag = {2},mthexpense_desc = {4},mthexpense_amt = {5}, seq_no ={6}
                        WHERE coop_id={0} AND member_no={1} AND seq_no={3}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, member_no, sign_flag, seq_no, mthexpense_desc, mthexpense_amt, num);
                        WebUtil.ExeSQL(sqlStr);
                    }

                    else
                    {
                        sqlStr = @"INSERT INTO mbmembmthexpense 
                        (coop_id,member_no,sign_flag,seq_no,mthexpense_desc,mthexpense_amt)
                        VALUES ({0},{1},{2},{3},{4},{5})";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, member_no, sign_flag, seq_no, mthexpense_desc, mthexpense_amt);
                        WebUtil.ExeSQL(sqlStr);
                    }

                }

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");

                dsMain.RetrieveMain(member_no);
                dsList.RetrieveData(member_no);
                cal_sum_pay();
                cal_sum_recv();

                /*dsMain.ResetRow();
                dsList.ResetRow();
                dsSum.ResetRow();*/
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ");
            }
        }


        public void WebSheetLoadEnd()
        {


        }

        public void cal_sum_pay()
        {
            int row = dsList.RowCount;
            decimal SUMMTHEXPENSE_AMT = 0;
            string SIGN_FLAG = "";
            for (int i = 0; i < row; i++)
            {
                SIGN_FLAG = dsList.DATA[i].SIGN_FLAG;
                if (SIGN_FLAG == "-1")
                {
                    SUMMTHEXPENSE_AMT += dsList.DATA[i].MTHEXPENSE_AMT;
                }
            }
            dsSum.DATA[0].Sum_amt = SUMMTHEXPENSE_AMT;
        }

        public void cal_sum_recv()
        {
            int row = dsList.RowCount;
            decimal SUMMTHEXPENSE_AMT = 0;
            string SIGN_FLAG = "";
            for (int i = 0; i < row; i++)
            {
                SIGN_FLAG = dsList.DATA[i].SIGN_FLAG;
                if (SIGN_FLAG == "1")
                {
                    SUMMTHEXPENSE_AMT += dsList.DATA[i].MTHEXPENSE_AMT;
                }
            }
            dsSum.DATA[0].SUM_RECV = SUMMTHEXPENSE_AMT;
        }

        //        public void update_numrow() 
        //        {
        //        String member_no = dsMain.DATA[0].MEMBER_NO;
        //        int row = dsList.RowCount;
        //        int seq_no = 0;
        //        for (int i = 0; i < row; i++) {
        //            int _cal = i + 1;
        //            seq_no = Convert.ToInt32(dsList.DATA[i].SEQ_NO);
        //            if(_cal != seq_no){
        //                try
        //                {
        //                    //เรียงลำดับ
        //                    String sqlStr = @" UPDATE mbmembmthexpense set seq_no={3}
        //                    WHERE coop_id={0} AND member_no={1} AND seq_no ={2}";
        //                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, member_no, seq_no, _cal);
        //                    WebUtil.ExeSQL(sqlStr);
        //                }
        //                catch (Exception ex)
        //                {
        //                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
        //                } 


        //            }

        //         }

        //        }
    }
}