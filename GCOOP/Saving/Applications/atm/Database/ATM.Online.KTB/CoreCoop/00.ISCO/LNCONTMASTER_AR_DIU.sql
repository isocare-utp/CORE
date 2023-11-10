--------------------------------------------------------
--  File created - Monday-July-18-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger LNCONTMASTER_AR_DIU
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "LNCONTMASTER_AR_DIU" 
  AFTER  DELETE OR INSERT OR UPDATE OF
         LOANTYPE_CODE,
         LOANAPPROVE_AMT,
         PRINCIPAL_BALANCE,
         CONTRACT_STATUS,
         WITHDRAWABLE_AMT,
         EXPENSE_ACCID
  ON LNCONTMASTER

  for each row
       WHEN (new.loantype_code = '88') DECLARE NUMROWS INTEGER;
memnm	mbmembmaster.memb_name%type;
memsnm	mbmembmaster.memb_surname%type;
person	mbmembmaster.card_person%type;
a	char(1);
x1 	char(1);
retVal 	integer;
AccNo	LNCONTMASTER.expense_accid%type;
CrLimit	LNCONTMASTER.LOANAPPROVE_AMT%type;
BEGIN
  select x into x1 from atmconfig;
  if x1 = 'Y' then
	if INSERTING OR UPDATING('LOANAPPROVE_AMT') OR  UPDATING('PRINCIPAL_BALANCE') OR  UPDATING('CONTRACT_STATUS') OR  UPDATING('LOANTYPE_CODE') OR
		UPDATING('WITHDRAWABLE_AMT') then
		if :old.loantype_code <> PKG_ATMMGMT.LOANTYPE and  :new.loantype_code = PKG_ATMMGMT.LOANTYPE  then
			a := 'A';
		elsif (:old.loantype_code = PKG_ATMMGMT.LOANTYPE and  :new.loantype_code <> PKG_ATMMGMT.LOANTYPE) or (:new.contract_status < 1) then
			a:= 'H';
		end if;
		PKG_ATMMgmt.GetMember('', :NEW.member_no, memnm, memsnm, person, retVal );
		if :NEW.expense_accid is null then
			AccNo := '';--pkg_atmMgmt.GetAccNo('', '', :NEW.member_no);
		else
			AccNo := :NEW.expense_accid;
		end if;
		CrLimit := :NEW.LOANAPPROVE_AMT - :NEW.PRINCIPAL_BALANCE;
		if CrLimit < 0 then
			CrLimit := 0;
		End if;
			dbms_output.put_line(' No : |'||CrLimit||'|');
    if AccNo is not null then
		INSERT INTO ATM_FIN_AMT (SEQNO, REFNO, FINTYPE, ACCNO, ITEMTYPE, ITEMAMT, OPERATEDATE, MEMNO, MEMNM, MEMSNM, PERSONID, BALAMT, STS, ISHOLD)
		VALUES (ATMFINAMT_SEQ.NEXTVAL, :NEW.LOANCONTRACT_NO, 'L', AccNo, :new.loantype_code, CrLimit, SYSDATE,
			:NEW.member_no, memnm, memsnm, person, :NEW.PRINCIPAL_BALANCE, :new.contract_status, a);
    end if;
	elsif DELETING then
		a := 'D';
		PKG_ATMMgmt.GetMember('', :OLD.member_no, memnm, memsnm, person, retVal );
		if :old.expense_accid is null then
			AccNo := pkg_atmMgmt.GetAccNo('', '', :old.member_no);
		else
			AccNo := :old.expense_accid;
		end if;
		CrLimit := :OLD.LOANAPPROVE_AMT - :OLD.PRINCIPAL_BALANCE;
		if CrLimit < 0 then
			CrLimit := 0;
		End if;
		INSERT INTO ATM_FIN_AMT (SEQNO, REFNO, FINTYPE, ACCNO, ITEMTYPE, ITEMAMT, OPERATEDATE, MEMNO, MEMNM, MEMSNM, PERSONID, BALAMT, STS, ISHOLD)
		VALUES (ATMFINAMT_SEQ.NEXTVAL, :OLD.LOANCONTRACT_NO, 'L', AccNo, :OLD.loantype_code, CrLimit, SYSDATE, :OLD.member_no, memnm, memsnm, person, :OLD.PRINCIPAL_BALANCE, :OLD.contract_status, a);
	end if;
  end if;
EXCEPTION
  WHEN NO_DATA_FOUND THEN
    NULL; 
  WHEN ZERO_DIVIDE THEN
    NULL; 
  WHEN OTHERS THEN
    NULL; 
END;
/
ALTER TRIGGER "LNCONTMASTER_AR_DIU" ENABLE;
