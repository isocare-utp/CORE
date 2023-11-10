--------------------------------------------------------
--  File created - Thursday-August-04-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package Body PKG_ATMMGMT
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "PKG_ATMMGMT" AS
        retVal integer;   row_count number;
        Function x(ContNo varchar2) return varchar2
	is
	begin return '??'||substr(ContNo, 3, length(ContNo));
	end;
        Function IsLinkValid(SLink varchar2) return boolean
	is
	db_connect number(1);
	begin
		db_connect := 1;
		begin 	EXECUTE IMMEDIATE  'select 1 from dual@'||SLink;
		EXCEPTION	WHEN OTHERS THEN db_connect := 0;
		end; return (db_connect = 1);
	end;
        Function IsUseLoanRec return boolean
	is
	begin
		return false;
	end;
        Function IsUseSlip return boolean
	is
	begin
		return true;
	end;

  Procedure getDeptCoop_ID(CoopAcc IN varchar2 Default '',Coop_ID OUT VARCHAR2 )
  is
  begin
   select coop_id into Coop_ID from dpdeptmaster where trim(deptaccount_no)=trim(CoopAcc);
  end;
  Procedure getDeptType(CoopAcc IN varchar2 Default '',DeptType OUT VARCHAR2 )
  is
  begin
   select depttype_code into DeptType from dpdeptmaster where trim(deptaccount_no)=trim(CoopAcc);
  end;
  Procedure getLoanCoop_ID(ContNo IN varchar2 Default '',Coop_ID OUT VARCHAR2 )
  is
  begin
   select coop_id into Coop_ID from lncontmaster where trim(loancontract_no)=trim(ContNo);
  end;
  Procedure getLoanType(ContNo IN varchar2 Default '',LoanType OUT VARCHAR2 )
  is
  begin
   select loantype_code into LoanType from lncontmaster where trim(loancontract_no)=trim(ContNo);
  end;
        Procedure GenDocNoNew(Doccd varchar2, DocPF IN varchar2 Default '', LastDocNo OUT varchar2, retVal OUT INTEGER )
	is
	DocYear		cmdocumentcontrol.document_year%type;
  BranchId		varchar2(3) := ''; --'001';
	LastNo		number;
	begin
		select	to_number(last_documentno) + 1, document_year into	LastNo, DocYear from	cmdocumentcontrol	where	(trim(document_code) = Doccd )
		FOR UPDATE;
		update 	cmdocumentcontrol
		set 	last_documentno = to_char(LastNo)
		where	(trim(document_code) = Doccd );
		LastDocNo := to_char(LastNo); LastDocNo := DocPF||substr(DocYear, 3, 2)||BranchId||substr( lpad(trim(LastDocNo), 10, '0'),  -(10- length(DocPF||substr(DocYear, 3, 2)||BranchId)), 10- length(DocPF||substr(DocYear, 3, 2)||BranchId));
	exception
 	  when no_data_found then 	null;
	end;
        Procedure Process_SetTransPosted(aSeqNo number, retVal OUT INTEGER)	is
	begin
		update atm_fin_amt set ispost = POSTED, postdate = sysdate  where seqno = aSeqNo;
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_Move2Success is
	begin
		insert into atm_post_scss (seqno, refno, fintype, accno, itemtype, itemamt, operatedate, memno, memnm, memsnm, personid, balamt, ispost, postdate)
		select seqno, refno, fintype, accno, itemtype, itemamt, operatedate, memno, memnm, memsnm, personid, balamt, ispost, postdate
		from atm_fin_amt
		where ispost = POSTED;
		delete from atm_fin_amt where ispost = POSTED;
	end;
        Procedure GetMember(CoopID varchar2, MemNo varchar2, MemNm OUT varchar2, MemSurNm OUT varchar2, PersonID OUT varchar2, retVal OUT INTEGER ) is
	begin
		select MEMB_NAME, MEMB_SURNAME, CARD_PERSON
		into 	MemNm, MemSurNm, PersonID
		from 	mbmembmaster
		where --coop_id = CoopID and
				trim(member_no) = trim(memno) ;
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_AddLoanSlip(CoopID varchar2,ContNo varchar2, MemNo varchar2, SlipNo varchar2,SlipTypeCode varchar2, BranchId varchar2, RefNo varchar2, SlipDate date, OperateDate date, ExpBank varchar2, ExpBranch varchar2, ExpAccID varchar2, ToFromAccId varchar2, SlipAmt number, EntryId varchar2, EntryDate date, retVal OUT INTEGER)
	is
  Coop_id LNCONTMASTER.coop_id%type;
	begin
  
    select Coop_id into Coop_id from LNCONTMASTER WHERE 	trim(LOANCONTRACT_NO) = trim(ContNo);
  
     insert into slslippayin (
     COOP_ID,PAYINSLIP_NO,MEMCOOP_ID,MEMBER_NO,SLIPTYPE_CODE,SLIP_DATE,OPERATE_DATE,
     SHARESTKBF_VALUE,SHARESTK_VALUE,INTACCUM_AMT,MONEYTYPE_CODE,
     FINBANKACC_NO,EXPENSE_BANK,EXPENSE_BRANCH,ACCID_FLAG,
     TOFROM_ACCID,SLIP_AMT,SLIP_STATUS,MEMBGROUP_CODE,
     ENTRY_ID,ENTRY_DATE,ENTRY_BYCOOPID,FINPOST_STATUS,POSTTOVC_FLAG,POST_TOFIN,REF_DOCNO 
     )values (
     Coop_id,SlipNo,(select coop_id from mbmembmaster where trim(member_no)=trim(MemNo)),trim(MemNo),SlipTypeCode,trunc(SlipDate),OperateDate,
     0,0,0,'CBT',
     ExpAccID,ExpBank,ExpBranch,1,
     ToFromAccId,SlipAmt,1,(select MEMBGROUP_CODE from mbmembmaster where trim(member_no)=trim(MemNo)),
     EntryId,EntryDate,Coop_id,0,0,0,RefNo
     );
   
	--	INSERT INTO CMSHRLONSLIP
	--		( SLIP_NO, BRANCH_ID, MEMBER_NO, DOCUMENT_NO, SLIPTYPE_CODE, SLIP_DATE, OPERATE_DATE, SHARESTK_VALUE,
	--		ACCUM_INTEREST, MONEYTYPE_CODE, EXPENSE_BANK, EXPENSE_BRANCH, EXPENSE_ACCID, ACCID_FLAG, TOFROM_ACCID,
	--		SLIP_AMT, SLIP_STATUS, ENTRY_ID, ENTRY_DATE, MBBRANCH_ID )
	--	VALUES
	--		( SlipNo, BranchId, MemNo, RefNo, SlipTypeCode, trunc(SlipDate), OperateDate, 0, 0, 
  --'CBT', ExpBank, ExpBranch, ExpAccID, 1, ToFromAccId, SlipAmt, 1, EntryId, EntryDate, '001' )  ;
	--
   retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_AddShrLonSlip(CoopID varchar2, SlipNo varchar2,StmItemType varchar2, BranchId varchar2, Seqno number, ShrLonTypeCD varchar2, ContNo varchar2, PerdCountSts number, Perd number, PrinPay number, CalIntFrom date, CalIntTo date, BfPerd 	number, BfLastCalIntDate date, BfWithDrawAmt number, BfBalAmt number, BfContSts number, ContIntType number, RkeepPrin number, RkeepInt number, NkeepInt number, LnBranchId varchar2, retVal OUT INTEGER ) 
        is
         Coop_id LNCONTMASTER.coop_id%type;
	begin
    select Coop_id into Coop_id from LNCONTMASTER WHERE 	trim(LOANCONTRACT_NO) = trim(ContNo);
  
    insert into slslippayindet ( 
     COOP_ID,PAYINSLIP_NO,SLIPITEMTYPE_CODE,SEQ_NO,OPERATE_FLAG,SHRLONTYPE_CODE,CONCOOP_ID,
     LOANCONTRACT_NO,SLIPITEM_DESC,PERIODCOUNT_FLAG,PERIOD,PRINCIPAL_PAYAMT,
     INTEREST_PAYAMT,INTARREAR_PAYAMT,ITEM_PAYAMT,ITEM_BALANCE,PRNCALINT_AMT,
     CALINT_FROM,CALINT_TO,INTEREST_PERIOD,INTEREST_RETURN,STM_ITEMTYPE,
     BFPERIOD,BFINTARR_AMT,BFINTARRSET_AMT,BFLASTCALINT_DATE,
     BFLASTPROC_DATE,BFLASTPAY_DATE,BFWITHDRAW_AMT,
     BFPERIOD_PAYMENT,BFSHRCONT_BALAMT,BFSHRCONT_STATUS,BFCONTLAW_STATUS,BFCOUNTPAY_FLAG,BFPAYSPEC_METHOD,
     BFINTRETURN_FLAG,BFINTRETURN_AMT,BFPXAFTERMTHKEEP_TYPE,REF_DOCNO,PRNC_RETURN
     )values(
      Coop_id,SlipNo,StmItemType,Seqno,1,ShrLonTypeCD,Coop_id,
     ContNo,'?????????',PerdCountSts,Perd,PrinPay,
     0,0,PrinPay,PrinPay,0,
     CalIntFrom,CalIntTo,0,0,StmItemType,
     BfPerd,0,0,BfLastCalIntDate,
     BfLastCalIntDate,BfLastCalIntDate,BfWithDrawAmt,
     0,0,0,0,0,0,
     0,0,0,'',0
     );
  
	--	INSERT INTO CMSHRLONSLIPDET
	--		( SLIP_NO, SLIPITEMTYPE_CODE, SEQ_NO, BRANCH_ID, SHRLONTYPE_CODE, OPERATE_FLAG, LOANCONTRACT_NO, SLIPITEM_DESC, PERIODCOUNT_STATUS,
	--		PERIOD, PRINCIPAL_PAYAMT, INTEREST_PAYAMT, INTARREAR_PAYAMT, ITEM_PAYAMT, ITEM_BALANCE, CALINT_FROM, CALINT_TO, INTEREST_PERIOD,
	--		INTARREAR_PERIOD, INTEREST_RETURN, INTEREST_NORMAL, STM_ITEMTYPE, SHRLON_TYPE, BFPERIOD, BFINTARR_AMT, BFINTMTHARR_AMT,
	--		BFINTYEARARR_AMT, BFLASTCALINT_DATE,   BFLASTPROC_DATE,   BFLASTPAY_DATE,
	--		BFWITHDRAW_AMT, BFBALANCE_AMT, BFPBPRINBAL_AMT, BFPBINTBAL_AMT,
	--		BFCONTRACT_STATUS, BFPROBLEM_FLAG, CONTRACTINT_TYPE,
	--		RKEEP_PRINCIPAL,   RKEEP_INTEREST,   NKEEP_INTEREST,   DISPLAYONLY_FLAG,   SLIPITEM_STATUS,
	--		CANCEL_ID,   CANCEL_DATE,   POSTTOVC_FLAG,   VOUCHER_NO,   CANCELTOVC_FLAG,
	--		CANCELVC_NO,   LNBRANCH_ID,   BFPRINCIPALARR_AMT,   BFPERIOD_ARREAR,   LNFINE_AMT,
	--		FINE_RECVPERIOD,   PRINCIPAL_RETURN,   BFPRINCIPAL_RETURN,   BFINTEREST_RETURN,   CLCFEE_AMT,
	--		ETCFROM_ACCID )
	--	VALUES
	--		( SlipNo, 'LON', 1, BranchId, ShrLonTypeCD, 1, ContNo, 'ATM', PerdCountSts, Perd,
	--		PrinPay, 0, 0, PrinPay, PrinPay, CalIntFrom, CalIntTo, 0, 0, 0, 0, StmItemType, 1, BfPerd, 0, 0,
	--		0, BfLastCalIntDate, BfLastCalIntDate, BfLastCalIntDate, BfWithDrawAmt, BfBalAmt, 0, 0,
	--		BfContSts, 0, ContIntType, RkeepPrin, RkeepInt, NkeepInt,   0,   1,
	--		'', null, 0, '', 0, '', LnBranchId, null, null,   0,
	--		'',   0,   0,   0,   0,
	--		'' );
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_AddLoanRec(CoopID varchar2, SlipNo varchar2,StmtRefno varchar2, BranchID varchar2, MemNo varchar2, ContNo varchar2, LoanType varchar2, BfPerd number, BfPrinAmt number, BfLastCalIntDate date, BfLastRecDate date, 	BfLastProcDate date, SharStkVal number, IntAcc number, LoanRecDate date, LoanRecAmt number, RecPerdFlaf number, ExpBank varchar2, ExpBranch varchar2, ExpAccID varchar2, ContIntType number, EntryId varchar2, EntryDate 	date, ToFromAccId varchar2, retVal OUT INTEGER )
	is
  Coop_id LNCONTMASTER.coop_id%type;
	begin

    select Coop_id into Coop_id from LNCONTMASTER WHERE 	trim(LOANCONTRACT_NO) = trim(ContNo);

		dbms_output.put_line('Member_no='||trim(MemNo));
		--INSERT INTO CMLOANRECEIVE
		--	( LOANRECEIVE_NO, BRANCH_ID, MEMBER_NO, LOANCONTRACT_NO, LOANTYPE_CODE, BFPERIOD, BFPRINBAL_AMT, BFWITHDRAW_AMT,
		--	BFINTEREST_ARREAR, BFINTMONTH_ARREAR, BFPAYMENT_STATUS, BFLASTCALINT_DATE, BFLASTRECEIVE_DATE, BFLASTPROC_DATE, BFSTARTKEEP_PERIOD,
		--	SHARESTK_VALUE,   INTEREST_ACCUM,   LOANRECEIVE_DATE,   LOANRECEIVE_AMT, BUYSHARE_AMT,   CLEARPRIN_AMT,   CLEARINT_AMT,   LOANRECEIVENET_AMT,   INTEREST_ARREAR,
		--	INTMONTH_ARREAR,   RECEIVEPERIOD_FLAG,   EXPENSE_CODE,   EXPENSE_BANK, EXPENSE_BRANCH,   EXPENSE_ACCID,   BANKSRV_AMT,   BANKFEE_AMT,   OVERLAP_FLAG,
		--	PAYMENT_STATUS,   CONTRACTINT_TYPE,   SLIPLWD_NO, SLIPCLC_NO,    RECEIPT_NO,   PAYOUT_NO,   LOANRECEIVE_STATUS,   ENTRY_ID,
		--	ENTRY_DATE,   COOPBRANCH_ID,   CANCEL_ID,   CANCEL_DATE,   VOUCHER_NO, TOFROM_ACCID,   POSTTOVC_FLAG,   POSTMASTER_STATUS,LNBRANCH_ID,   MBBRANCH_ID )
		--VALUES
		--	( SlipNo, BranchID, trim(MemNo), ContNo, LoanType, BfPerd, BfPrinAmt, 0, 0, 0,  1, BfLastCalIntDate, BfLastRecDate, BfLastProcDate, '001', SharStkVal, IntAcc, LoanRecDate, LoanRecAmt,
		--	0, 0, 0, LoanRecAmt,   0,   0, RecPerdFlaf, 'CBT', ExpBank,
		--	ExpBranch, ExpAccID, 0, 0, 0,1, ContIntType, '',  '',   '',   '',   1, EntryId, EntryDate,   '',   '', null, '',  	ToFromAccId,   0,   1,   '',   '' )  ;
		--retVal := SQL%ROWCOUNT;

		INSERT INTO SLSLIPPAYOUT (
    COOP_ID,PAYOUTSLIP_NO,MEMCOOP_ID,MEMBER_NO,SLIPTYPE_CODE,SLIP_DATE,OPERATE_DATE,
    SHRLONTYPE_CODE,RCVFROMREQCONT_CODE,CONCOOP_ID,LOANCONTRACT_NO,
    RCVPERIOD_FLAG,RCV_PERIOD,PAYOUT_AMT,PAYOUTCLR_AMT,PAYOUTNET_AMT,
    BFPERIOD,BFLOANAPPROVE_AMT,BFSHRCONT_BALAMT,BFWITHDRAW_AMT,BFINTEREST_ARREAR,
    BFCONTLAW_STATUS,BFPAYMENT_STATUS,BFCONTINT_TYPE,BFLNRCVCLRFUTURE_TYPE,
    PRNCALINT_AMT,CALINT_FROM,CALINT_TO,INTEREST_PERIOD,LNPAYMENT_STATUS,
    SETSHRARR_FLAG,SHARE_STATUS,SHRWTDPENDING_AMT,BFSHAREBEGIN_AMT,BFSHAREFSTWTD_AMT,BFSHARE_STATUS,
    MONEYTYPE_CODE,EXPENSE_BANK,EXPENSE_BRANCH,EXPENSE_ACCID,
    BANKFEE_AMT,BANKSRV_AMT,BANKVAT_AMT,ACCID_FLAG,TOFROM_ACCID,SLIP_STATUS,SLIPCLEAR_NO,
    MEMBGROUP_CODE,ENTRY_ID,ENTRY_DATE,ENTRY_BYCOOPID,FINPOST_STATUS,
    POSTTOVC_FLAG,CANCELTOVC_FLAG,CANCELVC_NO,POST_TOFIN,RETURNETC_AMT,REF_DOCNO
    ) values (
    Coop_id,SlipNo,(select coop_id from mbmembmaster where trim(member_no)=trim(MemNo)) ,trim(MemNo),'LWD',EntryDate,LoanRecDate,
    LoanType,'CON',Coop_id,trim(ContNo),
    0,1,LoanRecAmt,0,LoanRecAmt,
    BfPerd,LoanRecAmt,0,LoanRecAmt,0,
    1,1,1,null,
    0,BfLastCalIntDate,BfLastProcDate,0,1,
    0,0,0,0,0,0,
    'CBT',ExpBank,ExpBranch,ExpAccID,
    0,0,0,0,ToFromAccId,1,'',
    (select MEMBGROUP_CODE from mbmembmaster where trim(member_no)=trim(MemNo)),EntryId,EntryDate,Coop_id,0,
    0,0,null,0,0 ,StmtRefno);
    retVal := SQL%ROWCOUNT;
	end;
        Function GetAccNo(CoopID varchar2, ContNo varchar2, MemNo varchar2) return varchar2
	is
	AccNo	lncontmaster.expense_accid%type;
	begin
		select 	expense_accid
		into	AccNo
		from 	lncontmaster
		where 	trim(member_no) = trim(MemNo);
		return AccNo;
	exception
 	  when no_data_found then
		return null;
	end;
        Function IsHaveLoanStmt(CoopID varchar2, ContNo varchar2, BranchId varchar2, StmtDate Date) return boolean
	is
	begin
		SELECT 	count(1)
		INTO	row_count
		FROM 	lncontstatement
		WHERE 	( loancontract_no =ContNo) and
		--			( coop_id = BranchId ) and
					( SLIP_DATE = StmtDate )  ;
		return (row_count > 0);
	end;
        Function GetLoanMasterDt(CoopID varchar2, ContNo varchar2, PrnBal out number, WithDraw out number)  return integer
	is
	begin
		select 	nvl(principal_balance, 0), nvl(withdrawable_amt, 0)
		into 	PrnBal, WithDraw
		from 	LNCONTMASTER
		where 	trim(loancontract_no)  = trim(ContNo);   --trim(ContNo) ;
		return SQL%ROWCOUNT;
	exception
 	  when no_data_found then
		return -1;
	end;
        Function GetLoanMasterDt(CoopID varchar2, ContNo varchar2, LoanType out varchar2, LastStmtNo out number, PrnBal out number, WithDraw out number, ContSts out number, IntArr out number, LastPerdPay out number, PerdPayAll out number, ContIntType out number, RkeepPrn out number, RkeepInt out number, NkeepInt out number, LastProcDate out date, LastCalcIntDate out date, StartContDate out date, PaySts out number, PerdPayAmt out number, StartKeepDate out date)  return integer
	is
	begin
		select loantype_code, nvl(last_stm_no, 0), nvl(principal_balance, 0), nvl(withdrawable_amt, 0), contract_status, nvl(interest_arrear, 0), last_periodpay, period_payamt,
			int_continttype, nvl(rkeep_principal, 0), nvl(rkeep_interest, 0), nkeep_interest, lastprocess_date, startcont_date, payment_status, period_payment, startkeep_date, lastcalint_date
		into 	LoanType, LastStmtNo, PrnBal, WithDraw, ContSts, IntArr, LastPerdPay, PerdPayAll, ContIntType, RkeepPrn, RkeepInt, NkeepInt, LastProcDate,StartContDate, PaySts, PerdPayAmt, StartKeepDate, LastCalcIntDate
		from 	LNCONTMASTER
		where 	trim(loancontract_no)  = trim(ContNo) and
--					 loantype_code = LoanType and
					contract_status = 1;
--		where 	trim(substr(loancontract_no, 3, length(loancontract_no)))  = trim(substr(ContNo, 3, length(ContNo))) and
--		where 	substr(trim(loancontract_no), 3, length(trim(loancontract_no)))  = substr(trim(x(ContNo)), 3, length(trim(x(ContNo)))) and
		return SQL%ROWCOUNT;
	exception
 	  when no_data_found then
		return -1;
	end;
        Procedure Process_GetKPTempRecvDt(CoopID varchar2, ContNo varchar2, RecvPerd out varchar2, KpTot out number, KpPrnc out number, KpInt out number, retVal out integer) is
	begin
		select max(recv_period)
		into 	RecvPerd
		from 	kptempreceive;

		select item_payment, principal_payment, interest_payment
		into 	KpTot, KpPrnc, KpInt
		from 	KPTEMPRECEIVEDET
		where RECV_PERIOD = RecvPerd and
			trim(LOANCONTRACT_NO) = trim(ContNo);
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateKPTEMPRECEIVEDET(CoopID varchar2, ContNo varchar2, RecPerd varchar2, ReGenPrn number, ReKpGenPrn number, RegenInt number, CalcIntFrom date, CalcIntTo date, LastCalcInt date, retVal OUT INTEGER)
	is
	begin
		UPDATE 	KPTEMPRECEIVEDET
		SET 	PRINCIPAL_PAYMENT = ReKpGenPrn,
			INTEREST_PAYMENT = RegenInt,
			PRINCIPAL_BALANCE = ReGenPrn - ReKpGenPrn,
			CALINTFROM_DATE = CalcIntFrom,
			CALINTTO_DATE = CalcIntTo,
			PRINCIPAL_PERIOD = ReKpGenPrn,
			INTEREST_PERIOD = RegenInt,
			BFPRINBALANCE_AMT = ReGenPrn,
			BFLASTCALINT_DATE = LastCalcInt
		WHERE 	RECV_PERIOD = RecPerd AND
			trim(LOANCONTRACT_NO) = trim(ContNo);
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateLNCONTMASTER(CoopID varchar2, ContNo varchar2, WithDrawAmt number, PrnBal number, retVal OUT INTEGER)
	is
	begin
		UPDATE 	LNCONTMASTER
		SET 	PRINCIPAL_BALANCE = PrnBal,
			WithDrawable_amt = WithDrawAmt
		WHERE 	trim(LOANCONTRACT_NO) = trim(ContNo);
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateLNCONTMASTER(CoopID varchar2, ContNo varchar2, PrnBal number, PerdPay number, WithDrawAmt number, IntArr number, StartContDate date, StartKeepDate date, LastPerdPay number, LastStmtNo number, LastCalcIntDate date, LastRecDate date, LastProcDate date, LastAccDate date, NKeepPrn number default null, NKeepInt number default null, RKeepPrn number default null, RKeepInt number default null, retVal OUT INTEGER)
	is
	begin
		if NKeepPrn  is null then
			UPDATE 	LNCONTMASTER
			SET 	PRINCIPAL_BALANCE = PrnBal,
				PERIOD_PAYMENT = PerdPay,
				WITHDRAWABLE_AMT = WithDrawAmt ,
				INTEREST_ARREAR = IntArr,
				STARTCONT_DATE = StartContDate ,
				STARTKEEP_DATE = StartKeepDate ,
				LAST_PERIODPAY = LastPerdPay,
				LAST_STM_NO = LastStmtNo,
				LASTCALINT_DATE = LastCalcIntDate,
				LASTRECEIVE_DATE = LastRecDate,
				LASTPROCESS_DATE = LastProcDate,
				LASTACCESS_DATE = LastAccDate
			WHERE 	trim(LOANCONTRACT_NO) = trim(ContNo);
		else
			UPDATE 	LNCONTMASTER
			SET 	PRINCIPAL_BALANCE = PrnBal,
				PERIOD_PAYMENT = PerdPay,
				WITHDRAWABLE_AMT = WithDrawAmt ,
				INTEREST_ARREAR = IntArr,
				STARTCONT_DATE = StartContDate ,
				STARTKEEP_DATE = StartKeepDate ,
				LAST_PERIODPAY = LastPerdPay,
				LAST_STM_NO = LastStmtNo,
				LASTCALINT_DATE = LastCalcIntDate,
				LASTRECEIVE_DATE = LastRecDate,
				LASTPROCESS_DATE = LastProcDate,
				LASTACCESS_DATE = LastAccDate,
				NKEEP_PRINCIPAL = NKeepPrn,
				NKEEP_INTEREST = NKeepInt,
				RKEEP_PRINCIPAL = RKeepPrn,
				RKEEP_INTEREST = RKeepInt
			WHERE 	trim(LOANCONTRACT_NO) = trim(ContNo);
		end if;
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_AddLoanSTMT(CoopID varchar2, ContNo varchar2, SeqNo number, BranchId varchar2, LoanItemType varchar2, StmtDate date,
	Refno varchar2, Perd number, PrnPay number, PrnBal number, CalcIntFrom date, CalcIntTo date, IntPerd number,
	IntArr number, MoneyType varchar2, ItemSts number, EntryID varchar2, EntryDate date, CoopBranch varchar2, LoanRecNo varchar2, retVal OUT INTEGER )
	is
  Coop_id LNCONTMASTER.coop_id%type;
	begin

  select Coop_id into Coop_id from LNCONTMASTER WHERE 	trim(LOANCONTRACT_NO) = trim(ContNo);

  if LoanRecNo is not null then
		INSERT INTO LNCONTSTATEMENT
			( LOANCONTRACT_NO,   SEQ_NO,   COOP_ID,   LOANITEMTYPE_CODE,   SLIP_DATE,   OPERATE_DATE, ACCOUNT_DATE,
			REF_DOCNO,   PERIOD,   PRINCIPAL_PAYMENT,   INTEREST_PAYMENT,   PRINCIPAL_BALANCE,   CALINT_FROM,
			CALINT_TO,   INTEREST_PERIOD,   INTEREST_ARREAR,   INTEREST_RETURN,
			MONEYTYPE_CODE,   ITEM_STATUS,   ENTRY_ID,   ENTRY_DATE,   ENTRY_BYCOOPID )
		VALUES
			( ContNo, SeqNo, Coop_id, LoanItemType, StmtDate, StmtDate, StmtDate,
			Refno, Perd, PrnPay, 0, PrnBal, CalcIntFrom,
			CalcIntTo, IntPerd, IntArr, 0, MoneyType, 1, EntryID, EntryDate, Coop_id)  ;
  else
		INSERT INTO LNCONTSTATEMENT
			( LOANCONTRACT_NO,   SEQ_NO,   COOP_ID,   LOANITEMTYPE_CODE,   SLIP_DATE,   OPERATE_DATE, ACCOUNT_DATE,
			REF_DOCNO,   PERIOD,   PRINCIPAL_PAYMENT,   INTEREST_PAYMENT,   PRINCIPAL_BALANCE,   CALINT_FROM,
			CALINT_TO,   INTEREST_PERIOD,   INTEREST_ARREAR,   INTEREST_RETURN,
			MONEYTYPE_CODE,   ITEM_STATUS,   ENTRY_ID,   ENTRY_DATE,   ENTRY_BYCOOPID )
		VALUES
			( ContNo, SeqNo, Coop_id, LoanItemType, StmtDate, StmtDate, StmtDate,
			Refno, Perd, PrnPay, 0, PrnBal, CalcIntFrom,
			CalcIntTo, IntPerd, IntArr, 0, MoneyType, 1, EntryID, EntryDate, Coop_id)  ;
  end if;
		retVal := SQL%ROWCOUNT;
	end;

        Procedure CancelLoanSTMT(CoopID varchar2, ContNo varchar2, SeqNo number, BranchId varchar2, LoanItemType varchar2, StmtDate date,
	Refno varchar2, Perd number, PrnPay number, PrnBal number, CalcIntFrom IN OUT date, CalcIntTo IN OUT date, IntPerd IN OUT number,
	IntArr IN OUT number, MoneyType varchar2, ItemSts number, EntryID varchar2, EntryDate date, CoopBranch varchar2, LoanRecNo out varchar2, retVal OUT INTEGER )
	is
  Coop_id varchar2(6);
  DataNotFound 		EXCEPTION;
  DataNotFound2  		EXCEPTION;
  DataNotFound3  		EXCEPTION;
	begin

  select Coop_id into Coop_id from LNCONTMASTER WHERE 	trim(LOANCONTRACT_NO) = trim(ContNo);

		begin
			SELECT 	CALINT_FROM, INTEREST_PERIOD, INTEREST_ARREAR
			INTO		CalcIntFrom, IntPerd, IntArr
			FROM		LNCONTSTATEMENT
			WHERE	trim(LOANCONTRACT_NO) =  ContNo AND
						REF_DOCNO = Refno;
		exception
		  when no_data_found then
			raise DataNotFound;
		end;
		CalcIntTo := CalcIntFrom;
		begin
			UPDATE LNCONTSTATEMENT SET ITEM_STATUS = -9
			WHERE	trim(LOANCONTRACT_NO) =  ContNo AND
						REF_DOCNO = Refno;
		exception
		  when no_data_found then
			raise DataNotFound2;
		end;
		begin
			INSERT INTO LNCONTSTATEMENT
				( LOANCONTRACT_NO,   SEQ_NO,   ENTRY_BYCOOPID,   LOANITEMTYPE_CODE,   SLIP_DATE,   OPERATE_DATE,   ACCOUNT_DATE ,
				REF_DOCNO,   PERIOD,   PRINCIPAL_PAYMENT,   INTEREST_PAYMENT,   PRINCIPAL_BALANCE,   CALINT_FROM,
				CALINT_TO,   INTEREST_PERIOD,   INTEREST_ARREAR,   INTEREST_RETURN,
				MONEYTYPE_CODE,   ITEM_STATUS,   ENTRY_ID,   ENTRY_DATE,   COOP_ID )
			VALUES
				( ContNo, SeqNo, Coop_id, LoanItemType, StmtDate, StmtDate, StmtDate,
				Refno, Perd, PrnPay, 0, PrnBal, CalcIntFrom,
				CalcIntTo, IntPerd, IntArr, 0, MoneyType, -9, EntryID, EntryDate, Coop_id)  ;
		exception
		  when no_data_found then
			raise DataNotFound3;
		end;
		retVal :=1;
	exception
	  when DataNotFound then
		retVal :=-1;
	  when DataNotFound2 then
		retVal :=-2;
	  when DataNotFound3 then
		retVal :=-3;
	end;

        Procedure CancelLoanRec(CoopID varchar2,LoanItemType varchar2, Refno varchar2, ReconcileDate Date, retVal OUT INTEGER )
	is
	begin

  case
		when LoanItemType = 'LRC'  then

    update slslippayout  SET SLIP_STATUS = -9, CANCEL_ID = 'ATMKTB', CANCEL_DATE = ReconcileDate
		WHERE	trim(ref_docno) = Refno;
    
		when LoanItemType = 'LPX'  then

    update slslippayin  SET SLIP_STATUS = -9, CANCEL_ID = 'ATMKTB', CANCEL_DATE = ReconcileDate
		WHERE	trim(ref_docno) = Refno;
   
  end case; 
    
		--UPDATE CMLOANRECEIVE SET LOANRECEIVE_STATUS = -9, CANCEL_ID = 'ATMKTB', CANCEL_DATE = ReconcileDate
		--WHERE	trim(LOANRECEIVE_NO) = LoanRecNo;
	--exception
	  --when no_data_found then
		retVal :=-1;
	end;

        Function GetDeptMasterDt(CoopID varchar2, DeptNo varchar2, PrnBal out number, WithDraw out number, WithAmt out number, DeptAmt out number, DeptCloseSts out number, BranchId out varchar2) return number
	is
	begin
		select nvl(prncbal, 0), nvl(withdrawable_amt, 0), nvl(deptclose_status, 0), coop_id, with_amt, dept_amt
		into 	PrnBal, WithDraw, DeptCloseSts, BranchId, WithAmt,  DeptAmt
		from DPDEPTMASTER
		where trim(deptaccount_no) = trim(DeptNo) ;
		return SQL%ROWCOUNT;
	exception
 	  when no_data_found then
		return -1;
	end;
        Function GetDeptMasterDt(CoopID varchar2, DeptNo varchar2, DeptType out varchar2, LastStmt out number, PrnBal out number, WithDraw out number, DeptCloseSts out number, SequsetSts out number, LastCalcIntDate out date, AccIntAmt out number, IntArrearAmt out number, SpcIntRate out number, SpcIntRateSts out number, ChqPendAmt out number, DeptAmt out number, WithAmt out number, BranchId out varchar2) return number
	is
	begin
		select depttype_code, nvl(laststmseq_no, 0), nvl(prncbal, 0), nvl(withdrawable_amt, 0), nvl(deptclose_status, 0), nvl(sequest_status, 0), lastcalint_date, nvl(accuint_amt, 0), nvl(intarrear_amt, 0), nvl(spcint_rate, 0), nvl(spcint_rate_status, 0), nvl(checkpend_amt, 0), nvl(dept_amt, 0), nvl(with_amt, 0), coop_id
		into DeptType, LastStmt, PrnBal, WithDraw, DeptCloseSts, SequsetSts, LastCalcIntDate, AccIntAmt, IntArrearAmt, SpcIntRate, SpcIntRateSts , ChqPendAmt, DeptAmt, WithAmt, BranchId
		from DPDEPTMASTER
		where trim(deptaccount_no) = trim(DeptNo) ;
		return SQL%ROWCOUNT;
	exception
 	  when no_data_found then
		return -1;
	end;
        Procedure Process_UpdateDeptMaster(CoopID varchar2, DeptNo varchar2, BranchId varchar2, Withraw number, PrncBal1 number, WithAmt number, DeptAmt number, retVal OUT INTEGER) is
	begin
		update	DPDEPTMASTER
		set	prncbal		= PrncBal1,
			withdrawable_amt	= Withraw,
			dept_amt	= DeptAmt,
			with_amt	= WithAmt
		where	( trim(deptaccount_no)= trim(DeptNo) ) and
			( coop_id	=  BranchId );
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateDeptMaster(CoopID varchar2, DeptNo varchar2, BranchId varchar2, PrncBal1 number, Withraw number, AccuInt number, LastCalntDate date, LastAccDate date, LastStmSeqNo number, DoperateBal number, LastMoveDate date, DeptAmt number, WithAmt number, retVal OUT INTEGER) is
	begin
		update	DPDEPTMASTER
		set	prncbal		= PrncBal1,
			withdrawable_amt	= Withraw ,
			accuint_amt	= AccuInt,
			lastcalint_date 	= LastCalntDate,
			lastaccess_date	= LastAccDate,
			laststmseq_no	= LastStmSeqNo,
			doperate_bal	= DoperateBal,
			lastmovement_date	= LastMoveDate,
			dept_amt		= DeptAmt,
			with_amt		= WithAmt
		where	( trim(deptaccount_no)	= trim(DeptNo) ) and
			( coop_id	=  BranchId );
		retVal := SQL%ROWCOUNT;
	EXCEPTION
		WHEN OTHERS THEN
		retVal := -1;
	end;
        Procedure Process_AddDeptSlip(CoopID varchar2, SlipNo varchar2, DeptAccNo varchar2, DeptType varchar2, SlipDate Date, RecPayType varchar2, SlipAmt number, PrnBal number, WithDraw number, EntryId varchar2, EntryDate date, MachID varchar2, DeptItemType varchar2, DpStmNo varchar2, CalIntFrom date, CalIntTo date, DeptGrp varchar2, ToFromAccId varchar2, RefApp varchar2, RefSlipNo varchar2, CashType varchar2, IntAmt number, AccIntAmt number, BranchId varchar2, BranchOrg varchar2, retVal OUT INTEGER ) is
	begin
		INSERT INTO DPDEPTSLIP
			( DEPTSLIP_NO, DEPTACCOUNT_NO, DEPTTYPE_CODE, DEPTSLIP_DATE, RECPPAYTYPE_CODE, DEPTSLIP_AMT,
			PRNCBAL, WITHDRAWABLE_AMT, ENTRY_ID, ENTRY_DATE, MACHINE_ID,  DEPTITEMTYPE_CODE,
			DPSTM_NO, ITEM_STATUS, CALINT_FROM, CALINT_TO, DEPTGROUP_CODE, TOFROM_ACCID, REFER_APP,
			REFER_SLIPNO, CASH_TYPE, SHOWFOR_DEPT, INT_AMT, ACCUINT_AMT, coop_ID, deptcoop_ID,
			TAX_AMT, DUE_FLAG, DEPTSLIP_NETAMT)
		VALUES
			( SlipNo, DeptAccNo, DeptType, SlipDate, RecPayType, SlipAmt, PrnBal,
			WithDraw, EntryId, EntryDate, MachID, DeptItemType, DpStmNo, 1, CalIntFrom, CalIntTo,
			DeptGrp, ToFromAccId, RefApp,  RefSlipNo, CashType, 1, IntAmt, AccIntAmt,
			BranchId, BranchOrg, 0, 0, SlipAmt) ;
		retVal := SQL%ROWCOUNT;
	end;
        Function IsHaveDeptStmt(CoopID varchar2, DeptAccNo varchar2, DeptType varchar2, StmtDate Date) return boolean
	is
	begin
		SELECT 	count(1)
		INTO	row_count
		FROM 	dpdeptstatement
		WHERE 	( deptaccount_no = DeptAccNo )
		AND	( OPERATE_DATE = StmtDate )
		--and	( DEPTITEMTYPE_CODE =  DeptType )
    ;
		return (row_count > 0);
	end;
        Procedure Process_AddDeptSTMT(CoopID varchar2, SeqNo number, DeptAccNo varchar2, DeptType varchar2, SlipDate Date,
	RefNo varchar2, SlipAmt number, PrnBal number, PrnBalTemp number, PrncNo number, IntAmt number, AccIntAmt number,
	EntryId varchar2, EntryDate date, CalIntFrom date, CalIntTo date, CashType varchar2, MachID varchar2, IsChqPending number,
	BranchID varchar2, retVal OUT INTEGER)
	is
  Coop_id varchar2(6);
	begin

  select Coop_id into Coop_id from DPDEPTMASTER WHERE 	trim(DEPTACCOUNT_NO) = trim(DeptAccNo);

		INSERT INTO DPDEPTSTATEMENT
			(	DEPTACCOUNT_NO, SEQ_NO, DEPTITEMTYPE_CODE,
				OPERATE_DATE, REF_DOCNO, DEPTITEM_AMT,
				BALANCE_FORWARD, PRNCBAL, PRNC_NO, TAX_AMT, INT_AMT, ACCUINT_AMT,
				RETINT_AMT, ITEM_STATUS, PRNTOPB_STATUS, PRNTOCARD_STATUS, CHECKBOOK_CODE_PB,
				ENTRY_ID, ENTRY_DATE, CLOSEDAY_STATUS, CALINT_FROM, CALINT_TO, DEPTCOOP_ID,
				CASH_TYPE, AUTHORIZE_ID, MACHINE_ID, NO_BOOK_FLAG, CHEQUE_PENDING, REF_SEQ_NO, PAGE_PB, LINE_PB, coop_id)
		values
			(	DeptAccNo, SeqNo,	DeptType, SlipDate, RefNo, SlipAmt,
				PrnBal, PrnBalTemp, PrncNo,	0, IntAmt, AccIntAmt,
				0, 1, 0, 0, null, EntryId, EntryDate, 0, CalIntFrom,
				CalIntTo,	BranchID, CashType, EntryId, MachID, 0,
				IsChqPending, null,	 null, null,	Coop_id) ;
		retVal := SQL%ROWCOUNT;
	end;
       Procedure Process_PostLoanAmtLimit2ATM( CoopID varchar2, retVal OUT INTEGER)  is
	type 		fin_cursor is REF CURSOR;
	c_fin 		fin_cursor;
  LoanType varchar2(2);
	fin 		atm_fin_amt%rowtype;
	MemNo	mbmembmaster.member_no%type;
	MemNm	mbmembmaster.memb_name%type;
	MemSNm	mbmembmaster.memb_surname%type;
	PersonID	mbmembmaster.card_person%type;
	t char(1);
	begin
		open c_fin for
		select * from atm_fin_amt
		where fintype = 'L' and
		(ispost <> POSTED or ispost is null)
		order by operatedate;
		loop
			fetch c_fin into fin;
		   	exit when c_fin%notfound;
			fin.memno := trim(fin.memno);
			if fin.ishold = 'H' then
				Pkg_AtmMgmt2.Process_HoldLoanAccNo(CoopID, fin.memno, retVal);
			elsif fin.ishold = 'D' then
				null;
--				delete from atmserv_atmloan where coop_id = CoopID and
--					member_no = fin.memno and
--					trim(contract_no) = trim(fin.refno);
			end if;
			dbms_output.put_line('Memno : '||fin.memno||', RefNo : '||fin.refno);
			if  Pkg_AtmMgmt2.IsNewMember(CoopID, fin.memno) then
				dbms_output.put_line('fin.memno='||fin.memno);
				dbms_output.put_line('fin.MemNm='||fin.MemNm);
				dbms_output.put_line('fin.MemSNm='||fin.MemSNm);
				Pkg_AtmMgmt2.Process_AddMember(CoopID, fin.memno, fin.MemNm, fin.MemSNm, fin.PersonID, retVal);
			end if;
			t := Pkg_AtmMgmt2.GetLoanContractSTS(CoopID, fin.memno, fin.refno);
      --getLoanType(fin.refno,LoanType);
			if t = CONTRC_CHG then
				Pkg_AtmMgmt2.Process_UpdateLoanContract(CoopID, fin.memno, fin.refno, retVal);
			elsif t = CONTRC_NEW then
				if Pkg_AtmMgmt2.IsHaveLoanAcc(CoopID, fin.memno, fin.accno) then
					Pkg_AtmMgmt2.Process_HoldLoanAccNo(CoopID, fin.memno, retVal);
				else
					Pkg_AtmMgmt2.Process_AddLoanContract(CoopID, fin.memno, fin.refno, fin.itemamt, fin.balamt, 0, 0, fin.accno, 0 , retVal);
				end if;
			end if;
			if Pkg_AtmMgmt2.IsHaveLoanAcc(CoopID, fin.memno, fin.accno) then
				Pkg_AtmMgmt2.Process_HoldLoanAccNo(CoopID, fin.memno, retVal);
			else
				Pkg_AtmMgmt2.Process_UpdateLoanAccNo(CoopID, fin.memno, fin.refno, fin.accno, retVal);
			end if;
			Pkg_AtmMgmt2.Process_UpdateLoanAmt(CoopID, fin.memno, fin.refno, fin.itemamt, fin.balamt, retVal);
			dbms_output.put_line(fin.memno||'|'||fin.refno||'  UpdateLoanAmt : '|| fin.itemamt||'|'||fin.balamt||'|'||retVal);
			Process_SetTransPosted(fin.seqno, retVal);
			dbms_output.put_line('SetPost : '||retVal);
  		end loop;
  		close c_fin;
	end;
       Procedure Process_PostDeptAmtLimit2ATM( CoopID varchar2, retVal OUT INTEGER)  is
	type fin_cursor is REF CURSOR;
	c_fin fin_cursor;
	fin atm_fin_amt%rowtype;
	MemNo	mbmembmaster.member_no%type;
	MemNm	mbmembmaster.memb_name%type;
	MemSurNm	mbmembmaster.memb_surname%type;
	PersonID	mbmembmaster.card_person%type;
	t char(1);
	begin
		open c_fin for
		select * from atm_fin_amt
		where fintype = 'D' and
		(ispost <> POSTED or ispost is null)
		order by operatedate;
		loop
			fetch c_fin into fin;
		   	exit when c_fin%notfound;
			if fin.ishold = 'H' then
				Pkg_AtmMgmt2.Process_HoldDeptAccNo(CoopID, fin.memno,retVal);
			elsif fin.ishold = 'D' then
				Pkg_AtmMgmt2.Process_DelDeptContract(CoopID, fin.memno, fin.refno, retVal);
			end if;
			dbms_output.put_line('Memno : '||fin.memno||', RefNo : '||fin.refno);
			if  Pkg_AtmMgmt2.IsNewMember(CoopID, fin.memno) then
				Pkg_AtmMgmt2.Process_AddMember(CoopID, fin.memno, fin.MemNm, fin.MemSNm, fin.PersonID, retVal);
			end if;
			t := Pkg_AtmMgmt2.GetDeptContractSTS(CoopID, fin.memno, fin.refno);
			if t = CONTRC_CHG then
				Pkg_AtmMgmt2.Process_UpdateDeptContract(CoopID, fin.memno, fin.refno,retVal);
			elsif t = CONTRC_NEW then
				if Pkg_AtmMgmt2.IsHaveDeptAcc(CoopID, fin.memno, fin.accno) then
					Pkg_AtmMgmt2.Process_HoldDeptAccNo(CoopID, fin.memno, retVal);
				else
					Pkg_AtmMgmt2.Process_AddDeptContract(CoopID, fin.memno, fin.refno, fin.itemamt, fin.balamt, 0, 0, fin.accno, 0, retVal);
				end if;
			end if;
			if Pkg_AtmMgmt2.IsHaveDeptAcc(CoopID, fin.memno, fin.accno) then
				Pkg_AtmMgmt2.Process_HoldDeptAccNo(CoopID, fin.memno,retVal);
			else
				Pkg_AtmMgmt2.Process_UpdateDeptAccNo(CoopID, fin.memno, fin.refno, fin.accno, retVal);
			end if;
			retVal := 0;
			Pkg_AtmMgmt2.Process_UpdateDeptAmt(CoopID, fin.memno, fin.refno, fin.itemamt, fin.balamt, retVal);
			dbms_output.put_line(fin.memno||'|'||fin.refno||'  UpdateDeptAmt : '||retVal);
			Process_SetTransPosted(fin.seqno, retVal);
			dbms_output.put_line('SetPost : '||retVal);
  		end loop;
  		close c_fin;
	end;
End PKG_ATMMgmt;

/
