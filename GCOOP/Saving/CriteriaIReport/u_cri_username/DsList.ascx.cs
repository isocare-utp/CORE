using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.CriteriaIReport.u_cri_username
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.AMSECUSERSDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECUSERS;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void retrieve()
        {

            string sql = @"SELECT USER_NAME,FULL_NAME  
FROM AMSECUSERS WHERE  USER_TYPE = 1  AND FREEZ_FLAG=0  ORDER BY USER_NAME ASC ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}