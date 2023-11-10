using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.dlg.ws_dlg_sl_addloantype_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_clear");
            this.Button.Add("b_add");
            this.Button.Add("b_cancel");
            this.Register();
        }

        public void Ddloantype()
        {
            string sql = @"select
                        loantype_code, loantype_code||' - '||loantype_desc as display, 1 as sorter
                        from lnloantype
                        union
                        select '','',0 from dual order by sorter, loantype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "usepattern_lncode", "display", "loantype_code");

        }
    }
}