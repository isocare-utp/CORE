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
    public partial class DsIntspc : DataSourceRepeater
    {
        public DataSet1.LNREQCONTADJUSTINTSPCDataTable DATA { get; private set; }

        public void InitDsIntspc(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQCONTADJUSTINTSPC;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsIntspc");
            this.EventItemChanged = "OnDsIntspcItemChanged";
            this.EventClicked = "OnDsIntspcClicked";
            this.Button.Add("b_del");
            this.Register();
        }

        public void DdLnIntRate()
        {
            string sql = @"select loanintrate_code,
                loanintrate_code +' '+ loanintrate_desc as cp_loanintrate
            from lncfloanintrate
            where coop_id = {0} order by loanintrate_code
            ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "int_continttabcode", "cp_loanintrate", "loanintrate_code");
        }
    }
}