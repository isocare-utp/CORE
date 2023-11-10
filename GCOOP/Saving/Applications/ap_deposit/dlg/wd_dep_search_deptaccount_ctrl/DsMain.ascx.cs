using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.dlg.wd_dep_search_deptaccount_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DTDEPTMASTERDataTable DATA { get; set; }

        public void InitdsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DTDEPTMASTER;
            this.InitDataSource(pw, FormViewMain, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_searchdeptacc");
            this.Register();
        }        
        public void DD_Depttype()
        {
            string sql = @"
                SELECT DEPTTYPE_CODE AS VALUE_CODE,   
                DEPTTYPE_DESC AS VALUE_DESC
                FROM DPDEPTTYPE WHERE COOP_ID = {0} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            dt.Columns.Add("sort", typeof(System.Int32));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
                row["sort"] = 1;
            }
            dt.Rows.Add(new Object[] { "", "", "--กรุณาเลือก--", 0 });
            dt.DefaultView.Sort = "sort asc, value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "depttype_code", "display", "value_code");           
        }
    }
}