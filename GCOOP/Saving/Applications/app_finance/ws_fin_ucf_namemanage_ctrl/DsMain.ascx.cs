using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.app_finance.ws_fin_ucf_namemanage_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FINCONSTANTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINCONSTANT;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
        }
        public void RetrieveData(string coop_id)
        {
            string sql = @"SELECT COOP_ID , manager_name , finanecial_name  ,accountant_name 
                FROM FINCONSTANT  
               WHERE ( COOP_ID = {0} )    ";
            sql = WebUtil.SQLFormat(sql, coop_id);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}
