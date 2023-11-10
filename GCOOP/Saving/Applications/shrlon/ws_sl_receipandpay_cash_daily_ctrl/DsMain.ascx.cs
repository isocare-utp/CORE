using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_receipandpay_cash_daily_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_sum");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
        public void DdEntryId()
        {
            string sql = @" 
              	      select user_name ,1 as sorter
                        from fintableusermaster
                        union 
                      select '',0 from dual order by sorter, user_name 
            ";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "entry_id", "user_name", "user_name");

        }
    }
}