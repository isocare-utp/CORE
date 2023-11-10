
--1.ดึงรายการสร้างเคส เงินฝาก
--select * from dpdeptmaster 
update dpdeptmaster set deptclose_status=0,bank_accid='0520241541'
--update dpdeptmaster set deptclose_status=0,bank_accid='0006184952'
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d where 
 depttype_code='88' 
 --depttype_code='80' 
--and withdrawable_amt > 50000 
--and deptclose_status=0 
and d.member_no='00135670' 
);commit;

--1.1 ตรวจสอบวงเงินฝาก 
--create or replace view V_SCO_ATMDEPT_MASTER as 
select deptaccount_no,depttype_code,member_no,withdrawable_amt,prncbal,deptclose_status,bank_accid,(select expense_accid from mbmembmaster where l.member_no=member_no) as saving_acc from dpdeptmaster d 
--select * from dpdeptmaster 
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d where 
( depttype_code='88')
--and deptclose_status=0 
and d.member_no='00135670' 
)order by member_no asc,deptaccount_no asc;

--1.2 ตรวจสอบวงเงินฝาก statement
--create or replace view V_SCO_ATMDEPT_STATEMENT as 
select * from dpdeptstatement
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d where 
 ( depttype_code='88'  )
 --and withdrawable_amt > 50000 
--and deptclose_status=0 
and d.member_no='00135670' 
)order by deptaccount_no asc,seq_no desc;

--1.3 ตรวจสอบวงเงินฝาก Slip
--create or replace view V_SCO_ATMDEPT_SLIP as 
select * from dpdeptslip where deptaccount_no in (
select deptaccount_no from dpdeptmaster
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d where 
 ( depttype_code='88'  )
 --and withdrawable_amt > 50000 
--and deptclose_status=0 
and d.member_no='00135670' 
))order by deptslip_no desc
;


--1.3 ตรวจสอบวงเงินฝาก Slip Det
--create or replace view V_SCO_ATMDEPT_SLIPDET as 
select * from dpdeptslipdet where deptslip_no in (
select deptslip_no from dpdeptslip where deptaccount_no in (
select deptaccount_no from dpdeptmaster
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d where 
 depttype_code='88' 
 --and withdrawable_amt > 50000 
--and deptclose_status=0 
and d.member_no='00135670' )
)))order by deptslip_no desc
;


--2.ดึงรายการสร้างเคส เงินกู้
--select * from lncontmaster 
--select * from lncontstatement
update lncontmaster set contract_status=1
,expense_accid='0000405981'
where loancontract_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.loancontract_no 
 from lncontmaster d
 --,mbmembmaster m 
 where 
loantype_code='22'  
--and withdrawable_amt > 50000  
--and contract_status =1 
--and m.expense_accid is not null 
and d.member_no='00210545' 
);
commit;

--แล้วตรวจสอบที่ 
--create or replace view V_SCO_ATM_FIN_AMT as 
select * from atm_fin_amt where refno in (
select d.loancontract_no 
 from lncontmaster d
 --,mbmembmaster m 
 where 
loantype_code='22'  
--and withdrawable_amt > 50000  
--and contract_status =1 
--and m.expense_accid is not null 
and d.member_no='00210545' 
);

--2.1 ตรวจสอบ รายละเอียดเงินกู้
--create or replace view V_SCO_ATMLOAN_MASTER as 
select loancontract_no,loantype_code,member_no,withdrawable_amt,loanapprove_amt,principal_balance,(loanapprove_amt-principal_balance) as loan_amt,contract_status ,expense_accid,(select expense_accid from mbmembmaster where l.member_no=member_no) as saving_acc  from lncontmaster l 
--select * from lncontmaster 
--select * from lncontstatement
--update lncontmaster set contract_status=1
where loancontract_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.loancontract_no 
 from lncontmaster d
-- ,mbmembmaster m 
 where 
loantype_code='22'  
--and withdrawable_amt > 50000  
--and contract_status =1 
--and m.expense_accid is not null 
and d.member_no='00210545' 
)order by member_no asc,loancontract_no asc;

--2.2 ตรวจสอบ รายละเอียดเงินกู้ statement 
--create or replace view V_SCO_ATMLOAN_STATEMENT as 
--select * from lncontmaster 
select * from lncontstatement
--update lncontmaster set contract_status=1
where loancontract_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.loancontract_no 
 from lncontmaster d
 --,mbmembexpense m 
 where 
loantype_code='22'  
--and withdrawable_amt > 50000  
--and contract_status =1 
--and m.expense_accid is not null 
and d.member_no='00210545'
) 
--and entry_id like 'ATM%'
order by loancontract_no asc,seq_no desc
;

--2.3 ตรวจสอบ Slip เงินกู้ 

--create or replace view V_SCO_ATMLOAN_SLIPPAYOUT as 
select * from SLSLIPPAYOUT where 
trim(member_no)='00210545' and 
entry_id like 'ATM%'  
--กรณีถอนเงิน
--and sliptype_code ='LWD' 
--กรณีชำระ
--and sliptype_code ='PX'
order by member_no asc,payoutslip_no desc
;

--create or replace view V_SCO_ATMLOAN_SLIPPAYIN as 
select * from SLSLIPPAYIN where 
trim(member_no)='00210545' and 
entry_id like 'ATM%'  
--กรณีถอนเงิน
--and sliptype_code ='LWD' 
--กรณีชำระ
--and sliptype_code ='PX'
order by member_no asc,payinslip_no desc
;
--create or replace view V_SCO_ATMLOAN_SLIPPAYINDET as 
select * from SLSLIPPAYINDET where payinslip_no in (
select payinslip_no from SLSLIPPAYIN where 
trim(member_no)='00210545' and 
entry_id like 'ATM%'  
--กรณีถอนเงิน
--and sliptype_code ='LWD' 
--กรณีชำระ
--and sliptype_code ='PX'
) 
order by payinslip_no desc
;


--3.ดึงรายการ ปรับปรุงทะเบียนและวงเงิน ATM
select * from atm_fin_amt
where ishold is null 
and  member_no not in ('00135670','00210545')
; 

--4.ตรวจสอบเลขที่บัญชีธนาคาร
select member_no,expense_accid from mbmembmaster
where member_no in ('00135670','00210545');

--กรณีฐานข้อมูล 
กรณี Pkg_AtmMgmt.GetSlipNo ใช้  cmshrlondoccontrol กรณี เป็นเบส สหกรณ์ รุ่น =>SCO
กรณี Pkg_AtmMgmt.GetSlipNoNew ใช้  cmdocumentcontrol กรณี เป็นเบส สหกรณ์ รุ่น =>ISCO

--5.Step ระบบเงินกู้ ถอนเงิน 
"CORECOOP"."ATM_TRANS_AR_IU"
PKG_ATMMgmt2.Process_UpdateLoanWithDraw(PKG_ATMMgmt2.C_COOPID, retVal);
GetContract(CoopID, fin.memno);
update atmconfig set x = 'N';
Pkg_AtmMgmt.IsHaveLoanStmt(CoopID, ContNo, BranchId , fin.operatedate)
Pkg_AtmMgmt.GetLoanMasterDt(CoopID, ContNo, LoanType, LastStmtNo, PrnBal, WithDraw, ContSts, IntArrAmt, PerdPay,	PerdPayAll, ContIntType, RkeepPrn, RkeepInt, NkeepInt, LastProcDate, LastCalcIntDate, StartContDate, PaySts, PerdPayAmt, StartKeepDate);
loan_mgmt.CalcInt (LoanType, BfPrnBal, fin.Itemamt, CalcIntFrom, CalcIntTo, StmtDate, LastStmtNo, PerdPay, RkeepPrn, RkeepInt, IntArrAmt, LastCalcIntDate, StartContDate, LastProcDate, PerdPayAmt, PerdPayAll, RegenRkeeping) ;
ตรวจสอบว่า ต้องยิง RegenRkeeping หรือ  pkg_atmmgmt2.KEEPING_ENABLE
==> TRUE
			Pkg_AtmMgmt.Process_GetKPTempRecvDt(CoopID, ContNo, RecvPerd, KpTot, KpPrnc, KpInt, retVal);
				loan_mgmt.GetInitVal(CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType, GBfSts, CountDateFstType, IntRoundType, FixPayCalcType, IncludeIntArrSts, KpItemArrD);
				RegenInt := loan_mgmt.CalcInt (LoanType, ReGenPrnc, StmtDate, LastProcDate, CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType);
				Pkg_AtmMgmt.Process_UpdateKPTEMPRECEIVEDET(CoopID, ContNo, RecvPerd, ReGenPrnc, ReGenKpPrnc, RegenInt, StmtDate, LastProcDate, LastCalcIntDate, retVal);
				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt, StartContDate, StartKeepDate, 0, LastStmtNo, LastCalcIntDate, StmtDate, LastProcDate, StmtDate, ReGenKpPrnc, RegenInt, ReGenKpPrnc, RegenInt, retVal);
==> FALSE
				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt, StartContDate, StartKeepDate, 0, LastStmtNo, LastCalcIntDate, StmtDate, LastProcDate, StmtDate,null, null, null, null, retVal);
				
ตรวจสอบว่าต้อง สร้าง Slip ไหม
pkg_atmmgmt.IsUseSlip
==> TRUE
	SlipNo := loan_mgmt.GetSlipNo(CoopID, fin.memno, Refno, fin.operatedate, fin.Itemamt);
		Pkg_AtmMgmt.Process_AddShrLonSlip(CoopID, SlipNo, CoopBranch, 1, LoanType, ContNo, PerdPay, PerdPay, fin.Itemamt, CalcIntFrom, CalcIntTo, BfPerd, LastCalcIntDate, BfWithDrawAmt,	BfBalAmt, ContSts, ContIntType, RkeepPrn, RkeepInt, NkeepInt, CoopBranch, retVal );

ตรวจสอบว่าต้องสร้าง LoanRec ไหม
pkg_atmmgmt.IsUseLoanRec	
==> TRUE	
	pkg_atmmgmt.GenDocNo('LNRECEIVENO', 'RC', LoanRecNo, retVal);
				Pkg_AtmMgmt.Process_AddLoanRec(CoopID, LoanRecNo, CoopBranch, fin.MemNo, ContNo, LoanType, BfPerd, BfPrnAmt,BfLastCalIntDate, BfLastRecDate, BfLastProcDate, SharStkVal, IntAcc, StmtDate, fin.itemamt, RecPerdFlag, fin.bankcd, substr(fin.accno, 1, 3),  fin.accno, ContIntType, C_ENTRYID, EntryDate, C_TOFROMACCID, retVal);

Pkg_AtmMgmt.Process_AddLoanSTMT(CoopID, ContNo, LastStmtNo, CoopBranch, LoanItemType, StmtDate, StmtRefno, PerdPay, fin.Itemamt, PrnBal, CalcIntFrom, CalcIntTo, IntAmt, IntArrAmt, C_CASHTYPE, 1, C_ENTRYID, sysdate, CoopBranch, LoanRecNo, retVal) ;
rocess_UpdateLoanAmt(CoopID, fin.memno, ContNo, WithDraw, PrnBal, fin.Itemamt, retVal);
update atmconfig set x = 'Y';
Process_SetTransPosted(fin.seqno, retVal); dbms_output.put_line(' Posted : |'||retVal||'|');	


PKG_ATMMgmt2.Process_UpdateDeptWithDraw(PKG_ATMMgmt2.C_COOPID, retVal);
GetDept(CoopID, fin.memno,fin.accno);


-- ตรวจหารายการที่ยิ่งไปเบส สหกรณ์ ไม่สำเร็จ 

CREATE OR REPLACE FORCE VIEW "V_CHECK_ATMTRAN_DIFF_STMT_DEPT" AS 
  select p."CNT_ATM",p."OPERATE_DATE_",p."CNT_COOP",(cnt_atm-cnt_coop) as cnt_diff from ( 
  select a.* ,  (select count(*) from dpdeptstatement where entry_id='ATMKTB' and to_char(operate_date,'yyyymmdd')=operate_date_) as cnt_coop 
  from (  select count(*) as cnt_atm,operate_date_  from   ( 
  select t.*,to_char(operate_date,'yyyymmdd') as operate_date_ from atmtransaction t where system_code='02'  and item_status>=0 
  )  group by operate_date_ ) a   ) p where cnt_atm<> cnt_coop;
 
CREATE OR REPLACE FORCE VIEW "V_CHECK_ATMTRAN_DIFF_STMT_LOAN" AS 
  select p."CNT_ATM",p."OPERATE_DATE_",p."CNT_COOP",(cnt_atm-cnt_coop) as cnt_diff from ( 
  select a.* ,  (select count(*) from lncontstatement where entry_id='ATMKTB' and to_char(operate_date,'yyyymmdd')=operate_date_) as cnt_coop 
  from (  select count(*) as cnt_atm,operate_date_  from   ( 
  select t.*,to_char(operate_date,'yyyymmdd') as operate_date_ from atmtransaction t where system_code='01'  and item_status>=0 
  )  group by operate_date_ ) a   ) p where cnt_atm<> cnt_coop;
 
CREATE OR REPLACE FORCE VIEW "V_CHECK_ATMTRAN_DIFF_DEPT" AS 
select to_char(operate_date,'yyyymmdd') as operate_date_,t.* from atmtransaction t where system_code='02' and item_status>=0 
--and to_char(operate_date,'yyyymmdd')='20160721' 
and trim(atm_no)||trim(atm_seqno) not in (
 select trim(machine_id) from dpdeptstatement where entry_id='ATMKTB' 
 --and to_char(operate_date,'yyyymmdd')='20160721' 
);

CREATE OR REPLACE FORCE VIEW "V_CHECK_ATMTRAN_DIFF_LOAN" AS 
select to_char(operate_date,'yyyymmdd') as operate_date_,t.* from atmtransaction t where system_code='01' and item_status>=0 
--and to_char(operate_date,'yyyymmdd')='20160721' 
and trim(atm_no)||trim(atm_seqno) not in (
 select trim(ref_docno) from lncontstatement where entry_id='ATMKTB' 
 --and to_char(operate_date,'yyyymmdd')='20160721' 
);

			