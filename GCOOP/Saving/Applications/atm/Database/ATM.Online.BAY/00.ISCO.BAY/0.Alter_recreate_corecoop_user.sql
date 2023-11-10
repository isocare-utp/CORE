alter system set deferred_segment_creation = FALSE ;
alter session set "_ORACLE_SCRIPT"=true;
ALTER PROFILE DEFAULT LIMIT PASSWORD_LIFE_TIME UNLIMITED;

grant select on dba_jobs to sys;
grant execute  on dbms_job to sys;
grant select on dba_jobs to system;
grant execute  on dbms_job to system;
grant select on dba_jobs to CORECOOP;
grant execute  on dbms_job to CORECOOP;

SELECT  'SYS.DBMS_IJOB.BROKEN('||job||',TRUE); END;' FROM DBA_JOBS_RUNNING;
SELECT  'BEGIN dbms_ijob.remove('||job||'); END;' FROM DBA_JOBS_RUNNING;
--SELECT  '/'  FROM dual;
SELECT concat(concat(' ALTER SYSTEM KILL SESSION ''',concat(concat( s.sid,',' ),s.serial#) ),''' IMMEDIATE;') as sql  FROM   gv$session s JOIN DBA_JOBS_RUNNING p ON p.sid = s.sid ;
-- http://levicorp.com/2009/05/22/how-to-kill-the-running-job/
begin
 for i in (SELECT concat(concat(' ALTER SYSTEM KILL SESSION ''',concat(concat( s.sid,',' ),s.serial#) ),''' IMMEDIATE') as sql  FROM   gv$session s JOIN DBA_JOBS_RUNNING p ON p.sid = s.sid ) LOOP
  execute immediate i."SQL"||' ';
 end loop;
 end;
/

begin
 for i in (SELECT  'BEGIN dbms_ijob.remove('||job||'); END' as sql FROM DBA_JOBS_RUNNING ) LOOP
  execute immediate i."SQL"||' ';
 end loop;
end;
/

SELECT concat(concat(' ALTER SYSTEM KILL SESSION ''',concat(concat( s.sid,',' ),s.serial#) ),''' IMMEDIATE;')  sql  FROM   gv$session s JOIN gv$process p ON p.addr = s.paddr AND p.inst_id = s.inst_id WHERE   s.username='CORECOOP' ;

begin
 for i in (SELECT concat(concat(' ALTER SYSTEM KILL SESSION ''',concat(concat( s.sid,',' ),s.serial#) ),''' IMMEDIATE') as sql  FROM   gv$session s JOIN gv$process p ON p.addr = s.paddr AND p.inst_id = s.inst_id WHERE   s.username='CORECOOP' ) LOOP
  execute immediate i."SQL"||' ';
 end loop;
end;
/

Drop tablespace CORECOOP including contents and datafiles;
CREATE BIGFILE TABLESPACE "CORECOOP" DATAFILE 'd:\app\Administrator\oradata\gcoop\CORECOOP.DBF' 
SIZE 50M AUTOEXTEND ON NEXT 50M MAXSIZE UNLIMITED LOGGING EXTENT MANAGEMENT 
LOCAL SEGMENT SPACE MANAGEMENT AUTO;

DROP USER "CORECOOP" CASCADE;
CREATE USER "CORECOOP" PROFILE "DEFAULT" IDENTIFIED BY "corecoop" ACCOUNT UNLOCK 
DEFAULT TABLESPACE CORECOOP;
GRANT UNLIMITED TABLESPACE TO "CORECOOP" WITH ADMIN OPTION;
GRANT "AQ_ADMINISTRATOR_ROLE" TO "CORECOOP" WITH ADMIN OPTION;
GRANT "CONNECT" TO "CORECOOP" WITH ADMIN OPTION;
GRANT "DBA" TO "CORECOOP" WITH ADMIN OPTION;
grant IMP_FULL_DATABASE to CORECOOP;
alter profile "DEFAULT" limit password_life_time UNLIMITED;

CREATE OR REPLACE DIRECTORY DATAPUMP AS 'C:\AtmCoreCoop\Released\DB';

--ALTER SYSTEM SET job_queue_processes = 0;
