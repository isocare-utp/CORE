using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNDeposit;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_fundcoll_payment_ctrl
{
    public partial class ws_sl_fundcoll_payment : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string JsPostMember { get; set; }
        [JsPostBack]
        public string JsPostCalint { get; set; }
        [JsPostBack]
        public string JsPostTofromaccid { get; set; }

        Sdt ta = new Sdt();
        Sdt ta2 = new Sdt();

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                SetDefault();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostMember)
            {
                PostMember();
            }
            else if (eventArg == JsPostCalint)
            {
                PostCalint();
            }
            else if (eventArg == JsPostTofromaccid)
            {
                SetDefaultTofromaccid();
            }
        }

        public void SaveWebSheet()
        {
            String payoutslip_no = "",fundmember_no = "", member_no = "", membgroup_code = "", sliptype_code = "";
            String moneytype_code = "", expense_bank = "", expense_branch = "", expense_accid = "", tofrom_accid = "";
            DateTime slip_date, operate_date;
            decimal payout_amtnet = 0, fundbalance = 0, int_amt = 0, int_rate = 0;
            decimal seq_no = 0;
            try
            {
                sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE ;
                member_no = dsMain.DATA[0].MEMBER_NO;
                membgroup_code = dsMain.DATA[0].MEMBGROUP_CODE.Trim();
                fundmember_no = dsMain.DATA[0].FUNDMEMBER_NO;
                slip_date = dsMain.DATA[0].SLIP_DATE;
                operate_date = dsMain.DATA[0].OPERATE_DATE;
                payout_amtnet = dsMain.DATA[0].PAYOUTNET_AMT;
                moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                expense_bank = dsMain.DATA[0].EXPENSE_BANK;
                expense_branch = dsMain.DATA[0].EXPENSE_BRANCH;
                expense_accid = dsMain.DATA[0].EXPENSE_ACCID;
                tofrom_accid = dsMain.DATA[0].TOFROM_ACCID;
                int_rate = dsList.DATA[0].INT_RATE;
                int_amt = dsList.DATA[0].INT_AMT;
                fundbalance = dsMain.DATA[0].FUNDBALANCE;

                payoutslip_no = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopControl, "SLSLIPPAYOUT");

                //insert slslippayout
                String sqlInsertpay = @"INSERT INTO SLSLIPPAYOUT 
                        (COOP_ID, PAYOUTSLIP_NO, MEMCOOP_ID, MEMBER_NO, DOCUMENT_NO, 
                        SLIPTYPE_CODE, SLIP_DATE, OPERATE_DATE, RCVFROMREQCONT_CODE, 
                        PAYOUT_AMT, PAYOUTNET_AMT, MONEYTYPE_CODE, EXPENSE_BANK, 
                        EXPENSE_BRANCH, EXPENSE_ACCID, TOFROM_ACCID, SLIP_STATUS, 
                        MEMBGROUP_CODE, ENTRY_ID, ENTRY_DATE, ENTRY_BYCOOPID, 
                        POSTTOVC_FLAG, POST_TOFIN , REF_DOCNO) VALUES 
                        ({0},{1},{2},{3},{4},
                        {5},{6},{7},{8},
                        {9},{10},{11},{12},
                        {13},{14},{15},{16},
                        {17},{18},{19},{20},
                        {21},{22},{23})";
                sqlInsertpay = WebUtil.SQLFormat(sqlInsertpay, state.SsCoopControl, payoutslip_no, state.SsCoopControl, member_no, fundmember_no,
                        sliptype_code, slip_date, operate_date, "FUD",  
                        payout_amtnet, payout_amtnet, moneytype_code, expense_bank,
                        expense_branch, expense_accid, tofrom_accid, 1,
                        membgroup_code, state.SsUsername, DateTime.Now, state.SsCoopId,
                        0, 1, fundmember_no);
                Sdt dt = WebUtil.QuerySdt(sqlInsertpay);

                String sqlInsertpaydet = "";
                //insert slslippayoutdet ถอนต้น
                sqlInsertpaydet = @"INSERT INTO SLSLIPPAYOUTDET 
                        (COOP_ID, PAYOUTSLIP_NO, SLIPITEMTYPE_CODE, SEQ_NO, CONCOOP_ID, 
                        SLIPITEM_DESC, ITEM_PAYAMT, BFSHRCONT_BALAMT, BFCONTLAW_STATUS ) VALUES 
                        ({0},{1},{2},{3},{4},
                        {5},{6},{7},{8})";
                sqlInsertpaydet = WebUtil.SQLFormat(sqlInsertpaydet, state.SsCoopControl, payoutslip_no, sliptype_code, 1, state.SsCoopControl,
                        "ถอนเงินต้น", fundbalance, 0, 0);
                Sdt dt5 = WebUtil.QuerySdt(sqlInsertpaydet);
                //insert slslippayoutdet ถอนดอก
                sqlInsertpaydet = @"INSERT INTO SLSLIPPAYOUTDET 
                        (COOP_ID, PAYOUTSLIP_NO, SLIPITEMTYPE_CODE, SEQ_NO, CONCOOP_ID, 
                        SLIPITEM_DESC, ITEM_PAYAMT, BFSHRCONT_BALAMT, BFCONTLAW_STATUS ) VALUES 
                        ({0},{1},{2},{3},{4},
                        {5},{6},{7},{8})";
                sqlInsertpaydet = WebUtil.SQLFormat(sqlInsertpaydet, state.SsCoopControl, payoutslip_no, sliptype_code, 2, state.SsCoopControl,
                        "ถอนดอกเบี้ย", int_amt, 0, 0);
                Sdt dt6 = WebUtil.QuerySdt(sqlInsertpaydet);

                //insert fundcollstatement
                //รอบแรกยิงรายการ fundcollstatement ดอกเบี้ยที่คำนวณได้ ณ ตอนจ่าย
                fundbalance = fundbalance + int_amt;
                seq_no = GetSeqFundstate(state.SsCoopControl, fundmember_no);
                String sqlInsertfundstateInt = @"INSERT INTO FUNDCOLLSTATEMENT (FUNDMEMBER_NO, COOP_ID, SEQ_NO, ITEMTYPE_CODE, OPERATE_DATE, 
                        REF_DOCNO, ITEMPAY_AMT, FUNDBALANCE, ENTRY_ID, ENTRY_DATE, INT_RATE, INT_AMT, 
                        INT_ACCUM, PRNTOPB_STATUS, PAGE_PB, PAGE_CARD) VALUES 
                        ({0},{1},{2},{3},{4},
                        {5},{6},{7},{8},{9},{10},{11},
                        {12},{13},{14},{15})";
                sqlInsertfundstateInt = WebUtil.SQLFormat(sqlInsertfundstateInt, fundmember_no, state.SsCoopControl, seq_no, "INT", operate_date,
                        payoutslip_no, int_amt, fundbalance, state.SsUsername, DateTime.Now, int_rate, int_amt,
                        0, 0, dsMain.DATA[0].LASTPAGE_NO_PB, 0);
                Sdt dt2 = WebUtil.QuerySdt(sqlInsertfundstateInt);

                //รอบสองยิงรายการ fundcollstatement ยอดรายการจ่าย
                fundbalance = fundbalance - payout_amtnet;
                seq_no++;
                String sqlInsertfundstate = @"INSERT INTO FUNDCOLLSTATEMENT (FUNDMEMBER_NO, COOP_ID, SEQ_NO, ITEMTYPE_CODE, OPERATE_DATE, 
                        REF_DOCNO, ITEMPAY_AMT, FUNDBALANCE, ENTRY_ID, ENTRY_DATE, INT_RATE, INT_AMT, 
                        INT_ACCUM, PRNTOPB_STATUS, PAGE_PB, PAGE_CARD) VALUES 
                        ({0},{1},{2},{3},{4},
                        {5},{6},{7},{8},{9},{10},{11},
                        {12},{13},{14},{15})";
                sqlInsertfundstate = WebUtil.SQLFormat(sqlInsertfundstate, fundmember_no, state.SsCoopControl, seq_no, sliptype_code, operate_date,
                        payoutslip_no, payout_amtnet, fundbalance, state.SsUsername, DateTime.Now, int_rate, 0,
                        0 , 0, dsMain.DATA[0].LASTPAGE_NO_PB, 0);
                Sdt dt3 = WebUtil.QuerySdt(sqlInsertfundstate);

                //update fundcollmaster
                String sqlUpfundmast = @"update fundcollmaster set fundbalance = {2}, lastcalint_date = {3}, last_stm_no = {4}, fund_status = {5}, lastaccess_date = {6}
                        where coop_id = {0} and fundmember_no = {1}";
                sqlUpfundmast = WebUtil.SQLFormat(sqlUpfundmast, state.SsCoopControl, fundmember_no, fundbalance, slip_date, seq_no, -1, operate_date);
                Sdt dt4 = WebUtil.QuerySdt(sqlUpfundmast);

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการจ่ายเงินกองทุนเลขที่ " + fundmember_no + " สำเร็จ");
                PostPrintSlip(payoutslip_no);
                SetDefault();
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }


        }

        public void WebSheetLoadEnd()
        {

        }

        #region AllFunction
        private void PostMember()
        {
            string memberNo = "";
            int check_flag = 0;
            decimal itempay_amt = 0, intpay_amt = 0;
            DateTime calint_date = new DateTime();
            memberNo = dsMain.DATA[0].MEMBER_NO;
            memberNo = WebUtil.MemberNoFormat(memberNo);
            dsMain.DATA[0].MEMBER_NO = memberNo.Trim();
            calint_date = dsMain.DATA[0].SLIP_DATE;
            try
            {
                dsMain.Retrieve(memberNo);
                SetDefault();
                //check_flag = OfcheckState(state.SsCoopControl, memberNo);
                check_flag = 1; //ไม่เช็คเงื่อนไขใดๆๆ
                if (check_flag == 1)
                {
                    dsList.Retrieve(memberNo);
                    try
                    {
                        intpay_amt = wcf.NDeposit.of_calint_foudcoll(state.SsWsPass, memberNo, calint_date);
                    }
                    catch
                    {
                        intpay_amt = 0;
                        //throw new Exception("ไม่สามารถคำนวณดอกเบี้ยได้"); 
                    }
                    itempay_amt = dsList.DATA[0].FUNDBALANCE + intpay_amt;
                    dsList.DATA[0].INT_AMT = intpay_amt;
                    dsList.DATA[0].ITEMPAY_AMT = itempay_amt;
                    dsMain.DATA[0].PAYOUTNET_AMT = itempay_amt;
                }
                else
                {
                    LtServerMessage.Text = WebUtil.WarningMessage2("ยังไม่สามารถจ่ายกองทุนได้ กรุณาตรวจสอบเงื่อนไขสมาชิกเลขที่ " + memberNo);
                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void PostCalint()
        {
            string memberNo = "";
            int check_flag = 0;
            decimal itempay_amt = 0, intpay_amt = 0;
            DateTime calint_date = new DateTime();
            memberNo = dsMain.DATA[0].MEMBER_NO;
            calint_date = dsMain.DATA[0].SLIP_DATE;
            check_flag = OfcheckState(state.SsCoopControl, memberNo);
            if (check_flag == 1)
            {
                dsList.Retrieve(memberNo);
                try
                {
                    intpay_amt = wcf.NDeposit.of_calint_foudcoll(state.SsWsPass, memberNo, calint_date);
                }
                catch { intpay_amt = 0; }
                itempay_amt = dsList.DATA[0].FUNDBALANCE + intpay_amt;
                dsList.DATA[0].INT_AMT = intpay_amt;
                dsList.DATA[0].ITEMPAY_AMT = itempay_amt;
                dsMain.DATA[0].PAYOUTNET_AMT = itempay_amt;
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage2("ยังไม่สามารถจ่ายกองทุนได้ กรุณาตรวจสอบเงื่อนไขสมาชิกเลขที่ " + memberNo);
            }
        }

        public void SetDefault()
        {
            dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
            dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
            dsMain.DATA[0].DOCUMENT_NO = "AUTO";
            dsMain.DATA[0].SLIPTYPE_CODE = "FRT";
            dsMain.DATA[0].MONEYTYPE_CODE = "CSH";
            dsMain.DdMoneytype();
            dsMain.DdTofromaccid(dsMain.DATA[0].MONEYTYPE_CODE);
        }

        private decimal GetSeqFundstate(string coop_id, string fundmembno)
        {
            decimal maxseq_no = 0;
            String sqlgetmaxseq = @"select last_stm_no from fundcollmaster where coop_id = {0} and fundmember_no = {1}";
            sqlgetmaxseq = WebUtil.SQLFormat(sqlgetmaxseq, coop_id, fundmembno);
            ta = WebUtil.QuerySdt(sqlgetmaxseq);
            if (ta.Next())
            {
                maxseq_no = ta.GetDecimal("last_stm_no");
            }
            maxseq_no++;
            return maxseq_no;
        }

        private void SetDefaultTofromaccid()
        {
            string moneytypecode = dsMain.DATA[0].MONEYTYPE_CODE;
            dsMain.DdTofromaccid(moneytypecode);
        }

        private void PostPrintSlip(string PayoutSlipno)
        {
            string sliptype_code = "", ireport_obj = "", ireportpayout_obj = "";
            string[] objprint = WebUtil.GetIreportObjPrintLoan();
            sliptype_code = objprint[0];
            ireport_obj = objprint[1];
            ireportpayout_obj = objprint[2];
            if (sliptype_code == "IR")
            {
                Printing.PrintIRSlippayOutPBN(this, state.SsCoopControl, PayoutSlipno, ireportpayout_obj);
            }
            else
            {

            }
        }

        /// <summary>
        /// เงื่อนไขการจ่ายกองทุน 
        /// 1. กรณีเป็นสมาชิกปกติ
        ///     1.1 ต้องไม่ติดหนี้สหกรณ์
        ///     1.2 ต้องไม่ติดค้ำประกันสัญญาใดๆ
        /// 2. กรณีสมาชิกเสียชีวิต
        ///     2.1 ต้องไม่ติดหนี้สหกร์
        ///     2.2 ติดค้ำประกัน หรือ ไม่ติดค้ำประกันก็ได้
        /// สถานะ 1 = จ่ายกองทุนได้ , -9 = ยังจ่ายกองทุนไม่ได้
        /// </summary>
        /// <param name="coopId"></param>
        /// <param name="memberNo"></param>
        /// <returns></returns>
        public int OfcheckState(string coopId, string memberNo)
        {
            int checkstatus = 0;
            string fundmembno = "", loancontractno = "", refloancontractno = "";
            decimal resignstatus = 8, deadstatus = 8;
            try
            {
                //เช็ครายการหนี้ที่ติดกับสหกรณ์
                String sql = @"select fcs.fundmember_no as fundmember_no, lm.loancontract_no as loancontract_no, 
                    mb.resign_status as resign_status, mb.dead_status as dead_status from fundcollstatement fcs, mbmembmaster mb , lncontmaster lm
                    where fcs.coop_id = mb.coop_id
                    and fcs.fundmember_no = mb.member_no
                    and trim(fcs.loancontract_no) = trim(lm.loancontract_no)
                    and lm.contract_status = 1
                    and fcs.coop_id = {0}
                    and fcs.fundmember_no = {1}
                    union
                    select fcs.fundmember_no as fundmember_no, '' as loancontract_no, 
                    mb.resign_status as resign_status, mb.dead_status as dead_status from fundcollstatement fcs, mbmembmaster mb 
                    where fcs.coop_id = mb.coop_id
                    and fcs.fundmember_no = mb.member_no
                    and fcs.coop_id = {0}
                    and fcs.fundmember_no = {1} ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memberNo);
                ta = WebUtil.QuerySdt(sql);
                if (ta.Next())
                {
                    fundmembno = ta.GetString("fundmember_no");
                    loancontractno = ta.GetString("loancontract_no");
                    resignstatus = ta.GetDecimal("resign_status");
                    deadstatus = ta.GetDecimal("dead_status");
                    if (loancontractno == null || loancontractno == "")
                    {
                        checkstatus = 1;
                    }
                    else
                    {
                        checkstatus = -9;
                    }
                }
                if(loancontractno == null || loancontractno == "")
                {
                    //กรณีหนี้หมด ให้เช็คภาระค้ำต่อ
                    String sqlcoll = @"select lm.loancontract_no as loancontract_no from lncontmaster lm,  lncontcoll lc
                        where lm.coop_id = lc.coop_id
                        and lm.loancontract_no = lc.loancontract_no
                        and lm.contract_status = 1
                        and lc.coll_status = 1
                        and lc.loancolltype_code = '01'
                        and lc.coop_id = {0}
                        and lc.ref_collno = {1}";
                    sqlcoll = WebUtil.SQLFormat(sqlcoll, state.SsCoopControl, memberNo);
                    ta2 = WebUtil.QuerySdt(sqlcoll);
                    if (ta2.Next())
                    {
                        refloancontractno = ta2.GetString("loancontract_no");
                        checkstatus = -9;
                    }
                    else
                    {
                        checkstatus = 1;
                    }
                    //กรณีเสียชีวิต
                    if (deadstatus == 1 && (refloancontractno != null || refloancontractno != ""))
                    {
                        checkstatus = 1;
                    }
                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด " + ex.Message); }
            return checkstatus;
        }
        #endregion
    }
}