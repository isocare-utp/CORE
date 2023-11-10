using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_fund_ucf_fundcollkeeprate_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FUNDCOLLKEEPTYPEDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FUNDCOLLKEEPTYPE;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void DdFundcolltype(string coop_control)
        {
            string sql = @"
            SELECT FUNDKEEPTYPE AS VALUE_CODE,   
            FUNDKEEPDESC AS VALUE_DESC,
            SORT as sort
            FROM FUNDCOLLKEEPTYPE WHERE FUNDCOLLKEEPTYPE.COOP_ID={0} ";
            sql = WebUtil.SQLFormat(sql, coop_control);
            DataTable dt = WebUtil.Query(sql);
            dt.Rows.Add(new Object[] { "", "--กรุณาเลือก--", 0 });
            dt.DefaultView.Sort = "sort asc, value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "FUNDKEEPTYPE", "value_desc", "value_code");
        }        
    }
}