truncate table atm_trans;
truncate table atmact;
truncate table atmtransaction;
truncate table atmloan;
truncate table atmdept;
truncate table member;
truncate table atmtobankhistory;
truncate table atmsavingacchistory;
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
begin reset_seq('ATMSAVINGACC_SEQ'); end;
/
ALTER TRIGGER ATM_TRANS_AR_IU COMPILE;