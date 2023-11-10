using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_mb_mthexpense_adjust_ctrl
{
    public partial class ws_mb_mthexpense_adjust : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostInsertRowPlus { get; set; }
        [JsPostBack]
        public String PostInsertRowMinus { get; set; }
        [JsPostBack]
        public String PostDeleteRowPlus { get; set; }
        [JsPostBack]
        public String PostDeleteRowMinus { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsMinus.InitDsMinus(this);
            dsPlus.InitDsPlus(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdMembtypeCode();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(member_no);
                dsMain.DdMembtypeCode();
                dsMinus.RetrieveMinus(member_no);
                dsPlus.RetrievePlus(member_no);
                SumTotal();
            }
            else if (eventArg == PostInsertRowPlus)
            {
                dsPlus.InsertLastRow();
                int currow = dsPlus.RowCount - 1;
                try
                {
                    dsPlus.DATA[currow].SEQ_NO = dsPlus.GetMaxValueDecimal("SEQ_NO") + 1;
                    dsPlus.DATA[currow].COOP_ID = state.SsCoopControl;
                    dsPlus.DATA[currow].SIGN_FLAG = 1;
                    dsPlus.DATA[currow].MEMBER_NO = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                }
                catch
                {
                    if (dsPlus.RowCount < 1)
                    {
                        dsPlus.DATA[currow].SEQ_NO = 1;
                    }
                }
            }
            else if (eventArg == PostInsertRowMinus)
            {
                dsMinus.InsertLastRow();
                int currow = dsMinus.RowCount - 1;
                try
                {
                    dsMinus.DATA[currow].SEQ_NO = dsMinus.GetMaxValueDecimal("SEQ_NO") + 1;
                    dsMinus.DATA[currow].COOP_ID = state.SsCoopControl;
                    dsMinus.DATA[currow].SIGN_FLAG = -1;
                    dsMinus.DATA[currow].MEMBER_NO = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                }
                catch
                {
                    if (dsMinus.RowCount < 1)
                    {
                        dsMinus.DATA[currow].SEQ_NO = 1;
                    }
                }
            }
            else if (eventArg == PostDeleteRowPlus)
            {
                int r = dsPlus.GetRowFocus();
                dsPlus.DeleteRow(r);
            }
            else if (eventArg == PostDeleteRowMinus)
            {
                int r = dsMinus.GetRowFocus();
                dsMinus.DeleteRow(r);
            }

        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                string sql = @"delete from MBMEMBMTHEXPENSE where member_no='" + member_no + "'";
                exed1.SQL.Add(sql);

                for (int i = 0; i < dsPlus.RowCount; i++)
                {

                    string member_no2 = dsPlus.DATA[i].MEMBER_NO;
                    decimal sign_flag = dsPlus.DATA[i].SIGN_FLAG;
                    decimal seq_no = i + 1;
                    string mthexpense_desc = dsPlus.DATA[i].MTHEXPENSE_DESC;
                    decimal mthexpense_amt = dsPlus.DATA[i].MTHEXPENSE_AMT;


                    string saveplus = "insert into mbmembmthexpense (coop_id,member_no,sign_flag,seq_no,mthexpense_desc,mthexpense_amt) values ('"
                        + state.SsCoopControl + "','" + member_no2 + "'," + sign_flag + "," + seq_no + ",'" + mthexpense_desc + "'," + mthexpense_amt + ")";


                    exed1.SQL.Add(saveplus);

                    exed1.Execute();

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                }
                for (int k = 0; k < dsMinus.RowCount; k++)
                {

                    string member_no3 = dsMinus.DATA[k].MEMBER_NO;
                    decimal sign_flag2 = dsMinus.DATA[k].SIGN_FLAG;
                    string mthexpense_desc2 = dsMinus.DATA[k].MTHEXPENSE_DESC;
                    decimal mthexpense_amt2 = dsMinus.DATA[k].MTHEXPENSE_AMT;

                    decimal seq_no2 = k + 1;

                    string saveminus = "insert into mbmembmthexpense (coop_id,member_no,sign_flag,seq_no,mthexpense_desc,mthexpense_amt) values ('"
                        + state.SsCoopControl + "','" + member_no3 + "'," + sign_flag2 + "," + seq_no2 + ",'" + mthexpense_desc2 + "'," + mthexpense_amt2 + ")";
                    exed1.SQL.Add(saveminus);
                    exed1.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }

                dsMinus.ResetRow();
                dsMain.ResetRow();
                dsPlus.ResetRow();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {

        }

        public void SumTotal()
        {
            //คิดผลรวมจำนวนเงิน รายได้อื่นๆ
            int row_count = dsPlus.RowCount;
            decimal cpsum_mthexpense_amt = 0;
            for (int i = 0; i < row_count; i++)
            {
                decimal mthexpense_amt = dsPlus.DATA[i].MTHEXPENSE_AMT;
                cpsum_mthexpense_amt += mthexpense_amt;


            }
            dsPlus.TContract.Text = cpsum_mthexpense_amt.ToString("#,##0.00");
            //คิดผลรวมเงินที่ใช้ค้ำ รายการค้ำประกันใบคำขอ
            int row = dsMinus.RowCount;
            decimal cpsum_mthexpense_amt2 = 0;
            for (int i = 0; i < row; i++)
            {
                decimal mthexpense_amt2 = dsMinus.DATA[i].MTHEXPENSE_AMT;
                cpsum_mthexpense_amt2 += mthexpense_amt2;


            }
            dsMinus.TContract.Text = cpsum_mthexpense_amt2.ToString("#,##0.00");
        }
    }
}