using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_ucfassisttypedet_ctrl
{
    public partial class DsCopy : DataSourceRepeater
    {
        public DataSet1.ASSUCFASSISTTYPEDETDataTable DATA { get; private set; }
        public void InitDsCopy(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSUCFASSISTTYPEDET;
            this.EventItemChanged = "OnDsCopyItemChanged";
            this.EventClicked = "OnDsCopyClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsCopy");
            this.Button.Add("b_del");
            this.Register();
        }


        public void RetrieveCopyData(int li_year)
        {
            string sql = @"select * from ASSUCFASSISTTYPEDET 
                           where assist_year = '" + li_year + "'";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}