using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_proc_moneyreturn_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_proc");
            this.Register();
        }

        public void DdSloantype()
        {
            string sql = @"select
                        loantype_code, loantype_code||' - '||loantype_desc as display, 1 as sorter
                        from lnloantype
                        union
                        select '','',0 from dual order by sorter, loantype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "sloantype_code", "display", "loantype_code");
        }

        public void DdEloantype()
        {
            string sql = @"select
                        loantype_code, loantype_code||' - '||loantype_desc as display, 1 as sorter
                        from lnloantype
                        union
                        select '','',0 from dual order by sorter, loantype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "eloantype_code", "display", "loantype_code");
        }
    }       
}