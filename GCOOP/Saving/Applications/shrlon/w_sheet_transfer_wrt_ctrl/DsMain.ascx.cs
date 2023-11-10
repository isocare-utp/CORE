using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_transfer_wrt_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.wrtfundDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.wrtfund;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }


        public void DdWrtAccount()
        {
            string sql = @"select deptaccount_no,deptaccount_name ,1 as sorter from dpdeptmaster where deptaccount_no='0011001598'
union 
select '','',0 from dual order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "acc_id", "deptaccount_name", "deptaccount_no");
        }
        
    }
}