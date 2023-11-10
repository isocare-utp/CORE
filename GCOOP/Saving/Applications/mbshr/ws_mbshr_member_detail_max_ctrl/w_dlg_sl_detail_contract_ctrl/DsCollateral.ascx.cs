using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsCollateral : DataSourceRepeater
    {
        public DataSet1.DT_COLLATERALDataTable DATA { get; set; }

        public void InitDsCollateral(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_COLLATERAL;
            this.EventItemChanged = "OnDsCollateralItemChanged";
            this.EventClicked = "OnDsCollateralClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsCollateral");
            this.Register();
        }
        public void RetrieveCollateral(String ls_contno)
        {
            String sql = @"select	
		ct.loancolltype_desc,
		lc.ref_collno,
		lc.description,
		lc.collactive_percent,
		round((case when lt.collreturnval_status = 1 then ( nvl( lm.principal_balance, 0 )+nvl( lm.withdrawable_amt, 0 ) ) * ( lc.collactive_percent / lc.collbase_percent ) else
		lc.collactive_amt / (lc.collbase_percent / 100) end), 2 ) as collactive_amt
from	lncontmaster lm, lncontcoll lc,lnucfloancolltype ct, lnloantype lt
where	( lm.coop_id				= lc.coop_id )
and	( lm.loancontract_no	= lc.loancontract_no )
and	( lm.coop_id = lt.coop_id )
and	( lm.loantype_code = lt.loantype_code )
and	( lc.loancolltype_code	= ct.loancolltype_code )
and	( lm.coop_id	= {0} )
and	( lm.loancontract_no	= {1} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_contno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}