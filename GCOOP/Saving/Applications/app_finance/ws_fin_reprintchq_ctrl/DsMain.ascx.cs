using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_reprintchq_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }
        public void Ddbank()
        {
            string sql = @"
                SELECT BANK_CODE as value_code,BANK_DESC as value_desc,1 as sorter  
                FROM CMUCFBANK 
                WHERE BANK_CODE IN(SELECT BANK_CODE FROM FINCHEQUEMAS WHERE AVAILABLE_FLAG=1 )";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "bank_code", "display", "value_code");
        }
        public void Ddbankbranch(string bank)
        {
            string sql = @"
                SELECT BRANCH_ID as value_code,BRANCH_NAME as value_desc,1 as sorter 
                FROM CMUCFBANKBRANCH  WHERE BANK_CODE = {0}
                AND BRANCH_ID IN(SELECT BANK_BRANCH FROM FINCHEQUEMAS WHERE AVAILABLE_FLAG=1 AND BANK_CODE = {0})";
            sql = WebUtil.SQLFormat(sql, bank);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sorter,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "branch_code", "display", "value_code");
        }
    }
}