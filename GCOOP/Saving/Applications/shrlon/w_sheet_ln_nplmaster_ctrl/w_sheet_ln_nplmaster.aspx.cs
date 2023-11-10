using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl
{
    public partial class w_sheet_ln_nplmaster : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostLoanContractNo { get; set; }
        [JsPostBack]
        public String PostAddRowFollow1 { get; set; }
        [JsPostBack]
        public String PostAddRowFollow2 { get; set; }
        [JsPostBack]
        public String PostAddRowBoard { get; set; }
        [JsPostBack]
        public String PostDelRowFollow1 { get; set; }
        [JsPostBack]
        public String PostDelRowFollow2 { get; set; }
        [JsPostBack]
        public String PostDelRowBoard { get; set; }
        [JsPostBack]
        public String PostFollowSeq { get; set; }
        [JsPostBack]
        public String PostDeleteFollowSeq { get; set; }
        [JsPostBack]
        public String PostReport { get; set; }

        public void InitJsPostBack()
        {
            dsMemb.InitDs(this);
            dsLoan.InitDs(this);
            dsNPL.InitDs(this);
            dsFollowMast.InitDs(this);
            dsFollow1.InitDs(this, "dsFollow1");
            dsFollow2.InitDs(this, "dsFollow2");
            dsBoard.InitDs(this);
            dsResolution.InitDs(this);
            dsMort.InitDs(this);
            dsColl.InitDs(this);
            dsCollSub.InitDs(this);
            //dsReport.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                JsPostMemberNo();
            }
            else if (eventArg == PostLoanContractNo)
            {
                JsPostLoanContractNo();
            }
            else if (eventArg == PostAddRowFollow1)
            {
                dsFollow1.InsertAtRow(0);
                dsFollow1.DATA[0].DONE = "N";
            }
            else if (eventArg == PostAddRowFollow2)
            {
                dsFollow2.InsertAtRow(0);
                dsFollow2.DATA[0].DONE = "Y";
            }
            else if (eventArg == PostAddRowBoard)
            {
                dsBoard.InsertAtRow(0);
                dsBoard.DATA[0].COOP_ID = state.SsCoopControl;
                dsBoard.DATA[0].LOANCONTRACT_NO = dsLoan.DATA[0].LOANCONTRACT_NO;
                decimal maxSeq = dsBoard.GetMaxValueDecimal("seq_no");
                dsBoard.DATA[0].SEQ_NO = maxSeq + 1;
            }
            else if (eventArg == PostDelRowFollow1)
            {
                int row = dsFollow1.GetRowFocus();
                dsFollow1.DeleteRow(row);
            }
            else if (eventArg == PostDelRowFollow2)
            {
                int row = dsFollow2.GetRowFocus();
                dsFollow2.DeleteRow(row);
            }
            else if (eventArg == PostDelRowBoard)
            {
                int row = dsBoard.GetRowFocus();
                dsBoard.DeleteRow(row);
            }
            else if (eventArg == PostFollowSeq)
            {
                if (dsFollowMast.DATA[0].FOLLOW_SEQ == 999) return;
                dsFollowMast.Retrieve(dsLoan.DATA[0].LOANCONTRACT_NO, dsLoan.DATA[0].MEMBER_NO, dsFollowMast.DATA[0].FOLLOW_SEQ);
                dsFollowMast.DdFollowSeq(dsLoan.DATA[0].MEMBER_NO);
                dsFollow1.Retrieve(dsLoan.DATA[0].MEMBER_NO, dsFollowMast.DATA[0].FOLLOW_SEQ, "N");
                dsFollow2.Retrieve(dsLoan.DATA[0].MEMBER_NO, dsFollowMast.DATA[0].FOLLOW_SEQ, "Y");
                dsNPL.DATA[0].MARGIN = dsFollowMast.DATA[0].ADVANCE_AMT;
            }
            else if (eventArg == PostDeleteFollowSeq)
            {
                dsFollow1.ResetRow();
                dsFollow2.ResetRow();
            }
            else if (eventArg == PostReport)
            {
                if (!string.IsNullOrEmpty(dsNPL.DATA[0].LOANCONTRACT_NO))
                {
                    string sql = "update lnnplmaster set int_lastcal = {2} where coop_id = {0} and loancontract_no = {1}";
                    decimal intLastCal = wcf.NShrlon.of_computeinterest(state.SsWsPass, state.SsCoopControl, dsNPL.DATA[0].LOANCONTRACT_NO, state.SsWorkDate);
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsNPL.DATA[0].LOANCONTRACT_NO, intLastCal);
                    int ii = WebUtil.ExeSQL(sql);
                }
                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                args.Add("as_contno", iReportArgumentType.String, dsLoan.DATA[0].LOANCONTRACT_NO.Trim());
                args.Add("adtm_workdate", iReportArgumentType.Date, state.SsWorkDate);
                iReportBuider report = new iReportBuider(this, "รายงานคุณสมบัติลูกหนี้มีปัญหา");
                report.AddCriteria("r_npl_loancontract_detail", "รายงานคุณสมบัติลูกหนี้มีปัญหา", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                if (string.IsNullOrEmpty(dsMemb.DATA[0].MEMBER_NO))
                {
                    throw new Exception("ไม่มีข้อมูลทะเบียนสมาชิก");
                }
                string loancontractNo = dsLoan.DATA[0].LOANCONTRACT_NO;
                string memberNo = dsNPL.DATA[0].MEMBER_NO;
                if (string.IsNullOrEmpty(loancontractNo) || string.IsNullOrEmpty(memberNo))
                {
                    throw new Exception("ไม่มีข้อมูลทะเบียนสมาชิกหรือเลขสัญญา");
                }
                //เซ็ต primary key ซ้ำเพื่อความมั่นใจ
                dsNPL.DATA[0].LOANCONTRACT_NO = loancontractNo;
                dsNPL.DATA[0].MEMBER_NO = memberNo;
                dsNPL.DATA[0].COOP_ID = state.SsCoopControl;

                //ประกาศตัวแปร exed สำหรับเชื่อมต่อ transaction database อีกตัว
                ExecuteDataSource exed = new ExecuteDataSource(this);
                try
                {
                    //ลบทุกตารางที่เกี่ยวข้อง
                    string sql = WebUtil.SQLFormat("delete from lnnplboardmeetingsub where coop_id = {0} and loancontract_no = {1}", state.SsCoopControl, loancontractNo);
                    exed.SQL.Add(sql);
                    sql = WebUtil.SQLFormat("delete from lnnplcollmast where coop_id = {0} and ref_collno in (select ref_collno from lncontcoll where coop_id={0} and loancontract_no={1})", state.SsCoopControl, loancontractNo);
                    exed.SQL.Add(sql);

                    if (dsFollowMast.DATA[0].FOLLOW_SEQ != 999)
                    {
                        sql = WebUtil.SQLFormat("delete from lnnplfollowdetail where coop_id = {0} and member_no = {1} and follow_seq = {2}", state.SsCoopControl, memberNo, dsFollowMast.DATA[0].FOLLOW_SEQ);
                        exed.SQL.Add(sql);
                    }

                    sql = WebUtil.SQLFormat("delete from lnnplmaster where coop_id = {0} and loancontract_no = {1}", state.SsCoopControl, loancontractNo);
                    exed.SQL.Add(sql);

                    //LNNPLMASTER
                    sql = dsNPL.CreateSyntaxInsert();
                    exed.SQL.Add(sql);
                    sql = dsMort.CreateSyntaxUpdate();
                    exed.SQL.Add(sql);
                    sql = dsResolution.CreateSyntaxUpdate();
                    exed.SQL.Add(sql);

                    //LNNPLBOARDMEETINGSUB
                    dsBoard.ChangeRowStatusInsert();
                    exed.AddRepeater(dsBoard);

                    //LNNPLCOLLMAST
                    dsColl.ChangeRowStatusInsert();
                    List<String> sqlColls = dsColl.CreateSyntax();
                    for (int i = 0; i < dsColl.RowCount; i++)
                    {
                        if (!string.IsNullOrEmpty(dsColl.DATA[i].COLLMAST_NO))
                        {
                            sql = sqlColls[i];
                            exed.SQL.Add(sql);
                        }
                    }

                    //LNNPLFOLLOWMASTER & DETAIL
                    decimal followSeq = dsFollowMast.DATA[0].FOLLOW_SEQ;
                    if (followSeq > 0)
                    {
                        if (followSeq == 999)
                        {
                            string sqlf = "select max(follow_seq) as max_follow_seq from lnnplfollowmaster where coop_id = {0} and member_no = {1}";
                            sqlf = WebUtil.SQLFormat(sqlf, state.SsCoopControl, dsLoan.DATA[0].MEMBER_NO);
                            Sdt dt = WebUtil.QuerySdt(sqlf);
                            if (!dt.Next())
                            {
                                throw new Exception("ไม่พบข้อมูลสูงสุดของ follow_seq ");
                            }
                            followSeq = dt.GetDecimal(0) + 1;
                            dsFollowMast.DATA[0].COOP_ID = state.SsCoopControl;
                            dsFollowMast.DATA[0].MEMBER_NO = memberNo;
                            dsFollowMast.DATA[0].FOLLOW_SEQ = followSeq;
                            sql = dsFollowMast.CreateSyntaxInsert();
                            exed.SQL.Add(sql);
                        }
                        else
                        {
                            sql = dsFollowMast.CreateSyntaxUpdate();
                            exed.SQL.Add(sql);
                        }
                        if (dsFollow1.RowCount == 0 && dsFollow2.RowCount == 0)
                        {
                            followSeq = 0;
                        }

                        //UPDATE LNNPLMASTER
                        sql = "update lnnplmaster set follow_seq={0} where coop_id={1} and loancontract_no={2}";
                        sql = WebUtil.SQLFormat(sql, followSeq, state.SsCoopControl, loancontractNo);
                        exed.SQL.Add(sql);

                        int ii = 1;
                        for (int i = 0; i < dsFollow1.RowCount; i++)
                        {
                            dsFollow1.DATA[i].COOP_ID = state.SsCoopControl;
                            dsFollow1.DATA[i].MEMBER_NO = dsLoan.DATA[0].MEMBER_NO;
                            dsFollow1.DATA[i].FOLLOW_SEQ = followSeq;
                            dsFollow1.DATA[i].SEQ_NO = ii;
                            ii++;
                        }
                        for (int i = 0; i < dsFollow2.RowCount; i++)
                        {
                            dsFollow2.DATA[i].COOP_ID = state.SsCoopControl;
                            dsFollow2.DATA[i].MEMBER_NO = dsLoan.DATA[0].MEMBER_NO;
                            dsFollow2.DATA[i].FOLLOW_SEQ = followSeq;
                            dsFollow2.DATA[i].SEQ_NO = ii;
                            ii++;
                        }
                        dsFollow1.ChangeRowStatusInsert();
                        dsFollow2.ChangeRowStatusInsert();
                        exed.AddRepeater(dsFollow1);
                        exed.AddRepeater(dsFollow2);
                    }
                    else
                    {
                        sql = "update lnnplmaster set follow_seq={0} where coop_id={1} and loancontract_no={2}";
                        sql = WebUtil.SQLFormat(sql, 0, state.SsCoopControl, loancontractNo);
                        exed.SQL.Add(sql);
                    }

                    //LNNPLFOLLOWMASTER DELETE 0
                    sql = "delete from lnnplfollowmaster a where a.coop_id={0} and a.member_no={1} and (select count(*) from lnnplfollowdetail b where b.coop_id=a.coop_id and b.member_no=a.member_no and b.follow_seq=a.follow_seq) <= 0";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memberNo);
                    exed.SQL.Add(sql);

                    //EXECUTE
                    int iii = exed.Execute();
                    JsPostLoanContractNo();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                }
                catch (Exception ex)
                {
                    throw new Exception("เกิดข้อผิดผลาดฐานข้อมูล " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
        }

        private void JsPostMemberNo()
        {
            try
            {
                string memberNo = WebUtil.MemberNoFormat(dsMemb.DATA[0].MEMBER_NO);
                dsMemb.Retrieve(memberNo);
                dsLoan.ResetRow();
                dsNPL.ResetRow();
                dsFollow1.ResetRow();
                dsFollow2.ResetRow();
                dsBoard.ResetRow();
                dsResolution.ResetRow();
                dsMort.ResetRow();
                dsColl.ResetRow();
                dsCollSub.ResetRow();
                //dsReport.ResetRow();
                if (string.IsNullOrEmpty(dsMemb.DATA[0].MEMBER_NO))
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลทะเบียน " + memberNo);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void JsPostLoanContractNo()
        {
            try
            {
                //ต้อง reset ds ทุกตัว ยกเว้น dsLoan, dsMemb
                dsNPL.ResetRow();
                dsFollowMast.ResetRow();
                dsFollow1.ResetRow();
                dsFollow2.ResetRow();
                dsBoard.ResetRow();
                dsResolution.ResetRow();
                dsMort.ResetRow();
                dsColl.ResetRow();
                dsCollSub.ResetRow();
                //dsReport.ResetRow();

                //ดึงข้อมูล
                string loanNo = dsLoan.DATA[0].LOANCONTRACT_NO;
                dsLoan.Retrieve(loanNo);

                //ถ้าไม่พบข้อมูลให้แจ้ง error
                if (string.IsNullOrEmpty(dsLoan.DATA[0].LOANCONTRACT_NO))
                {
                    throw new Exception("ไม่พบข้อมูลสัญญา " + loanNo);
                }

                ////ถ้า member_no ของ dsLoan กับ dsMemb ไม่เท่ากันเนื่องจากคีย์เลขสัญญาตรงๆ ให้ retrieve dsMemb ใหม่
                //if (dsLoan.DATA[0].MEMBER_NO != dsMemb.DATA[0].MEMBER_NO)
                //{
                //    dsMemb.Retrieve(dsLoan.DATA[0].MEMBER_NO);
                //    if (string.IsNullOrEmpty(dsMemb.DATA[0].MEMBER_NO))
                //    {
                //        throw new Exception("พบข้อผิดผลาดข้อมูลเลขทะเบียนที่ขัดแย้งกัน");
                //    }
                //}

                //ดึงข้อมูล dsNPL
                dsNPL.Retrieve(loanNo);

                //ถ้า dsNPL ไม่มีข้อมูลให้ทำการ insert ข้อมูลเปล่า ใช้ coop_id + loancontract_no เป็น pk และดึงข้อมูล dsNPL อีกรอบ
                if (string.IsNullOrEmpty(dsNPL.DATA[0].LOANCONTRACT_NO))
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ยังไม่มีข้อมูลลูกหนี้มีปัญหา");
                    dsNPL.DATA[0].COOP_ID = state.SsCoopControl;
                    dsNPL.DATA[0].WORK_ORDER = "00";
                    dsNPL.DATA[0].MEMBER_NO = dsMemb.DATA[0].MEMBER_NO;
                }
                else
                {
                    decimal intAmt = wcf.NShrlon.of_computeinterest(state.SsWsPass, state.SsCoopControl, dsNPL.DATA[0].LOANCONTRACT_NO, state.SsWorkDate);
                    dsNPL.DATA[0].compute_2 = intAmt;
                    dsMemb.Retrieve(dsNPL.DATA[0].MEMBER_NO);
                }

                //ดึงข้อมูล ds ที่เหลือ
                dsFollowMast.Retrieve(loanNo);
                dsFollow1.Retrieve(dsNPL.DATA[0].MEMBER_NO, dsNPL.DATA[0].FOLLOW_SEQ, "N");
                dsFollow2.Retrieve(dsNPL.DATA[0].MEMBER_NO, dsNPL.DATA[0].FOLLOW_SEQ, "Y");
                dsBoard.Retrieve(loanNo);
                dsResolution.Retrieve(loanNo);
                dsMort.Retrieve(loanNo);
                dsColl.Retrieve(loanNo);

                //ดึง dropdown ต่างๆ
                dsNPL.DdLAWTYPE_CODE();
                dsNPL.DdLAWTYPE_CODE_OLD();
                dsNPL.DdWork_order();
                dsFollowMast.DdFollowSeq(dsNPL.DATA[0].MEMBER_NO);
                decimal advanceAmt = 0;
                if (dsNPL.DATA[0].FOLLOW_SEQ > 0)
                {
                    try
                    {
                        string sql = "select advance_amt from lnnplfollowmaster where coop_id={0} and member_no={1} and follow_seq={2}";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsNPL.DATA[0].MEMBER_NO, dsNPL.DATA[0].FOLLOW_SEQ);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            advanceAmt = dt.GetDecimal("advance_amt");
                        }
                    }
                    catch { }
                }
                dsNPL.DATA[0].MARGIN = advanceAmt;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}