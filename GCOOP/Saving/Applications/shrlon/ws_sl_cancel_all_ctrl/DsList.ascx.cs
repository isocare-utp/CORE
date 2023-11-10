using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cancel_all_ctrl
{
    public partial class DsList :DataSourceRepeater
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string member_no)
        {
            string sql = @"
            select bizztb_type , bizzslip_no , bizzslip_date , bizzslip_amt
from(
select 'PIN' as bizztb_type , payinslip_no as bizzslip_no , operate_date as bizzslip_date , slip_amt as bizzslip_amt
from slslippayin si
where si.coop_id = {0}
and si.member_no = {1}
and si.slip_status = 1
union
select 'POT' as bizztb_type , payoutslip_no as bizzslip_no , operate_date as bizzslip_date , payoutnet_amt as bizzslip_amt
from slslippayout so
where so.coop_id = {0}
and so.member_no = {1}
and so.slip_status = 1
union
select 'KPM' as bizztb_type , receipt_no as bizzslip_no , receipt_date as bizzslip_date , receive_amt as bizzslip_amt
from kpmastreceive km
where km.coop_id = {0}
and km.member_no = {1}
and km.keeping_status = 1
)
order by bizzslip_date desc , bizzslip_no desc";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}