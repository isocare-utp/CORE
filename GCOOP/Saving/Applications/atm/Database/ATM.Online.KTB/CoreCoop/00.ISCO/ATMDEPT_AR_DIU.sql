--------------------------------------------------------
--  File created - Tuesday-August-02-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger ATMDEPT_AR_DIU
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ATMDEPT_AR_DIU" 
  AFTER DELETE OR INSERT OR UPDATE
  ON ATMDEPT

  for each row

/* ERwin Builtin 19 ?????? 2555 11:20:39 */
/* default body for ATMDEPT_AR_DIU */
DECLARE NUMROWS INTEGER;
seq_no atmsavingacchistory.import_seq%type;
BEGIN
   dbms_output.put_line(' Trigger');
	if updating('saving_acc') and (:new.saving_acc <> :old.saving_acc) then
    select ATMSAVINGACC_SEQ.nextval into seq_no from dual;
		insert into atmsavingacchistory
			(import_file, import_seq, import_date, coop_id, member_no, contract_no, saving_acc_new, saving_acc_old, system_code)
			values ('ATMONLINE',seq_no , sysdate, :new.coop_id, :new.member_no, :new.coop_acc, :new.saving_acc, :old.saving_acc, '02');
	end if;
END;
/
ALTER TRIGGER "ATMDEPT_AR_DIU" ENABLE;
