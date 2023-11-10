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
    public partial class DsReceipt : DataSourceRepeater
    {
        public DataSet1.DT_RECEIPTDataTable DATA { get; set; }

        public void InitDsReceipt(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_RECEIPT;
            this.InitDataSource(pw, Repeater2, this.DATA, "dsReceipt");
            this.EventItemChanged = "OnDsReceiptItemChanged";
            this.EventClicked = "OnDsReceiptClicked";
            //this.Button.Add("b_delprop");
            this.Register();

        }
        public void RetrieveReceipt(DateTime slip_date, string entry_id)
        {
            String sql = @"select sid.shrlontype_code,
			                      lt.loantype_desc,
			                      count( si.payinslip_no ) as count_item,
			                      sum( sid.principal_payamt ) as sum_prnamt,
			                      sum( sid.interest_payamt ) as sum_intamt
                        from	 slslippayin si, slslippayindet sid, lnloantype lt
                        where	( si.coop_id = sid.coop_id )
                        and		( si.payinslip_no = sid.payinslip_no )
                        and		( sid.concoop_id = lt.coop_id )
                        and		( sid.shrlontype_code = lt.loantype_code )
                        and		( si.moneytype_code = 'CSH' )
                        and		( si.slip_status = 1 )
                        and		( si.sliptype_code in ( 'PX','CLC' ) )
                        and		( si.slip_date = {0} )
                        and		( si.entry_id like {1} )
                        and		( sid.slipitemtype_code in ('LON','IAR') )
                        group by sid.shrlontype_code, lt.loantype_desc
                        order by sid.shrlontype_code  ";

            sql = WebUtil.SQLFormat(sql, slip_date, entry_id);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public TextBox CountItem
        {
            get { return this.cp_sum_count_item; }
        }

        public TextBox Allreceiptprnc
        {
            get { return this.cp_sum_allreceiptprnc; }
        }

        public TextBox Allreceiptint
        {
            get { return this.cp_sum_allreceiptint; }
        }

    }
}