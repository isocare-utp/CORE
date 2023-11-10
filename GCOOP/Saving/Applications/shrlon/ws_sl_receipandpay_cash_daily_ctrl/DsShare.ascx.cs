using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_receipandpay_cash_daily_ctrl
{
    public partial class DsShare : DataSourceRepeater
    {
        public DataSet1.DT_SHAREDataTable DATA { get; set; }

        public void InitDsShare(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_SHARE;
            this.InitDataSource(pw, Repeater2, this.DATA, "dsShare");
            this.EventItemChanged = "OnDsShareItemChanged";
            this.EventClicked = "OnDsShareClicked";
            //this.Button.Add("b_delprop");
            this.Register();

        }
        public void RetrieveShare(DateTime slip_date, string entry_id)
        {
            String sql = @"select sid.slipitemtype_code,
			                        sid.slipitem_desc,
			                        count( si.payinslip_no ) as count_item,
			                        sum( sid.item_payamt ) as sum_itemamt
                        from		slslippayin si, slslippayindet sid
                        where	( si.coop_id = sid.coop_id )
                        and		( si.payinslip_no	= sid.payinslip_no )
                        and		( si.moneytype_code = 'CSH' )
                        and		( si.slip_status = 1 )
                        and		( si.sliptype_code in ( 'PX','CLC' ) )
                        and		( si.slip_date = {0} )
                        and		( si.entry_id like {1} )
                        and		( sid.slipitemtype_code not in ('LON','IAR') )
                        group by sid.slipitemtype_code, sid.slipitem_desc
                        order by sid.slipitemtype_code";

            sql = WebUtil.SQLFormat(sql, slip_date, entry_id);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}