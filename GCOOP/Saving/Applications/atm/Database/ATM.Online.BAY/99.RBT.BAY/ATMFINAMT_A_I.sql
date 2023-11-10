--------------------------------------------------------
--  File created - Monday-July-18-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger ATMFINAMT_A_I
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "ATMFINAMT_A_I" 
  AFTER INSERT
  ON ATM_FIN_AMT



/* ERwin Builtin 24 ?????? 2557 14:22:39 */
/* default body for ATMFINAMT_A_I */

DECLARE NUMROWS INTEGER;
        retVal integer;
BEGIN
	if PKG_ATMMgmt.IsLinkValid('ATMSERV') then
	        PKG_ATMMgmt2.Process_UpdateLoanWithDraw(PKG_ATMMgmt.C_COOPID, retVal);
	        PKG_ATMMgmt2.Process_UpdateDeptWithDraw(PKG_ATMMgmt.C_COOPID, retVal);
	        PKG_ATMMgmt.Process_PostLoanAmtLimit2ATM(PKG_ATMMgmt.C_COOPID, retVal);
	        PKG_ATMMgmt.Process_PostDeptAmtLimit2ATM(PKG_ATMMgmt.C_COOPID, retVal);
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
ALTER TRIGGER "ATMFINAMT_A_I" ENABLE;
