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
    public partial class DsPayloan : DataSourceRepeater
    {
        public DataSet1.DT_PAYLOANDataTable DATA { get; set; }
        public void InitDsPayloan(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_PAYLOAN;
            this.InitDataSource(pw, Repeater2, this.DATA, "dsPayloan");
            this.EventItemChanged = "OnDsPayloanItemChanged";
            this.EventClicked = "OnDsPayloanClicked";
            //this.Button.Add("b_delprop");
            this.Register();

        }

        public void RetrievePayloan(DateTime slip_date, string entry_id)
        {
            String sql = @"select so.shrlontype_code,
			                    lt.loantype_desc,
			                    count( so.payoutslip_no ) as count_item,
			                    sum( so.payout_amt ) as sum_prnamt
                    from		slslippayout so, lnloantype lt
                    where	( so.concoop_id = lt.coop_id )
                    and		( so.shrlontype_code = lt.loantype_code )
                    and		( so.moneytype_code = 'CSH' )
                    and		( so.slip_status = 1 )
                    and		( so.sliptype_code in ( 'LWD' ) )
                    and		( so.slip_date = {0} )
                    and		( so.entry_id like {1} )
                    group by so.shrlontype_code, lt.loantype_desc
                    order by so.shrlontype_code";

            sql = WebUtil.SQLFormat(sql, slip_date, entry_id);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public TextBox CountReceiptall
        {
            get { return this.cp_sum_count_receiptall; }
        }

        public TextBox Allreceiptprnc
        {
            get { return this.cp_sum_allreceiptprnc; }
        }


    }
}