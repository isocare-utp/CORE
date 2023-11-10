--------------------------------------------------------
--  File created - Tuesday-August-02-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger ATMTRANSACTION_AR_DIU
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ATMTRANSACTION_AR_DIU" 
  AFTER INSERT OR UPDATE OF
        ITEM_AMT,
        ITEM_STATUS
  ON ATMTRANSACTION

  for each row

/* ERwin Builtin 9 ?????? 2556 17:10:03 */
/* default body for ATMTRANSACTION_AR_DIU */
DECLARE NUMROWS INTEGER;
BEGIN
	dbms_output.put_line(' Trigger');
	if INSERTING then
		null;
-------------Move2schedule
/*		INSERT INTO ATM_TRANS  ( COOP_ID, SEQNO, OPERATEDATE, MEMNO, FINTYPE, OPERATECD, SYSTEMCD, BANKCD,
			BRANCHCD, ATMNO, ATMSEQNO, ACCNO, ITEMAMT, stampdate, TRANSSTS)  values
			(:new.coop_id, ATM_TRANS_SEQ.NEXTVAL, :new.operate_date, :new.member_no,
			decode(:new.system_code, '01', 'L', '02', 'D'), :new.operate_code, :new.system_code,
			:new.bank_code, :new.branch_code, :new.atm_no, :new.atm_seqno, :new.saving_acc,
			:new.item_amt, sysdate, decode(:new.item_status, 1, 'Y', -1, 'N', 'W')); */
	elsif UPDATING('ITEM_STATUS') then
		UPDATE 	ATM_TRANS
		SET	TRANSSTS = decode(:new.item_status, 1, 'Y', -1, 'N'),
			RECONDATE = :new.reconcile_date
		WHERE 	COOP_ID = :new.coop_id and
			trim(MEMNO) = trim(:new.member_no) and
			OPERATEDATE=:new.operate_date;
/*		select 	seqno
		into	NUMROWS
		from 	ATM_TRANS
		where	COOP_ID = :new.coop_id and
			MEMNO = :new.member_no and
			OPERATEDATE=:new.operate_date;

	dbms_output.put_line(' Posted : |'||NUMROWS||'|');
		UPDATE 	ATM_TRANS
		SET	TRANSSTS = decode(:new.item_status, 1, 'Y', -1, 'N'),
			RECONDATE = :new.reconcile_date
		WHERE 	SEQNO = NUMROWS;
			PKG_ATMMgmt2.Process_Loan('0104');	*/
	END IF;
	exception
 	  when no_data_found then
	dbms_output.put_line(' No Data : |'||to_char(:new.operate_date, 'DD/MM/YYYY hh:mm:ss')||'|');
END;
/
ALTER TRIGGER "ATMTRANSACTION_AR_DIU" ENABLE;
