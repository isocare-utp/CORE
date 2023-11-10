using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr_const.ws_mb_mbucfmemgrpcontrol_ctrl.w_dlg_sl_searchmembgroupcontrol_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBUCFMEMBGROUPCONTROLDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGROUPCONTROL;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_save");
            this.Register();
        }
        public void Retrievedata(string membgroup_control)
        {
            string sql = @"
                         select *
                           from mbucfmembgroupcontrol
                          where (coop_id={0}) and (membgroup_control ={1})";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, membgroup_control);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);


        }     
    }
}