using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_cmdocumentcontrol_reset_ctrl
{
    public partial class DsMain : DataSourceFormView
    {

        public DataSet1.DataTable1DataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_save");
            this.Register();
        }
        public void retrieve()
        {
            string sql = "select * from cmprocessing where coop_id='" + state.SsCoopId + "'  and trunc(entry_Date)=to_date('" + state.SsWorkDate.ToString("dd/MM/yyyy") + "','dd/mm/yyyy') ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}