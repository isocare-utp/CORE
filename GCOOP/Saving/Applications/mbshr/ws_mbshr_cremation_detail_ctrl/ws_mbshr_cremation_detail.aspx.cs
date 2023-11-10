using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_cremation_detail_ctrl
{
    public partial class ws_mbshr_cremation_detail : PageWebSheet, WebSheet
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
            dsSum.InitDsSum(this);
        }
        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                this.SetOnLoadedScript(" parent.Setfocus();");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMember")
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(memb_no);
                dsList.RetrieveData(memb_no);
                dsSum.Retrieve_Sum(memb_no);
                dsList.CMTType();

            }
            else if (eventArg == "SetRefMember")
            {

                int row = Convert.ToInt32(HdRow.Value);
                string memb_no = WebUtil.MemberNoFormat(dsList.DATA[row].MEMBER_NO);
                dsList.RetrieveData(memb_no);
                String DESCRIPTION = "";
                decimal ITEM_AMOUNT = 0;

                try
                {
                    String sql = @"select 
		                    coop_id,   
		                    member_no ,   
		                    seq_no,        
		                    cmttype_code,
		                    description,
                            item_amount   
		                    from mbcremationdet
		                    where   (mbcremationdet.member_no = {1}) and 
		                            ( mbcremationdet.coop_id = {0} )";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId, memb_no);
                    Sdt dt2 = WebUtil.QuerySdt(sql);
                    if (dt2.Next())
                    {
                        DESCRIPTION = dt2.GetString("DESCRIPTION");
                        ITEM_AMOUNT = dt2.GetDecimal("ITEM_AMOUNT");
                    }
                    dsList.DATA[row].DESCRIPTION = DESCRIPTION;
                    dsList.DATA[row].MEMBER_NO = memb_no;
                    dsList.DATA[row].ITEM_AMOUNT = ITEM_AMOUNT;
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ข้อมูลผิดพลาด");
                }

            }
            else if (eventArg == PostNewRow)
            {

                dsList.InsertLastRow(1);
                dsList.CMTType();
                int row = dsList.RowCount; //นับแถว
                  dsList.DATA[row - 1].SEQ_NO = Convert.ToString(row);
                dsList.FindTextBox(row, "item_amount").Enabled = true; 

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
                    ls_memb = @"DELETE FROM mbcremationdet WHERE coop_id={0} and  member_no={1} and seq_no={2}";
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
        }
        public void SaveWebSheet()
        {
            try
            {
                    String member_no = dsMain.DATA[0].MEMBER_NO;
                    String seq_no = "";
                    String description = "";
                    String sqlStr = "";
                    int row = dsList.RowCount;
                    String cmttype_code = "";
                    decimal item_amount = 0;
                    int num = 0;
                    for (int i = 0; i < row; i++)
                    {
                     seq_no = dsList.DATA[i].SEQ_NO;
                    description = dsList.DATA[i].DESCRIPTION;
                    cmttype_code = dsList.DATA[i].CMTTYPE_CODE;
                    item_amount = dsList.DATA[i].ITEM_AMOUNT;
                        num = i + 1;

                        string sql = @"select * from mbcremationdet WHERE coop_id={0} and  member_no={1} and seq_no={2}";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no, seq_no);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                        sqlStr = @" UPDATE mbcremationdet SET cmttype_code = {2},description = {4},item_amount = {5}, seq_no ={6}
                        WHERE coop_id={0} AND member_no={1} AND seq_no={3}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, member_no, cmttype_code, seq_no, description, item_amount, num);
                        WebUtil.ExeSQL(sqlStr);
                        }

                        else
                        {
                            sqlStr = @"INSERT INTO mbcremationdet 
                            (coop_id,member_no,seq_no,description,cmttype_code,item_amount)
                            VALUES ({0},{1},{2},{3},{4},{5})";
                            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, member_no, seq_no, description, cmttype_code, item_amount);
                            WebUtil.ExeSQL(sqlStr);
                        }

                    }


                    string sql_mb = @" UPDATE mbmembmaster SET remark_cremation = {0}
                        WHERE coop_id={1} AND member_no={2}";
                    sql_mb = WebUtil.SQLFormat(sql_mb,dsMain.DATA[0].REMARK_CREMATION ,state.SsCoopId, member_no);
                    WebUtil.ExeSQL(sql_mb);

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
                    dsMain.ResetRow();
                    dsList.ResetRow();

                    this.SetOnLoadedScript(" parent.Setfocus();");
                
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