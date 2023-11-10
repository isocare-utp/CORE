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
    public partial class DsClearlist : DataSourceRepeater
    {
        public DataSet1.LNLOANTYPECLRDataTable DATA { get; set; }

        public void InitDsClearlist(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPECLR;
            this.EventItemChanged = "OnDsClearlistItemChanged";
            this.EventClicked = "OnDsClearlistClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsClearlist");
            this.Button.Add("b_del");
            this.Register();
        }

        public void DdGrploanpermiss()
        {
            string sql = @" 
                select
                        loantype_code, prefix+'  '+loantype_desc as display, 1 as sorter
                        from lnloantype
                        union
                        select '','',0 from dual order by sorter, loantype_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_clear", "display", "loantype_code");

        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT 
                            LOANTYPE_CODE, 
                            LOANTYPE_CLEAR, 
                            MINPERIOD_PAY, 
                            MINPERCENT_PAY, 
                            CHKCONTCREDIT_FLAG, 
                            FINE_AMT, 
                            FINE_MAXAMT, 
                            FINE_PERCENT, 
                            FINECOND_TYPE, 
                            COOP_ID 
                            FROM LNLOANTYPECLR
                           WHERE loantype_code = {0}
                            ORDER BY LOANTYPE_CLEAR";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}