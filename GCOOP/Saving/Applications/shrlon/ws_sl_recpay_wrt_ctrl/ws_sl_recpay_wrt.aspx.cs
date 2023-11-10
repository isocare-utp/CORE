using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_recpay_wrt_ctrl
{
    public partial class ws_sl_recpay_wrt : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public string PostRetrieve { get; set; }
        [JsPostBack]
        public string PostMoneyCode { get; set; }

        string payout_no = "";

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdMoneyType();
                dsMain.DATA[0].CASH_TYPE = "CSH";
                dsMain.DATA[0].ITEMPAYTYPE_CODE = "RWT";
                dsMain.DdFromAccId("RWT", "CSH");
                dsMain.DATA[0].TOFROM_ACCID = "11010000";
                dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                dsMain.DATA[0].ENTRY_DATE = state.SsWorkDate;
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DATA[0].ITEMPAY_AMT = 0;
                dsMain.DATA[0].SLIP_NO = "AUTO";
                dsMain.DATA[0].PRINT_STATUS = 1;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostRetrieve)
            {
                try
                {
                    string member_no = "";
                    member_no = dsMain.DATA[0].MEMBER_NO;
                    member_no = WebUtil.MemberNoFormat(member_no);
                    //dsMain.RetrieveMemb(member_no);
                    string sql = @"select mb.member_no,mp.prename_desc||mb.memb_name||' '||mb.memb_surname as fullname ,mg.membgroup_code||'-'||mg.membgroup_desc as membgroup
from mbmembmaster mb  ,mbucfprename mp,mbucfmembgroup mg
where mb.member_no = {0}
and mb.coop_id = {1}  
and mb.prename_code = mp.prename_code
and mb.membgroup_code = mg.membgroup_code
and mb.coop_id = mg.coop_id";
                    sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopId);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        dsMain.DATA[0].MEMBER_NO = member_no;
                        dsMain.DATA[0].FULLNAME = dt.GetString("fullname");
                        dsMain.DATA[0].MEMBGROUP = dt.GetString("membgroup");


                        string sqlSel = @"select * from mbmoneyreturn where return_status=0 and member_no={1} and coop_id={0}";
                        sqlSel = WebUtil.SQLFormat(sqlSel, state.SsCoopId, member_no);
                        Sdt dtsel = WebUtil.QuerySdt(sqlSel);
                        if (!dtsel.Next())
                        {
                            SetMoneyReturn(member_no);
                        }
                        dsList.Retrieve(member_no);
                    }
                    else
                    {

                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิก");
                    }

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
            else if (eventArg == PostMoneyCode)
            {
                try
                {
                    string sliptype_code = "";
                    string moneytype_code = "";
                    sliptype_code = dsMain.DATA[0].ITEMPAYTYPE_CODE;
                    moneytype_code = dsMain.DATA[0].CASH_TYPE;

                    dsMain.DdFromAccId(sliptype_code, moneytype_code);
                }
                catch { }
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exed = new ExecuteDataSource(this);
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].OPERATE_FLAG == 0)
                    {
                        dsList.ChangeRowStatusNone(i);
                    }
                    else
                    {
                        dsList.ChangeRowStatusInsert(i);
                        dsList.DATA[i].TOFROM_ACCID = dsMain.DATA[0].TOFROM_ACCID;
                        dsList.DATA[i].PAY_TOWHOM = dsMain.DATA[0].FULLNAME;
                        dsList.DATA[i].CASH_TYPE = dsMain.DATA[0].CASH_TYPE;
                        dsList.DATA[i].ENTRY_DATE = state.SsWorkDate;
                        dsList.DATA[i].ENTRY_ID = state.SsUsername;
                        dsList.DATA[i].NONMEMBER_DETAIL = dsMain.DATA[0].FULLNAME;
                        dsList.DATA[i].FROM_SYSTEM = "LON";
                        dsList.DATA[i].SLIP_NO = Get_NumberDOC("FNRECEIVENO");
                        dsList.DATA[i].OPERATE_DATE = dsMain.DATA[0].OPERATE_DATE;
                        dsList.DATA[i].MACHINE_ID = state.SsClientIp;
                    }
                }
                //InsSlslippayout(dsList);
                //exed.AddRepeater(dsList);
                exed.SQL.Add(InsSlslippayout(dsList));

                string sqlUpdateRet = "";
                string sqlUpdateWrt = "";
                string sqlInsert = "";
                string sqlSeqno = "";
                string sqlSelWrt = "";
                string sqlUpdateMemb = "";
                decimal old_wrtbal = 0;
                decimal new_wrtbal = 0;
                decimal itempay_amt = 0;
                decimal seqno = 0;
                string memb_no = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].OPERATE_FLAG == 1 && dsList.DATA[i].ITEMPAYTYPE_CODE == "WRT")
                    {
                        memb_no = dsList.DATA[i].MEMBER_NO;
                        sqlUpdateRet = "update mbmoneyreturn set return_status =1  where coop_id={0} and member_no={1} and seq_no={2} ";
                        sqlUpdateRet = WebUtil.SQLFormat(sqlUpdateRet, state.SsCoopId, memb_no, dsList.DATA[i].SEQ_NO);

                        //sqlUpdateWrt = "update wrtfundstatement set return_status=1 where coop_id={0} and member_no={1} and ref_contno={2} and wrtitemtype_code='PWT'";
                        //sqlUpdateWrt = WebUtil.SQLFormat(sqlUpdateWrt, state.SsCoopId, memb_no, dsList.DATA[i].LOANCONTRACT_NO);


                        sqlSeqno = "select nvl(max(seq_no),0) as max_seqno from wrtfundstatement where member_no = {0} and coop_id={1} ";
                        sqlSeqno = WebUtil.SQLFormat(sqlSeqno, memb_no, state.SsCoopId);
                        Sdt dt = WebUtil.QuerySdt(sqlSeqno);
                        if (dt.Next())
                        {
                            seqno = dt.GetDecimal("max_seqno") + 1;
                        }
                        else
                        {
                            seqno = 1;
                        }
                        sqlSelWrt = "select * from wrtfundstatement where member_no={0} and coop_id={1} and ref_contno={2} and wrtitemtype_code='PWT'";
                        sqlSelWrt = WebUtil.SQLFormat(sqlSelWrt, memb_no, state.SsCoopId, dsList.DATA[i].LOANCONTRACT_NO);
                        Sdt dtWrt = WebUtil.QuerySdt(sqlSelWrt);
                        if (dtWrt.Next())
                        {
                            old_wrtbal = dtWrt.GetDecimal("wrtfund_balance");

                        }
                        itempay_amt = dsList.DATA[i].ITEMPAY_AMT;
                        new_wrtbal = old_wrtbal - itempay_amt;

                        sqlInsert = @"INSERT INTO wrtfundstatement (
                        coop_id,            member_no,      seq_no,             operate_date,
                        wrtitemtype_code,   wrtfund_amt,    wrtfund_balance,    ref_contno,
                        moneytype_code,     item_status,    entry_id,           entry_date,
                        ref_docno) 
                        values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                        sqlInsert = WebUtil.SQLFormat(sqlInsert,
                        state.SsCoopId, memb_no, seqno, dsMain.DATA[0].OPERATE_DATE,
                        "RWT", itempay_amt, new_wrtbal, dsList.DATA[i].LOANCONTRACT_NO,
                        dsList.DATA[i].CASH_TYPE, 1, state.SsUsername, state.SsWorkDate,
                        "");


                        sqlUpdateMemb = "update mbmembmaster set wrtfund_balance ={2}  where coop_id={0} and member_no={1}";
                        sqlUpdateMemb = WebUtil.SQLFormat(sqlUpdateMemb, state.SsCoopId, memb_no, new_wrtbal);

                        exed.SQL.Add(sqlUpdateRet);
                        //exed.SQL.Add(sqlUpdateWrt);
                        exed.SQL.Add(sqlInsert);
                        exed.SQL.Add(sqlUpdateMemb);
                    }
                }

                exed.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                try
                {
                    decimal print = 0;
                    print = dsMain.DATA[0].PRINT_STATUS;
                    if (print == 1)
                    {
                        PrintSlipWrt(payout_no);
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("พิมพ์ใบสำคัญจ่ายกสส ไม่สำเร็จ " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
        }

        public string Get_NumberDOC(string typecode)
        {
            string coop_id = state.SsCoopControl;
            Sta ta = new Sta(state.SsConnectionString);
            string postNumber = "";
            try
            {
                ta.AddInParameter("AVC_COOPID", coop_id, System.Data.OracleClient.OracleType.VarChar);
                ta.AddInParameter("AVC_DOCCODE", typecode, System.Data.OracleClient.OracleType.VarChar);
                ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.VarChar);
                ta.ExePlSql("N_PK_DOCCONTROL.OF_GETNEWDOCNO");
                postNumber = ta.OutParameter("return_value").ToString();
                ta.Close();
            }
            catch
            {
                ta.Close();
            }
            return postNumber.ToString();
        }

        public string InsSlslippayout(DsList ds)
        {
            string sql = "";
            try
            {
                string coop_id = state.SsCoopId;
                string payoutslip_no = Get_NumberDOC("SLSLIPPAYOUT");
                payout_no = payoutslip_no;
                string memb_no = ds.DATA[0].MEMBER_NO;
                string sliptype_code = ds.DATA[0].ITEMPAYTYPE_CODE;
                DateTime slip_date = ds.DATA[0].ENTRY_DATE;
                DateTime operate_date = ds.DATA[0].OPERATE_DATE;
                string loancontract_no = ds.DATA[0].LOANCONTRACT_NO;
                decimal payout_amt = ds.DATA[0].ITEMPAY_AMT;
                decimal payoutnet_amt = ds.DATA[0].ITEMPAY_AMT;
                string moneytype_code = ds.DATA[0].CASH_TYPE;
                //string expense_bank = ds.DATA[0].BANK_CODE;
                //string expense_branch = ds.DATA[0].BANK_BRANCH;
                //string expense_accid = ds.DATA[0].ACCOUNT_NO;
                string tofromacc_id = ds.DATA[0].TOFROM_ACCID;
                string entry_id = state.SsUsername;
                DateTime entry_date = state.SsWorkDate;
                decimal returnetc_amt = 0;

                if (sliptype_code == "WRT")
                {
                    returnetc_amt = payout_amt;
                }

                sql = @"INSERT INTO SLSLIPPAYOUT(
COOP_ID,            PAYOUTSLIP_NO,          MEMCOOP_ID,         MEMBER_NO,
DOCUMENT_NO,        SLIPTYPE_CODE,          SLIP_DATE,          OPERATE_DATE,
SHRLONTYPE_CODE,    RCVFROMREQCONT_CODE ,   LOANCONTRACT_NO,    LOANREQUEST_DOCNO,
RCVPERIOD_FLAG,     RCV_PERIOD,             PAYOUT_AMT,         PAYOUTCLR_AMT, 
PAYOUTNET_AMT,      BFPERIOD,               BFLOANAPPROVE_AMT,  BFSHRCONT_BALAMT, 
BFWITHDRAW_AMT,     BFINTEREST_ARREAR ,     BFCONTLAW_STATUS,   PRNCALINT_AMT, 
MONEYTYPE_CODE,     EXPENSE_BANK,           EXPENSE_BRANCH,     EXPENSE_ACCID,
ACCID_FLAG,         TOFROM_ACCID,           SLIP_STATUS,        SLIPCLEAR_NO,
MEMBGROUP_CODE,     ENTRY_ID,               ENTRY_DATE,         ENTRY_BYCOOPID,
POSTTOVC_FLAG,      POST_TOFIN,             RETURNETC_AMT) 
VALUES (
{0},{1},{2},{3},
{4},{5},{6},{7},
{8},{9},{10},{11},
{12},{13},{14},{15},
{16},{17},{18},{19},
{20},{21},{22},{23},
{24},{25},{26},{27},
{28},{29},{30},{31},
{32},{33},{34},{35},
{36},{37},{38})";
                sql = WebUtil.SQLFormat(sql,
                    coop_id, payoutslip_no, coop_id, memb_no,
                    "", sliptype_code, slip_date, operate_date,
                    "", "", loancontract_no, "",
                    0, 0, payout_amt, 0,
                    payoutnet_amt, 0, 0, 0,
                    0, 0, 1, 0,
                    moneytype_code, "", "", "",
                    0, tofromacc_id, 1, "",
                    "", entry_id, entry_date, coop_id,
                    0, 0, returnetc_amt);

            }
            catch (Exception ex)
            {
                sql = "";
            }
            return sql;
        }

        public void PrintSlipWrt(string payout_no)
        {
            string sql = "";
            sql = @"select
mp.prename_desc,
mb.memb_name,
mb.memb_surname,
so.loancontract_no,
so.shrlontype_code,
so.payoutslip_no,
so.member_no,
so.payout_amt,
so.returnetc_amt ,
so.payoutnet_amt,
so.moneytype_code,
so.expense_bank,
cb.bank_desc,
so.expense_accid,
so.slip_date,
decode(so.sliptype_code,
'WRT','จ่ายคืนกองทุน กสส. สัญญา '||nvl(so.loancontract_no,''),'') as slip_desc

from slslippayout so,mbmembmaster mb ,mbucfprename mp,cmucfbank cb
where so.payoutslip_no = {0}
and so.coop_id ={1}
and so.member_no = mb.member_no
and mb.prename_code = mp.prename_code
and so.expense_bank = cb.bank_code(+)
group by mp.prename_desc,
mb.memb_name,
mb.memb_surname,
so.loancontract_no,
so.shrlontype_code,
so.payoutslip_no,
so.member_no,
so.payout_amt,
so.returnetc_amt ,
so.payoutnet_amt,
so.moneytype_code,
so.expense_bank,
cb.bank_desc,
so.expense_accid,
so.slip_date,
decode(so.sliptype_code,'WRT','จ่ายคืนกองทุน กสส. สัญญา '||nvl(so.loancontract_no,''),'') ";
            sql = WebUtil.SQLFormat(sql, payout_no, state.SsCoopId);
            DataTable data = WebUtil.Query(sql);
            Printing.PrintAppletPB(this, "sl_slip_payout_wrt", data);
        }

        public void SetMoneyReturn(string member_no)
        {
            decimal seq = 0;
            string sqlSeq = @"select nvl(max(seq_no),0) as seq_no from mbmoneyreturn where member_no={0} and coop_id={1}";
            sqlSeq = WebUtil.SQLFormat(sqlSeq, member_no, state.SsCoopId);
            Sdt dtSeq = WebUtil.QuerySdt(sqlSeq);
            if (dtSeq.Next())
            {
                seq = dtSeq.GetDecimal("seq_no");
                seq++;
            }

            string sqlSelect = @"select wfs.coop_id,wfs.member_no,{4},'LON',null,'','','จ่ายคืนเงินกองทุนกสส '||wfs.ref_contno as description  ,
                                '',wfs.coop_id ,lm.loancontract_no,'','WRT' as returnitemtype_code,
                                wfs.wrtfund_balance  as return_amount,'',0,{2} as entry_id ,{3} as entry_date,'' as return_id ,null as return_date,'' as cancel_id,null as cancel_date,'' as cancel_code 
                                from mbmembmaster m , lncontmaster lm , wrtfundstatement wfs , (
	                                select wf.coop_id , wf.member_no , max( seq_no ) as seq_no
	                                from wrtfundstatement wf
	                                where wf.coop_id = {0}
	                                group by wf.coop_id , wf.member_no
	                                ) wrfs
                                where wfs.coop_id = wrfs.coop_id
                                and wfs.member_no = wrfs.member_no
                                and wfs.seq_no = wrfs.seq_no
                                and wfs.coop_id = lm.coop_id
                                and trim( wfs.ref_contno ) = trim( lm.loancontract_no )
                                and m.coop_id = lm.memcoop_id
                                and m.member_no = lm.member_no
                                and m.wrtfund_balance = wfs.wrtfund_balance 
                                and wfs.wrtitemtype_code = 'PWT' 
                                and m.wrtfund_balance > 0
                                and lm.principal_balance = 0
                                and m.member_no like {1}
                                and wfs.coop_id = {0} ";

            string sqlInsert = @"insert into mbmoneyreturn (COOP_ID, MEMBER_NO, SEQ_NO, SYSTEM_FROM, REF_SLIPDATE, REF_SLIPNO, REF_DOCNO,
                                 DESCRIPTION, SHRLONTYPE_CODE, BIZZCOOP_ID, LOANCONTRACT_NO, DEPTACCOUNT_NO, RETURNITEMTYPE_CODE, 
                                RETURN_AMOUNT, RETURN_SLIPPAYOUTNO, RETURN_STATUS, ENTRY_ID, ENTRY_DATE, RETURN_ID, RETURN_DATE, CANCEL_ID, CANCEL_DATE, CANCEL_CODE ) 

                                (select wfs.coop_id,wfs.member_no,{4},'LON',null,'','','จ่ายคืนเงินกองทุนกสส '||wfs.ref_contno as description  ,
                                '',wfs.coop_id ,lm.loancontract_no,'','WRT' as returnitemtype_code,
                                wfs.wrtfund_balance  as return_amount,'',0,{2} as entry_id ,{3} as entry_date,'' as return_id ,null as return_date,'' as cancel_id,null as cancel_date,'' as cancel_code 
                                from mbmembmaster m , lncontmaster lm , wrtfundstatement wfs , (
	                                select wf.coop_id , wf.member_no , max( seq_no ) as seq_no
	                                from wrtfundstatement wf
	                                where wf.coop_id = {0}
	                                group by wf.coop_id , wf.member_no
	                                ) wrfs
                                where wfs.coop_id = wrfs.coop_id
                                and wfs.member_no = wrfs.member_no
                                and wfs.seq_no = wrfs.seq_no
                                and wfs.coop_id = lm.coop_id
                                and trim( wfs.ref_contno ) = trim( lm.loancontract_no )
                                and m.coop_id = lm.memcoop_id
                                and m.member_no = lm.member_no
                                and m.wrtfund_balance = wfs.wrtfund_balance 
                                and wfs.wrtitemtype_code = 'PWT' 
                                and m.wrtfund_balance > 0
                                and lm.principal_balance = 0
                                and m.member_no like {1}
                                and wfs.coop_id = {0} )";

            sqlSelect = WebUtil.SQLFormat(sqlSelect, state.SsCoopId, member_no, state.SsUsername, state.SsWorkDate, seq);
            Sdt dtSel = WebUtil.QuerySdt(sqlSelect);
            if (dtSel.Next())
            {
                sqlInsert = WebUtil.SQLFormat(sqlInsert, state.SsCoopId, member_no, state.SsUsername, state.SsWorkDate, seq);
                int res = WebUtil.ExeSQL(sqlInsert);
                if (res == 1)
                {
                    //save conplete
                }
                else
                {
                    //save error
                }

            }
            else
            {

                //no data
            }
        }
    }
}