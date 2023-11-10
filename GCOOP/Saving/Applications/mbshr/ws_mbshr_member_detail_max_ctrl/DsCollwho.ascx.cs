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
    public partial class DsCollwho : DataSourceRepeater
    {
        public DataSet1.DT_COLLWHODataTable DATA { get; set; }

        public void InitDsCollwho(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_COLLWHO;
            this.EventItemChanged = "OnDsCollwhoItemChanged";
            this.EventClicked = "OnDsCollwhoClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsCollwho");
            this.Register();
        }

        public void RetrieveCollwho(String ls_ref_no)
        {
            String sql = @"select	lm.member_no,
		mpre.prename_desc,mb.memb_name,mb.memb_surname,
		( sm.sharestk_amt * 10 ) as sharestk_value,
		lc.loancontract_no,
		nvl( lm.principal_balance, 0 )+nvl( lm.withdrawable_amt, 0 ) as prnbal_amt,
		lc.collactive_percent,mb.resign_status,
		round((case when lt.collreturnval_status = 1 then ( nvl( lm.principal_balance, 0 )+nvl( lm.withdrawable_amt, 0 ) ) * ( lc.collactive_percent / lc.collbase_percent ) else
		lc.collactive_amt / (lc.collbase_percent / 100) end), 2 ) as collactive_amt
from	lncontcoll lc, lncontmaster lm, lnloantype lt, mbmembmaster mb, mbucfprename mpre, shsharemaster sm
where	( lc.coop_id				= lm.coop_id )
and	( lc.loancontract_no	= lm.loancontract_no )
and	( lm.coop_id				= lt.coop_id )
and	( lm.loantype_code	= lt.loantype_code )
and	( lm.memcoop_id		= mb.coop_id )
and	( lm.member_no		= mb.member_no )
and	( lm.memcoop_id		= sm.coop_id )
and	( lm.member_no		= sm.member_no )
and	( mb.prename_code	= mpre.prename_code )
and	( lc.loancolltype_code	= '01' )
and	( lm.contract_status > 0 )
and	( lc.coop_id			= {0} )
and	( lc.ref_collno			= {1} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_ref_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}