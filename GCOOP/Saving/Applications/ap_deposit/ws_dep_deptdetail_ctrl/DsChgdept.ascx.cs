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
    public partial class DsChgdept : DataSourceRepeater
    {
        public DataSet1.DataDsChgMonthDataTable DATA { get; set; }

        public void InitDsChgdept(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataDsChgMonth;
            this.EventItemChanged = "OnDsChgdeptItemChanged";
            this.EventClicked = "OnDsChgdeptClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsChgdept");
            this.Register();
        }

        public void RetrieveData(string dept_no)
        {
            string sql = @"  
                select dpreqchg_dept.dpreqchg_doc,dpreqchg_dept.deptaccount_no,dpdeptmaster.member_no,dpdeptmaster.deptaccount_name,
                dpreqchg_dept.deptmontchg_date,dpreqchg_dept.deptmonth_oldamt,dpreqchg_dept.deptmonth_newamt,dpreqchg_dept.reqchg_date
                from dpreqchg_dept 
                inner join dpdeptmaster on dpreqchg_dept.deptaccount_no = dpdeptmaster.deptaccount_no
                where dpreqchg_dept.coop_id = {0} and dpreqchg_dept.deptaccount_no = {1}
                and dpreqchg_dept.approve_flag=1 order by dpreqchg_dept.reqchg_date,dpreqchg_dept.deptaccount_no
                ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dept_no);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}