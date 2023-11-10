using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_estimated_payments_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNTEMPTABPAYINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNTEMPTABPAYIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_gen");
            this.Button.Add("b_print");
            this.Button.Add("b_pay");

            this.Register();
        }

        public void RetrieveMain()
        {
            String sql = @"select lnloantype.loantype_code,lnloantype.loantype_desc,interest_rate
from lnloantype,lncfloanintratedet
where lnloantype.coop_id = lncfloanintratedet.coop_id
and lnloantype.inttabrate_code = lncfloanintratedet.loanintrate_code
and lnloantype.coop_id = {0}

order by lnloantype.loantype_code ASC";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, state.SsWorkDate);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_code", "loantype_desc", "loantype_desc");
        }
    }
}