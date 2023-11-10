using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cfloantype_ctrl
{
    public partial class DsCmSalbal : DataSourceRepeater
    {
        public DataSet1.CMUCFSALARYBALANCEDataTable DATA { get; set; }

        public void InitDsCmSalbal(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMUCFSALARYBALANCE;
            this.EventItemChanged = "OnDsCmSalbalItemChanged";
            this.EventClicked = "OnDsCmSalbalClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsCmSalbal");
            this.Register();
        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"SELECT * FROM CMUCFSALARYBALANCE WHERE salarybal_code = {0}";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdCmSalarybal()
        {
            string sql = @"select salarybal_code, salarybal_code +' '+ salarybal_desc as display, 1 as sorter
                  from cmucfsalarybalance
                  union
                  select '','',0 from dual order by sorter, salarybal_code ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "salarybal_code", "display", "salarybal_code");

        }
    }
}