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
    public partial class DsPaymentlist : DataSourceRepeater
    {
        public DataSet1.LNLOANTYPEPERIODDataTable DATA { get; set; }

        public void InitDsPaymentlist(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPEPERIOD;
            this.EventItemChanged = "OnDsPaymentlistItemChanged";
            this.EventClicked = "OnDsPaymentlistClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPaymentlist");
            this.Button.Add("b_del");
            this.Register();
        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT *  FROM LNLOANTYPEPERIOD  
                           WHERE loantype_code = {0}";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}