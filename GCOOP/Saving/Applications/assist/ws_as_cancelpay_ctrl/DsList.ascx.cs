using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
namespace Saving.Applications.assist.ws_as_cancelpay_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }
        public void RetrieveList(string sqlsearch)
        {
            String sql = @" select 
		                            po.member_no,
		                            dbo.ft_getmemname( mb.coop_id, mb.member_no ) as mbname,
		                            po.assistslip_no,
		                            po.slip_date,
		                            po.payout_amt,
		                            po.asscontract_no,
		                            po.pay_period,
		                            po.assisttype_code,
                                    po.bflastpay_date
                            from	assslippayout po
		                            join mbmembmaster mb on po.member_no = mb.member_no
                            where   po.slip_status = 1
                            and     po.coop_id = {0} " + sqlsearch + @"
                            order by po.assisttype_code, po.assistslip_no";
                            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
                            DataTable dt = WebUtil.Query(sql);
                            this.ImportData(dt);
        }
    }
}