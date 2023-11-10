using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl
{
    public partial class DsInt : DataSourceFormView
    {
        public DataSet1.LNREQCONTADJUSTDETDataTable DATA { get; private set; }

        public void InitDsInt(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQCONTADJUSTDET;
            this.InitDataSource(pw, FormView1, this.DATA, "dsInt");
            this.EventItemChanged = "OnDsIntItemChanged";
            this.EventClicked = "OnDsIntClicked";
            //this.Button.Add("b_search");
            this.Register();
        }

        public void DdLnIntRate()
        {
            string sql = @"select loanintrate_code,
                loanintrate_code +' '+ loanintrate_desc as cp_loanintrate ,1 as sorter
            from lncfloanintrate
            where coop_id = {0} 
            union
            select  '','', 0  order by sorter,loanintrate_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "int_continttabcode", "cp_loanintrate", "loanintrate_code");
        }
    }
}