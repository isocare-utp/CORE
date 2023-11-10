using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Data;

namespace Saving.Applications.keeping.ws_kp_est_moneyreturn_ctrl
{
    public partial class ws_kp_est_moneyreturn : PageWebSheet,WebSheet
    {
        [JsPostBack]
        public string PostSetMoneyreturn { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack) {
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
                dsMain.DdStartMembgroup();
                dsMain.DdEndMembgroup();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSetMoneyreturn) {
                try
                {
                    SetMoneyreturnNew();
                }catch(Exception ex){
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }
                
        public void SaveWebSheet()
        {

        }
               
        public void WebSheetLoadEnd()
        {

        }

//        public void SetMoneyreturn() {
//            try
//            {
//                string kpslip_no = "", memcoop_id = "", member_no = "", recv_period = "", shrlontype_code = "", loancontract_no = "";
//                string sqlKpDet = "", sqlInsPrn = "", sqlInsInt = "", sqlSelSeq = "", sqlUpdKp = "", sqlIns = "";
//                decimal principal_payment = 0, interest_payment = 0, seq = 0,cnt=0;
//                recv_period = dsMain.DATA[0].RECV_PERIOD;
//                //by mikekong 
//                //ต้องเปลี่ยนไปดึงยอดต้นคืนดอกคืนจาก lncontmaster แทน คอลัม principal_return,interest_return
//                //ต้อง insert contstatement update contmaster
//                ExecuteDataSource exed = new ExecuteDataSource(this);
//                DateTime opdate = dsMain.DATA[0].OPERATE_DATE;
                     
//                sqlKpDet = "select * from kptempreceivedet where keepitem_status=-9 and money_return_status=8 and recv_period={0} and coop_id={1} and keepitemtype_code like 'L%'";
//                sqlKpDet = WebUtil.SQLFormat(sqlKpDet, recv_period, state.SsCoopId);
//                Sdt dtKpdet = WebUtil.QuerySdt(sqlKpDet);
//                while (dtKpdet.Next())
//                {
//                    kpslip_no = dtKpdet.GetString("kpslip_no");
//                    memcoop_id = dtKpdet.GetString("memcoop_id");
//                    member_no = dtKpdet.GetString("member_no");
//                    shrlontype_code = dtKpdet.GetString("shrlontype_code");
//                    loancontract_no = dtKpdet.GetString("loancontract_no");
//                    principal_payment = dtKpdet.GetDecimal("principal_payment");
//                    interest_payment = dtKpdet.GetDecimal("interest_payment");
//                    sqlSelSeq = "select nvl(max(seq_no),0) as max_seq from mbmoneyreturn where coop_id={0} and member_no={1} ";
//                    sqlSelSeq = WebUtil.SQLFormat(sqlSelSeq, state.SsCoopId, member_no);
//                    Sdt dtSelSeq = WebUtil.QuerySdt(sqlSelSeq);
//                    if (dtSelSeq.Next()) { seq = dtSelSeq.GetDecimal("max_seq"); }

//                    sqlIns = @"  INSERT INTO MBMONEYRETURN    ( COOP_ID,MEMBER_NO,SEQ_NO,SYSTEM_FROM,REF_SLIPNO,REF_DOCNO,DESCRIPTION,SHRLONTYPE_CODE,   
//                                BIZZCOOP_ID,LOANCONTRACT_NO,RETURNITEMTYPE_CODE,RETURN_AMOUNT,RETURN_STATUS,ENTRY_ID,ENTRY_DATE )  
//                                VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14} )  ";
//                    sqlInsPrn = WebUtil.SQLFormat(sqlIns, state.SsCoopId, member_no, ++seq, "LON", "", recv_period, "เงินต้นสัญญา " + loancontract_no, shrlontype_code,
//                                    memcoop_id, loancontract_no, "PRN", principal_payment, 8, state.SsUsername, opdate);
//                    sqlInsInt = WebUtil.SQLFormat(sqlIns, state.SsCoopId, member_no, ++seq, "LON", "", recv_period, "ดอกเบี้ยสัญญา " + loancontract_no, shrlontype_code,
//                                    memcoop_id, loancontract_no, "INT", interest_payment, 8, state.SsUsername, opdate);

//                    sqlUpdKp = "update kptempreceivedet set money_return_status=1 where coop_id={0} and kpslip_no={1} and member_no={2} and loancontract_no={3}";
//                    sqlUpdKp = WebUtil.SQLFormat(sqlUpdKp, state.SsCoopId, kpslip_no, member_no, loancontract_no);

//                    exed.SQL.Add(sqlInsPrn);
//                    exed.SQL.Add(sqlInsInt);
//                    exed.SQL.Add(sqlUpdKp);
//                    exed.Execute();
//                    exed.SQL.Clear();
//                    cnt++;
//                }
//                LtServerMessage.Text = WebUtil.CompleteMessage("ตั้งข้อมูลเงินรอจ่ายคืนสำเร็จ " +cnt +" สัญญา");
//            }
//            catch (Exception e) { throw e; }

//        }

        public void SetMoneyreturnNew()
        {
            try
            {
                string memcoop_id = "", member_no = "", recv_period = "", shrlontype_code = "", loancontract_no = "";
                string sqlContmas = "", sqlInsPrn = "", sqlInsInt = "", sqlSelSeq = "",  sqlIns = "";
                decimal principal_return = 0, interest_return = 0, seq = 0, cnt = 0, last_stm = 0, principal_balance = 0, current_stm = 0, currprinc_bal = 0 ;
                string sqlInsStatement = "", UpdateContmaster = "" ;
                string smembgroup_code = "", emembgroup_code = "";
                smembgroup_code = dsMain.DATA[0].SMEMBGROUP_CODE.Trim();
                emembgroup_code = dsMain.DATA[0].EMEMBGROUP_CODE.Trim();

                recv_period = dsMain.DATA[0].RECV_PERIOD;
                //by mikekong 
                //ต้องเปลี่ยนไปดึงยอดต้นคืนดอกคืนจาก lncontmaster แทน คอลัม principal_return,interest_return
                //ต้อง insert contstatement update contmaster
                ExecuteDataSource exed = new ExecuteDataSource(this);
                DateTime slip_date = dsMain.DATA[0].SLIP_DATE;
                DateTime operate_date = dsMain.DATA[0].OPERATE_DATE;

                //sqlContmas = "select * from lncontmaster where principal_return>0 or interest_return>0 and coop_id={0}";
                //sqlContmas = WebUtil.SQLFormat(sqlContmas, state.SsCoopId);
                sqlContmas = @"select l.*
from lncontmaster l,mbmembmaster m
where (l.principal_return>0 or l.interest_return>0)
and l.coop_id={0}
and l.coop_id=m.coop_id
and m.member_no=l.member_no
and ltrim(rtrim(m.membgroup_code)) between {1} and {2}";
                sqlContmas = WebUtil.SQLFormat(sqlContmas, state.SsCoopId, smembgroup_code, emembgroup_code);
                Sdt dtContmas = WebUtil.QuerySdt(sqlContmas);
                while (dtContmas.Next())
                {
                    last_stm = dtContmas.GetDecimal("last_stm_no");
                    principal_balance = dtContmas.GetDecimal("principal_balance");
                    memcoop_id = dtContmas.GetString("memcoop_id");
                    member_no = dtContmas.GetString("member_no");
                    shrlontype_code = dtContmas.GetString("loantype_code");
                    loancontract_no = dtContmas.GetString("loancontract_no");
                    principal_return = dtContmas.GetDecimal("principal_return");
                    interest_return = dtContmas.GetDecimal("interest_return");
                    sqlSelSeq = "select isnull(max(seq_no),0) as max_seq from mbmoneyreturn where coop_id={0} and member_no={1} ";
                    sqlSelSeq = WebUtil.SQLFormat(sqlSelSeq, state.SsCoopId, member_no);
                    Sdt dtSelSeq = WebUtil.QuerySdt(sqlSelSeq);
                    if (dtSelSeq.Next()) { seq = dtSelSeq.GetDecimal("max_seq"); }

                    sqlIns = @"  INSERT INTO MBMONEYRETURN    ( COOP_ID,MEMBER_NO,SEQ_NO,SYSTEM_FROM,REF_SLIPNO,REF_DOCNO,DESCRIPTION,SHRLONTYPE_CODE,   
                                BIZZCOOP_ID,LOANCONTRACT_NO,RETURNITEMTYPE_CODE,RETURN_AMOUNT,RETURN_STATUS,ENTRY_ID,ENTRY_DATE )  
                                VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14} )  ";
                    sqlInsPrn = WebUtil.SQLFormat(sqlIns, state.SsCoopId, member_no, ++seq, "LON", "", recv_period, "เงินต้นสัญญา " + loancontract_no, shrlontype_code,
                                    memcoop_id, loancontract_no, "PRN", principal_return, 8, state.SsUsername, operate_date);
                    sqlInsInt = WebUtil.SQLFormat(sqlIns, state.SsCoopId, member_no, ++seq, "LON", "", recv_period, "ดอกเบี้ยสัญญา " + loancontract_no, shrlontype_code,
                                    memcoop_id, loancontract_no, "INT", interest_return, 8, state.SsUsername, operate_date);


                    current_stm = last_stm + 1;
                    currprinc_bal = principal_balance + principal_return;

                    sqlInsStatement = @"insert into lncontstatement
( loancontract_no,		coop_id,				seq_no,					slip_date,				operate_date,			account_date,			intaccum_date,
	ref_slipno,			ref_docno,				loanitemtype_code,	
	period,		 		principal_payment,	    interest_payment,		principal_balance,		
	prncalint_amt,		bfintarrear_amt,		bfintreturn_amt,		interest_period,
	interest_arrear,	interest_return,		moneytype_code,		    item_status,			entry_id,				entry_date,				entry_bycoopid,
	remark )
values	( {0},			{1},			{2},			{3},			{4},			{5},			{6},
		{7},			{8},		{9},			
		{10},			{11},		{12},			{13},
		{14},			{15},		{16},			{17},
        {18},			{19},		{20},			{21},		{22},			{23},			{24},
{25})";
                    sqlInsStatement = WebUtil.SQLFormat(sqlInsStatement,
                        loancontract_no, state.SsCoopId, current_stm, slip_date, operate_date, slip_date, operate_date,
                        "", "", "LRT",
                        0, principal_return, interest_return, currprinc_bal,
                        0, interest_return, 0, 0,
                        0, 0, "TRN", 1, state.SsUsername, state.SsWorkDate, state.SsCoopId,
                        "เก็บเกินจ่ายคืน");

                    UpdateContmaster = @"update	lncontmaster
			set			principal_balance	= {0},
						interest_return		= {1},
						principal_return	= {2},
                        last_stm_no         ={3}
			where	coop_id				= {4}
			and		( loancontract_no	= {5} )
and member_no={6}";
                    UpdateContmaster = WebUtil.SQLFormat(UpdateContmaster, currprinc_bal, 0, 0, current_stm, state.SsCoopId, loancontract_no,member_no);
                    if (principal_return > 0)
                    {
                        exed.SQL.Add(sqlInsPrn);
                    }
                    if (interest_return > 0)
                    {
                        exed.SQL.Add(sqlInsInt);
                    }
                    exed.SQL.Add(sqlInsStatement);
                    exed.SQL.Add(UpdateContmaster);
                    exed.Execute();
                    exed.SQL.Clear();
                    cnt++;
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("ตั้งข้อมูลเงินรอจ่ายคืนสำเร็จ " + cnt + " สัญญา");
            }
            catch (Exception e) { throw e; }

        }
    }
}