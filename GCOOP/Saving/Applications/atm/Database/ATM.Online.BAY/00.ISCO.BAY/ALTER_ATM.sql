
alter table "ATMDEPT" add ("SEQUEST_AMT" number(15,2) default 0 not null );
alter table "ATMDEPT" add ("DEPTTYPE_CODE" char(2) default '88' not null );
alter table "ATMLOAN" add ("LOANTYPE_CODE" char(2) default '88' not null );
alter table "COOP" add ("BANK_POSTFIX" char(2) default '00' not null );
alter table "COOP" add ("COOPHOLD_DATE" date null );
alter table "ATMTOBANKHISTORY" modify ("SAVING_ACC" varchar2(15));
alter table "ATMTRANSACTION" modify ("SAVING_ACC" varchar2(15));
alter table "ATMDEPT" modify ("DEPTTYPE_CODE" char(2) default '88');
alter table "ATMLOAN" modify ("LOANTYPE_CODE" char(2) default '88' );