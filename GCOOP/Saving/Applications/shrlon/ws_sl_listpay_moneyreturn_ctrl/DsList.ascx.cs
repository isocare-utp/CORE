using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_listpay_moneyreturn_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MONEYRETURNDataTable DATA { get; private set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.MONEYRETURN;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void RetrieveListRet(string member_no,string bank_code,string cash_type) {
            string sql = @"select mrt.coop_id,mrt.member_no,dbo.FT_GETMEMNAME(mrt.coop_id,mrt.member_no) as full_name,mrt.loancontract_no,
sum(mrt.ret_prn) as ret_prn , sum(mrt.ret_int) as ret_int,sum(mrt.ret_prn)+sum(mrt.ret_int) as sum_return,
sum(mrt.seq_prn) as seq_prn,sum(mrt.seq_int) as seq_int,
tr.moneytype_code,tr.bank_code,tr.bank_branch,tr.bank_accid,'LON' as from_system,'RET' as returnitemtype_code,
mrt.entry_date
from (select rt.coop_id,rt.member_no,rt.loancontract_no,
		(case when rt.returnitemtype_code='1' then 'PRN' when rt.return_amount =2 then ' ' else 0 end) as ret_prn,
		(case when rt.returnitemtype_code='1' then 'INT'when rt.return_amount =2  then ' ' else 0 end) as ret_int,
		(case when rt.returnitemtype_code='1' then 'PRN' when rt.seq_no =2  then ' ' else 0 end) as seq_prn,
		(case when rt.returnitemtype_code='1'  then 'INT' when rt.seq_no =2 then ' '  else 0 end ) as seq_int,
        rt.entry_date
		from mbmoneyreturn rt
		where rt.return_status=8
		and rt.returnitemtype_code in ('INT','PRN')
		and rt.member_no like '%" + member_no + @"%'
) mrt

left join mbmembmoneytr tr  on  tr.member_no=mrt.member_no
and tr.coop_id =mrt.coop_id
where 
 tr.trtype_code='RET'
and tr.bank_code like '%" + bank_code + @"'
and tr.moneytype_code like '%" + cash_type + @"'
group by  mrt.coop_id,mrt.member_no,mrt.loancontract_no,dbo.FT_GETMEMNAME(mrt.coop_id,mrt.member_no),
tr.moneytype_code,tr.bank_code,tr.bank_branch,tr.bank_accid,
mrt.entry_date
order by mrt.entry_date,mrt.member_no,mrt.loancontract_no ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdBank();
        }

        public void RetrieveListWrt(string member_no, string bank_code, string cash_type)
        {
            string sql = @"select mrt.coop_id,mrt.member_no,dbo.FT_GETMEMNAME(mrt.coop_id,mrt.member_no) as full_name,mrt.loancontract_no,
0 as ret_prn ,  0 as ret_int,0 as seq_prn , 0 as seq_int ,return_amount as sum_return,
tr.moneytype_code,tr.bank_code,tr.bank_branch,tr.bank_accid,'LON' as from_system,'WRT' as returnitemtype_code,
mrt.entry_date
from mbmoneyreturn mrt,mbmembmoneytr tr
where mrt.coop_id = tr.coop_id
and mrt.member_no =  tr.member_no
and mrt.returnitemtype_code='WRT'
and tr.bank_code like '%" + bank_code + @"'
and tr.moneytype_code like '%" + cash_type + @"'
and mrt.return_status=8
and mrt.member_no like '%" + member_no + @"%'
and tr.trtype_code='RET'
order by mrt.entry_date";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdBank();
        }

        public void RetrieveListAll(String member_no, string bank_code, string cash_type)
        {
            string sql = @"select *
from(
select mrt.coop_id,mrt.member_no,dbo.FT_GETMEMNAME(mrt.coop_id,mrt.member_no) as full_name,mrt.loancontract_no,
sum(mrt.ret_prn) as ret_prn , sum(mrt.ret_int) as ret_int,sum(mrt.ret_prn)+sum(mrt.ret_int) as sum_return,
sum(mrt.seq_prn) as seq_prn,sum(mrt.seq_int) as seq_int,
tr.moneytype_code,tr.bank_code,tr.bank_branch,tr.bank_accid,'LON' as from_system,'RET' as returnitemtype_code,
mrt.entry_date
from (select rt.coop_id,rt.member_no,rt.loancontract_no,
		(case when rt.returnitemtype_code='1' then 'PRN' when rt.return_amount =2 then ' ' else 0 end) as ret_prn,
		(case when rt.returnitemtype_code='1' then 'INT'when rt.return_amount =2  then ' ' else 0 end) as ret_int,
		(case when rt.returnitemtype_code='1' then 'PRN' when rt.seq_no =2  then ' ' else 0 end) as seq_prn,
		(case when rt.returnitemtype_code='1'  then 'INT' when rt.seq_no =2 then ' '  else 0 end ) as seq_int,
        rt.entry_date
		from mbmoneyreturn rt
		where rt.return_status=8
		and rt.returnitemtype_code in ('INT','PRN')
		and rt.member_no like '%" + member_no + @"%'
) mrt
left join mbmembmoneytr tr on tr.member_no=mrt.member_no
and tr.coop_id=mrt.coop_id
where 
 tr.trtype_code='RET'
and tr.bank_code like '%" + bank_code + @"'
and tr.moneytype_code like '%" + cash_type + @"'
group by  mrt.coop_id,mrt.member_no,mrt.loancontract_no,dbo.FT_GETMEMNAME(mrt.coop_id,mrt.member_no),
tr.moneytype_code,tr.bank_code,tr.bank_branch,tr.bank_accid,
mrt.entry_date

union 
select mrt.coop_id,mrt.member_no,dbo.FT_GETMEMNAME(mrt.coop_id,mrt.member_no) as memb_name,mrt.loancontract_no,
0 as ret_prn ,  0 as ret_int,mrt.return_amount as sum_return,0 as seq_prn , 0 as seq_int ,
tr.moneytype_code,tr.bank_code,tr.bank_branch,tr.bank_accid,'LON' as from_system,'WRT' as returnitemtype_code,
mrt.entry_date
from mbmoneyreturn mrt,mbmembmoneytr tr
where mrt.coop_id = tr.coop_id
and mrt.member_no =  tr.member_no
and mrt.returnitemtype_code='WRT'
and mrt.return_status=8
and mrt.member_no like '%" + member_no + @"%'
and tr.trtype_code='RET'
and tr.bank_code like '%" + bank_code + @"'
and tr.moneytype_code like '%" + cash_type + @"'
) mr
order by mr.entry_date,mr.member_no,mr.loancontract_no";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdBank();
        }

        public void DdBank()
        {
            string sql = @"select bank_code,bank_desc ,1 as sorter from cmucfbank
union
select '','',0 from dual order by sorter,bank_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }

        public void DdBankbranch(string bank_code)
        {
            string sql = @"select branch_id,branch_name ,1 as sorter from cmucfbankbranch where bank_code={0}
union
select '','',0 from dual order by sorter,branch_id";
            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_branch", "branch_name", "branch_id");
        }
    }
}