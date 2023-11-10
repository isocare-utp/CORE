--------------------------------------------------------
--  File created - Tuesday-August-02-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package Body DEPT_MGMT
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "DEPT_MGMT" AS
        Procedure GetInitVal(DeptType varchar2, IntMeth out number, MinBal out number, CalcIntRound out number)
	is
	begin
		select 	nvl(int_method, 0), nvl(minprncbalnormint, 0), nvl(calint_round, 0)
		into		IntMeth, MinBal, CalcIntRound
		from 		dpdepttype
		where	depttype_code = DeptType;
	end;
        Procedure GetInitVal(CalcIntMeth out number, IntRoundMeth out number, IntRoundType out number, RoundIntPos out number, CountDateType out number, ChqIntMeth out number, AccUIntPos out number)
	is
	begin
		select nvl(calint_meth, 0), nvl(intround_meth, 0), nvl(intround_type, 0), nvl(roundint_pos, 4),
				case
					when nvl(countdate_type, 0) > 3 then 0
					else nvl(countdate_type, 0)
				end, checkint_meth,
				case
					when nvl(accuint_pos, 0) <= 0 then 2
					else nvl(accuint_pos, 0)
				end
		into	CalcIntMeth, IntRoundMeth, IntRoundType, RoundIntPos, CountDateType, ChqIntMeth, AccUIntPos
		from 	dpdeptconstant;
	end;
        Function RoundMoney (Money number, t integer) return number
	is
	Frac				integer;
	n					number(15,2);
	CalcIntMeth		dpdeptconstant.calint_meth%type;
	IntRoundMeth	dpdeptconstant.intround_meth%type;
	IntRoundType	dpdeptconstant.intround_type%type;
	RoundIntPos		dpdeptconstant.roundint_pos%type;
	CountDateType	dpdeptconstant.countdate_type%type;
	ChqIntMeth		dpdeptconstant.checkint_meth%type;
	AccuIntPos		dpdeptconstant.int_postacc%type;
	begin
		GetInitVal(CalcIntMeth, IntRoundMeth, IntRoundType, RoundIntPos, CountDateType, ChqIntMeth, AccuIntPos);
		n := Money;
		CASE t
			WHEN 0 THEN
				n := round(n, AccuIntPos);
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
			WHEN 2 THEN
				Frac := mod(n, 1)*100;
				if Frac >= 1 and Frac <= 10 then
					 Frac := 10;
				elsif Frac >= 11 and Frac <= 20 then
					 Frac := 20;
				elsif Frac >= 21 and Frac <= 30 then
					 Frac := 30;
				elsif Frac >= 31 and Frac <= 40 then
					 Frac := 40;
				elsif Frac >= 41 and Frac <= 50 then
					 Frac := 50;
				elsif Frac >= 51 and Frac <= 60 then
					 Frac := 60;
				elsif Frac >= 61 and Frac <= 70 then
					 Frac := 70;
				elsif Frac >= 71 and Frac <= 80 then
					 Frac := 80;
				elsif Frac >= 81 and Frac <= 90 then
					 Frac := 90;
				elsif Frac >= 91 and Frac <= 99 then
					 Frac := 100;
				else
					 Frac := 0;
				end if;
				n := trunc(n) + (Frac/100);
			WHEN 3 THEN
				Frac := mod(n, 1)*100;
				if  Frac > 0 then
					Frac := 1;
				else
					Frac := 0;
				end if;
				n := trunc(n) + Frac;
			WHEN 4 THEN --Check It
				Frac := to_number(substr(to_char(n, '999999999999999.999'), length(to_char(n, '999999999999999.999')), 1));
				if  Frac > 0 then
					n := 0.01;
				else
					n := 0;
				end if;
				n := trunc(Money) + n;
			WHEN 5 THEN  --Check It
				Frac := to_number(substr(to_char(n, '999999999999999.99'), length(to_char(n, '999999999999999.99')), 1));
--		dbms_output.put_line('Frac='||Frac);
				if  Frac > 5 then
					n := 0.01;
				else
					n := 0;
				end if;
				n := trunc(Money) + n;
			WHEN 6 THEN
				n := trunc(n, AccuIntPos) ;
			WHEN 7 THEN
				Frac := mod(n, 1)*100;
				if Frac >= 0 and Frac <= 24 then
					 Frac := 0;
				elsif Frac >= 25 and Frac <= 49 then
					 Frac := 25;
				elsif Frac >= 50 and Frac <= 74 then
					 Frac := 50;
				elsif Frac >= 75 and Frac <= 99 then
					 Frac := 75;
				else
					 Frac := 0;
				end if;
				n := trunc(n) + (Frac/100);
			WHEN 8 THEN
				Frac := mod(n, 1)*100;
				if Frac >= 0 and Frac <= 9 then
					 Frac := 0;
				elsif Frac >= 10 and Frac <= 19 then
					 Frac := 10;
				elsif Frac >= 20 and Frac <= 29 then
					 Frac := 20;
				elsif Frac >= 30 and Frac <= 39 then
					 Frac := 30;
				elsif Frac >= 40 and Frac <= 49 then
					 Frac := 40;
				elsif Frac >= 50 and Frac <= 59 then
					 Frac := 50;
				elsif Frac >= 60 and Frac <= 69 then
					 Frac := 60;
				elsif Frac >= 70 and Frac <= 79 then
					 Frac := 70;
				elsif Frac >= 80 and Frac <= 89 then
					 Frac := 80;
				elsif Frac >= 90 and Frac <= 99 then
					 Frac := 90;
				else
					 Frac := 0;
				end if;
				n := trunc(n) + (Frac/100);
			WHEN 9 THEN
				n := trunc(n);
		END CASE;
		return n;
	end;
        Function CalcInt (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number
	is
	IntMeth			dpdepttype.int_method%type;
	MinBal			dpdepttype.minprncbalnormint%type;
	CalcIntRound	dpdepttype.calint_round%type;
	IntAmt			number(15, 2);
	begin
		GetInitVal(DeptType, IntMeth, MinBal, CalcIntRound);
		IntAmt := CalcInt (DeptType, PrnBal, CalcIntFrom, CalcIntTo, IntMeth);
		return IntAmt  ;
	end;
        Function CalcInt (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date, IntMeth number) return number
	is
	DayInYear	number;
	IntAmt		number(15, 2);
	begin
		DayInYear := loan_mgmt.GetDayofYear(CalcIntFrom);
		if IntMeth = 1 then
			IntAmt := CalcIntDays (DeptType, PrnBal, CalcIntFrom, CalcIntTo) ;
		elsif IntMeth = 2 then
			IntAmt := CalcIntMonths (DeptType, PrnBal, CalcIntFrom, CalcIntTo) ;
		elsif IntMeth = 3 then
			IntAmt := CalcIntFix (DeptType, PrnBal, CalcIntFrom, CalcIntTo) ;
		elsif IntMeth = 4 then
			IntAmt := CalcIntDivd (DeptType, PrnBal, CalcIntFrom, CalcIntTo) ;
		else
			null;
		end if;
		return IntAmt  ;
	end;
        Function CalcInt (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date, SpcIntSts number, SpcIntRate number, ChqPend number) return number
	is
	IntAmt			number;
	CalcIntMeth		dpdeptconstant.calint_meth%type;
	IntRoundMeth	dpdeptconstant.intround_meth%type;
	IntRoundType	dpdeptconstant.intround_type%type;
	RoundIntPos		dpdeptconstant.roundint_pos%type;
	CountDateType	dpdeptconstant.countdate_type%type;
	ChqIntMeth		dpdeptconstant.checkint_meth%type;
	AccuInt			dpdeptconstant.accuint_pos%type;

	CalcintRound	dpdepttype.calint_round%type;
	IntMeth			dpdepttype.int_method%type;
	MinBal			dpdepttype.minprncbalnormint%type;
	BalCalcInt		number(17, 2);
	begin
		if (PrnBal = 0) or trunc(CalcIntTo) < trunc(CalcIntFrom) then return 0.00; end if;
		if (DeptType is null) or trim(DeptType) = '' then return 0.00; end if;
		GetInitVal(CalcIntMeth, IntRoundMeth, IntRoundType, RoundIntPos, CountDateType, ChqIntMeth, AccuInt);
		GetInitVal(DeptType, IntMeth, MinBal, CalcIntRound);
		if CalcintRound = 1 then
			BalCalcInt	:= trunc(PrnBal);
		else
			BalCalcInt	:= PrnBal;
		end if;
		if MinBal > PrnBal then
			IntAmt := 0;
		else
			if CalcIntTo > CalcIntFrom then
				if ChqIntMeth = 1 then
					BalCalcInt := BalCalcInt - ChqPend;
				end if;
				if  SpcIntSts = 1  then
					IntAmt := CalcInt( DeptType , BalCalcInt, CalcIntFrom, CalcIntTo, IntMeth );
				else
					IntAmt := CalcInt( DeptType , BalCalcInt, CalcIntFrom, CalcIntTo) ;
				end if;
				if IntAmt < 0 then IntAmt := 0; end if;
			else
				IntAmt := 0;
			end if;
		end if;
--		IntAmt := RoundMoney(IntAmt, IntRoundType);
		return IntAmt  ;
	end;
        Function CalcIntDays (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number
	is
	cursor c_IntRate(DeptType varchar2, CalcIntFrom date, CalcIntTo date) is
		select 	*
		from 		dpdeptintrate d1
		where 	depttype_code = DeptType and
					trunc(effective_date) >= (
					select 	max(trunc(effective_date))
					from 		dpdeptintrate d2
					where  	d2.depttype_code = d1.depttype_code and
					trunc(d2.effective_date) <= trunc(CalcIntFrom)) and
					trunc(effective_date) <= (
					select 	max(trunc(effective_date))
					from 		dpdeptintrate d2
					where  	d2.depttype_code = d1.depttype_code and
					trunc(d2.effective_date) <= trunc(CalcIntTo))
		order by depttype_code, effective_date, dept_step;
	type 	IntRateTab is table of dpdeptintrate%rowtype;
	intrate intratetab := intratetab();
	i	integer := 0;
	TYPE NumberTabType IS TABLE OF NUMBER  INDEX BY BINARY_INTEGER;
 	MinAmt NumberTabType;
	TYPE DateTabTyp IS TABLE OF DATE INDEX BY BINARY_INTEGER;
 	ExpDate DateTabTyp;
	DayNo NumberTabType;

	CalcIntMeth		dpdeptconstant.calint_meth%type;
	IntRoundMeth	dpdeptconstant.intround_meth%type;
	IntRoundType	dpdeptconstant.intround_type%type;
	RoundIntPos		dpdeptconstant.roundint_pos%type;
	CountDateType	dpdeptconstant.countdate_type%type;
	ChqIntMeth		dpdeptconstant.checkint_meth%type;
	AccuInt			dpdeptconstant.accuint_pos%type;

	IntAmt		number;
	TempIntAmt	number;
	DayInYear	number;
	StartDate	date;
	EndDate		date;
	TempAmt	number(15, 2);
	begin
		if trunc(CalcIntTo) < trunc(CalcIntFrom) then return 0.00; end if;
		DayInYear := loan_mgmt.GetDayofYear(CalcIntFrom);
--		DayNo := CalcIntTo - CalcIntFrom;
		GetInitVal(CalcIntMeth, IntRoundMeth, IntRoundType, RoundIntPos, CountDateType, ChqIntMeth, AccuInt);
		for n in c_IntRate(DeptType, CalcIntFrom, CalcIntTo) loop
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
					MinAmt(i) := intrate(i - 1).dept_step;
				end if;
			end if;
		end loop;
		TempAmt := PrnBal;
		IntAmt := 0;
		FOR i IN 1..intrate.COUNT LOOP
			TempIntAmt := 0;
			if TempAmt >= MinAmt(i) and TempAmt <= intrate(i).dept_step then
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
				if CalcIntMeth = 1 then
					TempIntAmt := (TempAmt*intrate(i).interest_rate)/DayInYear;
					if IntRoundMeth = 1 then
						TempIntAmt := roundmoney(TempIntAmt, IntRoundType);
					end if;
					TempIntAmt := TempIntAmt*dayno(i);
				else
					TempIntAmt := (TempAmt*intrate(i).interest_rate*dayno(i))/DayInYear;
				end if;
				TempIntAmt := round(TempIntAmt, RoundIntPos) ;
				if IntRoundMeth = 1 then
					TempIntAmt := roundmoney(TempIntAmt, IntRoundType) ;
				end if;
--				TempIntAmt := TempIntAmt + round(TempIntAmt, RoundIntPos);
				TempIntAmt := round(TempIntAmt, RoundIntPos);
				IntAmt := IntAmt + TempIntAmt;
				dbms_output.put_line('Day ='||dayno(i)||' '||intrate(i).effective_date||'=>'||ExpDate(i)||' S = '||StartDate||'=>'||EndDate||' Min = '||MinAmt(i)||'-'||intrate(i).dept_step||' Rate = '||intrate(i).interest_rate||' Interest = '||TempIntAmt);
			end if;
		END LOOP;
				dbms_output.put_line(IntAmt);
		IntAmt := roundmoney(IntAmt, IntRoundType);
		return IntAmt  ;
	end;
        Function CalcIntMonths (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number
	is
	cursor c_IntRate(DeptType varchar2, CalcIntFrom date, CalcIntTo date) is
		select 	*
		from 		dpdeptintrate d1
		where 	depttype_code = DeptType and
					trunc(effective_date) >= (
					select 	max(trunc(effective_date))
					from 		dpdeptintrate d2
					where  	depttype_code = DeptType and
					trunc(d2.effective_date) <= trunc(CalcIntFrom))
		order by depttype_code, effective_date, dept_step;
	type IntRateTab is table of dpdeptintrate%rowtype;
	intrate intratetab := intratetab();
	i	integer := 0;
	TYPE ntab IS TABLE OF NUMBER  INDEX BY BINARY_INTEGER;
	p ntab;
	ir	dpdeptintrate.interest_rate%type;
	PB		number;

	CalcIntMeth		dpdeptconstant.calint_meth%type;
	IntRoundMeth	dpdeptconstant.intround_meth%type;
	IntRoundType	dpdeptconstant.intround_type%type;
	RoundIntPos		dpdeptconstant.roundint_pos%type;
	CountDateType	dpdeptconstant.countdate_type%type;
	ChqIntMeth		dpdeptconstant.checkint_meth%type;
	AccuInt			dpdeptconstant.accuint_pos%type;

	PrnType			varchar2(10);
	IntAmt		number;
	DayInYear	number;
	DayNo	number;
	begin
		if trunc(CalcIntTo) < trunc(CalcIntFrom) then return 0.00; end if;
		DayInYear := loan_mgmt.GetDayofYear(CalcIntFrom);
		DayNo := CalcIntTo - CalcIntFrom;
		GetInitVal(CalcIntMeth, IntRoundMeth, IntRoundType, RoundIntPos, CountDateType, ChqIntMeth, AccuInt);
		for n in c_IntRate(DeptType, CalcIntFrom, CalcIntTo) loop
			i := i + 1;
			intrate.extend;
			intrate(i) := n;
			if i = 1 then
				p(i) := 0;
			else
				p(i) := intrate(i - 1).dept_step;
			end if;
--			dbms_output.put_line('loantype_code='||intrate(i).loantype_code||' Effective = '||intrate(i).effective_date||
--			' Loan = '||p(i)||' Loan Step = '||intrate(i).loan_step||' Interest Rate = '||intrate(i).interest_rate);
--			dbms_output.put_line('loantype_code='||p(i));
			if (PrnBal >= p(i) and PrnBal < intrate(i).dept_step)  then
				ir := intrate(i).interest_rate;
				dbms_output.put_line('DeptType='||intrate(i).depttype_code||' Effective = '||intrate(i).effective_date||
					' Dept = '||p(i)||' Dept Step = '||intrate(i).dept_step||' Interest Rate = '||intrate(i).interest_rate);
			end if;
		end loop;
		PB := round(PrnBal, 2);
		if CalcIntMeth = 1 then
			IntAmt := (PB*ir)/DayInYear;
			if IntRoundMeth = 1 then
				IntAmt := roundmoney(IntAmt, IntRoundType);
			end if;
			IntAmt := IntAmt*DayNo;
		else
			IntAmt := (PB*ir*DayNo)/DayInYear;
		end if;
		IntAmt := round(IntAmt, RoundIntPos) ;
		if IntRoundMeth = 1 then
			IntAmt := roundmoney(IntAmt, IntRoundType) ;
		end if;
		IntAmt := round(IntAmt, RoundIntPos) ;
		IntAmt := roundmoney(IntAmt, IntRoundType);
		return IntAmt  ;
	end;
        Function CalcIntFix (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number
	is
	cursor c_IntRate(DeptType varchar2, CalcIntFrom date, CalcIntTo date) is
		select 	*
		from 		dpdeptintrate d1
		where 	depttype_code = DeptType and
					trunc(effective_date) >= (
					select 	max(trunc(effective_date))
					from 		dpdeptintrate d2
					where  	depttype_code = DeptType and
					trunc(d2.effective_date) <= trunc(CalcIntFrom))
		order by depttype_code, effective_date, dept_step;
	type IntRateTab is table of dpdeptintrate%rowtype;
	intrate intratetab := intratetab();
	i	integer := 0;
	TYPE ntab IS TABLE OF NUMBER  INDEX BY BINARY_INTEGER;
	p ntab;
	ir	dpdeptintrate.interest_rate%type;
	PB		number;

	CalcIntMeth		dpdeptconstant.calint_meth%type;
	IntRoundMeth	dpdeptconstant.intround_meth%type;
	IntRoundType	dpdeptconstant.intround_type%type;
	RoundIntPos		dpdeptconstant.roundint_pos%type;
	CountDateType	dpdeptconstant.countdate_type%type;
	ChqIntMeth		dpdeptconstant.checkint_meth%type;
	AccuInt			dpdeptconstant.accuint_pos%type;

	IntAmt		number;
	DayInYear	number;
	DayNo	number;
	begin
		if trunc(CalcIntTo) < trunc(CalcIntFrom) then return 0.00; end if;
		DayInYear := loan_mgmt.GetDayofYear(CalcIntFrom);
		DayNo := CalcIntTo - CalcIntFrom;
		GetInitVal(CalcIntMeth, IntRoundMeth, IntRoundType, RoundIntPos, CountDateType, ChqIntMeth, AccuInt);
		for n in c_IntRate(DeptType, CalcIntFrom, CalcIntTo) loop
			i := i + 1;
			intrate.extend;
			intrate(i) := n;
			if i = 1 then
				p(i) := 0;
			else
				p(i) := intrate(i - 1).dept_step;
			end if;
--			dbms_output.put_line('loantype_code='||intrate(i).loantype_code||' Effective = '||intrate(i).effective_date||
--			' Loan = '||p(i)||' Loan Step = '||intrate(i).loan_step||' Interest Rate = '||intrate(i).interest_rate);
--			dbms_output.put_line('loantype_code='||p(i));
			if (PrnBal >= p(i) and PrnBal < intrate(i).dept_step)  then
				ir := intrate(i).interest_rate;
				dbms_output.put_line('DeptType='||intrate(i).depttype_code||' Effective = '||intrate(i).effective_date||
					' Dept = '||p(i)||' Dept Step = '||intrate(i).dept_step||' Interest Rate = '||intrate(i).interest_rate);
			end if;
		end loop;
		PB := round(PrnBal, 2);
		if CalcIntMeth = 1 then
			IntAmt := (PB*ir)/DayInYear;
			if IntRoundMeth = 1 then
				IntAmt := roundmoney(IntAmt, IntRoundType);
			end if;
			IntAmt := IntAmt*DayNo;
		else
			IntAmt := (PB*ir*DayNo)/DayInYear;
		end if;
		IntAmt := round(IntAmt, RoundIntPos) ;
		if IntRoundMeth = 1 then
			IntAmt := roundmoney(IntAmt, IntRoundType) ;
		end if;
		IntAmt := round(IntAmt, RoundIntPos) ;
		IntAmt := roundmoney(IntAmt, IntRoundType);
		return IntAmt  ;
	end;
        Function CalcIntDivd (DeptType varchar2, PrnBal number, CalcIntFrom date, CalcIntTo date) return number
	is
	cursor c_IntRate(DeptType varchar2, CalcIntFrom date, CalcIntTo date) is
		select 	*
		from 		dpdeptintrate d1
		where 	depttype_code = DeptType and
					trunc(effective_date) >= (
					select 	max(trunc(effective_date))
					from 		dpdeptintrate d2
					where  	depttype_code = DeptType and
					trunc(d2.effective_date) <= trunc(CalcIntFrom))
		order by depttype_code, effective_date, dept_step;
	type IntRateTab is table of dpdeptintrate%rowtype;
	intrate intratetab := intratetab();
	i	integer := 0;
	TYPE ntab IS TABLE OF NUMBER  INDEX BY BINARY_INTEGER;
	p ntab;
	ir	dpdeptintrate.interest_rate%type;
	PB		number;

	CalcIntMeth		dpdeptconstant.calint_meth%type;
	IntRoundMeth	dpdeptconstant.intround_meth%type;
	IntRoundType	dpdeptconstant.intround_type%type;
	RoundIntPos		dpdeptconstant.roundint_pos%type;
	CountDateType	dpdeptconstant.countdate_type%type;
	ChqIntMeth		dpdeptconstant.checkint_meth%type;
	AccuInt			dpdeptconstant.accuint_pos%type;

	IntAmt		number;
	DayInYear	number;
	DayNo	number;
	begin
		if trunc(CalcIntTo) < trunc(CalcIntFrom) then return 0.00; end if;
		DayInYear := loan_mgmt.GetDayofYear(CalcIntFrom);
		DayNo := CalcIntTo - CalcIntFrom;
		GetInitVal(CalcIntMeth, IntRoundMeth, IntRoundType, RoundIntPos, CountDateType, ChqIntMeth, AccuInt);
		for n in c_IntRate(DeptType, CalcIntFrom, CalcIntTo) loop
			i := i + 1;
			intrate.extend;
			intrate(i) := n;
			if i = 1 then
				p(i) := 0;
			else
				p(i) := intrate(i - 1).dept_step;
			end if;
--			dbms_output.put_line('loantype_code='||intrate(i).loantype_code||' Effective = '||intrate(i).effective_date||
--			' Loan = '||p(i)||' Loan Step = '||intrate(i).loan_step||' Interest Rate = '||intrate(i).interest_rate);
--			dbms_output.put_line('loantype_code='||p(i));
			if (PrnBal >= p(i) and PrnBal < intrate(i).dept_step)  then
				ir := intrate(i).interest_rate;
				dbms_output.put_line('DeptType='||intrate(i).depttype_code||' Effective = '||intrate(i).effective_date||
					' Dept = '||p(i)||' Dept Step = '||intrate(i).dept_step||' Interest Rate = '||intrate(i).interest_rate);
			end if;
		end loop;
		PB := round(PrnBal, 2);
		if CalcIntMeth = 1 then
			IntAmt := (PB*ir)/DayInYear;
			if IntRoundMeth = 1 then
				IntAmt := roundmoney(IntAmt, IntRoundType);
			end if;
			IntAmt := IntAmt*DayNo;
		else
			IntAmt := (PB*ir*DayNo)/DayInYear;
		end if;
		IntAmt := round(IntAmt, RoundIntPos) ;
		if IntRoundMeth = 1 then
			IntAmt := roundmoney(IntAmt, IntRoundType) ;
		end if;
		IntAmt := round(IntAmt, RoundIntPos) ;
		IntAmt := roundmoney(IntAmt, IntRoundType);
		return IntAmt  ;
	end;
        Procedure UpdateDeptMaster(CoopID varchar2, DeptAccNo varchar2, OperDate date, OperCd varchar2, ItemAmt number, retVal OUT INTEGER )
	is
	TmpPrnBal		dpdeptmaster.prncbal%type;
	TmpWDAmt		dpdeptmaster.withdrawable_amt%type;
	TmpWd		dpdeptmaster.withdrawable_amt%type;
	TmpDp		dpdeptmaster.withdrawable_amt%type;
	DeptCloseSts	dpdeptmaster.deptclose_status%type;
	BranchId			dpdeptmaster.coop_id%type;
	DeptType		varchar2(3);
	begin
		if Pkg_AtmMgmt.IsHaveDeptStmt(CoopID, DeptAccNo, DeptType, OperDate) then
			retVal := -1;	goto	CONTINUE;
		end if;
		retVal := Pkg_AtmMgmt.GetDeptMasterDt(CoopID, DeptAccNo, TmpPrnBal, TmpWDAmt, TmpWd, TmpDp, DeptCloseSts , BranchId);
		if DeptCloseSts = 1  then
			goto	CONTINUE;
		end if;
		update atmconfig set x = 'N';
		case
			when OperCd ='002'  then
				DeptType := 'WTE';
				TmpPrnBal := TmpPrnBal -  ItemAmt; TmpWDAmt := TmpWDAmt - ItemAmt;
				TmpWd := TmpWd -  ItemAmt;
--			when OperCd ='003' or OperCd ='013'  then
			else
				DeptType := 'DTE';
				TmpPrnBal := TmpPrnBal + ItemAmt;	TmpWDAmt := TmpWDAmt + ItemAmt;
				TmpDp :=TmpDp +  ItemAmt;
		end case;
		Pkg_AtmMgmt.Process_UpdateDeptMaster(CoopID, DeptAccNo, BranchId, TmpWDAmt, TmpPrnBal, TmpWd, TmpDp, retVal);
		update atmconfig set x = 'Y';
		<<CONTINUE>> null;
	exception
 	  when no_data_found then  null;
	end;
        Procedure UpdateDeptWithDraw(CoopID varchar2, DeptAccNo varchar2, OperDate date, OperCd varchar2, DeptItemType out varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER )
	is
	BranchId			dpdeptmaster.coop_id%type;
	TmpWd		dpdeptmaster.withdrawable_amt%type;
	TmpDp		dpdeptmaster.withdrawable_amt%type;
	DeptCloseSts	dpdeptmaster.deptclose_status%type;
--	DeptType		varchar2(3);
	begin
		if Pkg_AtmMgmt.IsHaveDeptStmt(CoopID, DeptAccNo, pkg_atmmgmt.DEPTTYPE, OperDate) then
			retVal := -1;	goto	CONTINUE;
		end if;
		retVal := Pkg_AtmMgmt.GetDeptMasterDt(CoopID, DeptAccNo, PrnBal, WithDrawAmt, TmpWd, TmpDp, DeptCloseSts , BranchId);
		if DeptCloseSts = 1  then
			retVal := -1;	goto	CONTINUE;
		end if;
		update atmconfig set x = 'N';
		case
			when OperCd ='002' or OperCd ='012'  then
				DeptItemType := 'WTE';
--				PrnBal := PrnBal  - ItemAmt;
				WithDrawAmt := WithDrawAmt - ItemAmt;
				TmpWd := TmpWd -  ItemAmt;
			else -- OperCd ='003' or OperCd ='013'  then
				DeptItemType := 'DTE';
--				PrnBal := PrnBal  + ItemAmt;
				WithDrawAmt := WithDrawAmt + ItemAmt;
				TmpDp :=TmpDp +  ItemAmt;
		end case;
		Pkg_AtmMgmt.Process_UpdateDeptMaster(CoopID, DeptAccNo, BranchId, WithDrawAmt, PrnBal, TmpWd, TmpDp, retVal);	dbms_output.put_line(' Update LnContMaster  : '||WithDrawAmt||'|'||retVal||'|');
		update atmconfig set x = 'Y';
		<<CONTINUE>> null;
	exception
 	  when no_data_found then  null;
	end;
        Procedure UpdateDeptWithDrawAndAddSlip(CoopID varchar2, DeptAccNo varchar2, ToFromAccId varchar2, EntryId varchar2, AtmNo varchar2, AtmSeqNo varchar2, CashType varchar2, OperDate date, OperCd varchar2, ItemAmt number, PrnBal out number, WithDrawAmt out number, retVal OUT INTEGER)
	is
	SlipNo	dpdeptslip.deptslip_no%type;
	RefNo		dpdeptslip.machine_id%type;
	DeptItemType	varchar2(3);
	BranchId		varchar2(3) := '001';
	BranchOrg	varchar2(3) := '001';
	DeptGrpCD	varchar2(2) := '00';
	begin
		UpdateDeptWithDraw(CoopID, DeptAccNo, OperDate, OperCd, DeptItemType, ItemAmt, PrnBal,  WithDrawAmt, retVal);
		RefNo := trim(AtmNo)||trim(AtmSeqNo);
--		if pkg_atmmgmt.IsUseSlip then
--			Refno := substr(trim(AtmNo), 1, 15);
			pkg_atmmgmt.GenDocNoNew('DPSLIPNO', '', SlipNo, retVal);
			Pkg_AtmMgmt.Process_AddDeptSlip(CoopID, SlipNo, DeptAccNo, pkg_atmmgmt.DEPTTYPE, OperDate, DeptItemType, ItemAmt, PrnBal, WithDrawAmt,
				EntryId, trunc(OperDate), RefNo, DeptItemType, null, null, null, DeptGrpCD, ToFromAccId, 'DEP', '', CashType, null, null, BranchId, BranchOrg, retVal);
--		end if;
		retVal := SQL%ROWCOUNT;
	end;
        function GetSlipNo(CoopID varchar2, DeptType varchar2, DeptAccNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number) return varchar2
	is
	SlipNo	dpdeptslip.deptslip_no%type;
	begin
		select deptslip_no into SlipNo from dpdeptslip
		where	depttype_code = DeptType and
					deptaccount_no = DeptAccNo and
					deptslip_date = OperDate and
					deptslip_amt = ItemAmt and
					trim(machine_id) = trim(AtmNo) and
					entry_id = 'ATMKTB';
		return trim(SlipNo);
		exception
 	  when no_data_found then  return null;
	end;
        Procedure UpdateDeptSlip(CoopID varchar2, DeptType varchar2, DeptAccNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number, PrncBal1 number, StmtDate date, StmtRefNo varchar2, CalcIntFrom date, CalcIntTo date, IntAmt number, AccIntAmt number, retVal OUT INTEGER)
	is
	SlipNo	dpdeptslip.deptslip_no%type;
	begin
		SlipNo := GetSlipNo(CoopID, DeptType, DeptAccNo, AtmNo, OperDate, ItemAmt);
		if SlipNo is not null then
			update	dpdeptslip
			set DPSTM_NO = StmtRefNo, CALINT_FROM = CalcIntFrom, CALINT_TO = CalcIntTo, INT_AMT = IntAmt, ACCUINT_AMT = AccIntAmt, prncbal = PrncBal1
			where	deptslip_no = SlipNo;
		end if;
		retVal := SQL%ROWCOUNT;
	end;
        Procedure Process_UpdateSlipSts(CoopID varchar2, DeptType varchar2, DeptAccNo varchar2, AtmNo varchar2, OperDate date, ItemAmt number, Sts integer, retVal OUT INTEGER)
	is
	SlipNo	dpdeptslip.deptslip_no%type;
	begin
		SlipNo := GetSlipNo(CoopID, DeptType, DeptAccNo, AtmNo, OperDate, ItemAmt);
		if SlipNo is not null then
			update	dpdeptslip
			set item_status = Sts
			where	deptslip_no = SlipNo;
		end if;
		retVal := SQL%ROWCOUNT;
	end;
END;

/
