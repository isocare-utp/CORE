using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_controlcash_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.AMSECUSERSDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.AMSECUSERS;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }
        public void RetrieveList()
        {
            String sql = @"  SELECT FINTABLEUSERMASTER.USER_NAME,   
                             FINTABLEUSERMASTER.OPDATEWORK,   
                             FINTABLEUSERMASTER.STATUS  
                        FROM FINTABLEUSERMASTER  
                       WHERE ( FINTABLEUSERMASTER.COOP_ID = {0} ) AND  
                             ( FINTABLEUSERMASTER.OPDATEWORK = {1} ) order by USER_NAME";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId,state.SsWorkDate);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}