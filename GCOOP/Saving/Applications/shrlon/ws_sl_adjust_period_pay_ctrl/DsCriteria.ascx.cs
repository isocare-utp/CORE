using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.shrlon.ws_sl_adjust_period_pay_ctrl
{
    public partial class DsCriteria : DataSourceFormView
    {
        public DataSet1.criterieDataTable DATA { get; set; }

        public void InitDsCriteria(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.criterie;
            this.EventItemChanged = "OnDsCriteriaItemChanged";
            this.EventClicked = "OnDsCriteriaClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsCriteria");
            this.Button.Add("b_retrieve");
            this.Register();
        }

        public void Ddloantype()
        {
            string sql = @"select loantype_code, 
                loantype_code||' - '||loantype_desc as loantype_desc, 1 as sorter
            from lnloantype 
            where coop_id = {0}
            union
            select '','',0 from dual order by sorter, loantype_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "sloantype_code", "loantype_desc", "loantype_code");
            this.DropDownDataBind(dt, "eloantype_code", "loantype_desc", "loantype_code");
        }
    }
}