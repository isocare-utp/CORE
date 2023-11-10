using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.assist.dlg.wd_ass_set_calculator_ctrl
{
    public partial class DsMaster : DataSourceRepeater
    {
        public DataSet1.ASSUCFCALCULATORDataTable DATA { get; set; }

        public void InitDsMaster(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSUCFCALCULATOR;
            this.EventItemChanged = "OnDsMasterItemChanged";
            this.EventClicked = "OnDsMasterClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMaster");
            this.Register();
        }

        public void RetrieveCalculator()
        {
            string sql = "select calculator_type , calculator_desc from assucfcalculator  order by calculator_type ";
            //sql = WebUtil.SQLFormat(sql, state.SsCoopId);
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }


    }
}