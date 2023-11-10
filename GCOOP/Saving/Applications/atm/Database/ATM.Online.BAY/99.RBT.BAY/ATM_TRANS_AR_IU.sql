--------------------------------------------------------
--  File created - Tuesday-August-02-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger ATM_TRANS_AR_IU
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ATM_TRANS_AR_IU" 
  AFTER INSERT OR UPDATE
  ON ATM_TRANS



/* ERwin Builtin 25 ?????? 2557 11:02:55 */
/* default body for ATM_TRANS_AR_IU */
DECLARE NUMROWS INTEGER;
        retVal integer;
BEGIN
	if PKG_ATMMgmt2.IsLinkValid('coopserv') then
		if inserting  then
       		PKG_ATMMgmt2.Process_UpdateLoanWithDraw(PKG_ATMMgmt2.C_COOPID, retVal);
       		PKG_ATMMgmt2.Process_UpdateDeptWithDraw(PKG_ATMMgmt2.C_COOPID, retVal);
		elsif  updating('TRANSSTS') then --or updating('RECONDATE') then
			PKG_ATMMgmt2.ReconcileLoan(PKG_ATMMgmt2.C_COOPID);
		  PKG_ATMMgmt2.ReconcileDept(PKG_ATMMgmt2.C_COOPID);
--	dbms_output.put_line(' ATM_TRANS_AR_IU :');
		end if;
	end if;
END;
/
ALTER TRIGGER "ATM_TRANS_AR_IU" ENABLE;
