
create or replace view V_SCO_ATMDEPT_MASTER as 
select deptaccount_no,depttype_code,member_no,withdrawable_amt,prncbal,deptclose_status,bank_accid,(select expense_accid from mbmembexpense where d.member_no=member_no) as saving_acc from dpdeptmaster d
--select * from dpdeptmaster 
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d,mbmembexpense m where 
 depttype_code='88' 
 --and withdrawable_amt > 50000 
--and deptclose_status=0 
and m.member_no=d.member_no and m.expense_accid is not null 
--and d.member_no='007632' 
)order by member_no asc,deptaccount_no asc;


create or replace view V_SCO_ATMDEPT_STATEMENT as 
select * from dpdeptstatement
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d,mbmembexpense m where 
 depttype_code='88' 
 --and withdrawable_amt > 50000 
--and deptclose_status=0 
and m.member_no=d.member_no and m.expense_accid is not null 
--and d.member_no='007632' 
)order by deptaccount_no asc,seq_no desc;


create or replace view V_SCO_ATMDEPT_SLIP as 
select * from dpdeptslip where deptaccount_no in (
select deptaccount_no from dpdeptmaster
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d,mbmembexpense m where 
 depttype_code='88' 
 --and withdrawable_amt > 50000 
--and deptclose_status=0 
and m.member_no=d.member_no and m.expense_accid is not null 
--and d.member_no='007632' 
))order by deptslip_no desc
;


create or replace view V_SCO_ATMDEPT_SLIPDET as 
select * from dpdeptslipdet where deptslip_no in (
select deptslip_no from dpdeptslip where deptaccount_no in (
select deptaccount_no from dpdeptmaster
where deptaccount_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.deptaccount_no 
 from dpdeptmaster d,mbmembexpense m where 
 depttype_code='88' 
 --and withdrawable_amt > 50000 
--and deptclose_status=0 
and m.member_no=d.member_no and m.expense_accid is not null 
--and d.member_no='007632' )
)))order by deptslip_no desc
;


create or replace view V_SCO_ATMLOAN_MASTER as 
select loancontract_no,loantype_code,member_no,withdrawable_amt,loanapprove_amt,principal_balance,(loanapprove_amt-principal_balance) as loan_amt,contract_status ,expense_accid,(select expense_accid from mbmembexpense where l.member_no=member_no) as saving_acc  from lncontmaster l 
--select * from lncontmaster 
--select * from lncontstatement
--update lncontmaster set contract_status=1
where loancontract_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.loancontract_no 
 from lncontmaster d,mbmembexpense m where 
loantype_code='88'  
--and withdrawable_amt > 50000  
--and contract_status =1 
and m.expense_accid is not null 
--and d.member_no='004566' 
)order by member_no asc,loancontract_no asc ;


create or replace view V_SCO_ATMLOAN_STATEMENT as 
--select * from lncontmaster 
select * from lncontstatement
--update lncontmaster set contract_status=1
where loancontract_no in (
--select m.expense_accid,m.expense_bank,d.* 
select d.loancontract_no 
 from lncontmaster d,mbmembexpense m where 
loantype_code='88'  
--and withdrawable_amt > 50000  
--and contract_status =1 
and m.expense_accid is not null 
--and d.member_no='004566'
) 
--and entry_id like 'ATM%'
order by loancontract_no asc,seq_no desc
;


create or replace view V_SCO_ATMLOAN_SLIP as 
select * from CMSHRLONSLIP where 
--trim(member_no)='004566' and 
entry_id like 'ATM%'  
--กรณีถอนเงิน
--and sliptype_code ='LWD' 
--กรณีชำระ
--and sliptype_code ='PX'
order by member_no asc,slip_no desc
;

create or replace view V_SCO_ATMLOAN_SLIPDET as 
select * from CMSHRLONSLIPDET where slip_no in (
  select slip_no from CMSHRLONSLIP where 
  --trim(member_no)='004566' and 
  entry_id like 'ATM%' 
  --กรณีถอนเงิน
  --and stm_itemtype='LRC'
  --กรณีชำระ
  --and stm_itemtype='LPX'
  )order by slip_no desc;
  
  
create or replace view V_SCO_ATMLOAN_SLIPRECEIVE as 
select * from CMLOANRECEIVE where 
--trim(member_no)='004566' and 
entry_id like 'ATM%'
order by member_no asc,loanreceive_no desc ;


CREATE OR REPLACE FORCE VIEW "V_CHECK_ATMTRAN_DIFF_STMT_DEPT" AS 
  select p."CNT_ATM",p."OPERATE_DATE_",p."CNT_COOP",(cnt_atm-cnt_coop) as cnt_diff from ( 
  select a.* ,  (select count(*) from dpdeptstatement where entry_id='ATMKTB' and to_char(operate_date,'yyyymmdd')=operate_date_) as cnt_coop 
  from (  select count(*) as cnt_atm,operate_date_  from   ( 
  select t.*,to_char(operate_date,'yyyymmdd') as operate_date_ from atmtransaction t where system_code='02'  and item_status>=0 
  )  group by operate_date_ ) a   ) p where cnt_atm<> cnt_coop;
 

CREATE OR REPLACE FORCE VIEW "V_CHECK_ATMTRAN_DIFF_STMT_LOAN" AS 
  select p."CNT_ATM",p."OPERATE_DATE_",p."CNT_COOP",(cnt_atm-cnt_coop) as cnt_diff from ( 
  select a.* ,  (select count(*) from lncontstatement where entry_id='ATMKTB' and to_char(operate_date,'yyyymmdd')=operate_date_) as cnt_coop 
  from (  select count(*) as cnt_atm,operate_date_  from   ( 
  select t.*,to_char(operate_date,'yyyymmdd') as operate_date_ from atmtransaction t where system_code='01'  and item_status>=0 
  )  group by operate_date_ ) a   ) p where cnt_atm<> cnt_coop;
 

CREATE OR REPLACE FORCE VIEW "V_CHECK_ATMTRAN_DIFF_DEPT" AS 
select to_char(operate_date,'yyyymmdd') as operate_date_,t.* from atmtransaction t where system_code='02' and item_status>=0 
--and to_char(operate_date,'yyyymmdd')='20160721' 
and trim(atm_no)||trim(atm_seqno) not in (
 select trim(machine_id) from dpdeptstatement where entry_id='ATMKTB' 
 --and to_char(operate_date,'yyyymmdd')='20160721' 
);

CREATE OR REPLACE FORCE VIEW "V_CHECK_ATMTRAN_DIFF_LOAN" AS 
select to_char(operate_date,'yyyymmdd') as operate_date_,t.* from atmtransaction t where system_code='01' and item_status>=0 
--and to_char(operate_date,'yyyymmdd')='20160721' 
and trim(atm_no)||trim(atm_seqno) not in (
 select trim(ref_docno) from lncontstatement where entry_id='ATMKTB' 
 --and to_char(operate_date,'yyyymmdd')='20160721' 
);

create or replace view v_check_atmtran_sco_diff as 
select t.* from (
select t.*,
case system_code when '02' then
 (select count(trim(machine_id)) from dpdeptstatement where entry_id='ATMKTB' and trim(machine_id)=trim(atm_no)||trim(atm_seqno) )
 else 
  (select count(trim(ref_docno)) from lncontstatement where entry_id='ATMKTB' and trim(ref_docno)=trim(atm_no)||trim(atm_seqno) )
 end as cnt
 from atmtransaction t where item_status >=0 
 --where to_char(t.operate_date,'yyyymmdd')='20160721' 
 ) t where t.cnt =0 
;