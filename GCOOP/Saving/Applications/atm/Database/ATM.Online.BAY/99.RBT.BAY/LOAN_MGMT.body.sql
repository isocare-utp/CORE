--------------------------------------------------------
--  File created - Thursday-August-04-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package Body LOAN_MGMT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "LOAN_MGMT" AS
        Procedure GetInitVal(CountDateType out number, IntRoundNum out number, IntRoundSumType out number, IntRoundFormat out number, IntRoundKpType out number, GBfSts out number, CountDateFstType out number, IntRoundType out number, FixPayCalcType out number, IncludeIntArrSts out number, KpItemArrD out number)
	is
	begin
		select 	countdate_type, intround_num, introundsum_type, intround_format, intround_type, genbf_status, countdatefst_type, intround_type, fixpaycal_type, incluintarr_status, kpitemarrdouble_flag
		into		CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType, GBfSts, CountDateFstType, IntRoundType, FixPayCalcType, IncludeIntArrSts, KpItemArrD
		from 		lnloanconstant;
	end;
        Procedure GetInitVal(LoanType varchar2, LoanPayType out number, PayRoundFactor out number, PayRoundType out number)
	is
	begin
		select 	loanpayment_type, payround_factor, payround_type
		into		LoanPayType, PayRoundFactor, PayRoundType
		from 		lnloantype
		where	loantype_code = LoanType;
	end;
        Function CalcContrcPerdPay(LoanType varchar2, LoanPayType varchar2,adc_money number,adtm_operate date,  IntRoundType number, PayRoundType number, FixPayCalcType number, PayRoundFactor number, PrnBal in out number, PerdPay in out number, LastPerdPay in out number) return number
	is
	Payment				number(15, 2);
	ModPay				number(15, 2);
	IntRate				number(15, 2);
	Perd					integer;
	IntAmt				number(15, 2);
	PrncPay				number(15, 2);
	fr						number(15, 2);
	PayAmt				number(15, 2);
	begin
		if (PrnBal = 0)  or (PerdPay = 0) then return -1; end if;
		CASE LoanPayType
			WHEN 1 THEN
				Payment := PrnBal/PerdPay;
				if PayRoundFactor > 0 then
					ModPay := mod(Payment, PayRoundFactor);
					if ModPay > 0 then
						Payment := Payment - ModPay;
						if IntRoundType = 1 then
							Payment := Payment + PayRoundFactor;
						end if;
						if Payment <= 0 then
							Payment:= PayRoundFactor;
						end if;
					end if;
				end if;
				PerdPay := trunc((PrnBal/Payment), 0);
				LastPerdPay := mod(PrnBal , Payment);
				if LastPerdPay = 0 then
					LastPerdPay := Payment;
				else
					PerdPay := PerdPay + 1;
				end if;
			WHEN 2 THEN
			
				select nvl( interest_rate/100 , 0 ) as interest_rate
				into Intrate
				from lncfloanintratedet
				where 
				-- coop_id = PKG_ATMMGMT.C_COOPID and
				 adtm_operate between effective_date and expire_date
				and loanintrate_code = (select INTTABRATE_CODE from lnloantype where trim(loantype_code)= trim(LoanType) )
				and adc_money between lower_amt and upper_amt;
				
				if IntRate = 0 then return -1;end if;
				if FixPayCalcType = 1 then
					fr := exp( - PerdPay * ln((1 + Intrate/12)));
					Payment := PrnBal* (IntRate/12)/(1 - fr);
				else
					fr := exp( - PerdPay * ln((1 + IntRate* (30/365))));
					Payment := PrnBal* (IntRate * 30/365)/(1 - fr);
				end if;
				if IntRoundType > 0 then
					Payment := roundmoney(Payment, IntRoundType);
				end if;
				if PayRoundFactor > 0 then
					ModPay :=mod(Payment, PayRoundFactor);
					if ModPay > 0 then
						Payment := Payment - ModPay;
						if PayRoundType = 1 then
							Payment := Payment + PayRoundFactor;
						end if;
					end if;
				end if;
				Perd := 1;
				WHILE PrnBal > 0
				 LOOP
						if FixPayCalcType = 1 then
							IntAmt := (PrnBal/12)*IntRate;
						else
							IntAmt := (PrnBal*30/365)*IntRate;
						end if;
						IntAmt := roundmoney(IntAmt, IntRoundType);
						PrncPay := Payment - IntAmt;
						if PrncPay > PrnBal then
							PrncPay := PrnBal;
						end if;
						PrnBal := PrnBal - PrncPay;
						Perd := Perd + 1;
						if (Perd = PerdPay - 1) and (PrnBal > PayAmt) and (FixPayCalcType <> 1) then
							PayAmt := PayAmt + (PayAmt*IntRate*30)/366;
							if IntRoundType > 0 then
								PayAmt := roundmoney(PayAmt, IntRoundType);
							end if;
						end if;
						if Perd > PerdPay then
							LastPerdPay := LastPerdPay + PrncPay;
							Perd := PerdPay;
						else
							LastPerdPay := PrncPay + IntAmt;
						end if;
				 END LOOP;
				PerdPay := Perd;
				if PayRoundFactor > 0 then
					ModPay := mod(PayAmt, PayRoundFactor);
					if ModPay > 0 then
						PayAmt := PayAmt - ModPay;
						if IntRoundType = 1 then
							PayAmt := PayAmt + PayRoundFactor;
						end if;
						if PayAmt <= 0 then
							PayAmt := PayRoundFactor;
						end if;
					end if;
				end if;
		END CASE;
		return Payment;
	end;
        Function GetDayofYear (d date ) return integer
	is
	y	integer;
	begin
		select to_char(to_date('31/12/'||to_char(d, 'YYYY'), 'DD/MM/YYYY'), 'DDD') into y from dual;
		return y;
	end;
        Function RoundMoney (Money number, t number) return number
	is
	Frac	integer;
	n		number(15,2);
	begin
		n := Money;
		CASE t
			WHEN 0 THEN
				null;
			WHEN 1 THEN
				Frac := mod(n, 1)*100;
				if Frac >= 1 and Frac <= 25 then
					 Frac := 25;
				elsif Frac >= 26 and Frac <= 50 then
					 Frac := 50;
				elsif Frac >= 51 and Frac <= 75 then
					 Frac := 75;
				elsif Frac >= 76 and Frac <= 99 then
					 Frac := 100;
				else
					 Frac := 0;
				end if;
				n := trunc(n) + (Frac/100);
			WHEN 2
				THEN n := round(n, 1);
			WHEN 3
				THEN n := round(n, 0);
			WHEN 4	THEN
				Frac := to_number(substr(to_char(n, '999999999999999.99'), length(to_char(n, '999999999999999.99')), 1));
		dbms_output.put_line('Frac='||Frac);
				if (Frac >= 1 and Frac <= 5) then
					n := 0.05;
				elsif (Frac >= 6 and Frac <= 9) then
					n := 0.1;
				else
					n := 0;
				end if;
				n := trunc(Money, 1) + n;
			WHEN 5
				THEN
				Frac := mod(n, 1)*100;
				if Frac > 0 then
					n := trunc(n) + 1;
				end if;
			WHEN 9	THEN
				Frac := mod(n, 1)*100;
				if Frac >= 1 and Frac <= 24 then
					 Frac := 0;
				elsif Frac >= 26 and Frac <= 49 then
					 Frac := 25;
				elsif Frac >= 51 and Frac <= 74 then
					 Frac := 50;
				elsif Frac >= 76 and Frac <= 99 then
					 Frac := 75;
				else
					 Frac := 0;
				end if;
				n := trunc(n) + (Frac/100);
		END CASE;
		return n;
	end;
        Function CalcInt (LoanType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date, CountDateType varchar2, IntRoundNum number, IntRoundSumType number, IntRoundFormat number, IntRoundKpType number) return number
	is
	cursor c_IntRate(LoanType varchar2, CalcIntFrom date, CalcIntTo date) is
	select		*
	from 		lncfloanintratedet l1
	where 	l1.loanintrate_code = (select INTTABRATE_CODE from lnloantype where trim(loantype_code)= trim(LoanType) )  and
				trunc(effective_date) >= (select max(trunc(effective_date))
	from 		lncfloanintratedet l2
	where  	l2.loanintrate_code = l1.loanintrate_code and
				trunc(l2.effective_date) <= trunc(CalcIntFrom)) and
				trunc(effective_date) <= (select max(trunc(effective_date))
	from 		lncfloanintratedet l2
	where  	l2.loanintrate_code = l1.loanintrate_code and
				trunc(l2.effective_date) <= trunc(CalcIntTo))
	order 		by 	loanintrate_code, effective_date, lower_amt;
	type 		IntRateTab is table of lncfloanintratedet%rowtype;
	intrate 	intratetab := intratetab();
	i			integer := 0;
	TYPE 		NumberTabType IS TABLE OF NUMBER  INDEX BY BINARY_INTEGER;
	TYPE 		DateTabTyp IS TABLE OF DATE INDEX BY BINARY_INTEGER;
	MinAmt 	NumberTabType;
	DayNo 	NumberTabType;
 	ExpDate 	DateTabTyp;
	IntAmt		number(15, 2);
	TempIntAmt	number(15, 2);
	LastChgInt	number;
	DayInYear	number;
	StartDate	date;
	EndDate		date;
	TempAmt	number(15, 2);
	begin
		if (PrnBal = 0) or trunc(CalcIntTo) < trunc(CalcIntFrom) then return 0.00; end if;
		DayInYear := GetDayofYear(CalcIntFrom);
/*		DayNo := CalcIntTo - CalcIntFrom;
		if CountDateType = 0 then
			LastChgInt := 0;
		elsif CountDateType = 1 then
			DayNo := DayNo - 1;
		elsif CountDateType = 2 then
			DayNo := DayNo + 1;
		end if; */
		for n in c_IntRate(LoanType, CalcIntFrom, CalcIntTo) loop
			i := i + 1;
			intrate.extend;
			intrate(i) := n;
			ExpDate(i) := CalcIntTo;
			if i = 1 then
				MinAmt(i) := 0;
			else
				if intrate(i).effective_date <> intrate(i - 1).effective_date then
					MinAmt(i) := 0;
					ExpDate(i - 1) :=intrate(i).effective_date; -- - 1;
				else
					MinAmt(i) := intrate(i - 1).lower_amt;
				end if;
			end if;
		end loop;
		TempAmt := PrnBal;
		IntAmt := 0;
		FOR i IN 1..intrate.COUNT LOOP
			if TempAmt >= MinAmt(i) and TempAmt <= intrate(i).upper_amt then
				if intrate(i).effective_date < CalcIntFrom then
					StartDate := CalcIntFrom;
				else
					StartDate := intrate(i).effective_date;
				end if;
				if ExpDate(i) < CalcIntTo then
					EndDate := ExpDate(i);
				else
					EndDate := CalcIntTo;
				end if;
				dayno(i) := trunc(EndDate) - trunc(StartDate);
				TempIntAmt := TempAmt*intrate(i).interest_rate/100*dayno(i)/DayInYear;
				if IntRoundFormat = 1 then
					TempIntAmt := round(TempIntAmt, IntRoundNum);
				else
					TempIntAmt := trunc(TempIntAmt, IntRoundNum);
				end if;
				TempIntAmt := round(TempIntAmt, 2);
				if IntRoundSumType = 1 and IntRoundKpType > 0 then
					TempIntAmt := RoundMoney(TempIntAmt, IntRoundKpType);
				end if;
--				dbms_output.put_line('Before IntAmt='||IntAmt);
				IntAmt := IntAmt + TempIntAmt;
--				dbms_output.put_line('After IntAmt='||IntAmt);
				dbms_output.put_line('Day ='||dayno(i)||' '||intrate(i).effective_date||'=>'||ExpDate(i)||' S = '||StartDate||'=>'||EndDate||' Min = '||MinAmt(i)||'-'||intrate(i).lower_amt||' Rate = '||intrate(i).interest_rate||' Interest = '||TempIntAmt);
			end if;
		END LOOP;
		if IntAmt <= 0 then IntAmt := 0; end if;
		dbms_output.put_line('IntAmt='||IntAmt);
		return IntAmt  ;
	end;
        Function CalcInt (LoanType varchar2, BfPrnBal number, PrnBal in out number, CalcIntFrom in out date, CalcIntTo in out date, StmtDate date,
LastStmtNo number, PerdPay in out number, Rkeep number, RkeepInt number, IntArrAmt in out number, LastCalcIntDate in out date, StartContDate in out date,
LastProcDate in out date, LastPerdPay in out number, PerdPayAll in out number, RegenRkeeing out boolean) return number
	is
	CountDateType		lnloanconstant.countdate_type%type;
	IntRoundNum		lnloanconstant.intround_num%type;
	IntRoundSumType	lnloanconstant.introundsum_type%type;
	IntRoundType		lnloanconstant.intround_type%type;
	IntRoundFormat	lnloanconstant.intround_format%type;
	FixPayCalcType		lnloanconstant.fixpaycal_type%type;
	IntRoundKpType	lnloanconstant.introundkp_type%type;
	GBfSts				lnloanconstant.genbf_status%type;
	CountDateFstType	lnloanconstant.countdatefst_type%type;
	IncludeIntArrSts	lnloanconstant.incluintarr_status%type;
	KpItemArrD			lnloanconstant.kpitemarrdouble_flag%type;
	PerdPayAmt			lncontmaster.period_payment%type;
	TmpPrnBal			lncontmaster.principal_balance%type;
	TmpPrnBal_			lncontmaster.principal_balance%type;
	IntAmt				lncontmaster.intpayment_amt%type;
	AddIntAmt			lncontmaster.intpayment_amt%type:=0;
	LoanPayType		lnloantype.loanpayment_type%type;
	PayRoundFactor	lnloantype.payround_factor%type;
	PayRoundType		lnloantype.payround_type%type;
	begin
				PerdPayAmt := LastPerdPay;
				dbms_output.put_line('test');
--		if (PrnBal = 0) or trunc(CalcIntTo) < trunc(CalcIntFrom) then return 0.00; end if;
        	GetInitVal(CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType, GBfSts, CountDateFstType, IntRoundType, FixPayCalcType, IncludeIntArrSts, KpItemArrD);
        	GetInitVal(LoanType, LoanPayType, PayRoundFactor, PayRoundType);
		if GBfSts = 1 then
            TmpPrnBal_:=BfPrnBal+PrnBal;
        		PerdPayAmt := CalcContrcPerdPay(LoanType, LoanPayType,TmpPrnBal_, StmtDate , IntRoundType, PayRoundType, FixPayCalcType, PayRoundFactor, TmpPrnBal_, PerdPayAll, LastPerdPay);
				dbms_output.put_line('PerdPayAmt='||PerdPayAmt);
		else
			null;
		end if;
		if (LastCalcIntDate is null) and (StartContDate is null) then
			StartContDate := StmtDate;
			LastCalcIntDate := StmtDate;
			LastProcDate := StmtDate;
		else
			null;
		end if;
		if (StartContDate is null) then StartContDate := LastCalcIntDate; end if;
		if CountDateFstType = 1 then
			if (LastStmtNo > 1) and  (LastCalcIntDate >= StartContDate) and (StmtDate > StartContDate) then
        			AddIntAmt := CalcInt (LoanType, PrnBal, StmtDate, StmtDate + 1, CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType);
				dbms_output.put_line('AddIntAmt='||AddIntAmt);
			else
				null;
			end if;
		end if;
		if (CountDateFstType = 1) and (LastStmtNo > 1)  and (LastCalcIntDate = StartContDate) and (StmtDate > StartContDate) then
			if (Rkeep > 0) or  (RkeepInt > 0) then
				if (KpItemArrD = 1) then
				dbms_output.put_line('RegenRkeeing');
					RegenRkeeing := True;
					CalcIntFrom := LastCalcIntDate;
					CalcIntTo := StmtDate;
					TmpPrnBal := BfPrnBal;
				else
					CalcIntFrom := StmtDate;
					CalcIntTo := LastProcDate;
					TmpPrnBal := PrnBal;
				end if;
 			elsif (LastCalcIntDate > StmtDate) then
				dbms_output.put_line('LastCalcIntDate > StmtDate');
					CalcIntFrom := StmtDate;
					CalcIntTo := LastCalcIntDate;
					TmpPrnBal := PrnBal;
 			else
					CalcIntFrom := LastCalcIntDate;
					CalcIntTo := StmtDate;
					TmpPrnBal := BfPrnBal;
			end if;
			IntAmt := CalcInt (LoanType, PrnBal, CalcIntFrom, CalcIntTo, 2, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType);
		else
			if (Rkeep > 0) or  (RkeepInt > 0) then
				if (KpItemArrD = 1) then
					RegenRkeeing := True;
					CalcIntFrom := LastCalcIntDate;
					CalcIntTo := StmtDate;
					if (StmtDate = LastCalcIntDate) then
						TmpPrnBal := 0;
					else
						TmpPrnBal := BfPrnBal;
					end if;
				else
					CalcIntFrom := StmtDate;
					CalcIntTo := LastProcDate;
					TmpPrnBal := PrnBal;
				end if;
 			elsif (LastCalcIntDate > StmtDate) then
					CalcIntFrom := StmtDate;
					CalcIntTo := LastCalcIntDate;
					TmpPrnBal := PrnBal;
 			else
					CalcIntFrom := LastCalcIntDate;
					CalcIntTo := StmtDate;
					if (StmtDate = LastCalcIntDate) then
						TmpPrnBal := 0;
					else
						TmpPrnBal := BfPrnBal;
					end if;
			end if;
			IntAmt := CalcInt (LoanType, PrnBal, CalcIntFrom, CalcIntTo, CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType);
		end if;
		if IntAmt <0 then IntAmt := 0; end if;

		select 	intround_type into	IntRoundType from lnloanconstant;

		IntAmt := IntAmt + AddIntAmt;
		IntAmt := roundmoney(IntAmt, IntRoundType);
		IntArrAmt := IntArrAmt+ IntAmt;
		LastCalcIntDate := CalcIntTo;
		LastPerdPay := PerdPayAmt;
		return IntAmt  ;
	end;
        Procedure UpdateLoanMaster(CoopID varchar2, ContNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, retVal OUT INTEGER )
	is
	TmpPrnBal		lncontmaster.principal_balance%type;
	TmpWDAmt		lncontmaster.withdrawable_amt%type;
	begin
		if Pkg_AtmMgmt.IsHaveLoanStmt(CoopID, ContNo, '001' , OperDate) then
			retVal := -1;	goto	CONTINUE;
		end if;
		retVal := Pkg_AtmMgmt.GetLoanMasterDt(CoopID, ContNo, TmpPrnBal, TmpWDAmt);dbms_output.put_line(' Get LnContMaster :'||TmpPrnBal||' W='||TmpWDAmt||'|'||retVal||'|');
		update atmconfig set x = 'N';
		case
			when OperCd ='002' or OperCd ='012'  then
				TmpPrnBal := TmpPrnBal + ItemAmt;
				TmpWDAmt := TmpWDAmt - ItemAmt;
			when OperCd ='003' or OperCd ='013'  then
				TmpPrnBal := TmpPrnBal - ItemAmt;
				TmpWDAmt := TmpWDAmt + ItemAmt;
		end case;
--		UPDATE 	LNCONTMASTER
--		SET 	PRINCIPAL_BALANCE = TmpPrnBal,
--			WithDrawable_amt = TmpWDAmt
--		WHERE 	trim(LOANCONTRACT_NO) = trim(Pkg_AtmMgmt.x(ContNo));
--		retVal := SQL%ROWCOUNT;
		Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, TmpWDAmt, TmpPrnBal, retVal);	dbms_output.put_line(' Update LnContMaster  : '||TmpWDAmt||'|'||retVal||'|');
		update atmconfig set x = 'Y';
		<<CONTINUE>> null;
	exception
 	  when no_data_found then  null;
	end;
        Procedure UpdateLoanWithDraw(CoopID varchar2, ContNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER )
	is
--	TmpPrnBal		lncontmaster.principal_balance%type;
--	TmpWDAmt		lncontmaster.withdrawable_amt%type;
	begin
		if Pkg_AtmMgmt.IsHaveLoanStmt(CoopID, ContNo, '001' , OperDate) then
			retVal := -1;	goto	CONTINUE;
		end if;
		retVal := Pkg_AtmMgmt.GetLoanMasterDt(CoopID, ContNo, PrnBal, WithDrawAmt);dbms_output.put_line(' Get LnContMaster :'||PrnBal||' W='||WithDrawAmt||'|'||retVal||'|');
		update atmconfig set x = 'N';
		case
			when OperCd ='002' or OperCd ='012'  then
--				PrnBal := PrnBal  + ItemAmt;
				WithDrawAmt := WithDrawAmt - ItemAmt;
			when OperCd ='003' or OperCd ='013'  then
--				PrnBal := PrnBal  - ItemAmt;
				WithDrawAmt := WithDrawAmt + ItemAmt;
		end case;
		Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, WithDrawAmt, PrnBal, retVal);	dbms_output.put_line(' Update LnContMaster  : '||WithDrawAmt||'|'||retVal||'|');
		update atmconfig set x = 'Y';
		<<CONTINUE>> null;
	exception
 	  when no_data_found then  null;
	end;
        Procedure UpdateLoanWithDrawAndAddSlip(CoopID varchar2, ContNo varchar2, MemNo varchar2, BankCd varchar2, BrandCd varchar2, AccNo varchar2, ToFromAccId varchar2, EntryId varchar2, AtmNo varchar2, AtmSeqNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER )
	is
	SlipNo		lncontstatement.ref_slipno%type;
	RefNo		lncontstatement.ref_docno%type;
	CoopBranch	varchar2(3) := '001';
	begin
		UpdateLoanWithDraw(CoopID, ContNo, OperDate, OperCd, ItemAmt, PrnBal, WithDrawAmt, retVal) ;
		if pkg_atmmgmt.IsUseSlip then
			Refno := substr(trim(AtmNo), 1, 15);
--			PKG_ATMMGMT.GENDOCNONEW('CMSLIPNO', 'SL', SlipNo, retVal);
--			Pkg_AtmMgmt.Process_AddLoanSlip(CoopID, MemNo, SlipNo,'LWD', CoopBranch, RefNo, OperDate, OperDate, BankCd, BrandCd, AccNo, ToFromAccId, ItemAmt, EntryId, sysdate, retVal );
--			Pkg_AtmMgmt.Process_AddShrLonSlip(CoopID, SlipNo, CoopBranch, 1, LoanType, ContNo, PerdPay, PerdPay, ItemAmt, CalcIntFrom, CalcIntTo, BfPerd, LastCalcIntDate, BfWithDrawAmt,	BfBalAmt, ContSts, ContIntType, RkeepPrn, RkeepInt, NkeepInt, CoopBranch, retVal );
		end if;
		retVal := SQL%ROWCOUNT;
	end;
        function GetSlipNo(CoopID varchar2, MemNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number) return varchar2
	is
	SlipNo	lncontstatement.ref_slipno%type;
--	RefNo		cmshrlonslip.document_no%type;
--	CoopBranch	varchar2(3) := '001';
	begin
		select ref_slipno into SlipNo from lncontstatement
		where
     -- member_no = MemNo and
					trim(ref_docno) = trim(AtmNo) and
					--sliptype_code = 'LWD' and
					operate_date = OperDate and
--					expense_branch = '999' and
					principal_payment = ItemAmt;
		return SlipNo;
		exception
 	  when no_data_found then  return null;
	end;
/*        Procedure Process_UpdateSlipSts(CoopID varchar2, ContNo varchar2, MemNo varchar2, BankCd varchar2, BrandCd varchar2, AccNo varchar2, ToFromAccId varchar2, EntryId varchar2, AtmNo varchar2, AtmSeqNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER )
	is
	SlipNo		cmshrlonslip.slip_no%type;
	RefNo		cmshrlonslip.document_no%type;
	CoopBranch	varchar2(3) := '001';
	begin
--		UpdateLoanWithDraw(CoopID, ContNo, OperDate, OperCd, ItemAmt, PrnBal, WithDrawAmt, retVal) ;
		if pkg_atmmgmt.IsUseSlip then
			Refno := substr(trim(AtmNo), 1, 15);
			Pkg_AtmMgmt.GenDocNo('CMSLIPNO', 'SL', SlipNo, retVal);
			Pkg_AtmMgmt.Process_AddLoanSlip(CoopID, MemNo, SlipNo, CoopBranch, RefNo, OperDate, OperDate, BankCd, BrandCd, AccNo, ToFromAccId, ItemAmt, EntryId, sysdate, retVal );
--			Pkg_AtmMgmt.Process_AddShrLonSlip(CoopID, SlipNo, CoopBranch, 1, LoanType, ContNo, PerdPay, PerdPay, ItemAmt, CalcIntFrom, CalcIntTo, BfPerd, LastCalcIntDate, BfWithDrawAmt,	BfBalAmt, ContSts, ContIntType, RkeepPrn, RkeepInt, NkeepInt, CoopBranch, retVal );
		end if;
		retVal := SQL%ROWCOUNT;
	end; */
        Procedure Process_UpdateSlipSts(CoopID varchar2, MemNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number, Sts integer, retVal OUT INTEGER)
	is
	SlipNo	lncontstatement.ref_docno%type;
	begin
		SlipNo := GetSlipNo(CoopID, MemNo, AtmNo,  OperDate, ItemAmt);  --GetSlipNo(CoopID, DeptType, DeptAccNo, AtmNo, OperDate, ItemAmt);
		--if SlipNo is not null then
		--	update	CMSHRLONSLIP
		--	set slip_status = Sts
		--	where	slip_no = SlipNo;
		--end if;
		retVal := SQL%ROWCOUNT;
	end;
/*        Procedure ReconcileLoan(CoopID varchar2, ContNo varchar2, MemNo varchar2, BankCd varchar2, BrandCd varchar2, AccNo varchar2, ToFromAccId varchar2, EntryId varchar2, AtmNo varchar2, AtmSeqNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER )
	is
--	SlipNo		cmshrlonslip.slip_no%type;
	RefNo		cmshrlonslip.document_no%type;
--	CoopBranch	varchar2(3) := '001';
	StartContDate date;
	LastProcDate date;
	LastCalcIntDate date;
	StmtDate date;
	LoanType char(2);
	StmtRefno varchar2(15);
	BFPrnBal number(15, 2);
	LastPerdPay number(3);
	PerdPay number(3);
	begin
		retVal := Pkg_AtmMgmt.GetLoanMasterDt(CoopID, ContNo, LoanType, LastStmtNo, PrnBal, WithDraw, ContSts, IntArrAmt, PerdPay,	PerdPayAll, ContIntType, RkeepPrn, RkeepInt, NkeepInt, LastProcDate, LastCalcIntDate, StartContDate, PaySts, PerdPayAmt, StartKeepDate);
		StmtDate := trunc(OperDate);
		StmtRefno := trim(AtmNo)||trim(AtmSeqNo);
		Refno := substr(trim(AtmNo), 1, 15);
		BFPrnBal := PrnBal;
		LastStmtNo := LastStmtNo + 1;
		LastPerdPay := PerdPay;
--		EntryDate := sysdate;
		if (LastStmtNo = 1) or (PrnBal = 0) then
			StartContDate := StmtDate;
			LastProcDate := StmtDate;
			LastCalcIntDate := StmtDate;
		end  if;
		case
			when OperCd ='002' or OperCd ='012'  then
				LoanItemType := 'LRC';
				PrnBal := PrnBal + fin.Itemamt;
				WithDraw := WithDraw;
				PerdPay := 0;
-------------------CalcInt
				dbms_output.put_line(' ------------PrnBal = '||PrnBal||' PerdPayAmt='||PerdPayAmt||' StmtDate='||StmtDate||' RkeepPrn='||RkeepPrn);dbms_output.put_line(' ------------RkeepInt = '||RkeepInt||' IntArrAmt='||IntArrAmt||' LastCalcIntDate='||LastCalcIntDate||' StartContDate='||StartContDate);dbms_output.put_line(' ------------LastProcDate = '||LastProcDate||' PerdPayAmt='||PerdPayAmt);
				IntAmt := loan_mgmt.CalcInt (LoanType, BfPrnBal, ItemAmt, CalcIntFrom, CalcIntTo, StmtDate, LastStmtNo, PerdPay, RkeepPrn, RkeepInt, IntArrAmt, LastCalcIntDate, StartContDate, LastProcDate, PerdPayAmt, PerdPayAll, RegenRkeeping) ;
			when fin.operatecd ='003' or fin.operatecd ='013'  then
				LoanItemType := 'LPX';
				PrnBal := PrnBal - fin.Itemamt;
				WithDraw := WithDraw;
				PerdPay := PerdPay + 1;
		end case;
		if RegenRkeeping then
			ReGenPrnc := PrnBal;
			Pkg_AtmMgmt.Process_GetKPTempRecvDt(CoopID, ContNo, RecvPerd, KpTot, KpPrnc, KpInt, retVal);
			ReGenKpPrnc := KpTot - RegenInt;
				loan_mgmt.GetInitVal(CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType, GBfSts, CountDateFstType, IntRoundType, FixPayCalcType, IncludeIntArrSts, KpItemArrD);
			RegenInt := loan_mgmt.CalcInt (LoanType, ReGenPrnc, StmtDate, LastProcDate, CountDateType, IntRoundNum, IntRoundSumType, IntRoundFormat, IntRoundKpType);
				dbms_output.put_line(' ------------RegenInt = '||RegenInt);
			Pkg_AtmMgmt.Process_UpdateKPTEMPRECEIVEDET(CoopID, ContNo, RecvPerd, ReGenPrnc, ReGenKpPrnc, RegenInt, StmtDate, LastProcDate, LastCalcIntDate, retVal);
			Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt, StartContDate, StartKeepDate, 0, LastStmtNo, LastCalcIntDate, StmtDate, LastProcDate, StmtDate, ReGenKpPrnc, RegenInt, ReGenKpPrnc, RegenInt, retVal);
		else
			Pkg_AtmMgmt.Process_UpdateLNCONTMASTER(CoopID, ContNo, PrnBal, PerdPayAmt, WithDraw, IntArrAmt, StartContDate, StartKeepDate, 0, LastStmtNo, LastCalcIntDate, StmtDate, LastProcDate, StmtDate,null, null, null, null, retVal);
		end if;
				dbms_output.put_line(' ------------CalcIntFrom = '||CalcIntFrom||' ='||CalcIntTo);
		Pkg_AtmMgmt.Process_AddLoanSTMT(CoopID, ContNo, LastStmtNo, CoopBranch, LoanItemType, StmtDate, StmtRefno, PerdPay, fin.Itemamt, PrnBal, CalcIntFrom, CalcIntTo, IntAmt, IntArrAmt, C_CASHTYPE, 1, C_ENTRYID, sysdate, CoopBranch, retVal) ;
		if pkg_atmmgmt.IsUseSlip then
			pkg_atmmgmt.GenDocNo('CMSLIPNO', 'SL', SlipNo, retVal);
			Pkg_AtmMgmt.Process_AddLoanSlip(CoopID, fin.MemNo, SlipNo, CoopBranch, RefNo, StmtDate, StmtDate, fin.bankcd, fin.branchcd, fin.accno, C_TOFROMACCID, fin.itemamt, C_ENTRYID, sysdate, retVal );
			Pkg_AtmMgmt.Process_AddShrLonSlip(CoopID, SlipNo, CoopBranch, 1, LoanType, ContNo, PerdPay, PerdPay, fin.Itemamt, CalcIntFrom, CalcIntTo, BfPerd, LastCalcIntDate, BfWithDrawAmt,	BfBalAmt, ContSts, ContIntType, RkeepPrn, RkeepInt, NkeepInt, CoopBranch, retVal );
		end if;
		if pkg_atmmgmt.IsUseLoanRec then ---LoanRec
			pkg_atmmgmt.GenDocNo('LNRECEIVENO', 'RC', LoanRecNo, retVal);
			Pkg_AtmMgmt.Process_AddLoanRec(CoopID, LoanRecNo, CoopBranch, fin.MemNo, ContNo, LoanType, BfPerd, BfPrnAmt,BfLastCalIntDate, BfLastRecDate, BfLastProcDate, SharStkVal, IntAcc, StmtDate, fin.itemamt, RecPerdFlag, fin.bankcd, fin.branchcd,  fin.accno, ContIntType, C_ENTRYID, EntryDate, C_TOFROMACCID, retVal);
		end if;
--		UpdateLoanWithDraw(CoopID, ContNo, OperDate, OperCd, ItemAmt, PrnBal, WithDrawAmt, retVal) ;
		retVal := SQL%ROWCOUNT;
	end; */
END;

/
