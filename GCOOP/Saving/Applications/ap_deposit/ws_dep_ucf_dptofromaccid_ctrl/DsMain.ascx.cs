using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_ucf_dptofromaccid_ctrl
{
    public partial class DsMain : DataSourceFormView
    {

        public DataSet1.CMUCFMONEYTYPEDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMUCFMONEYTYPE;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        }


        public void DDMoneyCode()
        {
            string sql = @"
                    SELECT moneytype_code AS VALUE_CODE,   
                    moneytype_desc AS VALUE_DESC,
                    1 as sort
                    FROM CMUCFMONEYTYPE  ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sort asc, value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "moneytype_code", "display", "value_code");
        }

    }
}



