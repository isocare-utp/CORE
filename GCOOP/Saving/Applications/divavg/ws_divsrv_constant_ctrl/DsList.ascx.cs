using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_constant_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.YRCFRATEDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.YRCFRATE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();

        }
        public void RetrieveList()
        {
            string sql = @"   SELECT YRCFRATE.COOP_ID,   
                                     YRCFRATE.DIV_YEAR,   
                                     YRCFRATE.DIVPERCENT_RATE,   
                                     YRCFRATE.AVGPERCENT_RATE,   
                                     YRCFRATE.LOCKPROC_FLAG  
                                FROM YRCFRATE  
                               WHERE ( yrcfrate.coop_id = {0}) 
                                     order by  YRCFRATE.DIV_YEAR DESC";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}