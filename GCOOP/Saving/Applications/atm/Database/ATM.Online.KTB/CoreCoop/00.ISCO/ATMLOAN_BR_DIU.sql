--------------------------------------------------------
--  File created - Tuesday-August-02-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger ATMLOAN_BR_DIU
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ATMLOAN_BR_DIU" 
  BEFORE INSERT OR UPDATE
  ON ATMLOAN

  for each row

/* ERwin Builtin 16 ?????? 2555 22:48:59 */
/* default body for ATMLOAN_BR_DIU */
DECLARE NUMROWS INTEGER;
 credit_amt LNCONTMASTER.withdrawable_amt%type;
 prncbal LNCONTMASTER.PRINCIPAL_BALANCE%type;
BEGIN

 if INSERTING then
  if (:new.post_flag <>'D' ) then 
	:new.post_flag := 'A';
  end if;
 end if;

 if updating('saving_acc') and (:new.saving_acc <> :old.saving_acc) then
  if (:new.post_flag <>'D' ) then 
	:new.post_flag := 'U';
  end if;
 end if;

 if UPDATING then
	if(:new.receive_amt <0) then
     :new.receive_amt :=0;
     select WithDrawable_amt ,PRINCIPAL_BALANCE
     into credit_amt,prncbal
     from LNCONTMASTER
     where trim(loancontract_no) = trim(:new.contract_no);
     :new.CREDIT_AMT :=credit_amt;
     :new.balance_amt :=prncbal;
  end if;
	if(:new.pay_amt <0) then
     :new.pay_amt :=0;
     select WithDrawable_amt ,PRINCIPAL_BALANCE
     into credit_amt,prncbal
     from LNCONTMASTER
     where trim(loancontract_no) = trim(:new.contract_no);
     :new.CREDIT_AMT :=credit_amt;
     :new.balance_amt :=prncbal;
  end if;
 end if;

END;
/
ALTER TRIGGER "ATMLOAN_BR_DIU" ENABLE;
