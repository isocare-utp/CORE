using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl
{
    public partial class DsCollall : DataSourceRepeater
    {
        public DataSet1.DT_COLLALLDataTable DATA { get; set; }

        public void InitDsCollall(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_COLLALL;
            this.EventItemChanged = "OnDsCollallItemChanged";
            this.EventClicked = "OnDsCollallClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsCollall");
            this.Register();
        }

        public void RetrieveCollall(String ls_member_no)
        {
            String sql = @"select	lm.loancontract_no,
		ct.loancolltype_desc,
		lc.ref_collno,
		lc.description,
		nvl( lm.principal_balance, 0 )+nvl( lm.withdrawable_amt, 0 ) as prnbal_amt,
		lc.collactive_percent,mb.resign_status,
		ROUND((case when lt.collreturnval_status = 1 then ( nvl( lm.principal_balance, 0 )+nvl( lm.withdrawable_amt, 0 ) ) * ( lc.collactive_percent / lc.collbase_percent ) else
		lc.collactive_amt / (lc.collbase_percent / 100) end),2) as collactive_amt
from	lncontmaster lm, lncontcoll lc,lnucfloancolltype ct,lnloantype lt,mbmembmaster mb
where	( lm.coop_id				= lc.coop_id )
and	( lm.loancontract_no	= lc.loancontract_no )
and	( ct.loancolltype_code	= lc.loancolltype_code )
and    ( lm.loantype_code = lt.loantype_code )
and    ( lc.ref_collno = mb.member_no )
and	( lm.contract_status > 0 )
and	( lm.coop_id	= {0} )
and	( lm.member_no	= {1} ) 
order by lc.loancontract_no
";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}