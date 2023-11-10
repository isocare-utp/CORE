using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_cmcoopmaster_ctrl
{
    public partial class DsList : DataSourceFormView 
    {

        public DataSet1.CMCOOPMASTERDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMCOOPMASTER;
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsList");
            this.Register();
        }
        public void retrieve()
        {
            string sql = "select * from CMCOOPMASTER";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
       
    }
}