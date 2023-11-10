using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Saving.Applications.shrlon.ws_sl_recpay_wrt_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FINSLIPDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINSLIP;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }

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
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}