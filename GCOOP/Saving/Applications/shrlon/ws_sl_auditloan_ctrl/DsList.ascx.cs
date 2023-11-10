using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_auditloan_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.dslistDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.dslist;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public TextBox principal_balance
        {
            get { return this.cp_sumprincipal; }
        }

        public void RetrieveList(string member_no)
        {
            string sql = @"select lncontmaster.loancontract_no,   
	            lncontmaster.loantype_code,    
	            lncontmaster.principal_balance  
            from lncontmaster  
            where ( lncontmaster.coop_id = {0} )
	            and ( lncontmaster.member_no = {1} ) 
            order by  lncontmaster.principal_balance desc,lncontmaster.contract_status desc ,loantype_code,loancontract_no desc";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}