--------------------------------------------------------
--  File created - Thursday-August-04-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package Body PKG_ATMMGMT2
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "PKG_ATMMGMT2" AS
       row_count number; retVal integer;
        Function x(ContNo varchar2) return varchar2
	is
	begin return '??'||substr(ContNo, 3, length(ContNo));
	end;
       Function IsLinkValid(SLink varchar2) return boolean	is
	n1 number(1);
	db_connect number(1);
	begin
		db_connect := 1;
		begin
			EXECUTE IMMEDIATE  'select 1 from dual@'||SLink;
		EXCEPTION
			WHEN OTHERS THEN db_connect := 0;
			update coop set coophold=1;     
		end;
		return (db_connect = 1);
	end;
        Procedure Process_Move2Success	is
	begin
		insert into atm_trans_post_scss
		(coop_id, seqno, memno, operatedate, fintype, operatecd, systemcd, bankcd, branchcd, atmno, atmseqno, accno, itemamt, ispost, postdate)
		select coop_id, seqno, memno, operatedate, fintype, operatecd, systemcd, bankcd, branchcd, atmno, atmseqno, accno, itemamt, ispost, postdate
		from 		atm_trans
		where 	ispost = POSTED and
			isrec = POSTED and
			transsts <> 'W';
		delete 	from atm_trans
		where 	ispost = POSTED and
			isrec = POSTED and
			transsts <> 'W';
	end;
        Procedure Process_SetTransPosted(aSeqNo number, retVal OUT INTEGER)	is
	begin
		update atm_trans set ispost = POSTED, postdate = sysdate  where seqno = aSeqNo;retVal := SQL%ROWCOUNT;
	end;
        Function IsNewMember(CoopID varchar2, memno varchar2) return boolean	is
	begin
		select count(1) 	into row_count 	from member where coop_id = coopid and trim(member_no) = trim(memno) ;return not (row_count > 0);
	exception
 	  when no_data_found then
		return True;
	end;
        Function getToFromAccID(CoopID varchar2) return varchar2	is
  TOFROMACCID  coop.TOFROMACCID%type;   
	begin
		select TOFROMACCID into TOFROMACCID	from coop where coop_id = coopid ;return TOFROMACCID;
	exception
 	  when no_data_found then
		return C_TOFROMACCID;
	end;
        Procedure Process_AddMember(coopid varchar2, memno varchar2, memnm varchar2, memsurnm varchar2, personid varchar2, retVal OUT INTEGER )	is
	nm	member.name%type;snm	member.surname%type;
	begin
--		nm := Get_name(memno);
--		snm := Get_surname(memno);
		insert into member (coop_id, member_no, name, surname, person_id)
		values (coopid, memno, memnm, memsurnm, personid);
		retVal := SQL%ROWCOUNT;
	end;
        Function GetContract(CoopID varchar2, MemNo varchar2) return varchar2	is
	c atmloan.contract_no%type;
	begin
		select 	trim(contract_no)
		into 		c
		from 		atmloan
		where 	coop_id = coopid and
					trim(member_no) = trim(memno) ;
					return trim(c);
	exception
 	  when no_data_found then
		return null;
	end;
        Function GetContract(CoopID varchar2, MemNo varchar2, AccNo varchar2) return varchar2	is
	c atmloan.contract_no%type;
	begin
		select 	trim(contract_no)
		into 		c
		from 		atmloan
		where 	coop_id = coopid and
					trim(member_no) = trim(memno) and
					trim(saving_acc) = trim(AccNo) ;
					return trim(c);
	exception
 	  when no_data_found then
		return null;
	end;
        Function GetLoanContractSTS(CoopID varchar2, MemNo varchar2, ContNo varchar2) return char	is
	c 	atmloan.contract_no%type;	t 	char(1);
	begin
		begin
		select contract_no into c 	from atmloan where coop_id = coopid and trim(member_no) = trim(memno)     
    -- KPC need to supporting multi coop_acc id 
    --and trim(contract_no) = trim(ContNo) 
    ;
		exception
 		  when no_data_found then
			t := CONTRC_NEW;
		end;
		if trim(ContNo) <> trim(c) then
			t := CONTRC_CHG;
		end if;
		return t;
	end;
        Procedure Process_AddLoanContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, RecAmt number, PayAmt number, AccNo varchar2, IsHold number, retVal OUT INTEGER)	is
	loantypecode lncontmaster.loantype_code%type;
  saving_acc_cnt integer;
  saving_accno atmloan.saving_acc%type;
  begin
    saving_accno:=AccNo;
    select count(saving_acc) into saving_acc_cnt from atmloan where saving_acc=saving_accno;
    if saving_acc_cnt > 0 then saving_accno:='0000000000'; end if;
    select loantype_code into loantypecode from lncontmaster where trim(loancontract_no)=trim(ContNo);
		insert into atmloan (coop_id, member_no, contract_no, credit_amt, balance_amt, receive_amt, pay_amt, saving_acc, loanhold,loantype_code)
		values (coopid, memno, ContNo, CrAmt, BalAmt, RecAmt, PayAmt, saving_accno, IsHold,loantypecode);
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateLoanContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, retVal OUT INTEGER)  is
	begin
		update atmloan set  contract_no = ContNo where coop_id = CoopID and trim(member_no) = trim(MemNo); retVal := SQL%ROWCOUNT;
	end;
        Function IsHaveLoanAcc(CoopID varchar2, MemNo varchar2, AccNo varchar2) return boolean is
	m 	atmloan.member_no%type;
	begin
		begin
		select member_no	into m from 	atmloan	where coop_id = CoopID and trim(member_no) <> trim(MemNo) and trim(saving_acc) = trim(AccNo) ;
	exception
 	  when TOO_MANY_ROWS then
		return True;
 	  when no_data_found then
		return False;
		end;
		return True;
	end;
        Function IsLoanAccCHG(CoopID varchar2, MemNo varchar2, ContNo varchar2, AccNo varchar2) return boolean	is
	acc 	atmloan.saving_acc%type;
	begin
		begin
		select saving_acc	into acc from atmloan	where coop_id = CoopID and trim(member_no) = trim(MemNo) and trim(contract_no) = trim(ContNo) ;
	exception
 	  when no_data_found then
		return False;
		end;
		return (trim(acc) <>  trim(AccNo));
	end;
        Procedure Process_HoldLoanAccNo(CoopID varchar2, MemNo varchar2, retVal OUT INTEGER) is
	begin
		update atmloan set loanhold = 1 where coop_id = CoopID and trim(member_no) = trim(MemNo);retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateLoanAccNo(CoopID varchar2, MemNo varchar2, ContNo varchar2, AccNo varchar2, retVal OUT INTEGER) is
	begin
		update atmloan set saving_acc= AccNo where coop_id = CoopID and trim(member_no) = trim(MemNo) and	trim(contract_no) = trim(ContNo);	retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateLoanAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, RecvAmt number,PayAmt number, retVal OUT INTEGER)	is
 WithDrawable_amt LNCONTMASTER.withdrawable_amt%type;
 prncbal LNCONTMASTER.PRINCIPAL_BALANCE%type;
  begin
    
    select WithDrawable_amt ,PRINCIPAL_BALANCE
     into WithDrawable_amt,prncbal
     from LNCONTMASTER
     where trim(loancontract_no) = trim(ContNo);
    
    if REGEN_PERIOD_PAYMENT_ENABLE then 
    update LNCONTMASTER l set l.period_payment=
    ( (principal_balance/period_payamt ) -
      mod(principal_balance/period_payamt ,  (select payround_factor  from lnloantype where	loantype_code = l.loantype_code) )
     +  (select payround_factor  from lnloantype where	loantype_code = l.loantype_code) 
     ) 
    where l.loanpayment_type=1 and trim(l.loancontract_no) = trim(ContNo);
    end if; 
     
		update 	atmloan
		set 		
          --credit_amt= CrAmt,
          credit_amt = WithDrawable_amt,
					--balance_amt = BalAmt 
					balance_amt = prncbal 
					,receive_amt = case when receive_amt - RecvAmt  < 0 then 0 else receive_amt - RecvAmt end
					,pay_amt =  case when pay_amt - PayAmt  < 0 then 0 else pay_amt - PayAmt end
		where 	coop_id = CoopID and
					trim(member_no) = trim(MemNo) and
					trim(contract_no)  = trim(ContNo);
					retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateLoanAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, retVal OUT INTEGER)	is
	WithDrawable_amt LNCONTMASTER.withdrawable_amt%type;
  prncbal LNCONTMASTER.PRINCIPAL_BALANCE%type;
  begin
   
    select WithDrawable_amt ,PRINCIPAL_BALANCE
     into WithDrawable_amt,prncbal
     from LNCONTMASTER
     where trim(loancontract_no) = trim(ContNo);
    
    if REGEN_PERIOD_PAYMENT_ENABLE then  
    update LNCONTMASTER l set l.period_payment=
    ( (principal_balance/period_payamt ) -
      mod(principal_balance/period_payamt ,  (select payround_factor  from lnloantype where	loantype_code = l.loantype_code) )
     +  (select payround_factor  from lnloantype where	loantype_code = l.loantype_code) 
     ) 
    where l.loanpayment_type=1 and trim(l.loancontract_no) = trim(ContNo);
    end if; 
     
		update 	atmloan
		set 		
          --credit_amt= CrAmt,
          credit_amt = WithDrawable_amt,
					--balance_amt = BalAmt 
					balance_amt = prncbal 
		where 	coop_id = CoopID and
					trim(member_no) = trim(MemNo) and
					trim(contract_no)  = trim(ContNo);
					retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateLoanAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, OperateCD varchar2, PayAmt number)	is
	WithDrawable_amt LNCONTMASTER.withdrawable_amt%type;
  prncbal LNCONTMASTER.PRINCIPAL_BALANCE%type;
  begin
  
    select WithDrawable_amt ,PRINCIPAL_BALANCE
     into WithDrawable_amt,prncbal
     from LNCONTMASTER
     where trim(loancontract_no) = trim(ContNo);
     
    if REGEN_PERIOD_PAYMENT_ENABLE then  
    update LNCONTMASTER l set l.period_payment=
    ( (principal_balance/period_payamt ) -
      mod(principal_balance/period_payamt ,  (select payround_factor  from lnloantype where	loantype_code = l.loantype_code) )
     +  (select payround_factor  from lnloantype where	loantype_code = l.loantype_code) 
     ) 
    where l.loanpayment_type=1 and trim(l.loancontract_no) = trim(ContNo);
    end if; 
     
--		if OperateCD = TRANS3 then
			update atmloan 	set --credit_amt= CrAmt,
--				pay_amt = case when pay_amt - PayAmt < 0 then 0	else pay_amt - PayAmt end,
--				balance_amt = case when balance_amt - PayAmt < 0 then 0	else balance_amt - PayAmt end 
          --credit_amt= CrAmt,
          credit_amt = WithDrawable_amt,
					--balance_amt = BalAmt 
					balance_amt = prncbal 
			where coop_id = CoopID and	trim(member_no) = trim(MemNo) and	trim(contract_no)  = rtrim(ContNo);
--		else if OperateCD = TRANS1 then
--			update 	atmloan	set
--				credit_amt = case when credit_amt - PayAmt < 0 then 0 else credit_amt - PayAmt end,
--				RECEIVE_AMT = case when RECEIVE_AMT - PayAmt < 0 then 0 else RECEIVE_AMT - PayAmt end,
--				balance_amt = balance_amt + PayAmt
--			where coop_id = CoopID and trim(member_no) = trim(MemNo) and	trim(contract_no)  = trim(ContNo);
--		end if;
	exception
 	  when NO_DATA_FOUND then
		raise_application_error(-20001, 'error');
	end;
        Procedure Process_UpdateDeptAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, RecvAmt number, PayAmt number, retVal OUT INTEGER) is
withdrawable_amt DPDEPTMASTER.withdrawable_amt%type;
 prncbal DPDEPTMASTER.prncbal%type;
 depttypecode DPDEPTMASTER.depttype_code%type;
  begin
   select withdrawable_amt ,prncbal,depttype_code
     into withdrawable_amt,prncbal,depttypecode
     from DPDEPTMASTER
     where trim(deptaccount_no) = trim(ContNo) and trim(member_no)=trim(MemNo);
   update atmdept set
			--withatm_amt = CrAmt  ,
      withatm_amt = withdrawable_amt  ,
			--balance_amt = BalAmt  
      balance_amt = prncbal 
      ,depttype_code =depttypecode 
			,receive_amt = case when receive_amt - RecvAmt  < 0 then 0 else receive_amt - RecvAmt end
			,pay_amt = case when pay_amt - PayAmt  < 0 then 0 else pay_amt - PayAmt end
		where coop_id = CoopID and trim(member_no) = trim(MemNo) and trim(coop_acc)  = trim(ContNo);

--		update atmdept set withatm_amt= CrAmt,balance_amt = BalAmt where coop_id = CoopID and trim(member_no) = trim(MemNo) and trim(coop_acc)  = trim(ContNo);
		retVal := SQL%ROWCOUNT;
	exception
 	  when NO_DATA_FOUND then  retVal := -1;
	end;
        Procedure Process_UpdateDeptAmt(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, retVal OUT INTEGER) is
	withdrawable_amt DPDEPTMASTER.withdrawable_amt%type;
 prncbal DPDEPTMASTER.prncbal%type;
 depttypecode DPDEPTMASTER.depttype_code%type;
  begin
     select withdrawable_amt ,prncbal,depttype_code
     into withdrawable_amt,prncbal,depttypecode
     from DPDEPTMASTER
     where trim(deptaccount_no) = trim(ContNo) and trim(member_no)=trim(MemNo);
		update atmdept 
    set 
        	--withatm_amt = CrAmt  ,
      withatm_amt = withdrawable_amt  ,
			--balance_amt = BalAmt  
      balance_amt = prncbal 
      --  receive_amt = 0 
      ,depttype_code = depttypecode
		where coop_id = CoopID and trim(member_no) = trim(MemNo) and trim(coop_acc)  = trim(ContNo);
		retVal := SQL%ROWCOUNT;
	exception
 	  when NO_DATA_FOUND then  retVal := -1;
	end;
        Procedure Process_UpdateDeptContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, retVal OUT INTEGER) is
	begin
		update atmdept set coop_acc = ContNo where coop_id = CoopID and trim(member_no) = trim(MemNo); 	retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_DelDeptContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, retVal OUT INTEGER) is
	begin
		delete from atmdept 	where coop_id = CoopID and trim(member_no) = trim(MemNo) and trim(coop_acc) = trim(ContNo);retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_AddDeptContract(CoopID varchar2, MemNo varchar2, ContNo varchar2, CrAmt number, BalAmt number, RecAmt number, PayAmt number, AccNo varchar2, IsHold number, retVal OUT INTEGER) is
	depttypecode dpdeptmaster.depttype_code%type;
  saving_acc_cnt integer;
  saving_accno atmdept.saving_acc%type;
  begin
    saving_accno:=AccNo;
    select count(saving_acc) into saving_acc_cnt from atmdept where saving_acc=saving_accno;
    if saving_acc_cnt > 0 then saving_accno:='0000000000'; end if;
    select depttype_code into depttypecode from dpdeptmaster where trim(deptaccount_no)=trim(ContNo);
		insert into atmdept (coop_id, member_no, coop_acc, withatm_amt, balance_amt, receive_amt, pay_amt, saving_acc, depthold,depttype_code) 	values (coopid, memno, ContNo, CrAmt, BalAmt, RecAmt, PayAmt, saving_accno, IsHold,depttypecode);
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateDeptAccNo(CoopID varchar2, MemNo varchar2, ContNo varchar2, AccNo varchar2, retVal OUT INTEGER) is
	begin
		update 	atmdept 	set saving_acc= AccNo where coop_id = CoopID and trim(member_no) = trim(MemNo) and	trim(coop_acc) = trim(ContNo);	retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_HoldDeptAccNo(CoopID varchar2, MemNo varchar2, retVal OUT INTEGER) is
	begin
		update atmdept set depthold = 1 where 	coop_id = CoopID and trim(member_no) = trim(MemNo);	retVal := SQL%ROWCOUNT;
	end;
        Function IsDeptAccCHG(CoopID varchar2, MemNo varchar2, ContNo varchar2, AccNo varchar2) return boolean	is
	acc 	atmdept.saving_acc%type;
	begin
		begin
		select saving_acc	into acc	from 	atmdept	where coop_id = CoopID and trim(member_no) = trim(MemNo) and trim(coop_acc) = trim(ContNo) ;
	exception
 	  when no_data_found then
		return False;
		end;
		return (trim(acc) <> trim(AccNo));
	end;
        Function IsHaveDeptAcc(CoopID varchar2, MemNo varchar2, AccNo varchar2) return boolean	is
	m 	atmdept.member_no%type;
	begin
		begin
		select member_no	into m from 	atmdept	where coop_id = CoopID and trim(member_no) not in ( trim(MemNo) ) and	trim(saving_acc) = trim(AccNo) ;
	exception
 	  when no_data_found then
		return False;
    --WHEN TOO_MANY_ROWS THEN 
    --return true;
		end;
		return True;
	end;
       Function GetDeptContractSTS(CoopID varchar2, MemNo varchar2, ContNo varchar2) return char is
	c 	atmdept.coop_acc%type;t 	char(1);
	begin
		begin
		select coop_acc into 	c from atmdept where coop_id = coopid and trim(member_no) = trim(memno) 
    -- KPC need to supporting multi coop_acc id 
    and trim(coop_acc) = trim(ContNo) 
    ;
		exception
 		  when no_data_found then
			t := CONTRC_NEW;
		end;
		if trim(ContNo) <> trim(c) then
			t := CONTRC_CHG;
		end if;
		return t;
	end;
        Function GetDeptType(CoopID varchar2, MemNo varchar2,AccNo varchar2) return varchar2 is
	c char(2);
	begin
		select depttype_code into c	from 	atmdept	where coop_id = coopid and trim(member_no) = trim(memno) and trim(saving_acc)=trim(AccNo) ;
		return trim(c);
	exception
 	  when no_data_found then
		return null;
	end;
        Function GetDept(CoopID varchar2, MemNo varchar2,AccNo varchar2) return varchar2 is
	c varchar2(15);
	begin
		select substr(coop_acc, 1, 15) into c	from 	atmdept	where coop_id = coopid and trim(member_no) = trim(memno) and trim(saving_acc)=trim(AccNo) ;
		return trim(c);
	exception
 	  when no_data_found then
		return null;
	end;
        Procedure Process_Loan(CoopID varchar2)	is
	type fin_cursor is REF CURSOR;
  c_fin fin_cursor;
  fin atm_trans%rowtype;
  RecvAmt atmloan.receive_amt%type;
  PayAmt atmloan.pay_amt%type;
  ContNo varchar2(10);
	ReGenKpPrnc	number(15, 2);
	ReGenPrnc	  number(15, 2);
	ReGenInt		number(15, 2);
	RecvPerd		number(15,2);
	KpTot			number(15,2);
	KpInt			number(15,2);
	KpPrnc		number(15, 2);
	IntAmt		number(15, 2);
	RegenRkeeping	boolean;
	RkeepPrn number(9, 2);
	RkeepInt number(9, 2);
	BranchId	varchar2(6);
	LoanRec	boolean := True;
	LoanType char(2);
	LastStmtNo number(5);
	EntryDate	date;
	LoanItemType 	varchar2(3);
	StmtRefno varchar2(15);
	Refno varchar2(15);
	BFPrnBal number(15, 2);
	LastPerdPay number(3);
	PerdPay number(3);
	LoanRecNo	varchar2(13);
	SlipNo varchar2(10);
	PerdPayAmt	number(9, 2);
--	PerdPayMent	number(9, 2);
	StartContDate	date;
	LastCalcIntDate	date;
	LastProcDate date;
	PrnBal number(15, 2);
	WithDraw number(15, 2);
	ContSts	number(15, 2);
	IntArrAmt	number(9, 2);
	CoopBranch varchar2(3) := '001';
	ContIntType number(2);
	NkeepInt number(9, 2);
	PaySts number(2);
	StartKeepDate date;
	PrnPay number(9, 2);
	CalcIntFrom date;
	CalcIntTo date;
	BfPerd number(3);
	StmtDate date;
	BfWithDrawAmt number(15, 2);
	BfBalAmt number(15, 2);
	BfPrnAmt number(15, 2);
	BfLastCalIntDate date;
	BfLastRecDate date;
	BfLastProcDate date;
	SharStkVal number(9, 2);
	IntAcc number(9, 2);
	RecPerdFlag number(2);
	CountDateType		number(2);
	IntRoundNum		number(2);
	IntRoundSumType	number(2);
	IntRoundType		number(2);
	IntRoundFormat	number(2);
	FixPayCalcType		number(2);
	IntRoundKpType	number(2);
	GBfSts				number(2);
	CountDateFstType	number(2);
	IncludeIntArrSts	number(2);
	KpItemArrD			number(2);
	PerdPayAll			number(3);
  SlipTypeCode varchar2(3);
  StmItemType varchar2(3);
	begin
		open c_fin for
		select 	*
		from 		atm_trans
		where 	fintype = 'L' and
					(transsts<>'W') and
					(ispost = 'Y') and
					(isrec is null or trim(isrec) = '') and
					itemamt > 0
		order by operatedate;
		loop
			fetch c_fin into fin;
			exit when c_fin%notfound;
			fin.memno := trim(fin.memno);
			ContNo := GetContract(CoopID, fin.memno);
			BranchId := '001';
			if ContNo is null  then
				goto	CONTINUE;
			end if;
--------------------------check Post ?
			if Pkg_AtmMgmt.IsHaveLoanStmt(CoopID, ContNo, BranchId , fin.operatedate) then
				goto	CONTINUE;
			end if;
			retVal := Pkg_AtmMgmt.GetLoanMasterDt(CoopID, ContNo, LoanType, LastStmtNo, PrnBal, WithDraw, ContSts, IntArrAmt, PerdPay,	PerdPayAll, ContIntType, RkeepPrn, RkeepInt, NkeepInt, LastProcDate, LastCalcIntDate, StartContDate, PaySts, PerdPayAmt, StartKeepDate);
				dbms_output.put_line(' ------------GetLoan = '||retVal);
			if fin.transsts = 'Y' then
				dbms_output.put_line(' ------------PerdPay = '||PerdPay);dbms_output.put_line(' GetLoanMasterDt: Seqno = '||fin.Seqno||'| '||'ContNo = '||ContNo||'|'||'PrnBal = '||PrnBal||'WithDraw ='||WithDraw||' fin.ItemAmt='||fin.ItemAmt||' '||retVal||'|');
				StmtDate := trunc(fin.operatedate);
				StmtRefno := trim(fin.atmno)||trim(fin.atmseqno);
				Refno := substr(trim(fin.atmno), 1, 15);
				BFPrnBal := PrnBal;
				LastStmtNo := LastStmtNo + 1;
				LastPerdPay := PerdPay;
				EntryDate := sysdate;
        RecvAmt:=0;
        PayAmt:=0;
				if (LastStmtNo = 1) or (PrnBal = 0) then
					StartContDate := StmtDate;
					LastProcDate := StmtDate;
					LastCalcIntDate := StmtDate;
				end  if;
				case
					when fin.operatecd =TRANS1 or fin.operatecd =TRANS2  then
						LoanItemType := 'LRC';
            SlipTypeCode := 'LWD';
            StmItemType := 'LRC';
            RecvAmt :=fin.Itemamt;
						PrnBal := PrnBal + fin.Itemamt;
--						WithDraw := WithDraw - fin.Itemamt + fin.Itemamt;
						PerdPay := 0;
	-------------------CalcInt
						dbms_output.put_line(' ------------PrnBal = '||PrnBal||' PerdPayAmt='||PerdPayAmt||' StmtDate='||StmtDate||' RkeepPrn='||RkeepPrn);dbms_output.put_line(' ------------RkeepInt = '||RkeepInt||' IntArrAmt='||IntArrAmt||' LastCalcIntDate='||LastCalcIntDate||' StartContDate='||StartContDate);dbms_output.put_line(' ------------LastProcDate = '||LastProcDate||' PerdPayAmt='||PerdPayAmt);
						dbms_output.put_line(' ------------WithDraw = '||WithDraw||' fin.Itemamt='||fin.Itemamt);
        					IntAmt := loan_mgmt.CalcInt (LoanType, BfPrnBal, fin.Itemamt, CalcIntFrom, CalcIntTo, StmtDate, LastStmtNo, PerdPay, RkeepPrn, RkeepInt, IntArrAmt, LastCalcIntDate, StartContDate, LastProcDate, PerdPayAmt, PerdPayAll, RegenRkeeping) ;
					when fin.operatecd =TRANS3 or fin.operatecd =TRANS4  then
						LoanItemType := 'LPX';
            SlipTypeCode := 'PX';
            StmItemType := 'LPX';
            PayAmt :=fin.Itemamt;
						PrnBal := PrnBal - fin.Itemamt;
--						WithDraw := WithDraw + fin.Itemamt - fin.Itemamt;
						PerdPay := PerdPay + 1;
				end case;
				if RegenRkeeping then
					ReGenPrnc := PrnBal;
					Pkg_AtmMgmt.Process_GetKPTempRecvDt(CoopID, ContNo, RecvPerd, KpTot, KpPrnc, KpInt, retVal);
					ReGenKpPrnc := KpTot - RegenInt;
			        	loan_mgmt.GetInitVal(CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType, GBfSts, CountDateFstType, IntRoundType, FixPayCalcType, IncludeIntArrSts, KpItemArrD);
					RegenInt := loan_mgmt.CalcInt (LoanType, ReGenPrnc, StmtDate, LastProcDate, CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType);
					Pkg_AtmMgmt.Process_UpdateKPTEMPRECEIVEDET(CoopID, ContNo, RecvPerd, ReGenPrnc, ReGenKpPrnc, RegenInt, StmtDate, LastProcDate, LastCalcIntDate, retVal);
					Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt, StartContDate, StartKeepDate, 0, LastStmtNo, LastCalcIntDate, StmtDate, LastProcDate, StmtDate, ReGenKpPrnc, RegenInt, ReGenKpPrnc, RegenInt, retVal);
				else
          Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt, StartContDate, StartKeepDate, 0, LastStmtNo, LastCalcIntDate, StmtDate, LastProcDate, StmtDate,null, null, null, null, retVal);
				end if;
						dbms_output.put_line(' -----Process_UpdateLNCONTMASTER-------retVal = '||retVal);

				if pkg_atmmgmt.IsUseSlip then
          SlipNo := loan_mgmt.GetSlipNo(CoopID, fin.memno, Refno, fin.operatedate, fin.Itemamt);
          --if SlipNo is not null then
          --pkg_atmmgmt.GenDocNo('CMSLIPNO', 'SL', SlipNo, retVal);
          if SlipNo is null then 
            case
				   when fin.operatecd =TRANS1 or fin.operatecd =TRANS2  then
             pkg_atmmgmt.GenDocNoNew('SLSLIPPAYOUT', 'SL', SlipNo, retVal);
             Pkg_AtmMgmt.Process_AddLoanRec(CoopID, SlipNo,StmtRefno,CoopBranch, fin.MemNo, ContNo, LoanType, BfPerd, BfPrnAmt,CalcIntFrom, BfLastRecDate, CalcIntTo, SharStkVal, IntAcc, StmtDate, fin.itemamt, RecPerdFlag, fin.bankcd, substr(fin.accno, 1, 3),  fin.accno, ContIntType, C_ENTRYID, EntryDate, C_TOFROMACCID, retVal);
           when fin.operatecd =TRANS3 or fin.operatecd =TRANS4  then
             pkg_atmmgmt.GenDocNoNew('SLSLIPPAYIN', 'SL', SlipNo, retVal);
             Pkg_AtmMgmt.Process_AddLoanSlip(CoopID, ContNo, fin.memno , SlipNo,SlipTypeCode, BranchId , Refno ,StmtDate, fin.operatedate, fin.bankcd, substr(fin.accno, 1, 3),  fin.accno , C_TOFROMACCID , fin.Itemamt , C_ENTRYID , EntryDate , retVal );
					   Pkg_AtmMgmt.Process_AddShrLonSlip(CoopID, SlipNo,StmItemType, CoopBranch, 1, LoanType, ContNo, PerdPay, PerdPay, fin.Itemamt, CalcIntFrom, CalcIntTo, BfPerd, LastCalcIntDate, BfWithDrawAmt,	BfBalAmt, ContSts, ContIntType, RkeepPrn, RkeepInt, NkeepInt, CoopBranch, retVal );         
           end case;
						dbms_output.put_line(' ------------CoopID = '||CoopID);
						dbms_output.put_line(' ------------SlipNo = '||SlipNo);
						dbms_output.put_line(' ------------fin.memno = '||fin.memno||'|');
          end if;  
				--	SlipNo := loan_mgmt.GetSlipNo(CoopID, fin.memno, Refno, fin.operatedate, fin.Itemamt);
				--	if SlipNo is not null then
				--		Pkg_AtmMgmt.Process_AddShrLonSlip(CoopID, SlipNo,StmItemType, CoopBranch, 1, LoanType, ContNo, PerdPay, PerdPay, fin.Itemamt, CalcIntFrom, CalcIntTo, BfPerd, LastCalcIntDate, BfWithDrawAmt,	BfBalAmt, ContSts, ContIntType, RkeepPrn, RkeepInt, NkeepInt, CoopBranch, retVal );
				--	end if;
				--end if;
				--if pkg_atmmgmt.IsUseLoanRec and ( fin.operatecd =TRANS1 or fin.operatecd =TRANS2 ) then ---LoanRec
				--	--pkg_atmmgmt.GenDocNo('LNRECEIVENO', 'RC', LoanRecNo, retVal);
        --  if LoanRecNo is null then pkg_atmmgmt.GenDocNoNew('LNRECEIVENO', 'RC', LoanRecNo, retVal);end if;
				--		dbms_output.put_line(' ------------LoanRecNo = '||LoanRecNo);
				--	Pkg_AtmMgmt.Process_AddLoanRec(CoopID, LoanRecNo,StmtRefno, CoopBranch, fin.MemNo, ContNo, LoanType, BfPerd, BfPrnAmt,BfLastCalIntDate, BfLastRecDate, BfLastProcDate, SharStkVal, IntAcc, StmtDate, fin.itemamt, RecPerdFlag, fin.bankcd, substr(fin.accno, 1, 3),  fin.accno, ContIntType, C_ENTRYID, EntryDate, C_TOFROMACCID, retVal);
				end if;
				Pkg_AtmMgmt.Process_AddLoanSTMT(CoopID, ContNo, LastStmtNo, CoopBranch, LoanItemType, StmtDate, StmtRefno, PerdPay, fin.Itemamt, PrnBal, CalcIntFrom, CalcIntTo, IntAmt, IntArrAmt, C_CASHTYPE, 1, C_ENTRYID, sysdate, CoopBranch, LoanRecNo, retVal) ;
				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, WithDraw, PrnBal, retVal);
				Process_UpdateLoanAmt(CoopID, fin.memno, ContNo, WithDraw, PrnBal,RecvAmt, PayAmt, retVal);
				update atm_trans set isrec = 'Y'  where seqno = fin.seqno;
				dbms_output.put_line(' Update LnContMaster  : '||WithDraw||'|'||retVal||'| PrnBal ='||PrnBal);
			elsif fin.transsts = 'N' then
				Refno := substr(trim(fin.atmno), 1, 15);
				case
					when fin.operatecd =TRANS1 or fin.operatecd =TRANS2  then
						LoanItemType := 'LRC';
--						PrnBal := PrnBal - fin.Itemamt;
						WithDraw := WithDraw + fin.Itemamt;
						PerdPay := 0;
					when fin.operatecd =TRANS3 or fin.operatecd =TRANS4  then
						LoanItemType := 'LPX';
--						PrnBal := PrnBal + fin.Itemamt;
						WithDraw := WithDraw - fin.Itemamt;
--						PerdPay := PerdPay + 1;
--						LoanRec:= true;
				end case;
				dbms_output.put_line(' Update LnContMaster  : '||WithDraw||'|'||retVal||'| PrnBal ='||PrnBal);
				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, WithDraw, PrnBal, retVal);
				Loan_Mgmt.Process_UpdateSlipSts(CoopID, fin.memno, Refno, fin.operatedate, fin.Itemamt, 0, retVal);
				Process_UpdateLoanAmt(CoopID, fin.memno, ContNo, WithDraw, PrnBal, retVal);
				update atm_trans set isrec = 'Y'  where seqno = fin.seqno;
			end if;
			<<CONTINUE>> null;
  		end loop;
  		close c_fin;
 	end;
        Procedure Process_Dept(CoopID varchar2)	is
	type fin_cursor is REF CURSOR;
  c_fin fin_cursor;fin atm_trans%rowtype; 
  RecvAmt atmdept.receive_amt%type;
  PayAmt atmdept.pay_amt%type;
	SlipNo	varchar2(10);	
  DeptAccNo 	varchar2(10);	StmtRefNo 	varchar2(20);
	StmtDate		date; 	Refno varchar2(15);
  LastCalcIntDate date;
  DeptItemType	char(3);
  AccIntAmt number(12, 5);
  PrnBalTemp	number(17, 5);	
  WithDrawTemp number(17, 5);	
  DeptAmt number(17, 5);	
  CalcIntFrom date; 
  CalcIntTo date;	
  IntAmt number(12,5);
  PrnBal	number(17, 5);	
  WithDraw number(17, 5);
  WithAmt number(17, 5);
  DOperateBal number (17,5) := 0;	
  LastStmtNo number(5); 
  BranchId varchar2(6);	
  PrncNo number;
  LastAccDate date;	
  DeptType varchar2(2);
  LastMoveDate date;
  DeptGrpCD varchar2(2) := '00';
  ChqPendAmt number(17, 5);
  BranchOrg varchar2(3) := '001';	
  c integer;	
  DeptCloseSts number(1);	
  SequsetSts number(1);	
  IntArrAmt number(12, 5);	
  SpcIntRate number(12, 5); 
  SpcIntRateSts number(1);
------------------------------------------------Calculate Interest
	CalcIntRound	integer;MinCalcInt	number(2);
	begin
		open c_fin for
		select 	*
		from 		atm_trans
		where 	fintype = 'D' and
					(transsts<>'W') and
					(ispost = 'Y') and
					(isrec is null or trim(isrec) = '') and
					itemamt > 0 order by operatedate;
		loop
			fetch c_fin into fin;
			exit when c_fin%notfound;
			DeptAccNo := GetDept(CoopID, fin.memno,fin.accno);
      DeptType :=  GetDeptType(CoopID, fin.memno,fin.accno);
			if DeptAccNo is null  then
				goto	CONTINUE;
			end if;
--------------------------check Post ?
			if Pkg_AtmMgmt.IsHaveDeptStmt(CoopID, DeptAccNo, DeptType, fin.operatedate) then
				goto	CONTINUE;
			end if;
			c := pkg_atmmgmt.GetDeptMasterDt(CoopID, DeptAccNo, DeptType, LastStmtNo, PrnBal, WithDraw, DeptCloseSts, SequsetSts, LastCalcIntDate, AccIntAmt, IntArrAmt, SpcIntRate, SpcIntRateSts, ChqPendAmt, DeptAmt, WithAmt, BranchId);
			if fin.transsts = 'Y' then
--------------------------check A/C Closed --
				if DeptCloseSts = 1  then
					goto	CONTINUE;
				end if;
				StmtDate := trunc(fin.operatedate);
				StmtRefno := trim(fin.atmno)||trim(fin.atmseqno);
				Refno := substr(trim(fin.atmno), 1, 15);
				DOperateBal := DOperateBal + fin.ItemAmt;
				LastStmtNo := LastStmtNo + 1;
        RecvAmt:=0;PayAmt:=0;
--------------------------Calc Interest
				if LastCalcIntDate > StmtDate then
					CalcIntFrom := StmtDate;
					CalcIntTo := LastCalcIntDate;
					IntAmt := Dept_Mgmt.CalcInt(DeptType, fin.ItemAmt, CalcIntFrom, CalcIntTo);
				else
					CalcIntFrom := LastCalcIntDate;
					CalcIntTo := StmtDate;
					IntAmt := Dept_Mgmt.CalcInt(DeptType, PrnBal, CalcIntFrom, CalcIntTo, SpcIntRateSts, SpcIntRate, ChqPendAmt);
				end if;
				dbms_output.put_line(' ----IntAmt='||IntAmt);
				if fin.operatecd = TRANS1 then
					DeptItemType := 'WTE';
					AccIntAmt := AccIntAmt + IntAmt;
					PrnBalTemp := PrnBal - fin.ItemAmt ;
					WithDrawTemp := WithDraw; -- - fin.ItemAmt;
					WithAmt := WithAmt + fin.ItemAmt;
          RecvAmt:=fin.ItemAmt;
				else
					DeptItemType := 'DTE';
					AccIntAmt := AccIntAmt + IntAmt;
					PrnBalTemp := PrnBal + fin.ItemAmt;
					WithDrawTemp := WithDraw; -- + fin.ItemAmt;
					DeptAmt := DeptAmt + fin.ItemAmt;
          PayAmt:=fin.ItemAmt;
				end if;
				DOperateBal := DOperateBal + fin.ItemAmt;
				dbms_output.put_line(' ----PrnBalTemp='||PrnBalTemp);dbms_output.put_line(' ----WithDrawTemp='||WithDrawTemp);dbms_output.put_line(' ----DOperateBal='||DOperateBal);	dbms_output.put_line(' ----CalcIntTo='||CalcIntTo);
				Pkg_AtmMgmt.Process_UpdateDeptMaster(CoopID, DeptAccNo, BranchId, PrnBalTemp, WithDrawTemp, AccIntAmt,LastCalcIntDate, LastAccDate, LastStmtNo, DOperateBal, LastMoveDate, DeptAmt, WithAmt, retVal );
				PrncNo := 0;
				SlipNo := dept_mgmt.GetSlipNo(CoopID, DeptType, DeptAccNo, StmtRefno, fin.operatedate, fin.Itemamt);
				if SlipNo is not null then
					dept_mgmt.UpdateDeptSlip(CoopID, DeptType, DeptAccNo, StmtRefno, fin.operatedate, fin.Itemamt, PrnBalTemp, StmtDate, LastStmtNo, CalcIntFrom, CalcIntTo, IntAmt, AccIntAmt, retVal);
				end if;
				Pkg_AtmMgmt.Process_AddDeptSTMT(CoopID, LastStmtNo, DeptAccNo, DeptItemType, StmtDate, SlipNo, fin.ItemAmt, PrnBal, PrnBalTemp, PrncNo, IntAmt, AccIntAmt, C_ENTRYID, StmtDate, CalcIntFrom, CalcIntTo, C_CashType, StmtRefno, ChqPendAmt, BranchID, retVal ) ;
				Process_UpdateDeptAmt(CoopID, fin.memno, DeptAccNo, WithDrawTemp, PrnBalTemp, RecvAmt,PayAmt, retVal);
				update atm_trans set isrec = 'Y'  where seqno = fin.seqno;
			elsif fin.transsts = 'N' then
				if fin.operatecd = '002' then
					PrnBalTemp := PrnBal; -- + fin.ItemAmt;
					WithDrawTemp := WithDraw + fin.ItemAmt;
					WithAmt := WithAmt - fin.ItemAmt;
				else
					PrnBalTemp := PrnBal; -- - fin.ItemAmt;
					WithDrawTemp := WithDraw - fin.ItemAmt;
					DeptAmt := DeptAmt - fin.ItemAmt;
				end if;
				Dept_Mgmt.UpdateDeptMaster(CoopID, DeptAccNo, fin.operatedate, fin.operatecd, fin.Itemamt, retVal);
				Process_UpdateDeptAmt(CoopID, fin.memno, DeptAccNo, WithDrawTemp, PrnBalTemp, retVal);
				Dept_Mgmt.Process_UpdateSlipSts(CoopID, DeptType, DeptAccNo, StmtRefno, fin.operatedate, fin.Itemamt, 0, retVal);
				update atm_trans set isrec = 'Y'  where seqno = fin.seqno;
			end if;
			<<CONTINUE>> null;
  		end loop;
  		close c_fin;
 	end;
        Procedure Process_UpdateLoanWithDraw(CoopID varchar2, retVal out integer)	is
	type fin_cursor is REF CURSOR;
	c_fin fin_cursor;
	fin atm_trans%rowtype;
	ContNo atmloan.contract_no%type;
  RecvAmt atmloan.receive_amt%type;
  PayAmt atmloan.pay_amt%type;
	TmpPrnBal		number(15, 2);
	TmpWDAmt		number(15, 2);
	ReGenKpPrnc	number(15, 2);
	ReGenPrnc	number(15, 2);
	ReGenInt		number(15, 2);
	RecvPerd		number(15,2);
	KpTot			number(15,2);
	KpInt			number(15,2);
	KpPrnc		number(15, 2);
	IntAmt		number(15, 2);
	RegenRkeeping	boolean;
	RkeepPrn number(9, 2);
	RkeepInt number(9, 2);
	BranchId	varchar2(6);
	LoanRec	boolean := True;
	LoanType char(2) := pkg_atmmgmt2.C_LoanType;
	LastStmtNo number(5);
	EntryDate	date;
	LoanItemType 	char(3);
	StmtRefno varchar2(15);
	Refno varchar2(15);
	BFPrnBal number(15, 2);
	LastPerdPay number(3);
	PerdPay number(3);
	LoanRecNo	varchar2(13);
	SlipNo varchar2(10);
	PerdPayAmt	number(9, 2);
--	PerdPayMent	number(9, 2);
	StartContDate	date;
	LastCalcIntDate	date;
	LastProcDate date;
	PrnBal number(15, 2);
	WithDraw number(15, 2);
	ContSts	number(15, 2);
	IntArrAmt	number(9, 2);
	CoopBranch varchar2(3) := '001';
	ContIntType number(2);
	NkeepInt number(9, 2);
	PaySts number(2);
	StartKeepDate date;
	PrnPay number(9, 2);
	CalcIntFrom date;
	CalcIntTo date;
	BfPerd number(3);
	StmtDate date;
	BfWithDrawAmt number(15, 2);
	BfBalAmt number(15, 2);
	BfPrnAmt number(15, 2);
	BfLastCalIntDate date;
	BfLastRecDate date;
	BfLastProcDate date;
	SharStkVal number(9, 2);
	IntAcc number(9, 2);
	RecPerdFlag number(2);
	CountDateType		number(2);
	IntRoundNum		number(2);
	IntRoundSumType	number(2);
	IntRoundType		number(2);
	IntRoundFormat	number(2);
	FixPayCalcType		number(2);
	IntRoundKpType	number(2);
	GBfSts				number(2);
	CountDateFstType	number(2);
	IncludeIntArrSts	number(2);
	KpItemArrD			number(2);
	PerdPayAll			number(3);
  SlipTypeCode varchar2(3);
  StmItemType varchar2(3);
   	begin
		open c_fin for select * from atm_trans	where fintype = 'L' and	(ispost <> POSTED or ispost is null) and	(transsts ='W') order by operatedate;
		loop
			fetch c_fin into fin;
			exit when c_fin%notfound;
			fin.memno := trim(fin.memno);
			ContNo := GetContract(CoopID, fin.memno,fin.accno);
			dbms_output.put_line(' ------------ContNo = '||ContNo||' fin.memno='||fin.memno||' fin.accno='||fin.accno);
			if ContNo is null  then
				goto	CONTINUE;
			end if;
			update atmconfig set x = 'N';
--			loan_mgmt.UpdateLoanWithDrawAndAddSlip(CoopID, ContNo, fin.memno, fin.bankcd, substr(fin.accno, 1, 3), fin.accno, C_TOFROMACCID,
--				C_ENTRYID, fin.atmno, fin.atmseqno, fin.operatedate, fin.operatecd, fin.Itemamt, TmpPrnBal, TmpWDAmt, retVal);
--			dbms_output.put_line(' ------------retVal = '||retVal);
--			if retVal <= 0 then
--				goto	CONTINUE;
--			end if;
--			Process_UpdateLoanAmt(CoopID, fin.memno, ContNo, TmpWDAmt, TmpPrnBal, retVal);
--			ContNo := GetContract(CoopID, fin.memno);
			BranchId := C_BranchId;
--			if ContNo is null  then
--				goto	CONTINUE;
--			end if;
--------------------------check Post ?
			if Pkg_AtmMgmt.IsHaveLoanStmt(CoopID, ContNo, BranchId , fin.operatedate) then
				goto	CONTINUE;
			end if;
			retVal := Pkg_AtmMgmt.GetLoanMasterDt(CoopID, ContNo,LoanType, LastStmtNo, PrnBal, WithDraw, ContSts, IntArrAmt, PerdPay,	PerdPayAll, ContIntType, RkeepPrn, RkeepInt, NkeepInt, LastProcDate, LastCalcIntDate, StartContDate, PaySts, PerdPayAmt, StartKeepDate);
			StmtDate := trunc(fin.operatedate);
			StmtRefno := trim(fin.atmno)||trim(fin.atmseqno);
			Refno := substr(trim(fin.atmno), 1, 15);
			BFPrnBal := PrnBal;
			LastStmtNo := LastStmtNo + 1;
			LastPerdPay := PerdPay;
			EntryDate := sysdate;
      RecvAmt:=0;
      PayAmt:=0;
			if (LastStmtNo = 1) or (PrnBal = 0) then
				StartContDate := StmtDate;
				LastProcDate := StmtDate;
				LastCalcIntDate := StmtDate;
			end  if;
			case
				when fin.operatecd =TRANS1 or fin.operatecd =TRANS2  then
					LoanItemType := 'LRC';
          SlipTypeCode := 'LWD';
          StmItemType := 'LRC';
          RecvAmt:=fin.Itemamt;
					PrnBal := PrnBal + fin.Itemamt;
					WithDraw := WithDraw - fin.Itemamt;
					PerdPay := 0;
					dbms_output.put_line(' ------------PrnBal = '||PrnBal||' WithDraw='||WithDraw||' StmtDate='||StmtDate||' RkeepPrn='||RkeepPrn);dbms_output.put_line(' ------------RkeepInt = '||RkeepInt||' IntArrAmt='||IntArrAmt||' LastCalcIntDate='||LastCalcIntDate||' StartContDate='||StartContDate);dbms_output.put_line(' ------------LastProcDate = '||LastProcDate||' PerdPayAmt='||PerdPayAmt);
-------------------CalcInt
					dbms_output.put_line(' ------------ContNo = '||ContNo);
					dbms_output.put_line(' ------------LoanType = '||LoanType);
						IntAmt := loan_mgmt.CalcInt (LoanType, BfPrnBal, fin.Itemamt, CalcIntFrom, CalcIntTo, StmtDate, LastStmtNo, PerdPay, RkeepPrn, RkeepInt, IntArrAmt, LastCalcIntDate, StartContDate, LastProcDate, PerdPayAmt, PerdPayAll, RegenRkeeping) ;
				when fin.operatecd =TRANS3 or fin.operatecd =TRANS4  then
					LoanItemType := 'LPX';
          SlipTypeCode := 'PX';
          StmItemType := 'LPX'; 
          PayAmt:=fin.Itemamt;
					PrnBal := PrnBal - fin.Itemamt;
					WithDraw := WithDraw + fin.Itemamt;
					PerdPay := PerdPay + 1;
			end case;
			if RegenRkeeping and pkg_atmmgmt2.KEEPING_ENABLE then
				ReGenPrnc := PrnBal;
				Pkg_AtmMgmt.Process_GetKPTempRecvDt(CoopID, ContNo, RecvPerd, KpTot, KpPrnc, KpInt, retVal);
					dbms_output.put_line(' ------GetKPTempRecvDt------retVal = '||retVal);
				ReGenKpPrnc := KpTot - RegenInt;
					loan_mgmt.GetInitVal(CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType, GBfSts, CountDateFstType, IntRoundType, FixPayCalcType, IncludeIntArrSts, KpItemArrD);
				RegenInt := loan_mgmt.CalcInt (LoanType, ReGenPrnc, StmtDate, LastProcDate, CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType);
				Pkg_AtmMgmt.Process_UpdateKPTEMPRECEIVEDET(CoopID, ContNo, RecvPerd, ReGenPrnc, ReGenKpPrnc, RegenInt, StmtDate, LastProcDate, LastCalcIntDate, retVal);
				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt, StartContDate, StartKeepDate, 0, LastStmtNo, LastCalcIntDate, StmtDate, LastProcDate, StmtDate, ReGenKpPrnc, RegenInt, ReGenKpPrnc, RegenInt, retVal);
					dbms_output.put_line(' ----Then-----');
			else
					dbms_output.put_line(' ----Else-----');
				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt, StartContDate, StartKeepDate, 0, LastStmtNo, LastCalcIntDate, StmtDate, LastProcDate, StmtDate,null, null, null, null, retVal);
			end if;

			if pkg_atmmgmt.IsUseSlip then
          SlipNo := loan_mgmt.GetSlipNo(CoopID, fin.memno, Refno, fin.operatedate, fin.Itemamt);
          --if SlipNo is null then
				  --pkg_atmmgmt.GenDocNo('CMSLIPNO', 'SL', SlipNo, retVal);
          if SlipNo is null then 
           case
				   when fin.operatecd =TRANS1 or fin.operatecd =TRANS2  then
             pkg_atmmgmt.GenDocNoNew('SLSLIPPAYOUT', 'SL', SlipNo, retVal);
             Pkg_AtmMgmt.Process_AddLoanRec(CoopID, SlipNo,StmtRefno,CoopBranch, fin.MemNo, ContNo, LoanType, BfPerd, BfPrnAmt,CalcIntFrom, BfLastRecDate, CalcIntTo, SharStkVal, IntAcc, StmtDate, fin.itemamt, RecPerdFlag, fin.bankcd, substr(fin.accno, 1, 3),  fin.accno, ContIntType, C_ENTRYID, EntryDate, C_TOFROMACCID, retVal);
           when fin.operatecd =TRANS3 or fin.operatecd =TRANS4  then
             pkg_atmmgmt.GenDocNoNew('SLSLIPPAYIN', 'SL', SlipNo, retVal);
             Pkg_AtmMgmt.Process_AddLoanSlip(CoopID, ContNo, fin.memno , SlipNo,SlipTypeCode, BranchId , Refno ,StmtDate, fin.operatedate, fin.bankcd, substr(fin.accno, 1, 3),  fin.accno , C_TOFROMACCID , fin.Itemamt , C_ENTRYID , EntryDate , retVal );
					   Pkg_AtmMgmt.Process_AddShrLonSlip(CoopID, SlipNo,StmItemType, CoopBranch, 1, LoanType, ContNo, PerdPay, PerdPay, fin.Itemamt, CalcIntFrom, CalcIntTo, BfPerd, LastCalcIntDate, BfWithDrawAmt,	BfBalAmt, ContSts, ContIntType, RkeepPrn, RkeepInt, NkeepInt, CoopBranch, retVal );
           end case;
          end if;
          --Pkg_AtmMgmt.Process_AddLoanSlip(CoopID, fin.memno , SlipNo,SlipTypeCode, BranchId , Refno ,StmtDate, fin.operatedate, fin.bankcd, substr(fin.accno, 1, 3),  fin.accno , C_TOFROMACCID , fin.Itemamt , C_ENTRYID , EntryDate , retVal );
					dbms_output.put_line(' ------------SlipNo = '||SlipNo);
					dbms_output.put_line(' ------------fin.memno = '||fin.memno||'|');
					dbms_output.put_line(' ------------Refno = '||Refno);         
          end if;
				--  SlipNo := loan_mgmt.GetSlipNo(CoopID, fin.memno, Refno, fin.operatedate, fin.Itemamt);
				--if SlipNo is not null then
				--	Pkg_AtmMgmt.Process_AddShrLonSlip(CoopID, SlipNo,StmItemType, CoopBranch, 1, LoanType, ContNo, PerdPay, PerdPay, fin.Itemamt, CalcIntFrom, CalcIntTo, BfPerd, LastCalcIntDate, BfWithDrawAmt,	BfBalAmt, ContSts, ContIntType, RkeepPrn, RkeepInt, NkeepInt, CoopBranch, retVal );
				--end if;
			--end if;
			--if pkg_atmmgmt.IsUseLoanRec and ( fin.operatecd =TRANS1 or fin.operatecd =TRANS2 ) then ---LoanRec
			--	--pkg_atmmgmt.GenDocNo('LNRECEIVENO', 'RC', LoanRecNo, retVal);
      --  if LoanRecNo is null then pkg_atmmgmt.GenDocNoNew('LNRECEIVENO', 'RC', LoanRecNo, retVal);end if;
			--		dbms_output.put_line(' ------------LoanRecNo = '||LoanRecNo);
			--	Pkg_AtmMgmt.Process_AddLoanRec(CoopID, LoanRecNo, CoopBranch, fin.MemNo, ContNo, LoanType, BfPerd, BfPrnAmt,BfLastCalIntDate, BfLastRecDate, BfLastProcDate, SharStkVal, IntAcc, StmtDate, fin.itemamt, RecPerdFlag, fin.bankcd, substr(fin.accno, 1, 3),  fin.accno, ContIntType, C_ENTRYID, EntryDate, C_TOFROMACCID, retVal);
			--end if;
			Pkg_AtmMgmt.Process_AddLoanSTMT(CoopID, ContNo, LastStmtNo, CoopBranch, LoanItemType, StmtDate, StmtRefno, PerdPay, fin.Itemamt, PrnBal, CalcIntFrom, CalcIntTo, IntAmt, IntArrAmt, C_CASHTYPE, 1, C_ENTRYID, sysdate, CoopBranch, LoanRecNo, retVal) ;
--			Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, WithDraw, PrnBal, retVal);
			Process_UpdateLoanAmt(CoopID, fin.memno, ContNo, WithDraw, PrnBal, RecvAmt,PayAmt, retVal);
--			update atm_trans set isrec = 'Y'  where seqno = fin.seqno;
			dbms_output.put_line(' retVal '||retVal);
			dbms_output.put_line(' Update LnContMaster  : '||WithDraw||'|'||retVal||'| PrnBal ='||PrnBal);
			update atmconfig set x = 'Y';
			Process_SetTransPosted(fin.seqno, retVal); dbms_output.put_line(' Posted : |'||retVal||'|');
			<<CONTINUE>> null;
 		end loop;
  		close c_fin;
 	end;
        Procedure Process_UpdateDeptWithDraw(CoopID varchar2, retVal out integer)	is
	type fin_cursor is REF CURSOR;
	c_fin 				fin_cursor;
	fin 				atm_trans%rowtype;  
  RecvAmt atmdept.receive_amt%type;
  PayAmt atmdept.pay_amt%type;
	WithDrawAmt 	number(17, 5);
	SlipNo	varchar2(10);	
  DeptAccNo 	varchar2(10);	
  StmtRefNo 	varchar2(20);
	StmtDate		date; 	Refno varchar2(15);
  LastCalcIntDate date;
  DeptItemType	varchar2(3);
  AccIntAmt number(12, 5);
  PrnBalTemp	number(17, 5);	
  WithDrawTemp number(17, 5);	
  DeptAmt number(17, 5);	
  CalcIntFrom date; 
  CalcIntTo date;	
  IntAmt number(12,5);
  PrnBal	number(17, 5);	
  WithDraw number(17, 5);
  WithAmt number(17, 5);
  DOperateBal number (17,5) := 0;	
  LastStmtNo number(5); 
  BranchId varchar2(6);	
  PrncNo number;
  LastAccDate date;	
  DeptType varchar2(2);
  LastMoveDate date;
  DeptGrpCD varchar2(2) := '00';
  ChqPendAmt number(17, 5);
  BranchOrg varchar2(3) := C_BranchId;	
  c integer;	
  DeptCloseSts number(1);	
  SequsetSts number(1);	
  IntArrAmt number(12, 5);	
  SpcIntRate number(12, 5); 
  SpcIntRateSts number(1);
------------------------------------------------Calculate Interest
	CalcIntRound	integer;MinCalcInt	number(2);
	begin
		open c_fin for select * from atm_trans where fintype = 'D' and	(ispost <> POSTED or ispost is null) and	(itemamt > 0) and	(transsts = 'W') order by operatedate;
		loop
			fetch c_fin into fin;
			exit when c_fin%notfound;
			DeptAccNo := GetDept(CoopID, fin.memno,fin.accno);
      DeptType :=  GetDeptType(CoopID, fin.memno,fin.accno);
			dbms_output.put_line(' ------DeptAccNo = '||DeptAccNo);
			dbms_output.put_line(' ------CoopID = '||CoopID);
			dbms_output.put_line(' ------fin.memno = '||fin.memno||'|');
			if DeptAccNo is null  then
				goto	CONTINUE;
			end if;
			update atmconfig set x = 'N';
--			Dept_Mgmt.UpdateDeptWithDrawAndAddSlip(CoopID, DeptAccNo, C_TOFROMACCID, C_ENTRYID, fin.atmno, fin.atmseqno, C_CashType, fin.operatedate, fin.operatecd, fin.Itemamt, PrnBal, WithDrawAmt, retVal);
--			if retVal <= 0 then
--				goto	CONTINUE;
--			end if;
--			Process_UpdateDeptAmt(CoopID, fin.memno, DeptAccNo, WithDrawAmt, PrnBal, retVal);
			if Pkg_AtmMgmt.IsHaveDeptStmt(CoopID, DeptAccNo, DeptType, fin.operatedate) then
				goto	CONTINUE;
			end if;
			retVal := pkg_atmmgmt.GetDeptMasterDt(CoopID, DeptAccNo, DeptType, LastStmtNo, PrnBal, WithDraw, DeptCloseSts, SequsetSts, LastCalcIntDate, AccIntAmt, IntArrAmt, SpcIntRate, SpcIntRateSts, ChqPendAmt, DeptAmt, WithAmt, BranchId);
--------------------------check A/C Closed --
			dbms_output.put_line(' ------GetDeptMasterDt------retVal = '||retVal);
			if DeptCloseSts = 1  then
				goto	CONTINUE;
			end if;
			StmtDate := trunc(fin.operatedate);
			StmtRefno := trim(fin.atmno)||trim(fin.atmseqno);
			Refno := substr(trim(StmtRefno), 1, 15);
			DOperateBal := DOperateBal + fin.ItemAmt;
			LastStmtNo := LastStmtNo + 1;
      RecvAmt:=0;
      PayAmt:=0;
--------------------------Calc Interest
			if LastCalcIntDate > StmtDate then
				CalcIntFrom := StmtDate;
				CalcIntTo := LastCalcIntDate;
				IntAmt := Dept_Mgmt.CalcInt(DeptType, fin.ItemAmt, CalcIntFrom, CalcIntTo);
			else
				CalcIntFrom := LastCalcIntDate;
				CalcIntTo := StmtDate;
				IntAmt := Dept_Mgmt.CalcInt(DeptType, PrnBal, CalcIntFrom, CalcIntTo, SpcIntRateSts, SpcIntRate, ChqPendAmt);
			end if;
			if fin.operatecd = TRANS1 then
				DeptItemType := 'WTE';
				AccIntAmt := AccIntAmt + IntAmt;
				PrnBalTemp := PrnBal - fin.ItemAmt ;
				WithDrawTemp := WithDraw - fin.ItemAmt;
				WithAmt := WithAmt + fin.ItemAmt;
        RecvAmt:=fin.ItemAmt;
			else
				DeptItemType := 'DTE';
				AccIntAmt := AccIntAmt + IntAmt;
				PrnBalTemp := PrnBal + fin.ItemAmt;
				WithDrawTemp := WithDraw + fin.ItemAmt;
				DeptAmt := DeptAmt + fin.ItemAmt;
        PayAmt:=fin.ItemAmt;
			end if;
			PrncNo := 0;
			LastMoveDate := StmtDate;
			LastAccDate := StmtDate;
			dbms_output.put_line(' ----CalcIntFrom='||CalcIntFrom||' CalcIntTo='||CalcIntTo);
			dbms_output.put_line(' ------------IntAmt = '||IntAmt);
      --pkg_atmmgmt.GenDocNo('DPSLIPNO', '', SlipNo, retVal);
      if SlipNo is null then pkg_atmmgmt.GenDocNoNew('DPSLIPNO', '', SlipNo, retVal);end if;
      Pkg_AtmMgmt.Process_AddDeptSlip(CoopID, SlipNo, DeptAccNo, DeptType, StmtDate, DeptItemType, fin.ItemAmt, PrnBalTemp, WithDrawTemp,
				C_ENTRYID, StmtDate, StmtRefno, DeptItemType, LastStmtNo, CalcIntFrom, CalcIntTo, DeptGrpCD, C_TOFROMACCID, 'DEP', '', C_CashType, IntAmt, AccIntAmt, BranchId, BranchOrg, retVal);
			Pkg_AtmMgmt.Process_AddDeptSTMT(CoopID, LastStmtNo, DeptAccNo, DeptItemType, StmtDate, SlipNo, fin.ItemAmt, PrnBal, PrnBalTemp, PrncNo, IntAmt, AccIntAmt, C_ENTRYID, StmtDate, CalcIntFrom, CalcIntTo, C_CashType, StmtRefno, ChqPendAmt, BranchID, retVal ) ;
			Pkg_AtmMgmt.Process_UpdateDeptMaster(CoopID, DeptAccNo, BranchId, PrnBalTemp, WithDrawTemp, AccIntAmt,CalcIntTo, LastAccDate, LastStmtNo, DOperateBal, LastMoveDate, DeptAmt, WithAmt, retVal );
			
--			DOperateBal := DOperateBal + fin.ItemAmt;
--			dbms_output.put_line(' ----PrnBalTemp='||PrnBalTemp);dbms_output.put_line(' ----WithDrawTemp='||WithDrawTemp);dbms_output.put_line(' ----DOperateBal='||DOperateBal);	dbms_output.put_line(' ----CalcIntTo='||CalcIntTo);
--			Pkg_AtmMgmt.Process_UpdateDeptMaster(CoopID, DeptAccNo, BranchId, PrnBalTemp, WithDrawTemp, AccIntAmt, LastCalcIntDate, LastAccDate, LastStmtNo, DOperateBal, LastMoveDate, DeptAmt, WithAmt, retVal );
--			dbms_output.put_line(' ----DeptType='||DeptType);
/*			SlipNo := dept_mgmt.GetSlipNo(CoopID, DeptType, DeptAccNo, StmtRefno, fin.operatedate, fin.Itemamt);
			dbms_output.put_line(' ----SlipNo='||SlipNo);
			if SlipNo is not null then
				dept_mgmt.UpdateDeptSlip(CoopID, DeptType, DeptAccNo, StmtRefno, fin.operatedate, fin.Itemamt, PrnBalTemp, StmtDate, LastStmtNo, CalcIntFrom, CalcIntTo, IntAmt, AccIntAmt, retVal);
			end if; */
--			Dept_Mgmt.UpdateDeptWithDrawAndAddSlip(CoopID, DeptAccNo, C_TOFROMACCID, C_ENTRYID, fin.atmno, fin.atmseqno, C_CashType, fin.operatedate, fin.operatecd, fin.Itemamt, PrnBal, WithDrawAmt, retVal);
--			dbms_output.put_line(' ------------DeptAccNo = '||DeptAccNo||' WithDrawTemp='||WithDrawTemp||' PrnBalTemp='||PrnBalTemp||' fin.Itemamt='||fin.Itemamt);
			Process_UpdateDeptAmt(CoopID, fin.memno, DeptAccNo, WithDrawTemp, PrnBalTemp, RecvAmt,PayAmt, retVal);
--			update atm_trans set isrec = 'Y'  where seqno = fin.seqno;
			update atmconfig set x = 'Y';
			Process_SetTransPosted(fin.seqno, retVal); dbms_output.put_line(' Posted : |'||retVal||'|');
			<<CONTINUE>> null;
  		end loop;
  		close c_fin;
 	end;

        Procedure ReconcileLoan(CoopID varchar2)	is
	type fin_cursor is REF CURSOR;
	c_fin fin_cursor;
	fin atm_trans%rowtype;
	ContNo varchar2(10);
	PrnBal number(15, 2);
	WithDraw number(15, 2);
	LastStmtNo number(5);
	LoanType char(2);
	ContSts	number(15, 2);
	IntArrAmt	number(9, 2);
	IntPerd	number(9, 2);
	PerdPayAmt	number(9, 2);
	PerdPayAll			number(3);
	ContIntType number(2);
	RkeepPrn number(9, 2);
	RkeepInt number(9, 2);
	NkeepInt number(9, 2);
	LastProcDate date;
	LastCalcIntDate date;
	StartContDate date;
	StartKeepDate	date;
	PaySts number(2);
	EntryDate	date;
	PerdPay number(3);
	StmtDate date;
	ReGenKpPrnc	number(15, 2);
	ReGenPrnc	number(15, 2);
	ReGenInt		number(15, 2);
	RecvPerd		number(15,2);
	KpTot			number(15,2);
	KpInt			number(15,2);
	KpPrnc		number(15, 2);
	CalcIntFrom	date;
	BranchId	varchar2(6) := '001';
	LoanItemType	char(3);
  LoanItemTypeCancel char(3);
	Refno varchar2(15);
	CalcIntTo	date;
	CoopBranch varchar2(3) := '001';
	LoanRecNo	varchar2(10);
	begin
		open c_fin for
		select 	*
		from 		atm_trans
		where 	fintype = 'L' and
					(transsts<>'W') and
					(ispost = 'Y') and
					(isrec is null or trim(isrec) = '') and
					itemamt > 0
		order by operatedate;
		loop
			fetch c_fin into fin;
			exit when c_fin%notfound;
			ContNo := GetContract(CoopID, fin.memno);
			if ContNo is null  then
				goto	CONTINUE;
			end if;
			if fin.transsts = 'N' then
				retVal := Pkg_AtmMgmt.GetLoanMasterDt(CoopID, ContNo, LoanType, LastStmtNo, PrnBal, WithDraw, ContSts, IntArrAmt, PerdPay,	PerdPayAll, ContIntType, RkeepPrn, RkeepInt, NkeepInt, LastProcDate, LastCalcIntDate, StartContDate, PaySts, PerdPayAmt, StartKeepDate);
				case
					when fin.operatecd =TRANS1 or fin.operatecd =TRANS2  then
						LoanItemType := 'LRC';
            LoanItemTypeCancel := 'RRC';
						PrnBal := PrnBal - fin.Itemamt;
						WithDraw := WithDraw + fin.Itemamt;
					when fin.operatecd =TRANS3 or fin.operatecd =TRANS4  then
						LoanItemType := 'LPX';
            LoanItemTypeCancel := 'RPX';
						PrnBal := PrnBal + fin.Itemamt;
						WithDraw := WithDraw - fin.Itemamt;
				end case;
				StmtDate := trunc(fin.operatedate);
				Refno := trim(fin.atmno)||trim(fin.atmseqno);
				LastStmtNo := LastStmtNo + 1;
				EntryDate := sysdate;
				Pkg_AtmMgmt.CancelLoanSTMT(CoopID, ContNo, LastStmtNo, BranchId, LoanItemTypeCancel, StmtDate, Refno, PerdPay,  fin.Itemamt, PrnBal, CalcIntFrom, CalcIntTo, IntPerd, IntArrAmt, C_CASHTYPE, -9, C_ENTRYID, EntryDate, CoopBranch, LoanRecNo, retVal) ;
				CalcIntTo := CalcIntFrom;
					dbms_output.put_line(' ------------retVal = '||retVal);
					dbms_output.put_line(' ------------LoanRecNo = '||LoanRecNo||'|');
					dbms_output.put_line(' ------------CalcIntFrom = '||CalcIntFrom||'|');
					dbms_output.put_line(' ------------CalcIntTo = '||CalcIntTo||'|');
				if retVal <= 0 then
					goto	CONTINUE;
				else
					dbms_output.put_line(' ------------LastStmtNo = '||LastStmtNo);
					dbms_output.put_line(' ------------ContNo = '||ContNo);
					dbms_output.put_line(' ------------Refno = '||Refno);
					dbms_output.put_line(' ------------retVal = '||retVal);
				end if;
				Pkg_AtmMgmt.CancelLoanRec(CoopID,LoanItemType, Refno, fin.recondate, retVal) ;
					dbms_output.put_line(' ----CancelLoanRec--------retVal = '||retVal);
				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt - IntPerd, StartContDate, StartKeepDate, 0, LastStmtNo, CalcIntFrom, StmtDate, LastProcDate, StmtDate, ReGenKpPrnc, RegenInt, ReGenKpPrnc, RegenInt, retVal);
--				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, WithDraw, PrnBal, retVal);
--				Loan_Mgmt.Process_UpdateSlipSts(CoopID, fin.memno, Refno, fin.operatedate, fin.Itemamt, 0, retVal);
				Process_UpdateLoanAmt(CoopID, fin.memno, ContNo, WithDraw, PrnBal, retVal);
			end if;
			update atm_trans set isrec = 'Y'  where seqno = fin.seqno;
			<<CONTINUE>> null;
  		end loop;
  		close c_fin;
 	end;

   Procedure ReconcileDept(CoopID varchar2)	is
	type fin_cursor is REF CURSOR;
	c_fin fin_cursor;
	fin atm_trans%rowtype; 
  RecvAmt atmdept.receive_amt%type;
  PayAmt atmdept.pay_amt%type;
	SlipNo	varchar2(10);	DeptAccNo 	varchar2(10);	
  StmtRefNo 	varchar2(20);	EntryDate date;
	StmtDate		date; 	
  Refno varchar2(15);
  LastCalcIntDate date;
  DeptItemType	char(3);
  AccIntAmt number(12, 5);
  PrnBalTemp	number(17, 5);	
  WithDrawTemp number(17, 5);	
  DeptAmt number(17, 5);	
  CalcIntFrom date; 
  CalcIntTo date;	
  IntAmt number(12,5);
  PrnBal	number(17, 5);	
  WithDraw number(17, 5);
  WithAmt number(17, 5);
  DOperateBal number (17,5) := 0;	
  LastStmtNo number(5); 
  BranchId varchar2(6);	
  PrncNo number;
  LastAccDate date;	
  DeptType varchar2(2);
  LastMoveDate date;
  DeptGrpCD varchar2(2) := '00';
  ChqPendAmt number(17, 5);
  BranchOrg varchar2(3) := '001';	
  c integer;	
  DeptCloseSts number(1);	
  SequsetSts number(1);	
  IntArrAmt number(12, 5);	
  SpcIntRate number(12, 5); 
  SpcIntRateSts number(1);
	begin
		open c_fin for
		select 	*
		from 		atm_trans
		where 	fintype = 'D' and
					(transsts<>'W') and
					(ispost = 'Y') and
					(isrec is null or trim(isrec) = '') and
					itemamt > 0
		order by operatedate;
		loop
			fetch c_fin into fin;
			exit when c_fin%notfound;
			DeptAccNo := GetDept(CoopID, fin.memno,fin.accno);
			if DeptAccNo is null  then
				goto	CONTINUE;
			end if;
      RecvAmt:=0;PayAmt:=0;
			if Pkg_AtmMgmt.IsHaveDeptStmt(CoopID, DeptAccNo, DeptType, fin.operatedate) then
				goto	CONTINUE;
			end if;
			if fin.transsts = 'N' then
				retVal := pkg_atmmgmt.GetDeptMasterDt(CoopID, DeptAccNo, DeptType, LastStmtNo, PrnBal, WithDraw, DeptCloseSts, SequsetSts, LastCalcIntDate, AccIntAmt, IntArrAmt, SpcIntRate, SpcIntRateSts, ChqPendAmt, DeptAmt, WithAmt, BranchId);
				if fin.operatecd = TRANS1 then
          DeptItemType :='AJI';
					PrnBalTemp := PrnBal + fin.ItemAmt;
					WithDrawTemp := WithDraw + fin.ItemAmt;
					WithAmt := WithAmt - fin.ItemAmt;
          RecvAmt:=fin.ItemAmt;
				else
          DeptItemType :='AJO';
					PrnBalTemp := PrnBal - fin.ItemAmt;
					WithDrawTemp := WithDraw - fin.ItemAmt;
					DeptAmt := DeptAmt - fin.ItemAmt;
          PayAmt:=fin.ItemAmt;
				end if;
				StmtDate := trunc(fin.operatedate);
				Refno := trim(fin.atmno)||trim(fin.atmseqno);
				LastStmtNo := LastStmtNo + 1;
				EntryDate := sysdate;
					dbms_output.put_line(' ------------retVal = '||retVal);
					dbms_output.put_line(' ------------PrnBalTemp = '||PrnBalTemp||'|');
					dbms_output.put_line(' ------------WithDrawTemp = '||WithDrawTemp||'|');
					dbms_output.put_line(' ------------DeptAmt = '||DeptAmt||'|');
          Pkg_AtmMgmt.Process_AddDeptSTMT(CoopID, LastStmtNo, DeptAccNo, DeptItemType, StmtDate, Refno, fin.ItemAmt, PrnBal, PrnBalTemp, PrncNo, IntAmt, AccIntAmt, C_ENTRYID, StmtDate, CalcIntFrom, CalcIntTo, C_CashType, StmtRefno, ChqPendAmt, BranchID, retVal ) ;			
/*				Pkg_AtmMgmt.CancelLoanSTMT(CoopID, ContNo, LastStmtNo, BranchId, 'RRC', StmtDate, Refno, PerdPay,  fin.Itemamt, PrnBal, CalcIntFrom, CalcIntTo, IntPerd, IntArrAmt, C_CASHTYPE, -9, C_ENTRYID, EntryDate, CoopBranch, LoanRecNo, retVal) ;
				CalcIntTo := CalcIntFrom;
					dbms_output.put_line(' ------------retVal = '||retVal);
					dbms_output.put_line(' ------------LoanRecNo = '||LoanRecNo||'|');
					dbms_output.put_line(' ------------CalcIntFrom = '||CalcIntFrom||'|');
					dbms_output.put_line(' ------------CalcIntTo = '||CalcIntTo||'|');
				if retVal <= 0 then
					goto	CONTINUE;
				else
					dbms_output.put_line(' ------------LastStmtNo = '||LastStmtNo);
					dbms_output.put_line(' ------------ContNo = '||ContNo);
					dbms_output.put_line(' ------------Refno = '||Refno);
					dbms_output.put_line(' ------------retVal = '||retVal);
				end if; */
				Pkg_AtmMgmt.Process_UpdateDeptMaster(CoopID, DeptAccNo, BranchId, PrnBalTemp, WithDrawTemp, AccIntAmt,LastCalcIntDate, LastAccDate, LastStmtNo, DOperateBal, LastMoveDate, DeptAmt, WithAmt, retVal );
					dbms_output.put_line(' ------------retVal = '||retVal);
--				Pkg_AtmMgmt.CancelLoanRec(CoopID, LoanRecNo, fin.recondate, retVal) ;
					dbms_output.put_line(' ----CancelLoanRec--------retVal = '||retVal);
--				Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt - IntPerd, StartContDate, StartKeepDate, 0, LastStmtNo, CalcIntFrom, StmtDate, LastProcDate, StmtDate, ReGenKpPrnc, RegenInt, ReGenKpPrnc, RegenInt, retVal);
				Process_UpdateDeptAmt(CoopID, fin.memno, DeptAccNo, WithDrawTemp, PrnBalTemp, RecvAmt,PayAmt, retVal);
			end if;
			update atm_trans set isrec = 'Y'  where seqno = fin.seqno;
			<<CONTINUE>> null;
  		end loop;
  		close c_fin;
 	end;

        Procedure Post2ATMTrans is
	type fin_cursor is REF CURSOR;
	c_fin 				fin_cursor;
	fin 				atmtransaction%rowtype;
	begin
		open c_fin for select * from atmtransaction  where (post_status = 0) and (item_amt >= 0) order by operate_date;
		loop
			fetch c_fin into fin;
			exit when c_fin%notfound;
				dbms_output.put_line(fin.operate_date||' '||fin.member_no);
			INSERT INTO ATM_TRANS  ( COOP_ID, SEQNO, OPERATEDATE, MEMNO, FINTYPE, OPERATECD, SYSTEMCD, BANKCD,
			BRANCHCD, ATMNO, ATMSEQNO, ACCNO, ITEMAMT, stampdate, TRANSSTS)  values
			(C_COOPID, ATM_TRANS_SEQ.NEXTVAL, fin.operate_date, fin.member_no,
			decode(fin.system_code, '01', 'L', '02', 'D'), fin.operate_code, fin.system_code,
			fin.bank_code, fin.branch_code, fin.atm_no, fin.atm_seqno, fin.saving_acc,
			fin.item_amt, sysdate, decode(fin.item_status, 1, 'Y', -1, 'N', 'W'));
				dbms_output.put_line('SQL%ROWCOUNT = '||SQL%ROWCOUNT);
			if SQL%ROWCOUNT > 0 then
				update atmtransaction set post_status = 1, post_date = sysdate
				where trim(member_no) = trim(fin.member_no) and
						coop_id = C_COOPID and
						ccs_operate_date = fin.ccs_operate_date;
			end if;
  		end loop;
  		close c_fin;
      
      --Clear sequest_amt
      update atmdept a set a.sequest_amt=0 where a.depttype_code='50' 
      and (select count(*) from atmtransaction 
           where saving_acc=a.saving_acc and item_status>=0 
           and operate_code='002' and system_code='02' 
           and to_char(operate_date,'yyyymm')=to_char(sysdate,'yyyymm') )=0;
           
      update atmdept a set a.sequest_amt=999999999 where a.depttype_code='50' 
      and (select count(*) from atmtransaction 
           where saving_acc=a.saving_acc and item_status>=0 
           and operate_code='002' and system_code='02' 
           and to_char(operate_date,'yyyymm')=to_char(sysdate,'yyyymm') )>0;
		        
      -- Hold when found multi saving account mapping cross member_no 
      update  atmloan set loanhold=1 where trim(saving_acc) in (
      select trim(saving_acc) from 
      ( select count(member_no) as cnt ,saving_acc
        from atmloan where loanhold=0  and post_flag<>'D' and  
         saving_acc is not null and trim(saving_acc) not like '%0000000000%'  
        group by  saving_acc  ) 
      where cnt >1
      )
      ;   
      
      -- Hold when found multi saving account mapping cross member_no 
      update  atmdept set depthold=1 where trim(saving_acc) in (
      select trim(saving_acc) from 
      ( select count(member_no) as cnt ,saving_acc
        from atmdept where depthold=0  and post_flag<>'D' and  
         saving_acc is not null and trim(saving_acc) not like '%0000000000%'  
        group by  saving_acc  ) 
      where cnt >1
      )
      ;
      
           
end;
End PKG_ATMMGMT2;

/
