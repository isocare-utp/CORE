using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cfloantype_ctrl
{
    public partial class DsIntspc : DataSourceRepeater
    {
        public DataSet1.LNLOANTYPEINTSPCDataTable DATA { get; set; }

        public void InitDsIntspc(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPEINTSPC;
            this.EventItemChanged = "OnDsIntspcItemChanged";
            this.EventClicked = "OnDsIntspcClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsIntspc");
            this.Button.Add("b_del");
            this.Register();
        }

        public void DdIntrate_code()
        {
            string sql = @" 
                select loanintrate_code,
                       loanintrate_code+'  '+loanintrate_desc as display,
                       1 as sorter
                  from lncfloanintrate
                  union
                  select '','',0  order by sorter, loanintrate_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "intratetab_code", "display", "loanintrate_code");

        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT *  FROM LNLOANTYPEINTSPC  
                           WHERE loantype_code = {0}";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}