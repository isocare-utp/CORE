using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_cremation_detail_ctrl
{
    public partial class DsSum : DataSourceFormView
    {
        public DataSet1.DT_SUMDataTable DATA { get; set; }

        public void InitDsSum(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_SUM;
            this.EventItemChanged = "OnDsSumItemChanged";
            this.EventClicked = "OnDsSumClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsSum");
            this.Register();
        }

        public void Retrieve_Sum(string memb_no)
        {
            string sql = @"select sum(item_amount) as sum_amt from mbcremationdet where coop_id= {0} and member_no= {1}";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId ,memb_no);
            Sdt dt = WebUtil.QuerySdt(sql);
            this.ImportData(dt);
        }
    }
}