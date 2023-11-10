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
    public partial class DsDropln : DataSourceRepeater
    {
        public DataSet1.LNLOANTYPEPAUSEDataTable DATA { get; set; }

        public void InitDsDropln(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPEPAUSE;
            this.EventItemChanged = "OnDsDroplnItemChanged";
            this.EventClicked = "OnDsDroplnClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDropln");
            this.Button.Add("b_del");
            this.Register();
        }

        public void DdGrploanpermiss()
        {
            string sql = @" 
                select
                        loantype_code, loantype_code+'  '+prefix+'  '+loantype_desc as display, 1 as sorter
                        from lnloantype
                        union
                        select '','',0 from dual order by sorter, loantype_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_pause", "display", "loantype_code");

        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT *  FROM LNLOANTYPEPAUSE  
                           WHERE loantype_code = {0}";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}