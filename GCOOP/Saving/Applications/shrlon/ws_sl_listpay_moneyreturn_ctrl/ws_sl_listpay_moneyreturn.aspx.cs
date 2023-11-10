using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_listpay_moneyreturn_ctrl
{
    public partial class ws_sl_listpay_moneyreturn : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostSalaryId { get; set; }
        [JsPostBack]
        public string PostMoneytype { get; set; }
        [JsPostBack]
        public string PostBtnRetieve { get; set; }

        string old_memno = "";
        string new_memno = "";
        decimal tmpseq_no = 0;

        public void InitJsPostBack()
        {
            dsList.InitDsList(this);
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.DdBank();
                dsMain.DdMoneyType();
                dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                dsMain.DATA[0].RETURN_TYPE = "ALL";
                dsMain.DdBank();
                dsMain.DdCashType();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string member_no = dsMain.DATA[0].MEMBER_NO;
                member_no = WebUtil.MemberNoFormat(member_no);
                string bank_code = dsMain.DATA[0].BANK_CODE;
                string cash_type = dsMain.DATA[0].CASH_TYPE;
                dsMain.DATA[0].MEMBER_NO = member_no;
                dsList.RetrieveListAll(member_no, bank_code, cash_type);
            }
            else if (eventArg == PostSalaryId)
            {
                string salary_id = dsMain.DATA[0].SALARY_ID;
                string member_no = WebUtil.GetMembnoBySalaryid(salary_id, state.SsCoopControl);
                string bank_code = dsMain.DATA[0].BANK_CODE;
                string cash_type = dsMain.DATA[0].CASH_TYPE;
                dsMain.DATA[0].MEMBER_NO = member_no;
                dsList.RetrieveListAll(member_no, bank_code, cash_type);
            }
            else if (eventArg == PostMoneytype)
            {
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                dsMain.DdFromAccId("PX", moneytype_code);
                SetDefaultTofromaccid();
            }
            else if (eventArg == PostBtnRetieve)
            {
                string return_type = dsMain.DATA[0].RETURN_TYPE;
                string bank_code = dsMain.DATA[0].BANK_CODE;
                string cash_type = dsMain.DATA[0].CASH_TYPE;
                if (return_type == "RET")
                {
                    dsList.RetrieveListRet("", bank_code, cash_type);
                }
                else if (return_type == "WRT")
                {
                    dsList.RetrieveListWrt("", bank_code, cash_type);
                }
                else if (return_type == "ALL")
                {
                    dsList.RetrieveListAll("", bank_code, cash_type);
                }
                try
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        dsList.DdBankbranch(dsList.DATA[i].BANK_CODE);
                    }
                }
                catch { }
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                int success = 0, fail = 0;
                string msg = "", msg2 = "";
                ExecuteDataSource exed = new ExecuteDataSource(this);
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].OPERATE_FLAG == 1)
                    {
                        new_memno = dsList.DATA[i].MEMBER_NO;
                        GenSQLSave(i, exed);
                        success++;

                    }
                }
                exed.Execute();
                msg = " บันทึกสำเร็จ " + success + " รายการ";
                msg2 = " บันทึกไม่สำเร็จเนื่องจากข้อมูลการทำรายการของสมาชิกไม่ตรงกับที่จะทำรายการจำนวน " + fail + " รายการ";
                if (success > 0 && fail > 0)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage(msg + msg2);
                }
                else if (success > 0 && fail == 0)
                {

                    LtServerMessage.Text = WebUtil.CompleteMessage(msg);
                }
                else if (success == 0 && fail > 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(msg2);
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

        private void SetDefaultTofromaccid()
        {
            string sql = @"select account_id
                from cmucftofromaccid 
                where coop_id = {0} 
	            and moneytype_code = {1}
	            and sliptype_code = {2}
	            and default_flag = 1";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].MONEYTYPE_CODE, "PX");
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                string accid = dt.GetString("account_id");
                dsMain.DATA[0].TOFROM_ACCID = accid;
            }
        }

        public void GenSQLSave(int row, ExecuteDataSource exd)
        {

            string sqlSlippayout = "";
            try
            {
                string coop_id = state.SsCoopControl;
                string payoutslip_no = Get_NumberDOC("SLSLIPPAYOUT");
                string memb_no = dsList.DATA[row].MEMBER_NO;
                string sliptype_code = "";
                DateTime operate_date = state.SsWorkDate;
                string loancontract_no = dsList.DATA[row].LOANCONTRACT_NO;
                decimal payout_amt = dsList.DATA[row].SUM_RETURN;
                decimal payoutnet_amt = dsList.DATA[row].SUM_RETURN;
                string expense_bank = "";
                string expense_branch = "";
                string expense_accid = "";
                string entry_id = state.SsUsername;
                DateTime entry_date = state.SsWorkDate;
                decimal returnetc_amt = 0;
                string returnitemtype_code = "";

                DateTime slip_date = dsMain.DATA[0].SLIP_DATE;
                string tofromacc_id = dsMain.DATA[0].TOFROM_ACCID;
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;

                expense_bank = dsList.DATA[row].BANK_CODE;
                expense_branch = dsList.DATA[row].BANK_BRANCH;
                expense_accid = dsList.DATA[row].BANK_ACCID;


                if (dsList.DATA[row].RETURNITEMTYPE_CODE == "WRT")
                {
                    sliptype_code = "WRT";
                    returnetc_amt = payout_amt;
                    returnitemtype_code = "'WRT'";
                }
                else if (dsList.DATA[row].RETURNITEMTYPE_CODE == "RET")
                {
                    sliptype_code = "LRT";
                    returnitemtype_code = "'INT','PRN'";
                }

                //create sql สำหรับต้นคืนดอกเบี้ยคืน ลง slslippayout
                sqlSlippayout = @"INSERT INTO SLSLIPPAYOUT(
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
                sqlSlippayout = WebUtil.SQLFormat(sqlSlippayout,
                    coop_id, payoutslip_no, state.SsCoopId, memb_no,
                    "", sliptype_code, slip_date, operate_date,
                    "", "", loancontract_no, "",
                    0, 0, payout_amt, 0,
                    payoutnet_amt, 0, 0, 0,
                    0, 0, 1, 0,
                    moneytype_code, expense_bank, expense_branch, expense_accid,
                    0, tofromacc_id, 1, "",
                    "", entry_id, entry_date, state.SsCoopId,
                    0, 0, returnetc_amt);

                exd.SQL.Add(sqlSlippayout);

                //create sql สำหรับต้นคืนดอกเบี้ยคืน ลง slslippayoutdet
                decimal ret_prn = dsList.DATA[row].RET_PRN;
                decimal ret_int = dsList.DATA[row].RET_INT;
                decimal sum_ret = dsList.DATA[row].SUM_RETURN;


                if (dsList.DATA[row].RETURNITEMTYPE_CODE == "WRT")
                {
                    //insert payoutdet
                    string sqlPayoutdet = @"insert into SLSLIPPAYOUTDET (COOP_ID , PAYOUTSLIP_NO, SLIPITEMTYPE_CODE , SEQ_NO ,
                        SHRLONTYPE_CODE, CONCOOP_ID , LOANCONTRACT_NO , DEPACCOUNT_NO ,
                        SLIPITEM_DESC , PRINCIPAL_PAYAMT , INTEREST_PAYAMT , ITEM_PAYAMT , 
                        REF_DOCNO , BFSHRCONT_BALAMT , BFCONTLAW_STATUS) 
                        VALUES (
                        {0},{1},{2},{3},
                        {4},{5},{6},{7},
                        {8},{9},{10},{11},
                        {12},{13},{14})
                        ";
                    sqlPayoutdet = WebUtil.SQLFormat(sqlPayoutdet,
                        coop_id, payoutslip_no, "WRT", 0,
                        "", state.SsCoopId, loancontract_no, "",
                        "จ่ายคืนกสส", ret_prn, ret_int, payout_amt,
                        "", 0, 0
                        );

                    string sqlSeqno = "", sqlSelWrt = "", sqlInsert = "", sqlUpdateMemb = "";
                    decimal seqno = 0, old_wrtbal = 0, new_wrtbal = 0;
                    sqlSeqno = "select nvl(max(seq_no),0) as max_seqno from wrtfundstatement where member_no = {0} and coop_id={1} ";
                    sqlSeqno = WebUtil.SQLFormat(sqlSeqno, memb_no, coop_id);
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
                    sqlSelWrt = WebUtil.SQLFormat(sqlSelWrt, memb_no, coop_id, loancontract_no);
                    Sdt dtWrt = WebUtil.QuerySdt(sqlSelWrt);
                    if (dtWrt.Next())
                    {
                        old_wrtbal = dtWrt.GetDecimal("wrtfund_balance");

                    }
                    new_wrtbal = old_wrtbal - sum_ret;

                    sqlInsert = @"INSERT INTO wrtfundstatement (
                        coop_id,            member_no,      seq_no,             operate_date,
                        wrtitemtype_code,   wrtfund_amt,    wrtfund_balance,    ref_contno,
                        moneytype_code,     item_status,    entry_id,           entry_date,
                        ref_docno) 
                        values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                    sqlInsert = WebUtil.SQLFormat(sqlInsert,
                    coop_id, memb_no, seqno, operate_date,
                    "RWT", sum_ret, new_wrtbal, loancontract_no,
                    moneytype_code, 1, state.SsUsername, state.SsWorkDate,
                    "");


                    sqlUpdateMemb = "update mbmembmaster set wrtfund_balance ={2}  where coop_id={0} and member_no={1}";
                    sqlUpdateMemb = WebUtil.SQLFormat(sqlUpdateMemb, state.SsCoopControl, memb_no, new_wrtbal);

                    exd.SQL.Add(sqlPayoutdet);
                    exd.SQL.Add(sqlInsert);
                    exd.SQL.Add(sqlUpdateMemb);
                }
                else if (dsList.DATA[row].RETURNITEMTYPE_CODE == "RET")
                {
                    //insert payoutdet
                    string sqlPayoutdet = @"insert into SLSLIPPAYOUTDET (COOP_ID , PAYOUTSLIP_NO, SLIPITEMTYPE_CODE , SEQ_NO ,
SHRLONTYPE_CODE, CONCOOP_ID , LOANCONTRACT_NO , DEPACCOUNT_NO ,
SLIPITEM_DESC , PRINCIPAL_PAYAMT , INTEREST_PAYAMT , ITEM_PAYAMT , 
REF_DOCNO , BFSHRCONT_BALAMT , BFCONTLAW_STATUS) 
VALUES (
{0},{1},{2},{3},
{4},{5},{6},{7},
{8},{9},{10},{11},
{12},{13},{14})
";
                    sqlPayoutdet = WebUtil.SQLFormat(sqlPayoutdet,
                        coop_id, payoutslip_no, "MRL", 0,
                        "", state.SsCoopId, loancontract_no, "",
                        "ต้นคืนดอกเบี้ยคืน", ret_prn, ret_int, payout_amt,
                        "", 0, 0
                        );

                    //sql select last_stm_no
                    decimal last_stm = 0, current_stm_no = 0, bfprin_bal = 0, bfint_arr = 0, bfint_ret = 0, bfprin_ret = 0;
                    decimal ldc_prnbal = 0, ldc_intretbal = 0, ldc_prnretbal = 0;
                    string status_desc = "";
                    string sqlSel = @"
                                select		last_stm_no, status_desc,principal_balance , interest_arrear , interest_return , principal_return
                                from		lncontmaster
                                where	( loancontract_no	= {0} ) and
			                            ( coop_id	= {1} ) ";
                    sqlSel = WebUtil.SQLFormat(sqlSel, loancontract_no, coop_id);
                    Sdt dtSel = WebUtil.QuerySdt(sqlSel);
                    if (dtSel.Next())
                    {
                        last_stm = dtSel.GetDecimal("last_stm_no");
                        status_desc = dtSel.GetString("status_desc");
                        bfprin_bal = dtSel.GetDecimal("principal_balance");
                        bfint_arr = dtSel.GetDecimal("interest_arrear");
                        bfint_ret = dtSel.GetDecimal("interest_return");
                        bfprin_ret = dtSel.GetDecimal("principal_return");
                    }


                    ldc_prnbal = bfprin_bal + ret_prn;
                    ldc_intretbal = bfint_ret - ret_int;
                    ldc_prnretbal = bfprin_ret - ret_prn;

                    current_stm_no = last_stm + 1;

                    //create sql insert lncontstatement
                    string sqlInsStatement = @"insert into lncontstatement
( loancontract_no,		coop_id,				seq_no,					slip_date,				operate_date,			account_date,			intaccum_date,
	ref_slipno,			ref_docno,				loanitemtype_code,	
	period,		 		principal_payment,	    interest_payment,		principal_balance,		
	prncalint_amt,		bfintarrear_amt,		bfintreturn_amt,		interest_period,
	interest_arrear,	interest_return,		moneytype_code,		    item_status,			entry_id,				entry_date,				entry_bycoopid,
	remark,					bfcontstatus_desc )
values	( {0},			{1},			{2},			{3},			{4},			{5},			{6},
		{7},			{8},		{9},			
		{10},			{11},		{12},			{13},
		{14},			{15},		{16},			{17},
        {18},			{19},		{20},			{21},		{22},			{23},			{24},
{25},		{26})";
                    sqlInsStatement = WebUtil.SQLFormat(sqlInsStatement,
                        loancontract_no, coop_id, current_stm_no, slip_date, operate_date, slip_date, operate_date,
                        payoutslip_no, "KPSLIPNO", "LRT",
                        0, ret_prn, ret_int, ldc_prnbal,
                        0, bfint_arr, bfint_ret, 0,
                        bfint_arr, ret_int, moneytype_code, 1, state.SsUsername, state.SsWorkDate, state.SsCoopId,
                        "", status_desc);

                    string UpdateContmaster = @"update	lncontmaster
			set			principal_balance	= {0},
						interest_return		= {1},
						principal_return	= {2},
                        last_stm_no         ={3}
			where	coop_id				= {4}
			and		( loancontract_no	= {5} )";
                    UpdateContmaster = WebUtil.SQLFormat(UpdateContmaster, ldc_prnbal, ret_int, ret_prn, current_stm_no, coop_id, loancontract_no);


                    exd.SQL.Add(sqlPayoutdet);
                    //เอาออกเนื่องจากไปอัพเดทLncontmaster ตอนตั้งข้อมูลเงินคืน
                    //exd.SQL.Add(sqlInsStatement);
                    //exd.SQL.Add(UpdateContmaster);
                    //end
                }
                //create sql สำหรับอัพเดท mbmoneyreturn
                string sqlUpdateMoneyreturn = @"update mbmoneyreturn set return_status=1,return_id={3},return_date={4}
where coop_id={0} and member_no={1} and loancontract_no={2} and returnitemtype_code in(" + returnitemtype_code + ")";
                sqlUpdateMoneyreturn = WebUtil.SQLFormat(sqlUpdateMoneyreturn, coop_id, memb_no, loancontract_no
                    , state.SsUsername, slip_date);
                exd.SQL.Add(sqlUpdateMoneyreturn);

                //เพิ่มการบันทึกลง dpdepttran
                if (moneytype_code == "TRN")
                {
                    decimal dbseq_no = 0, real_seq = 0;
                    string sqlSeqdepttran = "select isnull(max(seq_no),0)+1 as seq_no from dpdepttran where member_no={0} and deptaccount_no={1} and memcoop_id={2} and system_code='DRT' and tran_date={3}";
                    sqlSeqdepttran = WebUtil.SQLFormat(sqlSeqdepttran, memb_no, expense_accid, state.SsCoopId, operate_date);
                    Sdt dt = WebUtil.QuerySdt(sqlSeqdepttran);
                    if (dt.Next())
                    {
                        dbseq_no = dt.GetDecimal("seq_no");
                    }

                    if (old_memno == new_memno)
                    {
                        tmpseq_no++;
                        real_seq = tmpseq_no;
                    }
                    else
                    {
                        real_seq = dbseq_no;
                        tmpseq_no = dbseq_no;
                        old_memno = new_memno;
                    }
                    string sqldpdepttran = @"insert into dpdepttran
(coop_id,       deptaccount_no, memcoop_id,     member_no,
system_code,    tran_year,      tran_date,      seq_no,
deptitem_amt,   tran_status,    sequest_status, ref_slipno,
branch_operate)
values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})";
                    sqldpdepttran = WebUtil.SQLFormat(sqldpdepttran,
                        coop_id, expense_accid, state.SsCoopId, memb_no,
                        "DRT", (operate_date.Year + 543), operate_date, real_seq,
                        payout_amt, 0, 0, payoutslip_no, "001");
                    exd.SQL.Add(sqldpdepttran);
                }
                //จบการบันทึกลง dpdpettran

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string Get_NumberDOC(string typecode)
        {

            return wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, typecode);
            //string coop_id = state.SsCoopControl;
            //Sta ta = new Sta(state.SsConnectionString);
            //string postNumber = "";
            //try
            //{
            //    ta.AddInParameter("AVC_COOPID", coop_id, System.Data.OracleClient.OracleType.VarChar);
            //    ta.AddInParameter("AVC_DOCCODE", typecode, System.Data.OracleClient.OracleType.VarChar);
            //    ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.VarChar);
            //    ta.ExePlSql("N_PK_DOCCONTROL.OF_GETNEWDOCNO");
            //    postNumber = ta.OutParameter("return_value").ToString();
            //    ta.Close();
            //}
            //catch (Exception ex)
            //{
            //    ta.Close();
            //    throw new Exception(ex.Message);
            //}
            //return postNumber.ToString();
        }

    }
}