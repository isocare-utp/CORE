using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_fund_process_payfundcoll_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; private set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetriveData(string coop_control)
        {
            string sql = @"
            select
            member_no
            ,fullname
            ,fundbalance
            ,approve_date
            ,principal_balance
            ,sharestk_amt
            from(
            select
            mb.member_no,dbo.ft_getmemname(mb.coop_id,mb.member_no)  as fullname,
            fund.fundbalance,fund.approve_date,
            sum(ln.principal_balance) as principal_balance,
            sum( distinct sh.sharestk_amt) as sharestk_amt
            from mbmembmaster mb
            left join mbucfprename mbp on mb.prename_code = mbp.prename_code
            left join lncontmaster ln on mb.member_no = ln.member_no
            left join shsharemaster sh on mb.member_no = sh.member_no
             inner join fundcollmaster fund on  mb.member_no = fund.member_no 
            where mb.coop_id = {0}
             and fund.fundbalance > 0
            group by mb.coop_id,mb.member_no,fund.fundbalance,fund.approve_date
            ) asd
            where principal_balance = 0
            order by member_no
            ";
            sql = WebUtil.SQLFormat(sql, coop_control);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}