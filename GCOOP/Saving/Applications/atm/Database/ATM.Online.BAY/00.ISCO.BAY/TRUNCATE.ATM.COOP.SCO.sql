truncate table atm_fin_amt;
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
truncate table atm_trans;
begin reset_seq('ATM_TRANS_SEQ'); end;
/
truncate table atm_fin_amt;
begin reset_seq('ATMFINAMT_SEQ'); end;
/
ALTER TRIGGER ATMFINAMT_A_I COMPILE;