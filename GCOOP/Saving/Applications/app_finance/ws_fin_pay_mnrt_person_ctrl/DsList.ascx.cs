using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Saving.Applications.app_finance.ws_fin_pay_mnrt_person_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYOUTDETDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYOUTDET;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }

        // old use for wrt
        public void Retrieve(string member_no)
        {
            //string sql = @"select * from mbmoneyreturn where return_status=0 and member_no={1} and coop_id={0}";
            string sql = @"select coop_id ,member_no ,
 returnitemtype_code as itempaytype_code ,
0 as pay_recv_status ,return_amount as itempay_amt ,
return_amount as item_amtnet,
description as payment_desc, 1 as payment_status ,
1 as member_flag,seq_no,loancontract_no
from mbmoneyreturn where return_status=0 and member_no={1} and coop_id={0}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }


        public void RetrievePrnInt(string member_no) {
            
//            select mrt.memb_no,FT_MEMNAME( mrt.coop_id,mrt.memb_no),mg.manager_group,cb.bank_desc,so.expense_accid,so.moneytype_code,so.payoutslip_no,mrt.cont_no,so.slip_date,mrt.prn_amt,mrt.int_amt,lt.loangroup_code
//,'                                                                                                                                              ' as FIX_COOPNAME
//from (    
//       select     mr.coop_id ,mr.return_slippayoutno,mr.system_from ,mr.loancontract_no as cont_no,mr.member_no as memb_no, sum(decode(mr.returnitemtype_code,'PRN',mr.return_amount,0)) as prn_amt,sum(decode(mr.returnitemtype_code,'INT',mr.return_amount,0)) as int_amt
//       from mbmoneyreturn mr
//       where mr.return_status=1
//       and mr.returnitemtype_code in ('PRN','INT')
//       group by    loancontract_no,member_no,coop_id,system_from,return_slippayoutno
//   ) mrt ,slslippayout so,mbmembmaster mb,mbucfmembgroup mg,cmucfbank cb,lnloantype lt,lncontmaster ln
//where mrt.coop_id= :as_coopid
//and so.operate_date = :adtm_operate
//and mrt.return_slippayoutno = so.payoutslip_no
//and mrt.coop_id = so.coop_id
//and mrt.memb_no=so.member_no
//and mb.member_no=so.member_no
//and mb.membgroup_code = mg.membgroup_code
//and so.expense_bank = cb.bank_code(+)
//and mrt.cont_no = ln.loancontract_no
//and lt.loantype_code = ln.loantype_code 
            string sql = @"select coop_id ,member_no ,'MRL',coop_id as concoop_id,'ต้นคืนดอกเบี้ยคืน' as slipitem_desc,
sum(decode(returnitemtype_code,'PRN',return_amount,0)) as principal_payamt,
sum(decode(returnitemtype_code,'INT',return_amount,0)) as interest_payamt,
sum(decode(returnitemtype_code,'PRN',seq_no,0) )as seq_prn,
sum(decode(returnitemtype_code,'INT',seq_no,0)) as seq_int,
loancontract_no
from mbmoneyreturn 
where return_status=8 and returnitemtype_code in ('PRN','INT')and member_no={1} and coop_id={0}
group by coop_id ,member_no ,loancontract_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId,member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        
        }
    }
}