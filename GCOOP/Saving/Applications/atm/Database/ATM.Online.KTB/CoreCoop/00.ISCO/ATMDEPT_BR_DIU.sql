--------------------------------------------------------
--  File created - Tuesday-August-02-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger ATMDEPT_BR_DIU
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ATMDEPT_BR_DIU" 
  BEFORE INSERT OR UPDATE
  ON ATMDEPT

  for each row

/* ERwin Builtin 16 ?????? 2555 22:48:37 */
/* default body for ATMDEPT_BR_DIU */
DECLARE NUMROWS INTEGER;
 withdrawable_amt DPDEPTMASTER.withdrawable_amt%type;
 prncbal DPDEPTMASTER.prncbal%type;
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
     select withdrawable_amt ,prncbal
     into withdrawable_amt,prncbal
     from DPDEPTMASTER
     where trim(deptaccount_no) = trim(:new.coop_acc) ;
     :new.withatm_amt :=withdrawable_amt;
     :new.balance_amt :=prncbal;
  end if;
	if(:new.pay_amt <0) then
     :new.pay_amt :=0;
     select withdrawable_amt ,prncbal
     into withdrawable_amt,prncbal
     from DPDEPTMASTER
     where trim(deptaccount_no) = trim(:new.coop_acc) ;
     :new.withatm_amt :=withdrawable_amt;
     :new.balance_amt :=prncbal;
  end if;
 end if;

END;
/
ALTER TRIGGER "ATMDEPT_BR_DIU" ENABLE;
