--------------------------------------------------------
--  File created - Monday-July-18-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger DPDEPTMASTER_AR_DIU
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "DPDEPTMASTER_AR_DIU" 
 AFTER DELETE OR INSERT OR UPDATE OF
        WITHDRAWABLE_AMT,
        PRNCBAL,
        DEPTTYPE_CODE,
        DEPTCLOSE_STATUS,
        BANK_ACCID
  ON DPDEPTMASTER

  for each row
        WHEN (new.depttype_code = '88'
     -- KPC need to supporting multi coop_acc id
     --or new.depttype_code = '50'
     ) DECLARE NUMROWS INTEGER;
memnm mbmembmaster.memb_name%type;
memsnm mbmembmaster.memb_surname%type;
person mbmembmaster.card_person%type;
a char(1);
x1  char(1);
retVal  integer;
AccNo DPDEPTMASTER.bank_accid%type;
BEGIN
  select x into x1 from atmconfig;
  if x1 = 'Y' then
 if INSERTING OR UPDATING('WITHDRAWABLE_AMT') OR  UPDATING('PRNCBAL') OR  UPDATING('DEPTTYPE_CODE') OR  UPDATING('DEPTCLOSE_STATUS') OR  UPDATING('BANK_ACCID') then
  if :old.depttype_code <> PKG_ATMMGMT.DEPTTYPE and  :new.depttype_code = PKG_ATMMGMT.DEPTTYPE  then
   a := 'A';
  elsif (:old.depttype_code = PKG_ATMMGMT.DEPTTYPE and  :new.depttype_code <> PKG_ATMMGMT.DEPTTYPE) or (:new.deptclose_status = 1) then
   a:= 'H';
  end if;
  PKG_ATMMgmt.GetMember('', :NEW.member_no, memnm, memsnm, person, retVal );
  if :NEW.bank_accid is null then
   AccNo := pkg_atmMgmt.GetAccNo('', '', :NEW.member_no);
  else
   AccNo := :NEW.bank_accid;
  end if;
  if AccNo is not null then
  INSERT INTO ATM_FIN_AMT (SEQNO, REFNO, FINTYPE, ACCNO, ITEMTYPE, ITEMAMT, OPERATEDATE,
   MEMNO, MEMNM, MEMSNM, PERSONID, BALAMT, STS, ISHOLD) VALUES
   (ATMFINAMT_SEQ.NEXTVAL, :NEW.DEPTACCOUNT_NO, 'D', replace(AccNo, '-', ''),
   :NEW.depttype_code, :NEW.WITHDRAWABLE_AMT, SYSDATE,
   :NEW.member_no, memnm, memsnm, person, :NEW.PRNCBAL, decode(:NEW.deptclose_status, 1, 0, 1), a);
  end if;
 elsif DELETING then
  a := 'D';
  PKG_ATMMgmt.GetMember('', :OLD.member_no, memnm, memsnm, person, retVal );
  if :old.bank_accid is null then
   AccNo := '';--pkg_atmMgmt.GetAccNo('', '', :NEW.member_no);
  else
   AccNo := :old.bank_accid;
  end if;
  INSERT INTO ATM_FIN_AMT (SEQNO, REFNO, FINTYPE, ACCNO, ITEMTYPE, ITEMAMT, OPERATEDATE,
   MEMNO, MEMNM, MEMSNM, PERSONID, BALAMT, STS, ISHOLD) VALUES
   (ATMFINAMT_SEQ.NEXTVAL, :OLD.DEPTACCOUNT_NO, 'D', replace(AccNo, '-',''),
   :old.depttype_code, :old.WITHDRAWABLE_AMT, SYSDATE,
   :old.member_no, memnm, memsnm, person, :OLD.PRNCBAL, :old.deptclose_status, a);
 end if;
end if;
/*
EXCEPTION
  WHEN NO_DATA_FOUND THEN
    NULL; 
  WHEN ZERO_DIVIDE THEN
    NULL; 
  WHEN OTHERS THEN
    NULL;
	*/
END;
/
ALTER TRIGGER "DPDEPTMASTER_AR_DIU" ENABLE;
