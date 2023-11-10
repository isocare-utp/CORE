using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_approve_gen_contract_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Button.Add("b_clear");
            this.Button.Add("b_gencontno");
            this.Register();
        }

        public void DDloantype()
        {
            string sql = @"  select lnloantype.loantype_code,   
         lnloantype.loantype_code || ' ( ' || lnloantype.prefix || ' ) ' || lnloantype.loantype_desc as loantype_desc  ,
1 as sorter
    from lnloantype
   where ( lnloantype.coop_id = {0} ) and ( lnloantype.uselnreq_flag = 1 ) 
                           union
                           select '','',0 from dual order by sorter,loantype_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_code", "loantype_desc", "loantype_code");

        }
    }
}