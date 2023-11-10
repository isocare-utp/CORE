using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_procdeptuptran_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataDsMainDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataDsMain;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_retrive");
            this.Button.Add("b_process");
            this.Register();
        }

        public void DD_RecpPayType()
        {
            string sql = @"SELECT 
                RECPPAYTYPE_CODE AS VALUE_CODE,    
                RECPPAYTYPE_DESC AS VALUE_DESC,
                ORDER_SORT 
                FROM DPUCFRECPPAYTYPE WHERE SHOWTRAN_STATUS=1
                AND  ACTIVE_FLAG = 1";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            dt.Columns.Add("sort", typeof(System.Int32));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
                row["sort"] = 1;
            }
            dt.Rows.Add(new Object[] { "", "",0, "--กรุณาเลือก--", 0 });
            dt.DefaultView.Sort = "sort , order_sort , value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "system_code", "display", "value_code");
        }
    }
}