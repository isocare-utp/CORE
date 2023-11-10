using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfdepartment_ctrl
{
    public partial class DsList : DataSourceRepeater 
    {
        public DataSet1.MBUCFDEPARTMENTDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw) 
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFDEPARTMENT;
            this.Button.Add("b_del");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnListItemChanged";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void retrieve() {
            string sql = "select * from MBUCFDEPARTMENT order by department_code asc";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        
        }
    }
}