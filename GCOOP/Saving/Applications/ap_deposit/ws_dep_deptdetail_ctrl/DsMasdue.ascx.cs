using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl
{
    public partial class DsMasdue : DataSourceRepeater
    {
        public DataSet1.DPDEPTMASDUEDataTable DATA { get; set; }

        public void InitDsMasdue(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMASDUE;
            this.EventItemChanged = "OnDsMasdueItemChanged";
            this.EventClicked = "OnDsMasdueClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMasdue");
            this.Register();
        }
        public void RetrieveData(string dept_no)
        {
            string sql = @"  
                select * from dpdeptmasdue where coop_id={0} and deptaccount_no ={1} and still_use='Y'
                ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dept_no);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}