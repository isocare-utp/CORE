
BEGIN
    DBMS_SCHEDULER.CREATE_SCHEDULE (
    	   
        repeat_interval  => 'FREQ=SECONDLY;INTERVAL=10',     
        schedule_name  => '"ATM_POST2ATMTRANS"');
        
END;
/

BEGIN
    DBMS_SCHEDULER.CREATE_JOB (
            job_name => '"ATM_POST2ATMTRANSJOB"',
            schedule_name => '"ATM_POST2ATMTRANS"',
            job_type => 'PLSQL_BLOCK',
            job_action => 'BEGIN pkg_atmmgmt2.Post2ATMTrans; END;',
            number_of_arguments => 0,
            enabled => FALSE,
            auto_drop => TRUE,
            comments => '');

    DBMS_SCHEDULER.SET_ATTRIBUTE( 
             name => 'ATM_POST2ATMTRANSJOB', 
             attribute => 'logging_level', value => DBMS_SCHEDULER.LOGGING_RUNS);
  
    DBMS_SCHEDULER.enable(
             name => '"ATM_POST2ATMTRANSJOB"');
END;
/


--BEGIN
--    DBMS_SCHEDULER.CREATE_JOB (
--            job_name => '"SCO_POST2ATM"',
--            schedule_name => '"ATM_POST2ATMTRANS"',
--            job_type => 'PLSQL_BLOCK',
--            job_action => 'BEGIN  
--	if PKG_ATMMgmt.IsLinkValid(''ATMSERV'') then
--	        PKG_ATMMgmt2.Process_UpdateLoanWithDraw(PKG_ATMMgmt.C_COOPID, retVal);
--	        PKG_ATMMgmt2.Process_UpdateDeptWithDraw(PKG_ATMMgmt.C_COOPID, retVal);
--	        PKG_ATMMgmt.Process_PostLoanAmtLimit2ATM(PKG_ATMMgmt.C_COOPID, retVal);
--	        PKG_ATMMgmt.Process_PostDeptAmtLimit2ATM(PKG_ATMMgmt.C_COOPID, retVal);
--	end if;
-- END;',
--            number_of_arguments => 0,
--            enabled => FALSE,
--            auto_drop => TRUE,
--            comments => '');
--
--    DBMS_SCHEDULER.SET_ATTRIBUTE( 
--             name => 'SCO_POST2ATM', 
--             attribute => 'logging_level', value => DBMS_SCHEDULER.LOGGING_RUNS);
--  
--    DBMS_SCHEDULER.enable(
--             name => '"SCO_POST2ATM"');
--END;
--/
