using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;
using Saving.Applications.mbshr_const.ws_mb_mbucfmemgrp_ctrl;

namespace Saving.Applications.mbshr_const.ws_mb_ucfmemgrp_ctrl.w_dlg_sl_searchmembgroup_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MBUCFMEMBGROUPDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGROUP;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetrieveList(string sql_search)
        {
            string sql = @"select coop_id,
                                membgroup_code,
                                membgroup_control,
                                membgroup_desc,
                                membgroup_agentgrg 
                           from mbucfmembgroup
                          where (coop_id={0})" + sql_search + @" order by membgroup_code ASC";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}