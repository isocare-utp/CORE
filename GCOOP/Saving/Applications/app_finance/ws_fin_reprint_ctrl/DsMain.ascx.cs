using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_reprint_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DTFINSLIPDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DTFINSLIP;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_retrieve");
            this.Register();
        }

        public void DDUsername()
        {
            string sql = "select user_name,1 as sort from amsecusers";
            DataTable dt = WebUtil.Query(sql);
            dt.Rows.Add(new Object[] { "--กรุณาเลือก--",0 });         
            dt.DefaultView.Sort = "sort asc, user_name asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "entry_id", "user_name", "user_name");
        }
    }
}