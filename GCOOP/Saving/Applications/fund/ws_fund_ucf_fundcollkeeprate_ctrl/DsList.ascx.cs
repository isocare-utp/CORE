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
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FUNDCOLLKEEPRATEDataTable DATA { get; private set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FUNDCOLLKEEPRATE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }
        public void RetriveData(string coop_control,string fund_type)
        {
            string sql = @"SELECT * FROM FUNDCOLLKEEPRATE  WHERE COOP_ID={0} AND FUNDKEEPTYPE ={1} 
            ORDER BY FUNDKEEPTYPE,LOANTYPE_CODE,SEQ_NO";
            sql = WebUtil.SQLFormat(sql, coop_control,fund_type);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdLoantype()
        {
            string sql = @"SELECT LOANTYPE_CODE AS VALUE_CODE,  
                    LOANTYPE_DESC AS VALUE_DESC,
                    1 AS SORT                             
                    FROM LNLOANTYPE  ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "",0,"--กรุณาเลือก--"});
            dt.DefaultView.Sort = "sort,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "loantype_code", "display", "value_code");            
        }
        public void DdFundcollrate(string coop_control, string fund_type)
        {
            string sql = @"
            SELECT FUNDCOLLKEEPTYPE.LOANTYPE_CODE AS VALUE_CODE,
            LNLOANTYPE.LOANTYPE_DESC AS VALUE_DESC,
            FUNDCOLLKEEPTYPE.SORT AS SORT
            FROM FUNDCOLLKEEPTYPE 
            INNER JOIN LNLOANTYPE ON FUNDCOLLKEEPTYPE.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE 
            WHERE FUNDCOLLKEEPTYPE.COOP_ID={0} AND FUNDCOLLKEEPTYPE.LOANTYPE_CODE={1}";
            sql = WebUtil.SQLFormat(sql, coop_control, fund_type);
            DataTable dt = WebUtil.Query(sql);
            dt.Rows.Add(new Object[] { "", "--กรุณาเลือก--", 0 });
            dt.DefaultView.Sort = "sort asc, value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "LOANTYPE_CODE", "value_desc", "value_code");
        }
    }
}