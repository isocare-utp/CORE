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
    public partial class DsRightcollmast : DataSourceRepeater
    {
        public DataSet1.LNLOANTYPERIGHTCOLLDataTable DATA { get; set; }

        public void InitDsRightcollmast(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPERIGHTCOLL;
            this.EventItemChanged = "OnDsRightcollmastItemChanged";
            this.EventClicked = "OnDsRightcollmastClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsRightcollmast");
            this.Button.Add("b_del");
            this.Register();
        }

        public void DdColltype()
        {
            string sql = @" 
                        select
                        loancolltype_code, loancolltype_code+'  '+loancolltype_desc as display, 1 as sorter
                        from lnucfloancolltype
                        union
                        select '','',0 from dual order by sorter, loancolltype_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loancolltype_code", "display", "loancolltype_code");

        }

        public void DdCollmasttype()
        {
            string sql = @" 
                        select
                        collmasttype_code, collmasttype_code+'  '+collmasttype_desc as display, 1 as sorter
                        from lnucfcollmasttype
                        union
                        select '00','',0 from dual order by sorter, collmasttype_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "collmasttype_code", "display", "collmasttype_code");

        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT *  FROM LNLOANTYPERIGHTCOLL  
                           WHERE loantype_code = {0}";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}