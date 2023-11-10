using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.keeping.ws_kp_opr_exp_textfile_ctrl
{
    public partial class DsSalary : DataSourceFormView
    {
        public DataSet1.DT_SALARYDataTable DATA { get; private set; }

        public void InitDsSalary(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_SALARY;
            this.EventItemChanged = "OnDsSalaryItemChanged";
            this.EventClicked = "OnDsSalaryClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "DsSalary");
            this.Register();
        }
    }
}