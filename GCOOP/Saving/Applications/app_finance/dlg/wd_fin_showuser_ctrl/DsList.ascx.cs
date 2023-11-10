using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.dlg.wd_fin_showuser_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.AMSECUSERSDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECUSERS;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }
        public void RetrieveDetail(string ls_coopid)
        {
            string sql = @" SELECT AMSECUSERS.COOP_ID,
                         AMSECUSERS.USER_NAME,   
                         AMSECUSERS.FULL_NAME  
                    FROM AMSECUSERS  
                   WHERE ( amsecusers.user_type = 1 ) AND  
                         ( AMSECUSERS.COOP_ID = {0} ) AND  
                         ( AMSECUSERS.POINT_OFCASH = 1 )    ";
            sql = WebUtil.SQLFormat(sql, ls_coopid);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}