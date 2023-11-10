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
    public partial class DsPermdown : DataSourceRepeater
    {
        public DataSet1.LNLOANTYPEPERMDOWNDataTable DATA { get; set; }

        public void InitDsPermdown(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPEPERMDOWN;
            this.EventItemChanged = "OnDsPermdownItemChanged";
            this.EventClicked = "OnDsPermdownClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPermdown");
            this.Button.Add("b_del");
            this.Register();
        }

        public void DdTypedown()
        {
            string sql = @" 
                select
                        loantype_code, loantype_code+'  '+prefix+'  '+loantype_desc as display, 1 as sorter
                        from lnloantype
                        union
                        select '','',0 from dual order by sorter, loantype_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_down", "display", "loantype_code");

        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT *  FROM LNLOANTYPEPERMDOWN  
                           WHERE loantype_code = {0}";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}