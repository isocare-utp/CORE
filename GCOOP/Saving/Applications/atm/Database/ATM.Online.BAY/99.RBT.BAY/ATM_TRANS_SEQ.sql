--------------------------------------------------------
--  File created - Tuesday-June-21-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Sequence ATM_TRANS_SEQ
--------------------------------------------------------

CREATE SEQUENCE  ATMSAVINGACC_SEQ  MINVALUE 1 INCREMENT BY 1 START WITH 1 CACHE 20  ;
CREATE SEQUENCE  ATM_TRANS_SEQ  MINVALUE 1 INCREMENT BY 1 START WITH 1 CACHE 20  ;

create or replace procedure reset_seq( p_seq_name in varchar2 )
is
    l_val number;
begin
    execute immediate
    'select ' || p_seq_name || '.nextval from dual' INTO l_val;

    execute immediate
    'alter sequence ' || p_seq_name || ' increment by -' || l_val || ' minvalue 0';

    execute immediate
    'select ' || p_seq_name || '.nextval from dual' INTO l_val;

    execute immediate
    'alter sequence ' || p_seq_name || ' increment by 1 minvalue 0';
end;
/
ALTER procedure reset_seq COMPILE ;
begin reset_seq('ATM_TRANS_SEQ'); end;
/
begin reset_seq('ATMFINAMT_SEQ'); end;
/
begin reset_seq('ATMSAVINGACCHIS_SEQ'); end;
/
